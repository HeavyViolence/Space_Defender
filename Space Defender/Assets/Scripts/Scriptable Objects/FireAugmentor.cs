using UnityEngine;

[CreateAssetMenu(fileName = "New Fire Augmentor", menuName = "Configs/Fire Augmentor")]
public class FireAugmentor : ScriptableObject
{
    public const int DamageUnitBaseCost = 1000;
    public const int DamageBoostStep = 10;

    public const string DamageBoostKey = "DamageBoost";
    public const string PerformedDamageUpgradesKey = "PerformedDamageUpgrades";

    public float DamageBoost { get; private set; } = 0f;

    public int PerformedDamageUpgrades { get; private set; } = 0;

    public int DamageBoostCost => Mathf.RoundToInt(DamageBoostStep * DamageUnitBaseCost *
                                                   Mathf.Pow(AuxMath.Phi, PerformedDamageUpgrades));

    public void TryLoadData()
    {
        if (DataBase.Instance.TryGetFloat(DamageBoostKey, out float d)) DamageBoost = d;
        if (DataBase.Instance.TryGetInt(PerformedDamageUpgradesKey, out int u)) PerformedDamageUpgrades = u;
    }

    public bool BoostDamage(PlayerWallet wallet)
    {
        if (wallet.PayWithCredits(DamageBoostCost))
        {
            DamageBoost += DamageBoostStep;
            PerformedDamageUpgrades++;

            DataBase.Instance.SaveFloat(DamageBoostKey, DamageBoost);
            DataBase.Instance.SaveInt(PerformedDamageUpgradesKey, PerformedDamageUpgrades);

            return true;
        }
        else return false;
    }
}
