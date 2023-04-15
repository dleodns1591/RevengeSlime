using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum EEnemyType
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
    Die
}

public enum ESpeed
{
    Slow,
    Usual,
    Fast,
}


class BaseEnemy
{
    public Base thisBase;
    Enemy context;

    public int moveSpeed;

    public Transform transform;
    public GameObject gameObject;

    public Rigidbody2D rigidbody = null;
    public BoxCollider2D boxCollider2D = null;
    public SpriteRenderer spriteRenderer = null;

    public BaseEnemy(Enemy _context)
    {
        context = _context;

        transform = _context.transform;
        gameObject = _context.gameObject;

        rigidbody = _context.GetComponent<Rigidbody2D>();
        boxCollider2D = _context.GetComponent<BoxCollider2D>();
        spriteRenderer = _context.GetComponent<SpriteRenderer>();
    }


    // DI »ç¿ë
    public void BaseEnemyType(EEnemyType type)
    {
        switch (type)
        {
            case EEnemyType.Noob1:
                thisBase = new BaseNoob1(context);
                break;

            case EEnemyType.Noob2:
                thisBase = new BaseNoob2(context);
                break;

            case EEnemyType.Shieldbearer:
                thisBase = new BaseShieldbearer(context);
                break;

            case EEnemyType.Bargate:
                thisBase = new BaseBerserker(context);
                break;

            case EEnemyType.Swordman:
                thisBase = new BaseSwordman(context);
                break;

            case EEnemyType.Archer:
                thisBase = new BaseArcher(context);
                break;

            case EEnemyType.HeavyCavalry:
                thisBase = new BaseHeavyCavalry(context);
                break;

            case EEnemyType.Berserker:
                thisBase = new BaseBerserker(context);
                break;
        }
    }

    public void BaseEnemyMove(EMove move)
    {
        moveSpeed = thisBase.moveSpeed;

        switch (move)
        {
            case EMove.None:
                break;

            case EMove.BackMove:
                context.isKnockBack = true;
                if ((int)rigidbody.velocity.x <= 0)
                    context.eMove = EMove.ForwardMove;
                break;

            case EMove.ForwardMove:
                context.isKnockBack = false;
                transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
                break;

            case EMove.Die:
                boxCollider2D.enabled = false;
                transform.DOLocalMove(new Vector2(Player.Instance.transform.position.x, Player.Instance.transform.position.y + 0.5f), 0.5f).SetEase(Ease.Linear);
                break;
        }
    }

    public void EnemyAnimation()
    {
        thisBase.Animation();
    }
}

public abstract class Base
{
    public int hp;
    public int attack;
    public int bigBoneNum;
    public int smallBoneNum;
    public int moveSpeed;

    protected Enemy context;
    protected GameObject gameObject;
    protected Transform transform;
    protected Animator animator = null;

    public void State(Enemy _context, int hp, int attack, int bigBoneNum, int smallBoneNum, int moveSpeed)
    {
        context = _context;
        gameObject = _context.gameObject;
        transform = _context.transform;
        animator = _context.GetComponent<Animator>();

        this.hp = hp;
        this.attack = attack;
        this.bigBoneNum = bigBoneNum;
        this.smallBoneNum = smallBoneNum;
        this.moveSpeed = moveSpeed;
    }

    public abstract void Animation();
}

public class BaseNoob1 : Base
{
    public BaseNoob1(Enemy _context) => State(_context, 1, 0, 1, 0, -2);

    public override void Animation()
    {

    }
}

public class BaseNoob2 : Base
{
    public BaseNoob2(Enemy _context) => State(_context, 1, 0, 1, 0, -2);

    public override void Animation()
    {
    }
}

public class BaseShieldbearer : Base
{
    public BaseShieldbearer(Enemy _context) => State(_context, 2, 0, 1, 1, -3);

    public override void Animation()
    {
        animator.SetInteger("Idle", hp);
    }
}

public class BaseBargate : Base
{
    public BaseBargate(Enemy _context) => State(_context, 1, 40, 0, 0, -3);

    public override void Animation()
    {
    }
}

public class BaseSwordman : Base
{
    public BaseSwordman(Enemy _context) => State(_context, 2, 20, 1, 1, -3);

    public override void Animation()
    {
        animator.SetBool("isKnockBack", context.isKnockBack);

        if (context.eMove == EMove.ForwardMove)
        {
            if (transform.position.x <= -4.5f && Player.Instance.transform.position.y + 0.5f >= transform.position.y && Player.Instance.transform.position.y - 0.5f <= transform.position.y)
                animator.SetBool("Attack", true);
            else
                animator.SetInteger("Walk", hp);
        }
        else if (context.eMove == EMove.BackMove)
            animator.SetBool("Attack", false);
    }
}

public class BaseArcher : Base
{
    public BaseArcher(Enemy _context) => State(_context, 2, 10, 1, 1, -3);

    public override void Animation()
    {
        float archerAttackTimer = 0.0f;
        bool isArrow = false;

        archerAttackTimer += Time.deltaTime;
        animator.SetBool("isKnockBack", context.isKnockBack);



        if (context.eMove == EMove.ForwardMove)
        {
            if (1 < archerAttackTimer && hp == 2)
            {
                animator.SetBool("Attack", true);

                if (isArrow == false && transform.position.x >= -5)
                {
                    isArrow = true;
                    context.InstantiateSetParent(context.arrow, new Vector2(transform.localPosition.x - 0.55f, transform.localPosition.y + 0.5f), Quaternion.identity);
                }

                if (1.5f < archerAttackTimer)
                {
                    animator.SetBool("Attack", false);
                    isArrow = false;
                    archerAttackTimer = 0;
                }
            }
            else
                animator.SetInteger("Walk", hp);
        }

    }
}

public class BaseHeavyCavalry : Base
{
    public BaseHeavyCavalry(Enemy _context) => State(_context, 3, 0, 2, 0, -2);

    public override void Animation()
    {
        animator.SetBool("isKnockBack", context.isKnockBack);

        if (context.eMove == EMove.ForwardMove)
            animator.SetInteger("Walk", hp);
        else if (context.eMove == EMove.BackMove)
            animator.SetInteger("KnockBack", hp);
    }
}

public class BaseBerserker : Base
{
    public BaseBerserker(Enemy _context) => State(_context, 2, 30, 1, 2, -5);

    public override void Animation()
    {
        animator.SetBool("isKnockBack", context.isKnockBack);

        if (context.eMove == EMove.ForwardMove)
            animator.SetInteger("Walk", hp);
        else if (context.eMove == EMove.BackMove)
            animator.SetInteger("KnockBack", hp);
    }
}

