using Rule110.Gliders;
using NUnit.Framework;

namespace Rule110.Tests.Scenarios;

[Tag("EncoderJBlock")]
public class EncoderJBlockTests : Rule110TestBase
{
    [TestCase(1, "default")]
    public void GenerateEncoderJBlock(int prefNum, string prefStr)
    {
        SetupFolders(prefNum,  prefStr);

        var encoderFactory = new BlockEncoderFactory();

        var startTile = 5;
        var controlTile = 30;

        var baselineLst = new List<string>();
        var actualLst = new List<string>();

        for (int i = 0; i < EHatGlider.Size; i++)
        {
            var encoder = encoderFactory.Create(i);

            const int width = 1500;
            const int height = 1000;

            var background = new EtherBackground();

            var actualImgName = GetImgActualPath(prefNum,
                prefStr, FormatNumber(i));
            var baselineImgName = GetImgBaselinePath(prefNum,
                prefStr, FormatNumber(i));

            var observers = new List<IObserver>
            {
                new ImgObserver(width, height, actualImgName),
            };

            var scene = new Scene(width, background, observers);
            var gliders = new List<(int, IGlider)>();
            encoder.InsertEHat(gliders, i, startTile);
            encoder.EncodeJ(gliders);
            encoder.EncodeJ(gliders);

            scene.Init(gliders);

            scene.InitComplete();

            for (int j = 1; j < height; j++)
            {
                scene.Next();
            }
            scene.Complete();

            baselineLst.Add(baselineImgName);
            actualLst.Add(actualImgName);
        }

        AssertImgsEqual(baselineLst, actualLst);
    }
}
