namespace Rule110.Gliders;

public class BGlider : IGlider
{
    private static int[] _pattern = [ 1, 0 ];
    public int EtherEnter { get; } = 12;
    public int EtherLeave { get; } = 4;
    public int[] Pattern { get; } = _pattern;
}

