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

    [Header("왼쪽")]
    public Image selectBarTop;
    public GameObject barUpTop;
    public GameObject barDownTop;
    public RectTransform rectSkillTop;

    [Header("가운데")]
    public Image selectBarAmong;
    public GameObject barUpAmong;
    public GameObject barDownAmong;
    public RectTransform rectSkillAmong;

    [Header("오른쪽")]
    public Image selectBarBottom;
    public GameObject barUpBottom;
    public GameObject barDownBottom;
    public RectTransform rectSkillBottom;

    public Vector2 windowSize;
    public const int windowWidth = 545;
    public const int windowHeight = 845;

    float timer = 0.0f;
    float barSpeed = 0.46f;

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

        barUpTop.transform.DOLocalMoveY(barOpenPosY, barSpeed).SetEase(Ease.Linear);
        barDownTop.transform.DOLocalMoveY(-barOpenPosY, barSpeed).SetEase(Ease.Linear);

        barUpAmong.transform.DOLocalMoveY(barOpenPosY, barSpeed).SetEase(Ease.Linear);
        barDownAmong.transform.DOLocalMoveY(-barOpenPosY, barSpeed).SetEase(Ease.Linear);

        barUpBottom.transform.DOLocalMoveY(barOpenPosY, barSpeed).SetEase(Ease.Linear);
        barDownBottom.transform.DOLocalMoveY(-barOpenPosY, barSpeed).SetEase(Ease.Linear);

        while (timer < 1)
        {
            rectSkillTop.sizeDelta = Vector2.Lerp(windowSize * Vector2.right, windowSize, timer);

            //rectSkillTop.sizeDelta = new Vector2(windowWidth, Mathf.Lerp(0, windowHeight, timer));
            //rectSkillAmong.sizeDelta = new Vector2(windowWidth, Mathf.Lerp(0, windowHeight, timer));
            //rectSkillBottom.sizeDelta = new Vector2(windowWidth, Mathf.Lerp(0, windowHeight, timer));
            yield return null;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
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

    public void OnPointerExit(PointerEventData eventData)
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

    public void OnPointerClick(PointerEventData eventData)
    {
        timer = 0;

        int barClosePosY = 35;

        switch (eskillWindow)
        {
            case EskillWindow.Top:
                selectBarTop.DOFade(0, 0).SetEase(Ease.Linear);
                        
                //                                                            Height값만 0으로 바꾸기
                rectSkillTop.sizeDelta = Vector2.Lerp(windowSize, windowSize * Vector2.right, timer); //new Vector2(windowWidth, Mathf.Lerp(windowHeight, 0, timer));

                barUpTop.transform.DOLocalMoveY(barClosePosY, barSpeed).SetEase(Ease.Linear);
                barDownTop.transform.DOLocalMoveY(-barClosePosY, barSpeed).SetEase(Ease.Linear);
                break;
            case EskillWindow.Among:
                break;
            case EskillWindow.Bottom:
                break;
        }
    }
}