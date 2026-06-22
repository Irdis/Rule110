using Rule110.Gliders;
using NUnit.Framework;

namespace Rule110.Tests.Scenarios.Gliders;

[Tag("AGlider")]
public class AGliderTests : Rule110TestBase
{
    [TestCase(1, "an", 100, 1)]
    [TestCase(2, "an", 100, 2)]
    [TestCase(3, "an", 100, 3)]
    [TestCase(4, "an", 100, 5)]
    [TestCase(5, "an", 100, 10)]
    [TestCase(5, "an", 100, 20)]
    public void GenerateANGlider(int prefNum, string prefStr, int size, int n)
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
        gliders.Add((1, new ANGlider(n - 1)));
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
