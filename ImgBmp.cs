namespace Rule110;

public class ImgBmp : IDisposable
{
    private static byte[] _white = new byte[] { 0xFF, 0xFF, 0xFF };
    private static byte[] _black = new byte[] { 0x00, 0x00, 0x00 };

    private FileStream _writer;
    private BinaryWriter _binary;

    private int _padding = 0;
    private int _pixelsPerBlock;

    private bool _saved = false;

    public ImgBmp(string fileName, int width, int height, int pixelsPerBlock = 1)
    {
        _writer = new FileStream(fileName, FileMode.OpenOrCreate);
        _binary = new BinaryWriter(_writer);
        _pixelsPerBlock = pixelsPerBlock;
        WriteHeader(width * pixelsPerBlock, height * pixelsPerBlock);
    }

    private void WriteHeader(int width, int height)
    {
        var bpr = width * 3;
        var paddedWidth = bpr % 4 == 0 ? bpr : 4 * (bpr / 4 + 1);
        _padding = paddedWidth - bpr;
        var totalBytes = paddedWidth * height;
        const int fileHeader = 14;
        const int infoHeader = 40;
        const int headerSize = fileHeader + infoHeader;
        var total = totalBytes + headerSize;
        _binary.Write(new char[] { 'B', 'M' });
        _binary.Write(total);
        _binary.Write(0);
        _binary.Write(headerSize);

        _binary.Write(infoHeader);
        _binary.Write(width);
        _binary.Write(-height);
        _binary.Write((short)1); // planes
        _binary.Write((short)24); // bbp
        _binary.Write(0); // comression
        _binary.Write(0); // img size
        _binary.Write(0); // xpixelsperm
        _binary.Write(0); // ypixelsperm
        _binary.Write(0); // colorsused
        _binary.Write(0); // important colors
    }

    public void WriteRow(int[] pixels)
    {
        for (int j = 0; j < _pixelsPerBlock; j++)
        {
            for (int i = 0; i < pixels.Length; i++)
            {
                var color = pixels[i] == 0 ? _white : _black;
                for (int k = 0; k < _pixelsPerBlock; k++)
                {
                    _binary.Write(color);
                }
            }
            for (int i = 0; i < _padding; i++)
            {
                _binary.Write((byte)0);
            }
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
