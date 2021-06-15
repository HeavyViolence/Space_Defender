public class EnemyMovementFSM_Dodge : BaseEnemyMovementFSM
{
    protected override void OnSetup()
    {
        base.OnSetup();

        Dodge dodge = new Dodge(this);

        EscapeLeftBoundUpOrDown escapeLeftBoundUpOrDown = new EscapeLeftBoundUpOrDown(this);
        EscapeRightBoundUpOrDown escapeRightBoundUpOrDown = new EscapeRightBoundUpOrDown(this);

        EscapeLowerBound escapeLowerBound = new EscapeLowerBound(this);
        EscapeUpperBound escapeUpperBound = new EscapeUpperBound(this);

        AddTransition(dodge, escapeLeftBoundUpOrDown, () => Body.position.x < Config.LeftBound);
        AddTransition(dodge, escapeRightBoundUpOrDown, () => Body.position.x > Config.RightBound);
        AddTransition(dodge, escapeLowerBound, () => Body.position.y < Config.LowerBound);
        AddTransition(dodge, escapeUpperBound, () => Body.position.y > Config.UpperBound);

        AddTransition(escapeLeftBoundUpOrDown, dodge, () => Body.position.x > Config.LeftBound &&
                                                            AuxMath.ValueWithinRange(Body.position.y, Config.LowerBound, Config.UpperBound));
        AddTransition(escapeLeftBoundUpOrDown, escapeLowerBound, () => Body.position.y < Config.LowerBound);
        AddTransition(escapeLeftBoundUpOrDown, escapeUpperBound, () => Body.position.y > Config.UpperBound);

        AddTransition(escapeRightBoundUpOrDown, dodge, () => Body.position.x < Config.RightBound &&
                                                             AuxMath.ValueWithinRange(Body.position.y, Config.LeftBound, Config.RightBound));
        AddTransition(escapeRightBoundUpOrDown, escapeLowerBound, () => Body.position.y < Config.LowerBound);
        AddTransition(escapeRightBoundUpOrDown, escapeUpperBound, () => Body.position.y > Config.UpperBound);

        AddTransition(escapeLowerBound, dodge, () => Body.position.y > Config.LowerBound &&
                                                     AuxMath.ValueWithinRange(Body.position.x, Config.LeftBound, Config.RightBound));
        AddTransition(escapeLowerBound, escapeLeftBoundUpOrDown, () => Body.position.x < Config.LeftBound);
        AddTransition(escapeLowerBound, escapeRightBoundUpOrDown, () => Body.position.x > Config.RightBound);

        AddTransition(escapeUpperBound, dodge, () => Body.position.y < Config.UpperBound &&
                                                     AuxMath.ValueWithinRange(Body.position.x, Config.LeftBound, Config.RightBound));
        AddTransition(escapeUpperBound, escapeLeftBoundUpOrDown, () => Body.position.x < Config.LeftBound);
        AddTransition(escapeUpperBound, escapeRightBoundUpOrDown, () => Body.position.x > Config.RightBound);

        SetInitialState(dodge);
        SetDefaultState(dodge);
    }
}
