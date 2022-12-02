using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyData
{
    public string enemyName;
    public GameObject enemy;


    public EnemyData(EnemyData enemyData)
    {
        enemyName = enemyData.enemyName;
        enemy = enemyData.enemy;
    }
}
