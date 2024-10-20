using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombMirrorSkin : MonoBehaviour, LaserDamage
{

     // 炸弹镜面壳
     // 受到激光伤害降低生存时长
     [SerializeField] public float mirrorSkinLifeTime = 3.0f; // todo: mirrorSkin破坏程度反馈
     [SerializeField] private float lastLifeTime; 
     public float brokenSpeed = 10.0f;
     
     public void Start()
     {
          lastLifeTime = mirrorSkinLifeTime;
     }

     // LaserDamage接口
    public void OnLaserHit(int damage)
    {         
          Debug.Log("BombMirror剩余血量:" + lastLifeTime);
          lastLifeTime -= Time.deltaTime * brokenSpeed;
          if (lastLifeTime<0)
          {
               MirrorDestroy();
               
          }
    }

     private void MirrorDestroy()
     {
          MirrorBrokenEvent.CallReportMirrorBroken();
          // Destroy(gameObject); // 销毁镜面
     }
    

}
