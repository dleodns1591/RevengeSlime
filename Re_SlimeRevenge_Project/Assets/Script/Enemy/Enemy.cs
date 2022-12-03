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

    public enum EMove
    {
        None,
        BackMove,
        ForwardMove,
    }

    public enum Espeed
    {
        Slow,
        Usual,
        Fast,
    }

    [Header("수치적 데이터")]
    public Eenemy eenemy;
    public EMove emove;
    public Espeed espeed;

    public int hp;
    public int attack;
    public int bigBone;
    public int smallBone;
    private float moveSpeed;
    public bool isKnockBack;
    public bool isCollsionAttack;
    bool isMoveCheck = false;


    Animator animator;
    Rigidbody2D rb2D;


    void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        EnemyMove();
    }


    void EnemyMove()
    {
        switch (emove)
        {
            case EMove.None:
                break;
            case EMove.BackMove:
                if ((int)rb2D.velocity.x <= 0)
                    emove = EMove.ForwardMove;
                break;
            case EMove.ForwardMove:
                transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
                break;
        }

        switch (espeed)
        {
            case Espeed.Slow:
                moveSpeed = -2;
                break;
            case Espeed.Usual:
                moveSpeed = -3f;
                break;
            case Espeed.Fast:
                moveSpeed = -5f;
                break;

        }
    }

    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        int playerInvincibility = 3;

        if (collision.CompareTag("Player"))
        {
            --hp;
            if (hp == 0)
            {

            }

            else
            {
                if (attack > 0)
                {
                    emove = EMove.BackMove;
                    rb2D.AddForce(new Vector2(7, 0), ForceMode2D.Impulse);
                    Player.Instance.currentHp -= attack;

                    Player.Instance.eState = Player.EState.Shock;
                    Player.Instance.tag = "invincibility";
                    Player.Instance.spriteRenderer.DOFade(0.5f, 0.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);

                    yield return new WaitForSeconds(playerInvincibility);

                    Player.Instance.spriteRenderer.DOKill();
                    Player.Instance.spriteRenderer.DOFade(1, 0);

                    Player.Instance.eState = Player.EState.Walk;
                    Player.Instance.tag = "Player";
                }
            }


        }
    }
}
