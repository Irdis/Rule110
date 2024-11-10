namespace Rule110;

public interface IBackground
{
    int TileIndex { get; }
    int Position { get; }

    int Next();
    void Shift(int offset);

    int GetLeft(int lvl);
    int GetRight(int lvl);
}
