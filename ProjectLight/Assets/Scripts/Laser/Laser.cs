using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Laser : MonoBehaviour
{
     public LineRenderer lineRenderer;
    //  todo:public GameObject LaserLineRenderer; 

     // Laser Param
     private int pointCount ; 
     private Vector2 currentPosion;

     public string[] layerMasks;
     private LayerMask layerMask;

    private bool laserStatus = true;

    public int MAX_LENGTH = 1000; // todo : private
     public float OFFSET = 0.0001f; // todo : private

    private void Awake()
    {
        // LaserLineRenderer 基础属性配置

        // 碰撞关系配置
        layerMask = LayerMask.GetMask(layerMasks);


    }

    void Start()
    {   
       
    }

    void Update()
    {   
        // Test: 鼠标方向射线
        Vector2 mousePosition;
        if (Mouse.current!= null)
        {
            mousePosition = Mouse.current.position.ReadValue();
        }
        else
        {
            mousePosition = Vector2.zero;
        }

        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 0));
        Vector2 startDirection =  new Vector2(worldMousePosition.x - transform.position.x,worldMousePosition.y - transform.position.y).normalized;
        LaserRay(transform.position,startDirection);

    }


    public void LaserRay(Vector2 startPosition, Vector2 rayDirection)
    {   
         // Init lineRenderer
        pointCount = 1;
        lineRenderer.positionCount = 1;
        laserStatus = true;

        lineRenderer.SetPosition(0, transform.position);

        // Raycast Hit
        RaycastHit2D hit = Physics2D.Raycast(transform.position,rayDirection,MAX_LENGTH,layerMask);

        while(hit.collider != null && laserStatus)
        {   
            currentPosion = hit.point;

            GameObject hitObject = hit.collider.gameObject;

            lineRenderer.positionCount += 1;
  
            lineRenderer.SetPosition( pointCount++ , hit.point);

            if (hitObject.tag == "Mirror")
            {
                // 创建新射线
                rayDirection = Vector2.Reflect(rayDirection,hit.normal);
                currentPosion = hit.point + OFFSET * rayDirection;

                hit = Physics2D.Raycast(currentPosion,rayDirection,MAX_LENGTH,layerMask);

            }
            else if (hitObject.tag == "Player" | hitObject.tag == "Boss")
            {   
                OnHitPlayer(hitObject.tag);
                laserStatus = false;
                
            }
            else 
            {   
                // 销毁射线
                laserStatus = false;
            }
        }   

        if(laserStatus)
        {
            lineRenderer.positionCount += 1;
            lineRenderer.SetPosition(++pointCount, (currentPosion + rayDirection) * 1000);
        }
       
     
        //     if (hitObject.tag == "Enemy")
        //     {
        //         Debug.Log("Hit an enemy");
        //     }
   
    }

    public void OnHitPlayer(string hitTag)
    {
        Debug.Log("撞见鬼了");
    }

    
}
