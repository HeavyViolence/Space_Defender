using UnityEngine;

public class Escape : EnemyMovementState
{
    public Escape(BaseEnemyMovementFSM owner) : base(owner) { }

    public override void OnStateEnter()
    {
        Object.Destroy(Owner.gameObject);
    }

    protected override float GetTargetXSpeed() => 0f;

    protected override float GetTargetYSpeed() => 0f;
}
