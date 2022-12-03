using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        StateAnimation();
    }

    private void StateAnimation()
    {
        switch (Enemy.instance.eenemy)
        {
            case Enemy.Eenemy.Shieldbearer:
                animator.SetInteger("Idle", Enemy.instance.hp);
                break;

            case Enemy.Eenemy.Swordman:
                animator.SetBool("isKnockBack", Enemy.instance.isKnockBack);

                if (Enemy.instance.emove == Enemy.EMove.ForwardMove)
                    animator.SetInteger("Walk", Enemy.instance.hp);
                else if (Enemy.instance.emove == Enemy.EMove.BackMove)
                    animator.SetBool("isKnockBack", Enemy.instance.isKnockBack);
                break;

            case Enemy.Eenemy.Archer:
                animator.SetBool("isKnockBack", Enemy.instance.isKnockBack);

                if (Enemy.instance.emove == Enemy.EMove.ForwardMove)
                    animator.SetInteger("Walk", Enemy.instance.hp);
                break;

            case Enemy.Eenemy.HeavyCavalry:
                animator.SetBool("isKnockBack", Enemy.instance.isKnockBack);

                if (Enemy.instance.emove == Enemy.EMove.ForwardMove)
                    animator.SetInteger("Walk", Enemy.instance.hp);
                else if (Enemy.instance.emove == Enemy.EMove.BackMove)
                    animator.SetInteger("KnockBack", Enemy.instance.hp + 1);
                break;

            case Enemy.Eenemy.Berserker:
                animator.SetBool("isKnockBack", Enemy.instance.isKnockBack);

                if (Enemy.instance.emove == Enemy.EMove.ForwardMove)
                    animator.SetInteger("Walk", Enemy.instance.hp);
                else if (Enemy.instance.emove == Enemy.EMove.BackMove)
                    animator.SetInteger("KnockBack", Enemy.instance.hp + 1);
                break;
        }
    }
}
