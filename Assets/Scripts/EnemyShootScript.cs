using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootScript : MonoBehaviour
{
    public GameObject player;
    private float bulletSpeed;
    private float checkTime;
    void Awake()
    {
        player = GameObject.Find("Player");
        bulletSpeed = 8;
    }
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().transform.up * bulletSpeed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player && Time.timeSinceLevelLoad > checkTime)
        {
            Debug.Log(collision);
            PlayerScript.playerLives--;
            player.transform.position = new Vector2(0, -5);
            checkTime = Time.timeSinceLevelLoad + 5;
            Debug.Log("Time.time = " + Time.time);
            Debug.Log("checktime = " + checkTime);
            Debug.Log(PlayerScript.playerLives);
            if (PlayerScript.playerLives < 1)
            {
                Destroy(player);
            }
            Destroy(gameObject);
        }
    }
}
