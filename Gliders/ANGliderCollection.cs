namespace Rule110.Gliders;

public class ANGliderCollection : IGliderCollection
{
    private IList<IGlider> _collection;

    public ANGliderCollection(int n) 
    {
        var analyzer = new GliderAnalyzer();
        var an = new ANGlider(n);
        _collection = analyzer.Analyze(an);
    }

    public IGlider Get(int gliderNumber)
    {
        return _collection[gliderNumber];
    }
}


