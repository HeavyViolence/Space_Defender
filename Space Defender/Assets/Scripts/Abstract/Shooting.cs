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
        get => _config.InfiniteAmmo ? int.MaxValue : _ammo;
        private set => _ammo = Mathf.Clamp(value, 0, FireConfig.MaxAmmo);
    }

    protected GameObject Projectile => _config.Projectile;

    protected GameObject HitEffect => _config.HitEffect;

    protected AudioCollection ShotAudio => _config.ShotAudio;

    protected virtual void Awake()
    {
        SetupInitialAmmo();
        FindAllMuzzlePoints();
        SetRandomSelectedMuzzlePoint();
    }

    private void SetupInitialAmmo()
    {
        if (!_config.InfiniteAmmo) Ammo = _config.InitialAmmo;
    }

    private void FindAllMuzzlePoints()
    {
        foreach (var p in gameObject.GetComponentsInChildren<MuzzlePoint>()) _muzzlePoints.Add(p);
    }

    private void SetRandomSelectedMuzzlePoint()
    {
        _selectedMuzzlePoint = Random.Range(0, _muzzlePoints.Count);
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
                PerformShots();

                yield return new WaitForSeconds(1f / FireRate);
            }

            yield return new WaitForSeconds(Cooldown);
        }

        FiringCoroutine = null;
    }

    private void PerformShots()
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
        if (Projectile == null) throw new System.Exception("Projectile prefab is not set up!");

        if (Ammo <= 0) return;

        if (_config.SprayEnabled) for (int i = 0; i < _config.SprayShellsAmount; i++) Perform(point);
        else Perform(point);

        PlayShotAudioIfExists(point.Pos3D);

        Ammo--;

        void Perform(IMuzzlePoint point)
        {
            Quaternion dispersion = Quaternion.Euler(point.RotX, point.RotY, Dispersion);

            GameObject projectile = Instantiate(Projectile, point.Pos3D, point.Rot4D * dispersion);

            if (projectile.TryGetComponent(out IDamageDealer d)) d.ProjectileHit += ProjectileHitEventHandler;
            if (projectile.TryGetComponent(out ProjectileMovement m)) m.Speed = ProjectileSpeed;
        }
    }

    protected virtual void ProjectileHitEventHandler(object sender, ProjectileHitEventArgs e)
    {
        e.Recipient?.ApplyDamage(Damage);
        PlayHitEffectIfExists(e.HitPos);
    }

    private void PlayShotAudioIfExists(Vector3 playPos)
    {
        if (ShotAudio != null) ShotAudio.PlayRandomClip(playPos);
    }

    private void PlayHitEffectIfExists(Vector3 hitPos)
    {
        if (HitEffect != null)
        {
            GameObject hitEffect = Instantiate(HitEffect, hitPos, Quaternion.identity);
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
