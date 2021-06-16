public class EnemyMovementFSM_AvoidPlayer : BaseEnemyMovementFSM
{
    protected override void OnSetup()
    {
        base.OnSetup();

        FlyIntoBattle flyIntoBattle = new FlyIntoBattle(this);

        AvoidPlayer avoidPlayer = new AvoidPlayer(this);

        EscapeLeftBoundDown escapeLeftBoundDown = new EscapeLeftBoundDown(this);
        EscapeRightBoundDown escapeRightBoundDown = new EscapeRightBoundDown(this);

        Escape escape = new Escape(this);

        AddTransition(flyIntoBattle, avoidPlayer, () => Body.position.y < Config.UpperBound);

        AddTransition(avoidPlayer, escapeLeftBoundDown, () => Body.position.x < Config.LeftBound);
        AddTransition(avoidPlayer, escapeRightBoundDown, () => Body.position.x > Config.RightBound);
        AddTransition(avoidPlayer, escape, () => Body.position.y < Config.LowerBound);

        AddTransition(escapeLeftBoundDown, avoidPlayer, () => Body.position.x > Config.LeftBound);
        AddTransition(escapeLeftBoundDown, escape, () => Body.position.y < Config.LowerBound);

        AddTransition(escapeRightBoundDown, avoidPlayer, () => Body.position.x < Config.RightBound);
        AddTransition(escapeRightBoundDown, escape, () => Body.position.y < Config.LowerBound);

        SetInitialState(flyIntoBattle);
        SetDefaultState(avoidPlayer);
    }
}
