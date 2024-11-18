using Rule110.Gliders;

namespace Rule110;

public class Program
{
    public static void Gliders()
    {
        for (int k = 0; k < 1; k++)
        {
            const int size = 1000;
            var background = new EtherBackground();
            var observers = new List<IObserver>
            {
                new ImgObserver(size, $"img{k+1}.bmp"),
            };
            var scene = new Scene(size, background, observers);

            var gliders = new List<(int, IGlider)>();
            gliders.Add((4, new ANGlider(0)));
            gliders.Add((15, new BNStraightHatGlider(0)));
            gliders.Add((20, new BNStraightHatGlider(1)));
            gliders.Add((40, new BNStraightHatGlider(2)));
            gliders.Add((50, new BNStraightHatGlider(4)));
            gliders.Add((55, new BNStraightHatGlider(5)));

            scene.Init(gliders);

            scene.InitComplete();

            for (int j = 1; j < scene.Size; j++)
            {
                scene.Next();
            }
            scene.Complete();
        }
    }
    static void Main(string[] args)
    {
        /*Classic();*/
        /*Random();*/
        Gliders();
    }

    public static void Classic()
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

    public static void Random()
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
