using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    public GameObject EnemyShot;
    private GameObject player;
    private int turretLives;
    void Start()
    {
        turretLives = 4;
        player = GameObject.Find("Player");
        StartCoroutine("TurretShoot");
    }
    void Update()
    {
        transform.up = (player.transform.position - transform.position) / 1.5f;
    }
    IEnumerator TurretShoot()
    {
        yield return new WaitForSeconds(1);
        while (true)
        {
            Instantiate(EnemyShot, transform.position, transform.rotation);
            yield return new WaitForSeconds(2);
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
        if (collision.name == "SingleShot(Clone)" && PlayerScript.onUpperLayer == true)
        {
            Debug.Log("Enemy lives: " + turretLives);
            turretLives--;
            if (turretLives < 1)
            {
                Destroy(gameObject);
            }
        }
    }
}
