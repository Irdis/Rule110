using Rule110.Gliders;

namespace Rule110;

public class BlockEncoder
{
    private ANGliderCollection _a4;
    private EHatGliderCollection _eh;

    private int _a4GliderNumber = 2;
    private int _a4GliderDist;
    private bool _hasA4 = false;
    private int[] _a4ToERelation = [1, 2, 0];

    private int _ehGliderNumber = 11;

    private int _alignment = 0;
    private int _offset = 5;

    private object[] _args;

    public BlockEncoder(
        ANGliderCollection a4,
        EHatGliderCollection eh,
        params object[] args
    )
    {
        _a4 = a4;
        _eh = eh;
        _args = args;
    }

    public void Encode(List<BlockType> blocks, List<(int, IGlider)> gliders)
    {
        _a4GliderNumber = GetA4StartingNumber(blocks);

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
                case BlockType.C:
                    EncodeC(gliders);
                    break;
                case BlockType.D:
                    EncodeD(gliders);
                    break;
                case BlockType.E:
                    EncodeE(gliders);
                    break;
                case BlockType.F:
                    EncodeF(gliders);
                    break;
            }
        }
    }

    public int GetA4StartingNumber(List<BlockType> blocks)
    {
        var bs = blocks.Count(b => b == BlockType.B);
        return _a4ToERelation[bs % 3];
    }

    public void EncodeA(List<(int, IGlider)> gliders)
    {
        if (!_hasA4)
        {
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
            _offset += _alignment;
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

    public void EncodeC(List<(int, IGlider)> gliders)
    {
        _a4GliderDist += -16;

        var (a4Offset, a4Number) = ANGlider.NextA(_a4GliderNumber, _a4GliderDist);
        _offset += a4Offset;

        var align = EHatGlider.RightAlignment(_ehGliderNumber);
        gliders.Add((_offset, _eh.Get(_ehGliderNumber)));
        _offset += align;
        _alignment += align;
    }

    public void EncodeD(List<(int, IGlider)> gliders)
    {
        var (ehOffset, ehNumber) = EHatGlider.Next(_ehGliderNumber, -12);
        _offset += ehOffset;

        var align = EHatGlider.RightAlignment(_ehGliderNumber);
        gliders.Add((_offset, _eh.Get(ehNumber)));
        _offset += align;
        _alignment += align;
        _ehGliderNumber = ehNumber;
    }

    public void EncodeE(List<(int, IGlider)> gliders)
    {
        for (int i = 0; i < 3; i++)
        {
            var (ehOffset, ehNumber) = EHatGlider.Next(_ehGliderNumber, -60);
            _offset += ehOffset;

            var align = EHatGlider.RightAlignment(_ehGliderNumber);
            gliders.Add((_offset, _eh.Get(ehNumber)));
            _offset += align;
            _alignment += align;
            _ehGliderNumber = ehNumber;
        }
    }

    public void EncodeF(List<(int, IGlider)> gliders)
    {
        int[] gaps = [-60, -52, -60];
        for (int i = 0; i < 3; i++)
        {
            var (ehOffset, ehNumber) = EHatGlider.Next(_ehGliderNumber, gaps[i]);
            _offset += ehOffset;

            var align = EHatGlider.RightAlignment(_ehGliderNumber);
            gliders.Add((_offset, _eh.Get(ehNumber)));
            _offset += align;
            _alignment += align;
            _ehGliderNumber = ehNumber;
        }
    }

    public static List<BlockType> Parse(string str)
    {
        var lst = new List<BlockType>(str.Length);
        foreach(var ch in str)
        {
            lst.Add(ch switch {
                'A' => BlockType.A,
                'B' => BlockType.B,
                'C' => BlockType.C,
                'D' => BlockType.D,
                'E' => BlockType.E,
                'F' => BlockType.F,
                _ => throw new NotImplementedException("Unknown block " + ch)
            });
        }
        return lst;
    }
}

