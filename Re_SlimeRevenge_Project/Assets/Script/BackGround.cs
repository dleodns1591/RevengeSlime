using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    [Header("�̵�")]
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
            // ����� moveDirection �������� moveSpeed�� �ӵ��� �̵�
            transform.localPosition += moveDirection * moveSpeed * Time.deltaTime;

            // ����� ������ ������ ����� ��ġ �缳��
            if (transform.localPosition.x <= -scrollRange)
                transform.localPosition = target.localPosition + Vector3.right * 9.8f;
        }
    }
}
