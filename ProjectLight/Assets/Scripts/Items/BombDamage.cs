using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface BombDamage
{
    // 挂载该接口的class都可受到炸弹伤害
    public void OnHit(int damage); 
    // 目前仅配置爆炸伤害一个属性
  
}