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
                
                break;

            case EBomb.EnergyBomb:
                transform.DOMove(enemySpawn.transform.GetChild(enemySpawn.transform.childCount -1).position, 1f).SetEase(Ease.Linear);
                break;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
