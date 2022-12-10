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

    [Header("스킬 카드")]
    public GameObject slimeBomb;
    public GameObject energyBomb;

    public bool isSlimeBombCheck;
    public bool isEnergyBombCheck;

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            AddSkill();

    }

    private void Awake()
    {
        instance = this;
    }

    public void AddSkill()
    {
        GameObject summon = Instantiate(skillWindowPick) as GameObject;
        summon.transform.SetParent(GameObject.Find("Canvas").transform, false);

        var skillUI = summon.GetComponent<SkillUI>();
        int skillIndex = 0;

        for (int i = 0; i < 3; i++)
        {
            int skillRandom = Random.Range(0, skill.Count);

            skillUI.SkillCard(skill[skillRandom], skillIndex++);
        }
    }
}
