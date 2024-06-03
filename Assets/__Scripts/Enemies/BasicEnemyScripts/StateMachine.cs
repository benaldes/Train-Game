
using UnityEngine;

public class StateMachine
{
    public State currentState { get; private set; }

    public void Initialize(State startingState)
    {
        currentState = startingState;
        currentState.Enter();
    }

    public void SwitchState(State state)
    {
        currentState.Exit();
        currentState = state;
        Debug.Log(state);
        currentState.Enter();
    }
}
