namespace Rule110;

public class BHNGlider : IGlider
{
    private static int[] _pattern0 = new [] { 1, 0, 0, 0, 0, 1, 0, 0, 0, 1, 1, 0, 0, 1, 0, 1, 1, 0 };
    /*private static int[] _pattern0 = new [] { 1, 0, 0, 0, 0, 1, 0, 0, 0, 1, 1, 0, 0, 1, 0, 1, 1, 0, 1, 1, 1, 0, 0, 1, 1, 0 };*/
    /*private static int[] _pattern0 = new [] { 1, 0, 0, 0, 0, 1, 0, 0, 0, 1, 1, 0, 0, 1, 0, 1, 1, 0, 1, 1, 1, 0, 0, 1, 1, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0 };*/
    /*private static int[] _pattern1 = new [] { 1, 0, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0 };*/
    public int Shift { get; } = 0;
    public int[] Pattern { get; } = _pattern0;
}

