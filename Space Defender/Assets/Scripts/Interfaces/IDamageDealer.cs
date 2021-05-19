using System;

public interface IDamageDealer
{
    public event EventHandler<ProjectileHitEventArgs> ProjectileHit;
}
