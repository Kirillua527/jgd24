using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/ServantSpiderState/Hanging", fileName = "ServantSpiderState_Hanging")]
public class ServantSpiderState_Hanging : ServantSpiderState
{
    [SerializeField]
    private float attackRadius;
    public float AttackRadius => attackRadius;

    private Vector2 playerPosition;

    public override void Enter()
    {
        base.Enter();
        stateMachine.transform.localScale = 1.5f * Vector3.one;
    }

    public override void Execute()
    {
        base.Execute();

        // 移动到目标点
        playerPosition = stateMachine.GetPlayerPosition();
        float distance = ((Vector2)stateMachine.transform.position - playerPosition).magnitude;
        stateMachine.rb.MovePosition(Vector2.MoveTowards(stateMachine.transform.position, playerPosition, stateMachine.moveSpeed * Time.deltaTime));

        if(distance <= attackRadius)
        {
            stateMachine.transform.localScale = Vector3.one;
            stateMachine.ChangeState(typeof(ServantSpiderState_Idle));
        }
    }

    public void Init(ServantSpiderState_Hanging state)
    {
        this.animator = state.animator;
        this.stateMachine = state.stateMachine;
        this.attackRadius = state.attackRadius;
    }

}