using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    #region ��ų ����
    [Header("��ų")]
    public float skillHP;
    public float skillRespiration;
    public float skillDefense;
    public float skillCamouflage;
    public float skillIntellect;

    [Header("��ų ����")]
    public int hpLevel; // ������ ü��
    public int respirationLevel; // ��ȣ��
    public int defenseLevel; // ź����
    public int camouflageLevel; // ưư�� ����
    public int intellectLevel; // ���� �н�

    [Header("��ų ����")]
    public float hpPrice;
    public float respirationPrice;
    public float defensePrice;
    public float camouflagePrice;
    public float intellectPrice;
    #endregion

    [Header("���� �ݾ�")]
    public float money = 0;

    [Header("�Ÿ�")]
    public int currentDistance = 0;
    public int bestDistance = 0;
    float distanceTimer = 0;

    [Header("���� ����")]
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
