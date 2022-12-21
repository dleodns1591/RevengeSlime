using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, IListener
{
    public static GameManager instance;
    float distanceTimer = 0;

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
    private float money;

    public float _money
    {
        get { return money; }
        set { money = value; }
    }

    private bool isStartGame;

    public bool _isStartGame
    {
        get { return isStartGame; }
        set { isStartGame = value; }
    }

    private int distance;

    public int _distance
    {
        get { return distance; }
        set { distance = value; }
    }

    private int maximumdistance;

    public int _maximumdistance
    {
        get { return maximumdistance; }
        set { maximumdistance = value; }
    }

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
        distanceTimer += Time.deltaTime;

        if (isStartGame == true)
        {
            if (distanceTimer >= 0.8f)
            {
                distanceTimer = 0;
                distance += 1;
            }
        }
    }

    void Cheat()
    {
        if (Input.GetKeyDown(KeyCode.G))
            money += 10000f;
    }

    public void Event(EventType type)
    {

    }
}
