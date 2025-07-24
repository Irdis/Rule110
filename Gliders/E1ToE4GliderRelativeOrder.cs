namespace Rule110.Gliders;

public class E1ToE4GliderRelativeOrder
{
    public static (int, int) Next(int e1GliderNumber)
    {
        var nextE4GliderNumber = e1GliderNumber;
        return (4, nextE4GliderNumber);
    }
}
