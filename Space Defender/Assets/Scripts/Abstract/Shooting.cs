using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Shooting : MonoBehaviour
{
    [SerializeField] protected FireConfig _config = null;

    protected Coroutine _firingCoroutine = null;

    private readonly List<IMuzzlePoint> _muzzlePoints = new List<IMuzzlePoint>();
    private int _selectedMuzzlePoint = 0;

    private int _ammo = 0;

    protected virtual float Damage => _config.Damage;

    protected virtual float Dispersion => _config.Dispersion * AuxMath.RandomSign;

    protected virtual float FireDuration => _config.FireDuration;

    protected virtual float Cooldown => _config.Cooldown;

    protected virtual float FireRate => _config.FireRate;

    protected int Ammo
    {
        get => _config.InfiniteAmmo ? -1 : _ammo;
        set => _ammo = Mathf.Clamp(value, 0, FireConfig.MaxAmmo);
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
        while (Ammo > 0 || _config.InfiniteAmmo)
        {
            int projectilesToFire = Mathf.RoundToInt(FireDuration * FireRate);

            for (int i = 0; i < projectilesToFire; i++)
            {
                switch (_config.FireType)
                {
                    case FireType.SingleConsecutive:
                        {
                            PerformShots(1, _config.FireType);
                            break;
                        }
                    case FireType.SingleRandom:
                        {
                            PerformShots(1, _config.FireType);
                            break;
                        }
                    case FireType.AllAtOnce:
                        {
                            PerformShots(1, _config.FireType);
                            break;
                        }
                }

                yield return new WaitForSeconds(1f / FireRate);
            }

            yield return new WaitForSeconds(Cooldown);
        }

        _firingCoroutine = null;
    }

    private void PerformShots(int shotsAmount, FireType fireType)
    {
        for (int i  = 0; i < shotsAmount; i++)
        {
            switch (fireType)
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
    }

    protected virtual void PerformShot(IMuzzlePoint point)
    {
        if (_config.Projectile != null && (Ammo > 0 || _config.InfiniteAmmo))
        {
            Ammo--;

            Quaternion dispersion = Quaternion.Euler(point.RotX, point.RotY, Dispersion);

            GameObject projectile = Instantiate(_config.Projectile, point.Pos3D, point.Rot4D * dispersion);

            PlayShotAudioIfExists(point.Pos3D);

            if (projectile.gameObject.TryGetComponent(out IDamageDealer d)) d.ProjectileHit += ProjectileHitEventHandler;
        }
    }

    protected virtual void ProjectileHitEventHandler(object sender, ProjectileHitEventArgs e)
    {
        e.Recipient?.ApplyDamage(Damage);
        SpawnHitEffectIfExists(e.HitPos);
    }

    private void PlayShotAudioIfExists(Vector3 playPos)
    {
        if (ShotAudio != null) ShotAudio.PlayRandomClip(playPos);
    }

    private void SpawnHitEffectIfExists(Vector3 hitPos)
    {
        if (HitEffect != null)
        {
            GameObject hitEffect = Instantiate(HitEffect, hitPos, Quaternion.identity);

            float lifeTime = 0f;

            if (HitEffect.TryGetComponent(out ParticleSystem p)) lifeTime = p.main.duration;

            Destroy(hitEffect, lifeTime);
        }
    }

    private IMuzzlePoint GetRandomMuzzlePoint() => _muzzlePoints[Random.Range(0, _muzzlePoints.Count)];

    private IMuzzlePoint GetNextMuzzlePoint()
    {
        _selectedMuzzlePoint = (int)Mathf.Repeat(++_selectedMuzzlePoint, _muzzlePoints.Count);

        return _muzzlePoints[_selectedMuzzlePoint];
    }
}
