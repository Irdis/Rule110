namespace Rule110.Gliders;

public abstract class BNHatGlider : IGlider
{
    public static Tile B { get; } = TileUtils.ParseTile([
        "*******",
        "*.....*",
        "*....**",
        "*...**.",
        "*..***.",
        "*.**.**",
        "******.",
        "*....**",
        "*...**.",
        "*..****",
        "*.**...",
        "****..*",
        "*..*.**",
        "*.****.",
        "***..**",
        "..*.**.",
    ], 12);

    // 3 -> 15 -> 11 -> 7 -> 3
    public static int ThinTile = 1;
    public static TilePrefix BPrefix { get; } = TileUtils.ParsePrefix("*....", 3);

    public int EtherLeave { get; } = 4;
    public int EtherEnter { get; }
    public int[] Pattern { get; }
    public abstract TileSuffix Suffix { get; }

    public BNHatGlider(int n)
    {
        var tileCount = n + 1 + n / 3 + (n % 3 < ThinTile ? 0 : 1);

        var tile = B;
        var row = BPrefix.TileEntrance;
        var bodyWidth = 7 * tileCount - (tileCount - 1) / 4 - ((tileCount - 1) % 4 >= ThinTile ? 1 : 0);

        var lastRow = (row + tile.NextRow * (tileCount - 1)) % tile.YPeriod;
        var pref = BPrefix.Arr;
        var suff = this.Suffix.Options[lastRow];
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

        this.EtherEnter = this.Suffix.EtherEntrances[lastRow];
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

