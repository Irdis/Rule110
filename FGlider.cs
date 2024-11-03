namespace Rule110;

public class FGlider : IGlider
{
    private static int[][] _pattern = TileUtils.ParseStrips([
        "*.***.*.***...*",
        "*.*..**.*******",
        "*.*..**....",
        "*.***.**.*.",
        "*.*.....**.",
        "*..**",
        "*....*.....",
        "*.******.******",

        "*..*****...",
        "*....*.**..*..*",
    ]);
    private static int[] _etherEntrances = [
        4, 4, 0, 0, 0, 8, 0, 4,
        0, 4
    ];

    public int Shift { get; }
    public int[] Pattern { get; }

    public FGlider(int opt)
    {
        this.Shift = _etherEntrances[opt];
        this.Pattern = _pattern[opt];
    }
}
