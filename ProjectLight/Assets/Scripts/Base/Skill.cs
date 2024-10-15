using UnityEngine;

public class Skill : ScriptableObject
{
    public int skillID;
    public string skillName;

    public float Range;

    public void Init(int id, string name)
    {
        this.skillID = id;
        this.skillName = name;
    }
}