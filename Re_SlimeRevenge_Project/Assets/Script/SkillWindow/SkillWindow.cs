using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillWindow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public enum EskillWindow
    {
        Top,
        Among,
        Bottom,
    }
    public EskillWindow eskillWindow;

    public GameObject selectBarTop;
    public GameObject selectBarAmong;
    public GameObject selectBarBottom;

    void Start()
    {
        
    }

    void Update()
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        switch (eskillWindow)
        {
            case EskillWindow.Top:
                selectBarTop.SetActive(true);
                break;
            case EskillWindow.Among:
                selectBarAmong.SetActive(true);
                break;
            case EskillWindow.Bottom:
                selectBarBottom.SetActive(true);
                break;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        switch (eskillWindow)
        {
            case EskillWindow.Top:
                selectBarTop.SetActive(false);
                break;
            case EskillWindow.Among:
                selectBarAmong.SetActive(false);
                break;
            case EskillWindow.Bottom:
                selectBarBottom.SetActive(false);
                break;
        }
    }
}
