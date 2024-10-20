namespace Rule110;

class Program
{
    static void Main(string[] args)
    {
        var rand = new Random();
        var scene = new Scene(1000);

        var gliders = new List<(int, IGlider)>();
        gliders.Add((4, new ANGlider(5)));
        gliders.Add((40, new BHNGlider()));

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
