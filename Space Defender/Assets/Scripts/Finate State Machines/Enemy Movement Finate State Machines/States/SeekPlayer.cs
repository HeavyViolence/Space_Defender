using UnityEngine;

public class SeekPlayer : EnemyMovementState
{
    public SeekPlayer(BaseEnemyMovementFSM owner) : base(owner) { }

    protected override float GetTargetXSpeed()
    {
        if (StateHasJustBegun)
        {
            if (Owner.PreviousState is EscapeLeftBoundDown)
                return Owner.Config.XSpeed;

            if (Owner.PreviousState is EscapeRightBoundDown)
                return Owner.Config.XSpeed * -1f;
        }
        else
        {
            RaycastHit2D hitInfo = Physics2D.CircleCast(Owner.Body.position,
                                                        Mathf.Infinity,
                                                        Vector2.down,
                                                        Mathf.Infinity,
                                                        LayerMask.GetMask("Player"));

            if (hitInfo.point.x > Owner.Body.position.x)
                return Owner.Config.XSpeed;

            else return Owner.Config.XSpeed * -1f;
        }

        return Owner.Config.XSpeed * AuxMath.RandomSign;
    }

    protected override float GetTargetYSpeed() =>
        Owner.Config.YSpeed * -1f;
}
