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

    const float bigBoneSize = 0.15f;
    const float smallBoneSize = 0.2f;

    void Start()
    {
        transform.DOScale(0, 0);

        switch (ebone)
        {
            case Ebone.BigBone:
                transform.DOScale(new Vector2(bigBoneSize, bigBoneSize), 0.5f);
                break;
            case Ebone.SmallBone:
                transform.DOScale(new Vector2(smallBoneSize, smallBoneSize), 0.5f);
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
                    Player.Instance.currentHp += 20;
                    Player.Instance.currentExperience += 20;
                    break;

                case Ebone.SmallBone:
                    Player.Instance.currentHp += 10;
                    Player.Instance.currentExperience += 10;
                    break;
            }

            Destroy(gameObject);
        }
    }
}