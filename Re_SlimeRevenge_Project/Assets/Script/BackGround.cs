using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    [Header("배경 이동")]
    [SerializeField] float moveSpeed;
    [SerializeField] float scrollRange;

    [SerializeField] Transform target; // 현재 게임에서는 3개의 배경의 서로가 서로의 타켓
    [SerializeField] Vector3 moveDirection = Vector3.left;

    //public GameObject upgradeWindow;

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
            // 배경이 moveDirection 방향으로 moveSpeed의 속도로 이동
            transform.localPosition += moveDirection * moveSpeed * Time.deltaTime;

            // 배경이 설정된 범위를 벗어나면 위치 재설정
            if (transform.localPosition.x <= -scrollRange)
            {
                transform.localPosition = target.localPosition + Vector3.right * 9.8f;
            }
        }
    }
}
