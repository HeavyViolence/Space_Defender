using System;

public class CreditsAmountChangedEventArgs : EventArgs
{
    public int PreviousCredits { get; } = 0;
    public int CurrentCredits { get; } = 0;

    public CreditsAmountChangedEventArgs(int previousCredits,
                                         int currentCredits)
    {
        PreviousCredits = previousCredits;
        CurrentCredits = currentCredits;
    }
}
