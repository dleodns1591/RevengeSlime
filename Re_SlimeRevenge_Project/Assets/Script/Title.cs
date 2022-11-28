using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public SpriteRenderer touchStart;

    void Start()
    {
        touchStart.DOFade(0, 1).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }

    void Update()
    {
        Next_Scene();
    }

    public void Next_Scene()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchStart.DOPause();
            SceneManager.LoadScene("Main");
        }
    }
}
