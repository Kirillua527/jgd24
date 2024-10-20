using UnityEngine;

public class Debug_SpiderChaseRange : MonoBehaviour
{
    public SpiderState_Move spiderState_Move;
    public GameObject spider;

    private void OnDrawGizmos()
    {
        if(spiderState_Move != null)
        {
            
            Vector2 spiderPos = spider.transform.position;
            Vector2 ps = spider.transform.position - transform.position;
            Vector2 direction = ps.normalized;
            float distance = ps.magnitude;
            (float, float) externalAngleRange = MathTool.CalculateAngleRange(transform.position, spiderState_Move.TargetAreaExternalRadius, spiderPos);
            float oppsiteAngle = Mathf.Rad2Deg * (externalAngleRange.Item2 - externalAngleRange.Item1);
            float angle = distance <= spiderState_Move.TargetAreaExternalRadius 
            ? 180
            : Mathf.Clamp(oppsiteAngle, spiderState_Move.MinTargetAngle, spiderState_Move.MaxTargetAngle);
            int angleINT = (int)angle;

            GizmosTool.DrawWireSemicircle2D(transform.position, direction, spiderState_Move.TargetAreaExternalRadius, angleINT);
            GizmosTool.DrawWireSemicircle2D(transform.position, direction, spiderState_Move.TargetAreaInnerRadius, angleINT);

            
            /*
            if ((spiderPos - (Vector2)transform.position).magnitude <= spiderState_Move.TargetAreaExternalRadius)
            {
                spiderPos = (Vector2)transform.position + (spiderPos - (Vector2)transform.position).normalized *
                    spiderState_Move.TargetAreaExternalRadius * 2;
            }
            (Vector2, Vector2) tangentPoints = MathTool.CalculateTangent(transform.position, spiderState_Move.TargetAreaExternalRadius, spiderPos);
            (float, float) angleRange = MathTool.CalculateAngleRange(transform.position,
                spiderState_Move.TargetAreaExternalRadius, spiderPos);
            int angle = (int)(Mathf.Rad2Deg * (Mathf.PI - (angleRange.Item2 - angleRange.Item1)));
            Vector2 direction = (spider.transform.position - transform.position).normalized;
            GizmosTool.DrawWireSemicircle2D(transform.position, direction, spiderState_Move.TargetAreaExternalRadius, angle);
            GizmosTool.DrawWireSemicircle2D(transform.position, direction, spiderState_Move.TargetAreaInnerRadius, angle);
            Gizmos.DrawLine(tangentPoints.Item1, spiderPos);
            Gizmos.DrawLine(tangentPoints.Item2, spiderPos);
            */
        }
    }
}