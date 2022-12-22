using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public List<EnemyData> enemy = new List<EnemyData>();
    public static EnemySpawn instance;

    void Start()
    {
        StartCoroutine(Spawn());
    }

    void Update()
    {

    }

    private void Awake()
    {
        instance = this;
    }

    IEnumerator Spawn()
    {
        int distance = GameManager.instance._distance;

        while (true)
        {
            if (GameManager.instance._isStartGame == true)
            {
                int randomSpawn = Random.Range(1, 101);
                float randomPosY = Random.Range(-3.5f, 0f);

                Vector2 spawnPos = new Vector2(11, randomPosY);

                if (0 <= distance && distance < 20)
                {
                    yield return new WaitForSeconds(4.3f);

                    if (0 <= randomSpawn && randomSpawn <= 40)
                        Instantiate(enemy[0].enemy, spawnPos, Quaternion.identity, transform);
                    else
                        Instantiate(enemy[1].enemy, spawnPos, Quaternion.identity, transform);
                }

                else if (20 <= distance && distance < 45)
                {
                    yield return new WaitForSeconds(4);

                    if (0 <= randomSpawn && randomSpawn <= 50)
                        Instantiate(enemy[0].enemy, spawnPos, Quaternion.identity, transform);
                    else
                        Instantiate(enemy[1].enemy, spawnPos, Quaternion.identity, transform);
                }

                else if (45 <= distance && distance < 70)
                {
                    yield return new WaitForSeconds(3.7f);

                    if (0 <= randomSpawn && randomSpawn <= 50)
                        Instantiate(enemy[0].enemy, spawnPos, Quaternion.identity, transform);
                    else if (60 <= randomSpawn && randomSpawn <= 80)
                        Instantiate(enemy[1].enemy, spawnPos, Quaternion.identity, transform);
                    else
                        Instantiate(enemy[2].enemy, spawnPos, Quaternion.identity, transform);
                }

                else if (70 <= distance && distance < 95)
                {
                    yield return new WaitForSeconds(3.4f);

                    if (0 <= randomSpawn && randomSpawn <= 35)
                        Instantiate(enemy[0].enemy, spawnPos, Quaternion.identity, transform);
                    else if (35 < randomSpawn && randomSpawn <= 65)
                        Instantiate(enemy[1].enemy, spawnPos, Quaternion.identity, transform);
                    else if (65 < randomSpawn && randomSpawn <= 90)
                        Instantiate(enemy[2].enemy, spawnPos, Quaternion.identity, transform);
                    else
                        Instantiate(enemy[3].enemy, spawnPos, Quaternion.identity, transform);
                }

                else if (95 <= distance && distance < 120)
                {
                    yield return new WaitForSeconds(3f);

                    if (0 <= randomSpawn && randomSpawn <= 30)
                        Instantiate(enemy[0].enemy, spawnPos, Quaternion.identity, transform);
                    else if (30 < randomSpawn && randomSpawn <= 50)
                        Instantiate(enemy[1].enemy, spawnPos, Quaternion.identity, transform);
                    else if (50 < randomSpawn && randomSpawn <= 75)
                        Instantiate(enemy[2].enemy, spawnPos, Quaternion.identity, transform);
                    else if (75 < randomSpawn && randomSpawn <= 85)
                        Instantiate(enemy[3].enemy, spawnPos, Quaternion.identity, transform);
                    else
                        Instantiate(enemy[4].enemy, spawnPos, Quaternion.identity, transform);
                }

                else if (120 <= distance && distance < 145)
                {
                    yield return new WaitForSeconds(2.8f);

                    if (0 <= randomSpawn && randomSpawn <= 10)
                        Instantiate(enemy[0].enemy, spawnPos, Quaternion.identity, transform);
                    else if (10 < randomSpawn && randomSpawn <= 30)
                        Instantiate(enemy[1].enemy, spawnPos, Quaternion.identity, transform);
                    else if (30 < randomSpawn && randomSpawn <= 50)
                        Instantiate(enemy[2].enemy, spawnPos, Quaternion.identity, transform);
                    else if (50 < randomSpawn && randomSpawn <= 60)
                        Instantiate(enemy[3].enemy, spawnPos, Quaternion.identity, transform);
                    else if (60 < randomSpawn && randomSpawn <= 70)
                        Instantiate(enemy[4].enemy, spawnPos, Quaternion.identity, transform);
                    else if (70 < randomSpawn && randomSpawn <= 85)
                        Instantiate(enemy[5].enemy, spawnPos, Quaternion.identity, transform);
                    else
                        Instantiate(enemy[7].enemy, spawnPos, Quaternion.identity, transform);
                }

                else if (145 <= distance && distance < 170)
                {
                    yield return new WaitForSeconds(2.6f);

                    if (0 <= randomSpawn && randomSpawn <= 10)
                        Instantiate(enemy[0].enemy, spawnPos, Quaternion.identity, transform);
                    else if (10 < randomSpawn && randomSpawn <= 25)
                        Instantiate(enemy[1].enemy, spawnPos, Quaternion.identity, transform);
                    else if (25 < randomSpawn && randomSpawn <= 50)
                        Instantiate(enemy[2].enemy, spawnPos, Quaternion.identity, transform);
                    else if (50 < randomSpawn && randomSpawn <= 60)
                        Instantiate(enemy[3].enemy, spawnPos, Quaternion.identity, transform);
                    else if (60 < randomSpawn && randomSpawn <= 70)
                        Instantiate(enemy[4].enemy, spawnPos, Quaternion.identity, transform);
                    else if (70 < randomSpawn && randomSpawn <= 80)
                        Instantiate(enemy[5].enemy, spawnPos, Quaternion.identity, transform);
                    else if (80 < randomSpawn && randomSpawn <= 85)
                        Instantiate(enemy[6].enemy, spawnPos, Quaternion.identity, transform);
                    else
                        Instantiate(enemy[7].enemy, spawnPos, Quaternion.identity, transform);
                }

                else if (170 <= distance && distance < 200)
                {
                    yield return new WaitForSeconds(2.4f);

                    if (0 <= randomSpawn && randomSpawn <= 10)
                        Instantiate(enemy[0].enemy, spawnPos, Quaternion.identity, transform);
                    else if (10 < randomSpawn && randomSpawn <= 25)
                        Instantiate(enemy[1].enemy, spawnPos, Quaternion.identity, transform);
                    else if (25 < randomSpawn && randomSpawn <= 45)
                        Instantiate(enemy[2].enemy, spawnPos, Quaternion.identity, transform);
                    else if (45 < randomSpawn && randomSpawn <= 55)
                        Instantiate(enemy[3].enemy, spawnPos, Quaternion.identity, transform);
                    else if (55 < randomSpawn && randomSpawn <= 65)
                        Instantiate(enemy[4].enemy, spawnPos, Quaternion.identity, transform);
                    else if (65 < randomSpawn && randomSpawn <= 80)
                        Instantiate(enemy[5].enemy, spawnPos, Quaternion.identity, transform);
                    else if (80 < randomSpawn && randomSpawn <= 90)
                        Instantiate(enemy[6].enemy, spawnPos, Quaternion.identity, transform);
                    else
                        Instantiate(enemy[7].enemy, spawnPos, Quaternion.identity, transform);
                }

                else if (200 <= distance)
                {
                    yield return new WaitForSeconds(2);

                    if (0 <= randomSpawn && randomSpawn <= 10)
                        Instantiate(enemy[0].enemy, spawnPos, Quaternion.identity, transform);
                    else if (10 < randomSpawn && randomSpawn <= 20)
                        Instantiate(enemy[1].enemy, spawnPos, Quaternion.identity, transform);
                    else if (20 < randomSpawn && randomSpawn <= 40)
                        Instantiate(enemy[2].enemy, spawnPos, Quaternion.identity, transform);
                    else if (40 < randomSpawn && randomSpawn <= 50)
                        Instantiate(enemy[3].enemy, spawnPos, Quaternion.identity, transform);
                    else if (50 < randomSpawn && randomSpawn <= 60)
                        Instantiate(enemy[4].enemy, spawnPos, Quaternion.identity, transform);
                    else if (60 < randomSpawn && randomSpawn <= 75)
                        Instantiate(enemy[5].enemy, spawnPos, Quaternion.identity, transform);
                    else if (75 < randomSpawn && randomSpawn <= 90)
                        Instantiate(enemy[6].enemy, spawnPos, Quaternion.identity, transform);
                    else
                        Instantiate(enemy[7].enemy, spawnPos, Quaternion.identity, transform);
                }
            }
            else
                yield return new WaitForSeconds(1);
        }
    }
}
