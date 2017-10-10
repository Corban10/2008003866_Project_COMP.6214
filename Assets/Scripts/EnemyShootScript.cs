using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootScript : MonoBehaviour
{
    public static float bulletSpeed; //static so that it can be accessed from other scripts
    void Awake()
    {
        bulletSpeed = 10;
    }
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().transform.up * bulletSpeed;
    }
}
