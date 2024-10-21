using UnityEngine;

public class Debug_Spider : MonoBehaviour
{
    public SpiderStateMachine stateMachine;

#if UNITY_EDITOR
    [Label("显示移动距离")]
#endif
    public bool showMoveRange = true;

    private void OnDrawGizmos()
    {
        if(stateMachine != null)
        {
            if(showMoveRange)
            {
                Vector2 spiderPos = stateMachine.gameObject.transform.position;
            Vector2 ps = stateMachine.gameObject.transform.position - transform.position;
            Vector2 direction = ps.normalized;
            float distance = ps.magnitude;
            (float, float) externalAngleRange = MathTool.CalculateAngleRange(transform.position, stateMachine.TargetAreaExternalRadius, spiderPos);
            float oppsiteAngle = Mathf.Rad2Deg * (externalAngleRange.Item2 - externalAngleRange.Item1);
            float angle = distance <= stateMachine.TargetAreaExternalRadius 
            ? 180
            : Mathf.Clamp(oppsiteAngle, stateMachine.MinTargetAngle, stateMachine.MaxTargetAngle);
            int angleINT = (int)angle;

            GizmosTool.DrawWireSemicircle2D(transform.position, direction, stateMachine.TargetAreaExternalRadius, angleINT);
            GizmosTool.DrawWireSemicircle2D(transform.position, direction, stateMachine.TargetAreaInnerRadius, angleINT);

            
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
}