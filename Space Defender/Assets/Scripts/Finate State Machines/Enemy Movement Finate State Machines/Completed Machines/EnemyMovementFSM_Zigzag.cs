public class EnemyMovementFSM_Zigzag : BaseEnemyMovementFSM
{
    protected override void OnSetup()
    {
        base.OnSetup();

        Zigzag zigzag = new Zigzag(this);

        EscapeLeftBoundDown escapeLeftBoundDown = new EscapeLeftBoundDown(this);
        EscapeRightBoundDown escapeRightBoundDown = new EscapeRightBoundDown(this);

        AddTransition(zigzag, escapeLeftBoundDown, () => Body.position.x < Config.LeftBound);
        AddTransition(zigzag, escapeRightBoundDown, () => Body.position.x > Config.RightBound);

        AddTransition(escapeLeftBoundDown, zigzag, () => Body.position.x > Config.LeftBound);
        AddTransition(escapeRightBoundDown, zigzag, () => Body.position.x < Config.RightBound);

        SetInitialState(zigzag);
        SetDefaultState(zigzag);
    }
}
