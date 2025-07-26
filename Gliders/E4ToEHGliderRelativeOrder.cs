using System.Text;

namespace Rule110.Gliders;

public class E4ToEHGliderRelativeOrder
{
    private static int[] _offset = [
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
        0, 0, 0, 0, 0, 0, 0, -1, 0, 0,
        0, 0, 0, 0, 1, 0, 0, 0, 1, 0
    ];

    public static (int, int) Next(int e4GliderNumber, int type)
    {
        var period = type == 1 ? 15 : 0;
        var nextEHGliderNumber = (e4GliderNumber + 8 + period) % EHatGlider.Size;
        var offset = _offset[nextEHGliderNumber];
        return (2 + offset, nextEHGliderNumber);
    }
}
