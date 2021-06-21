using UnityEngine;

public enum FireType
{
    SingleConsecutive, SingleRandom, AllAtOnce
}

[CreateAssetMenu(fileName = "New Fire Config", menuName = "Configs/Fire Config")]
public class FireConfig : ScriptableObject
{
    public const int MinSprayShells = 2;
    public const int MaxSprayShells = 10;

    public const float MinFireDelay = 1f;
    public const float MaxFireDelay = 10f;

    public const float MinDamage = 10f;
    public const float MaxDamage = 1000f;

    public const float MinSpeed = 5f;
    public const float MaxSpeed = 30f;

    public const float MinRPM = 30f;
    public const float MaxRPM = 300f;

    public const float MinDispersion = 0.1f;
    public const float MaxDispersion = 10f;

    public const float MinFireDuration = 1f;
    public const float MaxFireDuration = 10f;

    public const float MinCoolDown = 1f;
    public const float MaxCooldown = 10f;

    public const float MinFireRate = 0.1f;
    public const float MaxFireRate = 10f;

    public const int MaxAmmo = 999;

    public const float MinHitEffectDuration = 1f;
    public const float MaxHitEffectDuration = 5f;

    [SerializeField] private FireType _fireType = FireType.SingleConsecutive;

    [SerializeField] private bool _sprayEnabled = false;
    [SerializeField] private int _sprayShells = MinSprayShells;
    [SerializeField] private float _sprayShellsRandom = 0f;

    [SerializeField] private bool _fireDelayEnabled = false;
    [SerializeField] private float _fireDelay = MinFireDelay;
    [SerializeField] private float _fireDelayRandom = 0f;

    [SerializeField] private float _damage = MinDamage;
    [SerializeField] private float _damageRandom = 0f;

    [SerializeField] private float _speed = MinSpeed;
    [SerializeField] private float _speedRandom = 0f;

    [SerializeField] private bool _rotationEnabled = false;
    [SerializeField] private float _rpm = MinRPM;
    [SerializeField] private float _rpmRandom = 0f;

    [SerializeField] private float _dispersion = MinDispersion;
    [SerializeField] private float _dispersionRandom = 0f;

    [SerializeField] private float _fireDuration = MinFireDuration;
    [SerializeField] private float _fireDurationRandom = 0f;

    [SerializeField] private float _cooldown = MinCoolDown;
    [SerializeField] private float _cooldownRandom = 0f;

    [SerializeField] private float _fireRate = MinFireRate;
    [SerializeField] private float _fireRateRandom = 0f;

    [SerializeField] private bool _infiniteAmmo = false;
    [SerializeField] private int _initialAmmo = 0;

    [SerializeField] private GameObject _projectile = null;

    [SerializeField] private GameObject _hitEffect = null;
    [SerializeField] private float _hitEffectDuration = MinHitEffectDuration;

    [SerializeField] private AudioCollection _shotAudio = null;

    public FireType FireType => _fireType;

    public bool SprayEnabled => _sprayEnabled;

    public int SprayShellsAmount
    {
        get
        {
            float rawValue = AuxMath.Randomize(_sprayShells, _sprayShellsRandom);
            float clampedRawValue = Mathf.Clamp(rawValue, 1f, Mathf.Infinity);

            return Mathf.RoundToInt(clampedRawValue);
        }
    }

    public bool FireDelayEnabled => _fireDelayEnabled;

    public float FireDelay => _fireDelayEnabled ? AuxMath.Randomize(_fireDelay, _fireDelayRandom) : 0f;

    public float ShortestFireDelay => _fireDelayEnabled ? AuxMath.GetLowestRandom(_fireDelay, _fireDelayRandom) : 0f;

    public float LongestFireDelay => _fireDelayEnabled ? AuxMath.GetHighestRandom(_fireDelay, _fireDelayRandom) : 0f;

    public float Damage => AuxMath.Randomize(_damage, _damageRandom);

    public float HighestDamage => AuxMath.GetHighestRandom(_damage, _damageRandom);

    public float LowestDamage => AuxMath.GetLowestRandom(_damage, _damageRandom);

    public float Speed => AuxMath.Randomize(_speed, _speedRandom);

    public float FastestSpeed => AuxMath.GetHighestRandom(_speed, _speedRandom);

    public float SlowestSpeed => AuxMath.GetLowestRandom(_speed, _speedRandom);

    public bool RotationEnabled => _rotationEnabled;

    public float RPM => _rotationEnabled ? AuxMath.Randomize(_rpm, _rpmRandom) : 0f;

    public float HighestRPM => _rotationEnabled ? AuxMath.GetHighestRandom(_rpm, _rpmRandom) : 0f;

    public float LowestRPM => _rotationEnabled ? AuxMath.GetLowestRandom(_rpm, _rpmRandom) : 0f;

    public float Dispersion => AuxMath.Randomize(_dispersion, _dispersionRandom) * AuxMath.RandomSign;

    public float HighestDispersion => AuxMath.GetHighestRandom(_dispersion, _dispersionRandom);

    public float LowestDispersion => AuxMath.GetLowestRandom(_dispersion, _dispersionRandom);

    public float FireDuration => AuxMath.Randomize(_fireDuration, _fireDurationRandom);

    public float LongestFireDuration => AuxMath.GetHighestRandom(_fireDuration, _fireDurationRandom);

    public float ShortestFireDuration => AuxMath.GetLowestRandom(_fireDuration, _fireDurationRandom);

    public float Cooldown => AuxMath.Randomize(_cooldown, _cooldownRandom);

    public float LongestCooldown => AuxMath.GetHighestRandom(_cooldown, _cooldownRandom);

    public float ShortestCooldown => AuxMath.GetLowestRandom(_cooldown, _cooldownRandom);

    public float FireRate => AuxMath.Randomize(_fireRate, _fireRateRandom);

    public float HighestFireRate => AuxMath.GetHighestRandom(_fireRate, _fireRateRandom);

    public float LowestFireRate => AuxMath.GetLowestRandom(_fireRate, _fireRateRandom);

    public bool InfiniteAmmo => _infiniteAmmo;

    public int InitialAmmo => _initialAmmo;

    public GameObject Projectile => _projectile;

    public GameObject HitEffect => _hitEffect;

    public float HitEffectDuration => _hitEffectDuration;

    public AudioCollection ShotAudio => _shotAudio;
}
