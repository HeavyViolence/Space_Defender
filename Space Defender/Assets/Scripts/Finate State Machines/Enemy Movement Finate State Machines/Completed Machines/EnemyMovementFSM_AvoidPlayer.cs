public class EnemyMovementFSM_AvoidPlayer : BaseEnemyMovementFSM
{
    protected override void OnSetup()
    {
        base.OnSetup();

        AvoidPlayer avoidPlayer = new AvoidPlayer(this);

        EscapeLeftBoundDown escapeLeftBoundDown = new EscapeLeftBoundDown(this);
        EscapeRightBoundDown escapeRightBoundDown = new EscapeRightBoundDown(this);

        AddTransition(avoidPlayer, escapeLeftBoundDown, () => Body.position.x < Config.LeftBound);
        AddTransition(avoidPlayer, escapeRightBoundDown, () => Body.position.x > Config.RightBound);

        AddTransition(escapeLeftBoundDown, avoidPlayer, () => Body.position.x > Config.LeftBound);
        AddTransition(escapeRightBoundDown, avoidPlayer, () => Body.position.x < Config.RightBound);

        SetInitialState(avoidPlayer);
        SetDefaultState(avoidPlayer);
    }
}
