using Rule110.Gliders;
using NUnit.Framework;

namespace Rule110.Tests.Scenarios.Encoder;

[Tag("Encoder")]
public class EncoderTests : Rule110TestBase
{
    [TestCase(1, "small", "BCFGG")]
    [TestCase(2, "small", "BCDFFFF")]
    public void GenerateSmall(int prefNum, string prefStr, string pattern)
    {
        SetupFolders(prefNum, prefStr);

        var encoderFactory = new BlockEncoderFactory();

        var encoder = encoderFactory.Create();

        const int width = 1500;
        const int height = 1000;

        var background = new EtherBackground();

        var actualImgName = GetImgActualPath(prefNum,
            prefStr);
        var baselineImgName = GetImgBaselinePath(prefNum,
            prefStr);

        var observers = new List<IObserver>
        {
            new ImgObserver(width, height, actualImgName),
        };

        var scene = new Scene(width, background, observers);
        var gliders = new List<(int, IGlider)>();
        var blocks = BlockEncoder.Parse(pattern);
        encoder.Encode(blocks, gliders);

        scene.Init(gliders);

        scene.InitComplete();

        for (int j = 1; j < height; j++)
        {
            scene.Next();
        }
        scene.Complete();

        AssertImgsEqual(baselineImgName, actualImgName);
    }

    [TestCase(3, "big")]
    public void GenerateBig(int prefNum, string prefStr)
    {
        SetupFolders(prefNum, prefStr);

        string pattern = @"
                B 13A B 11A B 12A B 336A
                B 13A B 11A B 12A B 336A
                B 13A B 11A B 12A B
                (C E D F G)
                H J I I I I I I I I I I K
                H J I I I I I I I I I I K
            ";

        var actualR110ImgName = GetImgActualPath(prefNum, prefStr, 
            ext: FileExt.R110);
        var actualTemplateName = GetImgActualPath(prefNum, prefStr, 
            suffStr: "{0}");

        var baselineLst = GetBaselineXByYImgNames(prefNum, prefStr,
            2, 1);
        var encoderFactory = new BlockEncoderFactory();

        var encoder = encoderFactory.Create();

        const int size = 30000;

        var background = new EtherBackground();

        var actualImgName = GetImgActualPath(prefNum,
            prefStr);
        var baselineImgName = GetImgBaselinePath(prefNum,
            prefStr);

        var observers = new List<IObserver>
        {
            new FileObserver(size, actualR110ImgName)
        };

        var scene = new Scene(size, background, observers);
        var gliders = new List<(int, IGlider)>();
        var blocks = BlockEncoder.Parse(pattern);
        encoder.Encode(blocks, gliders);

        scene.Init(gliders);

        scene.InitComplete();

        for (int j = 1; j < size; j++)
        {
            scene.Next();
        }
        scene.Complete();

        var cutter = new ImgCutter(actualR110ImgName);
        var actualLst = cutter.CutImages(actualTemplateName, 
            width: 10000,
            height: 20000,
            frameX: 15000,
            // frameY: 10000,
            frameWidth: 10000,
            frameHeight: 30000);

        Assert.AreEqual(baselineLst.Count, actualLst.Count);
        for (int i = 0; i < baselineLst.Count; i++)
        {
            Assert.AreEqual(
                Path.GetFileName(baselineLst[i]), 
                Path.GetFileName(actualLst[i])
            );
        }
        AssertImgsEqual(baselineLst, actualLst);
    }
}
