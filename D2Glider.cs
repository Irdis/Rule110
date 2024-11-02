namespace Rule110;

public class D2Glider : IGlider
{
    private static int[][] _prefix = [
        TileUtils.ParseStrip("* *  **  "),
        TileUtils.ParseStrip("* ***    "),
    ];
    private static int[] _etherEntrances = [8, 8];
    public int Shift { get; }
    public int[] Pattern { get; }

    public D2Glider(int opt)
    {
        this.Shift = _etherEntrances[opt];
        this.Pattern = _prefix[opt];
    }
}
