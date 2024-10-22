using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossOnHit : MonoBehaviour, BombDamage, LaserDamage
{
    private int health = 100;
    public int Health
    {
        get { return health; }
        set
        {
            health = value;
            if (health <= 0)
            {
                Debug.Log("Death");
                // TODO: 广播Death事件
            }
            else
            {
                Debug.Log("OnHit");
            }
        }
    }

     [SerializeField]
    public float bombDamageRate = 1.0f;

    [SerializeField]
    public float laserDamageRate = 1.0f;

    [SerializeField]
    private float laserDamageProtectTime = 0.1f;
    private float currentLaserTime;

    void Awake()
    {
        currentLaserTime = 0;
    }

    void FixUpdate()
    {
        currentLaserTime -= Time.deltaTime;
    }

    public void OnBombHit(int damage)
    {
        Health -= (int)(damage * bombDamageRate);
        OnHit();
    }

    public void OnLaserHit(int damage)
    {
        if (currentLaserTime <= 0)
        {
             Health -= (int)(damage * laserDamageRate);
            currentLaserTime = laserDamageProtectTime;
            OnHit();
        }
    }

    public void OnHit()
    {
        Debug.Log("TODO: OnHit Animation");
    }
}
