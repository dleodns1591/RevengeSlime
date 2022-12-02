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
    
    public enum Espeed
    {
        Slow,
        Usual,
        Fast,
    }

    [Header("수치적 데이터")]
    public Eenemy eenemy;
    public Espeed espeed;

    public int hp;
    public int attack;
    public bool isCollsionAttack;
    public int bigBone;
    public int smallBone;

    public Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
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

            if (hp > 1)
            {
                
            }
            else
            {
                //죽음
            }
        }
    }
}
