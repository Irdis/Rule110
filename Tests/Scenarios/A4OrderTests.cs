using Rule110.Gliders;
using NUnit.Framework;

namespace Rule110.Tests.Scenarios;

[Tag("a4order")]
public class A4OrderTests : Rule110TestBase
{
    [TestCase(1, "double")]
    public void GenerateDoubleA4Order(int prefNum, string prefStr)
    {
        SetupFolders(prefNum,  prefStr);

        var a4 = new ANGlider(3);
        var c2 = new C2Glider();
        var analyzer = new GliderAnalyzer();
        var a4Lst = analyzer.Analyze(a4);

        var aInd = 2;

        var a4StartTile = 15;
        var controlTile = 50;

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

            var gliders = new List<(int, IGlider)>();

            var (offset1, gliderIndex1) = ANGlider.NextA(aInd, -3);
            var align1 = ANGlider.RightAlignment(gliderIndex1);

            var (offset2, gliderIndex2) = ANGlider.NextA(gliderIndex1, -3 - k);
            var align2 = ANGlider.RightAlignment(gliderIndex2);

            var (offset3, gliderIndex3) = ANGlider.NextA(gliderIndex2, -3);
            var align3 = ANGlider.RightAlignment(gliderIndex3);

            var offset = a4StartTile;
            var align = 0;

            gliders.Add((offset, a4Lst[aInd]));

            offset += offset1;
            gliders.Add((offset, a4Lst[gliderIndex1]));
            offset += align1;
            align += align1;

            offset += offset2;
            gliders.Add((offset, a4Lst[gliderIndex2]));
            offset += align2;
            align += align2;

            offset += offset3;
            gliders.Add((offset, a4Lst[gliderIndex2]));
            offset += align3;
            align += align3;

            gliders.Add((controlTile + align, c2));

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
