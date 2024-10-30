namespace Rule110;

public class C2Glider : IGlider
{
    private static int[][] _prefix = [
        [ 1, 0, 0, 0, 0, 0, 0],
        [ 1, 0, 1, 0, 0, 1, 1]
    ];
    private static int[] _etherEntrances = [8, 8];
    public int Shift { get; }
    public int[] Pattern { get; }

    public C2Glider(int opt)
    {
        this.Shift = _etherEntrances[opt];
        this.Pattern = _prefix[opt];
    }
}

