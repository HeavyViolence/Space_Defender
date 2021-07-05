using UnityEngine;

[CreateAssetMenu(fileName = "New Durability Augmentor", menuName = "Configs/Durability Augmentor")]
public class DurabilityAugmentor : ScriptableObject
{
    public const int DurabilityUnitBaseCost = 100;
    public const int MaxDurabilityBoostStep = 100;

    public const int ReconstructionUnitBaseCost = 1000;
    public const int ReconstructionBoostStep = 10;

    public const string MaxDurabilityBoostKey = "MaxDurabilityBoost";
    public const string PerformedDurabilityUpgradesKey = "PerformedDurabilityUpgrades";

    public const string ReconstructionBoostKey = "ReconstructionBoost";
    public const string PerformedReconstructionUpgradesKey = "PerformedReconstructionUpgrades";

    public float MaxDurabilityBoost { get; private set; } = 0f;

    public int PerformedMaxDurabilityUpgrades { get; private set; } = 0;

    public int MaxDurabilityBoostCost => Mathf.RoundToInt(MaxDurabilityBoostStep * DurabilityUnitBaseCost *
                                                          Mathf.Pow(AuxMath.Phi, PerformedMaxDurabilityUpgrades));

    public float ReconstructionBoost { get; private set; } = 0f;

    public int PerformedReconstructionUpgrades { get; private set; } = 0;

    public int ReconstructionBoostCost => Mathf.RoundToInt(ReconstructionBoostStep * ReconstructionUnitBaseCost *
                                                           Mathf.Pow(AuxMath.Phi, PerformedReconstructionUpgrades));

    public void TryLoadData()
    {
        if (DataBase.Instance.TryGetFloat(MaxDurabilityBoostKey, out float d)) MaxDurabilityBoost = d;
        if (DataBase.Instance.TryGetInt(PerformedDurabilityUpgradesKey, out int u1)) PerformedMaxDurabilityUpgrades = u1;

        if (DataBase.Instance.TryGetFloat(ReconstructionBoostKey, out float r)) ReconstructionBoost = r;
        if (DataBase.Instance.TryGetInt(PerformedReconstructionUpgradesKey, out int u2)) PerformedReconstructionUpgrades = u2;
    }

    public bool BoostMaxDurability(PlayerWallet wallet)
    {
        if (wallet.PayWithCredits(MaxDurabilityBoostCost))
        {
            MaxDurabilityBoost += MaxDurabilityBoostStep;
            PerformedMaxDurabilityUpgrades++;

            DataBase.Instance.SaveFloat(MaxDurabilityBoostKey, MaxDurabilityBoost);
            DataBase.Instance.SaveInt(PerformedDurabilityUpgradesKey, PerformedMaxDurabilityUpgrades);

            return true;
        }
        else return false;
    }

    public bool BoostReconstruction(PlayerWallet wallet)
    {
        if (wallet.PayWithCredits(ReconstructionBoostCost))
        {
            ReconstructionBoost += ReconstructionBoostStep;
            PerformedReconstructionUpgrades++;

            DataBase.Instance.SaveFloat(ReconstructionBoostKey, ReconstructionBoost);
            DataBase.Instance.SaveInt(PerformedReconstructionUpgradesKey, PerformedReconstructionUpgrades);

            return true;
        }
        else return false;
    }
}
