using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class SkillWindow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public enum EskillWindow
    {
        Top,
        Among,
        Bottom,
    }
    public EskillWindow eskillWindow;

    public GameObject skillWindow;

    [Header("¿ÞÂÊ")]
    [SerializeField] Image selectBarTop;
    [SerializeField] Image mouseRangeTop;
    [SerializeField] GameObject windowTop;
    [SerializeField] GameObject barUpTop;
    [SerializeField] GameObject barDownTop;
    [SerializeField] RectTransform rectSkillTop;

    [Header("°¡¿îµ¥")]
    [SerializeField] Image selectBarAmong;
    [SerializeField] Image mouseRangeAmong;
    [SerializeField] GameObject windowAmong;
    [SerializeField] GameObject barUpAmong;
    [SerializeField] GameObject barDownAmong;
    [SerializeField] RectTransform rectSkillAmong;

    [Header("¿À¸¥ÂÊ")]
    [SerializeField] Image selectBarBottom;
    [SerializeField] Image mouseRangeBottom;
    [SerializeField] GameObject windowBottom;
    [SerializeField] GameObject barUpBottom;
    [SerializeField] GameObject barDownBottom;
    [SerializeField] RectTransform rectSkillBottom;

    public const int windowWidth = 545;
    public const int windowHeight = 845;
    public const float barSpeed = 0.4f;

    float timer = 0.0f;
    bool isOpenCheck = false;

    void Start()
    {
        StartCoroutine(SkillWindowOpen());
    }

    void Update()
    {
        timer += Time.unscaledDeltaTime * 2.5f;
    }

    IEnumerator SkillWindowOpen()
    {
        int barOpenPosY = 440;

        barUpTop.transform.DOLocalMoveY(barOpenPosY, barSpeed).SetEase(Ease.Linear).SetUpdate(true);
        barDownTop.transform.DOLocalMoveY(-barOpenPosY, barSpeed).SetEase(Ease.Linear).SetUpdate(true);

        barUpAmong.transform.DOLocalMoveY(barOpenPosY, barSpeed).SetEase(Ease.Linear).SetUpdate(true);
        barDownAmong.transform.DOLocalMoveY(-barOpenPosY, barSpeed).SetEase(Ease.Linear).SetUpdate(true);

        barUpBottom.transform.DOLocalMoveY(barOpenPosY, barSpeed).SetEase(Ease.Linear).SetUpdate(true);
        barDownBottom.transform.DOLocalMoveY(-barOpenPosY, barSpeed).SetEase(Ease.Linear).SetUpdate(true);

        while (timer < 1)
        {
            //rectSkillTop.sizeDelta = Vector2.Lerp(windowSize * Vector2.right, windowSize, timer);

            rectSkillTop.sizeDelta = new Vector2(windowWidth, Mathf.Lerp(0, windowHeight, timer));
            rectSkillAmong.sizeDelta = new Vector2(windowWidth, Mathf.Lerp(0, windowHeight, timer));
            rectSkillBottom.sizeDelta = new Vector2(windowWidth, Mathf.Lerp(0, windowHeight, timer));
            yield return null;
        }
        isOpenCheck = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isOpenCheck == true)
        {
            switch (eskillWindow)
            {
                case EskillWindow.Top:
                    selectBarTop.DOFade(1, 0.2f).SetEase(Ease.Linear).SetUpdate(true);
                    break;
                case EskillWindow.Among:
                    selectBarAmong.DOFade(1, 0.2f).SetEase(Ease.Linear).SetUpdate(true);
                    break;
                case EskillWindow.Bottom:
                    selectBarBottom.DOFade(1, 0.2f).SetEase(Ease.Linear).SetUpdate(true);
                    break;
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isOpenCheck == true)
        {
            switch (eskillWindow)
            {
                case EskillWindow.Top:
                    selectBarTop.DOFade(0, 0.2f).SetEase(Ease.Linear).SetUpdate(true);
                    break;
                case EskillWindow.Among:
                    selectBarAmong.DOFade(0, 0.2f).SetEase(Ease.Linear).SetUpdate(true);
                    break;
                case EskillWindow.Bottom:
                    selectBarBottom.DOFade(0, 0.2f).SetEase(Ease.Linear).SetUpdate(true);
                    break;
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Time.timeScale = 1;
        Player.Instance.currentExperience = 0;
        StartCoroutine(SkillWindowClick());
    }

    IEnumerator SkillWindowClick()
    {
        timer = 0;
        int barClosePosY = 35;

        mouseRangeTop.raycastTarget = false;
        mouseRangeAmong.raycastTarget = false;
        mouseRangeBottom.raycastTarget = false;

        switch (eskillWindow)
        {
            case EskillWindow.Top:
                for (int i = 0; i < SkillManager.instance.skill.Count; i++)
                {
                    if (SkillManager.instance.skill[i].skillName == SkillManager.instance.skillTop.skillName)
                        ++SkillManager.instance.skill[i].skillLevel;
                }

                switch (SkillManager.instance.skillTop.eskill)
                {
                    case SkillData.Eskill.Vitality:
                        Debug.Log("²öÁú±ä »ý¸í·Â");
                        Player.Instance.hpReductionSpeed += 0.1f;
                        break;

                    case SkillData.Eskill.Shell:
                        Debug.Log("°ÅºÏÀÌ µî²®Áú");
                        Player.Instance.defense += 2;
                        break;

                    case SkillData.Eskill.Exercise:
                        Debug.Log("±ÙÀ°¿îµ¿");
                        Player.Instance.currentHp += ((Player.Instance.maxHp * 20) / 100);
                        break;

                    case SkillData.Eskill.Predator:
                        Debug.Log("Æ÷½ÄÀÚ");
                        Player.Instance.getExperience += 0.15f;
                        break;

                    case SkillData.Eskill.SumptuousFeast:
                        Debug.Log("Áø¼ö¼ºÂù");
                        Player.Instance.getExperience += 0.2f;
                        break;

                    case SkillData.Eskill.EnergyBomb:
                        Debug.Log("¿¡³ÊÁöÅº");
                        if (SkillManager.instance.isEnergyBombClick == false)
                        {
                            Instantiate(SkillManager.instance.energyBomb, new Vector2(Player.Instance.transform.position.x, Player.Instance.transform.position.y + 0.5f), Quaternion.identity);
                            SkillManager.instance.isEnergyBombClick = true;
                        }
                        else
                            SkillManager.instance.energyBombCoolTime -= 1;
                        break;

                    case SkillData.Eskill.SlimeBomb:
                        Debug.Log("½½¶óÀÓÅº");
                        if (SkillManager.instance.isSlimeBombClick == false)
                        {
                            Instantiate(SkillManager.instance.slimeBomb, new Vector2(Player.Instance.transform.position.x, Player.Instance.transform.position.y + 0.5f), Quaternion.identity);
                            SkillManager.instance.isSlimeBombClick = true;
                        }
                        else
                            SkillManager.instance.slimeBombCoolTime -= 1;
                        break;

                    case SkillData.Eskill.BoneFestival:
                        Debug.Log("»À ÃàÁ¦");
                        break;

                    case SkillData.Eskill.Gluttonous:
                        Debug.Log("½ÄÅ½ µ¹Áø");
                        break;

                    case SkillData.Eskill.Resurrection:
                        Debug.Log("ºÎÈ°");
                        break;
                }


                #region Ã¢ ´Ý±â
                selectBarTop.DOFade(0, 0).SetEase(Ease.Linear);

                barUpTop.transform.DOLocalMoveY(barClosePosY, barSpeed).SetEase(Ease.Linear).SetUpdate(true);
                barDownTop.transform.DOLocalMoveY(-barClosePosY, barSpeed).SetEase(Ease.Linear).SetUpdate(true);

                windowAmong.transform.DOLocalMoveY(1200, 0.5f).SetUpdate(true);
                windowBottom.transform.DOLocalMoveY(1200, 0.5f).SetUpdate(true);

                while (timer < 1)
                {
                    rectSkillTop.sizeDelta = new Vector2(windowWidth, Mathf.Lerp(windowHeight, 0, timer));
                    yield return null;
                }

                barUpTop.transform.DOKill();
                barDownTop.transform.DOKill();
                windowAmong.transform.DOKill();
                windowBottom.transform.DOKill();

                Destroy(skillWindow);
                #endregion
                break;

            case EskillWindow.Among:
                for (int i = 0; i < SkillManager.instance.skill.Count; i++)
                {
                    if (SkillManager.instance.skill[i].skillName == SkillManager.instance.skillAmong.skillName)
                        ++SkillManager.instance.skill[i].skillLevel;
                }

                switch (SkillManager.instance.skillAmong.eskill)
                {
                    case SkillData.Eskill.Vitality:
                        Debug.Log("²öÁú±ä »ý¸í·Â");
                        Player.Instance.hpReductionSpeed += 0.1f;
                        break;

                    case SkillData.Eskill.Shell:
                        Debug.Log("°ÅºÏÀÌ µî²®Áú");
                        Player.Instance.defense += 2;
                        break;

                    case SkillData.Eskill.Exercise:
                        Debug.Log("±ÙÀ°¿îµ¿");
                        Player.Instance.currentHp += ((Player.Instance.maxHp * 20) / 100);
                        break;

                    case SkillData.Eskill.Predator:
                        Debug.Log("Æ÷½ÄÀÚ");
                        Player.Instance.getExperience += 0.15f;
                        break;

                    case SkillData.Eskill.SumptuousFeast:
                        Debug.Log("Áø¼ö¼ºÂù");
                        Player.Instance.getExperience += 0.2f;
                        break;

                    case SkillData.Eskill.EnergyBomb:
                        Debug.Log("¿¡³ÊÁöÅº");
                        if (SkillManager.instance.isEnergyBombClick == false)
                        {
                            SkillManager.instance.isEnergyBombClick = true;
                            Instantiate(SkillManager.instance.energyBomb, new Vector2(Player.Instance.transform.position.x, Player.Instance.transform.position.y + 0.5f), Quaternion.identity);
                        }
                        else
                            SkillManager.instance.energyBombCoolTime -= 1;
                        break;

                    case SkillData.Eskill.SlimeBomb:
                        Debug.Log("½½¶óÀÓÅº");
                        if (SkillManager.instance.isSlimeBombClick == false)
                        {
                            SkillManager.instance.isSlimeBombClick = true;
                            Instantiate(SkillManager.instance.slimeBomb, new Vector2(Player.Instance.transform.position.x, Player.Instance.transform.position.y + 0.5f), Quaternion.identity);
                        }
                        else
                            SkillManager.instance.slimeBombCoolTime -= 1;
                        break;

                    case SkillData.Eskill.BoneFestival:
                        Debug.Log("»À ÃàÁ¦");
                        break;

                    case SkillData.Eskill.Gluttonous:
                        Debug.Log("½ÄÅ½ µ¹Áø");
                        break;

                    case SkillData.Eskill.Resurrection:
                        Debug.Log("ºÎÈ°");
                        break;
                }

                #region Ã¢ ´Ý±â
                selectBarAmong.DOFade(0, 0).SetEase(Ease.Linear);

                barUpAmong.transform.DOLocalMoveY(barClosePosY, barSpeed).SetEase(Ease.Linear).SetUpdate(true);
                barDownAmong.transform.DOLocalMoveY(-barClosePosY, barSpeed).SetEase(Ease.Linear).SetUpdate(true);

                windowTop.transform.DOLocalMoveY(1200, 0.5f).SetUpdate(true);
                windowBottom.transform.DOLocalMoveY(1200, 0.5f).SetUpdate(true);

                while (timer < 1)
                {
                    rectSkillAmong.sizeDelta = new Vector2(windowWidth, Mathf.Lerp(windowHeight, 0, timer));
                    yield return null;
                }

                barUpAmong.transform.DOKill();
                barDownAmong.transform.DOKill();
                windowTop.transform.DOKill();
                windowBottom.transform.DOKill();

                Destroy(skillWindow);
                #endregion
                break;

            case EskillWindow.Bottom:
                for (int i = 0; i < SkillManager.instance.skill.Count; i++)
                {
                    if (SkillManager.instance.skill[i].skillName == SkillManager.instance.skillBottom.skillName)
                        ++SkillManager.instance.skill[i].skillLevel;
                }

                switch (SkillManager.instance.skillBottom.eskill)
                {
                    case SkillData.Eskill.Vitality:
                        Debug.Log("²öÁú±ä »ý¸í·Â");
                        Player.Instance.hpReductionSpeed += 0.1f;
                        break;

                    case SkillData.Eskill.Shell:
                        Debug.Log("°ÅºÏÀÌ µî²®Áú");
                        Player.Instance.defense += 2;
                        break;

                    case SkillData.Eskill.Exercise:
                        Debug.Log("±ÙÀ°¿îµ¿");
                        Player.Instance.currentHp += ((Player.Instance.maxHp * 20) / 100);
                        break;

                    case SkillData.Eskill.Predator:
                        Debug.Log("Æ÷½ÄÀÚ");
                        Player.Instance.getExperience += 0.15f;
                        break;

                    case SkillData.Eskill.SumptuousFeast:
                        Debug.Log("Áø¼ö¼ºÂù");
                        Player.Instance.getExperience += 0.2f;
                        break;

                    case SkillData.Eskill.EnergyBomb:
                        Debug.Log("¿¡³ÊÁöÅº");
                        if (SkillManager.instance.isEnergyBombClick == false)
                        {
                            SkillManager.instance.isEnergyBombClick = true;
                            Instantiate(SkillManager.instance.energyBomb, new Vector2(Player.Instance.transform.position.x, Player.Instance.transform.position.y + 0.5f), Quaternion.identity);
                        }
                        else
                            SkillManager.instance.energyBombCoolTime -= 1;
                        break;

                    case SkillData.Eskill.SlimeBomb:
                        Debug.Log("½½¶óÀÓÅº");
                        if (SkillManager.instance.isSlimeBombClick == false)
                        {
                            SkillManager.instance.isSlimeBombClick = true;
                            Instantiate(SkillManager.instance.slimeBomb, new Vector2(Player.Instance.transform.position.x, Player.Instance.transform.position.y + 0.5f), Quaternion.Euler(0, -180, 0));
                        }
                        else
                            SkillManager.instance.slimeBombCoolTime -= 1;
                        break;

                    case SkillData.Eskill.BoneFestival:
                        Debug.Log("»À ÃàÁ¦");
                        break;

                    case SkillData.Eskill.Gluttonous:
                        Debug.Log("½ÄÅ½ µ¹Áø");
                        break;

                    case SkillData.Eskill.Resurrection:
                        Debug.Log("ºÎÈ°");
                        break;
                }

                #region Ã¢ ´Ý±â
                selectBarBottom.DOFade(0, 0).SetEase(Ease.Linear);

                barUpBottom.transform.DOLocalMoveY(barClosePosY, barSpeed).SetEase(Ease.Linear).SetUpdate(true);
                barDownBottom.transform.DOLocalMoveY(-barClosePosY, barSpeed).SetEase(Ease.Linear).SetUpdate(true);

                windowTop.transform.DOLocalMoveY(1200, 0.5f).SetUpdate(true);
                windowAmong.transform.DOLocalMoveY(1200, 0.5f).SetUpdate(true);

                while (timer < 1)
                {
                    rectSkillBottom.sizeDelta = new Vector2(windowWidth, Mathf.Lerp(windowHeight, 0, timer));
                    yield return null;
                }

                barUpBottom.transform.DOKill();
                barDownBottom.transform.DOKill();
                windowTop.transform.DOKill();
                windowAmong.transform.DOKill();

                Destroy(skillWindow);
                #endregion
                break;
        }
    }
}