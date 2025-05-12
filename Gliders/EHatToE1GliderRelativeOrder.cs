using System.Text;

namespace Rule110.Gliders;

public class EHatToE1GliderRelativeOrder
{
    private static int[] _offset = [
        0, 1, 0, 0, 1, 1, 0, 0, 1, 1, 
        0, 0, 1, 1, 0, 0, 1, 1, 0, 1,
        1, 0, 0, 1, 0, 0, 0, 1, 0, 0
    ];

    public static (int, int) Next(int ehGliderNumber)
    {
        var offset = _offset[ehGliderNumber];
        var nextE1GliderNumber = (ehGliderNumber + 5) % ENGlider.Size;
        return (1 + offset, nextE1GliderNumber);
    }
}
