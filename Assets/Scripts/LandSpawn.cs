using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandSpawn : MonoBehaviour
{    
    public GameObject island; //to instantiate island sprite
    public Transform Spawn1;
    public Transform Spawn2;
    void Start()
    {
        Instantiate(island, Spawn1.position, Spawn1.rotation);
        Instantiate(island, Spawn2.position, Spawn2.rotation);
    }
}
