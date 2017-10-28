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
            yield return new WaitForSeconds(2);
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
        
        if (collision.name == "SingleShot(Clone)" && PlayerScript.onUpperLayer == false)
        {
            turretLives--;
            StartCoroutine (FlashDamage(GetComponent<SpriteRenderer>(), 1));
            if (turretLives < 1)
            {
                Instantiate(Explosion, transform.position, transform.rotation);
                Destroy(gameObject);
                PlayerScript.score += 100;
            }
            Destroy(collision.gameObject);
        }
    }
}
