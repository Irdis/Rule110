namespace Rule110.Gliders;

public class BNConeHatGlider : BNHatGlider, IGlider
{
    public static TileSuffix BSuffix { get; } = TileUtils.ParseSuffix([
        ("..*.***.**.....*..**.", 0),
        (".****.****....**.", 0),
        ("**..***..*...", 0),
        (".*.**.*.**..*", 4),
        ("**********.**", 8),
        ("*........***.", 12),
        ("*.......**.", 0),
        ("*......", 0),
        ("*.....*", 4),
        ("*....**", 8),
        ("*...**.", 12),
        ("*..******", 4),
        ("*.**.....", 8),
        ("****....*", 12),
        ("*..*...", 0),
        ("*.**..*", 4),
    ]);

    public override TileSuffix Suffix => BSuffix;

    public BNConeHatGlider(int n) 
        : base(n) {}
}

