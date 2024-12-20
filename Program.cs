using Rule110.Gliders;
using System.Linq;

namespace Rule110;

public class Program
{
    public static void Main(string[] args)
    {
        CleanUp();
        /*Classic();*/
        /*Random();*/
        /*C2Order();*/
        /*EOrder();*/
        A4Order();
    }

    private static void CleanUp()
    {
        var files = Directory.GetFiles(".", "*.bmp");
        foreach(var file in files)
        {
            File.Delete(file);
        }
    }

    private static void A4Order()
    {
        var eh1 = new EHatGlider();
        var c2 = new C2Glider();
        var a4 = new ANGlider(3);
        var analyzer = new GliderAnalyzer();
        var ehLst = analyzer.Analyze(eh1);
        var c2Lst = analyzer.Analyze(c2);
        var a4Lst = analyzer.Analyze(a4);

        var eGap = 20;
        var aGap = 0;

        var eInd = 0;
        var aInd = 0;
        var a4Init = 4;

        for (int k = 0; k < 50; k++)
        {
            const int width = 1000;
            const int height = 1000;

            var background = new EtherBackground();
            var imgName = $"img_{k}.bmp";
            var observers = new List<IObserver>
            {
                new ImgObserver(width, height, imgName),
            };

            var scene = new Scene(width, background, observers);
            var (offset1, gliderIndex1) = EHatGlider.Next(eInd);
            var (offset2, gliderIndex2) = EHatGlider.Next(eInd, eGap);
            var align2 = EHatGlider.RightAlignment(gliderIndex2);
            var align1 = EHatGlider.RightAlignment(gliderIndex1);

            var gliders = new List<(int, IGlider)>();

            var (offset3, gliderIndex3) = ANGlider.Next(aInd, a4Init);
            var align3 = ANGlider.RightAlignment(gliderIndex3);

            var (offset4, gliderIndex4) = ANGlider.Next(aInd, a4Init + aGap + k + 1);
            var align4 = ANGlider.RightAlignment(gliderIndex4);

            var alignTotal = 0;

            gliders.Add((30 + offset4 + alignTotal, a4Lst[gliderIndex4]));
            alignTotal += align4;

            gliders.Add((30 + offset3 + alignTotal, a4Lst[gliderIndex3]));
            alignTotal += align3;

            gliders.Add((50 + offset2 + alignTotal, ehLst[gliderIndex2]));
            alignTotal += align2;

            gliders.Add((50 + offset1 + alignTotal, ehLst[gliderIndex1]));
            alignTotal += align1;

            scene.Init(gliders);

            scene.InitComplete();

            for (int j = 1; j < height; j++)
            {
                scene.Next();
            }
            scene.Complete();
        }
    }

    private static void EOrder()
    {
        var eh1 = new EHatGlider();
        var c2 = new C2Glider();
        var analyzer = new GliderAnalyzer();
        var ehLst = analyzer.Analyze(eh1);
        var c2Lst = analyzer.Analyze(c2);

        var eInd = 20;
        var cInd = 0;

        var c2gap = 6;
        var eDist = 50;
        for (int k = 3; k < 30; k++)
        {
            const int width = 1000;
            const int height = 700;

            var background = new EtherBackground();
            var imgName = $"img_{k}.bmp";
            var observers = new List<IObserver>
            {
                new ImgObserver(width, height, imgName),
            };

            var scene = new Scene(width, background, observers);
            var (offset1, gliderIndex1) = EHatGlider.Next(eInd, eDist);
            var (offset2, gliderIndex2) = EHatGlider.Next(eInd, eDist + k);
            var align2 = EHatGlider.RightAlignment(gliderIndex2);
            var align1 = EHatGlider.RightAlignment(gliderIndex1);
            var (offset3, gliderIndex3) = C2Glider.Next(cInd, c2gap);

            var gliders = new List<(int, IGlider)>();
            gliders.Add((15 + offset3, c2Lst[gliderIndex3]));
            gliders.Add((15, c2Lst[cInd]));
            gliders.Add((30 + offset2, ehLst[gliderIndex2]));
            gliders.Add((30 + offset1 + align2, ehLst[gliderIndex1]));
            gliders.Add((50 + align1 + align2, c2));

            scene.Init(gliders);

            scene.InitComplete();

            for (int j = 1; j < height; j++)
            {
                scene.Next();
            }
            scene.Complete();
        }
    }

    private static void C2Order()
    {
        var eh = new EHatGlider();
        var c2 = new C2Glider();
        var analyzer = new GliderAnalyzer();
        var c2Lst = analyzer.Analyze(c2);
        var startGlider = 5;

        for (int i = 0; i < 20; i++)
        {
            const int width = 1000;
            const int height = 700;

            var background = new EtherBackground();
            var observers = new List<IObserver>
            {
                new ImgObserver(width, height, $"img_{i}.bmp"),
            };

            var scene = new Scene(width, background, observers);

            var gliders = new List<(int, IGlider)>();

            var (tileOffset, orderPosition) = C2Glider.Next(startGlider, i);
            gliders.Add((26 + tileOffset, c2Lst[C2Glider.OverOrder[orderPosition]]));

            gliders.Add((26, c2Lst[startGlider]));
            gliders.Add((30, eh));

            scene.Init(gliders);

            scene.InitComplete();

            for (int j = 1; j < height; j++)
            {
                scene.Next();
            }
            scene.Complete();
        }
    }

    private static void Classic()
    {
        const int size = 100;
        var background = new EmptyBackground();
        var observers = new List<IObserver>
        {
            new ImgObserver(size, $"img_classic.bmp"),
            new ConsoleObserver(size, 100),
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
    }

    private static void Random()
    {
        var random = new Random();
        const int size = 1000;
        var background = new EmptyBackground();
        var observers = new List<IObserver>
        {
            new ImgObserver(size, $"img_random.bmp"),
        };
        var scene = new Scene(size, background, observers);

        var gliders = new List<(int, IGlider)>();
        scene.Init(gliders);

        for(int i = 0; i < size; i++)
        {
            scene.SetState(i, random.Next(2));
        }

        scene.InitComplete();

        for (int j = 1; j < scene.Size; j++)
        {
            scene.Next();
        }
        scene.Complete();
    }
}
