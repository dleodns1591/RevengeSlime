using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class Title : MonoBehaviour
{
    public SpriteRenderer TouchToStart;

    void Start()
    {
        StartCoroutine(Text_Blinking_01());
    }

    void Update()
    {
        Next_Scene();
    }

    public void Next_Scene()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TouchToStart.DOPause();
            SceneManager.LoadScene("Main");
        }
    }

    public IEnumerator Text_Blinking_01()
    {
        TouchToStart.DOFade(0f, 1);
        yield return new WaitForSeconds(1f);
        StartCoroutine(Text_Blinking_02());
    }

    public IEnumerator Text_Blinking_02()
    {
        TouchToStart.DOFade(1f, 1);
        yield return new WaitForSeconds(1f);
        StartCoroutine(Text_Blinking_01());
    }

}
