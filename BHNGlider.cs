namespace Rule110;

public class BHNGlider : IGlider
{
    public static TileSuffix BSuffix { get; } = TileUtils.ParseSuffix(new [] {
        ("  * ", 0),
        (" ***", 4),
        ("**  ", 8),
        (" * *", 12),
        ("******", 4),
        ("*     ", 8),
        ("*    *", 12),
        ("*   ", 0),
        ("*  *", 4),
        ("* **", 8),
        ("*** ", 12),
        ("* ", 0),
        ("**", 4),
        ("  ", 8),
        ("* **   *** ", 12),
        ("", 0),
    });

    // 3 -> 15 -> 11 -> 7 -> 3
    // 10 -> 6 -> 2 -> 14 -> 10
    public static int[] ThinTiles = new[] { 1, 2, 3 };
    public static TilePrefix BPrefix { get; } = TileUtils.ParsePrefix(new [] {
        ("*    ", 3),
        ("* *****", 7),
        ("*  **", 10),
    });

    public int Shift { get; }
    public int[] Pattern { get; }


    public BHNGlider(int n, int opt)
    {
        var tileCount = n + (n - 1) / 3 + 1 - ((n - 1) % 3 < ThinTiles[opt] ? 1 : 0);

        var tile = TileUtils.B;
        var row = BPrefix.TileEntrances[opt];
        var bodyWidth = 7 * tileCount - (tileCount - 1) / 4 - ((tileCount - 1) % 4 >= ThinTiles[opt] ? 1 : 0);

        var lastRow = (row + tile.NextRow * (tileCount - 1)) % tile.YPeriod;
        var pref = BPrefix.Options[opt];
        var suff = BSuffix.Options[lastRow];
        var pattern = new int[bodyWidth + pref.Length + suff.Length];

        var ind = 0;
        foreach(var bit in pref)
        {
            pattern[ind++] = bit;
        }

        for (int i = 0; i < tileCount; i++)
        {
            AddRow(pattern, tile, row, ref ind);
            row = (row + tile.NextRow) % tile.YPeriod;
        }

        foreach(var bit in suff)
        {
            pattern[ind++] = bit;
        }

        this.Shift = BSuffix.EtherEntrances[lastRow];
        this.Pattern = pattern;
   }

    public void AddRow(int[] pattern, Tile t, int row, ref int ind)
    {
        for(int i = 0; i < t.XPeriod; i++)
        {
            if (row >= t.NextRow && i == 0)
            {
                continue;
            }
            pattern[ind++] = t.Arr[row * t.XPeriod + i];
        }
    }
}

