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
    [SerializeField] int speed;

    void Start()
    {
        
    }

    void Update()
    {
        transform.DOLocalMoveX(-11, 2).SetEase(Ease.Linear).OnComplete(() =>
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
