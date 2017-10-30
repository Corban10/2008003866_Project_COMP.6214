using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandSpawn : MonoBehaviour
{
    public GameObject island;
    public GameObject battleship;
    public GameObject enemy;
    public Transform Spawn1;
    public Transform Spawn2;
    

    System.Random R = new System.Random();
    void Start()
    {
        StartCoroutine("ObstacleSpawner");
        StartCoroutine("EnemySpawner");
    }
    IEnumerator ObstacleSpawner()
    {
        yield return new WaitForSeconds(3); // tiny breather before starting wave 1
        while (true)
        {
            float spawnTimer = R.Next(10, 13);
            //left
            InstantiateRandomLandObject(island, battleship, Spawn1, Spawn1, R.Next(1, 3));
            yield return new WaitForSeconds(spawnTimer);

            //right
            InstantiateRandomLandObject(island, battleship, Spawn2, Spawn2, R.Next(1, 3));
            yield return new WaitForSeconds(spawnTimer);
        }
    }
    void InstantiateRandomLandObject(GameObject gameObject1, GameObject gameObject2, Transform pos, Transform rot, int rNum)
    {
        switch (rNum)
        {
            case 1:
                Instantiate(gameObject1, pos.position, rot.rotation);
                break;
            case 2:
                Instantiate(gameObject2, pos.position, rot.rotation);
                break;
        }
    }

    IEnumerator EnemySpawner()
    {
        yield return new WaitForSeconds(3); // tiny breather before starting wave 1
        int difficulty = 12;
        float spawnTimer = 0f;
        Vector3 spacer = new Vector3(0, 11, 0);
        while (true)
        {
            //left
            spawnTimer = R.Next(difficulty, difficulty+1);
            spacer.x = 0;
            spacer.x += R.Next(1, 4);
            Instantiate(enemy, spacer, enemy.transform.rotation);
            yield return new WaitForSeconds(spawnTimer);
            //right
            spawnTimer = R.Next(difficulty, difficulty+1);
            spacer.x = 0;
            spacer.x -= R.Next(1, 4);
            Instantiate(enemy, spacer, enemy.transform.rotation);
            yield return new WaitForSeconds(spawnTimer);
            //reset
            if (difficulty > 3)
            {
                difficulty--;
            }
        }
    }
}
