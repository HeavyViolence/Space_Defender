using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Shooting : MonoBehaviour
{
    [SerializeField] protected FireConfig _config = null;

    protected Coroutine FiringCoroutine { get; set; } = null;

    private readonly List<IMuzzlePoint> _muzzlePoints = new List<IMuzzlePoint>();
    private int _selectedMuzzlePoint = 0;

    private int _ammo = 0;

    protected virtual float Damage => _config.Damage;

    protected virtual float ProjectileSpeed => _config.Speed;

    protected virtual float Dispersion => _config.Dispersion;

    protected virtual float FireDuration => _config.FireDuration;

    protected virtual float Cooldown => _config.Cooldown;

    protected virtual float FireRate => _config.FireRate;

    protected int Ammo
    {
        get => _config.InfiniteAmmoEnabled ? int.MaxValue : _ammo;
        private set => _ammo = Mathf.Clamp(value, 0, FireConfig.MaxAmmo);
    }

    protected virtual void Awake()
    {
        SetupInitialAmmo();
        FindAllMuzzlePoints();
        SetRandomSelectedMuzzlePoint();
        SetupAmmoReplenishment();
    }

    private void SetupInitialAmmo()
    {
        if (!_config.InfiniteAmmoEnabled) Ammo = _config.InitialAmmo;
    }

    private void FindAllMuzzlePoints()
    {
        foreach (var p in gameObject.GetComponentsInChildren<MuzzlePoint>())
            _muzzlePoints.Add(p);

        _config.GunsAmount = _muzzlePoints.Count;
    }

    private void SetRandomSelectedMuzzlePoint()
    {
        _selectedMuzzlePoint = Random.Range(0, _muzzlePoints.Count);
    }

    private void SetupAmmoReplenishment()
    {
        if (_config.AmmoReplenismentEnabled)
            StartCoroutine(RefillAmmo());
    }

    private IEnumerator RefillAmmo()
    {
        while (true)
        {
            yield return new WaitForSeconds(_config.AmmoRefillDelay);

            Ammo += _config.RefilledAmmoAmount;
        }
    }

    protected abstract void Fire();

    protected IEnumerator Firing()
    {
        if (_config.FireDelayEnabled)
            yield return new WaitForSeconds(_config.FireDelay);

        while (Ammo > 0)
        {
            int projectilesToFire = Mathf.RoundToInt(FireDuration * FireRate);

            for (int i = 0; i < projectilesToFire; i++)
            {
                HandleShot();

                yield return new WaitForSeconds(1f / FireRate);
            }

            yield return new WaitForSeconds(Cooldown);
        }

        FiringCoroutine = null;
    }

    private void HandleShot()
    {
        switch (_config.FireType)
        {
            case FireType.SingleConsecutive:
                {
                    PerformShot(GetNextMuzzlePoint());
                    break;
                }
            case FireType.SingleRandom:
                {
                    PerformShot(GetRandomMuzzlePoint());
                    break;
                }
            case FireType.AllAtOnce:
                {
                    for (int j = 0; j < _muzzlePoints.Count; j++)
                        PerformShot(GetNextMuzzlePoint());
                    break;
                }
        }
    }

    protected virtual void PerformShot(IMuzzlePoint point)
    {
        if (_config.Projectile == null) throw new System.Exception("Projectile prefab is not set up!");

        if (Ammo == 0) return;

        if (_config.SprayEnabled)
            for (int i = 0; i < _config.SprayShellsAmount; i++) Perform(point);
        
        else Perform(point);

        PlayShotAudioIfExists(point.Pos3D);

        Ammo--;

        void Perform(IMuzzlePoint point)
        {
            Quaternion dispersion = Quaternion.Euler(point.RotX, point.RotY, Dispersion);

            GameObject projectile = Instantiate(_config.Projectile, point.Pos3D, point.Rot4D * dispersion);

            if (projectile.TryGetComponent(out IDamageDealer d)) d.ProjectileHit += ProjectileHitEventHandler;
            if (projectile.TryGetComponent(out ProjectileMovement m)) m.Speed = ProjectileSpeed;
        }
    }

    protected virtual void ProjectileHitEventHandler(object sender, ProjectileHitEventArgs e)
    {
        e.Recipient?.ApplyDamage(Damage);
        PlayHitEffectIfExists(e.HitPos);
        PlayHitAudioIfExists(e.HitPos);
    }

    private void PlayShotAudioIfExists(Vector3 playPos)
    {
        if (_config.ShotAudio != null) _config.ShotAudio.PlayRandomClip(playPos);
    }

    private void PlayHitAudioIfExists(Vector3 playPos)
    {
        if (_config.HitAudio != null) _config.HitAudio.PlayRandomClip(playPos);
    }

    private void PlayHitEffectIfExists(Vector3 hitPos)
    {
        if (_config.HitEffect != null)
        {
            GameObject hitEffect = Instantiate(_config.HitEffect, hitPos, Quaternion.identity);
            Destroy(hitEffect, _config.HitEffectDuration);
        }
    }

    private IMuzzlePoint GetRandomMuzzlePoint() =>
        _muzzlePoints[Random.Range(0, _muzzlePoints.Count)];

    private IMuzzlePoint GetNextMuzzlePoint()
    {
        _selectedMuzzlePoint = (int)Mathf.Repeat(++_selectedMuzzlePoint, _muzzlePoints.Count);

        return _muzzlePoints[_selectedMuzzlePoint];
    }
}
