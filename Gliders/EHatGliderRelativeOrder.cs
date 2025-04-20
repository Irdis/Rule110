using System.Text;

namespace Rule110.Gliders;

public class EHatGliderRelativeOrder
{
    private static int[] _typeStart = [11, 15, 19, 22, 25, 29, 3, 7];
    private static int[] _startOffset = [1, 1, 0, 0, 0, 1, 0, 0, 0, 0, -1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0];
    private static int[] _initialNumberOfTriangles = [1, 2, 3, 3, 1, 1, 2, 1];
    private static int[][] _typePeriods; 
    private static int[] _period = [2, 4, 4, 4, 4, 3, 3, 4, 2];
    private const int TYPE_COUNT = 8;

    static EHatGliderRelativeOrder()
    {
        _typePeriods = new int[EHatGlider.Size][];
        for (int i = 0; i < EHatGlider.Size; i++)
        {
            _typePeriods[i] = RotatePeriodValues(i);
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

        var startGliderNumber = _typeStart[type];
        var startOffset = _startOffset[gliderNumber];
        startGliderNumber = (startGliderNumber + gliderNumber) % EHatGlider.Size;
        var periods = _typePeriods[startGliderNumber];

        var ethRem = ethCount % EHatGlider.Size;
        if (gliderNumber == 0)
        {
            for (int i = 0; i < periods.Length; i++)
            {
                if (ethRem < periods[i])
                {
                    break;
                }
                ethRem -= periods[i];
                offset++;
            }
        } 
        else 
        {
            offset += EHatGliderRelOrderOffsetTable.Table[gliderNumber][type][ethRem];
        }

        var nextGliderNumber = (startGliderNumber + ethCount) % EHatGlider.Size;
        return (offset + startOffset, nextGliderNumber);
    }

    public static void AnalyzeRelOrder(int startingGlider)
    {
        var ehGliderCollection = new EHatGliderCollection();
        var offsets = new int[EHatGlider.Size];

        int[][] offsetTable = new int[TYPE_COUNT][];
        for (int k = 0; k < 8; k++)
        {
            var offsetRow = new int[EHatGlider.Size];

            for (int i = 0; i < EHatGlider.Size; i++)
            {
                const int width = 500;
                const int height = 2;

                var background = new EtherBackground();

                var (ehOffset, ehNumber) = Next(startingGlider, i + 1, k);
                var nextStartingGlider = (startingGlider + 1) % EHatGlider.Size;
                var nextFollowupGlider = (ehNumber + 1) % EHatGlider.Size;
                var relOrderObserver = new EHatRelativeOrderObserver(
                    ehGliderCollection,
                    nextStartingGlider,
                    nextFollowupGlider
                );

                var scene = new Scene(width, background, [relOrderObserver]);
                var gliders = new List<(int, IGlider)>();
                var offset = 1;
                var alignment = 0;

                var alignDelta = EHatGlider.RightAlignment(startingGlider);
                gliders.Add((offset, ehGliderCollection.Get(startingGlider)));
                offset += alignDelta;
                alignment += alignDelta;

                offset += ehOffset;

                alignDelta = EHatGlider.RightAlignment(ehNumber);
                gliders.Add((offset, ehGliderCollection.Get(ehNumber)));
                offset += alignDelta;
                alignment += alignDelta;

                scene.Init(gliders);
                scene.InitComplete();
                for (int j = 1; j < height; j++)
                {
                    scene.Next();
                }
                scene.Complete();
                offsetRow[i] = relOrderObserver.Offset;
            }
            offsetTable[k] = offsetRow;
        }
        var sb = new StringBuilder();
        sb.Append("[");
        for (int i = 0; i < TYPE_COUNT; i++)
        {
            sb.Append("[");
            for (int j = 0; j < EHatGlider.Size; j++)
            {
                sb.Append(offsetTable[i][j]);
                sb.Append(",");
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append("],");
        }
        sb.Remove(sb.Length - 1, 1);
        sb.Append("],");
        Console.WriteLine(sb.ToString());
    }
}

public class EHatRelativeOrderObserver : IObserver
{
    private IGlider _first;
    private IGlider _second;

    public int Offset { get; set; }

    public EHatRelativeOrderObserver(EHatGliderCollection collection, int g1, int g2)
    {
        _first = collection.Get(g1);
        _second = collection.Get(g2);
    }

    public void Next(int lvl, int[] tape)
    {
        if (lvl == 0)
            return;
        var etherPointer = 4;
        var etherInd = 1;
        var tapePointer = 0;
        var state = 0;
        var etherTile = EtherBackground.Tile;
        var offset = 0;
        while (true)
        {
            switch (state)
            {
                case 0:// reading ether
                    if (!TestTile(ref tapePointer, etherPointer, tape))
                        state = 1;
                    else 
                    {
                        etherInd = (etherInd + 1) % 4;
                        etherPointer = EtherBackground.Periods[etherInd];
                    }
                    break;
                case 1: // reading glider 1
                    tapePointer += _first.Pattern.Length;
                    etherInd = _first.EtherEnter / 4;
                    etherPointer = _first.EtherEnter;
                    state = 2;
                    break;
                case 2:// reading ether
                    if (!TestTile(ref tapePointer, etherPointer, tape))
                        state = 3;
                    else 
                    {
                        etherInd = (etherInd + 1) % 4;
                        if (etherInd == 0)
                            offset++;
                        etherPointer = EtherBackground.Periods[etherInd];
                    }
                    break;
            }
            if (state == 3)
            {
                break;
            }
        }
        this.Offset = offset;
    }

    private bool TestTile(ref int position, 
        int etherPeriod, 
        int[] arr)
    {
        var p = position;
        var stripLength = etherPeriod == 12 ? 2 : 4;
        for (int i = 0; i < stripLength; i++)
        {
            if (arr[p + i] != EtherBackground.Tile[etherPeriod + i])
                return false;
        }
        position += stripLength;
        return true;
    }

    private void PrintTape(int[] tape, int[] p)
    {
        var limit = Math.Min(tape.Length, 150);
        for (int i = 0; i < limit; i++)
        {
            Console.Write(tape[i] == 1 ? '*' : '.');
        }
        Console.WriteLine();
        if (p.Length == 0)
        {
            return;
        }
        int cur = p[0];
        int curInd = 0;
        for (int i = 0; i < limit; i++)
        {
            if (i == cur)
            {
                Console.Write('^');
                curInd++;
                if (curInd == p.Length)
                    break;
                cur = p[curInd];
            } else {
                Console.Write(' ');
            }
        }
        Console.WriteLine();
    }

    public void Complete() {}
}
