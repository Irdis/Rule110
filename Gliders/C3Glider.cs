namespace Rule110.Gliders;

public class C3Glider : IGlider
{
    private static int[] _pattern = TileUtils.ParseStrip("*.**.");
    public int EtherEnter { get; } = 12;
    public int EtherLeave { get; } = 4;
    public int[] Pattern { get; } = _pattern;
}

