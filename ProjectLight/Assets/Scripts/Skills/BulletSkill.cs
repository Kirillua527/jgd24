using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Bullet Skill", menuName = "Skill/Bullet Skill")]
public class BulletSkill : Skill
{
    public GameObject launcherPrefab;
    public List<LauncherStat> launcherStats;

    public bool aimToPlayer = false;
}