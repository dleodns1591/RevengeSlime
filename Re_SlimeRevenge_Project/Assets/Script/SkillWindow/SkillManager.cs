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

    public bool isSlimeBombCheck;
    public bool isEnergyBombCheck;

    [Space(10)]
    public int skillCount;
    public GameObject resurrectionPrefab;
    public bool isResurrectionCheck = false;

    void Start()
    {
        skillCount = skill.Count;
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
            Instantiate(slimeBomb, new Vector2(Player.Instance.transform.position.x, Player.Instance.transform.position.y + 0.5f), Quaternion.Euler(0, -180, 0));
        }

        if (isEnergyBombClick == true && isEnergyBombCheck == false && EnemySpawn.instance.gameObject.transform.childCount > 0)
        {
            isEnergyBombCheck = true;
            yield return new WaitForSeconds(energyBombCoolTime);
            Instantiate(energyBomb, new Vector2(Player.Instance.transform.position.x, Player.Instance.transform.position.y + 0.5f), Quaternion.identity);
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
            int skillRandom = Random.Range(0, skillCount);

            skillUI.SkillCard(skill[skillRandom], skillIndex++);
        }
    }
}
