
public class EnemyStateMachine
{
    public EnemyState currentState { get; private set; }

    public void Initialize(EnemyState startingState)
    {
        currentState = startingState;
        currentState.Enter();
    }

    public void ChangeState(EnemyState state)
    {
        currentState.Exit();
        currentState = state;
        currentState.Enter();
    }
}
