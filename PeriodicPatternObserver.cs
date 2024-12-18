namespace Rule110;

public class PeriodicPatternObserver : IObserver
{
    private readonly int[] _pattern;
    private readonly int _yPeriod;
    private int _depth;
    private int? _xPos;
    private bool _terminated;

    public int Depth => _depth;

    public PeriodicPatternObserver(int[] pattern, int yPeriod)
    {
        _pattern = pattern;
        _yPeriod = yPeriod;
    }

    public void Next(int lvl, int[] tape)
    {
        if (_terminated)
            return;

        if (lvl % _yPeriod != 0)
            return;

        for (int i = 0; i < tape.Length - _pattern.Length + 1; i++)
        {
            if (!SeqEqual(tape, i, _pattern))
                continue;

            if (!_xPos.HasValue)
            {
                _xPos = i;
                _depth++;
                return;
            }

            if (_xPos != i)
            {
                _terminated = true;
                return;
            }
            _depth++;
            return;
        }
        _terminated = true;
    }

    private static bool SeqEqual(int[] tape, int pos, int[] pattern)
    {
        for (int i = 0; i < pattern.Length; i++)
        {
            if (tape[i + pos] != pattern[i])
                return false;
        }
        return true;
    }

    public void Complete()
    {
    }
}
