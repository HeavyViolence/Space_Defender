public class FlyIntoBattle : EnemyMovementState
{
    public const float XSpeedSuppressor = 0.1f;
    public const float YSpeedAccelerator = 2f;

    public FlyIntoBattle(BaseEnemyMovementFSM owner) : base(owner) { }

    protected override float GetTargetXSpeed() =>
        Owner.Config.XSpeed * AuxMath.RandomSign * XSpeedSuppressor;

    protected override float GetTargetYSpeed() =>
        Owner.Config.YSpeed * YSpeedAccelerator * -1f;
}
