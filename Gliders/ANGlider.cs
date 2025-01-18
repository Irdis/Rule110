namespace Rule110.Gliders;

public class ANGlider : IGlider
{
    private static int[] _etherEnters = [ 8, 4, 12 ];
    private static int[] _pattern = TileUtils.ParseStrip("*.**");
    public int EtherEnter { get; set; }
    public int EtherLeave { get; } = 4;
    public int[] Pattern { get; set; }

    public ANGlider(int n)
    {
        if (n == 0)
        {
            this.Pattern = [ 1, 0 ];
            return;
        }
        var m = n - 1;
        var len = 2 * (2 * (m / 3) + (m % 3 == 0 ? 0 : 1) + 1);
        this.EtherEnter = _etherEnters[m % 3];

        this.Pattern = new int[len];
        for (int i = 0; i < len; i++)
        {
            this.Pattern[i] =  _pattern[i % 4];
        }
    }

    // A4 Glider
    public static int[] UpOrder { get; } = [1, 0, 2];

    public static (int, int) Next(int gliderNumber, int dist = 0)
    {
        if (dist == -1)
            return (0, gliderNumber);

        var ind = Array.IndexOf(UpOrder, gliderNumber);
        var sign = dist < 0 ? -1 : 1;
        var distWrapAroundCount = Math.Abs(dist + 1) / UpOrder.Length;
        var tileCount = distWrapAroundCount;
        var nextInd = (dist + 1) % UpOrder.Length;
        nextInd = nextInd < 0 ? nextInd + UpOrder.Length : nextInd;

        return (-tileCount * sign, UpOrder[nextInd]);
    }

    public static int RightAlignment(int gliderNumber) => 
        gliderNumber == 1 ? -1 : 0;
    
    // A4 Spacing
    public static int[] AInitialGap { get; } = [2, 1, 3];

    public static (int, int) NextA(int gliderNumber, int dist = 0)
    {
        return Next(gliderNumber, AInitialGap[gliderNumber] + dist);
    }

    // E4 Glider Crossing
    private static int EUpPeriod = 6;
    
    public static int ECrossingInitialGap = 18;

    public static (int, int) NextECrossing(int gliderNumber, int dist = 0)
    {
        return NextA(gliderNumber, ECrossingInitialGap + dist * EUpPeriod);
    }
}

