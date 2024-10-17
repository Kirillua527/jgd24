using System.Security.AccessControl;
using Cinemachine;
using UnityEngine;
using UnityEngine.UIElements;

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
        Vector2 intersectionPointWithExternalCircle = MathTool.CalculateIntersectionPointWithCircle(playerPos, targetAreaExternalRadius, currentPos);
        Vector2 intersectionPointWithInnerCircle = MathTool.CalculateIntersectionPointWithCircle(playerPos, targetAreaInnerRadius, currentPos);

        if ((playerPos - currentPos).magnitude <= targetAreaExternalRadius)
        {
            currentPos = playerPos + (currentPos - playerPos).normalized * targetAreaExternalRadius;
        }
        
        // 计算目标点
        (float, float) externalAngleRange =
            MathTool.CalculateAngleRange(playerPos, targetAreaExternalRadius, currentPos);
        (float, float) innerAngleRange = MathTool.CalculateAngleRange(playerPos, targetAreaInnerRadius, currentPos);
        float targetAngle = UnityEngine.Random.Range(externalAngleRange.Item1, externalAngleRange.Item2);
        Debug.Log("targetAngle: " + targetAngle);
        (float, float) lengthRange = GetLengthRange(playerPos, targetAreaExternalRadius, targetAreaInnerRadius,
            currentPos, targetAngle);
        float targetLength = UnityEngine.Random.Range(lengthRange.Item1, lengthRange.Item2);
        Vector2 unitVectorPC = (playerPos - currentPos).normalized;
        // 旋转向量
        Vector2 targetVector = new Vector2(
            unitVectorPC.x * Mathf.Cos(targetAngle) - unitVectorPC.y * Mathf.Sin(targetAngle),
            unitVectorPC.x * Mathf.Sin(targetAngle) + unitVectorPC.y * Mathf.Cos(targetAngle));
        targetPos = currentPos + targetVector * targetLength;
        // Debug.Log(targetPos);
        
        // 绘制路径
        Debug.DrawLine(currentPos, currentPos + targetVector * lengthRange.Item2, Color.blue, 1f);
        Debug.DrawLine(currentPos, intersectionPointWithExternalCircle, Color.red, 1f);
        // Debug.DrawLine(currentPos, targetPos, Color.yellow, 1f);
    }

    public override void Execute()
    {
        base.Execute();

        // 移动到目标点
        // stateMachine.rb.MovePosition(Vector2.MoveTowards(stateMachine.transform.position, targetPos, stateMachine.moveSpeed * Time.deltaTime));

        stateMachine.GoToNextState();
        // 判断是否到达目标点
        if(Vector2.Distance(stateMachine.transform.position, targetPos) <= 0.1f)
        {
            stateMachine.GoToNextState();
        }
    }

    private (float, float) GetLengthRange(Vector2 center, float externalRadius, float innerRadius, Vector2 point,
        float angle)
    {
        (float, float) externalAngleRange = MathTool.CalculateAngleRange(center, externalRadius, point);
        Debug.Log("externalAngleRange: " + externalAngleRange);
        (float, float) innerAngleRange = MathTool.CalculateAngleRange(center, innerRadius, point);
        Debug.Log("innerAngleRange" + innerAngleRange);

        if (angle > innerAngleRange.Item1 && angle < innerAngleRange.Item2)
        {
            float externalLength = MathTool.CalculateDistanceToCircle(center, externalRadius, point, angle);
            float innerLength = MathTool.CalculateDistanceToCircle(center, innerRadius, point, angle);
            Debug.Log("(externalLength, innerLength): " + (externalLength, innerLength));
            return (externalLength, innerLength);
        }
        else if (angle <= innerAngleRange.Item1 && angle >= externalAngleRange.Item1 ||
                 angle >= innerAngleRange.Item2 && angle <= externalAngleRange.Item2)
        {
            float externalLength = MathTool.CalculateDistanceToCircle(center, externalRadius, point, angle);
            
            float distance = (center - point).magnitude;
            float baseAngle = Mathf.Asin(externalRadius / distance);
            float externalAngle = baseAngle - angle;
            float externalTangentLineLength = Mathf.Sqrt(distance * distance - externalRadius * externalRadius);
            float innerLength = externalTangentLineLength / Mathf.Cos(externalAngle);
            Debug.Log("(externalLength, innerLength): " + (externalLength, innerLength));
            return (externalLength, innerLength);
        }
        else
        {
            Debug.LogError("Invalid Angle:" + angle);
            return (0, 0);
        }
    }
}