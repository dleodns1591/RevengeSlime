using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    #region 스킬 관련
    [Header("스킬")]
    public float skillHP;
    public float skillRespiration;
    public float skillDefense;
    public float skillCamouflage;
    public float skillIntellect;

    [Header("스킬 레벨")]
    public int hpLevel; // 굳건한 체력
    public int respirationLevel; // 심호흡
    public int defenseLevel; // 탄성력
    public int camouflageLevel; // 튼튼한 위장
    public int intellectLevel; // 지능 학습

    [Header("스킬 가격")]
    public float hpPrice;
    public float respirationPrice;
    public float defensePrice;
    public float camouflagePrice;
    public float intellectPrice;
    #endregion

    [Header("소지 금액")]
    public float money = 0;

    [Header("거리")]
    public int currentDistance = 0;
    public int bestDistance = 0;
    float distanceTimer = 0;

    [Header("게임 시작")]
    public bool isStartGame = false;

    void Start()
    {
        hpPrice = 30;
        respirationPrice = 100;
        defensePrice = 300;
        camouflagePrice = 150;
        intellectPrice = 150;
    }

    void Update()
    {
        Cheat();
        Skill();
        Distance();
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    void Skill()
    {
        skillHP = (200 + (hpLevel * 5));
        skillRespiration = respirationLevel * 0.2f;
        skillDefense = defenseLevel;
        skillCamouflage = camouflageLevel * 5;
        skillIntellect = intellectLevel * 6;
    }

    void Distance()
    {
        if (isStartGame)
        {
            distanceTimer += Time.deltaTime;

            if (distanceTimer >= 0.8f)
            {
                distanceTimer = 0;
                currentDistance += 1;
            }
        }
    }

    void Cheat()
    {
        if (Input.GetKeyDown(KeyCode.G))
            money += 10000;

        if (Input.GetKeyDown(KeyCode.D))
            Player.Instance._currentHp = 0;
    }
}
