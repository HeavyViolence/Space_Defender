using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Player Wallet", menuName = "Configs/Player Wallet")]
public class PlayerWallet : ScriptableObject
{
    public const string CreditsKey = "Credits";
    public const string DiamondsKey = "Diamonds";

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

    private void OnEnable()
    {
        DataBase.Instance.Loaded += LoadedEventhandler;
    }

    private void OnDisable()
    {
        DataBase.Instance.Loaded -= LoadedEventhandler;
    }

    private void LoadedEventhandler(object sender, System.EventArgs e)
    {
        LoadPlayerWalletData();
    }

    private void LoadPlayerWalletData()
    {
        DataBase.Instance.TryGetInt(CreditsKey, out _credits);
        DataBase.Instance.TryGetInt(DiamondsKey, out _diamonds);
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
