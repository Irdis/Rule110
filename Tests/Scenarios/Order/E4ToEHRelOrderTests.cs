using Rule110.Gliders;
using NUnit.Framework;

namespace Rule110.Tests.Scenarios.Order;

[Tag("E4ToEHRelOrder")]
public class E4ToEHRelOrderTests : Rule110TestBase
{
    [TestCase(1, "default")]
    public void GenerateE4ToEHRelOrder(int prefNum, string prefStr)
    {
        SetupFolders(prefNum,  prefStr);

        var e4GliderType = 3;
        var e4GliderCollection = new ENGliderCollection(e4GliderType);
        var ehGliderCollection = new EHatGliderCollection();
        var c2GliderCollection = new C2GliderCollection();

        var startTile = 5;
        var controlTile = 15;

        var baselineLst = new List<string>();
        var actualLst = new List<string>();

        for (int k = 0; k < 2 * ENGlider.Size; k++)
        {
            const int width = 500;
            const int height = 500;

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
            var offset = startTile;
            var alignment = 0;
            var initialGlider = k / 2;

            var alignDelta = ENGlider.RightAlignment(initialGlider, e4GliderType);
            gliders.Add((offset, e4GliderCollection.Get(initialGlider)));
            offset += alignDelta;
            alignment += alignDelta;

            var (ehOffset, ehNumber) = E4ToEHGliderRelativeOrder.Next(initialGlider, k % 2);
            offset += ehOffset;

            alignDelta = EHatGlider.RightAlignment(ehNumber);
            gliders.Add((offset, ehGliderCollection.Get(ehNumber)));
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
