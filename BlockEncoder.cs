using Rule110.Gliders;

namespace Rule110;

public class BlockEncoder
{
    private ANGliderCollection _a4;
    private EHatGliderCollection _eh;
    private ENGliderCollection _en1;
    private ENGliderCollection _en2;
    private ENGliderCollection _en4;
    private ENGliderCollection _en5;

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
        ENGliderCollection en1,
        ENGliderCollection en2,
        ENGliderCollection en4,
        ENGliderCollection en5,
        params object[] args
    )
    {
        _a4 = a4;
        _eh = eh;
        _en1 = en1;
        _en2 = en2;
        _en4 = en4;
        _en5 = en5;
        _args = args;
    }

    public void Encode(List<BlockItem> blocks, List<(int, IGlider)> gliders)
    {
        _a4GliderNumber = GetA4StartingNumber(blocks);

        foreach (var block in blocks)
        {
            Action<List<(int, IGlider)>> fn = block.Type switch
            {
                BlockType.A => EncodeA,
                BlockType.B => EncodeB,
                BlockType.C => EncodeC,
                BlockType.D => EncodeD,
                BlockType.E => EncodeE,
                BlockType.F => EncodeF,
                BlockType.G => EncodeG,
                BlockType.H => EncodeH,
                BlockType.I => EncodeI,
                BlockType.J => EncodeJ,
                BlockType.K => EncodeK,
                BlockType.L => EncodeL,
            };
            for (int i = 0; i < block.Count; i++)
            {
                fn(gliders);
            }
        }
    }

    private int GetA4StartingNumber(List<BlockItem> blocks)
    {
        var bs = blocks
            .Where(b => b.Type == BlockType.B)
            .Sum(b => b.Count);
        return _a4ToERelation[bs % 3];
    }

    private void EncodeA(List<(int, IGlider)> gliders)
    {
        if (!_hasA4)
        {
            return;
        }
        _a4GliderDist += -3 * 2;
    }

    private void EncodeB(List<(int, IGlider)> gliders)
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

    private void EncodeC(List<(int, IGlider)> gliders)
    {
        _a4GliderDist += -16;

        var (a4Offset, a4Number) = ANGlider.NextA(_a4GliderNumber, _a4GliderDist);
        _offset += a4Offset;

        var align = EHatGlider.RightAlignment(_ehGliderNumber);
        gliders.Add((_offset, _eh.Get(_ehGliderNumber)));
        _offset += align;
        _alignment += align;
    }

    private void EncodeD(List<(int, IGlider)> gliders)
    {
        var (ehOffset, ehNumber) = EHatGliderRelativeOrder.Next(_ehGliderNumber, 3, 0);
        _offset += ehOffset;

        var align = EHatGlider.RightAlignment(_ehGliderNumber);
        gliders.Add((_offset, _eh.Get(ehNumber)));
        _offset += align;
        _alignment += align;
        _ehGliderNumber = ehNumber;
    }

    private void EncodeE(List<(int, IGlider)> gliders)
    {
        for (int i = 0; i < 3; i++)
        {
            var (ehOffset, ehNumber) = EHatGliderRelativeOrder.Next(_ehGliderNumber, 27, 0);
            _offset += ehOffset;

            var align = EHatGlider.RightAlignment(_ehGliderNumber);
            gliders.Add((_offset, _eh.Get(ehNumber)));
            _offset += align;
            _alignment += align;
            _ehGliderNumber = ehNumber;
        }
    }

    private void EncodeF(List<(int, IGlider)> gliders)
    {
        int[] gaps = [27, 21, 27];
        for (int i = 0; i < 3; i++)
        {
            var (ehOffset, ehNumber) = EHatGliderRelativeOrder.Next(_ehGliderNumber, gaps[i], 0);
            _offset += ehOffset;

            var align = EHatGlider.RightAlignment(_ehGliderNumber);
            gliders.Add((_offset, _eh.Get(ehNumber)));
            _offset += align;
            _alignment += align;
            _ehGliderNumber = ehNumber;
        }
    }

    private void EncodeG(List<(int, IGlider)> gliders)
    {
        var (ehOffset, ehNumber) = EHatGlider.Next(_ehGliderNumber, -10);
        _offset += ehOffset;
        _offset += 2;

        var align = EHatGlider.RightAlignment(_ehGliderNumber);
        gliders.Add((_offset, _eh.Get(ehNumber)));
        _offset += align;
        _alignment += align;
        _ehGliderNumber = ehNumber;

        var e1Number = 4;

        _offset += 1;
        align = ENGlider.RightAlignment(e1Number, _en1.N);
        gliders.Add((_offset, _en1.Get(e1Number)));
        _offset += align;
        _alignment += align;

        var e4Number = 4;

        _offset += 4;
        align = ENGlider.RightAlignment(e4Number, _en4.N);
        gliders.Add((_offset, _en4.Get(e4Number)));
        _offset += align;
        _alignment += align;

        AddEs(gliders, [
            (27, 2),
            (1, 2),
            (5, 3),
            (2, 2),
            (28, 3)
        ]);
    }

    private void EncodeH(List<(int, IGlider)> gliders)
    {
        AddEs(gliders, [
            (6, 1),
            (12, 1),
            (13, 2),
            (21, 2),
            (24, 3),
            (20, 2),
        ]);
    }

    private void EncodeI(List<(int, IGlider)> gliders)
    {
        AddEs(gliders, [
            (6, 1),
            (18, 3),
            (29, 1),
            (7, 1),
            (10, 3),
            (6, 3),
        ]);
    }

    private void EncodeJ(List<(int, IGlider)> gliders)
    {
        AddEs(gliders, [
            (0, 2),
            (12, 3),
            (23, 1),
            (1, 1),
            (4, 3),
            (0, 3),
        ]);
    }

    private void EncodeK(List<(int, IGlider)> gliders)
    {
        var e5Number = 4;

        _offset += 1;
        var align = ENGlider.RightAlignment(e5Number, _en5.N);
        gliders.Add((_offset, _en5.Get(e5Number)));
        _offset += align;
        _alignment += align;

        var e2Number = 1;

        _offset += 1;
        align = ENGlider.RightAlignment(e2Number, _en2.N);
        gliders.Add((_offset, _en2.Get(e2Number)));
        _offset += align;
        _alignment += align;

        var e4Number = 4;

        _offset += 4;
        align = ENGlider.RightAlignment(e4Number, _en4.N);
        gliders.Add((_offset, _en4.Get(e4Number)));
        _offset += align;
        _alignment += align;


        AddEs(gliders, [
            (27, 2),
            (1, 2),
            (5, 3),
            (2, 2),
            (28, 3)
        ]);
    }

    private void EncodeL(List<(int, IGlider)> gliders)
    {
        var e5Number = 4;

        _offset += 1;
        var align = ENGlider.RightAlignment(e5Number, _en5.N);
        gliders.Add((_offset, _en5.Get(e5Number)));
        _offset += align;
        _alignment += align;

        var e2Number = 1;

        _offset += 1;
        align = ENGlider.RightAlignment(e2Number, _en2.N);
        gliders.Add((_offset, _en2.Get(e2Number)));
        _offset += align;
        _alignment += align;

        var e4Number = 10;

        _offset += 6;
        align = ENGlider.RightAlignment(e4Number, _en4.N);
        gliders.Add((_offset, _en4.Get(e4Number)));
        _offset += align;
        _alignment += align;


        AddEs(gliders, [ 
            (21, 1),
            (28, 1),
            (12, 1),
        ]);
    }

    private void AddEs(List<(int, IGlider)> gliders, (int, int)[] es)
    {
        foreach (var (ehNumber, offset) in es)
        {
            _offset += offset;
            var align = EHatGlider.RightAlignment(ehNumber);
            gliders.Add((_offset, _eh.Get(ehNumber)));
            _offset += align;
            _alignment += align;
            _ehGliderNumber = ehNumber;
        }
    }

    public static List<BlockItem> Parse(string str)
    {
        var lst = new List<BlockItem>(str.Length);

        var expectCharOrDigit = true;
        var expectChar = false;

        int ind = 0;
        while (ind < str.Length)
        {
            var ch = str[ind];
            if (ch == ' ')
            {
                ind++;
                continue;
            }

            if (expectCharOrDigit)
            {
                expectCharOrDigit = false;
                expectChar = 'A' <= ch && ch <= 'Z';
            } 
            else if (expectChar)
            {
                lst.Add(new BlockItem()
                {
                    Type = (BlockType)ch - 'A',
                    Count = 1
                });
                ind++;
                expectCharOrDigit = true;
            } 
            else 
            {
                var count = 0;
                while ('0' <= ch && ch <= '9')
                {
                    count = count * 10 + ch - '0';
                    ch = str[++ind];
                }
                lst.Add(new BlockItem()
                {
                    Type = (BlockType)ch - 'A',
                    Count = count
                });
                
                ind++;
                expectCharOrDigit = true;
            }
        }
        return lst;
    }
}

