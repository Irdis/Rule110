namespace Rule110.Gliders;

public class EHatGliderCollection : IGliderCollection
{
    private IList<IGlider> _collection;

    public EHatGliderCollection() 
    {
        var analyzer = new GliderAnalyzer();
        var eh = new EHatGlider();
        _collection = analyzer.Analyze(eh);
    }

    public IGlider Get(int gliderNumber)
    {
        return _collection[gliderNumber];
    }
}
