namespace Rule110;
public class EHatGlider : IGlider
{
    private static int[][] _pattern = [
        TileUtils.ParseStrip("*    *   ***** * "),
        TileUtils.ParseStrip("* ****** ***** * "),
        TileUtils.ParseStrip("*  ***** ***  ** "),
        TileUtils.ParseStrip("*    * **  *  ** "),
        TileUtils.ParseStrip("* *** * ***   ** "),
        TileUtils.ParseStrip("* *  ** ****  ** "),
        TileUtils.ParseStrip("* *  ****  *  ** "),
        TileUtils.ParseStrip("*  ***     *  ** "),
    ];
    private static int[] _etherEntrances = [
        0, 0, 0, 0, 0, 0, 0, 0
    ];

    public int Shift { get; }
    public int[] Pattern { get; }

    public EHatGlider(int opt)
    {
        this.Shift = _etherEntrances[opt];
        this.Pattern = _pattern[opt];
    }
}
