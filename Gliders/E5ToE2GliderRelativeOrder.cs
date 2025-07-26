namespace Rule110.Gliders;

public class E5ToE2GliderRelativeOrder
{
    private static int[] _offset = [
        0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 
        0, 0, 1, 0, 0
    ];

    public static (int, int) Next(int e5GliderNumber)
    {
        var offset = _offset[e5GliderNumber];
        var nextE2GliderNumber = e5GliderNumber;
        return (1 + offset, (nextE2GliderNumber + 12) % ENGlider.Size);
    }
}
