using Rule110.Gliders;

namespace Rule110;

public class Program
{
    public static void Main(string[] args)
    {
        CleanUp("out");
        CleanUp();

        // Classic();
        // EncoderSmall();
        EncoderBig();
        // EncoderBBlock();
        // EncoderFBlock();
        // EncoderGBlock();
        // EncoderHBlock();
        // EncoderIBlock();
        // EncoderJBlock();
        // EncoderKBlock();
        // EncoderLBlock();
        // E5ToE2RelOrderTest();
        // E2ToE4KRelOrderTest();
        // E2ToE4LRelOrderTest();
        // E4ToEHLRelOrderTest();

        CutR110Images();
    }

    private static void CleanUp(string? folder = null)
    {
        folder ??= ".";
        var files = Directory.GetFiles(folder, "*.bmp");
        foreach(var file in files)
        {
            File.Delete(file);
        }
        files = Directory.GetFiles(folder, "*.r110");
        foreach(var file in files)
        {
            File.Delete(file);
        }
    }

    public static void CutR110Images()
    {
        var cutter = new ImgCutter("img_0.r110");
        cutter.CutImages("out/img_{0}.bmp", width: 10000, height: 20000,
            frameX: 15000,
            // frameY: 10000,
            frameWidth: 10000,
            frameHeight: 30000);
    }

