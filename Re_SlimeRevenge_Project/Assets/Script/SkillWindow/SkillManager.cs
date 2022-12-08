using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    public GameObject skillUI;
    public List<SkillData> skill = new List<SkillData>();

    public SkillData skillTop;
    public SkillData skillAmong;
    public SkillData skillBottom;

    void Start()
    {
        AddSkill();
    }

    void Update()
    {

    }

    private void Awake()
    {
        instance = this;
    }

    public void AddSkill()
    {
        var ui =  skillUI.GetComponent<SkillUI>();

        int skillIndex = 0;

        for (int i = 0; i < 3; i++)
        {
            int skillRandom = Random.Range(0, skill.Count);
            ui.SkillCard(skill[skillRandom], skillIndex++);
        }
    }
}
