using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bomb : MonoBehaviour
{
    private enum EBomb
    {
        SlimeBomb,
        EnergyBomb
    }

    [SerializeField] EBomb eBomb;

    void Start()
    {
        Attack();
    }

    void Update()
    {
    }

    void Attack()
    {
        switch (eBomb)
        {
            case EBomb.SlimeBomb:
                transform.DOMoveX(20, 5).SetEase(Ease.Linear).OnComplete(() =>
                {
                    transform.DOKill();
                    SkillManager.instance.isSlimeBombCheck = false;
                    Destroy(gameObject);
                });
                break;

            case EBomb.EnergyBomb:
                for (int i = 0; i < EnemySpawn.instance.transform.childCount; i++)
                {
                    GameObject target = EnemySpawn.instance.transform.GetChild(i).gameObject;

                    if (target.transform.position.x >= -3 && target.transform.position.x <= 4)
                    {
                        transform.DOLocalMove(target.transform.position, 1).SetEase(Ease.Linear).OnComplete(() =>
                        {
                            SkillManager.instance.isEnergyBombCheck = false;
                        });
                    }
                    else
                    {
                        Debug.Log("asdfsdfasdf");
                        SkillManager.instance.isEnergyBombCheck = false;
                        Destroy(this.gameObject);
                    }
                }
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            transform.DOKill();
            switch (eBomb)
            {
                case EBomb.SlimeBomb:
                    SkillManager.instance.isSlimeBombCheck = false;
                    break;

                case EBomb.EnergyBomb:
                    SkillManager.instance.isEnergyBombCheck = false;
                    break;
            }
            Destroy(gameObject);
        }
    }
}
