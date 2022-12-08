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

    [Header("哭率")]
    public Image selectBarTop;
    public Image mouseRangeTop;
    public GameObject windowTop;
    public GameObject barUpTop;
    public GameObject barDownTop;
    public RectTransform rectSkillTop;

    [Header("啊款单")]
    public Image selectBarAmong;
    public Image mouseRangeAmong;
    public GameObject windowAmong;
    public GameObject barUpAmong;
    public GameObject barDownAmong;
    public RectTransform rectSkillAmong;

    [Header("坷弗率")]
    public Image selectBarBottom;
    public Image mouseRangeBottom;
    public GameObject windowBottom;
    public GameObject barUpBottom;
    public GameObject barDownBottom;
    public RectTransform rectSkillBottom;

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
        StartCoroutine(SkillWindowClose());
    }

    IEnumerator SkillWindowClose()
    {
        timer = 0;
        int barClosePosY = 35;

        mouseRangeTop.raycastTarget = false;
        mouseRangeAmong.raycastTarget = false;
        mouseRangeBottom.raycastTarget = false;

        switch (eskillWindow)
        {
            case EskillWindow.Top:
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
                break;

            case EskillWindow.Among:
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
                break;

            case EskillWindow.Bottom:
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
                break;
        }
    }
}