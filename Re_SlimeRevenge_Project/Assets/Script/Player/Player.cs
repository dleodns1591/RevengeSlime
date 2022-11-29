using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    public enum EState
    {
        Walk,
        Skill,
        Shock,
        Eat,
        Die,
    }
    public EState eState;

    public int moveSpeed;

    void Start()
    {

    }

    void Update()
    {
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    void PlayerMove()
    {
        if (GameManager.instance._isStartGame == true)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                transform.Translate(0, moveSpeed * Time.deltaTime, 0);
            }
            else
            {
                transform.Translate(0, -moveSpeed * Time.deltaTime, 0);
            }
        }
    }
}
