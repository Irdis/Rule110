namespace Rule110.Gliders;

public class EHatGlider : IGlider
{
    private static int[][] _pattern = TileUtils.ParseStrips([
        "*....*...*****.*.",
        "*.******.*****.*.",
        "*..*****.***..**.",
        "*....*.**..*..**.",
        "*.***.*.***...**.",
        "*.*..**.****..**.",
        "*.*..****..*..**.",
        "*..***.....*..**.",
    ]);
    private static int[] _etherEntrances = [
        0, 0, 0, 0, 0, 0, 0, 0
    ];

    public int EtherEnter { get; }
    public int EtherLeave { get; } = 4;
    public int[] Pattern { get; }

    public EHatGlider(int opt)
    {
        this.EtherEnter = _etherEntrances[opt];
        this.Pattern = _pattern[opt];
    }
}
