using Rule110.Gliders;
using NUnit.Framework;

namespace Rule110.Tests.Scenarios;

[Tag("ehorder")]
public class EHOrderTests : Rule110TestBase
{
    [TestCase(1, "default")]
    public void GenerateEHOrder(int prefNum, string prefStr)
    {
        SetupFolders(prefNum,  prefStr);

        var eh1 = new EHatGlider();
        var c2 = new C2Glider();
        var analyzer = new GliderAnalyzer();
        var ehLst = analyzer.Analyze(eh1);
        var c2Lst = analyzer.Analyze(c2);

        var eInd = 20;
        var cInd = 0;

        var c2gap = 6;
        var eDist = 50;

        var c2StartTile = 15;
        var ehStartTile = 30;
        var controlTile = 50;

        var baselineLst = new List<string>();
        var actualLst = new List<string>();

        for (int k = 3; k < 30; k++)
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
            var (offset1, gliderIndex1) = EHatGlider.Next(eInd, eDist);
            var (offset2, gliderIndex2) = EHatGlider.Next(eInd, eDist + k);
            var align2 = EHatGlider.RightAlignment(gliderIndex2);
            var align1 = EHatGlider.RightAlignment(gliderIndex1);
            var (offset3, gliderIndex3) = C2Glider.Next(cInd, c2gap);

            var gliders = new List<(int, IGlider)>();
            var alignTotal = 0;

            gliders.Add((c2StartTile + offset3, c2Lst[gliderIndex3]));

            gliders.Add((c2StartTile, c2Lst[cInd]));

            gliders.Add((ehStartTile + offset2, ehLst[gliderIndex2]));
            alignTotal += align2;

            gliders.Add((ehStartTile + offset1 + alignTotal, ehLst[gliderIndex1]));
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
