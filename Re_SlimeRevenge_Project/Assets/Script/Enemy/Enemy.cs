using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    public static Enemy instance;
    void Awake() => instance = this;

    public enum Eenemy
    {
        Noob1,
        Noob2,
        Shieldbearer,
        Bargate,
        Swordman,
        Archer,
        HeavyCavalry,
        Berserker,
    }
    public Eenemy eenemy;

    public int hp;
    public int attack;
    public int moveSpeed;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        int playerInvincibility = 3;

        if (collision.CompareTag("Player"))
        {
            --hp;

            if (attack > 0)
            {
                Player.Instance.eState = Player.EState.Shock;
                Player.Instance.tag = "invincibility";
                Player.Instance.spriteRenderer.DOFade(0.5f, 0.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);

                yield return new WaitForSeconds(playerInvincibility);

                Player.Instance.spriteRenderer.DOKill();
                Player.Instance.spriteRenderer.DOFade(1, 0);

                Player.Instance.eState = Player.EState.Walk;
                Player.Instance.tag = "Player";
            }

            if (hp > 0)
            {
                //³Ë¹é
            }
            else
            {
                //Á×À½
            }
        }
    }
}
