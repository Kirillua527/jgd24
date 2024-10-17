using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "StateMachine/SpiderState/Skill", fileName = "SpiderState_Skill")]
public class SpiderState_Skill : SpiderState
{
    public List<Skill> skillList;

    [SerializeField, ReadOnly]
    private Skill skill;

    public override void Enter()
    {
        base.Enter();

        skill = skillList[Random.Range(0, skillList.Count)];
    }

    public override void Execute()
    {
        base.Execute();

        if (skill != null)
        {
            if (skill.GetType() == typeof(BulletSkill))
            {
                BulletSkill bulletSkill = (BulletSkill)skill;
                foreach (LauncherStat launcherStat in bulletSkill.launcherStats)
                {
                    Quaternion bulletRotation = bulletSkill.aimToPlayer
                        ? Quaternion.LookRotation(Vector3.forward,
                            stateMachine.GetPlayerPosition() - (Vector2)stateMachine.transform.position)
                        : Quaternion.identity;
                    GameObject launcher = Instantiate(bulletSkill.launcherPrefab, stateMachine.transform.position,
                        bulletRotation);
                    launcher.GetComponent<Launcher>().Init(launcherStat);
                }
            }
        }
        
        stateMachine.GoToNextState();
    }
}