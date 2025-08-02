namespace Rule110;

public class FileObserver : IObserver
{
    private FileStream _fs;
    private BinaryWriter _bw;

    private int _width;
    private static byte[] _mask = [
        1 << 7,
        1 << 6,
        1 << 5,
        1 << 4,
        1 << 3,
        1 << 2,
        1 << 1,
        1 << 0,
    ];
    
    public FileObserver(int size, string filePath) 
        : this(size, size, filePath)
    {
    }

    public FileObserver(int width, int height, string filePath) 
    {
        if (File.Exists(filePath))
            File.Delete(filePath);
        _width = width;
        _fs = new FileStream(filePath, new FileStreamOptions {
            Mode = FileMode.Create,
            Access = FileAccess.Write,
            PreallocationSize = GetInBytes(width) * height + 8
        });
        _bw = new BinaryWriter(_fs);

        _bw.Write(width);
        _bw.Write(height);
    }

    private int GetInBytes(int width) => width / 8 + (width % 8 != 0 ? 1 : 0);

    public void Next(int lvl, int[] tape)
    {
        var bytesTotal = GetInBytes(_width);
        for (int i = 0; i < bytesTotal; i++)
        {
            byte b = 0;
            for (int j = i * 8, ind = 0; j < Math.Min(_width, (i + 1) * 8); j++, ind++)
            {
                if (tape[j] == 1)
                    b |= _mask[ind];
            }
            _bw.Write(b);
        }
    }

    public void Complete()
    {
        _fs.Dispose();
        _bw.Dispose();
    }
}
