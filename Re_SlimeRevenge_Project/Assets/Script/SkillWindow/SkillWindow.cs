using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;

public class SkillWindow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public enum EskillWindow
    {
        Top,
        Among,
        Bottom,
    }
    public EskillWindow eskillWindow;

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
    public const float barOpenSpeed = 0.46f;
    public const float barCloseSpeed = 0.4f;

    float timer = 0.0f;
    bool isOpenCheck = false;
    bool isCloseCheck = false;

    void Start()
    {
        StartCoroutine(SkillWindowOpen());
    }

    void Update()
    {
        timer += Time.deltaTime * 2.5f;
    }

    IEnumerator SkillWindowOpen()
    {
        int barOpenPosY = 440;

        barUpTop.transform.DOLocalMoveY(barOpenPosY, barOpenSpeed).SetEase(Ease.Linear);
        barDownTop.transform.DOLocalMoveY(-barOpenPosY, barOpenSpeed).SetEase(Ease.Linear);

        barUpAmong.transform.DOLocalMoveY(barOpenPosY, barOpenSpeed).SetEase(Ease.Linear);
        barDownAmong.transform.DOLocalMoveY(-barOpenPosY, barOpenSpeed).SetEase(Ease.Linear);

        barUpBottom.transform.DOLocalMoveY(barOpenPosY, barOpenSpeed).SetEase(Ease.Linear);
        barDownBottom.transform.DOLocalMoveY(-barOpenPosY, barOpenSpeed).SetEase(Ease.Linear);

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
                    selectBarTop.DOFade(1, 0.2f).SetEase(Ease.Linear);
                    break;
                case EskillWindow.Among:
                    selectBarAmong.DOFade(1, 0.2f).SetEase(Ease.Linear);
                    break;
                case EskillWindow.Bottom:
                    selectBarBottom.DOFade(1, 0.2f).SetEase(Ease.Linear);
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
                    selectBarTop.DOFade(0, 0.2f).SetEase(Ease.Linear);
                    break;
                case EskillWindow.Among:
                    selectBarAmong.DOFade(0, 0.2f).SetEase(Ease.Linear);
                    break;
                case EskillWindow.Bottom:
                    selectBarBottom.DOFade(0, 0.2f).SetEase(Ease.Linear);
                    break;
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
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

                barUpTop.transform.DOLocalMoveY(barClosePosY, barCloseSpeed).SetEase(Ease.Linear);
                barDownTop.transform.DOLocalMoveY(-barClosePosY, barCloseSpeed).SetEase(Ease.Linear);

                windowAmong.transform.DOLocalMoveY(1200, 0.5f);
                windowBottom.transform.DOLocalMoveY(1200, 0.5f);

                while (timer < 1)
                {
                    rectSkillTop.sizeDelta = new Vector2(windowWidth, Mathf.Lerp(windowHeight, 0, timer));
                    yield return null;
                }
                break;

            case EskillWindow.Among:
                selectBarAmong.DOFade(0, 0).SetEase(Ease.Linear);
                isCloseCheck = true;

                barUpAmong.transform.DOLocalMoveY(barClosePosY, barCloseSpeed).SetEase(Ease.Linear);
                barDownAmong.transform.DOLocalMoveY(-barClosePosY, barCloseSpeed).SetEase(Ease.Linear);

                windowTop.transform.DOLocalMoveY(1200, 0.5f);
                windowBottom.transform.DOLocalMoveY(1200, 0.5f);

                while (timer < 1)
                {
                    rectSkillAmong.sizeDelta = new Vector2(windowWidth, Mathf.Lerp(windowHeight, 0, timer));
                    yield return null;
                }
                break;

            case EskillWindow.Bottom:
                selectBarBottom.DOFade(0, 0).SetEase(Ease.Linear);
                isCloseCheck = true;

                barUpBottom.transform.DOLocalMoveY(barClosePosY, barCloseSpeed).SetEase(Ease.Linear);
                barDownBottom.transform.DOLocalMoveY(-barClosePosY, barCloseSpeed).SetEase(Ease.Linear);

                windowTop.transform.DOLocalMoveY(1200, 0.5f);
                windowAmong.transform.DOLocalMoveY(1200, 0.5f);

                while (timer < 1)
                {
                    rectSkillBottom.sizeDelta = new Vector2(windowWidth, Mathf.Lerp(windowHeight, 0, timer));
                    yield return null;
                }
                break;
        }
    }
}