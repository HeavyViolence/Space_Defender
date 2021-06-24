using UnityEngine;

[CreateAssetMenu(fileName = "New Durability Config", menuName = "Configs/Durability Config")]
public class DurabilityConfig : ScriptableObject
{
    public const float DurabilityFloor = 100f;
    public const float DurabilityCeiling = 100000f;

    public const float MaxReconstructionRate = 100f;

    public const float MinExplosionDuration = 1f;
    public const float MaxExplosionDuration = 5f;

    [SerializeField] private float _maxDurability = DurabilityFloor;
    [SerializeField] private float _maxDurabilityRandom = 0f;

    [SerializeField] private bool _reconstructionEnabled = false;

    [SerializeField] private float _reconstructionRate = 0f;
    [SerializeField] private float _reconstructionRateRandom = 0f;

    [SerializeField] bool _reconstructionBounded = false;

    [SerializeField] private float _reconstructionLowerBound = 0f;
    [SerializeField] private float _reconstructionUpperBound = 0.5f;

    [SerializeField] private GameObject _explosionEffect = null;
    [SerializeField] private float _explosionDuration = MinExplosionDuration;

    [SerializeField] private AudioCollection _explosionAudio = null;

    public float MaxDurability { get; private set; } = DurabilityFloor;

    public float LowestMaxDurability => AuxMath.MinRandom(_maxDurability, _maxDurabilityRandom);

    public float HighestMaxDurability => AuxMath.MaxRandom(_maxDurability, _maxDurabilityRandom);

    public bool ReconstructionEnabled => _reconstructionEnabled;

    public float ReconstructionRate { get; private set; } = 0f;

    public float LowestReconstructionRate => AuxMath.MinRandom(_reconstructionRate, _reconstructionRateRandom);

    public float HighestReconstructionRate => AuxMath.MaxRandom(_reconstructionRate, _reconstructionRateRandom);

    public bool ReconstructionBounded => _reconstructionBounded;

    public float ReconstructionLowerBound => MaxDurability * _reconstructionLowerBound;

    public float ReconstructionUpperBound => MaxDurability * _reconstructionUpperBound;

    public GameObject ExplosionEffect => _explosionEffect;

    public float ExplosionDuration => _explosionDuration;

    public AudioCollection ExplosionAudio => _explosionAudio;

    private void Awake()
    {
        MaxDurability = AuxMath.Randomize(_maxDurability, _maxDurabilityRandom);
        ReconstructionRate = AuxMath.Randomize(_reconstructionRate, _reconstructionRateRandom);
    }
}
