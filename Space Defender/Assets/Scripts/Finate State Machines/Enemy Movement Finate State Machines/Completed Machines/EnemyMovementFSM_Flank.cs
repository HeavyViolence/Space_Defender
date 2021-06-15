public class EnemyMovementFSM_Flank : BaseEnemyMovementFSM
{
    protected override void OnSetup()
    {
        base.OnSetup();

        Flank flank = new Flank(this);

        EscapeLeftBoundDown escapeLeftBoundDown = new EscapeLeftBoundDown(this);
        EscapeRightBoundDown escapeRightBoundDown = new EscapeRightBoundDown(this);

        AddTransition(flank, escapeLeftBoundDown, () => Body.position.x < Config.LeftBound);
        AddTransition(flank, escapeRightBoundDown, () => Body.position.x > Config.RightBound);

        AddTransition(escapeLeftBoundDown, flank, () => Body.position.x > Config.LeftBound);
        AddTransition(escapeRightBoundDown, flank, () => Body.position.x < Config.RightBound);

        SetInitialState(flank);
        SetDefaultState(flank);
    }
}
