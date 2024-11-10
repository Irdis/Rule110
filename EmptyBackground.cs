namespace Rule110;

public class EmptyBackground : IBackground
{
    public int TileIndex { get; set; }
    public int Position => 0;

    public int Next() 
    {
        TileIndex++;
        return 0;
    }
    public void Shift(int offset) {}

    public int GetLeft(int lvl) => 0;
    public int GetRight(int lvl) => 0;
}
