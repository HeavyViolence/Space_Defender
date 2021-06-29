using System;

public class DiamondsAmountChangedEventArgs : EventArgs
{
    public int PreviousDiamonds { get; } = 0;
    public int CurrentDiamonds { get; } = 0;

    public DiamondsAmountChangedEventArgs(int previousDiamonds,
                                          int currentDiamonds)
    {
        PreviousDiamonds = previousDiamonds;
        CurrentDiamonds = currentDiamonds;
    }
}
