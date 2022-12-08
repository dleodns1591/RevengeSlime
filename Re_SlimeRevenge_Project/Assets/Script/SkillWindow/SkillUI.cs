using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillUI : MonoBehaviour
{
    [Header("哭率")]
    public TextMeshProUGUI nameTop;
    public TextMeshProUGUI levelTop;
    public TextMeshProUGUI descriptionTop;
    public Image iconTop;

    [Header("啊款单")]
    public TextMeshProUGUI nameAmong;
    public TextMeshProUGUI levelAmong;
    public TextMeshProUGUI descriptionAmong;
    public Image iconAmong;

    [Header("坷弗率")]
    public TextMeshProUGUI nameBottom;
    public TextMeshProUGUI levelBottom;
    public TextMeshProUGUI descriptionBottom;
    public Image iconBottom;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SkillCard(SkillData skillData, int skillIndex)
    {
        switch (skillIndex)
        {
            case 0:
                SkillManager.instance.skillTop = skillData;

                iconTop.sprite = SkillManager.instance.skillTop.skillIcon;
                nameTop.text = SkillManager.instance.skillTop.skillName;
                levelTop.text = "Lv. " + SkillManager.instance.skillTop.skillLevel;
                descriptionTop.text = SkillManager.instance.skillTop.skillDescription;

                break;

            case 1:
                SkillManager.instance.skillAmong = skillData;

                iconAmong.sprite = SkillManager.instance.skillAmong.skillIcon;
                nameAmong.text = SkillManager.instance.skillAmong.skillName;
                levelAmong.text = "Lv. " + SkillManager.instance.skillAmong.skillLevel;
                descriptionAmong.text = SkillManager.instance.skillAmong.skillDescription;
                break;

            case 2:
                SkillManager.instance.skillBottom = skillData;

                iconBottom.sprite = SkillManager.instance.skillBottom.skillIcon;
                nameBottom.text = SkillManager.instance.skillBottom.skillName;
                levelBottom.text = "Lv. " + SkillManager.instance.skillBottom.skillLevel;
                descriptionBottom.text = SkillManager.instance.skillBottom.skillDescription;
                break;
        }
    }
}
