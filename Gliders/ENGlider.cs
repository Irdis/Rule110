namespace Rule110.Gliders;

public class ENGlider : IGlider
{
    public static int[] Prefix { get; } = TileUtils.ParseStrip("*........");

    public static int[][] Body { get; } = TileUtils.ParseStrips(["*..*", "*.", "*.**"]);

    public static int[] Suffix { get; set; } = [ 4, 12, 8 ];

    public int EtherEnter { get; set; }
    public int EtherLeave { get; } = 4;
    public int[] Pattern { get;  set; }

    public ENGlider(int n) 
    {
        var pref = Prefix;
        var prefLen = pref.Length;

        var strips = Body;
        var lastTileIndex = n - 1;
        var bodyLen = lastTileIndex < 0 ? 0 : TileUtils.BodyLength(strips, lastTileIndex);

        var suffixIndex = lastTileIndex < 0 ? strips.Length - 1 : lastTileIndex % strips.Length;
        var etherEntrance = Suffix[suffixIndex];

        var pattern = new int[prefLen + bodyLen];

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

        this.Pattern = pattern;
        this.EtherEnter = etherEntrance;
    }
}
