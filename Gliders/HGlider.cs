namespace Rule110.Gliders;

public class HGlider : IGlider
{
    private static int[] _pattern = TileUtils.ParseStrip("*.***.***********..**...*..**.*********"); 
    public int[] Pattern { get;  set; } = _pattern;
    public int EtherEnter { get; set; } = 4;
    public int EtherLeave { get; } = 4;
}
