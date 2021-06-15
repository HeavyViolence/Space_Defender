public class EscapeLowerBound : EnemyMovementState
{
    public EscapeLowerBound(BaseEnemyMovementFSM owner) : base(owner) { }

    protected override float GetTargetXSpeed() =>
        Owner.Config.XSpeed * AuxMath.RandomSign;

    protected override float GetTargetYSpeed() =>
        Owner.Config.YSpeed;
}
