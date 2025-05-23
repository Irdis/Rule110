namespace Rule110.Gliders;

public class EHatGlider : IGlider
{
    private static int[] _pattern = TileUtils.ParseStrip("*....*...*****.*.");
    public int EtherEnter { get; } = 0;
    public int EtherLeave { get; } = 4;
    public int[] Pattern { get; } = _pattern;

    public static int Size { get; } = 30;

    public static int[] OverOrder { get; } = [
            24, 1, 8, 15, 22, 29, 6,
            13, 20, 27, 4, 11, 18, 25, 2,
            9, 16, 23, 0, 7, 14, 21, 28,
            5, 12, 19, 26, 3, 10, 17
    ];
    public static int[] OverOrderSectionLengths { get; } = 
        [ 7, 8, 8, 7 ];

    public static int RightAlignment(int gliderNumber) =>
        gliderNumber switch 
        {
            1 => -1,
            27 => -1,
            5 => -1,
            10 => 1,
            17 => -1,
            _ => 0
        };

    public static (int, int) Next(int gliderNumber, int dist = 0) 
    {
        if (dist == -1)
            return (0, gliderNumber);

        var ind = Array.IndexOf(OverOrder, gliderNumber);
        var sign = dist < 0 ? -1 : 1;
        var distWrapAroundCount = Math.Abs(dist + 1) / OverOrder.Length;
        var tileCount = 4 * distWrapAroundCount;
        var nextInd = (ind + dist + 1) % OverOrder.Length;
        nextInd = nextInd < 0 ? nextInd + OverOrder.Length : nextInd;

        var initRow = GetOrderOverRow(ind);
        var destRow = GetOrderOverRow(nextInd);
        if (dist < 0)
        {
            if (destRow > initRow || (destRow == initRow && nextInd > ind))
                initRow += 4;
            tileCount += initRow - destRow;
        }
        else
        {
            if (destRow < initRow || (destRow == initRow && nextInd < ind))
                destRow += 4;
            tileCount += destRow - initRow;
        }

        if (nextInd == OverOrder.Length - 1)
        {
            tileCount--;
        } 
        else if (nextInd == 0)
        {
            tileCount++;
        }
        
        var nextGliderNumber = OverOrder[nextInd];
        return (-tileCount * sign, nextGliderNumber);
    }

    private static int GetOrderOverRow(int ind)
    {
        var sectionLen = OverOrderSectionLengths[0];
        if (ind < sectionLen)
            return 0;

        sectionLen += OverOrderSectionLengths[1];
        if (ind < sectionLen)
            return 1;

        sectionLen += OverOrderSectionLengths[2];
        if (ind < sectionLen)
            return 2;

        return 3;
    }
}
