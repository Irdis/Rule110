namespace Rule110;

public class ConsoleObserver : IObserver
{
    private int _size;
    private int _window;

    public ConsoleObserver(int size, int? window = null)
    {
        _size = size;
        _window = window ?? size;
    }
        
    public void Next(int lvl, int[] tape)
    {
        var first = _size - _window;
        for (int i = first; i < _size; i++)
        {
            Console.Write(tape[i] == 1 ? '*' : ' ');
        }
        Console.WriteLine();
    }

    public void Complete()
    {
    }
}
