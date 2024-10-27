namespace Rule110;

public class Scene
{
    private int[] _table = Construct();

    private int _size;
    private int[] _tape;
    private int[] _tmp;
    private int _row = 0;

    private ImgBmp _img;
    public const int BLOCK_SIZE = 1;

    private bool _useEther;
    private int _etherPointer = 0;
    private const int ETHER_PERIOD_X = 14;
    private const int ETHER_PERIOD_Y = 7;
    private static int[] _etherTile = new []{
        1, 1, 1, 1, 
        1, 0, 0, 0,
        1, 0, 0, 1,
        1, 0 //, 1, 1 
    };
    private int[,] _etherBorder = ConstructEtherBorder();

    private static readonly string FILE_PATH = "img.bmp";

    public int Size => _size;

    public Scene(int size, string? filePath = null)
    {
        _size = size;
        _tape = new int[_size];
        _tmp = new int[_size];
        _img = new ImgBmp(filePath ?? FILE_PATH, _size, _size, BLOCK_SIZE);
    }

    private static int[,] ConstructEtherBorder()
    {
        var border = new int[ETHER_PERIOD_X, ETHER_PERIOD_Y];
        for (int j = 0; j < ETHER_PERIOD_Y; j++)
        for (int i = 0; i < ETHER_PERIOD_X; i++)
        {
            border[i, j] = _etherTile[(i + j*4) % ETHER_PERIOD_X];
        }
        return border;
    }

    public void FillWithEther(List<(int, IGlider)> gliders)
    {
        _useEther = true;
        var etherCounter = 0;
        var ind = 0;

        var gliderIndex = -1;
        var curGliderPos = -1;
        var curGlider = (IGlider?)null;
        if (gliders.Count > 0)
        {
            gliderIndex = 0;
            (curGliderPos, curGlider) = gliders[0];
        }

        while (ind < _size)
        {
            if (etherCounter == curGliderPos && _etherPointer == 4) {
                if (curGlider == null)
                    throw new InvalidOperationException("Glider cannot be null");

                for (int i = 0; i < curGlider.Pattern.Length; i++)
                {
                    _tape[ind] = curGlider.Pattern[i];
                    ind++;
                }
                _etherPointer = curGlider.Shift;
                gliderIndex++;
                (curGliderPos, curGlider) = gliderIndex < gliders.Count 
                    ? gliders[gliderIndex]
                    : (-1, null);
            } else {
                _tape[ind] = _etherTile[_etherPointer];
                ind++;
                _etherPointer++;
                _etherPointer %= ETHER_PERIOD_X;
                if (_etherPointer == 0)
                {
                    etherCounter++;
                }
            }
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
    }
    
    public void Draw()
    {
        _img.WriteRow(_tape);
    }

    public void SaveImg()
    {
        _img.Save();
    }

    public void Print(int window)
    {
        for (int i = _size - 1 - window; i < _size; i++)
        {
            Console.Write(_tape[i] == 1 ? '*' : ' ');
        }
        Console.WriteLine();
    }

    public int GetState(int[] arr, int p) 
    {
        var s1 = p > 0 ? arr[p-1] : GetDefaultLeft();
        var s2 = arr[p];
        var s3 = p < arr.Length - 1 ? arr[p+1] : GetDefaultRight();
        return (s1 << 2) + (s2 << 1) + s3;
    }

    private int GetDefaultLeft()
    {
        if (_useEther)
            return _etherBorder[ETHER_PERIOD_X - 1, _row % ETHER_PERIOD_Y];
        return 0;
    }

    private int GetDefaultRight()
    {
        if (_useEther)
            return _etherBorder[_etherPointer % ETHER_PERIOD_X, _row % ETHER_PERIOD_Y];
        return 0;
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

