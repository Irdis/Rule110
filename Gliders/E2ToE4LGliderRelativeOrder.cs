namespace Rule110.Gliders;

public class E2ToE4LGliderRelativeOrder
{
    private static int[] _offset = [
        0, 0, -1, -1, 0, 0, -1, -1, 0, -1, 
        -1, -1, 0, -1, -1
    ];

    public static (int, int) Next(int e2GliderNumber)
    {
        var offset = _offset[e2GliderNumber];
        var nextE4GliderNumber = e2GliderNumber;
        return (6 + offset, (nextE4GliderNumber + 9) % ENGlider.Size);
    }
}
