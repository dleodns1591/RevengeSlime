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
    GameObject enemySpawn;

    void Start()
    {
        enemySpawn = GameObject.Find("EnemySpawnManager");
    }

    void Update()
    {
        Attack();
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
                int count = 1;

                if (enemySpawn.transform.GetChild(enemySpawn.transform.childCount - count).position.x >= -3.5f)
                    transform.DOMove(enemySpawn.transform.GetChild(enemySpawn.transform.childCount - count).position, 1f).SetEase(Ease.Linear);
                else
                    count++;
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
