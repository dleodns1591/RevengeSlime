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

    public int slimeBombCoolTime;
    public int energyBombCoolTime;

    public bool isSlimeBombClick;
    public bool isEnergyBombClick;

    bool isSlimeBombCheck;
    bool isEnergyBombCheck;

    void Start()
    {
        energyBombCoolTime = 10;
        slimeBombCoolTime = 7;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            AddSkill();

        StartCoroutine(SKillBomb());
    }

    private void Awake()
    {
        instance = this;
    }

    IEnumerator SKillBomb()
    {
        if (isSlimeBombClick == true && isSlimeBombCheck == false)
        {
            isSlimeBombCheck = true;
            yield return new WaitForSeconds(slimeBombCoolTime);
            Instantiate(slimeBomb, Player.Instance.transform.position, Quaternion.identity);
        }

        if (isEnergyBombClick == true && isEnergyBombCheck == false)
        {
            isEnergyBombCheck = true;
            yield return new WaitForSeconds(energyBombCoolTime);
            Instantiate(energyBomb, Player.Instance.transform.position, Quaternion.identity);
        }
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
