using Rule110.Gliders;
using NUnit.Framework;

namespace Rule110.Tests.Scenarios.Order;

[Tag("A4EHCrossing")]
public class A4EHCrossingTests : Rule110TestBase
{
    [TestCase(1, "default")]
    public void GenerateA4EHCrossing(int prefNum, string prefStr)
    {
        SetupFolders(prefNum,  prefStr);

        var eh1 = new EHatGlider();
        var c2 = new C2Glider();
        var a4 = new ANGlider(3);
        var analyzer = new GliderAnalyzer();
        var ehLst = analyzer.Analyze(eh1);
        var c2Lst = analyzer.Analyze(c2);
        var a4Lst = analyzer.Analyze(a4);

        var eGap = -20;

        var eInd = 0;
        var aInd = 2;

        var a4StartTile = 30;
        var ehStartTile = 50;

        var baselineLst = new List<string>();
        var actualLst = new List<string>();

        for (int k = 0; k < 50; k++)
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
            var (offset1, gliderIndex1) = EHatGlider.Next(eInd, -1);
            var (offset2, gliderIndex2) = EHatGlider.Next(eInd, eGap - k);
            var align2 = EHatGlider.RightAlignment(gliderIndex2);
            var align1 = EHatGlider.RightAlignment(gliderIndex1);

            var gliders = new List<(int, IGlider)>();

            var offsetATotal = 0;

            var (offset3, gliderIndex3) = ANGlider.Next(aInd, -1);
            var align3 = ANGlider.RightAlignment(gliderIndex3);
            offsetATotal += offset3;

            var (offset4, gliderIndex4) = ANGlider.NextECrossing(gliderIndex3, 2);
            var align4 = ANGlider.RightAlignment(gliderIndex4);
            offsetATotal += offset4;

            var (offset5, gliderIndex5) = ANGlider.NextECrossing(gliderIndex4, 0);
            var align5 = ANGlider.RightAlignment(gliderIndex5);
            offsetATotal += offset5;

            var alignTotal = 0;

            gliders.Add((a4StartTile + offsetATotal + alignTotal, a4Lst[gliderIndex5]));
            alignTotal += align5;
            offsetATotal -= offset5;

            gliders.Add((a4StartTile + offsetATotal + alignTotal, a4Lst[gliderIndex4]));
            alignTotal += align4;
            offsetATotal -= offset4;

            gliders.Add((a4StartTile + offsetATotal + alignTotal, a4Lst[gliderIndex3]));
            alignTotal += align3;
            offsetATotal -= offset3;

            gliders.Add((ehStartTile + offset1 + alignTotal, ehLst[gliderIndex1]));
            alignTotal += align1;

            gliders.Add((ehStartTile + offset2 + alignTotal, ehLst[gliderIndex2]));
            alignTotal += align2;

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
