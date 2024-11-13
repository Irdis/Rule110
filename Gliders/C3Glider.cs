namespace Rule110.Gliders;

public class C3Glider : IGlider
{
    private static int[][] _prefix = [
        TileUtils.ParseStrip("*.**."),
        TileUtils.ParseStrip("*.*...."),
    ];
    private static int[] _etherEntrances = [12, 0];
    public int EtherEnter { get; }
    public int EtherLeave { get; } = 4;
    public int[] Pattern { get; }

    public C3Glider(int opt)
    {
        this.EtherEnter = _etherEntrances[opt];
        this.Pattern = _prefix[opt];
    }
}

