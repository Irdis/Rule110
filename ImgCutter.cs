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

    public void CutImages(
        string pathTemplate,
        int width, 
        int height,
        int frameX = 0,
        int frameY = 0,
        int frameWidth = -1,
        int frameHeight = -1
    )
    {
        using var fs = File.OpenRead(_scenePath);
        using var br = new BinaryReader(fs);

        var totalWidth = br.ReadInt32();
        var totalHeight = br.ReadInt32();

        if (frameWidth == -1)
            frameWidth = totalWidth - frameX;
        if (frameHeight == -1)
            frameHeight = totalHeight - frameY;

        var cols = Math.Min(
            DivCeil(width, frameWidth),
            DivCeil(width, (totalWidth - frameX))
        );
        var rows = Math.Min(
            DivCeil(height, frameHeight),
            DivCeil(height, (totalHeight - frameY))
        );


        var imgs = new ImgBmp[cols];
        var imgInd = 0;
        var rowInd = 0;
        CreateImgs(imgs, pathTemplate, rowInd, width, height);

        int[] buf = new int[width];
        int bufInd = 0;
        var widthBytes = DivCeil(8, totalWidth);
        for (int i = 0; i < totalHeight; i++)
        {
            for (int j = 0; j < widthBytes; j++)
            {
                var b = br.ReadByte();
                var lastX = (j + 1) * 8 - 1;
                if (lastX < frameX ||
                    i < frameY ||
                    imgInd == cols) 
                    continue;
                for (int k = 0; k < 8; k++)
                {
                    if (imgInd == cols)
                        break;

                    buf[bufInd] = (b & _mask[k]) != 0 ? 1 : 0;
                    bufInd++;
                    if (bufInd == width)
                    {
                        var img = imgs[imgInd];
                        img.WriteRow(buf);
                        bufInd = 0;
                        imgInd++;
                    }
                }
            }
            if (i < frameY)
                continue;

            if (imgInd < cols)
            {
                Array.Fill(buf, 0, bufInd, width - bufInd);
                var img = imgs[imgInd];
                img.WriteRow(buf);
            }
            if ((i - frameY + 1) % height == 0)
            {
                SaveImgs(imgs);
                rowInd++;

                if (rowInd == rows)
                    break;

                if (i != totalHeight - 1)
                    CreateImgs(imgs, pathTemplate, rowInd, width, height);
            }
            bufInd = 0;
            imgInd = 0;
        }

        if (rowInd < rows && 
            (totalHeight - frameY) % height != 0)
        {
            Array.Fill(buf, 0);
            for (int i = (totalHeight - frameY) % height; i < height; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    imgs[j].WriteRow(buf);
                }
            }
            SaveImgs(imgs);
        }
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
        int height)
    {
        for (int i = 0; i < imgs.Length; i++)
        {
            var fileName = string.Format(pathTemplate, $"{row}x{i}");
            imgs[i] = new ImgBmp(fileName, width, height);
        }
    }

    private static int DivCeil(int i, int n) => n / i + (n % i != 0 ? 1 : 0);
}
