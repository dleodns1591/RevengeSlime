using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager Inst;

    void Awake() => Inst = this;

    [Header("소지 금액")]
    public TextMeshProUGUI Money_Txt;

    [Header("스킬레벨 텍스트")]
    public TextMeshProUGUI HP_Level_Txt; // 체력
    public TextMeshProUGUI Respiration_Level_Txt; // 심호흡
    public TextMeshProUGUI Defense_Level_Txt; // 방어력
    public TextMeshProUGUI Camouflage_Level_Txt; // 튼튼한 위장
    public TextMeshProUGUI Intellect_Level_Txt; // 지능 학습

    [Header("스킬 가격 비용 텍스트")]
    public TextMeshProUGUI HP_Price_Txt; // 체력
    public TextMeshProUGUI Respiration_Price_Txt; // 심호흡
    public TextMeshProUGUI Defense_Price_Txt; // 방어력
    public TextMeshProUGUI Camouflage_Price_Txt; // 튼튼한 위장
    public TextMeshProUGUI Intellect_Price_Txt; // 지능 학습

    [Header("스킬 내용 텍스트")]
    public TextMeshProUGUI HP_Content_Text; // 체력
    public TextMeshProUGUI Respiration_Content_Txt; // 심호흡
    public TextMeshProUGUI Defense_Content_Txt; // 방어력
    public TextMeshProUGUI Camouflage_Content_Txt; // 튼튼한 위장
    public TextMeshProUGUI Intellect_Content_Txt; // 지능 학습

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

    }
}
