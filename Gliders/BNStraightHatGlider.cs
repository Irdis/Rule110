namespace Rule110.Gliders;

public class BNStraightHatGlider : BNHatGlider, IGlider
{
    public static TileSuffix BSuffix { get; } = TileUtils.ParseSuffix([
        ("..*.", 0),
        (".***", 4),
        ("**..", 8),
        (".*.*", 12),
        ("******", 4),
        ("*.....", 8),
        ("*....*", 12),
        ("*...", 0),
        ("*..*", 4),
        ("*.**", 8),
        ("***.", 12),
        ("*.", 0),
        ("**", 4),
        ("..", 8),
        ("*.**...***.", 12),
        ("", 0),
    ]);

    public override TileSuffix Suffix => BSuffix;

    public BNStraightHatGlider(int n) 
        : base(n) {}
}

