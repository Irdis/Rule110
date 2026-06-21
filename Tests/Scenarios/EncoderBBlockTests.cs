using Rule110.Gliders;
using NUnit.Framework;

namespace Rule110.Tests.Scenarios;

[Tag("EncoderBBlock")]
public class EncoderBBlockTests : Rule110TestBase
{
    [TestCase(1, "default")]
    public void GenerateEncoderBBlock(int prefNum, string prefStr)
    {
        SetupFolders(prefNum,  prefStr);

        var encoderFactory = new BlockEncoderFactory();

        var startTile = 5;
        var controlTile = 30;

        var baselineLst = new List<string>();
        var actualLst = new List<string>();

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
        encoder.EncodeB(gliders);

        encoder.EncodeA(gliders);
        encoder.EncodeB(gliders);

        encoder.EncodeA(gliders);
        encoder.EncodeA(gliders);
        encoder.EncodeB(gliders);

        encoder.EncodeA(gliders);
        encoder.EncodeA(gliders);
        encoder.EncodeA(gliders);
        encoder.EncodeB(gliders);

        scene.Init(gliders);

        scene.InitComplete();

        for (int j = 1; j < height; j++)
        {
            scene.Next();
        }
        scene.Complete();

        baselineLst.Add(baselineImgName);
        actualLst.Add(actualImgName);

        AssertImgsEqual(baselineLst, actualLst);
    }
}
