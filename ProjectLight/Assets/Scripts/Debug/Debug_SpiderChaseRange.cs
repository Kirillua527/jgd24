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
            if ((spiderPos - (Vector2)transform.position).magnitude <= spiderState_Move.TargetAreaExternalRadius)
            {
                spiderPos = (Vector2)transform.position + (spiderPos - (Vector2)transform.position).normalized *
                    spiderState_Move.TargetAreaExternalRadius;
            }
            (float, float) angleRange = MathTool.CalculateAngleRange(transform.position,
                spiderState_Move.TargetAreaExternalRadius, spiderPos);
            int angle = (int)(Mathf.Rad2Deg * (angleRange.Item2 - angleRange.Item1));
            Vector2 direction = (spider.transform.position - transform.position).normalized;
            GizmosTool.DrawWireSemicircle2D(transform.position, direction, spiderState_Move.TargetAreaExternalRadius, angle);
            GizmosTool.DrawWireSemicircle2D(transform.position, direction, spiderState_Move.TargetAreaInnerRadius, angle);
        }
    }
}