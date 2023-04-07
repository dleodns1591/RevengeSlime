using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    [Header("이동")]
    [SerializeField] float moveSpeed = 0;
    [SerializeField] float scrollRange = 0;
    [SerializeField] Transform target; 
    [SerializeField] Vector3 moveDirection = Vector3.zero;

    private void Start()
    {

    }

    private void Update()
    {
        Move();
    }

    void Move()
    {
        if (GameManager.instance._isStartGame)
        {
            // 배경이 moveDirection 방향으로 moveSpeed의 속도로 이동
            transform.localPosition += moveDirection * moveSpeed * Time.deltaTime;

            // 배경이 설정된 범위를 벗어나면 위치 재설정
            if (transform.localPosition.x <= -scrollRange)
                transform.localPosition = target.localPosition + Vector3.right * 9.8f;
        }
    }
}
