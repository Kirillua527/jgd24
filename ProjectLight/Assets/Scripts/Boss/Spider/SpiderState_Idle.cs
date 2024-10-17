using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/SpiderState/Idle", fileName = "SpiderState_Idle")]
public class SpiderState_Idle : SpiderState
{
    [SerializeField]
    private float idleTime = 0;
    public float IdleTime => idleTime;

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

        if(timer >= IdleTime)
        {
            stateMachine.GoToNextState();
        }
    }
}