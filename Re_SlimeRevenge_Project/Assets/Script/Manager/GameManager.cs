using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Inst;

    [Header("소지 금액")]
    public float Money;

    [Header("스킬")]
    public float Skill_HP;
    public float Skill_Respiration;
    public float Skill_Defense;
    public float Skill_Camouflage;
    public float Skill_Intellect;

    [Header("스킬 레벨")]
    public int HP_Level; // 굳건한 체력
    public int Respiration_Level; // 심호흡
    public int Defense_Level; // 탄성력
    public int Camouflage_Level; // 튼튼한 위장
    public int Intellect_Level; // 지능 학습

    [Header("스킬 가격")]
    public float HP_Price;
    public float Respiration_Price;
    public float Defense_Price;
    public float Camouflage_Price;
    public float Intellect_Price;


    void Start()
    {
        HP_Price = 30;
        Respiration_Price = 100;
        Defense_Price = 300;
        Camouflage_Price = 150;
        Intellect_Price = 150;
    }

    void Update()
    {
        Cheat();
        Skill();
    }

    private void Awake()
    {
        if (Inst == null)
        {
            Inst = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void Cheat()
    {
        if (Input.GetKeyDown(KeyCode.G))
            Money += 10000f;
    }

    private void Skill()
    {
        Skill_HP = (200 +(HP_Level * 5));
        Skill_Respiration = Respiration_Level * 0.2f;
        Skill_Defense = Defense_Level;
        Skill_Camouflage = Camouflage_Level * 5;
        Skill_Intellect = Intellect_Level * 6;
    }
}
