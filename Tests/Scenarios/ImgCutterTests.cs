using NUnit.Framework;

namespace Rule110.Tests.Scenarios;

[Tag("ImgCutter")]
public class ImgCutterTests : Rule110TestBase
{
    [TestCase(1, "4piece", 100, 0, 0, true)]
    [TestCase(2, "4piece", 100, 10, 20, true)]
    [TestCase(3, "4piece", 100, 10, 20, false)]
    public void CutEntireImg(int prefNum, string prefStr, int size,
        int frameX, int frameY, bool pad)
    {
        SetupFolders(prefNum, prefStr);

        var actualR110ImgName = GetImgActualPath(prefNum, prefStr, 
            ext: FileExt.R110);
        var actualTemplateName = GetImgActualPath(prefNum, prefStr, 
            suffStr: "{0}");

        var baselineLst = GetBaselineXByYImgNames(prefNum, prefStr,
            2, 2);

        var background = new EmptyBackground();
        var observers = new List<IObserver>
        {
            new FileObserver(size, actualR110ImgName)
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

        var cutter = new ImgCutter(actualR110ImgName);
        var actualLst = cutter.CutImages(actualTemplateName, 
            width: size / 2, 
            height: size / 2,
            frameX: frameX,
            frameY: frameY,
            pad: pad);

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
