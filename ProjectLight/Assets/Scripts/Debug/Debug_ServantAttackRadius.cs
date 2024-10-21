using UnityEngine;

public class Debug_ServantSpiderAttackRadius : MonoBehaviour
{
    public ServantSpiderState_Hanging servantSpiderState_Hanging;
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, servantSpiderState_Hanging.AttackRadius);

    }
}