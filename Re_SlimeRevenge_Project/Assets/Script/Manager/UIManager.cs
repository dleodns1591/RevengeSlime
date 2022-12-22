using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    #region 버튼
    [Header("버튼")]
    [SerializeField] Button startBtn;

    [SerializeField] Button hpBtn;
    [SerializeField] Button respirationBtn;
    [SerializeField] Button defenseBtn;
    [SerializeField] Button camouflageBtn;
    [SerializeField] Button intellectBtn;
    #endregion

    #region 인게임 UI
    [Header("인게임UI")]
    [SerializeField] GameObject ingame;

    [SerializeField] GameObject coinBackGround;
    [SerializeField] GameObject distancBackGround;
    [SerializeField] GameObject barBackGround;
    [SerializeField] GameObject skillBackGround;
    [SerializeField] GameObject skillWindowPick;
    [SerializeField] TextMeshProUGUI distance;
    [SerializeField] TextMeshProUGUI ingameMoney;
    [SerializeField] Slider hpSlider;
    [SerializeField] Slider levelSlider;
    bool isHPUSe = false;
    bool isDie = false;
    #endregion

    #region 특수능력
    [Header("특수능력")]
    [SerializeField] Image ability;
    [SerializeField] TextMeshProUGUI abilityText;

    [SerializeField] float abilityCoolTime;
    [SerializeField] float abilityCurrentCoolTime;
    bool isAbilityUse;
    #endregion

    #region 게임오버 화면
    [Header("게임오버 화면")]
    [SerializeField] GameObject gameOverWindow;
    [SerializeField] GameObject gameOverBarUp;
    [SerializeField] GameObject gameOverBarDown;
    [SerializeField] Image whiteScreen;
    [SerializeField] RectTransform rectWindow;
    [SerializeField] TextMeshProUGUI currentDistance;
    [SerializeField] TextMeshProUGUI maximumDistance;
    [SerializeField] TextMeshProUGUI myTitle;
    [SerializeField] string[] title;

    public const int windowWidth = 1670;
    public const int windowHeight = 845;
    public const float barOpenSpeed = 0.45f;
    public const float barCloseSpeed = 0.35f;
    float timer = 0.0f;
    #endregion

    [Header("모닥불")]
    [SerializeField] GameObject cameFire;

    [Header("스킬 화면")]
    [SerializeField] GameObject skillWindow;
    [SerializeField] GameObject startBtnObj;

    void Start()
    {
        MainBtns();
        Debug.Log("asdfasdf");
    }

    void Update()
    {
        if (gameOverWindow.activeSelf == true)
            timer += Time.unscaledDeltaTime * 2.5f;

        distance.text = GameManager.instance._distance.ToString();

        Amount_Text();
        SpecialAbility();

        SkillLevel_Text();
        SkillPrice_Text();
        SkillContent_Text();

        LevelBar();
        StartCoroutine(HpBar());
    }

    void Amount_Text() => money.text = GameManager.instance._money.ToString();

    #region 게임오버 화면

    public IEnumerator GameOverWindowOpen()
    {
        int barOpenPosY = 440;

        gameOverWindow.SetActive(true);

        MyTitle();

        if (GameManager.instance._distance > GameManager.instance._maximumdistance)
            GameManager.instance._maximumdistance = GameManager.instance._distance;

        currentDistance.text = GameManager.instance._distance.ToString();
        maximumDistance.text = GameManager.instance._maximumdistance.ToString();

        gameOverBarUp.transform.DOLocalMoveY(barOpenPosY, barOpenSpeed).SetEase(Ease.Linear).SetUpdate(true);
        gameOverBarDown.transform.DOLocalMoveY(-barOpenPosY, barOpenSpeed).SetEase(Ease.Linear).SetUpdate(true);

        while (timer < 1)
        {
            rectWindow.sizeDelta = new Vector2(windowWidth, Mathf.Lerp(0, windowHeight, timer));
            yield return null;
        }

        yield return new WaitForSecondsRealtime(5);

        whiteScreen.DOFade(1, 1).SetUpdate(true).OnComplete(() =>
        {
            DOTween.KillAll();
            Time.timeScale = 1;
            SceneManager.LoadScene("Main");
        });
    }

    void MyTitle()
    {
        int distance = GameManager.instance._distance;
        int score = 30;

        for (int i = 0; i < title.Length; i++)
        {
            if (distance < 1000)
            {
                if (distance < score)
                {
                    myTitle.text = title[i].ToString();
                    Debug.Log(title[i]);
                    break;
                }
                else
                {
                    if (score < 180)
                        score += 50;

                    else if (score < 240)
                        score += 60;

                    else if (score < 320)
                        score += 80;

                    else if (score < 420)
                        score += 100;

                    else if (score < 1000)
                        score = 580;
                }
            }
            else if (distance >= 1000)
                myTitle.text = title[7].ToString();

        }
    }
    #endregion

    #region 슬라이더 바

    IEnumerator HpBar()
    {
        float maxHp = Player.Instance.maxHp;
        float currentHp = Player.Instance.currentHp;

        if (GameManager.instance._isStartGame == true)
        {
            hpSlider.value = Mathf.Lerp(hpSlider.value, currentHp / maxHp, Time.deltaTime * 10);

            if (isHPUSe == false)
            {
                isHPUSe = true;
                while (hpSlider.value > 0)
                {
                    yield return new WaitForSeconds(waitTime + Player.Instance.hpReductionSpeed);
                    Player.Instance.currentHp -= 3;
                }
                yield break;
            }

            if (currentHp > maxHp)
                currentHp = maxHp;

            if (currentHp <= 0 && isDie == false)
            {
                isDie = true;
                Player.Instance.eState = Player.EState.Die;
            }
        }
    }

    void LevelBar()
    {
        levelSlider.value = Mathf.Lerp(levelSlider.value, Player.Instance.currentExperience / Player.Instance.maxExperience, Time.deltaTime * 10);
        if (Player.Instance.currentExperience >= Player.Instance.maxExperience)
        {
            Player.Instance.currentExperience = 0;
            Time.timeScale = 0;

            SkillManager.instance.AddSkill();
        }
    }

    #endregion

    #region 특수능력
    void SpecialAbility()
    {
        abilityText.text = Player.Instance.specialAbilityCount.ToString();


        if (GameManager.instance._isStartGame == true && Player.Instance.specialAbilityCount < Player.Instance.specialAbility)
        {
            if (isAbilityUse == false)
            {
                isAbilityUse = true;
                ability.fillAmount = 1;
                abilityCurrentCoolTime = abilityCoolTime;
                StartCoroutine(CoolTime());
                StartCoroutine(CoolTimeCounter());
            }

            if (abilityCurrentCoolTime == 0)
            {
                isAbilityUse = false;
                ++Player.Instance.specialAbilityCount;
            }
        }
    }

    IEnumerator CoolTime()
    {
        while (ability.fillAmount > 0)
        {
            ability.fillAmount = Mathf.Lerp(ability.fillAmount, abilityCurrentCoolTime / abilityCoolTime, Time.deltaTime * 10);
            yield return null;
        }
        yield break;
    }

    IEnumerator CoolTimeCounter()
    {
        WaitForSeconds waitSec = new WaitForSeconds(1);
        while (abilityCurrentCoolTime > 0)
        {
            yield return waitSec;
            abilityCurrentCoolTime -= 1;
        }
        yield break;
    }
    #endregion

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
        // 시작 버튼을 눌렀을 때
        startBtn.onClick.AddListener(() =>
        {
            int startBtnPosY = 710;
            int cameFirePosX = 11;
            int skillWindowPosX = 1000;

            GameManager.instance._isStartGame = true;

            Player.Instance.transform.DOLocalMoveX(-7, waitTime).SetEase(Ease.Linear);
            cameFire.transform.DOMoveX(-cameFirePosX, 1.4f).SetEase(Ease.Linear).OnComplete(() =>
            {
                Destroy(cameFire);
            });

            // 메인UI 치우기
            skillWindow.transform.DOLocalMoveX(skillWindowPosX, waitTime).SetEase(Ease.InOutSine);
            startBtnObj.transform.DOLocalMoveY(-startBtnPosY, waitTime).SetEase(Ease.InOutSine);

            // 인게임UI 보이기
            distancBackGround.transform.DOLocalMoveY(480, 1).SetEase(Ease.Linear);
            barBackGround.transform.DOLocalMoveY(480, 1).SetEase(Ease.Linear);
            coinBackGround.transform.DOLocalMoveY(480, 1).SetEase(Ease.Linear);

            skillBackGround.transform.DOLocalMoveY(-410, 1).SetEase(Ease.Linear);
        });

        // 굳건한 체력 구매 버튼을 눌렀을 때
        hpBtn.onClick.AddListener(() =>
        {
            if (GameManager.instance.hpLevel < 5 && GameManager.instance._money >= GameManager.instance.hpPrice)
            {
                GameManager.instance._money -= GameManager.instance.hpPrice;
                GameManager.instance.hpPrice += 30;
                GameManager.instance.hpLevel += 1;
            }
        });

        // 심호흡 구매 버튼을 눌렀을 때
        respirationBtn.onClick.AddListener(() =>
        {
            if (GameManager.instance.respirationLevel < 10 && GameManager.instance._money >= GameManager.instance.respirationPrice)
            {
                GameManager.instance._money -= GameManager.instance.respirationPrice;
                GameManager.instance.respirationPrice += 100;
                GameManager.instance.respirationLevel += 1;
            }
        });

        // 방어력 구매 버튼을 눌렀을 떄
        defenseBtn.onClick.AddListener(() =>
        {
            if (GameManager.instance.defenseLevel < 10 && GameManager.instance._money >= GameManager.instance.defensePrice)
            {
                GameManager.instance._money -= GameManager.instance.defensePrice;
                GameManager.instance.defensePrice += 300;
                GameManager.instance.defenseLevel += 1;
            }
        });

        // 튼튼한 위장 구매 버튼을 눌렀을 때
        camouflageBtn.onClick.AddListener(() =>
        {
            if (GameManager.instance.camouflageLevel < 5 && GameManager.instance._money >= GameManager.instance.camouflagePrice)
            {
                GameManager.instance._money -= GameManager.instance.camouflagePrice;
                GameManager.instance.camouflagePrice += 150;
                GameManager.instance.camouflageLevel += 1;
            }
        });

        // 지능학습 구매 버튼을 눌렀을 때
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