    public static void EncoderBig()
    {
        string[] patterns = [
            @"
                B 13A B 11A B 12A B 336A
                B 13A B 11A B 12A B 336A
                B 13A B 11A B 12A B
                (C E D F G)
                H J I I I I I I I I I I K
                H J I I I I I I I I I I K
            ",
        ];
        var encoderFactory = new BlockEncoderFactory();

        for (int i = 0; i < patterns.Length; i++)
        {
            var pattern = patterns[i];
            var encoder = encoderFactory.Create(i);

            const int width = 30000;
            const int height = 30000;

            var background = new EtherBackground();
            // var imgName = $"img_{i}.bmp";
            var r110Name = $"img_{i}.r110";
            var observers = new List<IObserver>
            {
                // new ImgObserver(width, height, imgName),
                new FileObserver(width, height, r110Name),
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
        }
    }

    public static void EncoderSmall()
    {
        string[] patterns = [
            "BCFGG",
            "BCDFFFF",
        ];
        var encoderFactory = new BlockEncoderFactory();

        for (int i = 0; i < patterns.Length; i++)
        {
            var pattern = patterns[i];
            var encoder = encoderFactory.Create(i);

            const int width = 1500;
            const int height = 1000;

            var background = new EtherBackground();
            var imgName = $"img_{i}.bmp";
            var observers = new List<IObserver>
            {
                new ImgObserver(width, height, imgName),
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
        }
    }

    public static void EncoderBBlock()
    {
        var encoderFactory = new BlockEncoderFactory();

        for (int i = 0; i < EHatGlider.Size; i++)
        {
            var encoder = encoderFactory.Create(i);

            const int width = 1500;
            const int height = 1000;

            var background = new EtherBackground();
            var imgName = $"img_{i}.bmp";
            var observers = new List<IObserver>
            {
                new ImgObserver(width, height, imgName),
            };

            var scene = new Scene(width, background, observers);
            var gliders = new List<(int, IGlider)>();
            encoder.InsertEHat(gliders, i, 5);
            encoder.EncodeF(gliders);
            encoder.EncodeF(gliders);
            encoder.EncodeF(gliders);
            encoder.EncodeF(gliders);

            scene.Init(gliders);

            scene.InitComplete();

            for (int j = 1; j < height; j++)
            {
                scene.Next();
            }
            scene.Complete();
        }
    }

    public static void EncoderFBlock()
    {
        var encoderFactory = new BlockEncoderFactory();

        for (int i = 0; i < EHatGlider.Size; i++)
        {
            var encoder = encoderFactory.Create(i);

            const int width = 1500;
            const int height = 1000;

            var background = new EtherBackground();
            var imgName = $"img_{i}.bmp";
            var observers = new List<IObserver>
            {
                new ImgObserver(width, height, imgName),
            };

            var scene = new Scene(width, background, observers);
            var gliders = new List<(int, IGlider)>();
            encoder.InsertEHat(gliders, i, 5);
            encoder.EncodeF(gliders);
            encoder.EncodeF(gliders);

            scene.Init(gliders);

            scene.InitComplete();

            for (int j = 1; j < height; j++)
            {
                scene.Next();
            }
            scene.Complete();
        }
    }

    public static void EncoderGBlock()
    {
        var encoderFactory = new BlockEncoderFactory();

        for (int i = 0; i < EHatGlider.Size; i++)
        {
            var encoder = encoderFactory.Create(i);

            const int width = 1500;
            const int height = 1000;

            var background = new EtherBackground();
            var imgName = $"img_{i}.bmp";
            var observers = new List<IObserver>
            {
                new ImgObserver(width, height, imgName),
            };

            var scene = new Scene(width, background, observers);
            var gliders = new List<(int, IGlider)>();
            encoder.InsertEHat(gliders, i, 5);
            encoder.EncodeG(gliders);
            encoder.EncodeG(gliders);

            scene.Init(gliders);

            scene.InitComplete();

            for (int j = 1; j < height; j++)
            {
                scene.Next();
            }
            scene.Complete();
        }
    }

    public static void EncoderHBlock()
    {
        var encoderFactory = new BlockEncoderFactory();

        for (int i = 0; i < EHatGlider.Size; i++)
        {
            var encoder = encoderFactory.Create(i);

            const int width = 1500;
            const int height = 1000;

            var background = new EtherBackground();
            var imgName = $"img_{i}.bmp";
            var observers = new List<IObserver>
            {
                new ImgObserver(width, height, imgName),
            };

            var scene = new Scene(width, background, observers);
            var gliders = new List<(int, IGlider)>();
            encoder.InsertEHat(gliders, i, 5);
            encoder.EncodeH(gliders);
            encoder.EncodeH(gliders);

            scene.Init(gliders);

            scene.InitComplete();

            for (int j = 1; j < height; j++)
            {
                scene.Next();
            }
            scene.Complete();
        }
    }

    public static void EncoderIBlock()
    {
        var encoderFactory = new BlockEncoderFactory();

        for (int i = 0; i < EHatGlider.Size; i++)
        {
            var encoder = encoderFactory.Create(i);

            const int width = 1500;
            const int height = 1000;

            var background = new EtherBackground();
            var imgName = $"img_{i}.bmp";
            var observers = new List<IObserver>
            {
                new ImgObserver(width, height, imgName),
            };

            var scene = new Scene(width, background, observers);
            var gliders = new List<(int, IGlider)>();
            encoder.InsertEHat(gliders, i, 5);
            encoder.EncodeI(gliders);
            encoder.EncodeI(gliders);

            scene.Init(gliders);

            scene.InitComplete();

            for (int j = 1; j < height; j++)
            {
                scene.Next();
            }
            scene.Complete();
        }
    }

    public static void EncoderJBlock()
    {
        var encoderFactory = new BlockEncoderFactory();

        for (int i = 0; i < EHatGlider.Size; i++)
        {
            var encoder = encoderFactory.Create(i);

            const int width = 1500;
            const int height = 1000;

            var background = new EtherBackground();
            var imgName = $"img_{i}.bmp";
            var observers = new List<IObserver>
            {
                new ImgObserver(width, height, imgName),
            };

            var scene = new Scene(width, background, observers);
            var gliders = new List<(int, IGlider)>();
            encoder.InsertEHat(gliders, i, 5);
            encoder.EncodeJ(gliders);
            encoder.EncodeJ(gliders);

            scene.Init(gliders);

            scene.InitComplete();

            for (int j = 1; j < height; j++)
            {
                scene.Next();
            }
            scene.Complete();
        }
    }

    public static void EncoderKBlock()
    {
        var encoderFactory = new BlockEncoderFactory();

        for (int i = 0; i < EHatGlider.Size; i++)
        {
            var encoder = encoderFactory.Create(i);

            const int width = 1500;
            const int height = 1000;

            var background = new EtherBackground();
            var imgName = $"img_{i}.bmp";
            var observers = new List<IObserver>
            {
                new ImgObserver(width, height, imgName),
            };

            var scene = new Scene(width, background, observers);
            var gliders = new List<(int, IGlider)>();
            encoder.InsertEHat(gliders, i, 5);
            encoder.EncodeK(gliders);
            encoder.EncodeK(gliders);

            scene.Init(gliders);

            scene.InitComplete();

            for (int j = 1; j < height; j++)
            {
                scene.Next();
            }
            scene.Complete();
        }
    }

    public static void EncoderLBlock()
    {
        var encoderFactory = new BlockEncoderFactory();

        for (int i = 0; i < EHatGlider.Size; i++)
        {
            var encoder = encoderFactory.Create(i);

            const int width = 1500;
            const int height = 1000;

            var background = new EtherBackground();
            var imgName = $"img_{i}.bmp";
            var observers = new List<IObserver>
            {
                new ImgObserver(width, height, imgName),
            };

            var scene = new Scene(width, background, observers);
            var gliders = new List<(int, IGlider)>();
            encoder.InsertEHat(gliders, i, 5);
            encoder.EncodeL(gliders);
            encoder.EncodeL(gliders);

            scene.Init(gliders);

            scene.InitComplete();

            for (int j = 1; j < height; j++)
            {
                scene.Next();
            }
            scene.Complete();
        }
    }

    public static void E5ToE2RelOrderTest()
    {
        var e5GliderType = 4;
        var e2GliderType = 1;

        var e5GliderCollection = new ENGliderCollection(e5GliderType);
        var e2GliderCollection = new ENGliderCollection(e2GliderType);
        var c2GliderCollection = new C2GliderCollection();

        for (int i = 0; i < ENGlider.Size; i++)
        {
            const int width = 500;
            const int height = 500;

            var background = new EtherBackground();
            var imgName = $"img_{i}.bmp";
            var observers = new List<IObserver>
            {
                new ImgObserver(width, height, imgName),
            };

            var scene = new Scene(width, background, observers);
            var gliders = new List<(int, IGlider)>();
            var offset = 5;
            var alignment = 0;
            var initialGlider = i;

            var alignDelta = ENGlider.RightAlignment(initialGlider, e5GliderType);
            gliders.Add((offset, e5GliderCollection.Get(initialGlider)));
            offset += alignDelta;
            alignment += alignDelta;

            var (e2Offset, e2Number) = E5ToE2GliderRelativeOrder.Next(initialGlider);
            offset += e2Offset;

            alignDelta = ENGlider.RightAlignment(e2Number, e2GliderType);
            gliders.Add((offset, e2GliderCollection.Get(e2Number)));
            offset += alignDelta;
            alignment += alignDelta;

            offset = 30 + alignment;

            gliders.Add((offset, c2GliderCollection.Get(0)));
            offset += alignDelta;
            alignment += alignDelta;

            scene.Init(gliders);
            scene.InitComplete();
            for (int j = 1; j < height; j++)
            {
                scene.Next();
            }
            scene.Complete();
        }
    }

    public static void E2ToE4KRelOrderTest()
    {
        var e2GliderType = 1;
        var e4GliderType = 3;

        var e2GliderCollection = new ENGliderCollection(e2GliderType);
        var e4GliderCollection = new ENGliderCollection(e4GliderType);
        var c2GliderCollection = new C2GliderCollection();

        for (int i = 0; i < ENGlider.Size; i++)
        {
            const int width = 500;
            const int height = 500;

            var background = new EtherBackground();
            var imgName = $"img_{i}.bmp";
            var observers = new List<IObserver>
            {
                new ImgObserver(width, height, imgName),
            };

            var scene = new Scene(width, background, observers);
            var gliders = new List<(int, IGlider)>();
            var offset = 5;
            var alignment = 0;
            var initialGlider = i;

            var alignDelta = ENGlider.RightAlignment(initialGlider, e2GliderType);
            gliders.Add((offset, e2GliderCollection.Get(initialGlider)));
            offset += alignDelta;
            alignment += alignDelta;

            var (e4Offset, e4Number) = E2ToE4KGliderRelativeOrder.Next(initialGlider);
            offset += e4Offset;

            alignDelta = ENGlider.RightAlignment(e4Number, e4GliderType);
            gliders.Add((offset, e4GliderCollection.Get(e4Number)));
            offset += alignDelta;
            alignment += alignDelta;

            offset = 30 + alignment;

            gliders.Add((offset, c2GliderCollection.Get(0)));
            offset += alignDelta;
            alignment += alignDelta;

            scene.Init(gliders);
            scene.InitComplete();
            for (int j = 1; j < height; j++)
            {
                scene.Next();
            }
            scene.Complete();
        }
    }

    public static void E2ToE4LRelOrderTest()
    {
        var e2GliderType = 1;
        var e4GliderType = 3;

        var e2GliderCollection = new ENGliderCollection(e2GliderType);
        var e4GliderCollection = new ENGliderCollection(e4GliderType);
        var c2GliderCollection = new C2GliderCollection();

        for (int i = 0; i < ENGlider.Size; i++)
        {
            const int width = 500;
            const int height = 500;

            var background = new EtherBackground();
            var imgName = $"img_{i}.bmp";
            var observers = new List<IObserver>
            {
                new ImgObserver(width, height, imgName),
            };

            var scene = new Scene(width, background, observers);
            var gliders = new List<(int, IGlider)>();
            var offset = 5;
            var alignment = 0;
            var initialGlider = i;

            var alignDelta = ENGlider.RightAlignment(initialGlider, e2GliderType);
            gliders.Add((offset, e2GliderCollection.Get(initialGlider)));
            offset += alignDelta;
            alignment += alignDelta;

            var (e4Offset, e4Number) = E2ToE4LGliderRelativeOrder.Next(initialGlider);
            offset += e4Offset;

            alignDelta = ENGlider.RightAlignment(e4Number, e4GliderType);
            gliders.Add((offset, e4GliderCollection.Get(e4Number)));
            offset += alignDelta;
            alignment += alignDelta;

            offset = 30 + alignment;

            gliders.Add((offset, c2GliderCollection.Get(0)));
            offset += alignDelta;
            alignment += alignDelta;

            scene.Init(gliders);
            scene.InitComplete();
            for (int j = 1; j < height; j++)
            {
                scene.Next();
            }
            scene.Complete();
        }
    }

    public static void EHToE5RelOrderTest()
    {
        var e5GliderType = 4;
        var ehGliderCollection = new EHatGliderCollection();
        var e5GliderCollection = new ENGliderCollection(e5GliderType);
        var c2GliderCollection = new C2GliderCollection();

        for (int i = 0; i < EHatGlider.Size; i++)
        {
            const int width = 500;
            const int height = 500;

            var background = new EtherBackground();
            var imgName = $"img_{i}.bmp";
            var observers = new List<IObserver>
            {
                new ImgObserver(width, height, imgName),
            };

            var scene = new Scene(width, background, observers);
            var gliders = new List<(int, IGlider)>();
            var offset = 5;
            var alignment = 0;
            var initialGlider = i;

            var alignDelta = EHatGlider.RightAlignment(initialGlider);
            gliders.Add((offset, ehGliderCollection.Get(initialGlider)));
            offset += alignDelta;
            alignment += alignDelta;

            var (e5Offset, e5Number) = EHatToE5GliderRelativeOrder.Next(initialGlider);
            offset += e5Offset;

            alignDelta = ENGlider.RightAlignment(e5Number, e5GliderType);
            gliders.Add((offset, e5GliderCollection.Get(e5Number)));
            offset += alignDelta;
            alignment += alignDelta;

            offset = 30 + alignment;

            gliders.Add((offset, c2GliderCollection.Get(0)));
            offset += alignDelta;
            alignment += alignDelta;

            scene.Init(gliders);
            scene.InitComplete();
            for (int j = 1; j < height; j++)
            {
                scene.Next();
            }
            scene.Complete();
        }
    }

    public static void E4ToEHLRelOrderTest()
    {
        var e4GliderType = 3;
        var e4GliderCollection = new ENGliderCollection(e4GliderType);
        var ehGliderCollection = new EHatGliderCollection();
        var c2GliderCollection = new C2GliderCollection();

        for (int k = 0; k < 2 * ENGlider.Size; k++)
        {
            const int width = 500;
            const int height = 500;

            var background = new EtherBackground();
            var imgName = $"img_{k}.bmp";
            var observers = new List<IObserver>
            {
                new ImgObserver(width, height, imgName),
            };

            var scene = new Scene(width, background, observers);
            var gliders = new List<(int, IGlider)>();
            var offset = 5;
            var alignment = 0;
            var initialGlider = k / 2;

            var alignDelta = ENGlider.RightAlignment(initialGlider, e4GliderType);
            gliders.Add((offset, e4GliderCollection.Get(initialGlider)));
            offset += alignDelta;
            alignment += alignDelta;

            var (ehOffset, ehNumber) = E4ToEHLGliderRelativeOrder.Next(initialGlider, k % 2);
            offset += ehOffset;

            alignDelta = EHatGlider.RightAlignment(ehNumber);
            gliders.Add((offset, ehGliderCollection.Get(ehNumber)));
            offset += alignDelta;
            alignment += alignDelta;

            offset = 30 + alignment;

            gliders.Add((offset, c2GliderCollection.Get(0)));
            offset += alignDelta;
            alignment += alignDelta;

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
        var r110Name = $"img_0.r110";
        var background = new EmptyBackground();
        var observers = new List<IObserver>
        {
            new ImgObserver(size, $"img_0.bmp"),
            new FileObserver(size, size, r110Name),
            // new ConsoleObserver(size, 100),
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
}
