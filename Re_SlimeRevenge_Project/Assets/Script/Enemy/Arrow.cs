using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Arrow : MonoBehaviour
{
    const int attack = 10;
    const float speed = 0.07f;

    bool isCheck = false;

    void Start()
    {
        Attack();
    }

    void Update()
    {
        if (isCheck)
            transform.Translate(Vector2.left * speed);
    }

    void Attack()
    {
        Vector3 dir = Player.Instance.transform.position - transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 180, Vector3.forward);
        transform.DOMove(Player.Instance.transform.position, 1f).SetEase(Ease.Linear).OnComplete(() =>
        {
            isCheck = true;
        });

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            transform.DOKill();
            Player.Instance.currentHp -= attack;
            Destroy(gameObject);
        }

        if (collision.CompareTag("DestroyBox"))
        {
            transform.DOKill();
            Destroy(gameObject);
        }
    }
}
