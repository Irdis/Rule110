namespace Rule110;

class Program
{
    static void Main(string[] args)
    {
        var rand = new Random();
        var scene = new Scene(500);

        var gliders = new List<(int, IGlider)>();
        gliders.Add((1, new ANGlider(15)));
        for (int i = 0; i < 15; i++)
        {
            gliders.Add((15 + i, new BGlider()));
        }

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
