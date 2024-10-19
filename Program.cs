using System.Drawing;
using System.Drawing.Imaging;

namespace Rule110;
public interface IGlider
{
    int[] Pattern { get; }
    int Shift { get; }
}
public class ANGlider : IGlider
{
    private static int[] _shifts = new []{ 8, 4, 12 };
    private static int[] _pattern = new [] { 1, 0, 1, 1 };
    public int Shift { get; set; }
    public int[] Pattern { get; set; }
    public ANGlider(int n)
    {
        if (n == 1)
        {
            this.Pattern = new[] { 1, 0 };
            return;
        }
        var m = n - 2;
        // 2 4 4 6 8 8 10 12 12
        var len = 2 * (2 * (m / 3) + (m % 3 == 0 ? 0 : 1) + 1);
        Console.WriteLine(n + " -> " + len);
        this.Shift = _shifts[m % 3];

        this.Pattern = new int[len];
        for (int i = 0; i < len; i++)
        {
            this.Pattern[i] =  _pattern[i % 4];
        }
    }
}
public class Scene
{
    private int[] _table = Construct();

    private int _size;
    private int[] _tape;
    private int[] _tmp;
    private int _row = 0;
    private int _totalShift = 0;
    private int _etherPointer = 0;

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

    private string _filePath = "img.bmp";

    public int Size => _size;
    

    public Scene(int size, string filePath = null)
    {
        _size = size;
        _tape = new int[_size];
        _tmp = new int[_size];
        _img = new Bitmap((_size + 1) * BLOCK_SIZE, (_size + 1) * BLOCK_SIZE);

        if (filePath != null)
            _filePath = filePath;
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

    public void FillWithEther(int pos, IGlider glider)
    {
        _useEther = true;
        var ind = 0;
        while (ind < _size)
        {
            var offset = pos * ETHER_PERIOD_X + 4;
            if (offset == ind) {
                for (int i = 0; i < glider.Pattern.Length; i++)
                {
                    _tape[ind] = glider.Pattern[i];
                    ind++;
                }
                _etherPointer = glider.Shift;
            } else {
                _tape[ind] = _etherTile[_etherPointer];
                ind++;
                _etherPointer++;
                _etherPointer %= ETHER_PERIOD_X;
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

    public void SaveImg()
    {
        _img.Save(_filePath);
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

class Program
{
    static void Main(string[] args)
    {
        var rand = new Random();
        for (int i = 1; i <= 15; i++)
        {
            var scene = new Scene(500, $"img{i}.bmp");
            scene.FillWithEther(1, new ANGlider(i));
            scene.Draw();
            for (int j = 0; j < scene.Size; j++)
            {
                scene.Next();
                scene.Draw();
            }
            scene.SaveImg();
        }
    }
}
