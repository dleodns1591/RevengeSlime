using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : Singleton<Player>
{
    public enum EState
    {
        Walk,
        Skill,
        Shock,
        Eat,
        Die,
    }

    public SpriteRenderer spriteRenderer;

    public EState eState;

    [Header("ü��")]
    public int maxHp = 0;
    float currentHp = 0;


    public float _currentHp
    {
        get { return currentHp; }

        set
        {
            currentHp = value;

            if (value <= 0 && GameManager.instance.isStartGame)
                eState = EState.Die;
        }
    }

    [Header("����ġ")]
    public int maxEXP = 100;
    float currentEXP = 0;

    public float _currentEXP
    {
        get { return currentEXP; }

        set
        {
            currentEXP = value;

            if (value >= maxEXP)
            {
                Time.timeScale = 0;
                value = 0;

                SkillManager.instance.AddSkill();
            }
        }
    }

    [Header("�̵�")]
    public int moveSpeed = 0;

    public int defense = 0;
    public float getHP = 0;
    public float getExperience = 0;
    public float hpReductionSpeed = 0;

    public bool isReuse = false;
    bool isDieCheck = false;

    void Start()
    {

    }

    void Update()
    {
        Die();
        Data();
        StartCoroutine(PlayerSkill());
    }

    void LateUpdate()
    {
        // �̵� ����
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4, 0), 0);
    }

    void FixedUpdate() => Move();

    // �÷��̾� �̵�
    void Move()
    {
        if (GameManager.instance.isStartGame)
        {
            if (Input.GetKey(KeyCode.Space))
                transform.Translate(0, moveSpeed * Time.deltaTime, 0);
            else
                transform.Translate(0, -moveSpeed * Time.deltaTime, 0);
        }
    }

    // �÷��̾� ������
    void Data()
    {
        if (!GameManager.instance.isStartGame)
        {
            _currentHp = maxHp;
            maxHp = (int)GameManager.instance.skillHP;
            defense = (int)GameManager.instance.skillDefense;
            hpReductionSpeed = GameManager.instance.skillRespiration;
        }
    }

    void Die()
    {
        if (eState == EState.Die)
        {
            Time.timeScale = 0;

            // ��Ȱ�� ���� ���
            if (SkillManager.instance.isResurrectionCheck)
            {
                _currentHp = maxHp / 2;
                eState = EState.Walk;
                SkillManager.instance.isResurrectionCheck = false;

                Instantiate(SkillManager.instance.resurrectionPrefab, Vector3.zero, Quaternion.identity, GameObject.Find("Canvas").transform);
            }

            // ��Ȱ�� ���� ���
            else
            {
                if (!isDieCheck)
                {
                    isDieCheck = true;
                    UIManager.instance.GameOver();

                    PlayerPrefs.SetInt("BestDistance", GameManager.instance.bestDistance);
                }
            }
        }
    }

    // �÷��̾� Ư���ɷ�
    IEnumerator PlayerSkill()
    {
        if (GameManager.instance.isStartGame)
        {
            if (Input.GetKeyDown(KeyCode.Z) && !isReuse && !UIManager.instance.isAbilityUse)
            {
                isReuse = true;
                eState = EState.Skill;

                for (int i = 0; i < EnemySpawn.instance.transform.childCount; i++)
                {
                    GameObject enemy = EnemySpawn.instance.transform.GetChild(i).gameObject;
                    var targetColider = enemy.GetComponent<BoxCollider2D>();
                    var targetSprite = enemy.GetComponent<SpriteRenderer>();
                    int boneValue = 30;

                    if (enemy.transform.position.x <= -3)
                    {
                        if (transform.position.y + enemy.transform.position.y <= 0.5f || transform.position.y - enemy.transform.position.y >= -0.5f)
                        {
                            targetColider.enabled = false;
                            _currentHp += boneValue + getHP;
                            _currentEXP += boneValue + getExperience;

                            targetSprite.DOFade(0, 0.5f);
                            enemy.transform.DOScale(new Vector2(0.1f, 0.1f), 0.5f).SetEase(Ease.Linear);
                            enemy.transform.DORotate(new Vector3(0, 0, -180), 0.5f).SetEase(Ease.Linear);
                            enemy.transform.DOLocalMove(new Vector2(transform.position.x, transform.position.y + 0.5f), 0.5f).SetEase(Ease.Linear).OnComplete(() =>
                            {
                                enemy.transform.DOKill();
                                Destroy(enemy.gameObject);
                            });
                        }
                    }
                }

                yield return new WaitForSeconds(1);

                isReuse = false;
                eState = EState.Walk;
            }
        }
    }
}
