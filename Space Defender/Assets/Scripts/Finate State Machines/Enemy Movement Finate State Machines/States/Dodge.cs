public class Dodge : EnemyMovementState
{
    public Dodge(BaseEnemyMovementFSM owner) : base(owner) { }

    protected override float GetTargetXSpeed()
    {
        if (StateHasJustBegun)
        {
            if (Owner.PreviousState is EscapeLeftBoundUpOrDown)
                return Owner.Config.XSpeed;

            if (Owner.PreviousState is EscapeRightBoundUpOrDown)
                return Owner.Config.XSpeed * -1f;
        }

        return Owner.Config.XSpeed * AuxMath.RandomSign;
    }

    protected override float GetTargetYSpeed()
    {
        if (StateHasJustBegun)
        {
            if (Owner.PreviousState is EscapeLowerBound)
                return Owner.Config.YSpeed;

            if (Owner.PreviousState is EscapeUpperBound)
                return Owner.Config.YSpeed * -1f;
        }

        return Owner.Config.YSpeed * AuxMath.RandomSign;
    }
}
