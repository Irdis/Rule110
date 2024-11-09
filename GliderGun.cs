
namespace Rule110;

public class GliderGun : IGlider
{
    private static int[] _pattern = TileUtils.ParseStrip("*...*.**...***.***..***...*"); 
    public int[] Pattern { get;  set; } = _pattern;
    public int Shift { get; set; } = 4;
}
