using System.Threading;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/SpiderState/Idle", fileName = "SpiderState_Idle")]
public class SpiderState_Idle : SpiderState
{
    private float timer = 0;
    public override void Enter()
    {
        base.Enter();
        stateMachine.SetCurrentStateType(SpiderStateType.Idle);
        timer = 0;
    }

    public override void Execute()
    {
        base.Execute();
        timer += Time.deltaTime;

        if(timer >= stateMachine.IdleTime)
        {
            stateMachine.GoToNextState();
        }
    }
}