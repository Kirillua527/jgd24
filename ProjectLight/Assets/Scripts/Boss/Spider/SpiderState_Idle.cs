using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/SpiderState/Idle", fileName = "SpiderState_Idle")]
public class SpiderState_Idle : SpiderState
{
    [SerializeField, ReadOnly]
    private float timer = 0;
    public float Timer => timer;

    public override void Enter()
    {
        base.Enter();
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