using Rule110.Gliders;

namespace Rule110;

public class Program
{
    public static void Main(string[] args)
    {
        CleanUp("out");
        CleanUp();

        // Classic();
        EncoderBig();

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
