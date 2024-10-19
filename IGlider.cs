namespace Rule110;

public interface IGlider
{
    int[] Pattern { get; }

    // From what position start an ether tile after inserting pattern
    // * * * *  <- Pattern -> ?
    // * o o o 
    // * o o * 
    // * o * * 
    int Shift { get; }
}
