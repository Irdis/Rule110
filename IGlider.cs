namespace Rule110;

public interface IGlider
{
    int[] Pattern { get; }

    // Indicate position in the ether tile immediately after pattern ends
    // * * * *  <- Pattern -> ?
    // * . . . 
    // * . . * 
    // * . * * 
    int Shift { get; }
}
