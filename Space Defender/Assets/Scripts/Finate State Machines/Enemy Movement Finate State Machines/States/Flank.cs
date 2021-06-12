public class Flank : EnemyMovementState
{
    public Flank(BaseEnemyMovementFSM owner) : base(owner) { }

    protected override float GetTargetXSpeed() =>
        Owner.Config.XSpeed * AuxMath.RandomSign;

    protected override float GetTargetYSpeed() =>
        Owner.Config.YSpeed * -1f;
}
