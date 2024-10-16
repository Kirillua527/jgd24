using System.ComponentModel;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public LauncherStat launcherStat;

    public int currentLaunchQuantity;
    public float currentLaunchAngle;

    void Awake()
    {
        Init(launcherStat);
    }

    void Start()
    {
        InvokeRepeating("Launch", 0, launcherStat.IntervalLaunchTime);
    }

    void Update()
    {
        if (currentLaunchQuantity >= launcherStat.BulletQuantity)
        {
            CancelInvoke("Launch");
            Destroy(gameObject);
        }
    }

    public void Init(LauncherStat launcherStat)
    {
        this.launcherStat = launcherStat;
        currentLaunchQuantity = 0;
        currentLaunchAngle = 0;
    }

    void Launch()
    {
        // 子弹角度为发射器角度加上当前发射角度
        GameObject bulletObject = Instantiate(launcherStat.BulletPrefab, transform.position, Quaternion.identity);
        bulletObject.GetComponent<Bullet>().SetBulletStat(launcherStat);
        bulletObject.transform.localScale *= launcherStat.BulletSize;
        bulletObject.transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + launcherStat.InitLaunchAngle + currentLaunchAngle);
        Debug.Log("Launcher Angle" + transform.rotation.eulerAngles.z);
        currentLaunchQuantity++;
        currentLaunchAngle += launcherStat.IntervalLaunchAngle;
    }
}