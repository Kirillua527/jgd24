using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/ServantSpiderState/Cocoon", fileName = "ServantSpiderState_Cocoon")]
public class ServantSpiderState_Cocoon : ServantSpiderState
{
    [SerializeField
#if UNITY_EDITOR
        , ReadOnly
#endif
        ]
    private float timer = 0;
    public float Timer => timer;

    public override void Enter()
    {
        base.Enter();
        timer = 0;
        stateMachine.sr.sprite = stateMachine.cocoonSprite;
    }

    public override void Execute()
    {
        base.Execute();
        timer += Time.deltaTime;

        if(timer >= stateMachine.CocoonTime)
        {
            stateMachine.ChangeState(typeof(ServantSpiderState_Idle));
        }
    }

    public override void Exit()
    {
        base.Exit();
        stateMachine.sr.sprite = stateMachine.spiderSprite;
    }
}