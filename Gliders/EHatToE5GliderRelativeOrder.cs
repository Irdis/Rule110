namespace Rule110.Gliders;

public class EHatToE5GliderRelativeOrder
{
    private static int[] _offset = [
        1, 1, 0, 1, 1, 1, 0, 1, 1, 1, 
        0, 1, 1, 1, 0, 1, 1, 1, 1, 1,
        1, 0, 1, 1, 0, 0, 1, 1, 0, 0
    ];

    public static (int, int) Next(int ehGliderNumber)
    {
        var offset = _offset[ehGliderNumber];
        var nextE5GliderNumber = (ehGliderNumber + 6) % ENGlider.Size;
        return (1 + offset, nextE5GliderNumber);
    }
}
