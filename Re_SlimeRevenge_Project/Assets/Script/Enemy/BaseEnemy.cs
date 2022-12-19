using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    Base thisBase;
    Enemy context;

    public int moveSpeed;
    public bool isKnockBack;
    public bool isCollsionAttack;

    public Transform transform;
    public GameObject gameObject;

    public Rigidbody2D rigidbody = null;
    public BoxCollider2D boxCollider2D = null;
    public SpriteRenderer spriteRenderer = null;

    public BaseEnemy(Enemy _context)
    {
        context = _context;
        rigidbody = _context.GetComponent<Rigidbody2D>();
        boxCollider2D = _context.GetComponent<BoxCollider2D>();
        spriteRenderer = _context.GetComponent<SpriteRenderer>();
    }

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
                isKnockBack = true;
                if ((int)rigidbody.velocity.x <= 0)
                    context.eMove = EMove.ForwardMove;
                break;

            case EMove.ForwardMove:
                isKnockBack = false;
                
                break;

            case EMove.Die:
                break;
        }
    }

    public void BaseEnemySpeed(ESpeed speed)
    {
        switch (speed)
        {
            case ESpeed.Slow:
                moveSpeed = -2;
                break;
            case ESpeed.Usual:
                moveSpeed = -3;
                break;
            case ESpeed.Fast:
                moveSpeed = -5;
                break;
        }
    }
}

public abstract class Base
{
    public int moveSpeed;

    protected int hp;
    protected int attack;
    protected int bigBoneNum;
    protected int smallBoneNum;
    protected float archerAttackTimer = 0.0f;


    protected Enemy context;
    protected GameObject gameObject;
    protected Transform transform;

    protected Animator animator = null;
    protected Rigidbody2D rigidbody = null;
    protected BoxCollider2D boxCollider2D = null;
    protected SpriteRenderer spriteRenderer = null;

}

public class BaseNoob1 : Base
{
    public BaseNoob1(Enemy _context)
    {
        context = _context;
        gameObject = _context.gameObject;
        transform = _context.transform;
        animator = _context.GetComponent<Animator>();

        hp = 1;
        attack = 0;
        bigBoneNum = 1;
        smallBoneNum = 0;
        moveSpeed = -2;
    }
}

public class BaseNoob2 : Base
{
    public BaseNoob2(Enemy _context)
    {
        context = _context;
        gameObject = _context.gameObject;
        transform = _context.transform;
        animator = _context.GetComponent<Animator>();
    }
}

public class BaseShieldbearer : Base
{
    public BaseShieldbearer(Enemy _context)
    {
        context = _context;
        gameObject = _context.gameObject;
        transform = _context.transform;
        animator = _context.GetComponent<Animator>();
    }

}

public class BaseBargate : Base
{
    public BaseBargate(Enemy _context)
    {
        context = _context;
        gameObject = _context.gameObject;
        transform = _context.transform;
        animator = _context.GetComponent<Animator>();
    }

}

public class BaseSwordman : Base
{
    public BaseSwordman(Enemy _context)
    {
        context = _context;
        gameObject = _context.gameObject;
        transform = _context.transform;
        animator = _context.GetComponent<Animator>();
    }

}

public class BaseArcher : Base
{
    public BaseArcher(Enemy _context)
    {
        context = _context;
        gameObject = _context.gameObject;
        transform = _context.transform;
        animator = _context.GetComponent<Animator>();
    }

}

public class BaseHeavyCavalry : Base
{
    public BaseHeavyCavalry(Enemy _context)
    {
        context = _context;
        gameObject = _context.gameObject;
        transform = _context.transform;
        animator = _context.GetComponent<Animator>();
    }

}

public class BaseBerserker : Base
{
    public BaseBerserker(Enemy _context)
    {
        context = _context;
        gameObject = _context.gameObject;
        transform = _context.transform;
        animator = _context.GetComponent<Animator>();
    }

}

