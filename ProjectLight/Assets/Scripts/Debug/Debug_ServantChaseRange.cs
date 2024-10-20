using UnityEngine;

public class Debug_ServantSpiderChaseRange : MonoBehaviour
{
    public ServantSpiderState_Idle servantSpiderState_Idle;
    public ServantSpiderState_Chase servantSpiderState_Chase;
    public GameObject[] servantSpiders;

    private void OnDrawGizmos()
    {
        if(servantSpiderState_Idle != null)
        {
            Gizmos.DrawWireSphere(transform.position, servantSpiderState_Idle.ChaseDistance);
        }
        if(servantSpiderState_Chase != null)
        {
            foreach(GameObject servantSpider in servantSpiders)
            {
                Vector2 direction = (transform.position - servantSpider.transform.position).normalized;
                Vector2 targetPos = (Vector2)transform.position + direction * servantSpiderState_Chase.ChaseDistanceOffset;

                Gizmos.color = Color.red;
                Gizmos.DrawLine(servantSpider.transform.position, targetPos);

            }
            

        }

    }
}