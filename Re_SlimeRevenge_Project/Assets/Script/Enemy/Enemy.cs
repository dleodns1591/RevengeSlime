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
    public bool isCollsionAttack;


    Animator animator;
    Rigidbody2D rb2D;
    SpriteRenderer spriteRenderer;


    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        float waitTime = 0.5f;
        int playerInvincibility = 3;

        if (collision.CompareTag("Player"))
        {
            --hp;

            if (hp == 0)
            {
                emove = EMove.None;
                Player.Instance.eState = Player.EState.Eat;
                transform.DOScale(new Vector2(0.1f, 0.1f), waitTime);
                transform.DORotate(new Vector3(0, 0, -180), waitTime);
                transform.DOLocalMove(new Vector2(Player.Instance.transform.position.x, Player.Instance.transform.position.y), waitTime).OnComplete(() =>
                {
                    transform.DOKill();
                    spriteRenderer.DOFade(0, 0);

                    // 뼈소환
                });

                yield return new WaitForSeconds(2f);
                Player.Instance.eState = Player.EState.Walk;
                Destroy(gameObject);
            }

            else
            {
                if (isCollsionAttack == true)
                {
                    emove = EMove.BackMove;

                    //넉백
                    rb2D.AddForce(new Vector2(7, 0), ForceMode2D.Impulse); 

                    // 공격력 만큼 플레이어 체력 차감
                    Player.Instance.currentHp -= attack;

                    // 무적 처리
                    Player.Instance.eState = Player.EState.Shock;
                    Player.Instance.tag = "invincibility";
                    Player.Instance.spriteRenderer.DOFade(0.5f, waitTime).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);

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
