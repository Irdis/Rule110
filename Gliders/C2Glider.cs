namespace Rule110.Gliders;

public class C2Glider : IGlider
{
    private static int[] _pattern = TileUtils.ParseStrip("*......");

    public int EtherEnter { get; } = 8;
    public int EtherLeave { get; } = 4;
    public int[] Pattern { get; } = _pattern;

    public static int[] OverOrder { get; } = [5, 1, 4, 0, 3, 6, 2];
    public static int OverShift { get; } = 3;

    public static (int, int) Next(int gliderNumber, int dist = 0) 
    {
        var ordInd = Array.IndexOf(OverOrder, gliderNumber);
        var nextInd = ordInd + dist + OverShift;
        var tilesCount = nextInd / OverOrder.Length;
        var nextOrdInd = nextInd % OverOrder.Length;

        return (-tilesCount, nextOrdInd);
    }
}

