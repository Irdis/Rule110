namespace Rule110;

public class ImgCutter
{
    private string _scenePath;

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

    public ImgCutter(string scenePath)
    {
        _scenePath = scenePath;
    }

    // Layout:
    // ----------------------
    // | width: 4, height: 4|
    // ----------------------
    // |        data        |
    // ----------------------
    //
    // Params:
    //                     totalWidth
    // -------------------------------------------------------
    // | (frameX, frameY).                                   |
    // |                 |                                   |
    // |                 V    frameWidth                     |
    // |                 ---------------------               |
    // |                 |        |          |               |
    // |                 |        |          |               | totalHeight
    // |   frameHeight   ---------------------               |
    // |                 |        |          | height        |
    // |                 |        |          |               |
    // |                 ---------------------               |
    // |                              width                  |
    // -------------------------------------------------------
    //
    // Vars:
    // CroppedHeight
    // |                                                     |
    // |                             -------------           |
    // |                           ^ |           | ^         |
    // |             croppedHeight | |           | |         |
    // |                           v |           | |         |
    // -------------------------------------------------------
    //                               |           | | height
    //                               |           | v
    //                               -------------
    public void CutImages(
        string pathTemplate,
        int width,
        int height,
        int frameX = 0,
        int frameY = 0,
        int frameWidth = -1,
        int frameHeight = -1,
        bool pad = false
    )
    {
        using var fs = File.OpenRead(_scenePath);
        using var br = new BinaryReader(fs);

        var totalWidth = br.ReadInt32();
        var totalHeight = br.ReadInt32();
        if (frameX >= totalWidth)
            throw new ArgumentOutOfRangeException(nameof(frameX), frameX,
                $"FrameX is too big total width {totalWidth}");
        if (frameY >= totalHeight)
            throw new ArgumentOutOfRangeException(nameof(frameY), frameY,
                $"FrameY is too big total height {totalHeight}");

        if (frameWidth == -1)
            frameWidth = totalWidth - frameX;
        if (frameHeight == -1)
            frameHeight = totalHeight - frameY;

        var cols = Math.Min(
            DivCeil(frameWidth, width),
            DivCeil((totalWidth - frameX), width)
        );
        var rows = Math.Min(
            DivCeil(frameHeight, height),
            DivCeil((totalHeight - frameY), height)
        );

        var lastCroppedHeight = Math.Min(totalHeight - (frameY + (rows - 1) * height), height);
        var lastCroppedWidth = Math.Min(totalWidth - (frameX + (cols - 1) * width), width);
        var croppedFrameWidth = Math.Min(frameWidth, totalWidth - frameX);

        int[] buf = new int[width];
        int rowWidthBytes = DivCeil(totalWidth, 8);
        int skipColBytes = DivFloor(frameX, 8);

        for (int rowInd = 0; rowInd < rows; rowInd++)
        {
            var imgs = new ImgBmp[cols];
            var lastRow = rowInd == rows - 1;

            var imgHeight = pad || !lastRow ? height : lastCroppedHeight;
            CreateImgs(imgs,
                pathTemplate,
                rowInd,
                width,
                pad ? width : lastCroppedWidth,
                imgHeight);

            for (int i = 0; i < (lastRow ? lastCroppedHeight : height); i++)
            {
                MoveStreamToBegin(fs,
                    frameY + rowInd * height + i,
                    rowWidthBytes,
                    skipColBytes);

                int curByte = br.ReadByte();
                for (int j = 0; j < croppedFrameWidth; j++)
                {
                    var bufInd = j % width;
                    var bitInd = (frameX + j) % 8;
                    var colInd = j / width;

                    buf[bufInd] = (curByte & _mask[bitInd]) == 0
                        ? 0 : 1;

                    var lastCol = colInd == cols - 1;
                    if (IsLastBitInCurrentImg(j, width, lastCroppedWidth, lastCol))
                    {
                        WriteCurrentBuf(imgs[colInd],
                            buf,
                            width,
                            lastCroppedWidth,
                            lastCol,
                            pad);
                    }
                    var isLastBitInCurrentByte = bitInd == 7;
                    if (isLastBitInCurrentByte)
                    {
                        curByte = br.ReadByte();
                    }
                }
            }
            if (pad && lastRow && height != lastCroppedHeight)
            {
                Array.Fill(buf, 0);

                for (int colInd = 0; colInd < imgs.Length; colInd++)
                for (int i = lastCroppedHeight; i < height; i++)
                {
                    WriteCurrentBuf(imgs[colInd],
                        buf,
                        width,
                        lastCroppedWidth,
                        colInd == imgs.Length - 1,
                        pad);
                }
            }
            for (int colInd = 0; colInd < imgs.Length; colInd++)
            {
                imgs[colInd].Save();
            }
        }
    }

    private void WriteCurrentBuf(ImgBmp img,
        int[] buf,
        int width,
        int lastCroppedWidth,
        bool lastCol,
        bool pad)
    {
        if (!lastCol)
        {
            img.WriteRow(buf);
            return;
        }

        if (!pad || lastCroppedWidth == width)
        {
            img.WriteRow(buf, lastCroppedWidth);
        }
        else
        {
            Array.Fill(buf, 0, lastCroppedWidth, width - lastCroppedWidth);
            img.WriteRow(buf);
        }
    }

    private bool IsLastBitInCurrentImg(int colFrame, int width, int lastCroppedWidth, bool lastCol)
    {
        var col = colFrame % width;
        if (lastCol)
        {
            return col == lastCroppedWidth - 1;
        }
        return col == width - 1;
    }

    private void MoveStreamToBegin(FileStream fs,
        int curY,
        int rowWidthBytes,
        int skipColBytes)
    {
        const int headerBytes = 8;
        fs.Seek(headerBytes +
            curY * rowWidthBytes +
            skipColBytes,
            SeekOrigin.Begin);
    }

    private void SaveImgs(ImgBmp[] imgs)
    {
        for (int i = 0; i < imgs.Length; i++)
        {
            imgs[i].Save();
        }
    }

    private void CreateImgs(ImgBmp[] imgs,
        string pathTemplate,
        int row,
        int width,
        int lastWidth,
        int height)
    {
        for (int i = 0; i < imgs.Length; i++)
        {
            var fileName = string.Format(pathTemplate, $"{row}x{i}");
            var curWidth = i != imgs.Length - 1
                ? width
                : lastWidth;
            imgs[i] = new ImgBmp(fileName, curWidth, height);
        }
    }

    private static int DivCeil(int n, int i) => n / i + (n % i != 0 ? 1 : 0);
    private static int DivFloor(int n, int i) => n / i;
}
