public class EscapeRightBoundDown : EnemyMovementState
{
    public EscapeRightBoundDown(BaseEnemyMovementFSM owner) : base(owner) { }

    public bool HasEscaped { get; private set; } = false;

    protected override float GetTargetXSpeed() =>
        Owner.Config.XSpeed * -1f;

    protected override float GetTargetYSpeed() =>
        Owner.Config.YSpeed * -1f;
}
