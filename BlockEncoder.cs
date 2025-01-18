using Rule110.Gliders;

namespace Rule110;

public class BlockEncoder
{
    private ANGliderCollection _a4;

    private int _a4GliderNumber = 0;
    private int _a4GliderDist;
    private bool _hasA4 = false;

    private int _alignment = 0;
    private int _offset = 5;

    public BlockEncoder(
        ANGliderCollection a4
    )
    {
        _a4 = a4;
    }

    public void Encode(List<BlockType> blocks, List<(int, IGlider)> gliders)
    {
        foreach(var block in blocks)
        {
            switch(block)
            {
                case BlockType.A: 
                    EncodeA(gliders);
                    break;
                case BlockType.B:
                    EncodeB(gliders);
                    break;
            }
        }
        gliders.Add((50 + _alignment, new C2Glider()));
    }

    public void EncodeA(List<(int, IGlider)> gliders)
    {
        if (!_hasA4)
        {
            _offset += 2;
            return;
        }
        _a4GliderDist += -3 * 2;
    }

    public void EncodeB(List<(int, IGlider)> gliders)
    {
        if (!_hasA4)
        {
            gliders.Add((_offset, _a4.Get(_a4GliderNumber)));
            _alignment = ANGlider.RightAlignment(_a4GliderNumber);
            _a4GliderDist = -5;
            _hasA4 = true;
            return;
        }

        _a4GliderDist += -3;

        var (a4Offset, a4Number) = ANGlider.NextA(_a4GliderNumber, _a4GliderDist);
        var align = ANGlider.RightAlignment(a4Number);

        _offset += a4Offset;
        gliders.Add((_offset, _a4.Get(a4Number)));
        _offset += align;
        _alignment += align;

        _a4GliderDist = -5;
        _a4GliderNumber = a4Number;
    }

    public static List<BlockType> Parse(string str)
    {
        var lst = new List<BlockType>(str.Length);
        foreach(var ch in str)
        {
            lst.Add(ch switch {
                'A' => BlockType.A,
                'B' => BlockType.B,
                _ => throw new NotImplementedException("Unknown block " + ch)
            });
        }
        return lst;
    }
}

