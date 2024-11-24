namespace Rule110;

public class EtherBackground : IBackground
{
    public const int PERIOD_X = 14;
    public const int PERIOD_Y = 7;
    public static int[] Periods { get; } = [ 0, 4, 8, 12 ];
    public static int[] Tile { get; } = [
        1, 1, 1, 1, 
        1, 0, 0, 0,
        1, 0, 0, 1,
        1, 0 //, 1, 1 
    ];
    private static int[,] _border = ConstructEtherBorder();

    private static int[,] ConstructEtherBorder()
    {
        var border = new int[PERIOD_X, PERIOD_Y];
        for (int j = 0; j < PERIOD_Y; j++)
        for (int i = 0; i < PERIOD_X; i++)
        {
            border[i, j] = Tile[(i + j*4) % PERIOD_X];
        }
        return border;
    }

    private int _pointer = 0; 

    public int TileIndex => _pointer / PERIOD_X;
    public int Position => _pointer % PERIOD_X;
    
    public int Next() 
    {
        var val = Tile[_pointer % PERIOD_X];
        _pointer++;
        return val;
    }

    public void Shift(int offset) 
    {
        _pointer -= _pointer % PERIOD_X;
        _pointer += offset;
    }

    public int GetLeft(int lvl)
    {
        return _border[PERIOD_X - 1, lvl % PERIOD_Y];
    }

    public int GetRight(int lvl)
    {
        return _border[Position, lvl % PERIOD_Y];
    }
}

