public class EnemyMovementFSM_SeekPlayer : BaseEnemyMovementFSM
{
    protected override void OnSetup()
    {
        base.OnSetup();

        SeekPlayer seekPlayer = new SeekPlayer(this);

        EscapeLeftBoundDown escapeLeftBoundDown = new EscapeLeftBoundDown(this);
        EscapeRightBoundDown escapeRightBoundDown = new EscapeRightBoundDown(this);

        AddTransition(seekPlayer, escapeLeftBoundDown, () => Body.position.x < Config.LeftBound);
        AddTransition(seekPlayer, escapeRightBoundDown, () => Body.position.x > Config.RightBound);

        AddTransition(escapeLeftBoundDown, seekPlayer, () => Body.position.x > Config.LeftBound);
        AddTransition(escapeRightBoundDown, seekPlayer, () => Body.position.x < Config.RightBound);

        SetInitialState(seekPlayer);
        SetDefaultState(seekPlayer);
    }
}
