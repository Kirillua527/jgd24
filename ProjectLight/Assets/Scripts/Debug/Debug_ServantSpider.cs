using UnityEditor.Search;
using UnityEngine;

public class Debug_ServantSpiderAttackRadius : MonoBehaviour
{
    public ServantSpiderStateMachine stateMachine;

    void OnEnable()
    {
        stateMachine = GetComponent<ServantSpiderStateMachine>();
    }

#if UNITY_EDITOR
    [Label("显示Idle时追击范围")]
#endif
    public bool showIdleChaseRadius = true;

#if UNITY_EDITOR
    [Label("显示Chase时追击范围")]
#endif
    public bool showChaseChaseRadius = true;

#if UNITY_EDITOR
    [Label("显示攻击范围")]
#endif
    public bool showAttackRadius = true;

    public void OnDrawGizmos()
    {
        if(showIdleChaseRadius)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position,stateMachine.ChaseDistance);
        }
        if(showChaseChaseRadius)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position,stateMachine.MaxChaseDistance);
        }
        if(showAttackRadius)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position,stateMachine.AttackRadius);
        }
    }
}