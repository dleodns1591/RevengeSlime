using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Resurrection : MonoBehaviour
{
    [SerializeField] GameObject blackScreen;
    [SerializeField] Image whiteScreen;
    [SerializeField] GameObject resurrectioBar;

    void Start()
    {
        ResurrectionDirector();
    }

    void Update()
    {
    }


    void ResurrectionDirector()
    {
        resurrectioBar.transform.DORotate(new Vector2(0, 0), 1.5f).SetEase(Ease.Linear).SetUpdate(true);
        resurrectioBar.transform.DOLocalMoveY(0, 2).SetEase(Ease.OutQuad).SetUpdate(true).OnComplete(() =>
        {
            resurrectioBar.transform.DOShakePosition(5, 10).SetEase(Ease.Linear).SetUpdate(true);
            whiteScreen.DOFade(1, 1).SetEase(Ease.Linear).SetUpdate(true).OnComplete(() =>
            {
                blackScreen.SetActive(false);
                resurrectioBar.SetActive(false);

                Time.timeScale = 1;

                whiteScreen.DOFade(0, 1).SetEase(Ease.Linear).SetUpdate(true).OnComplete(() =>
                {
                    transform.DOKill();
                    resurrectioBar.transform.DOKill();
                    whiteScreen.DOKill();
                    Destroy(this.gameObject);
                });
            });
        });
    }
}
