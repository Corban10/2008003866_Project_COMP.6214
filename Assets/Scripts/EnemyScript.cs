using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject EnemyShot;
    public Transform ShotSpawn;
    public GameObject player;

    public float enemySpeed;
    public float shootTimer;
    private int enemyLives;

    private float fireRate;
    private float nextFire;

    Vector3 bulletOffset;
    Quaternion bulletRotation1;
    Quaternion bulletRotation2;
    void Awake()
    {
        player = GameObject.Find("Player");
        bulletOffset = new Vector3(0.3f, 0, 0);
        bulletRotation1.eulerAngles = new Vector3(0,0,170);
        bulletRotation2.eulerAngles = new Vector3(0, 0, -170);
        fireRate = 2f;
        enemySpeed = 2f;
        enemyLives = 6;
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            PlayerScript.playerLives--;
            player.transform.position = new Vector2(0, -5);
            Debug.Log("Player lives: " + PlayerScript.playerLives);
            if (PlayerScript.playerLives < 1)
            {
                Destroy(player);
            }
            Destroy(gameObject);
        }
        if (collision.name == "SingleShot(Clone)")
        {
            Debug.Log("Enemy lives: " + enemyLives);
            enemyLives--;
            if (enemyLives < 1)
            {
                Destroy(gameObject);
            }
        }
    }
}
