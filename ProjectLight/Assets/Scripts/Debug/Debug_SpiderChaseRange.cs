using UnityEngine;

public class Debug_SpiderChaseRange : MonoBehaviour
{
    public SpiderState_Move spiderState_Move;

    private void OnDrawGizmos()
    {
        if(spiderState_Move != null)
        {
            Gizmos.DrawWireSphere(transform.position, spiderState_Move.TargetAreaExternalRadius);
            Gizmos.DrawWireSphere(transform.position, spiderState_Move.TargetAreaInnerRadius);
        }
    }
}