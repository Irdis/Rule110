namespace Rule110;

public class ImgObserver : IObserver
{
    public static readonly string FILE_PATH = "img.bmp";
    public const int BLOCK_SIZE = 1;

    private ImgBmp _img;
    
    public ImgObserver(int size, string? filePath = null) 
    {
        _img = new ImgBmp(filePath ?? FILE_PATH, size, size, BLOCK_SIZE);
    }

    public void Next(int lvl, int[] tape)
    {
        _img.WriteRow(tape);
    }

    public void Complete()
    {
        _img.Save();
    }
}
