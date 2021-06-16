public class EnemyMovementFSM_Flank : BaseEnemyMovementFSM
{
    protected override void OnSetup()
    {
        base.OnSetup();

        FlyIntoBattle flyIntoBattle = new FlyIntoBattle(this);

        Flank flank = new Flank(this);

        EscapeLeftBoundDown escapeLeftBoundDown = new EscapeLeftBoundDown(this);
        EscapeRightBoundDown escapeRightBoundDown = new EscapeRightBoundDown(this);

        Escape escape = new Escape(this);

        AddTransition(flyIntoBattle, flank, () => Body.position.y < Config.UpperBound);

        AddTransition(flank, escapeLeftBoundDown, () => Body.position.x < Config.LeftBound);
        AddTransition(flank, escapeRightBoundDown, () => Body.position.x > Config.RightBound);
        AddTransition(flank, escape, () => Body.position.y < Config.LowerBound);

        AddTransition(escapeLeftBoundDown, flank, () => Body.position.x > Config.LeftBound);
        AddTransition(escapeLeftBoundDown, escape, () => Body.position.y < Config.LowerBound);

        AddTransition(escapeRightBoundDown, flank, () => Body.position.x < Config.RightBound);
        AddTransition(escapeRightBoundDown, escape, () => Body.position.y < Config.LowerBound);

        SetInitialState(flyIntoBattle);
        SetDefaultState(flank);
    }
}
