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
    public SpriteRenderer sr;

    [SerializeField]
    private List<ServantSpiderState> states;

    public List<ServantSpiderState> AvailableStates;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        stateTable = new Dictionary<System.Type, IState>(states.Count);

        foreach (ServantSpiderState state in states)
        {
            state.Init(animator, this);
            if(state.GetType() == typeof(ServantSpiderState_Cocoon))
            {
                ServantSpiderState_Cocoon newState = ScriptableObject.CreateInstance<ServantSpiderState_Cocoon>();
                newState.Init((ServantSpiderState_Cocoon)state);
                stateTable.Add(state.GetType(), newState);
                AvailableStates.Add(newState);
            }
            else if(state.GetType() == typeof(ServantSpiderState_Idle))
            {
                ServantSpiderState_Idle newState = ScriptableObject.CreateInstance<ServantSpiderState_Idle>();
                newState.Init((ServantSpiderState_Idle)state);
                stateTable.Add(state.GetType(), newState);
                AvailableStates.Add(newState);
            }
            else if (state.GetType() == typeof(ServantSpiderState_Chase))
            {
                ServantSpiderState_Chase newState = ScriptableObject.CreateInstance<ServantSpiderState_Chase>();
                newState.Init((ServantSpiderState_Chase)state);
                stateTable.Add(state.GetType(), newState);
                AvailableStates.Add(newState);
            }
            else if (state.GetType() == typeof(ServantSpiderState_Hanging))
            {
                ServantSpiderState_Hanging newState = ScriptableObject.CreateInstance<ServantSpiderState_Hanging>();
                newState.Init((ServantSpiderState_Hanging)state);
                stateTable.Add(state.GetType(), newState);
                AvailableStates.Add(newState);
            }
        }
    }

    void Start()
    {
        SwitchOn(stateTable[typeof(ServantSpiderState_Cocoon)]);
    }

    public Vector2 GetPlayerPosition()
    {
        return GameObject.FindGameObjectWithTag("Player").transform.position;
    }
}
