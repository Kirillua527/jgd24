using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/ServantSpiderState/Cocoon", fileName = "ServantSpiderState_Cocoon")]
public class ServantSpiderState_Cocoon : ServantSpiderState
{
    [SerializeField]
    private float cocoonTime = 0;
    public float CocconTime => cocoonTime;

    [SerializeField
#if UNITY_EDITOR
        , ReadOnly
#endif
        ]
    private float timer = 0;
    public float Timer => timer;

    public Sprite cocoonSprite;
    public Sprite spiderSprite;

    public override void Enter()
    {
        base.Enter();
        timer = 0;
        stateMachine.sr.sprite = cocoonSprite;
    }

    public override void Execute()
    {
        base.Execute();
        timer += Time.deltaTime;

        if(timer >= cocoonTime)
        {
            stateMachine.ChangeState(typeof(ServantSpiderState_Idle));
        }
    }

    public override void Exit()
    {
        base.Exit();
        stateMachine.sr.sprite = spiderSprite;
    }

    public void Init(ServantSpiderState_Cocoon state)
    {
        this.animator = state.animator;
        this.stateMachine = state.stateMachine;
        this.cocoonTime = state.cocoonTime;
        this.cocoonSprite = state.cocoonSprite;
        this.spiderSprite = state.spiderSprite;
    }
}