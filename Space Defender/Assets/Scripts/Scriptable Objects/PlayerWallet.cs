using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Player Wallet", menuName = "Configs/Player Wallet")]
public class PlayerWallet : ScriptableObject
{
    public const string CreditsKey = "Credits";
    public const string DiamondsKey = "Diamonds";

    public const int DefaultCredits = 1000;
    public const int DefaultDiamonds = 10;

    private int _credits = 0;
    private int _diamonds = 0;

    public int Credits => _credits;
    public int Diamonds => _diamonds;

    public event EventHandler<CreditsAmountChangedEventArgs> CreditsAmountChanged;

    private void OnCreditsAmountChanged(CreditsAmountChangedEventArgs e)
    {
        CreditsAmountChanged?.Invoke(this, e);
    }

    public event EventHandler<DiamondsAmountChangedEventArgs> DiamondsAmountChanged;

    private void OnDiamondsAmountChanged(DiamondsAmountChangedEventArgs e)
    {
        DiamondsAmountChanged?.Invoke(this, e);
    }

    public void TryLoadData()
    {
        if (DataBase.Instance.TryGetInt(CreditsKey, out int c)) _credits = c;
        else _credits = DefaultCredits;

        if (DataBase.Instance.TryGetInt(DiamondsKey, out int d)) _diamonds = d;
        else _diamonds = DefaultDiamonds;
    }

    public bool PayWithCredits(int price)
    {
        if (_credits >= price)
        {
            int previousCredits = _credits;
            _credits -= price;

            OnCreditsAmountChanged(new CreditsAmountChangedEventArgs(previousCredits, _credits));
            DataBase.Instance.SaveInt(CreditsKey, _credits);

            return true;
        }
        else return false;
    }

    public bool PayWithDiamonds(int price)
    {
        if (_diamonds >= price)
        {
            int previousDiamonds = _diamonds;
            _diamonds -= price;

            OnDiamondsAmountChanged(new DiamondsAmountChangedEventArgs(previousDiamonds, _diamonds));
            DataBase.Instance.SaveInt(DiamondsKey, _diamonds);

            return true;
        }
        else return false;
    }
}
