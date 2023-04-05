using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    [SerializeField] Image touchToStart;

    void Start()
    {
        Director();
    }

    void Update()
    {
        NextScene();
    }

    void Director()
    {
        touchToStart.DOFade(0, 1).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }

    void NextScene()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchToStart.DOPause();
            SceneManager.LoadScene("Main");
        }

    }
}
