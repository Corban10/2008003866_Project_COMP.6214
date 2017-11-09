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
        while (true) //this repeats infinitely until player dies or game is restarted/exited
        {
            float spawnTimer = R.Next(10, 13); 
            //timer is set to a random value between 10 and 13

            //spawns a random land object on the left side, random number 1 or 2
            InstantiateRandomLandObject(island, battleship, Spawn1, Spawn1, R.Next(1, 3));
            yield return new WaitForSeconds(spawnTimer); 
			// waits for the duration of spawnTimer, thanks to the coroutine function waitforseconds.

            //same as above by for the right side.
            InstantiateRandomLandObject(island, battleship, Spawn2, Spawn2, R.Next(1, 3));
            yield return new WaitForSeconds(spawnTimer);
        }
    }
    void InstantiateRandomLandObject(GameObject gameObject1, GameObject gameObject2, Transform pos, Transform rot, int rNum)
    {
        // takes this parameter from the R.Next(1,3) function in the ObstacleSpawner Coroutine
        switch (rNum)
        {
            case 1: // if the random number is 1, spawn island
                Instantiate(gameObject1, pos.position, rot.rotation);
                break;
            case 2: // if the random number is 2, spawn battleship
                Instantiate(gameObject2, pos.position, rot.rotation);
                break;
        }
    }

    IEnumerator EnemySpawner()
    {
        yield return new WaitForSeconds(3); // 3 seconds breather before first spawn
        int difficulty = 12; //adds to the spawnTimer variable via the random class
        float spawnTimer = 0f; //speed of which enemies spawn in seconds
        Vector3 spacer = new Vector3(0, 11, 0);
        // this vector3 object "spacer" will be added to the spawn locations transform position 
        while (true) //infinite loop until game restart
        {
            //left side spawns first
            spawnTimer = R.Next(difficulty, difficulty+1);
            spacer.x = 0; 
            //reset x value in spacer object
            spacer.x += R.Next(1, 4); 
            // adds to the x value to spawn in random location on left side
            Instantiate(enemy, spacer, enemy.transform.rotation); //instantiate enemy
            yield return new WaitForSeconds(spawnTimer);
            //right side second
            spawnTimer = R.Next(difficulty, difficulty+1);
            spacer.x = 0;
            //reset x value in spacer object
            spacer.x -= R.Next(1, 4); 
            //minus from the x value to spawn in random location on right side
            Instantiate(enemy, spacer, enemy.transform.rotation); //instantiate enemy
            yield return new WaitForSeconds(spawnTimer);
            //reset
            if (difficulty > 3) // dont go lower/faster than 3 difficulty
            {
                difficulty--;
            }
        }
    }
}
