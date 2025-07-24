using System.Text;

namespace Rule110.Gliders;

public class E4ToEHatGliderRelativeOrder
{
    private static int[] _offset = [
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0
    ];

    public static (int, int) Next(int e4GliderNumber, int type)
    {
        var offset = _offset[e4GliderNumber];
        var nextEHGliderNumber = e4GliderNumber % ENGlider.Size;
        return (2 + offset, nextEHGliderNumber);
    }
}
