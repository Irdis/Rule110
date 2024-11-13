namespace Rule110.Gliders;

public class GNGlider : IGlider
{
    public static int[] Prefix = TileUtils.ParseStrip("*....******.*..**.*.");
    public static int[][] Body = TileUtils.ParseStrips([
        "**",
        "*..*",
        "*...",
        "**",
        "*..*",
        "*...",
        "****",
        "*.....",

        "****",
        "*.",
        "*.**",
        "****",
        "*.",
        "*.",
    ]);
    public static TileSuffix Suffix = TileUtils.ParseSuffix([
        ("", 8),
        ("", 4),
        ("", 0),
        ("", 8),
        ("", 4),
        ("", 0),
        ("*.", 12),
        ("", 8),

        ("", 4),
        ("", 12),
        ("", 8),
        ("", 4),
        ("", 12),
        ("**", 8),
    ]);

    public int[] Pattern { get;  set; }
    public int EtherEnter { get; set; }
    public int EtherLeave { get; } = 4;

    public GNGlider(int n)
    {
        if (n == 0)
        {
            this.EtherEnter = 0;
            this.Pattern = Prefix;
            return;
        }
        n--;
        var pref = Prefix;
        var prefLen = Prefix.Length;
        var bodyLen = TileUtils.BodyLength(Body, n);
        var suff = Suffix.Options[n % Body.Length];
        var suffLen = suff.Length;

        var pattern = new int[prefLen + bodyLen + suffLen];
        var ind = 0;
        for (int i = 0; i < prefLen; i++)
            pattern[ind++] = pref[i];
        for (int i = 0; i <= n; i++)
        {
            var strip = Body[i % Body.Length];
            for (int j = 0; j < strip.Length; j++)
            {
                pattern[ind++] = strip[j];
            }
        }
        for (int i = 0; i < suffLen; i++)
            pattern[ind++] = suff[i];

        this.Pattern = pattern;
        this.EtherEnter = Suffix.EtherEntrances[n % Body.Length];
    }
}
