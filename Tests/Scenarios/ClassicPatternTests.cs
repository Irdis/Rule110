using NUnit.Framework;

namespace Rule110.Tests.Scenarios;

[Tag("ClassicPattern")]
public class ClassicPatternTests : Rule110TestBase
{
    [TestCase(1, "small", 10)]
    [TestCase(1, "normal", 100)]
    [TestCase(1, "big", 1000)]
    public void GenerateClassicPattern(int prefNum, string prefStr, int size)
    {
        SetupFolders(prefNum,  prefStr);

        var actualImgName = GetImgActualPath(prefNum, prefStr);
        var baselineImgName = GetImgBaselinePath(prefNum, prefStr);

        var background = new EmptyBackground();
        var observers = new List<IObserver>
        {
            new ImgObserver(size, actualImgName)
        };
        var scene = new Scene(size, background, observers);

        var gliders = new List<(int, IGlider)>();
        scene.Init(gliders);
        scene.FlipState(size - 1);
        scene.InitComplete();

        for (int j = 1; j < scene.Size; j++)
        {
            scene.Next();
        }
        scene.Complete();

        AssertImgsEqual(baselineImgName, actualImgName);
    }
}
