namespace Rule110;

class Program
{
    static void Main(string[] args)
    {
        var rand = new Random();
        for (int k = 0; k < 20; k++)
        {
            var scene = new Scene(1000, $"img{k+1}.bmp");

            var gliders = new List<(int, IGlider)>();
            gliders.Add((4, new ANGlider(8)));
            gliders.Add((20, new ENGlider(k, 0)));
            gliders.Add((40, new BNConeHatGlider(10, 0)));

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
