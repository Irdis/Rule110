using Rule110.Gliders;
using NUnit.Framework;

namespace Rule110.Tests.Scenarios.Order;

[Tag("ENOrder")]
public class ENOrderTests : Rule110TestBase
{
    [TestCase(1, "default")]
    public void GenerateENOrder(int prefNum, string prefStr)
    {
        SetupFolders(prefNum,  prefStr);

        var enGliderType = 4;
        var en1 = new ENGlider(enGliderType);
        var c2 = new C2Glider();

        var analyzer = new GliderAnalyzer();
        var enLst = analyzer.Analyze(en1);
        var enInd = 1;

        var c2StartTile = 15;
        var enStartTile = 30;
        var controlTile = 50;

        var baselineLst = new List<string>();
        var actualLst = new List<string>();

        for (int k = 0; k < 40; k++)
        {
            const int width = 1000;
            const int height = 1000;

            var background = new EtherBackground();

            var actualImgName = GetImgActualPath(prefNum,
                prefStr, FormatNumber(k));
            var baselineImgName = GetImgBaselinePath(prefNum,
                prefStr, FormatNumber(k));

            var observers = new List<IObserver>
            {
                new ImgObserver(width, height, actualImgName),
            };

            var scene = new Scene(width, background, observers);
            var gliders = new List<(int, IGlider)>();

            var (offset1, gliderIndex1) = ENGlider.Next(enInd, k);
            var align1 = ENGlider.RightAlignment(gliderIndex1, enGliderType);

            var alignTotal = 0;

            gliders.Add((c2StartTile + alignTotal, c2));

            gliders.Add((enStartTile + offset1 + alignTotal, enLst[gliderIndex1]));
            alignTotal += align1;

            gliders.Add((controlTile + alignTotal, c2));

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
