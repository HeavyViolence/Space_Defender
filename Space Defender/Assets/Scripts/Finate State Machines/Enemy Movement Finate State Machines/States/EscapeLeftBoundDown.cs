public class EscapeLeftBoundDown : EnemyMovementState
{
    public EscapeLeftBoundDown(BaseEnemyMovementFSM owner) : base(owner) { }

    public bool HasEscaped { get; private set; } = false;

    protected override float GetTargetXSpeed() =>
        Owner.Config.XSpeed;

    protected override float GetTargetYSpeed() =>
        Owner.Config.YSpeed * -1f;
}
