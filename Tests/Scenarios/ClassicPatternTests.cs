using NUnit.Framework;

namespace Rule110.Tests.Scenarios;

[Tag("classic")]
public class ClassicPattern : Rule110TestBase
{
    [TestCase(1, "small")]
    public void GenerateClassicPattern(int prefNum, string prefSuff)
    {
        const int size = 100;

        var actualImgName = GetImgActualPath(prefNum, prefSuff);
        var baselineImgName = GetImgBaselinePath(prefNum, prefSuff);
        SetupFolders(actualImgName, baselineImgName);

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
