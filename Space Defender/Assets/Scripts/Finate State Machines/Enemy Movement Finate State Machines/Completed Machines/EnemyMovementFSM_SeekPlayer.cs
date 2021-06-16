public class EnemyMovementFSM_SeekPlayer : BaseEnemyMovementFSM
{
    protected override void OnSetup()
    {
        base.OnSetup();

        FlyIntoBattle flyIntoBattle = new FlyIntoBattle(this);

        SeekPlayer seekPlayer = new SeekPlayer(this);

        EscapeLeftBoundDown escapeLeftBoundDown = new EscapeLeftBoundDown(this);
        EscapeRightBoundDown escapeRightBoundDown = new EscapeRightBoundDown(this);

        Escape escape = new Escape(this);

        AddTransition(flyIntoBattle, seekPlayer, () => Body.position.y < Config.UpperBound);

        AddTransition(seekPlayer, escapeLeftBoundDown, () => Body.position.x < Config.LeftBound);
        AddTransition(seekPlayer, escapeRightBoundDown, () => Body.position.x > Config.RightBound);
        AddTransition(seekPlayer, escape, () => Body.position.y < Config.LowerBound);

        AddTransition(escapeLeftBoundDown, seekPlayer, () => Body.position.x > Config.LeftBound);
        AddTransition(escapeLeftBoundDown, escape, () => Body.position.y < Config.LowerBound);

        AddTransition(escapeRightBoundDown, seekPlayer, () => Body.position.x < Config.RightBound);
        AddTransition(escapeRightBoundDown, escape, () => Body.position.y < Config.LowerBound);

        SetInitialState(flyIntoBattle);
        SetDefaultState(seekPlayer);
    }
}
