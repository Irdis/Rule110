namespace Rule110.Gliders;

public class C2Glider : IGlider
{
    private static int[] _pattern = TileUtils.ParseStrip("*......");
    public int EtherEnter { get; } = 8;
    public int EtherLeave { get; } = 4;
    public int[] Pattern { get; } = _pattern;
}

