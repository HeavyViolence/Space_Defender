public class EnemyDurability : Durability
{
    protected override void PerformDestruction()
    {
        base.PerformDestruction();

        CameraShaker.Instance.Shake(0.03f, 1f, 2f, 0.01f);
    }
}
