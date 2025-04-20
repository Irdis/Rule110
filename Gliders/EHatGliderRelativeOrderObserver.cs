using System.Text;

namespace Rule110.Gliders;

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
