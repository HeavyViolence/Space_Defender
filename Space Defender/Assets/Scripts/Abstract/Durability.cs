using UnityEngine;

public abstract class Durability : MonoBehaviour, IDamageable
{
    [SerializeField] private DurabilityConfig _config = null;

    [SerializeField] private Healthbar _healthbar = null;

    private float _value = 0f;

    protected float Value
    {
        get => _value;

        set
        {
            _value = Mathf.Clamp(value, 0f, _config.MaxDurability);

            UpdateHealthbar();

            if (_value == 0f) PerformDestruction();
        }
    }

    protected virtual float MaxValue => _config.MaxDurability;

    protected virtual float ReconstructionRate => _config.ReconstructionRate;

    protected float TotalRegainedValue { get; private set; } = 0f;

    protected bool DestructionInProgress { get; private set; } = false;

    protected bool ReconstructionActive => _config.ReconstructionEnabled &&
                                           _config.ReconstructionRate > 0f &&
                                           Value < MaxValue;

    protected bool ValueInReconstructionRange =>
        AuxMath.ValueWithinRange(Value, _config.ReconstructionLowerBound, _config.ReconstructionUpperBound);

    protected virtual void Awake()
    {
        Value = MaxValue;
    }

    protected virtual void Update()
    {
        Reconstruct();
    }

    private void UpdateHealthbar()
    {
        if (_healthbar != null)
        {
            if (_value == _config.MaxDurability) _healthbar.Hide();
            else _healthbar.Show();

            _healthbar.SetValue(_value, _config.MaxDurability);
        }
    }

    public virtual void ApplyDamage(float damage)
    {
        if (DestructionInProgress) return;

        Value -= Mathf.Abs(damage);
    }

    protected virtual void PerformDestruction()
    {
        if (DestructionInProgress) return;

        DestructionInProgress = true;

        PlayExplosionEffectIfExists();
        PlayExplosionAudioIfExists();
    }

    private void PlayExplosionEffectIfExists()
    {
        if (_config.ExplosionEffect != null)
        {
            GameObject effect = Instantiate(_config.ExplosionEffect, transform.position, Quaternion.identity);
            Destroy(effect, _config.ExplosionDuration);
        }
    }

    private void PlayExplosionAudioIfExists()
    {
        if (_config.ExplosionAudio != null)
            _config.ExplosionAudio.PlayRandomClipUnrepeated(transform.position);
    }

    private void Reconstruct()
    {
        if (ReconstructionActive || ReconstructionRate > 0f)
        {
            if (_config.ReconstructionBounded)
            {
                if (ValueInReconstructionRange)
                    Perform();

                else if (_healthbar.BlinkingEnabled)
                    _healthbar.DisableBlinking();
            }
            else Perform();
        }

        void Perform()
        {
            float gainedValue = ReconstructionRate * Time.deltaTime;

            Value += gainedValue;
            TotalRegainedValue += gainedValue;

            if (!_healthbar.BlinkingEnabled) _healthbar.EnableBlinking();
        }
    }
}
