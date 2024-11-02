namespace Rule110;

public class ENGlider : IGlider
{
    public static int[][] EPrefix { get; } = [
        TileUtils.ParseStrip("*        "),
    ];

    public static int[][][] EStrips { get; } = [
        TileUtils.ParseStrips(["*  *", "* ", "* **"]),
    ];
    public static int[][] EEtherEntrances { get; } = [
        [ 4, 12, 8 ]
    ];

    public int Shift { get; }
    public int[] Pattern { get; }

    public ENGlider(int n, int opt) 
    {
        var pref = EPrefix[opt];
        var prefLen = pref.Length;

        var strips = EStrips[opt];
        var bodyLen = n == 0 ? 0 : TileUtils.BodyLength(strips, n - 1);

        var etherEntrances = EEtherEntrances[opt];

        var pattern = new int[prefLen + bodyLen];

        var ind = 0;
        for (int i = 0; i < prefLen; i++)
        {
            pattern[ind++] = pref[i];
        }
        for (int i = 0; i < n; i++)
        {
            var strip = strips[i % strips.Length];
            for (int j = 0; j < strip.Length; j++)
            {
                pattern[ind++] = strip[j];
            }
        }

        this.Pattern = pattern;
        this.Shift = n == 0 
            ? etherEntrances[^1] 
            : etherEntrances[(n - 1) % strips.Length];
    }
}
