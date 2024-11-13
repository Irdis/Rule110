namespace Rule110;

public class Scene
{
    private static int[] _table = Construct();

    private int _size;
    private int[] _tape;
    private int[] _tmp;
    private int _row = 0;

    private IBackground _background;
    private List<IObserver> _observers;

    public int Size => _size;

    public Scene(int size, IBackground background, List<IObserver> observers)
    {
        _size = size;
        _tape = new int[_size];
        _tmp = new int[_size];
        _background = background;
        _observers = observers ?? new List<IObserver>(0);
    }

    public void Init(List<(int, IGlider)> gliders)
    {
        var gliderIndex = -1;
        var tileIndex = -1;
        var glider = (IGlider?)null;
        if (gliders.Count > 0)
        {
            gliderIndex = 0;
            (tileIndex, glider) = gliders[0];
        }

        var ind = 0;
        while (ind < _size)
        {
            if (glider != null &&
                _background.TileIndex == tileIndex && 
                _background.Position == glider.EtherLeave) {

                for (int i = 0; i < glider.Pattern.Length; i++)
                {
                    _tape[ind++] = glider.Pattern[i];
                }
                _background.Shift(glider.EtherEnter);

                gliderIndex++;
                (tileIndex, glider) = gliderIndex < gliders.Count 
                    ? gliders[gliderIndex]
                    : (-1, null);
            } else {
                _tape[ind++] = _background.Next(); 
            }
        }
    }

    public void InitComplete()
    {
        foreach(var obs in _observers)
        {
            obs.Next(_row, _tape);
        }
    }

    public void FlipState(int pos)
    {
        _tape[pos] = 1 - _tape[pos];
    }

    public void SetState(int pos, int val)
    {
        _tape[pos] = val;
    }

    public void Next()
    {
        for (int i = 0; i < _size; i++)
        {
            var state = GetState(_tape, i);
            _tmp[i] = _table[state];
        }
        (_tape, _tmp) = (_tmp, _tape);
        _row++;
        foreach (var obs in _observers)
        {
            obs.Next(_row, _tape);
        }
    }
    
    public void Complete()
    {
        foreach (var obs in _observers)
        {
            obs.Complete();
        }
    }

    public int GetState(int[] arr, int p) 
    {
        var s1 = p > 0 ? arr[p-1] : _background.GetLeft(_row);
        var s2 = arr[p];
        var s3 = p < arr.Length - 1 ? arr[p+1] : _background.GetRight(_row);
        return (s1 << 2) + (s2 << 1) + s3;
    }

    public static int[] Construct()
    {
        var len = 1<<3;
        var rule = 110;
        var t = new int[len];
        for (int i = 0; i < len; i++)
        {
            t[i] = (rule & (1<<i)) != 0 ? 1 : 0;
        }
        return t;
    }
}

