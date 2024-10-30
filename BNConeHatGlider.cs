namespace Rule110;

public class BNConeHatGlider : BNHatGlider, IGlider
{
    public static TileSuffix BSuffix { get; } = TileUtils.ParseSuffix(new [] {
        ("  * *** **     *  ** ", 0),
        (" **** ****    ** ", 0),
        ("**  ***  *   ", 0),
        (" * ** * **  *", 4),
        ("********** **", 8),
        ("*        *** ", 12),
        ("*       ** ", 0),
        ("*      ", 0),
        ("*     *", 4),
        ("*    **", 8),
        ("*   ** ", 12),
        ("*  ******", 4),
        ("* **     ", 8),
        ("****    *", 12),
        ("*  *   ", 0),
        ("* **  *", 4),
    });

    public override TileSuffix Suffix => BSuffix;

    public BNConeHatGlider(int n, int opt) 
        : base(n, opt) {}
}

