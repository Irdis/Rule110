namespace Rule110.Gliders;

public class C2GliderCollection : IGliderCollection
{
    private IList<IGlider> _collection;

    public C2GliderCollection() 
    {
        var analyzer = new GliderAnalyzer();
        var an = new C2Glider();
        _collection = analyzer.Analyze(an);
    }

    public IGlider Get(int gliderNumber)
    {
        return _collection[gliderNumber];
    }
}
