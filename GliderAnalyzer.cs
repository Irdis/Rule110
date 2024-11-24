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
        var (patternEnds, etherEnter) = TrimEnd(tape, end);

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

    private (int, int) TrimStart(int[] tape, int start)
    {
        var pointer = 0;
        var periods = EtherBackground.Periods;

        var periodNumber = Array.IndexOf(periods, start);
        var patternBegins = 0;
        while(true)
        {
            if (!TestTile(ref pointer, periods[periodNumber], tape, 
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

    private (int, int) TrimEnd(int[] tape, int end)
    {
        var pointer = tape.Length - 1;
        var periods = EtherBackground.Periods;

        var periodNumber = Array.IndexOf(periods, end);
        var patternEnds = 0;
        while(true)
        {
            if (!TestTile(ref pointer, periods[periodNumber], tape, 
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
        bool forward)
    {
        var p = position;
        var stripLength = etherPeriod == 12 ? 2 : 4;
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
