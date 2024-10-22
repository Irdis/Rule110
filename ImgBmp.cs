namespace Rule110;

public class ImgBmp : IDisposable
{
    private FileStream _writer;
    private BinaryWriter _binary;

    private int _padding = 0;
    private int _bitsPerBlock;

    private bool _saved = false;

    public ImgBmp(string fileName, int width, int height, int bitsPerBlock = 1)
    {
        _writer = new FileStream(fileName, FileMode.OpenOrCreate);
        _binary = new BinaryWriter(_writer);
        _writer.SetLength(0);
        _bitsPerBlock = bitsPerBlock;
        WriteHeader(width * bitsPerBlock, height * bitsPerBlock);
    }

    private void WriteHeader(int width, int height)
    {
        var bitsPerRow = width;
        var paddedWidth = bitsPerRow % 32 == 0 
            ? bitsPerRow 
            : 32 * (bitsPerRow / 32 + 1);
        _padding = paddedWidth - bitsPerRow;
        var totalBytes = paddedWidth * height;
        const int fileHeader = 14;
        const int infoHeader = 40;
        const int headerSize = fileHeader + infoHeader + 8;
        var total = totalBytes + headerSize;
        _binary.Write(new char[] { 'B', 'M' });
        _binary.Write(total);
        _binary.Write(0);
        _binary.Write(headerSize);

        _binary.Write(infoHeader);
        _binary.Write(width);
        _binary.Write(-height);
        _binary.Write((short)1); //planes
        _binary.Write((short)1); //bbp
        _binary.Write(0); // comression
        _binary.Write(0); // img size
        _binary.Write(0); // xpixelsperm
        _binary.Write(0); // ypixelsperm
        _binary.Write(2); // colorsused
        _binary.Write(2); // important colors

        _binary.Write((byte)0xFF);
        _binary.Write((byte)0xFF);
        _binary.Write((byte)0xFF);
        _binary.Write((byte)0x00);

        _binary.Write((byte)0x00);
        _binary.Write((byte)0x00);
        _binary.Write((byte)0x00);
        _binary.Write((byte)0x00);
    }

    public void WriteRow(int[] pixels)
    {
        for (int k = 0; k < _bitsPerBlock; k++)
        {
            var len = pixels.Length * _bitsPerBlock;
            byte b = 0;
            for (int i = 0; i < len; i++)
            {
                if (pixels[i / _bitsPerBlock] == 1)
                {
                    b = (byte)(b + (1 << (7 - i % 8)));
                }
                if (i % 8 == 7)
                {
                    _binary.Write(b);
                    b = 0;
                }
            }
            var rem = len % 8;
            if (rem > 0)
                _binary.Write(b);

            for (int i = 0; i < _padding / 8; i++)
                _binary.Write((byte)0);
        }
    }

    public void Save()
    {
        if (!_saved)
        {
            _saved = true;
            _binary.Dispose();
            _writer.Dispose();
        }
    }

    public void Dispose()
    {
        Save();
    }
}
