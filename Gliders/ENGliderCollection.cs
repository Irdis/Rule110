namespace Rule110.Gliders;

public class ENGliderCollection : IGliderCollection
{
    private IList<IGlider> _collection;
    public int N { get; init; }

    public ENGliderCollection(int n) 
    {
        var analyzer = new GliderAnalyzer();
        var en = new ENGlider(n);
        _collection = analyzer.Analyze(en);
        this.N = n;
    }

    public IGlider Get(int gliderNumber)
    {
        return _collection[gliderNumber];
    }
}
