using Rule110.Gliders;
using NUnit.Framework;

namespace Rule110.Tests.Scenarios.Order;

[Tag("E5ToE2RelOrder")]
public class E5ToE2RelOrderTests : Rule110TestBase
{
    [TestCase(1, "default")]
    public void GenerateE1ToE4RelOrder(int prefNum, string prefStr)
    {
        SetupFolders(prefNum,  prefStr);

        var e5GliderType = 4;
        var e2GliderType = 1;

        var e5GliderCollection = new ENGliderCollection(e5GliderType);
        var e2GliderCollection = new ENGliderCollection(e2GliderType);
        var c2GliderCollection = new C2GliderCollection();

        var startTile = 5;
        var controlTile = 30;

        var baselineLst = new List<string>();
        var actualLst = new List<string>();

        for (int i = 0; i < ENGlider.Size; i++)
        {
            const int width = 500;
            const int height = 500;

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
            var offset = startTile;
            var alignment = 0;
            var initialGlider = i;

            var alignDelta = ENGlider.RightAlignment(initialGlider, e5GliderType);
            gliders.Add((offset, e5GliderCollection.Get(initialGlider)));
            offset += alignDelta;
            alignment += alignDelta;

            var (e2Offset, e2Number) = E5ToE2GliderRelativeOrder.Next(initialGlider);
            offset += e2Offset;

            alignDelta = ENGlider.RightAlignment(e2Number, e2GliderType);
            gliders.Add((offset, e2GliderCollection.Get(e2Number)));
            offset += alignDelta;
            alignment += alignDelta;

            offset = controlTile + alignment;

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
