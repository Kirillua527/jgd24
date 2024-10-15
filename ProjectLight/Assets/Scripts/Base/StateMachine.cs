using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    IState currentState;

    public IState CurrentState => currentState;

    protected Dictionary<System.Type, IState> stateTable;

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

    public void ChangeState(System.Type newStateType)
    {
        ChangeState(stateTable[newStateType]);
    }
}