using UnityEditor.Callbacks;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/ServantSpiderState/Chase", fileName = "ServantSpiderState_Chase")]
public class ServantSpiderState_Chase : ServantSpiderState
{
    [SerializeField]
    private float chaseTime = 0;
    public float ChaseTime => chaseTime;
    [SerializeField]
    private float maxChaseDistance = 0;
    public float MaxChaseDistance => maxChaseDistance;
    [SerializeField]
    private float chaseDistanceOffset = 0;
    public float ChaseDistanceOffset => chaseDistanceOffset;
    [SerializeField, ReadOnly]
    private float timer = 0;
    public float Timer { get; set; }

    private Vector2 playerPosition;
    private Vector2 targetPosition;
    public override void Enter()
    {
        base.Enter();
        timer = 0;
    }

    public override void Execute()
    {
        base.Execute();
        timer += Time.deltaTime;

        playerPosition = stateMachine.GetPlayerPosition();

        /*'
        stateMachine.rb.MovePosition(Vector2.MoveTowards(stateMachine.transform.position, CalculateTargetPosition(playerPosition, (Vector2)stateMachine.transform.position, chaseDistanceOffset), stateMachine.moveSpeed * Time.deltaTime));
        */
        
        if(timer >= ChaseTime)
        {
            playerPosition = stateMachine.GetPlayerPosition();
            float distance = (playerPosition - (Vector2)stateMachine.transform.position).magnitude;
            
            if(distance > maxChaseDistance)
            {
                stateMachine.ChangeState(typeof(ServantSpiderState_Hanging));
            }
            timer = 0;
        }
    }

    public override void FixedExecute()
    {
        base.FixedExecute();

        // 移动到目标点
        stateMachine.rb.MovePosition(Vector2.MoveTowards(stateMachine.transform.position, playerPosition, stateMachine.moveSpeed * Time.deltaTime));
    }

    private Vector2 CalculateTargetPosition(Vector2 playerPos, Vector2 currentPos, float chaseDistanceOffset)
    {
        Vector2 chaseVector = (playerPos - currentPos).normalized;
        return playerPos + chaseVector * chaseDistanceOffset;
    }

    public void Init(ServantSpiderState_Chase state)
    {
        this.animator = state.animator;
        this.stateMachine = state.stateMachine;
        this.chaseTime = state.chaseTime;
        this.maxChaseDistance = state.maxChaseDistance;
        this.chaseDistanceOffset = state.chaseDistanceOffset;
    }

    public override void PrintState()
    {
        Debug.Log(this.animator);
        Debug.Log(this.stateMachine);
        Debug.Log(this.chaseTime);
        Debug.Log(this.maxChaseDistance);
        Debug.Log(this.chaseDistanceOffset);
    }
}