using System.Collections.Generic;
using UnityEngine;

public class SpiderStateMachine : StateMachine
{
    [Header("Boss属性")]
    [Header("Idle状态参数")]
    [SerializeField]
    private float idleTime = 0;
    public float IdleTime => idleTime;
    [Header("Move状态参数")]
    [SerializeField]
    private float moveSpeed = 0;
    public float MoveSpeed => moveSpeed;
    [SerializeField]
    private float targetAreaExternalRadius = 0;
    public float TargetAreaExternalRadius => targetAreaExternalRadius;
    [SerializeField]
    private float targetAreaInnerRadius = 0;
    public float TargetAreaInnerRadius => targetAreaInnerRadius;
    [SerializeField]
    private float minTargetAngle;
    public float MinTargetAngle => minTargetAngle;
    [SerializeField]
    private float maxTargetAngle;
    public float MaxTargetAngle => maxTargetAngle;

    [Header("随从数量")]
    public int maxServantAmount;

    [ReadOnly]
    public int currentServantAmonut;
    
    [Header("状态机参数")]
    public Animator animator;
    public Rigidbody2D rb;
    public List<SpiderState> states;

    public LinkedList<SpiderState> stateList = new LinkedList<SpiderState>();
    public LinkedListNode<SpiderState> nextState;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentServantAmonut = 0;

        foreach (SpiderState state in states)
        {
            state.Init(animator, this);
            stateList.AddLast(state);
        }
        nextState = stateList.First;
    }

    void Start()
    {
        SwitchOn(nextState.Value);
    }

    public void GoToNextState()
    {
        if (nextState.Next == null)
        {
            nextState = stateList.First;
        }
        else
        {
            nextState = nextState.Next;
        }
        ChangeState(nextState.Value);
    }

    public Vector2 GetPlayerPosition()
    {
        return GameObject.FindGameObjectWithTag("Player").transform.position;
    }
}