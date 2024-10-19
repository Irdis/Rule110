namespace Rule110;

public class ANGlider : IGlider
{
    private static int[] _shifts = new []{ 8, 4, 12 };
    private static int[] _pattern = new [] { 1, 0, 1, 1 };
    public int Shift { get; set; }
    public int[] Pattern { get; set; }
    public ANGlider(int n)
    {
        if (n == 1)
        {
            this.Pattern = new[] { 1, 0 };
            return;
        }
        var m = n - 2;
        // _pattern repeats in following manner
        // 2 4 4 6 8 8 10 12 12
        var len = 2 * (2 * (m / 3) + (m % 3 == 0 ? 0 : 1) + 1);
        this.Shift = _shifts[m % 3];

        this.Pattern = new int[len];
        for (int i = 0; i < len; i++)
        {
            this.Pattern[i] =  _pattern[i % 4];
        }
    }
}

