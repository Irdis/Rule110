using System.Drawing;
using System.Drawing.Imaging;

namespace Rule110;

class Program
{
    private int _size;
    private int[] _tape;
    private int[] _tmp;
    private int[] _table = Construct();
    private int _row = 0;
    private Bitmap _img;
    public const int BLOCK_SIZE = 5;

    private bool _useEther;
    private const int ETHER_PERIOD_X = 14;
    private const int ETHER_PERIOD_Y = 7;
    private static int[] _etherTile = new []{
        1, 1, 1, 1, 
        1, 0, 0, 0,
        1, 0, 0, 1,
        1, 0 //, 1, 1 
    };

    private int[,] _etherBorder = ConstructEtherBorder();
    
    public int Size => _size;
    

    public Program(int size)
    {
        _size = size;
        _tape = new int[_size];
        _tmp = new int[_size];
        _img = new Bitmap((_size + 1) * BLOCK_SIZE, (_size + 1) * BLOCK_SIZE);
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

    public void FillWithEther()
    {
        _useEther = true;
        for (int i = 0; i < _size; i++)
        {
            _tape[i] = _etherTile[i % ETHER_PERIOD_X];
        }
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
        for (int i = 0; i < _size; i++)
        {
            var rect = new Rectangle(
                    i * BLOCK_SIZE, 
                    _row * BLOCK_SIZE, 
                    BLOCK_SIZE, 
                    BLOCK_SIZE);
            DrawRect(rect, _tape[i] == 1 ? Color.Black : Color.White);
        }
    }

    public void DrawRect(Rectangle rect, Color col)
    {
        for (int i = 0; i < rect.Width; i++)
            for (int j = 0; j < rect.Height; j++)
                _img.SetPixel(rect.X + i, rect.Y + j, col);
    }

    public void SaveImg(string path)
    {
        _img.Save(path);
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
            return _etherBorder[_size % ETHER_PERIOD_X, _row % ETHER_PERIOD_Y];
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

    static void Main(string[] args)
    {
        var p = new Program(500);
        var rand = new Random();
        /*for (int i = 0; i < p.Size; i++)*/
        /*    p.SetState(i, rand.Next(0, 2) == 0 ? 1 : 0);*/
        p.FillWithEther();
        p.Draw();
        for (int i = 0; i < p.Size; i++)
        {
            p.Next();
            p.Draw();
        }
        p.SaveImg("img.bmp");
    }
}
