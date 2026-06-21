using Rule110.Gliders;
using NUnit.Framework;

namespace Rule110.Tests.Scenarios;

[Tag("C2Order")]
public class C2OrderTests : Rule110TestBase
{
    [TestCase(1, "default")]
    public void GenerateC2Order(int prefNum, string prefStr)
    {
        SetupFolders(prefNum,  prefStr);

        var eh = new EHatGlider();
        var c2 = new C2Glider();
        var analyzer = new GliderAnalyzer();
        var c2Lst = analyzer.Analyze(c2);
        var startGlider = 5;

        var startTile = 25;
        var controlTile = 30;

        var baselineLst = new List<string>();
        var actualLst = new List<string>();

        for (int i = 0; i < 20; i++)
        {
            const int width = 700;
            const int height = 700;

            var actualImgName = GetImgActualPath(prefNum,
                prefStr, FormatNumber(i));
            var baselineImgName = GetImgBaselinePath(prefNum,
                prefStr, FormatNumber(i));

            var background = new EtherBackground();
            var observers = new List<IObserver>
            {
                new ImgObserver(width, height, actualImgName),
            };

            var scene = new Scene(width, background, observers);

            var gliders = new List<(int, IGlider)>();

            var (tileOffset, orderPosition) = C2Glider.Next(startGlider, i);
            gliders.Add((startTile + tileOffset, c2Lst[orderPosition]));
            gliders.Add((startTile, c2Lst[startGlider]));

            gliders.Add((controlTile, eh));

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
