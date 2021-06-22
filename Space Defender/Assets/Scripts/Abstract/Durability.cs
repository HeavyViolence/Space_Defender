using UnityEngine;

public abstract class Durability : MonoBehaviour, IDamageable
{
    [SerializeField] private DurabilityConfig _config = null;

    [SerializeField] private Healthbar _healthbar = null;

    private float _value = 0f;

    public float Value
    {
        get => _value;

        protected set
        {
            _value = Mathf.Clamp(value, 0f, _config.MaxDurability);

            UpdateHealthbar();

            if (_value == 0f) PerformDestruction();
        }
    }

    public bool DestructionInProgress { get; private set; } = false;

    protected virtual void Awake()
    {
        Value = _config.MaxDurability;
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

    public void ApplyDamage(float damage)
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

        Destroy(gameObject);
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
        if (_config.ReconstructionEnabled && _config.ReconstructionRate > 0f)
        {
            if (_config.ReconstructionBounded)
            {
                if (AuxMath.ValueWithinRange(Value, _config.ReconstructionLowerBound, _config.ReconstructionUpperBound))
                {
                    Value += _config.ReconstructionRate * Time.deltaTime;

                    if (!_healthbar.BlinkingEnabled) _healthbar.EnableBlinking();
                }
                else if (_healthbar.BlinkingEnabled) _healthbar.DisableBlinking();
            }
            else
            {
                Value += _config.ReconstructionRate * Time.deltaTime;

                if (!_healthbar.BlinkingEnabled) _healthbar.EnableBlinking();
            }
        }
    }
}
