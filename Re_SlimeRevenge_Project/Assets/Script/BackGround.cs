using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    [Header("��� �̵�")]
    [SerializeField] float moveSpeed;
    [SerializeField] float scrollRange;

    [SerializeField] Transform target; // ���� ���ӿ����� 3���� ����� ���ΰ� ������ Ÿ��
    [SerializeField] Vector3 moveDirection = Vector3.left;

    private void Start()
    {

    }

    private void Update()
    {
        BackGroundScroll();
    }

    void BackGroundScroll()
    {
        if (GameManager.instance._isStartGame == true)
        {
            // ����� moveDirection �������� moveSpeed�� �ӵ��� �̵�
            transform.localPosition += moveDirection * moveSpeed * Time.deltaTime;

            // ����� ������ ������ ����� ��ġ �缳��
            if (transform.localPosition.x <= -scrollRange)
            {
                transform.localPosition = target.localPosition + Vector3.right * 9.8f;
            }
        }
    }
}
