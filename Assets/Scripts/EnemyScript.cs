using System;
using System.Collections;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    #region Objects
    public GameObject EnemyShot;
    public GameObject Explosion;
    public Transform ShotSpawn;
    private Rigidbody2D enemy;
    #endregion

    #region States
    private float enemySpeed;
    private int enemyLives;
    #endregion

    #region Vectors and Quarternions
    Vector3 bulletOffset;
    Quaternion bulletRotation1;
    Quaternion bulletRotation2;
    #endregion

    void Awake()
    {
        // bullet state
        bulletOffset = new Vector3(0.3f, 0, 0);
        bulletRotation1.eulerAngles = new Vector3(0, 0, 170);
        bulletRotation2.eulerAngles = new Vector3(0, 0, -170);
        //enemy state
        enemySpeed = 2;
        enemyLives = 6;
        //movement logic
        enemy = GetComponent<Rigidbody2D>();
        StartCoroutine("EnemyMotion");
        StartCoroutine("EnemyShoot");
    }
    IEnumerator EnemyMotion()
    {
        enemy.velocity = transform.up * enemySpeed; //sets motion downwards
        yield return new WaitForSeconds(1);
        while (true)
        {
            enemy.velocity -= new Vector2(0, 0.1f) * enemySpeed; //resets x velocity
            enemy.velocity += new Vector2(-enemy.position.x * 0.25f, 0) * enemySpeed;
            // adds x velocity towards the middle of screen
            yield return new WaitForSeconds(1); //wait 1 second till next x motion change
        }
    }
    IEnumerator EnemyShoot() //instantiates 3 bullets with different rotations
    {
        yield return new WaitForSeconds(1); //1 second initial buffer
        while(true) //infinite coroutine loop until game restart or enemy death.
        { 
            Instantiate(EnemyShot, ShotSpawn.position - bulletOffset, bulletRotation1);
            Instantiate(EnemyShot, ShotSpawn.position, ShotSpawn.rotation);
            Instantiate(EnemyShot, ShotSpawn.position + bulletOffset, bulletRotation2);
            yield return new WaitForSeconds(2.5f); //every 2.5 seconds
        }
    }
    IEnumerator FlashDamage(SpriteRenderer sr, int times) 
    {
        for (int i = 0; i < times; i++) 
        {
            sr.color = new Color (1f, 1f, 1f, 0.3f);
            yield return new WaitForSeconds (.1f);
            sr.color = Color.white;
            yield return new WaitForSeconds (.1f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "SingleShot(Clone)" && PlayerScript.onUpperLayer == true)
        {
            enemyLives--;
            StartCoroutine (FlashDamage(GetComponent<SpriteRenderer>(), 1));
            if (enemyLives < 1)
            {
                Instantiate(Explosion, transform.position, transform.rotation);
                Destroy(gameObject);
                PlayerScript.score += 200;
            }
			Destroy(collision.gameObject);
        }
    }
}
