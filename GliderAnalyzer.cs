using System.Linq;
using Rule110.Gliders;

namespace Rule110;

public class GliderAnalyzer
{
    public List<IGlider> Analyze(IGlider glider)
    {
        var width = Width(glider);
        var background = new EtherBackground();
        var miniScene = new Scene(width, background);
        miniScene.Init([(1, glider)]);
        miniScene.InitComplete();
        var initialGlider = ExtractGlider(0, 12, miniScene.Tape);
        var currentGlider = initialGlider;

        var res = new List<IGlider>();
        res.Add(initialGlider);
        while(true)
        {
            width = Width(currentGlider);
            background = new EtherBackground();
            miniScene = new Scene(width, background);
            miniScene.Init([(1, currentGlider)]);
            miniScene.InitComplete();
            miniScene.Next();
            var nextGlider = ExtractGlider(4, 0, miniScene.Tape); 
            if (AreTheSame(initialGlider, nextGlider))
                break;
            res.Add(nextGlider);
            currentGlider = nextGlider;
        }
        return res;
    }

    private bool AreTheSame(IGlider a, IGlider b)
    {
        if (a.EtherEnter != b.EtherEnter)
            return false;
        if (a.EtherLeave != b.EtherLeave)
            return false;
        return a.Pattern.SequenceEqual(b.Pattern);
    }
    
    private IGlider ExtractGlider(int start, int end, int[] tape)
    {
        var (patternBegins, etherLeave) = TrimStart(tape, start);
        var (patternEnds, etherEnter) = TrimEnd(tape, end, patternBegins);

        var patternLen = patternEnds - patternBegins + 1;
        var pattern = new int[patternLen];

        Array.Copy(tape, patternBegins, pattern, 0, patternLen);

        return new GenericGlider
        {
            EtherEnter = etherEnter,
            EtherLeave = etherLeave,
            Pattern = pattern
        };
    }

    private void PrintTape(int[] tape, int p1, int p2)
    {
        for (int i = 0; i < tape.Length; i++)
        {
            Console.Write(tape[i] == 1 ? '*' : '.');
        }
        Console.WriteLine();
        for (int i = 0; i < tape.Length; i++)
        {
            Console.Write(i == p1 || i == p1 ? '^' : ' ');
        }
        Console.WriteLine();
    }

    private (int, int) TrimStart(int[] tape, int start)
    {
        var pointer = 0;
        var periods = EtherBackground.Periods;

        var periodNumber = Array.IndexOf(periods, start);
        var patternBegins = 0;
        while(true)
        {
            if (!TestTile(ref pointer, 
                periods[periodNumber], 
                tape, 
                tape.Length - 1,
                forward: true))
            {
                patternBegins = pointer;
                break;
            }
            periodNumber += 1;
            periodNumber %= 4;
        }
        var etherLeave = periods[periodNumber];
        return (patternBegins, etherLeave);
    }

    private (int, int) TrimEnd(int[] tape, int end, int begin)
    {
        var pointer = tape.Length - 1;
        var periods = EtherBackground.Periods;

        var periodNumber = Array.IndexOf(periods, end);
        var patternEnds = 0;
        while(true)
        {
            if (!TestTile(ref pointer, 
                periods[periodNumber], 
                tape, 
                begin,
                forward: false))
            {
                patternEnds = pointer;
                break;
            }
            periodNumber -= 1;
            periodNumber += 4;
            periodNumber %= 4;
        }
        var etherEnter = periods[(periodNumber + 1) % 4];
        return (patternEnds, etherEnter);
    }

    private bool TestTile(ref int position, 
        int etherPeriod, 
        int[] arr,
        int border,
        bool forward)
    {
        var p = position;
        var stripLength = etherPeriod == 12 ? 2 : 4;
        var delta = (forward ? 1 : -1) * stripLength;

        if (forward && position + delta >= border ||
            !forward && position + delta <= border)
            return false;

        var offset = forward ? 0 : - stripLength + 1;
        for (int i = 0; i < stripLength; i++)
        {
            if (arr[p + i + offset] != EtherBackground.Tile[etherPeriod + i])
                return false;
        }
        position += (forward ? 1 : -1) * stripLength;
        return true;
    }

    private static int Width(IGlider glider)
    {
        return EtherBackground.PERIOD_X + 
            glider.EtherLeave + 
            glider.Pattern.Length + 
            EtherBackground.PERIOD_X - glider.EtherEnter + 
            EtherBackground.PERIOD_X;
    }
}
