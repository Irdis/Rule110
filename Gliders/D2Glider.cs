namespace Rule110.Gliders;

public class D2Glider : IGlider
{
    private static int[][] _prefix = [
        TileUtils.ParseStrip("*.*..**.."),
        TileUtils.ParseStrip("*.***...."),
    ];
    private static int[] _etherEntrances = [8, 8];
    public int EtherEnter { get; }
    public int EtherLeave { get; } = 4;
    public int[] Pattern { get; }

    public D2Glider(int opt)
    {
        this.EtherEnter = _etherEntrances[opt];
        this.Pattern = _prefix[opt];
    }
}
