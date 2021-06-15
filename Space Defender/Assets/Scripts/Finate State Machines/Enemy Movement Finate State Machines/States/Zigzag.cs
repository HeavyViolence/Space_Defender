public class Zigzag : EnemyMovementState
{
    public bool PreviousXSpeedWasPositive { get; private set; } = AuxMath.RandomBoolean;

    public Zigzag(BaseEnemyMovementFSM owner) : base(owner) { }

    protected override float GetTargetXSpeed()
    {
        if (StateHasJustBegun)
        {
            if (Owner.PreviousState is EscapeLeftBoundDown)
            {
                PreviousXSpeedWasPositive = true;
                return Owner.Config.XSpeed;
            }

            if (Owner.PreviousState is EscapeRightBoundDown)
            {
                PreviousXSpeedWasPositive = false;
                return Owner.Config.XSpeed * -1f;
            }
        }
        else
        {
            if (PreviousXSpeedWasPositive)
            {
                PreviousXSpeedWasPositive = false;
                return Owner.Config.XSpeed * -1f;
            }
            else
            {
                PreviousXSpeedWasPositive = true;
                return Owner.Config.XSpeed;
            }
        }

        return Owner.Config.XSpeed * AuxMath.RandomSign;
    }

    protected override float GetTargetYSpeed() =>
        Owner.Config.YSpeed * -1f;
}
