using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/SpiderState/Move", fileName = "SpiderState_Move")]
public class SpiderState_Move : SpiderState
{
    [SerializeField]
    private float targetAreaExternalRadius = 0;
    public float TargetAreaExternalRadius => targetAreaExternalRadius;
    [SerializeField]
    private float targetAreaInnerRadius = 0;
    public float TargetAreaInnerRadius => targetAreaInnerRadius;

    private Vector2 playerPos;
    private Vector2 targetPos;

    public override void Enter()
    {
        base.Enter();
        playerPos = stateMachine.GetPlayerPosition();
        Vector2 currentPos = stateMachine.transform.position;

        // 计算目标点
        Vector2 intersectionPointWithExternalCircle = GetIntersectionPointWithCircle(playerPos, targetAreaExternalRadius, currentPos);
        Vector2 intersectionPointWithInnerCircle = GetIntersectionPointWithCircle(playerPos, targetAreaInnerRadius, currentPos);
        (Vector2, Vector2) tangnetPoints = CalculateTangent(playerPos, targetAreaExternalRadius, currentPos);
        Vector4 boundingBox = CalculateBoundingBox(intersectionPointWithExternalCircle, intersectionPointWithInnerCircle, tangnetPoints.Item1, tangnetPoints.Item2);

        targetPos = GetRandomPointInBoudingBox(boundingBox);
        /*
        while(!IsPointInRing(targetPos, playerPos, targetAreaInnerRadius, targetAreaExternalRadius) || !IsPointInQuadrilateral(targetPos, intersectionPointWithExternalCircle, playerPos, tangnetPoints.Item1, tangnetPoints.Item2))
        {
            targetPos = GetRandomPointInBoudingBox(boundingBox);
        }
        */


        // 绘制路径
        Debug.DrawLine(currentPos, intersectionPointWithExternalCircle, Color.red, 1f);
        Debug.DrawLine(currentPos, tangnetPoints.Item1, Color.green, 1f);
        Debug.DrawLine(currentPos, tangnetPoints.Item2, Color.green, 1f);
        Debug.DrawLine(currentPos, targetPos, Color.yellow, 1f);

        // 绘制包围盒
        Debug.DrawLine(new Vector2(boundingBox.x, boundingBox.z), new Vector2(boundingBox.x, boundingBox.w), Color.blue, 1f);
        Debug.DrawLine(new Vector2(boundingBox.x, boundingBox.z), new Vector2(boundingBox.y, boundingBox.z), Color.blue, 1f);
        Debug.DrawLine(new Vector2(boundingBox.x, boundingBox.w), new Vector2(boundingBox.y, boundingBox.w), Color.blue, 1f);
        Debug.DrawLine(new Vector2(boundingBox.y, boundingBox.z), new Vector2(boundingBox.y, boundingBox.w), Color.blue, 1f);
    }

    public override void Execute()
    {
        base.Execute();

        // 移动到目标点
        stateMachine.rb.MovePosition(Vector2.MoveTowards(stateMachine.transform.position, targetPos, stateMachine.moveSpeed * Time.deltaTime));

        // 判断是否到达目标点
        if(Vector2.Distance(stateMachine.transform.position, targetPos) <= 0.1f)
        {
            stateMachine.GoToNextState();
        }
    }

    private (Vector2, Vector2) CalculateTangent(Vector2 center, float radius, Vector2 point)
    {
        Vector2 pc = point - center;
        float distance = pc.magnitude;

        float tangnetLineLength = Mathf.Sqrt(distance * distance - radius * radius);

        if(distance <= radius)
        {
            Debug.LogError("Point is inside the circle");
            return (Vector2.zero, Vector2.zero);
        }
        
        float ux = (center.x - point.x) / distance;
        float uy = (center.y - point.y) / distance;

        float angle = Mathf.Asin(radius / distance);

        float q1x = ux * Mathf.Cos(angle) - uy * Mathf.Sin(angle);
        float q1y = ux * Mathf.Sin(angle) + uy * Mathf.Cos(angle);
        float q2x = ux * Mathf.Cos(-angle) - uy * Mathf.Sin(-angle);
        float q2y = ux * Mathf.Sin(-angle) + uy * Mathf.Cos(-angle);

        q1x = q1x * tangnetLineLength + point.x;
        q1y = q1y * tangnetLineLength + point.y;
        q2x = q2x * tangnetLineLength + point.x;
        q2y = q2y * tangnetLineLength + point.y;

        return (new Vector2(q1x, q1y), new Vector2(q2x, q2y));
    }

    private bool IsPointInRing(Vector2 p, Vector2 center, float innerRadius, float externalRadius)
    {
        float distance = Vector2.Distance(p, center);

        return distance >= innerRadius && distance <= externalRadius;
    }

    private bool IsPointInQuadrilateral(Vector2 p, Vector2 a, Vector2 b, Vector2 c, Vector2 d)
    {
        Vector2 ab = b - a;
        Vector2 bc = c - b;
        Vector2 cd = d - c;
        Vector2 da = a - d;

        Vector2 ap = p - a;
        Vector2 bp = p - b;
        Vector2 cp = p - c;
        Vector2 dp = p - d;

        // 计算叉乘
        float cross1 = ab.x * ap.y - ab.y * ap.x;
        float cross2 = bc.x * bp.y - bc.y * bp.x;
        float cross3 = cd.x * cp.y - cd.y * cp.x;
        float cross4 = da.x * dp.y - da.y * dp.x;

        return (cross1 >= 0 && cross2 >= 0 && cross3 >= 0 && cross4 >= 0) || (cross1 <= 0 && cross2 <= 0 && cross3 <= 0 && cross4 <= 0);
    }

    private Vector4 CalculateBoundingBox(Vector2 a, Vector2 b, Vector2 c, Vector2 d)
    {
        float minX = Mathf.Min(a.x, b.x, c.x, d.x);
        float maxX = Mathf.Max(a.x, b.x, c.x, d.x);
        float minY = Mathf.Min(a.y, b.y, c.y, d.y);
        float maxY = Mathf.Max(a.y, b.y, c.y, d.y);

        return new Vector4(minX, maxX, minY, maxY);
    }

    private Vector2 GetRandomPointInBoudingBox(Vector4 boundingBox)
    {
        float x = Random.Range(boundingBox.x, boundingBox.y);
        float y = Random.Range(boundingBox.z, boundingBox.w);

        return new Vector2(x, y);
    }

    private Vector2 GetIntersectionPointWithCircle(Vector2 center, float radius, Vector2 point)
    {
        float dx = point.x - center.x;
        float dy = point.y - center.y;
        float distance = Mathf.Sqrt(dx * dx + dy * dy);

        if(distance <= radius)
        {
            Debug.LogError("Point is inside the circle");
        }

        float angle = Mathf.Atan2(dy, dx);

        float intersectionX = center.x + radius * Mathf.Cos(angle);
        float intersectionY = center.y + radius * Mathf.Sin(angle);

        return new Vector2(intersectionX, intersectionY);
    }
}