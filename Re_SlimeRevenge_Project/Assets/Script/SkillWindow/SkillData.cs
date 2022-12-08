using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillData
{
    public enum Eskill
    {
        None,
        Vitality,
        Shell,
        Exercise,
        Predator,
        SumptuousFeast,
        EnergyBomb,
        SlimeBomb,
        BoneFestival,
        Gluttonous,
        Resurrection
    }

    public string skillName;
    public string skillDescription;
    public int skillLevel;
    public Sprite skillIcon;
    public Eskill eskill;

    public SkillData(SkillData skillData)
    {
        skillName = skillData.skillName;
        skillDescription = skillData.skillDescription;
        skillLevel = skillData.skillLevel;
        skillIcon = skillData.skillIcon;
        eskill = skillData.eskill;
    }
}
