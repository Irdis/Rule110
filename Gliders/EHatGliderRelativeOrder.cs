namespace Rule110.Gliders;

public class EHatGliderRelativeOrder
{
    private static (int, int)[] _typeStart = [
        (1, 11), 
        (1, 15), 
        (1, 19), 
        (1, 22), 
        (1, 25)
    ];
    private static int[][] _typePeriods; 
    private static int[] _period = [2, 4, 4, 4, 4, 3, 3, 4, 2];

    static EHatGliderRelativeOrder()
    {
        _typePeriods = new int[EHatGlider.Size][];
        for (int i = 0; i < EHatGlider.Size; i++)
        {
            _typePeriods[i] = RotatePeriodValues(i);
            if (i == 15)
            {
                for (int j = 0; j < _typePeriods[i].Length; j++)
                {
                    Console.Write(_typePeriods[i][j]);
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
        }
    }

    public static int[] RotatePeriodValues(int gliderNumber) 
    {
        GetStartValues(gliderNumber, out var periodIndex, out var startOffset);

        var sliceSize = _period.Length;
        if (periodIndex > 0 && startOffset == 0)
            sliceSize--;

        var slice = new int[sliceSize];
        var sliceInd = 0;

        // part1
        if (periodIndex != _period.Length - 1)
            slice[sliceInd++] = _period[periodIndex] - startOffset;

        for (int i = periodIndex + 1; i < _period.Length - 1; i++)
        {
            slice[sliceInd++] = _period[i];
        }

        var carryElement = _period[_period.Length - 1];
        if (periodIndex == _period.Length - 1)
            carryElement -= startOffset;

        // part2
        var firstPeriod = _period[0];
        if (periodIndex == 0)
            firstPeriod = startOffset;

        slice[sliceInd++] = firstPeriod + carryElement;
        for (int i = 1; i < periodIndex; i++)
        {
            slice[sliceInd++] = _period[i];
        }
        if (periodIndex > 0 && startOffset > 0)
        {
            slice[sliceInd++] = startOffset;
        }
        return slice;
    }

    private static void GetStartValues(int gliderNumber, out int periodIndex, out int startOffset)
    {
        periodIndex = 0;
        startOffset = gliderNumber;
        for (; periodIndex < _period.Length; periodIndex++)
        {
            if (_period[periodIndex] > startOffset)
                break;
            startOffset -= _period[periodIndex];
        }
    }

    public static (int, int) Next(int gliderNumber, int ethCount, int type)
    {
        ethCount--;

        var offset = 8 * (ethCount / EHatGlider.Size);

        var (startOffset, startGliderNumber) = _typeStart[type];
        var periods = _typePeriods[startGliderNumber];

        var ethRem = ethCount % EHatGlider.Size;
        for (int i = 0; i < periods.Length; i++)
        {
            if (ethRem < periods[i])
            {
                break;
            }
            ethRem -= periods[i];
            offset++;
        }

        var nextGliderNumber = (startGliderNumber + ethCount) % EHatGlider.Size;
        return (offset + startOffset, nextGliderNumber);
    }
}
