using System.Collections;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    #region Objects
    public GameObject EnemyShot;
    public Transform ShotSpawn;
    private GameObject player;
    private Rigidbody2D enemy;
    System.Random R;
    #endregion

    #region States
    private float enemySpeed;
    private int enemyLives;
    private float fireRate;
    private float nextFire;
    #endregion

    #region Vectors and Quarternions
    Vector3 bulletOffset;
    Quaternion bulletRotation1;
    Quaternion bulletRotation2;
    #endregion

    void Awake()
    {
        //player object to find collision
        player = GameObject.Find("Player");
        // bullet state
        bulletOffset = new Vector3(0.3f, 0, 0);
        bulletRotation1.eulerAngles = new Vector3(0, 0, 170);
        bulletRotation2.eulerAngles = new Vector3(0, 0, -170);
        //enemy state
        fireRate = 3f;
        enemySpeed = 2;
        enemyLives = 6;
        //movement logic
        enemy = GetComponent<Rigidbody2D>();
        R = new System.Random();
        StartCoroutine("EnemyMotion");
        StartCoroutine("EnemyShoot");
    }
    IEnumerator EnemyMotion()
    {
        enemy.velocity = transform.up * enemySpeed;
        yield return new WaitForSeconds(1);
        while (true)
        {
            enemy.velocity -= new Vector2(0, 0.1f) * enemySpeed;
            enemy.velocity += new Vector2(-enemy.position.x * 0.25f, 0) * enemySpeed;
            yield return new WaitForSeconds(1);
        }
    }
    IEnumerator EnemyShoot()
    {
        yield return new WaitForSeconds(1);
        while(true)
        {
            Instantiate(EnemyShot, ShotSpawn.position - bulletOffset, bulletRotation1);
            Instantiate(EnemyShot, ShotSpawn.position, ShotSpawn.rotation);
            Instantiate(EnemyShot, ShotSpawn.position + bulletOffset, bulletRotation2);
            yield return new WaitForSeconds(2.5f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "SingleShot(Clone)" && PlayerScript.onUpperLayer == true)
        {
            enemyLives--;
            //PlayerScript.UpdateText();
            if (enemyLives < 1)
            {
                Destroy(gameObject);
                PlayerScript.score += 200;
            }
			Destroy(collision.gameObject);
        }
    }
}
