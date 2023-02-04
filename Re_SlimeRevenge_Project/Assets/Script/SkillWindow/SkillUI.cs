using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class skill
{
    public string distanceName;
    public TextMeshProUGUI name;
    public TextMeshProUGUI level;
    public TextMeshProUGUI description;
    public Image Icon;
}

public class SkillUI : MonoBehaviour
{
    public static SkillUI instance;

    public List<skill> skillList = new List<skill>();

    void Start()
    {
    }

    void Update()
    {
        
    }

    private void Awake()
    {
        instance = this;
    }

    public void SkillCard(SkillData skillData, int skillIndex)
    {
        switch (skillIndex)
        {
            case 0:
                SkillManager.instance.skillTop = skillData;

                skillList[skillIndex].Icon.sprite = SkillManager.instance.skillTop.skillIcon;
                skillList[skillIndex].name.text = SkillManager.instance.skillTop.skillName;
                skillList[skillIndex].level.text = "Lv. " + SkillManager.instance.skillTop.skillLevel;
                skillList[skillIndex].description.text = SkillManager.instance.skillTop.skillDescription;
                break;

            case 1:
                SkillManager.instance.skillAmong = skillData;

                skillList[skillIndex].Icon.sprite = SkillManager.instance.skillAmong.skillIcon;
                skillList[skillIndex].name.text = SkillManager.instance.skillAmong.skillName;
                skillList[skillIndex].level.text = "Lv. " + SkillManager.instance.skillAmong.skillLevel;
                skillList[skillIndex].description.text = SkillManager.instance.skillAmong.skillDescription;
                break;

            case 2:
                SkillManager.instance.skillBottom = skillData;

                skillList[skillIndex].Icon.sprite = SkillManager.instance.skillBottom.skillIcon;
                skillList[skillIndex].name.text = SkillManager.instance.skillBottom.skillName;
                skillList[skillIndex].level.text = "Lv. " + SkillManager.instance.skillBottom.skillLevel;
                skillList[skillIndex].description.text = SkillManager.instance.skillBottom.skillDescription;
                break;
        }
    }
}
