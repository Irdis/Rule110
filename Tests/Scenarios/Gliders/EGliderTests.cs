using Rule110.Gliders;
using NUnit.Framework;

namespace Rule110.Tests.Scenarios.Gliders;

[Tag("EGlider")]
public class EGliderTests : Rule110TestBase
{
    [TestCase(1, "eh_glider", 100)]
    public void GenerateEHatGlider(int prefNum, string prefStr, int size)
    {
        SetupFolders(prefNum, prefStr);

        var actualImgName = GetImgActualPath(prefNum, prefStr);
        var baselineImgName = GetImgBaselinePath(prefNum, prefStr);

        var background = new EtherBackground();
        var observers = new List<IObserver>
        {
            new ImgObserver(size, actualImgName)
        };
        var scene = new Scene(size, background, observers);

        var gliders = new List<(int, IGlider)>();
        gliders.Add((2, new EHatGlider()));
        scene.Init(gliders);
        scene.InitComplete();

        for (int j = 1; j < scene.Size; j++)
        {
            scene.Next();
        }
        scene.Complete();

        AssertImgsEqual(baselineImgName, actualImgName);
    }

    [TestCase(2, "en_glider", 300)]
    public void GenerateENGlider(int prefNum, string prefStr, int size)
    {
        SetupFolders(prefNum, prefStr);

        var actualLst = new List<string>();
        var baselineLst = new List<string>();

        for (int i = 0; i < 15; i++)
        {
            var actualImgName = GetImgActualPath(prefNum, prefStr, 
                suffStr: FormatNumber(i));
            var baselineImgName = GetImgBaselinePath(prefNum, prefStr,
                suffStr: FormatNumber(i));

            actualLst.Add(actualImgName);
            baselineLst.Add(baselineImgName);

            var background = new EtherBackground();
            var observers = new List<IObserver>
            {
                new ImgObserver(size, actualImgName)
            };
            var scene = new Scene(size, background, observers);

            var gliders = new List<(int, IGlider)>();
            gliders.Add((5, new ENGlider(i)));
            scene.Init(gliders);
            scene.InitComplete();

            for (int j = 1; j < scene.Size; j++)
            {
                scene.Next();
            }
            scene.Complete();
        }

        AssertImgsEqual(baselineLst, actualLst);
    }
}
