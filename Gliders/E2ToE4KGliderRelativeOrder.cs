namespace Rule110.Gliders;

public class E2ToE4KGliderRelativeOrder
{
    private static int[] _offset = [
        0, 0, -1, 0, 0, 0, 0, 0, 0, -1, 
        0, 0, 0, -1, 0
    ];

    public static (int, int) Next(int e2GliderNumber)
    {
        var offset = _offset[e2GliderNumber];
        var nextE4GliderNumber = e2GliderNumber;
        return (4 + offset, (nextE4GliderNumber + 3) % ENGlider.Size);
    }
}
