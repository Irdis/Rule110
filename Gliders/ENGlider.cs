namespace Rule110.Gliders;

public class ENGlider : IGlider
{
    public static int[] Prefix { get; } = TileUtils.ParseStrip("*........");

    public static int[][] Body { get; } = TileUtils.ParseStrips(["*..*", "*.", "*.**"]);

    public static int[] Suffix { get; set; } = [ 4, 12, 8 ];

    public int EtherEnter { get; set; }
    public int EtherLeave { get; } = 4;
    public int[] Pattern { get;  set; }

    public ENGlider(int n) 
    {
        var pref = Prefix;
        var prefLen = pref.Length;

        var strips = Body;
        var lastTileIndex = n - 1;
        var bodyLen = lastTileIndex < 0 ? 0 : TileUtils.BodyLength(strips, lastTileIndex);

        var suffixIndex = lastTileIndex < 0 ? strips.Length - 1 : lastTileIndex % strips.Length;
        var etherEntrance = Suffix[suffixIndex];

        var pattern = new int[prefLen + bodyLen];

        var ind = 0;
        for (int i = 0; i < prefLen; i++)
        {
            pattern[ind++] = pref[i];
        }
        for (int i = 0; i <= lastTileIndex; i++)
        {
            var strip = strips[i % strips.Length];
            for (int j = 0; j < strip.Length; j++)
            {
                pattern[ind++] = strip[j];
            }
        }

        this.Pattern = pattern;
        this.EtherEnter = etherEntrance;
    }

    public static int[] OverOrder { get; } = [
            5, 12, 4, 11, 3, 10, 2, 9,
            1, 8, 0, 7, 14, 6, 13
    ];

    public static int[] OverOrderSectionLengths { get; } = 
        [ 8, 7 ];

    public static int RightAlignment(int gliderNumber, int n)
    {
        if (n > 0 && n % 3 == 0)
            return 0;

        if ((n + 2) % 3 == 0)
            return gliderNumber switch 
            {
                6 => 1,
                13 => 1,
                10 => 1,
                2 => 1,
                9 => 1,
                _ => 0
            };
        if ((n + 1) % 3 == 0)
            return gliderNumber switch {
                7 => 1,
                14 => 1,
                6 => 1,
                13 => 1,
                11 => 1,
                3 => 1,
                10 => 1,
                2 => 1,
                9 => 1,
                0 => 1,
                _ => 0
            };

        return gliderNumber switch {
            13 => 1,
             _ => 0
        };
    }

    public static (int, int) Adjacent(int gliderNumber, int tileCount)
    {
        var (offset, gliderNumber2) = Next(gliderNumber, 7);
        return (tileCount + 1 + offset, gliderNumber2);
    }

    public static (int, int) Next(int gliderNumber, int dist) 
    {
        if (dist == -1)
            return (0, gliderNumber);

        var ind = Array.IndexOf(OverOrder, gliderNumber);
        var sign = dist < 0 ? -1 : 1;
        var distWrapAroundCount = Math.Abs(dist + 1) / OverOrder.Length;
        var tileCount = 2 * distWrapAroundCount;
        var nextInd = (ind + dist + 1) % OverOrder.Length;
        nextInd = nextInd < 0 ? nextInd + OverOrder.Length : nextInd;

        var initRow = GetOrderOverRow(ind);
        var destRow = GetOrderOverRow(nextInd);
        if (dist < 0)
        {
            if (destRow > initRow || (destRow == initRow && nextInd > ind))
                initRow += 2;
            tileCount += initRow - destRow;
        }
        else
        {
            if (destRow < initRow || (destRow == initRow && nextInd < ind))
                destRow += 2;
            tileCount += destRow - initRow;
        }

        var nextGliderNumber = OverOrder[nextInd];
        return (-tileCount * sign, nextGliderNumber);
    }


    private static int GetOrderOverRow(int ind)
    {
        var sectionLen = OverOrderSectionLengths[0];
        if (ind < sectionLen)
            return 0;

        return 1;
    }
}
