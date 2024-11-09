namespace Rule110;

public class HGlider : IGlider
{
    private static int[] _pattern = TileUtils.ParseStrip("*.***.***********..**...*..**.*********"); 
    public int[] Pattern { get;  set; } = _pattern;
    public int Shift { get; set; } = 4;
}
