namespace Rule110;

public class D2Glider : IGlider
{
    private static int[][] _prefix = [
        [ 1, 0, 1, 0, 0, 1, 1, 0, 0 ],
        [ 1, 0, 1, 1, 1, 0, 0, 0, 0 ]
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
