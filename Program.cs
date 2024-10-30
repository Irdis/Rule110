namespace Rule110;

class Program
{
    static void Main(string[] args)
    {
        var rand = new Random();
        for (int i = 1; i <= 20; i++)
        {
            var scene = new Scene(1000, $"img{i}.bmp");

            var gliders = new List<(int, IGlider)>();
            gliders.Add((4, new ANGlider(1)));
            gliders.Add((20, new C3Glider(1)));
            gliders.Add((40, new BNConeHatGlider(i, 0)));

            scene.FillWithEther(gliders);
            scene.Draw();

            for (int j = 0; j < scene.Size; j++)
            {
                scene.Next();
                scene.Draw();
            }
            scene.SaveImg();
        }
    }
}
