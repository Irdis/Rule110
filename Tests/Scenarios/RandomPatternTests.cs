using NUnit.Framework;

namespace Rule110.Tests.Scenarios;

[Tag("random")]
public class RandomPatternTests : Rule110TestBase
{
    [TestCase(1, "rand110", 110, 300)]
    [TestCase(1, "rand109", 109, 300)]
    [TestCase(1, "rand30", 30, 300)]
    [TestCase(1, "rand90", 90, 300)]
    [TestCase(1, "rand184", 184, 300)]
    public void GenerateRandomPattern(int prefNum, string prefStr, int ruleNumber, int size)
    {
        var random = new Random();
        for (int i = 0; i < 3; i++)
        {
            var background = new EmptyBackground();
            var actualImgName = GetImgActualPath(prefNum, prefStr, FormatNumber(i));
            var baselineImgName = GetImgBaselinePath(prefNum, prefStr, FormatNumber(i));

            var observers = new List<IObserver>
            {
                new ImgObserver(size, actualImgName),
            };
            var scene = new Scene(size, background, observers, ruleNumber);

            var gliders = new List<(int, IGlider)>();
            scene.Init(gliders);

            for(int j = 0; j < size; j++)
            {
                scene.SetState(j, random.Next(2));
            }

            scene.InitComplete();

            for (int j = 1; j < scene.Size; j++)
            {
                scene.Next();
            }
            scene.Complete();

            Assert.True(File.Exists(actualImgName));

            // File.Copy(actualImgName, baselineImgName, overwrite: true);
        }
    }

    // 18, 22, 26, 30, 45, 54, 57, 60, 62, 73, 75, 90, 101, 105, 106, 122, 126
    // [TestCase(2, "all", 300)]
    public void GenerateAllNumbers(int prefNum, string prefStr, int size)
    {
        var random = new Random();
        for (int i = 0; i < 256; i++)
        {
            var background = new EmptyBackground();
            var actualImgName = GetImgActualPath(prefNum, prefStr, FormatNumber(i, 3));
            var baselineImgName = GetImgBaselinePath(prefNum, prefStr, FormatNumber(i, 3));

            var observers = new List<IObserver>
            {
                new ImgObserver(size, actualImgName, blockSize: 3),
            };
            var scene = new Scene(size, background, observers, i);

            var gliders = new List<(int, IGlider)>();
            scene.Init(gliders);

            for(int j = 0; j < size; j++)
            {
                scene.SetState(j, random.Next(2));
            }

            scene.InitComplete();

            for (int j = 1; j < scene.Size; j++)
            {
                scene.Next();
            }
            scene.Complete();

            Assert.True(File.Exists(actualImgName));
        }
    }
}
