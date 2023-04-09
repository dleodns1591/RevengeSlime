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

    [Header("수치적 데이터")]
    public EState eState;

    public float currentHp = 0;
    public int maxHp = 0;

    public float currentEXP = 0;
    public int maxEXP = 100;

    public float hpReductionSpeed = 0;
    public int defense = 0;
    public int moveSpeed = 0;
    public float getHP = 0;
    public float getExperience = 0;

    public bool isReuse = false;
    bool isDieCheck = false;
    bool isStateCheck = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        PlayerState();
        StartCoroutine(PlayerSkill());
    }

    void LateUpdate()
    {
        // 이동 제한
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4, 0), 0);
    }

    private void FixedUpdate()
    {
        Move();
    }

    // 플레이어 이동
    void Move()
    {
        if (GameManager.instance._isStartGame)
        {
            if (Input.GetKey(KeyCode.Space))
                transform.Translate(0, moveSpeed * Time.deltaTime, 0);
            else
                transform.Translate(0, -moveSpeed * Time.deltaTime, 0);
        }
    }

    // 플레이어 데이터
    void PlayerState()
    {
        maxHp = (int)GameManager.instance.skillHP;

        if (GameManager.instance._isStartGame && !isStateCheck)
        {
            isStateCheck = true;
            currentHp = maxHp;
            defense = (int)GameManager.instance.skillDefense;
            hpReductionSpeed = GameManager.instance.skillRespiration;
        }

        if (eState == EState.Die)
        {
            Time.timeScale = 0;
            if (SkillManager.instance.isResurrectionCheck)
            {
                SkillManager.instance.isResurrectionCheck = false;
                eState = EState.Walk;

                GameObject resurrection = Instantiate(SkillManager.instance.resurrectionPrefab) as GameObject;
                resurrection.transform.SetParent(GameObject.Find("Canvas").transform, false);
            }

            else
            {
                if (!isDieCheck)
                {
                    isDieCheck = true;
                    UIManager.instance.GameOverWindowOpen();
                }
            }
        }
    }

    // 플레이어 특수능력
    IEnumerator PlayerSkill()
    {
        if (GameManager.instance._isStartGame)
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
                            currentHp += boneValue + getHP;
                            currentEXP += boneValue + getExperience;

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
