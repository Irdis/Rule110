namespace Rule110.Gliders;

public class GenericGlider : IGlider
{
    public required int[] Pattern { get; set; }

    public required int EtherLeave { get; set; }

    public required int EtherEnter { get; set; }
}
