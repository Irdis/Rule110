namespace Rule110;

public class TileUtils
{
    public static Tile B { get; } = ParseTile(new []{
        "*******",
        "*     *",
        "*    **",
        "*   ** ",
        "*  *** ",
        "* ** **",
        "****** ",
        "*    **",
        "*   ** ",
        "*  ****",
        "* **   ",
        "****  *",
        "*  * **",
        "* **** ",
        "***  **",
        "  * ** ",
    }, 12);

    public static Tile ParseTile(string[] stars, int nextRow)
    {
        var len = stars.Length;
        var per = stars[0].Length;

        var arr = new int[per * len];
        for (int i = 0; i < len; i++)
        {
            for (int j = 0; j < stars[i].Length; j++)
            {
                arr[i * per + j] = stars[i][j] == '*' 
                    ? 1 : 0;
            }
            
        }

        var tile = new Tile {
            XPeriod = per,
            YPeriod = len,
            Arr = arr,
            NextRow = nextRow
        };
        return tile;
    }

    public static TilePrefix ParsePrefix((string, int)[] stars)
    {
        var len = stars.Length;

        var entr = new int[len];
        var opt = new List<int[]>();
        for (int i = 0; i < len; i++)
        {
            var (str, entrance) = stars[i];
            entr[i] = entrance;
            var prefix = new int[str.Length];
            for (int j = 0; j < str.Length; j++)
            {
                prefix[j] = str[j] == '*' ? 1 : 0;
            }
            opt.Add(prefix);
        }

        var tilePrefix = new TilePrefix {
            Options = opt,
            TileEntrances = entr
        };
        return tilePrefix;
    }

    public static TileSuffix ParseSuffix((string, int)[] stars)
    {
        var len = stars.Length;

        var entr = new int[len];
        var opt = new List<int[]>();
        for (int i = 0; i < len; i++)
        {
            var (str, entrance) = stars[i];
            entr[i] = entrance;
            var suffix = new int[str.Length];
            for (int j = 0; j < str.Length; j++)
            {
                suffix[j] = str[j] == '*' ? 1 : 0;
            }
            opt.Add(suffix);
        }

        var tileSuffix = new TileSuffix {
            Options = opt,
            EtherEntrances = entr
        };
        return tileSuffix;
    }
}

