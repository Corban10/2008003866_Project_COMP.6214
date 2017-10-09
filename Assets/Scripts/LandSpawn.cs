using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandSpawn : MonoBehaviour
{    
    public GameObject island;
    public GameObject battleship;

    public Transform Spawn1;
    public Transform Spawn2;
    
    System.Random R = new System.Random();
    void Start()
    {
        //Instantiate(island, Spawn1.position, Spawn1.rotation);
        StartCoroutine("Spawner");
    }
    IEnumerator Spawner()
    {
        while (true) //while loop instead of StartCoroutine(Spawner()); Maybe less memory use, and optional break;
        {
            float spawnTimer = R.Next(10, 15);
            switch (R.Next(1, 3))
            {
                case 1:
                    Instantiate(island, Spawn1.position, Spawn1.rotation);
                    break;
                case 2:
                    Instantiate(battleship, Spawn1.position, Spawn1.rotation);
                    break;
            }
            yield return new WaitForSeconds(spawnTimer);

            spawnTimer = R.Next(10, 15);
            switch (R.Next(1, 3))
            {
                case 1:
                    Instantiate(island, Spawn2.position, Spawn2.rotation);
                    break;
                case 2:
                    Instantiate(battleship, Spawn2.position, Spawn2.rotation);
                    break;
            }
            yield return new WaitForSeconds(spawnTimer);
        }

        //StartCoroutine("Spawner");
    }
}
