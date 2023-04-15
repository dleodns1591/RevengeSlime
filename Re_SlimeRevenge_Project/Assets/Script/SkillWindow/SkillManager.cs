using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;
    void Awake() => instance = this;

    public GameObject skillWindowPick;
    public List<SkillData> skill = new List<SkillData>();

    public SkillData skillTop;
    public SkillData skillAmong;
    public SkillData skillBottom;

    [Header("스킬 카드")]
    public GameObject slimeBomb;
    public GameObject energyBomb;

    public int slimeBombCoolTime = 0;
    public int energyBombCoolTime = 0;

    public bool isSlimeBombClick = false;
    public bool isEnergyBombClick = false;

    public bool isSlimeBombCheck = false;
    public bool isEnergyBombCheck = false;

    [Space(10)]
    public int skillCount = 0;
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
        StartCoroutine(SKillBomb());
    }

    IEnumerator SKillBomb()
    {
        if (isSlimeBombCheck)
        {
            isSlimeBombCheck = false;
            yield return new WaitForSeconds(slimeBombCoolTime);
            Instantiate(slimeBomb, new Vector2(Player.Instance.transform.position.x, Player.Instance.transform.position.y + 0.5f), Quaternion.Euler(0, -180, 0));

            if (EnemySpawn.instance.transform.childCount > 0)
            {
                isEnergyBombCheck = false;
                yield return new WaitForSeconds(energyBombCoolTime);
                Instantiate(energyBomb, new Vector2(Player.Instance.transform.position.x, Player.Instance.transform.position.y + 0.5f), Quaternion.identity);
            }
        }
    }

    public void AddSkill()
    {
        GameObject summon = Instantiate(skillWindowPick, Vector2.zero, Quaternion.identity, GameObject.Find("Canvas").transform);

        var skillUI = summon.GetComponent<SkillUI>();
        int skillIndex = 0;

        for (int i = 0; i < 3; i++)
        {
            int skillRandom = Random.Range(0, skillCount);
            skillUI.SkillCard(skill[skillRandom], skillIndex++);
        }
    }
}
