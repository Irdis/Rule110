namespace Rule110;

public class C3Glider : IGlider
{
    private static int[][] _prefix = [
        TileUtils.ParseStrip("*.**."),
        TileUtils.ParseStrip("*.*...."),
    ];
    private static int[] _etherEntrances = [12, 0];
    public int Shift { get; }
    public int[] Pattern { get; }

    public C3Glider(int opt)
    {
        this.Shift = _etherEntrances[opt];
        this.Pattern = _prefix[opt];
    }
}

