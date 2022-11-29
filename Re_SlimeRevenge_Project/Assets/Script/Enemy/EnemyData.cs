using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyData
{
    public string enemyName;
    public GameObject enemyPrefab;
    public int weight;

    public EnemyData(EnemyData enemyData)
    {
        enemyName = enemyData.enemyName;
        enemyPrefab = enemyData.enemyPrefab;
        weight = enemyData.weight;
    }
}
