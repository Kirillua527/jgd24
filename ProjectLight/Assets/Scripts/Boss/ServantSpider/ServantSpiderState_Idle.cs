using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/ServantSpiderState/Idle", fileName = "ServantSpiderState_Idle")]
public class ServantSpiderState_Idle : ServantSpiderState
{
    [SerializeField]
    private float idleTime = 0;
    public float IdleTime => idleTime;

    [SerializeField]
    private float chaseDistance = 0;
    public float ChaseDistance => chaseDistance;

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
            Vector2 playerPos = stateMachine.GetPlayerPosition();
            float distance = (playerPos - (Vector2)stateMachine.transform.position).magnitude;
            if(distance <= ChaseDistance)
            {
                stateMachine.ChangeState(typeof(ServantSpiderState_Chase));
            }
            else
            {
                stateMachine.ChangeState(typeof(ServantSpiderState_Hanging));
            }
        }
    }

    public void Init(ServantSpiderState_Idle state)
    {
        this.animator = state.animator;
        this.stateMachine = state.stateMachine;
        this.idleTime = state.idleTime;
        this.chaseDistance = state.chaseDistance;
    }
}