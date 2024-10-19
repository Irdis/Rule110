namespace Rule110;

public class BGlider : IGlider
{
    private static int[] _pattern = new [] { 1, 0 };
    public int Shift { get; } = 12;
    public int[] Pattern { get; } = _pattern;
}

