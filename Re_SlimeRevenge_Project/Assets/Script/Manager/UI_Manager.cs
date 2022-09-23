using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class UI_Manager : MonoBehaviour
{
    // HP : 굳건한 체력 / Respiration : 심호흡 / Defense : 탄성력 / Camouflage : 튼튼한 위장 / Intellect : 지능학습

    public static UI_Manager Inst;

    void Awake() => Inst = this;

    [Header("소지 금액")]
    public TextMeshProUGUI Money_Txt;

    [Header("스킬레벨 텍스트")]
    public TextMeshProUGUI HP_Level_Txt;
    public TextMeshProUGUI Respiration_Level_Txt;
    public TextMeshProUGUI Defense_Level_Txt; 
    public TextMeshProUGUI Camouflage_Level_Txt;
    public TextMeshProUGUI Intellect_Level_Txt; 

    [Header("스킬 가격 비용 텍스트")]
    public TextMeshProUGUI HP_Price_Txt; 
    public TextMeshProUGUI Respiration_Price_Txt; 
    public TextMeshProUGUI Defense_Price_Txt; 
    public TextMeshProUGUI Camouflage_Price_Txt;
    public TextMeshProUGUI Intellect_Price_Txt;

    [Header("스킬 내용 텍스트")]
    public TextMeshProUGUI HP_Content_Text; 
    public TextMeshProUGUI Respiration_Content_Txt; 
    public TextMeshProUGUI Defense_Content_Txt; 
    public TextMeshProUGUI Camouflage_Content_Txt; 
    public TextMeshProUGUI Intellect_Content_Txt; 

    [Header("스킬 화면")]
    public GameObject SkillWindow_obj;
    public GameObject StartBtn_obj;

    void Start()
    {

    }

    void Update()
    {
        Amount_Text();

        SkillLevel_Text();
        SkillPrice_Text();
        SkillContent_Text();
    }
    void Amount_Text() => Money_Txt.text = GameManager.Inst.Money.ToString();

    void SkillLevel_Text()
    {
        if (GameManager.Inst.HP_Level < 5) // 체력
            HP_Level_Txt.text = GameManager.Inst.HP_Level.ToString();
        else
            HP_Level_Txt.text = "Max";

        if (GameManager.Inst.Defense_Level < 10) // 탄성력
            Defense_Level_Txt.text = GameManager.Inst.Defense_Level.ToString();
        else
            Defense_Level_Txt.text = "Max";

        if (GameManager.Inst.Intellect_Level < 5) // 지능학습
            Intellect_Level_Txt.text = GameManager.Inst.Intellect_Level.ToString();
        else
            Intellect_Level_Txt.text = "Max";

        if (GameManager.Inst.Camouflage_Level < 5) // 튼튼한 위장
            Camouflage_Level_Txt.text = GameManager.Inst.Camouflage_Level.ToString();
        else
            Camouflage_Level_Txt.text = "Max";

        if (GameManager.Inst.Respiration_Level < 10) // 심호흡
            Respiration_Level_Txt.text = GameManager.Inst.Respiration_Level.ToString();
        else
            Respiration_Level_Txt.text = "Max";
    }

    void SkillPrice_Text()
    {
        if (GameManager.Inst.HP_Level < 5) // 체력
            HP_Price_Txt.text = GameManager.Inst.HP_Price.ToString();
        else
            HP_Price_Txt.text = "Max";

        if (GameManager.Inst.Defense_Level < 10) // 탄성력
            Defense_Price_Txt.text = GameManager.Inst.Defense_Price.ToString();
        else
            Defense_Price_Txt.text = "Max";

        if (GameManager.Inst.Intellect_Level < 5) // 지능학습
            Intellect_Price_Txt.text = GameManager.Inst.Intellect_Price.ToString();
        else
            Intellect_Price_Txt.text = "Max";

        if (GameManager.Inst.Camouflage_Level < 5) // 튼튼한 위장
            Camouflage_Price_Txt.text = GameManager.Inst.Camouflage_Price.ToString();
        else
            Camouflage_Price_Txt.text = "Max";

        if (GameManager.Inst.Respiration_Level < 10) // 심호흡
            Respiration_Price_Txt.text = GameManager.Inst.Respiration_Price.ToString();
        else
            Respiration_Price_Txt.text = "Max";
    }

    void SkillContent_Text()
    {
        HP_Content_Text.text = "최대 체력 : " + GameManager.Inst.Skill_HP;
        Respiration_Content_Txt.text = "체력 감소 속도 : -" + GameManager.Inst.Skill_Respiration + "%";
        Defense_Content_Txt.text = "방어력 : " + GameManager.Inst.Skill_Defense;
        Camouflage_Content_Txt.text = "뼈로부터 얻는 체력 : +" + GameManager.Inst.Skill_Camouflage + "%";
        Intellect_Content_Txt.text = "뼈로부터 얻는 경험치 : +" + GameManager.Inst.Skill_Intellect + "%";
    }

    public void GameStart_Btn()
    {
        SkillWindow_obj.transform.DOLocalMoveX(1000, 1).SetEase(Ease.InBack);
        StartBtn_obj.transform.DOLocalMoveY(-710, 1).SetEase(Ease.InBack);
    }

}
