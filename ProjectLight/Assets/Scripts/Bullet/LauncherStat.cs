using UnityEngine;

[CreateAssetMenu(menuName = "Bullet.LauncherStat", fileName = "New LauncherStat")]
public class LauncherStat : ScriptableObject
{

    [Header("Bullet Parameters")]
    
    [SerializeField]
    private GameObject bulletPrefab;
    public GameObject BulletPrefab => bulletPrefab;

    [SerializeField]
    private float bulletSpeed;
    public float BulletSpeed => bulletSpeed;

    [SerializeField]
    private float bulletSize;
    public float BulletSize => bulletSize;
    
    [SerializeField]
    private float bulletLifeTime;
    public float BulletLifeTime => bulletLifeTime;

    [Header("Launcher Parameters")]

    [SerializeField]
    private float initLaunchAngle;
    public float InitLaunchAngle => initLaunchAngle;

    [SerializeField]
    private int bulletQuantity;
    public int BulletQuantity => bulletQuantity;

    [SerializeField]
    private float intervalLaunchTime;
    public float IntervalLaunchTime => intervalLaunchTime;
    
    [SerializeField]
    private float intervalLaunchAngle;
    public float IntervalLaunchAngle => intervalLaunchAngle;

    [SerializeField]
    private float damageRate;
    public float DamageRate => damageRate;
}