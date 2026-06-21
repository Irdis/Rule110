using Rule110.Gliders;
using NUnit.Framework;

namespace Rule110.Tests.Scenarios;

[Tag("ehtoe1relorder")]
public class EHToE1RelOrderTest : Rule110TestBase
{
    [TestCase(1, "default")]
    public void GenerateEHRelOrder(int prefNum, string prefStr)
    {
        SetupFolders(prefNum,  prefStr);

        var e1GliderType = 0;
        var ehGliderCollection = new EHatGliderCollection();
        var e1GliderCollection = new ENGliderCollection(e1GliderType);
        var c2GliderCollection = new C2GliderCollection();

        var ehStartTile = 10;
        var controlTile = 15;

        var baselineLst = new List<string>();
        var actualLst = new List<string>();

        for (int i = 0; i < EHatGlider.Size; i++)
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
            var offset = ehStartTile;
            var alignment = 0;
            var initialGlider = i;

            var alignDelta = EHatGlider.RightAlignment(initialGlider);
            gliders.Add((offset, ehGliderCollection.Get(initialGlider)));
            offset += alignDelta;
            alignment += alignDelta;

            var (e1Offset, e1Number) = EHatToE1GliderRelativeOrder.Next(initialGlider);
            offset += e1Offset;

            alignDelta = ENGlider.RightAlignment(e1Number, e1GliderType);
            gliders.Add((offset, e1GliderCollection.Get(e1Number)));
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
