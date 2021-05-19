using System;
using UnityEngine;

public class DamageDealer : MonoBehaviour, IDamageDealer
{
    public event EventHandler<ProjectileHitEventArgs> ProjectileHit;

    protected virtual void OnProjectileHit(ProjectileHitEventArgs e)
    {
        ProjectileHit?.Invoke(this, e);
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        HandleProjectileHit(c);
    }

    protected virtual void HandleProjectileHit(Collider2D c)
    {
        IDamageable recipient = null;

        if (c.gameObject.TryGetComponent(out IDamageable d)) recipient = d;

        OnProjectileHit(new ProjectileHitEventArgs(transform.position, recipient));
    }
}
