using NUnit.Framework;

namespace Rule110.Tests.Scenarios;

[Tag("random")]
public class RandomPatternTests : Rule110TestBase
{
    [TestCase(1, "neighbors", 300)]
    public void GenerateAllNumbers(int prefNum, string prefStr, int size)
    {
        SetupFolders(prefNum, prefStr);

        var random = new Random();
        for (int i = 0; i < 256; i++)
        {
            var background = new EmptyBackground();
            var actualImgName = GetImgActualPath(prefNum, prefStr, FormatNumber(i, 3));
            var galleryImgName = GetImgGalleryPath(prefNum, prefStr, FormatNumber(i, 3));

            var observers = new List<IObserver>
            {
                new ImgObserver(size, actualImgName),
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

            if (WriteToGallery)
            {
                File.Copy(actualImgName, galleryImgName, overwrite: true);
            }
        }
    }
}
