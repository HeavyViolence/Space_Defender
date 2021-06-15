public class EscapeUpperBound : EnemyMovementState
{
    public EscapeUpperBound(BaseEnemyMovementFSM owner) : base(owner) { }

    protected override float GetTargetXSpeed() =>
        Owner.Config.XSpeed;

    protected override float GetTargetYSpeed() =>
        Owner.Config.YSpeed * -1f;
}
