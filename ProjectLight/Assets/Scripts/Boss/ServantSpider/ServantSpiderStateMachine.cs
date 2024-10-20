using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class ServantSpiderStateMachine : StateMachine
{
    [Header("Boss属性")]
    public float moveSpeed;

    [Header("状态机参数")]
    public Animator animator;
    public Rigidbody2D rb;

    [SerializeField]
    private List<ServantSpiderState> states;

    public List<ServantSpiderState> AvailableStates;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        stateTable = new Dictionary<System.Type, IState>(states.Count);

        foreach (ServantSpiderState state in states)
        {
            state.Init(animator, this);
            ServantSpiderState newState = ScriptableObject.CreateInstance<ServantSpiderState>();
            if(state.GetType() == typeof(ServantSpiderState_Idle))
            {
                newState = ScriptableObject.CreateInstance<ServantSpiderState_Idle>();
                newState.Init((ServantSpiderState_Idle)state);
            }
            else if (state.GetType() == typeof(ServantSpiderState_Chase))
            {
                newState = ScriptableObject.CreateInstance<ServantSpiderState_Chase>();
                newState.Init((ServantSpiderState_Chase)state);
                newState.PrintState();
            }
            else if (state.GetType() == typeof(ServantSpiderState_Hanging))
            {
                newState = ScriptableObject.CreateInstance<ServantSpiderState_Hanging>();
                newState.Init((ServantSpiderState_Hanging)state);
            }
            stateTable.Add(state.GetType(), newState);
            AvailableStates.Add(newState);
        }
    }

    void Start()
    {
        SwitchOn(stateTable[typeof(ServantSpiderState_Idle)]);
    }

    public Vector2 GetPlayerPosition()
    {
        return GameObject.FindGameObjectWithTag("Player").transform.position;
    }
}
