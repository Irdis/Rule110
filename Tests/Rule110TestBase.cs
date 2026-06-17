using NUnit.Framework;

namespace Rule110.Tests;

public class Rule110TestBase
{
    protected string GetSuffix(int prefNum, string prefStr) => 
        $"{FormatNumber(prefNum)}_{prefStr}";

    protected string GetImageName(int prefNum, string prefStr, int suffStr) => 
        GetImageName(prefNum, prefStr, FormatNumber(suffStr));

    protected string GetImageName(int prefNum, string prefStr, string suffStr = null) => 
        $"{GetSuffix(prefNum, prefStr)}{(suffStr == null ? null : "_" + suffStr)}.bmp";

    private string FormatNumber(int number) => number.ToString("D2");

    protected string GetImgBaselinePath(int prefNum, string prefStr) =>
        GetImagePath(OutputType.Baseline, GetImageName(prefNum, prefStr));

    protected string GetImgActualPath(int prefNum, string prefStr) =>
        GetImagePath(OutputType.Actual, GetImageName(prefNum, prefStr));

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

    protected void SetupFolders(params string[] fileNames) 
    {
        for (int i = 0; i < fileNames.Length; i++)
        {
            var fileName = fileNames[i];
            Directory.CreateDirectory(Path.GetDirectoryName(fileName));
        }
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
