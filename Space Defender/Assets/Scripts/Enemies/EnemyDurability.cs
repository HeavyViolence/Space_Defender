public class EnemyDurability : Durability
{
    protected bool InsideViewport => transform.position.y < CameraHolder.Instance.ViewportUpperBound &&
                                     transform.position.y > CameraHolder.Instance.ViewportLowerBound;

    protected override void PerformDestruction()
    {
        base.PerformDestruction();

        CameraShaker.Instance.Shake(0.03f, 1f, 2f, 0.01f);
        Destroy(gameObject);
    }

    public override void ApplyDamage(float damage)
    {
        if (!InsideViewport) return;

        base.ApplyDamage(damage);
    }
}
