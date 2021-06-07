using UnityEngine;

public enum FireType
{
    SingleConsecutive, SingleRandom, AllAtOnce
}

[CreateAssetMenu(fileName = "New Fire Config", menuName = "Configs/Fire Config")]
public class FireConfig : ScriptableObject
{
    public const float MaxDamage = 1000f;
    public const float MaxDispersion = 10f;
    public const float MaxFireDuration = 10f;
    public const float MaxCooldown = 10f;
    public const float MaxFireRate = 10f;
    public const int MaxAmmo = 999;

    public const float MinHitEffectDuration = 1f;
    public const float MaxHitEffectDuration = 5f;

    [SerializeField] private FireType _fireType = FireType.SingleConsecutive;

    [SerializeField] private float _damage = 0f;
    [SerializeField] private float _damageRandom = 0f;

    [SerializeField] private float _dispersion = 0f;
    [SerializeField] private float _dispersionRandom = 0f;

    [SerializeField] private float _fireDuration = 0f;
    [SerializeField] private float _fireDurationRandom = 0f;

    [SerializeField] private float _cooldown = 0f;
    [SerializeField] private float _cooldownRandom = 0f;

    [SerializeField] private float _fireRate = 0f;
    [SerializeField] private float _fireRateRandom = 0f;

    [SerializeField] private bool _infiniteAmmo = false;
    [SerializeField] private int _initialAmmo = 0;

    [SerializeField] private GameObject _projectile = null;

    [SerializeField] private GameObject _hitEffect = null;
    [SerializeField] private float _hitEffectDuration = MinHitEffectDuration;

    [SerializeField] private AudioCollection _shotAudio = null;

    public FireType FireType => _fireType;

    public float Damage => AuxMath.Randomize(_damage, _damageRandom);

    public float HighestDamage => AuxMath.HighestRandom(_damage, _damageRandom);

    public float LowestDamage => AuxMath.LowestRandom(_damage, _damageRandom);

    public float Dispersion => AuxMath.Randomize(_dispersion, _dispersionRandom) * AuxMath.RandomSign;

    public float HighestDispersion => AuxMath.HighestRandom(_dispersion, _dispersionRandom);

    public float LowestDispersion => AuxMath.LowestRandom(_dispersion, _dispersionRandom);

    public float FireDuration => AuxMath.Randomize(_fireDuration, _fireDurationRandom);

    public float LongestFireDuration => AuxMath.HighestRandom(_fireDuration, _fireDurationRandom);

    public float ShortestFireDuration => AuxMath.LowestRandom(_fireDuration, _fireDurationRandom);

    public float Cooldown => AuxMath.Randomize(_cooldown, _cooldownRandom);

    public float LongestCooldown => AuxMath.HighestRandom(_cooldown, _cooldownRandom);

    public float ShortestCooldown => AuxMath.LowestRandom(_cooldown, _cooldownRandom);

    public float FireRate => AuxMath.Randomize(_fireRate, _fireRateRandom);

    public float HighestFireRate => AuxMath.HighestRandom(_fireRate, _fireRateRandom);

    public float LowestFireRate => AuxMath.LowestRandom(_fireRate, _fireRateRandom);

    public bool InfiniteAmmo => _infiniteAmmo;

    public int InitialAmmo => _initialAmmo;

    public GameObject Projectile => _projectile;

    public GameObject HitEffect => _hitEffect;

    public float HitEffectDuration => _hitEffectDuration;

    public AudioCollection ShotAudio => _shotAudio;
}
