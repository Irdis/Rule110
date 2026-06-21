using NUnit.Framework;

namespace Rule110.Tests;

public class Rule110TestBase
{
    public bool IsRecording 
    { 
        get => Rule110TestConfigProvider.Instance.Mode == TestMode.Recording;
    }

    public bool WriteToGallery 
    { 
        get => Rule110TestConfigProvider.Instance.WriteToGallery;
    }

    protected string GetPrefix(int prefNum,
        string prefStr
    ) => $"{FormatNumber(prefNum)}_{prefStr}";

    protected string GetImageName(int prefNum,
        string prefStr,
        int suffStr
    ) => GetImageName(prefNum, prefStr, FormatNumber(suffStr));

    protected string GetImageName(int prefNum,
        string prefStr,
        string suffStr = null,
        string ext = null
    ) => $"{GetPrefix(prefNum, prefStr)}{(suffStr == null ? null : "_" + suffStr)}.{(ext == null ? "bmp" : ext)}";

    protected string FormatNumber(int number, int? n = null) => number.ToString(n == null ? "D2" : "D" + n);

    protected string GetImgGalleryPath(int prefNum,
        string prefStr,
        string suffStr = null,
        string ext = null
    ) => GetImagePath(OutputType.Gallery, GetImageName(prefNum, prefStr, suffStr));

    protected string GetImgBaselinePath(int prefNum,
        string prefStr,
        string suffStr = null,
        string ext = null
    ) => GetImagePath(OutputType.Baseline, GetImageName(prefNum, prefStr, suffStr));

    protected List<string> GetBaselineXByYImgNames(int prefNum,
        string prefStr,
        int x, int y)
    {
        var result = new List<string>(x * y);
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                var baselineImg = GetImgBaselinePath(prefNum,
                    prefStr, 
                    suffStr: $"{i}x{j}");
                result.Add(baselineImg);
            }
        }
        return result;
    }

    protected string GetImgActualPath(int prefNum,
        string prefStr,
        string suffStr = null,
        string ext = null
    ) => GetImagePath(OutputType.Actual, GetImageName(prefNum, prefStr, suffStr));

    protected string GetImgActualFolder() => GetImageFolder(OutputType.Actual);

    protected string GetImgBaselineFolder() => GetImageFolder(OutputType.Baseline);

    protected string GetImgGalleryFolder() => GetImageFolder(OutputType.Gallery);

    protected string GetImageFolder(OutputType type) => 
        Path.Combine("Tests", type.ToString(), GetTagName());

    protected string GetImagePath(OutputType type, string imageName) => 
        Path.Combine("Tests", type.ToString(), GetTagName(), imageName);

    protected void AssertImgsEqual(string baseline, string actual) =>
        AssertImgsEqual([baseline], [actual]);

    protected void AssertImgsEqual(List<string> baseline, List<string> actual)
    {
        var config = Rule110TestConfigProvider.Instance;
        if (config.Mode == TestMode.Recording)
        {
            for (int i = 0; i < baseline.Count; i++)
            {
                File.Copy(actual[i], baseline[i], overwrite: true);
            }
            return;
        }
        for (int i = 0; i < baseline.Count; i++)
        {
            AssertFilesAreEqual(baseline[i], actual[i]);
        }
    }

    public static void AssertFilesAreEqual(string expectedPath, string actualPath)
    {
        const int bufferSize = 8192;

        using FileStream fs1 = File.OpenRead(expectedPath);
        using FileStream fs2 = File.OpenRead(actualPath);

        Assert.AreEqual(fs1.Length, fs2.Length, 
            FormatFileMistmatch("File sizes differ", expectedPath, actualPath));

        byte[] buffer1 = new byte[bufferSize];
        byte[] buffer2 = new byte[bufferSize];

        int read1, read2;
        long offset = 0;

        while ((read1 = fs1.Read(buffer1, 0, bufferSize)) > 0)
        {
            read2 = fs2.Read(buffer2, 0, bufferSize);

            Assert.AreEqual(read1, read2, 
                FormatFileMistmatch("Read size mismatch", expectedPath, actualPath));

            for (int i = 0; i < read1; i++)
            {
                if (buffer1[i] != buffer2[i])
                {
                    Assert.Fail(FormatFileMistmatch($"Files differ at byte {offset + i}",
                            expectedPath, actualPath));
                }
            }

            offset += read1;
        }
    }

    private static string FormatFileMistmatch(string err,
        string expectedPath,
        string actualPath) => 
        err + Environment.NewLine + 
        "expectedFile: " + expectedPath + Environment.NewLine +
        "actualFile: " + actualPath;

    protected void SetupFolders(int prefNum,
        string prefStr) 
    {
        var baselineFolder = GetImgBaselineFolder();
        var actualFolder = GetImgActualFolder();

        Directory.CreateDirectory(baselineFolder);
        Directory.CreateDirectory(actualFolder);

        var filePrefix = GetPrefix(prefNum, prefStr);
        if (IsRecording)
        {
            CleanupFilesWithPrefix(filePrefix, baselineFolder);
        }

        if (WriteToGallery && HasGalleryAttribute())
        {
            var galleryFolder = GetImgGalleryFolder();
            Directory.CreateDirectory(galleryFolder);
            CleanupFilesWithPrefix(filePrefix, galleryFolder);
        }
    }

    private void CleanupFilesWithPrefix(string prefix, string folder)
    {
        foreach (var fileToDelete in Directory.EnumerateFiles(folder)
            .Where(f => Path.GetFileName(f).StartsWith(prefix)))
        {
            File.Delete(fileToDelete);
        }
    }

    private bool HasGalleryAttribute()
    {
        var fixtureType = TestContext.CurrentContext.Test.Type;

        return (GalleryAttribute)fixtureType
            .GetCustomAttributes(typeof(GalleryAttribute), true)
            .FirstOrDefault() != null;
    }

    private string GetTagName()
    {
        var fixtureType = TestContext.CurrentContext.Test.Type;

        var tag = (TagAttribute)fixtureType
            .GetCustomAttributes(typeof(TagAttribute), true)
            .First();

        return tag.Name;
    }
}
