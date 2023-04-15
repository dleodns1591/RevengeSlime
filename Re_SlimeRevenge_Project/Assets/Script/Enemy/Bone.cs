using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bone : MonoBehaviour
{
    public enum Ebone
    {
        BigBone,
        SmallBone,
    }
    public Ebone ebone;

    public int bigBoneValue = 20;
    public int smallBoneValue = 10;

    const float bigBoneSize = 0.15f;
    const float smallBoneSize = 0.2f;

    void Start()
    {
        transform.DOScale(0, 0);

        switch (ebone)
        {
            case Ebone.BigBone:
                transform.DOScale(new Vector2(bigBoneSize, bigBoneSize), 0.5f).SetEase(Ease.Linear);
                break;
            case Ebone.SmallBone:
                transform.DOScale(new Vector2(smallBoneSize, smallBoneSize), 0.5f).SetEase(Ease.Linear);
                break;
        }
    }

    void Update()
    {
        transform.DOLocalMoveX(-11, 4).SetEase(Ease.Linear).OnComplete(() =>
        {
            transform.DOKill();
            Destroy(gameObject);
        });
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            transform.DOKill();
            switch(ebone)
            {
                case Ebone.BigBone:
                    Player.Instance._currentHp += bigBoneValue + (bigBoneValue * ((int)GameManager.instance.skillCamouflage / 100));
                    Player.Instance._currentEXP += bigBoneValue + (bigBoneValue * ((int)GameManager.instance.skillIntellect / 100));
                    break;

                case Ebone.SmallBone:
                    Player.Instance._currentHp += smallBoneValue + (smallBoneValue * ((int)GameManager.instance.skillCamouflage / 100));
                    Player.Instance._currentEXP += smallBoneValue + (smallBoneValue * ((int)GameManager.instance.skillIntellect / 100));
                    break;
            }

            Destroy(gameObject);
        }
    }
}
