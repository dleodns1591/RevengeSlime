using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;

    void Start()
    {

    }

    void Update()
    {
        StateAnimation();
    }

    void Awake()
    {
        animator = GetComponent<Animator>();    
    }

    public void StateAnimation()
    {
        switch (Player.Instance.eState)
        {
            case Player.EState.Walk:
                animator.SetInteger("PlayerState", 0);
                break;

            case Player.EState.Eat:
                animator.SetInteger("PlayerState", 1);
                break;

            case Player.EState.Skill:
                animator.SetInteger("PlayerState", 2);
                break;

            case Player.EState.Shock:
                animator.SetInteger("PlayerState", 3);
                break;

        }
    }
}
