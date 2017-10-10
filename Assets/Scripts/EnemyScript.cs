using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject EnemyShot;
    public Transform ShotSpawn;

    public static float enemySpeed;
    public static float shootTimer;

    private float fireRate;
    private float nextFire;

    Vector3 bulletOffset;
    Quaternion bulletRotation1;
    Quaternion bulletRotation2;
    void Awake()
    {
        bulletOffset = new Vector3(0.3f, 0, 0);
        bulletRotation1.eulerAngles = new Vector3(0,0,170);
        bulletRotation2.eulerAngles = new Vector3(0, 0, -170);
        fireRate = 1.5f;
        enemySpeed = 2f;
        GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().transform.up * enemySpeed;
    }
    void Start()
    {
        
    }
    void Update()
    {
        shootController();
    }
    void shootController()
    {
        if (Time.time > nextFire) //Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(EnemyShot, ShotSpawn.position - bulletOffset, bulletRotation1);
            Instantiate(EnemyShot, ShotSpawn.position, ShotSpawn.rotation);
            Instantiate(EnemyShot, ShotSpawn.position + bulletOffset, bulletRotation2);
        }
    }
}
