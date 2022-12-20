using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Resurrection : MonoBehaviour
{
    [SerializeField] GameObject barUp;
    [SerializeField] GameObject barDown;
    [SerializeField] RectTransform rectSkill;

    public const int windowWidth = 545;
    public const int windowHeight = 845;
    public const float barSpeed = 0.4f;

    float timer = 0.0f;

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

        barUp.transform.DOLocalMoveY(barOpenPosY, barSpeed).SetEase(Ease.Linear).SetUpdate(true);
        barDown.transform.DOLocalMoveY(-barOpenPosY, barSpeed).SetEase(Ease.Linear).SetUpdate(true);

        while (timer < 1)
        {
            rectSkill.sizeDelta = new Vector2(windowWidth, Mathf.Lerp(0, windowHeight, timer));
            yield return null;
        }
    }
}
