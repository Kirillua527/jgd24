using UnityEngine;

public class Debug_ServantSpiderChaseRange : MonoBehaviour
{
    public ServantSpiderState_Idle servantSpiderState_Idle;

    private void OnDrawGizmos()
    {
        if(servantSpiderState_Idle != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, servantSpiderState_Idle.ChaseDistance);
        }
    }
}