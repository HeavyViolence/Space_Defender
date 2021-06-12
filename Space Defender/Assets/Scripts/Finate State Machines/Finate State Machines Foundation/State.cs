public abstract class State : IState
{
    public abstract void OnStateEnter();

    public abstract void OnStateExit();

    public abstract void OnStatePhysicsUpdate();

    public abstract void OnStateUpdate();
 }
