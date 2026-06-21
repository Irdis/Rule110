using Rule110.Gliders;
using NUnit.Framework;

namespace Rule110.Tests.Scenarios;

[Tag("EHRelOrder")]
public class EHRelOrderTests : Rule110TestBase
{
    [TestCase(1, "default")]
    public void GenerateEHRelOrder(int prefNum, string prefStr)
    {
        SetupFolders(prefNum,  prefStr);

        var ehGliderCollection = new EHatGliderCollection();
        var c2GliderCollection = new C2GliderCollection();

        var initialGlider = 1;

        var a4StartTile = 30;
        var ehStartTile = 50;

        var baselineLst = new List<string>();
        var actualLst = new List<string>();

        for (int i = 0; i < 100; i++)
        {
            const int width = 300;
            const int height = 300;

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
            var offset = 5;
            var alignment = 0;

            var alignDelta = EHatGlider.RightAlignment(initialGlider);
            gliders.Add((offset, ehGliderCollection.Get(initialGlider)));
            offset += alignDelta;
            alignment += alignDelta;

            var initialTriangleCount = EHatGliderRelativeOrder.InitialNumberOfTriangles[i % 8];
            var (ehOffset, ehNumber) = EHatGliderRelativeOrder.Next(initialGlider, i / 8 + initialTriangleCount, i % 8);
            offset += ehOffset;

            alignDelta = EHatGlider.RightAlignment(ehNumber);
            gliders.Add((offset, ehGliderCollection.Get(ehNumber)));
            offset += alignDelta;
            alignment += alignDelta;

            offset = 40 + alignment;

            gliders.Add((offset, c2GliderCollection.Get(0)));
            offset += alignDelta;
            alignment += alignDelta;

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
