using UnityEngine;

public class StateMachine : MonoBehaviour
{
    IState currentState;

    void Update()
    {
        currentState.Execute();
    }

    public void SwitchOn(IState newState)
    {
        currentState = newState;
        currentState.Enter();
    }

    public void ChangeState(IState newState)
    {
        currentState.Exit();
        SwitchOn(newState);
    }
}