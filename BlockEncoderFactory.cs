using Rule110.Gliders;

namespace Rule110;

public class BlockEncoderFactory
{
    private ANGliderCollection _a4GliderCollection;
    private EHatGliderCollection _ehGliderCollection;
    private ENGliderCollection _en1GliderCollection;
    private ENGliderCollection _en2GliderCollection;
    private ENGliderCollection _en4GliderCollection;
    private ENGliderCollection _en5GliderCollection;

    public BlockEncoderFactory()
    {
        _a4GliderCollection = new ANGliderCollection(3);
        _ehGliderCollection = new EHatGliderCollection();
        _en1GliderCollection = new ENGliderCollection(0);
        _en2GliderCollection = new ENGliderCollection(1);
        _en4GliderCollection = new ENGliderCollection(3);
        _en5GliderCollection = new ENGliderCollection(4);
    }

    public BlockEncoder Create(object arg)
    {
        var encoder = new BlockEncoder(
            _a4GliderCollection,
            _ehGliderCollection,
            _en1GliderCollection,
            _en2GliderCollection,
            _en4GliderCollection,
            _en5GliderCollection,
            arg
        );
        return encoder;
    }
}
