using System.Resources;
using UnityEngine;

public class ServantSpiderState : ScriptableObject, IState
{
    protected Animator animator;
    protected ServantSpiderStateMachine stateMachine;
    public virtual void Enter()
    {
        
    }

    public virtual void Execute()
    {
        
    }

    public virtual void Exit()
    {
        
    }

    public void Init(Animator animator, ServantSpiderStateMachine stateMachine)
    {
        this.animator = animator;
        this.stateMachine = stateMachine;
    }

    public void Init(ServantSpiderState state)
    {
        this.animator = state.animator;
        this.stateMachine = state.stateMachine;
    }

    public void PrintState()
    {
        Debug.Log(this.animator);
        Debug.Log(this.stateMachine);
    }
}