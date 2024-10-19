namespace Rule110;

public interface IGlider
{
    int[] Pattern { get; }

    // Indicate position in the ether tile immediately after pattern ends
    // * * * *  <- Pattern -> ?
    // * o o o 
    // * o o * 
    // * o * * 
    int Shift { get; }
}
