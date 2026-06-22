using Rule110.Gliders;
using NUnit.Framework;

namespace Rule110.Tests.Scenarios.Gliders;

[Tag("HGlider")]
public class HGliderTests : Rule110TestBase
{
    [TestCase(1, "h_glider", 200)]
    public void GenerateGliderGun(int prefNum, string prefStr, int size)
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
        gliders.Add((5, new HGlider()));
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
