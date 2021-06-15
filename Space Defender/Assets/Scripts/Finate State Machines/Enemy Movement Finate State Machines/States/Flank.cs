public class Flank : EnemyMovementState
{
    public Flank(BaseEnemyMovementFSM owner) : base(owner) { }

    protected override float GetTargetXSpeed()
    {
        if (StateHasJustBegun)
        {
            if (Owner.PreviousState is EscapeLeftBoundDown)
                return Owner.Config.XSpeed;

            if (Owner.PreviousState is EscapeRightBoundDown)
                return Owner.Config.XSpeed * -1f;
        }

        return Owner.Config.XSpeed * AuxMath.RandomSign;
    }

    protected override float GetTargetYSpeed() =>
        Owner.Config.YSpeed * -1f;
}
