using System.Collections.Generic;
using UnityEngine;

public class OctopusStateMachine : StateMachine
{
    [Header("待机状态参数")]
    [SerializeField, Label("待机时间")]
    private float idleTime = 2f;

    [Header("状态机参数")]
    public Animator animator;
    public Rigidbody2D rb;
    public List<OctopusState> states;

    public LinkedList<OctopusState> stateList = new LinkedList<OctopusState>();
    public LinkedListNode<OctopusState> nextState;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        
        foreach(OctopusState state in states)
        {
            state.Init(animator, this);
            stateList.AddLast(state);
        }
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
