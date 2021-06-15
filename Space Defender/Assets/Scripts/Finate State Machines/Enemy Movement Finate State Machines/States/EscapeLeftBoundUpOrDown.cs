public class EscapeLeftBoundUpOrDown : EnemyMovementState
{
    public EscapeLeftBoundUpOrDown(BaseEnemyMovementFSM owner) : base(owner) { }

    protected override float GetTargetXSpeed() =>
        Owner.Config.XSpeed;

    protected override float GetTargetYSpeed() =>
        Owner.Config.YSpeed * AuxMath.RandomSign;
}
