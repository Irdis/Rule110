namespace Rule110;

public static class EStrip 
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

}
