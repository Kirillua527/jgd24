using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/NewTestBossState/Skill", fileName = "NewTestState_Skill")]
public class NewTestState_Skill : NewTestState
{
    public List<Skill> skillList;
    private Skill skill;

    public override void Enter()
    {
        base.Enter();

        // 随机选择一个技能
        skill = skillList[Random.Range(0, skillList.Count)];
    }

    public override void Execute()
    {
        base.Execute();

        if (skill != null)
        {
            if (skill.GetType() == typeof(BulletSkill))
            {
                BulletSkill bulletSkill = (BulletSkill) skill;
                foreach (LauncherStat launcherStat in bulletSkill.launcherStats)
                {
                    GameObject launcher = Instantiate(bulletSkill.launcherPrefab, stateMachine.transform.position, Quaternion.identity);
                    launcher.GetComponent<Launcher>().Init(launcherStat);
                }
            }
        }
        stateMachine.GoToNextState();
    }
}