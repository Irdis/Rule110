using Rule110.Gliders;
using NUnit.Framework;

namespace Rule110.Tests.Scenarios.Gliders;

[Tag("CGlider")]
public class CGliderTests : Rule110TestBase
{
    [TestCase(1, "c1_glider", 100)]
    public void GenerateC1Glider(int prefNum, string prefStr, int size)
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
        gliders.Add((2, new C1Glider()));
        scene.Init(gliders);
        scene.InitComplete();

        for (int j = 1; j < scene.Size; j++)
        {
            scene.Next();
        }
        scene.Complete();

        AssertImgsEqual(baselineImgName, actualImgName);
    }

    [TestCase(2, "c2_glider", 100)]
    public void GenerateC2Glider(int prefNum, string prefStr, int size)
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
        gliders.Add((2, new C2Glider()));
        scene.Init(gliders);
        scene.InitComplete();

        for (int j = 1; j < scene.Size; j++)
        {
            scene.Next();
        }
        scene.Complete();

        AssertImgsEqual(baselineImgName, actualImgName);
    }

    [TestCase(3, "c3_glider", 100)]
    public void GenerateC3Glider(int prefNum, string prefStr, int size)
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
        gliders.Add((2, new C3Glider()));
        scene.Init(gliders);
        scene.InitComplete();

        for (int j = 1; j < scene.Size; j++)
        {
            scene.Next();
        }
        scene.Complete();

        AssertImgsEqual(baselineImgName, actualImgName);
    }
}
