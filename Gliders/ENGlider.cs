namespace Rule110.Gliders;

public class ENGlider : IGlider
{
    public static int[][] Prefix { get; } = TileUtils.ParseStrips([
        "*........",
        "*.*....**",
        "*.*",
        "*.***",
    ]);

    public static int[][][] Body { get; } = [
        TileUtils.ParseStrips(["*..*", "*.", "*.**"]),
        TileUtils.ParseStrips(["*..*", "*...", "**"]),
        TileUtils.ParseStrips(["*.....", "", "****"]),
        TileUtils.ParseStrips(["*.", "******", "*."]),
    ];

    public static int[] FirstStripOffset { get; } = [
        -1, -1, 0, 0
    ];

    public static TileSuffix[] Suffix { get; set; } = [
        TileUtils.ParseSuffix([
                ("", 4),
                ("", 12),
                ("", 8),
        ]),
        TileUtils.ParseSuffix([
                ("", 4),
                ("", 0),
                ("", 8),
        ]),
        TileUtils.ParseSuffix([
                ("", 8),
                ("", 0),
                ("*.", 12),
        ]),
        TileUtils.ParseSuffix([
                ("**", 8),
                ("", 4),
                ("", 12),
        ])
    ];

    public int EtherEnter { get; set; }
    public int EtherLeave { get; } = 4;
    public int[] Pattern { get;  set; }

    public ENGlider(int n, int opt) 
    {
        var pref = Prefix[opt];
        var prefLen = pref.Length;

        var strips = Body[opt];
        var lastTileIndex = n + FirstStripOffset[opt];
        var bodyLen = lastTileIndex < 0 ? 0 : TileUtils.BodyLength(strips, lastTileIndex);

        var suffixIndex = lastTileIndex < 0 ? strips.Length - 1 : lastTileIndex % strips.Length;
        var suff = Suffix[opt];
        var suffArr = suff.Options[suffixIndex];
        var suffLen = suffArr.Length;
        var etherEntrance = suff.EtherEntrances[suffixIndex];

        var pattern = new int[prefLen + bodyLen + suffLen];

        var ind = 0;
        for (int i = 0; i < prefLen; i++)
        {
            pattern[ind++] = pref[i];
        }
        for (int i = 0; i <= lastTileIndex; i++)
        {
            var strip = strips[i % strips.Length];
            for (int j = 0; j < strip.Length; j++)
            {
                pattern[ind++] = strip[j];
            }
        }
        for (int i = 0; i < suffLen; i++)
        {
            pattern[ind++] = suffArr[i];
        }

        this.Pattern = pattern;
        this.EtherEnter = etherEntrance;
    }
}
