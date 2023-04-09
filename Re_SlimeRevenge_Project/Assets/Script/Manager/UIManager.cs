using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // HP : ������ ü�� / Respiration : ��ȣ�� / Defense : ź���� / Camouflage : ưư�� ���� / Intellect : �����н�

    public static UIManager instance;

    void Awake() => instance = this;

    const float waitTime = 0.5f;

    [Header("���� �ݾ�")]
    public int currentMoney = 0;
    [SerializeField] TextMeshProUGUI mainMoney;
    [SerializeField] TextMeshProUGUI ingameMoney;

    #region ��ų �ؽ�Ʈ
    [Header("��ų���� �ؽ�Ʈ")]
    [SerializeField] TextMeshProUGUI hpLevel;
    [SerializeField] TextMeshProUGUI respirationLevel;
    [SerializeField] TextMeshProUGUI defenseLevel;
    [SerializeField] TextMeshProUGUI camouflageLevel;
    [SerializeField] TextMeshProUGUI intellectLevel;

    [Header("��ų ���� ��� �ؽ�Ʈ")]
    [SerializeField] TextMeshProUGUI hpPrice;
    [SerializeField] TextMeshProUGUI respirationPrice;
    [SerializeField] TextMeshProUGUI defensePrice;
    [SerializeField] TextMeshProUGUI camouflagePrice;
    [SerializeField] TextMeshProUGUI intellectPrice;

    [Header("��ų ���� �ؽ�Ʈ")]
    [SerializeField] TextMeshProUGUI hPContent;
    [SerializeField] TextMeshProUGUI respirationContent;
    [SerializeField] TextMeshProUGUI defenseContent;
    [SerializeField] TextMeshProUGUI camouflageContent;
    [SerializeField] TextMeshProUGUI intellectContent;
    #endregion

    #region ��ư
    [Header("��ư")]
    [SerializeField] Button startBtn;

    [SerializeField] Button hpBtn;
    [SerializeField] Button respirationBtn;
    [SerializeField] Button defenseBtn;
    [SerializeField] Button camouflageBtn;
    [SerializeField] Button intellectBtn;
    #endregion

    #region �ΰ��� UI
    [Header("�ΰ���UI")]
    [SerializeField] GameObject ingame;

    [SerializeField] GameObject coinBackGround;
    [SerializeField] GameObject distancBackGround;
    [SerializeField] GameObject barBackGround;
    [SerializeField] GameObject skillBackGround;
    [SerializeField] GameObject skillWindowPick;
    [SerializeField] TextMeshProUGUI distance;
    [SerializeField] Slider hpSlider;
    [SerializeField] Slider levelSlider;
    public bool isDie = false;
    bool isHPUSe = false;
    #endregion

    #region Ư���ɷ�
    [Header("Ư���ɷ�")]
    [SerializeField] Image ability;
    [SerializeField] TextMeshProUGUI abilityText;

    [SerializeField] float abilityCoolTime;
    [SerializeField] float abilityCurrentCoolTime;
    public bool isAbilityUse;
    #endregion

    #region ���ӿ��� ȭ��
    [Header("���ӿ��� ȭ��")]
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

    [Header("��ں�")]
    [SerializeField] GameObject cameFire;

    [Header("��ų ȭ��")]
    [SerializeField] GameObject skillWindow;
    [SerializeField] GameObject startBtnObj;

    Player player;
    GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.instance;
        player = Player.Instance;

        MainBtns();
    }

    void Update()
    {
        Distance();
        MoneyText();
        SpecialAbility();

        SkillLevel_Text();
        SkillPrice_Text();
        SkillContent_Text();

        LevelBar();
        HpBar();
    }

    void MoneyText()
    {
        mainMoney.text = gameManager._money.ToString();
        ingameMoney.text = currentMoney.ToString();
    }

    void Distance()
    {
        if (gameOverWindow.activeSelf)
            timer += Time.unscaledDeltaTime * 2.5f;

        distance.text = gameManager._distance.ToString();
    }

    #region ���ӿ��� ȭ��

    public void GameOverWindowOpen()
    {
        int barOpenPosY = 440;

        gameOverWindow.SetActive(true);

        MyTitle();

        if (gameManager._distance > gameManager._maximumdistance)
            gameManager._maximumdistance = gameManager._distance;

        currentDistance.text = gameManager._distance.ToString();
        maximumDistance.text = gameManager._maximumdistance.ToString();

        gameOverBarUp.transform.DOLocalMoveY(barOpenPosY, barOpenSpeed).SetEase(Ease.Linear).SetUpdate(true);
        gameOverBarDown.transform.DOLocalMoveY(-barOpenPosY, barOpenSpeed).SetEase(Ease.Linear).SetUpdate(true);

        rectWindow.DOSizeDelta(new Vector2(windowWidth, windowHeight), barOpenSpeed).SetEase(Ease.Linear).SetUpdate(true).OnComplete(() =>
        {
            whiteScreen.DOFade(1, 1).SetEase(Ease.Linear).SetUpdate(true).OnComplete(() =>
            {
                DOTween.KillAll();
                Time.timeScale = 1;
                GameManager.instance._isStartGame = false;
                GameManager.instance._money += currentMoney;

                SceneManager.LoadScene("Main");
            });
        });
    }

    void MyTitle()
    {
        int distance = gameManager._distance;
        int score = 30;

        for (int i = 0; i < title.Length; i++)
        {
            if (distance < 1000)
            {
                if (distance < score)
                {
                    myTitle.text = title[i].ToString();
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

    #region �����̴� ��

    void HpBar()
    {
        float maxHp = player.maxHp;
        float currentHp = player.currentHp;

        if (gameManager._isStartGame)
        {
            hpSlider.value = Mathf.Lerp(hpSlider.value, currentHp / maxHp, Time.deltaTime * 10);
            player.currentHp -= Time.deltaTime * 1.5f * (5 - player.hpReductionSpeed);

            if (currentHp > maxHp)
                currentHp = maxHp;

            if (currentHp <= 0 && !isDie)
            {
                isDie = true;
                player.eState = Player.EState.Die;
            }
        }
    }

    void LevelBar()
    {
        float currentEXP = player.currentEXP;
        int maxEXP = player.maxEXP;

        levelSlider.value = Mathf.Lerp(levelSlider.value, currentEXP / maxEXP, Time.deltaTime * 10);
        if (currentEXP >= maxEXP)
        {
            Time.timeScale = 0;
            player.currentEXP = 0;

            SkillManager.instance.AddSkill();
        }
    }

    #endregion

    #region Ư���ɷ�
    void SpecialAbility()
    {
        ability.fillAmount = Mathf.Lerp(ability.fillAmount, abilityCurrentCoolTime / abilityCoolTime, Time.deltaTime * 10);

        if (gameManager._isStartGame)
        {
            if (!isAbilityUse && Player.Instance.isReuse)
            {
                    isAbilityUse = true;
                    ability.fillAmount = 1;
                    abilityCurrentCoolTime = abilityCoolTime;

                    StartCoroutine(CoolTime());
            }

            if (abilityCurrentCoolTime == 0)
                isAbilityUse = false;
        }
    }

    IEnumerator CoolTime()
    {
        while (abilityCurrentCoolTime >= 0)
        {
            abilityCurrentCoolTime -= 1;
            yield return new WaitForSeconds(1);
        }
    }


    #endregion

    #region ��ų �ؽ�Ʈ
    void SkillLevel_Text()
    {
        if (gameManager.hpLevel < 5) // ü��
            hpLevel.text = gameManager.hpLevel.ToString();
        else
            hpLevel.text = "Max";

        if (gameManager.defenseLevel < 10) // ź����
            defenseLevel.text = gameManager.defenseLevel.ToString();
        else
            defenseLevel.text = "Max";

        if (gameManager.intellectLevel < 5) // �����н�
            intellectLevel.text = gameManager.intellectLevel.ToString();
        else
            intellectLevel.text = "Max";

        if (gameManager.camouflageLevel < 5) // ưư�� ����
            camouflageLevel.text = gameManager.camouflageLevel.ToString();
        else
            camouflageLevel.text = "Max";

        if (gameManager.respirationLevel < 10) // ��ȣ��
            respirationLevel.text = gameManager.respirationLevel.ToString();
        else
            respirationLevel.text = "Max";
    }

    void SkillPrice_Text()
    {
        if (gameManager.hpLevel < 5) // ü��
            hpPrice.text = gameManager.hpPrice.ToString();
        else
            hpPrice.text = "Max";

        if (gameManager.defenseLevel < 10) // ź����
            defensePrice.text = gameManager.defensePrice.ToString();
        else
            defensePrice.text = "Max";

        if (gameManager.intellectLevel < 5) // �����н�
            intellectPrice.text = gameManager.intellectPrice.ToString();
        else
            intellectPrice.text = "Max";

        if (gameManager.camouflageLevel < 5) // ưư�� ����
            camouflagePrice.text = gameManager.camouflagePrice.ToString();
        else
            camouflagePrice.text = "Max";

        if (gameManager.respirationLevel < 10) // ��ȣ��
            respirationPrice.text = gameManager.respirationPrice.ToString();
        else
            respirationPrice.text = "Max";
    }

    void SkillContent_Text()
    {
        hPContent.text = "�ִ� ü�� : " + gameManager.skillHP;
        respirationContent.text = "ü�� ���� �ӵ� : -" + gameManager.skillRespiration + "%";
        defenseContent.text = "���� : " + gameManager.skillDefense;
        camouflageContent.text = "���κ��� ��� ü�� : +" + gameManager.skillCamouflage + "%";
        intellectContent.text = "���κ��� ��� ����ġ : +" + gameManager.skillIntellect + "%";
    }
    #endregion

    #region ���� ��ư
    public void MainBtns()
    {
        // ���� ��ư�� ������ ��
        startBtn.onClick.AddListener(() =>
        {
            int startBtnPosY = 710;
            int cameFirePosX = 11;
            int skillWindowPosX = 1000;

            gameManager._isStartGame = true;

            Player.Instance.transform.DOLocalMoveX(-7, waitTime).SetEase(Ease.Linear); // �÷��̾� ������ �̵�
            cameFire.transform.DOMoveX(-cameFirePosX, 1.4f).SetEase(Ease.Linear).OnComplete(() => // ��ڹ� �ڷ� �̵� �� ����
            {
                Destroy(cameFire);
            });

            // ����UI ġ���
            skillWindow.transform.DOLocalMoveX(skillWindowPosX, waitTime).SetEase(Ease.InOutSine);
            startBtnObj.transform.DOLocalMoveY(-startBtnPosY, waitTime).SetEase(Ease.InOutSine);

            // �ΰ���UI ���̱�
            distancBackGround.transform.DOLocalMoveY(480, 1).SetEase(Ease.Linear);
            barBackGround.transform.DOLocalMoveY(480, 1).SetEase(Ease.Linear);
            coinBackGround.transform.DOLocalMoveY(480, 1).SetEase(Ease.Linear);

            skillBackGround.transform.DOLocalMoveY(-410, 1).SetEase(Ease.Linear);
        });

        // ������ ü�� ���� ��ư�� ������ ��
        hpBtn.onClick.AddListener(() =>
        {
            if (gameManager.hpLevel < 5 && gameManager._money >= gameManager.hpPrice)
            {
                gameManager._money -= gameManager.hpPrice;
                gameManager.hpPrice += 30;
                gameManager.hpLevel += 1;
            }
        });

        // ��ȣ�� ���� ��ư�� ������ ��
        respirationBtn.onClick.AddListener(() =>
        {
            if (gameManager.respirationLevel < 10 && gameManager._money >= gameManager.respirationPrice)
            {
                gameManager._money -= gameManager.respirationPrice;
                gameManager.respirationPrice += 100;
                gameManager.respirationLevel += 1;
            }
        });

        // ���� ���� ��ư�� ������ ��
        defenseBtn.onClick.AddListener(() =>
        {
            if (gameManager.defenseLevel < 10 && gameManager._money >= gameManager.defensePrice)
            {
                gameManager._money -= gameManager.defensePrice;
                gameManager.defensePrice += 300;
                gameManager.defenseLevel += 1;
            }
        });

        // ưư�� ���� ���� ��ư�� ������ ��
        camouflageBtn.onClick.AddListener(() =>
        {
            if (gameManager.camouflageLevel < 5 && gameManager._money >= gameManager.camouflagePrice)
            {
                gameManager._money -= gameManager.camouflagePrice;
                gameManager.camouflagePrice += 150;
                gameManager.camouflageLevel += 1;
            }
        });

        // �����н� ���� ��ư�� ������ ��
        intellectBtn.onClick.AddListener(() =>
        {
            if (gameManager.intellectLevel < 5 && gameManager._money >= gameManager.intellectPrice)
            {

                gameManager._money -= gameManager.intellectPrice;
                gameManager.intellectPrice += 150;
                gameManager.intellectLevel += 1;
            }
        });
    }
    #endregion
}
