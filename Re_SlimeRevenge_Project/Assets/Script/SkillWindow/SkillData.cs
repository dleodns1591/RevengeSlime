using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillData
{
    public string skillName;
    public string skillDescription;
    public int skillLevel;
    public Sprite skillIcon;

    public SkillData(SkillData skillData)
    {
        skillName = skillData.skillName;
        skillDescription = skillData.skillDescription;
        skillLevel = skillData.skillLevel;
        skillIcon = skillData.skillIcon;
    }
}
