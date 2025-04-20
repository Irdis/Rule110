using System.Text;

namespace Rule110.Gliders;

public class EHatGliderRelativeOrder
{
    private static int[] _typeStart = [11, 15, 19, 22, 25, 29, 3, 7];
    private static int[] _startOffset = [
        0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 
        -1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 
        0, 0, 0, 0, 0, 0, 0, 1, 0, 0
    ];
    private static int[] _initialNumberOfTriangles = [1, 2, 3, 3, 1, 1, 2, 1];
    private const int TYPE_COUNT = 8;

    public static (int, int) Next(int gliderNumber, int ethCount, int type)
    {
        ethCount--;

        var offset = 8 * (ethCount / EHatGlider.Size);

        var startGliderNumber = _typeStart[type];
        var startOffset = _startOffset[gliderNumber];
        startGliderNumber = (startGliderNumber + gliderNumber) % EHatGlider.Size;

        var ethRem = ethCount % EHatGlider.Size;
        offset += EHatGliderRelOrderTable.Table[gliderNumber][type][ethRem];

        var nextGliderNumber = (startGliderNumber + ethCount) % EHatGlider.Size;
        return (offset + startOffset, nextGliderNumber);
    }

    public static void AnalyzeRelOrder(int startingGlider)
    {
        var ehGliderCollection = new EHatGliderCollection();
        var offsets = new int[EHatGlider.Size];

        int[][] offsetTable = new int[TYPE_COUNT][];
        for (int k = 0; k < 8; k++)
        {
            var offsetRow = new int[EHatGlider.Size];

            for (int i = 0; i < EHatGlider.Size; i++)
            {
                const int width = 500;
                const int height = 2;

                var background = new EtherBackground();

                var (ehOffset, ehNumber) = Next(startingGlider, i + 1, k);
                var nextStartingGlider = (startingGlider + 1) % EHatGlider.Size;
                var nextFollowupGlider = (ehNumber + 1) % EHatGlider.Size;
                var relOrderObserver = new EHatRelativeOrderObserver(
                    ehGliderCollection,
                    nextStartingGlider,
                    nextFollowupGlider
                );

                var scene = new Scene(width, background, [relOrderObserver]);
                var gliders = new List<(int, IGlider)>();
                var offset = 1;
                var alignment = 0;

                var alignDelta = EHatGlider.RightAlignment(startingGlider);
                gliders.Add((offset, ehGliderCollection.Get(startingGlider)));
                offset += alignDelta;
                alignment += alignDelta;

                offset += ehOffset;

                alignDelta = EHatGlider.RightAlignment(ehNumber);
                gliders.Add((offset, ehGliderCollection.Get(ehNumber)));
                offset += alignDelta;
                alignment += alignDelta;

                scene.Init(gliders);
                scene.InitComplete();
                for (int j = 1; j < height; j++)
                {
                    scene.Next();
                }
                scene.Complete();
                offsetRow[i] = relOrderObserver.Offset;
            }
            offsetTable[k] = offsetRow;
        }
        var sb = new StringBuilder();
        sb.Append("[");
        for (int i = 0; i < TYPE_COUNT; i++)
        {
            sb.Append("[");
            for (int j = 0; j < EHatGlider.Size; j++)
            {
                sb.Append(offsetTable[i][j]);
                sb.Append(",");
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append("],");
        }
        sb.Remove(sb.Length - 1, 1);
        sb.Append("],");
        Console.WriteLine(sb.ToString());
    }
}
