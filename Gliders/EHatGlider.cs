namespace Rule110.Gliders;

public class EHatGlider : IGlider
{
    private static int[] _pattern = TileUtils.ParseStrip("*....*...*****.*.");
    public int EtherEnter { get; } = 0;
    public int EtherLeave { get; } = 4;
    public int[] Pattern { get; }
}
