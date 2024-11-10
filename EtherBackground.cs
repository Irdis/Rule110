namespace Rule110;

public class EtherBackground : IBackground
{
    private const int ETHER_PERIOD_X = 14;
    private const int ETHER_PERIOD_Y = 7;
    private static int[] _etherTile = [
        1, 1, 1, 1, 
        1, 0, 0, 0,
        1, 0, 0, 1,
        1, 0 //, 1, 1 
    ];
    private static int[,] _border = ConstructEtherBorder();

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

    private int _pointer = 0; 

    public int TileIndex => _pointer / ETHER_PERIOD_X;
    public int Position => _pointer % ETHER_PERIOD_X;
    
    public int Next() 
    {
        var val = _etherTile[_pointer % ETHER_PERIOD_X];
        _pointer++;
        return val;
    }

    public void Shift(int offset) 
    {
        _pointer -= _pointer % ETHER_PERIOD_X;
        _pointer += offset;
    }

    public int GetLeft(int lvl)
    {
        return _border[ETHER_PERIOD_X - 1, lvl % ETHER_PERIOD_Y];
    }

    public int GetRight(int lvl)
    {
        return _border[Position, lvl % ETHER_PERIOD_Y];
    }
}

