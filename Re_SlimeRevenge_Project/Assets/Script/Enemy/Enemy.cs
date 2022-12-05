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
    private float archerAttackTimer = 0.0f;
    public bool isKnockBack;
    public bool isCollsionAttack;

    [Space(10)]
    bool isArrow;
    public GameObject arrow;

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
        StateAnimation();
    }


    void EnemyMove()
    {
        switch (emove)
        {
            case EMove.None:
                break;
            case EMove.BackMove:
                isKnockBack = true;
                if ((int)rb2D.velocity.x <= 0)
                    emove = EMove.ForwardMove;
                break;
            case EMove.ForwardMove:
                isKnockBack = false;
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

    #region 적 애니메이션
      private void StateAnimation()
    {
        archerAttackTimer += Time.deltaTime;

        switch (eenemy)
        {
            case Eenemy.Shieldbearer:
                animator.SetInteger("Idle", hp);
                break;

            case Eenemy.Swordman:
                animator.SetBool("isKnockBack", isKnockBack);

                if (emove == EMove.ForwardMove)
                {
                    if (transform.position.x >= -7.8f)
                        animator.SetInteger("Walk", hp);
                    else
                        animator.SetBool("Attack", true);
                }
                else if (emove == EMove.BackMove)
                    animator.SetBool("Attack", false);
                break;

            case Eenemy.Archer:
                animator.SetBool("isKnockBack", isKnockBack);

                if (emove == EMove.ForwardMove)
                {
                    if (1 < archerAttackTimer && hp == 2)
                    {
                        animator.SetBool("Attack", true);

                        if (isArrow == false)
                        {
                            isArrow = true;
                            Instantiate(arrow, new Vector2(transform.localPosition.x - 0.55f, transform.localPosition.y - 0.5f), Quaternion.identity, transform);
                        }

                        if (1.5f < archerAttackTimer)
                        {
                            animator.SetBool("Attack", false);
                            archerAttackTimer = 0;
                        }
                    }
                    else
                        animator.SetInteger("Walk", hp);

                }
                break;

            case Eenemy.HeavyCavalry:
                EnemyState();
                break;

            case Eenemy.Berserker:
                EnemyState();
                break;
        }
    }

    void EnemyState()
    {
        animator.SetBool("isKnockBack", instance.isKnockBack);

        if (emove == EMove.ForwardMove)
            animator.SetInteger("Walk", hp);
        else if (emove == EMove.BackMove)
            animator.SetInteger("KnockBack", hp);
    }
    #endregion

    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        float waitTime = 0.5f;
        int playerInvincibility = 3;

        float bone = ((20 * bigBone) + (10 * smallBone));

        if (collision.CompareTag("Player"))
        {
            --hp;

            if (hp == 0)
            {
                emove = EMove.None;
                Player.Instance.eState = Player.EState.Eat;
                spriteRenderer.DOFade(0, waitTime);
                transform.DOScale(new Vector2(0.1f, 0.1f), waitTime);
                transform.DORotate(new Vector3(0, 0, -180), waitTime);
                transform.DOLocalMove(new Vector2(Player.Instance.transform.position.x, Player.Instance.transform.position.y), waitTime).OnComplete(() =>
                {
                    transform.DOKill();

                    Player.Instance.currentHp += bone;
                    Player.Instance.currentExperience += bone;
                });

                yield return new WaitForSeconds(2f);
                Player.Instance.eState = Player.EState.Walk;
                Destroy(gameObject);
            }

            else
            {
                //넉백
                emove = EMove.BackMove;
                rb2D.AddForce(new Vector2(7, 0), ForceMode2D.Impulse);

                if (isCollsionAttack == true)
                {
                    // 공격력 만큼 플레이어 체력 차감
                    Player.Instance.currentHp -= (attack - Player.Instance.defense);

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
