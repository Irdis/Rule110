﻿using Rule110.Gliders;
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
        ENOrder();
        /*A4Order();*/
    }

    private static void CleanUp()
    {
        var files = Directory.GetFiles(".", "*.bmp");
        foreach(var file in files)
        {
            File.Delete(file);
        }
    }

    public static void ENOrder()
    {
        var enGliderType = 4;
        var en1 = new ENGlider(enGliderType);
        var c2 = new C2Glider();

        var analyzer = new GliderAnalyzer();
        var enLst = analyzer.Analyze(en1);
        var lst1 = new List<(int, int)>();
        var lst2 = new List<(int, int)>();
        var enInd = 1;

        for (int u = 0; u < 2; u++)
        for (int k = 0; k < 40; k++)
        {
            const int width = 1000;
            const int height = 1000;

            var background = new EtherBackground();
            /*var imgName = $"img_{u}_{k}.bmp";*/
            var imgName = $"img_{k}_{u}.bmp";
            var patternObserver = new PeriodicPatternObserver(TileUtils.ParseStrip("*......*"), 7);
            var observers = new List<IObserver>
            {
                new ImgObserver(width, height, imgName),
                    patternObserver
            };

            var scene = new Scene(width, background, observers);
            var gliders = new List<(int, IGlider)>();

            var (offset1, gliderIndex1) = ENGlider.Next(enInd, k);
            var align1 = ENGlider.RightAlignment(gliderIndex1, enGliderType);

            var (offset2, gliderIndex2) = ENGlider.Adjacent(gliderIndex1, offset1);
            var align2 = ENGlider.RightAlignment(gliderIndex2, enGliderType);
            Console.WriteLine($"{k} {offset1} {gliderIndex1}");
            Console.WriteLine($"{k} {offset2} {gliderIndex2}");
            Console.WriteLine();

            var alignTotal = 0;

            gliders.Add((15 + alignTotal, c2));

            if (u == 0) {
                gliders.Add((30 + offset1 + alignTotal, enLst[gliderIndex1]));
                alignTotal += align1;
            } else {
                gliders.Add((30 + offset2 + alignTotal, enLst[gliderIndex2]));
                alignTotal += align2;
            }

            gliders.Add((50 + alignTotal, c2));

            scene.Init(gliders);

            scene.InitComplete();

            for (int j = 1; j < height; j++)
            {
                scene.Next();
            }
            scene.Complete();
            if (u == 0)
                lst1.Add((gliderIndex1, patternObserver.Depth));
            if (u == 1)
                lst2.Add((gliderIndex2, patternObserver.Depth));
        }

        /*for (int i = 0; i < lst1.Count; i++)*/
        /*{*/
        /*    Console.WriteLine($"{lst1[i].Item1} {lst1[i].Item2}");*/
        /*    Console.WriteLine($"{lst2[i].Item1} {lst2[i].Item2}");*/
        /*    Console.WriteLine();*/
        /*}*/
        Console.WriteLine();
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

        var eGap = -20;

        var eInd = 0;
        var aInd = 2;

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
            var (offset1, gliderIndex1) = EHatGlider.Next(eInd, -1);
            var (offset2, gliderIndex2) = EHatGlider.Next(eInd, eGap - k);
            var align2 = EHatGlider.RightAlignment(gliderIndex2);
            var align1 = EHatGlider.RightAlignment(gliderIndex1);

            var gliders = new List<(int, IGlider)>();

            var offsetATotal = 0;
            
            var (offset3, gliderIndex3) = ANGlider.Next(aInd, -1);
            var align3 = ANGlider.RightAlignment(gliderIndex3);
            offsetATotal += offset3;

            var (offset4, gliderIndex4) = ANGlider.NextECrossing(gliderIndex3, 2);
            var align4 = ANGlider.RightAlignment(gliderIndex4);
            offsetATotal += offset4;

            var (offset5, gliderIndex5) = ANGlider.NextECrossing(gliderIndex4, 0);
            var align5 = ANGlider.RightAlignment(gliderIndex5);
            offsetATotal += offset5;

            var alignTotal = 0;

            Console.WriteLine(offsetATotal);
            gliders.Add((30 + offsetATotal + alignTotal, a4Lst[gliderIndex5]));
            alignTotal += align5;
            offsetATotal -= offset5;

            gliders.Add((30 + offsetATotal + alignTotal, a4Lst[gliderIndex4]));
            alignTotal += align4;
            offsetATotal -= offset4;

            gliders.Add((30 + offsetATotal + alignTotal, a4Lst[gliderIndex3]));
            alignTotal += align3;
            offsetATotal -= offset3;

            gliders.Add((50 + offset1 + alignTotal, ehLst[gliderIndex1]));
            alignTotal += align1;

            gliders.Add((50 + offset2 + alignTotal, ehLst[gliderIndex2]));
            alignTotal += align2;

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
            const int height = 1000;

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
            var alignTotal = 0;

            gliders.Add((15 + offset3, c2Lst[gliderIndex3]));

            gliders.Add((15, c2Lst[cInd]));

            gliders.Add((30 + offset2, ehLst[gliderIndex2]));
            alignTotal += align2;

            gliders.Add((30 + offset1 + alignTotal, ehLst[gliderIndex1]));
            alignTotal += align1;

            gliders.Add((50 + alignTotal, c2));

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
