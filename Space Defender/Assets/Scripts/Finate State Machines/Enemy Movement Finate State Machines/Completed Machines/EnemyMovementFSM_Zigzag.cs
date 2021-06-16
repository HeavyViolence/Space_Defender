public class EnemyMovementFSM_Zigzag : BaseEnemyMovementFSM
{
    protected override void OnSetup()
    {
        base.OnSetup();

        FlyIntoBattle flyIntoBattle = new FlyIntoBattle(this);

        Zigzag zigzag = new Zigzag(this);

        EscapeLeftBoundDown escapeLeftBoundDown = new EscapeLeftBoundDown(this);
        EscapeRightBoundDown escapeRightBoundDown = new EscapeRightBoundDown(this);

        Escape escape = new Escape(this);

        AddTransition(flyIntoBattle, zigzag, () => Body.position.y < Config.UpperBound);

        AddTransition(zigzag, escapeLeftBoundDown, () => Body.position.x < Config.LeftBound);
        AddTransition(zigzag, escapeRightBoundDown, () => Body.position.x > Config.RightBound);
        AddTransition(zigzag, escape, () => Body.position.y < Config.LowerBound);

        AddTransition(escapeLeftBoundDown, zigzag, () => Body.position.x > Config.LeftBound);
        AddTransition(escapeLeftBoundDown, escape, () => Body.position.y < Config.LowerBound);

        AddTransition(escapeRightBoundDown, zigzag, () => Body.position.x < Config.RightBound);
        AddTransition(escapeRightBoundDown, escape, () => Body.position.y < Config.LowerBound);

        SetInitialState(flyIntoBattle);
        SetDefaultState(zigzag);
    }
}
