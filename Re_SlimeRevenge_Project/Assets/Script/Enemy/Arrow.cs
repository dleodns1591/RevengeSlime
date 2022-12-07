using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Arrow : MonoBehaviour
{
    public const int attack = 10;

    void Start()
    {
        Attack();
    }

    void Update()
    {
    }

    void Attack()
    {
        Vector3 dir = Player.Instance.transform.position - transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 180, Vector3.forward);
        transform.DOMove(Player.Instance.transform.position, 1f).SetEase(Ease.Linear).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            transform.DOKill();

            Player.Instance.currentHp -= attack;
            Destroy(gameObject);
        }
    }
}
