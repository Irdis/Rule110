namespace Rule110;

public class D1Glider : IGlider
{
    private static int[][] _prefix = [
        TileUtils.ParseStrip("*.***.*****"),
        TileUtils.ParseStrip("*.***.."),
    ];
    private static int[] _etherEntrances = [4, 0];
    public int Shift { get; }
    public int[] Pattern { get; }

    public D1Glider(int opt)
    {
        this.Shift = _etherEntrances[opt];
        this.Pattern = _prefix[opt];
    }
}

