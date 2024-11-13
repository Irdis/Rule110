namespace Rule110.Gliders;

public class C1Glider : IGlider
{
    private static int[][] _prefix = [
        TileUtils.ParseStrip("*.*******"),
        TileUtils.ParseStrip("*...."),
    ];
    private static int[] _etherEntrances = [4, 0];
    public int EtherEnter { get; }
    public int EtherLeave { get; } = 4;
    public int[] Pattern { get; }

    public C1Glider(int opt)
    {
        this.EtherEnter = _etherEntrances[opt];
        this.Pattern = _prefix[opt];
    }
}

