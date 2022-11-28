using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // HP : 굳건한 체력 / Respiration : 심호흡 / Defense : 탄성력 / Camouflage : 튼튼한 위장 / Intellect : 지능학습

    public static UIManager instance;

    void Awake() => instance = this;

    private const float waitTime = 0.5f;

    [Header("소지 금액")]
    [SerializeField] TextMeshProUGUI money;

    #region 스킬 텍스트
    [Header("스킬레벨 텍스트")]
    [SerializeField] TextMeshProUGUI hpLevel;
    [SerializeField] TextMeshProUGUI respirationLevel;
    [SerializeField] TextMeshProUGUI defenseLevel;
    [SerializeField] TextMeshProUGUI camouflageLevel;
    [SerializeField] TextMeshProUGUI intellectLevel;

    [Header("스킬 가격 비용 텍스트")]
    [SerializeField] TextMeshProUGUI hpPrice;
    [SerializeField] TextMeshProUGUI respirationPrice;
    [SerializeField] TextMeshProUGUI defensePrice;
    [SerializeField] TextMeshProUGUI camouflagePrice;
    [SerializeField] TextMeshProUGUI intellectPrice;

    [Header("스킬 내용 텍스트")]
    [SerializeField] TextMeshProUGUI hPContent;
    [SerializeField] TextMeshProUGUI respirationContent;
    [SerializeField] TextMeshProUGUI defenseContent;
    [SerializeField] TextMeshProUGUI camouflageContent;
    [SerializeField] TextMeshProUGUI intellectContent;
    #endregion

    #region 메인 버튼
    [Header("버튼")]
    [SerializeField] Button startBtn;

    [SerializeField] Button hpBtn;
    [SerializeField] Button respirationBtn;
    [SerializeField] Button defenseBtn;
    [SerializeField] Button camouflageBtn;
    [SerializeField] Button intellectBtn;
    #endregion

    [Header("모닥불")]
    public GameObject cameFire;

    [Header("스킬 화면")]
    [SerializeField] GameObject skillWindow;
    [SerializeField] GameObject startBtnObj;

    void Start()
    {
        MainBtns();
    }

    void Update()
    {
        Amount_Text();

        SkillLevel_Text();
        SkillPrice_Text();
        SkillContent_Text();
    }
    void Amount_Text() => money.text = GameManager.instance._money.ToString();


    #region 스킬 텍스트
    void SkillLevel_Text()
    {
        if (GameManager.instance.hpLevel < 5) // 체력
            hpLevel.text = GameManager.instance.hpLevel.ToString();
        else
            hpLevel.text = "Max";

        if (GameManager.instance.defenseLevel < 10) // 탄성력
            defenseLevel.text = GameManager.instance.defenseLevel.ToString();
        else
            defenseLevel.text = "Max";

        if (GameManager.instance.intellectLevel < 5) // 지능학습
            intellectLevel.text = GameManager.instance.intellectLevel.ToString();
        else
            intellectLevel.text = "Max";

        if (GameManager.instance.camouflageLevel < 5) // 튼튼한 위장
            camouflageLevel.text = GameManager.instance.camouflageLevel.ToString();
        else
            camouflageLevel.text = "Max";

        if (GameManager.instance.respirationLevel < 10) // 심호흡
            respirationLevel.text = GameManager.instance.respirationLevel.ToString();
        else
            respirationLevel.text = "Max";
    }

    void SkillPrice_Text()
    {
        if (GameManager.instance.hpLevel < 5) // 체력
            hpPrice.text = GameManager.instance.hpPrice.ToString();
        else
            hpPrice.text = "Max";

        if (GameManager.instance.defenseLevel < 10) // 탄성력
            defensePrice.text = GameManager.instance.defensePrice.ToString();
        else
            defensePrice.text = "Max";

        if (GameManager.instance.intellectLevel < 5) // 지능학습
            intellectPrice.text = GameManager.instance.intellectPrice.ToString();
        else
            intellectPrice.text = "Max";

        if (GameManager.instance.camouflageLevel < 5) // 튼튼한 위장
            camouflagePrice.text = GameManager.instance.camouflagePrice.ToString();
        else
            camouflagePrice.text = "Max";

        if (GameManager.instance.respirationLevel < 10) // 심호흡
            respirationPrice.text = GameManager.instance.respirationPrice.ToString();
        else
            respirationPrice.text = "Max";
    }

    void SkillContent_Text()
    {
        hPContent.text = "최대 체력 : " + GameManager.instance.skillHP;
        respirationContent.text = "체력 감소 속도 : -" + GameManager.instance.skillRespiration + "%";
        defenseContent.text = "방어력 : " + GameManager.instance.skillDefense;
        camouflageContent.text = "뼈로부터 얻는 체력 : +" + GameManager.instance.skillCamouflage + "%";
        intellectContent.text = "뼈로부터 얻는 경험치 : +" + GameManager.instance.skillIntellect + "%";
    }
    #endregion

    #region 메인 버튼
    public void MainBtns()
    {
        startBtn.onClick.AddListener(() =>
        {
            int startBtnPosY = 710;
            int cameFirePosX = 1350;
            int skillWindowPosX = 1000;

            GameManager.instance._isStartGame = true;
            cameFire.transform.DOLocalMoveX(-cameFirePosX, 2.8f).SetEase(Ease.Linear).OnComplete(() =>
            {
                Destroy(cameFire);
            });

            skillWindow.transform.DOLocalMoveX(skillWindowPosX, waitTime).SetEase(Ease.InOutSine);
            startBtnObj.transform.DOLocalMoveY(-startBtnPosY, waitTime).SetEase(Ease.InOutSine);
        });

        hpBtn.onClick.AddListener(() =>
        {
            if (GameManager.instance.hpLevel < 5 && GameManager.instance._money >= GameManager.instance.hpPrice)
            {
                GameManager.instance._money -= GameManager.instance.hpPrice;
                GameManager.instance.hpPrice += 30;
                GameManager.instance.hpLevel += 1;
            }
        });

        respirationBtn.onClick.AddListener(() =>
        {
            if (GameManager.instance.respirationLevel < 10 && GameManager.instance._money >= GameManager.instance.respirationPrice)
            {
                GameManager.instance._money -= GameManager.instance.respirationPrice;
                GameManager.instance.respirationPrice += 100;
                GameManager.instance.respirationLevel += 1;
            }
        });

        defenseBtn.onClick.AddListener(() =>
        {
            if (GameManager.instance.defenseLevel < 10 && GameManager.instance._money >= GameManager.instance.defensePrice)
            {
                GameManager.instance._money -= GameManager.instance.defensePrice;
                GameManager.instance.defensePrice += 300;
                GameManager.instance.defenseLevel += 1;
            }
        });

        camouflageBtn.onClick.AddListener(() =>
        {
            if (GameManager.instance.camouflageLevel < 5 && GameManager.instance._money >= GameManager.instance.camouflagePrice)
            {
                GameManager.instance._money -= GameManager.instance.camouflagePrice;
                GameManager.instance.camouflagePrice += 150;
                GameManager.instance.camouflageLevel += 1;
            }
        });

        intellectBtn.onClick.AddListener(() =>
        {
            if (GameManager.instance.intellectLevel < 5 && GameManager.instance._money >= GameManager.instance.intellectPrice)
            {

                GameManager.instance._money -= GameManager.instance.intellectPrice;
                GameManager.instance.intellectPrice += 150;
                GameManager.instance.intellectLevel += 1;
            }
        });
    }
    #endregion
}
