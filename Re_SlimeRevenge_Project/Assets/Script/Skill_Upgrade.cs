using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Upgrade : MonoBehaviour
{
    public enum Skill_LIst
    {
        HP, // 체력
        Respiration, // 심호흡
        Defense, // 방어력
        Camouflage, // 튼튼한 위장
        Intellect // 지능 학습
    }
    public Skill_LIst skill_List;


    void Start()
    {

    }

    void Update()
    {

    }

    public void Upgrade_Click()
    {
        switch (skill_List)
        {
            case Skill_LIst.HP: // 굳건한 체력
                if (GameManager.Inst.HP_Level < 5 && GameManager.Inst.Money >= GameManager.Inst.HP_Price)
                {
                    GameManager.Inst.Money -= GameManager.Inst.HP_Price;
                    GameManager.Inst.HP_Price += 30;
                    GameManager.Inst.HP_Level += 1;
                }
                else
                {

                }
                break;

            case Skill_LIst.Respiration: // 심호흡
                if (GameManager.Inst.Respiration_Level < 10 && GameManager.Inst.Money >= GameManager.Inst.Respiration_Price)
                {
                    GameManager.Inst.Money -= GameManager.Inst.Respiration_Price;
                    GameManager.Inst.Respiration_Price += 100;
                    GameManager.Inst.Respiration_Level += 1;
                }
                else
                {

                }
                break;

            case Skill_LIst.Defense: // 탄성력
                if (GameManager.Inst.Defense_Level < 10 && GameManager.Inst.Money >= GameManager.Inst.Defense_Price)
                {
                    GameManager.Inst.Money -= GameManager.Inst.Defense_Price;
                    GameManager.Inst.Defense_Price += 300;
                    GameManager.Inst.Defense_Level += 1;
                }
                else
                {

                }
                break;

            case Skill_LIst.Camouflage: // 튼튼한 위장
                if (GameManager.Inst.Camouflage_Level < 5 && GameManager.Inst.Money >= GameManager.Inst.Camouflage_Price)
                {
                    GameManager.Inst.Money -= GameManager.Inst.Camouflage_Price;
                    GameManager.Inst.Camouflage_Price += 150;
                    GameManager.Inst.Camouflage_Level += 1;
                }
                else
                {
                    
                }
                break;

            case Skill_LIst.Intellect: // 지능 학습
                if (GameManager.Inst.Intellect_Level < 5 && GameManager.Inst.Money >= GameManager.Inst.Intellect_Price)
                {

                    GameManager.Inst.Money -= GameManager.Inst.Intellect_Price;
                    GameManager.Inst.Intellect_Price += 150;
                    GameManager.Inst.Intellect_Level += 1;
                }
                else
                {

                }
                break;
        }
    }
}
