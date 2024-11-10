namespace Rule110;

public interface IObserver
{
    void Next(int lvl, int[] tape);
    void Complete();
}
