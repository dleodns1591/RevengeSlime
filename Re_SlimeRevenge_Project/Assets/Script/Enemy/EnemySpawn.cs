using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public List<GameObject> enemy = new List<GameObject>();

    [SerializeField] int distanceCheck;

    void Start()
    {
        distanceCheck = 0;
        StartCoroutine(Spawn());
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        int early = 25;
        int secondHalf = 30;
        int maxDistance = 170;

        if (distanceCheck + 20 == GameManager.instance._distance)
        {
            if (distanceCheck <= maxDistance)
                distanceCheck += early;
            else
                distanceCheck += secondHalf;
        }
    }



    IEnumerator Spawn()
    {

        while (true)
        {
            if (GameManager.instance._isStartGame == true && distanceCheck <= GameManager.instance._distance)
            {
                switch (distanceCheck)
                {
                    case 0:
                        break;
                    case 20:
                        break;
                    case 45:
                        break;
                    case 70:
                        break;
                    case 95:
                        break;
                    case 120:
                        break;
                    case 145:
                        break;
                    case 170:
                        break;
                    case 200:
                        break;
                }
            }
            else
                yield return new WaitForSeconds(1);
        }
    }
}
