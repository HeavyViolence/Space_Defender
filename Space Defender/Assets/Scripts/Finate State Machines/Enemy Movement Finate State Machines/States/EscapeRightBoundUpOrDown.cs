public class EscapeRightBoundUpOrDown : EnemyMovementState
{
    public EscapeRightBoundUpOrDown(BaseEnemyMovementFSM owner) : base(owner) { }

    protected override float GetTargetXSpeed() =>
        Owner.Config.XSpeed * -1f;

    protected override float GetTargetYSpeed() =>
        Owner.Config.YSpeed * AuxMath.RandomSign;
}
