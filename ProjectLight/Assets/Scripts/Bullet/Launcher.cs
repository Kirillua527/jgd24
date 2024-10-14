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

    }

    public void Init(LauncherStat launcherStat)
    {
        this.launcherStat = launcherStat;
        currentLaunchQuantity = 0;
        currentLaunchAngle = 0;
    }

    void Launch()
    {
        GameObject bulletObject = Instantiate(launcherStat.BulletPrefab, transform.position, Quaternion.Euler(0, 0, launcherStat.InitLaunchAngle));
        bulletObject.GetComponent<Bullet>().SetBulletStat(launcherStat);
        bulletObject.transform.localScale *= launcherStat.BulletSize;
        bulletObject.transform.rotation = Quaternion.Euler(0, 0, launcherStat.InitLaunchAngle + currentLaunchAngle);
        currentLaunchQuantity++;
        currentLaunchAngle += launcherStat.IntervalLaunchAngle;
    }
}