using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // HP : ������ ü�� / Respiration : ��ȣ�� / Defense : ź���� / Camouflage : ưư�� ���� / Intellect : �����н�

    public static UIManager instance;

    void Awake() => instance = this;

    private const float waitTime = 0.5f;

    [Header("���� �ݾ�")]
    [SerializeField] TextMeshProUGUI money;

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

    [Header("�ΰ���UI")]
    [SerializeField] GameObject ingame;
    [SerializeField] TextMeshProUGUI distance;
    [SerializeField] TextMeshProUGUI ingameMoney;
    [SerializeField] Slider hpSlider;
    [SerializeField] Slider levelSlider;

    public int maxHP;
    public int currentHP;
    float sliderTimer;
    bool isHPUSe;

    [Header("Ư���ɷ�")]
    [SerializeField] Image ability;
    [SerializeField] TextMeshProUGUI abilityText;

    [SerializeField] float abilityCoolTime;
    [SerializeField] float abilityCurrentCoolTime;
    int abilityCount;
    bool isAbilityUse;

    [Header("��ں�")]
    [SerializeField] GameObject cameFire;

    [Header("��ų ȭ��")]
    [SerializeField] GameObject skillWindow;
    [SerializeField] GameObject startBtnObj;

    void Start()
    {
        MainBtns();
    }

    void Update()
    {
        distance.text = GameManager.instance._distance.ToString();

        Amount_Text();
        SpecialAbility();

        SkillLevel_Text();
        SkillPrice_Text();
        SkillContent_Text();

        StartCoroutine(SliderBar());
    }

    void Amount_Text() => money.text = GameManager.instance._money.ToString();

    IEnumerator SliderBar()
    {
        if (GameManager.instance._isStartGame == true)
        {
            sliderTimer += Time.deltaTime * 0.25f;

            if (isHPUSe == false)
            {
                isHPUSe = true;
                while (hpSlider.value > 0)
                {
                    yield return new WaitForSeconds(1f);
                    hpSlider.value -= (15 - GameManager.instance.skillRespiration);
                }
                yield break;
            }
        }
        else
        {
            hpSlider.maxValue = GameManager.instance.skillHP;
            hpSlider.value = hpSlider.maxValue;
        }

    }

    #region Ư���ɷ�
    void SpecialAbility()
    {
        abilityText.text = abilityCount.ToString();

        if (Input.GetKeyDown(KeyCode.Z) && abilityCount > 0)
        {
            --abilityCount;
        }

        if (GameManager.instance._isStartGame == true && abilityCount < 2)
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
                ++abilityCount;
            }
        }
    }

    IEnumerator CoolTime()
    {
        while (ability.fillAmount > 0)
        {
            ability.fillAmount = abilityCurrentCoolTime / abilityCoolTime;
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

    #region ��ų �ؽ�Ʈ
    void SkillLevel_Text()
    {
        if (GameManager.instance.hpLevel < 5) // ü��
            hpLevel.text = GameManager.instance.hpLevel.ToString();
        else
            hpLevel.text = "Max";

        if (GameManager.instance.defenseLevel < 10) // ź����
            defenseLevel.text = GameManager.instance.defenseLevel.ToString();
        else
            defenseLevel.text = "Max";

        if (GameManager.instance.intellectLevel < 5) // �����н�
            intellectLevel.text = GameManager.instance.intellectLevel.ToString();
        else
            intellectLevel.text = "Max";

        if (GameManager.instance.camouflageLevel < 5) // ưư�� ����
            camouflageLevel.text = GameManager.instance.camouflageLevel.ToString();
        else
            camouflageLevel.text = "Max";

        if (GameManager.instance.respirationLevel < 10) // ��ȣ��
            respirationLevel.text = GameManager.instance.respirationLevel.ToString();
        else
            respirationLevel.text = "Max";
    }

    void SkillPrice_Text()
    {
        if (GameManager.instance.hpLevel < 5) // ü��
            hpPrice.text = GameManager.instance.hpPrice.ToString();
        else
            hpPrice.text = "Max";

        if (GameManager.instance.defenseLevel < 10) // ź����
            defensePrice.text = GameManager.instance.defensePrice.ToString();
        else
            defensePrice.text = "Max";

        if (GameManager.instance.intellectLevel < 5) // �����н�
            intellectPrice.text = GameManager.instance.intellectPrice.ToString();
        else
            intellectPrice.text = "Max";

        if (GameManager.instance.camouflageLevel < 5) // ưư�� ����
            camouflagePrice.text = GameManager.instance.camouflagePrice.ToString();
        else
            camouflagePrice.text = "Max";

        if (GameManager.instance.respirationLevel < 10) // ��ȣ��
            respirationPrice.text = GameManager.instance.respirationPrice.ToString();
        else
            respirationPrice.text = "Max";
    }

    void SkillContent_Text()
    {
        hPContent.text = "�ִ� ü�� : " + GameManager.instance.skillHP;
        respirationContent.text = "ü�� ���� �ӵ� : -" + GameManager.instance.skillRespiration + "%";
        defenseContent.text = "���� : " + GameManager.instance.skillDefense;
        camouflageContent.text = "���κ��� ��� ü�� : +" + GameManager.instance.skillCamouflage + "%";
        intellectContent.text = "���κ��� ��� ����ġ : +" + GameManager.instance.skillIntellect + "%";
    }
    #endregion

    #region ���� ��ư
    public void MainBtns()
    {
        startBtn.onClick.AddListener(() =>
        {
            int startBtnPosY = 710;
            int cameFirePosX = 1350;
            int skillWindowPosX = 1000;

            GameManager.instance._isStartGame = true;

            Player.Instance.transform.DOLocalMoveX(-7, waitTime).SetEase(Ease.Linear);
            cameFire.transform.DOLocalMoveX(-cameFirePosX, 1.7f).SetEase(Ease.Linear).OnComplete(() =>
            {
                Destroy(cameFire);
            });

            // ����UI ġ���
            skillWindow.transform.DOLocalMoveX(skillWindowPosX, waitTime).SetEase(Ease.InOutSine);
            startBtnObj.transform.DOLocalMoveY(-startBtnPosY, waitTime).SetEase(Ease.InOutSine);

            // �ΰ���UI ���̱�
            for (int i = 0; i <= 2; i++)
            {
                ingame.transform.GetChild(i).DOLocalMoveY(480, 1).SetEase(Ease.Linear);
            }
            ingame.transform.GetChild(3).DOLocalMoveY(-410, 1).SetEase(Ease.Linear);


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