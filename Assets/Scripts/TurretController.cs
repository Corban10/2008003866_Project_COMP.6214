using System.Collections;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    public GameObject EnemyShot;
    public GameObject Explosion;
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
        transform.position = transform.parent.position;
        if (player != null)
        {
            transform.up = (player.transform.position - transform.position) / 1.5f;
        }
    }
    IEnumerator TurretShoot()
    {
        yield return new WaitForSeconds(1);
        while (true)
        {
            Instantiate(EnemyShot, transform.position, transform.rotation);
            //instantiate a single bullet for turrets
            yield return new WaitForSeconds(2); //shoot every 2 seconds
        }
    }
    IEnumerator FlashDamage(SpriteRenderer sr, int times) 
    {
        for (int i = 0; i < times; i++) //flash N times
        {
            sr.color = new Color (1f, 1f, 1f, 0.3f); //turns opacity down
            yield return new WaitForSeconds (.1f); // flash every 0.1 seconds
            sr.color = Color.white; //resets colour value
            yield return new WaitForSeconds (.1f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if collision is SingleShot, and player is on the upper layer
        if (collision.name == "SingleShot(Clone)" && PlayerScript.onUpperLayer == false)
        {
            turretLives--; // takes damage
            StartCoroutine (FlashDamage(GetComponent<SpriteRenderer>(), 1)); // flash
            if (turretLives < 1) 
                // if lives less than 1, instantiate explosion and destroy gameobject
            {
                Instantiate(Explosion, transform.position, transform.rotation);
                Destroy(gameObject);
                PlayerScript.score += 100; //add points to score
            }
            Destroy(collision.gameObject); //destroy bullet
        }
    }
}
