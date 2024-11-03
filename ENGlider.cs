namespace Rule110;

public class ENGlider : IGlider
{
    public int Shift { get; set; }
    public int[] Pattern { get;  set; }

    public ENGlider(int n, int opt) 
    {
        var pref = EStrip.Prefix[opt];
        var prefLen = pref.Length;

        var strips = EStrip.Body[opt];
        var lastTileIndex = n + EStrip.FirstStripOffset[opt];
        var bodyLen = lastTileIndex < 0 ? 0 : TileUtils.BodyLength(strips, lastTileIndex);

        var suffixIndex = lastTileIndex < 0 ? strips.Length - 1 : lastTileIndex % strips.Length;
        var suff = EStrip.Suffix[opt];
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
        this.Shift = etherEntrance;
    }
}
