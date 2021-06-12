using UnityEngine;

public abstract class EnemyMovementState : State
{
    public float TargetXSpeed { get; private set; } = 0f;
    public float PreviousTargetXSpeed { get; private set; } = 0f;
    public float CurrentXSpeed { get; private set; } = 0f;
    public float XSpeedDuration { get; private set; } = 0f;
    public float XSpeedTimer { get; private set; } = 0f;
    public float XSpeedTransitionDuration { get; private set; } = 0f;

    public float TargetYSpeed { get; private set; } = 0f;
    public float PreviousTargetYSpeed { get; private set; } = 0f;
    public float CurrentYSpeed { get; private set; } = 0f;
    public float YSpeedDuration { get; private set; } = 0f;
    public float YSpeedTimer { get; private set; } = 0f;
    public float YSpeedTransitionDuration { get; private set; } = 0f;

    public Vector2 Velocity => new Vector2(CurrentXSpeed, CurrentYSpeed) * Time.deltaTime;
    public BaseEnemyMovementFSM Owner { get; } = null;

    public EnemyMovementState(BaseEnemyMovementFSM owner) { Owner = owner; }

    public override void OnStateEnter()
    {
        TargetXSpeed = GetTargetXSpeed();
        PreviousTargetXSpeed = Owner.LastXSpeed;
        XSpeedDuration = Owner.Config.XSpeedDuration;
        XSpeedTimer = 0f;
        XSpeedTransitionDuration = Owner.Config.XSpeedTransitionDuration;

        TargetYSpeed = GetTargetYSpeed();
        PreviousTargetYSpeed = Owner.LastYSpeed;
        YSpeedDuration = Owner.Config.YSpeedDuration;
        YSpeedTimer = 0f;
        YSpeedTransitionDuration = Owner.Config.YSpeedTransitionDuration;
    }

    public override void OnStateExit()
    {
        Owner.LastXSpeed = CurrentXSpeed;
        Owner.LastYSpeed = CurrentYSpeed;
    }

    public override void OnStatePhysicsUpdate()
    {
        Owner.Body.MovePosition(Owner.Body.position + Velocity);
    }

    public override void OnStateUpdate()
    {
        UpdateXSpeed();
        UpdateYSpeed();
    }

    private void UpdateXSpeed()
    {
        XSpeedTimer += Time.deltaTime;

        if (XSpeedTimer < XSpeedDuration + XSpeedTransitionDuration)
            CurrentXSpeed = Mathf.Lerp(PreviousTargetXSpeed, TargetXSpeed, XSpeedTimer / XSpeedTransitionDuration);

        else PerformXSpeedTransition();
    }

    private void UpdateYSpeed()
    {
        YSpeedTimer += Time.deltaTime;

        if (YSpeedTimer < YSpeedDuration + YSpeedTransitionDuration)
            CurrentYSpeed = Mathf.Lerp(PreviousTargetYSpeed, TargetYSpeed, YSpeedTimer / YSpeedTransitionDuration);

        else PerformYSpeedTransition();
    }

    protected void PerformXSpeedTransition()
    {
        XSpeedTimer = 0f;
        XSpeedDuration = Owner.Config.XSpeedDuration;
        XSpeedTransitionDuration = Owner.Config.XSpeedTransitionDuration;
        PreviousTargetXSpeed = TargetXSpeed;
        TargetXSpeed = GetTargetXSpeed();
    }

    protected void PerformYSpeedTransition()
    {
        YSpeedTimer = 0f;
        YSpeedDuration = Owner.Config.YSpeedDuration;
        YSpeedTransitionDuration = Owner.Config.YSpeedTransitionDuration;
        PreviousTargetYSpeed = TargetYSpeed;
        TargetYSpeed = GetTargetYSpeed();
    }

    protected abstract float GetTargetXSpeed();
    protected abstract float GetTargetYSpeed();
}
