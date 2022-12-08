using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    public GameObject skillWindowPick;
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
        GameObject summon = Instantiate(skillWindowPick) as GameObject;
        summon.transform.SetParent(GameObject.Find("Canvas").transform, false);

        var ui = summon.GetComponent<SkillUI>();

        int skillIndex = 0;

        for (int i = 0; i < 3; i++)
        {
            int skillRandom = Random.Range(0, skill.Count);
            ui.SkillCard(skill[skillRandom], skillIndex++);
        }
    }
}
