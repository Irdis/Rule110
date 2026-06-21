using Rule110.Gliders;
using NUnit.Framework;

namespace Rule110.Tests.Scenarios;

[Tag("Encoder")]
public class EncoderTests : Rule110TestBase
{
    [TestCase(1, "small", "BCFGG")]
    [TestCase(2, "small", "BCDFFFF")]
    public void GenerateSmall(int prefNum, string prefStr, string pattern)
    {
        SetupFolders(prefNum, prefStr);

        var encoderFactory = new BlockEncoderFactory();

        var encoder = encoderFactory.Create();

        const int width = 1500;
        const int height = 1000;

        var background = new EtherBackground();

        var actualImgName = GetImgActualPath(prefNum,
            prefStr);
        var baselineImgName = GetImgBaselinePath(prefNum,
            prefStr);

        var observers = new List<IObserver>
        {
            new ImgObserver(width, height, actualImgName),
        };

        var scene = new Scene(width, background, observers);
        var gliders = new List<(int, IGlider)>();
        var blocks = BlockEncoder.Parse(pattern);
        encoder.Encode(blocks, gliders);

        scene.Init(gliders);

        scene.InitComplete();

        for (int j = 1; j < height; j++)
        {
            scene.Next();
        }
        scene.Complete();

        AssertImgsEqual(baselineImgName, actualImgName);
    }
}
