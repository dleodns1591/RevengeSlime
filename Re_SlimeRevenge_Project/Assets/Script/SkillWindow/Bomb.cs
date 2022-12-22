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
                for (int i = 0; i < EnemySpawn.instance.transform.childCount - 1; i++)
                {
                    GameObject targetEnemy = EnemySpawn.instance.gameObject.transform.GetChild(i).gameObject;

                    if (targetEnemy.transform.position.x >= -3 && targetEnemy.transform.position.x <= 4)
                    {
                        if (targetEnemy != null)
                            transform.DOMove(EnemySpawn.instance.gameObject.transform.GetChild(i).position, 1f).SetEase(Ease.Linear);
                        else
                            return;

                        break;
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
