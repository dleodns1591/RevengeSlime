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
    private bool isStateCheck = false;

    [Header("수치적 데이터")]
    public EState eState;

    public float maxHp;
    public float currentHp;
    public float hpReductionSpeed;
    public int defense;
    public int moveSpeed;
    public int experienceValue;
    public int getPlayerHP;
    public int getExperienceValue;

    public int specialAbility = 2;
    public int specialAbilityCount;



    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        PlayerState();
        StartCoroutine(PlayerSkill());
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }



    // 플레이어 이동
    void PlayerMove()
    {
        if (GameManager.instance._isStartGame == true)
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
        maxHp = GameManager.instance.skillHP;

        if (GameManager.instance._isStartGame == true && isStateCheck == false)
        {
            isStateCheck = true;

            currentHp = maxHp;
            defense = (int)GameManager.instance.skillDefense;
            hpReductionSpeed = GameManager.instance.skillRespiration;
        }
    }

    // 플레이어 특수능력
    IEnumerator PlayerSkill()
    {
        if (Input.GetKeyDown(KeyCode.Z) && specialAbilityCount > 0)
        {
            --specialAbilityCount;
            eState = EState.Skill;

            yield return new WaitForSeconds(1f);

            eState = EState.Walk;
        }
    }
}
