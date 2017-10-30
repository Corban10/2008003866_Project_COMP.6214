using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax;
}

public class PlayerScript : MonoBehaviour
{
    #region Objects
    public Boundary boundary;
    public GameObject SingleShot;
    public GameObject EnemyShot;
    public GameObject Land1;
    public GameObject Land2;
    public GameObject Enemy;
    public Transform ShotSpawn;
    public Text scoreText;
    public Text livesText;
    public GameObject Explosion;
    #endregion

    #region Player Values
    public static int playerLives;
    public static bool onUpperLayer;
    private float speed;
    private float fireRate;
    private float nextFire;
    private Vector2 minScale, maxScale, newScale;
    private float layerTransitionSpeed;
    public static int score;
    private static float deathBuffer;
    #endregion

    void Awake()
    {
        #region Initializations
        playerLives = 3;
        fireRate = 0.15F;
        layerTransitionSpeed = 5;
        speed = 12;
        onUpperLayer = true;
        maxScale = GetComponent<Transform>().transform.localScale;
        minScale = maxScale / 2;
        newScale = maxScale;
        score = 0;
        StartCoroutine(UpdateText());
        #endregion
    }
    void FixedUpdate()
    {
        shootController();
        layerController();
        movementController();
    }
    void shootController()
    {
        if (Time.time > nextFire && Input.GetButton("Fire1"))
        {
            nextFire = Time.time + fireRate;
            Instantiate(SingleShot, ShotSpawn.position - new Vector3(0.3f, 0, 0), ShotSpawn.rotation);
            Instantiate(SingleShot, ShotSpawn.position - new Vector3(-0.3f, 0, 0), ShotSpawn.rotation);
        }
    }
    void layerController()
    {
        if (Input.GetKeyDown(KeyCode.F) && onUpperLayer)
        {
            newScale = minScale;
            onUpperLayer = false;
        }
        else if (Input.GetKeyDown(KeyCode.R) && !onUpperLayer)
        {
            newScale = maxScale;
            onUpperLayer = true;
        }
            GetComponent<Rigidbody2D>().transform.localScale = Vector2.Lerp(transform.localScale, newScale, Time.deltaTime * layerTransitionSpeed);
            boundaryClamper();
    }
    void movementController()
    {
        float moveHorizontal = (float)Math.Round(Input.GetAxis("Horizontal"), 2);
        float moveVertical = (float)Math.Round(Input.GetAxis("Vertical"), 2);
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        GetComponent<Rigidbody2D>().velocity = movement * speed;
        boundaryClamper();
    }
    void boundaryClamper()
    {
        GetComponent<Rigidbody2D>().position = new Vector2
        (
            Mathf.Clamp(GetComponent<Rigidbody2D>().position.x, boundary.xMin, boundary.xMax),
            Mathf.Clamp(GetComponent<Rigidbody2D>().position.y, boundary.yMin, boundary.yMax)
        );
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (Time.time > deathBuffer && (collision.tag == EnemyShot.tag || collision.tag == Enemy.tag))
        {
            PlayerTookDamage(collision);
            Destroy(collision.gameObject);
        }
        if (Time.time > deathBuffer && !onUpperLayer && (collision.tag == Land1.tag || collision.tag == Land2.tag))
        {
            PlayerTookDamage(collision);
        }
    }
    void PlayerTookDamage(Collider2D collision)
    {
        playerLives--;
        StartCoroutine (FlashDamage(GetComponent<SpriteRenderer>(), 3));
        if (PlayerScript.playerLives < 1)
            PlayerDied();
        transform.position = new Vector2(0, -5);
        deathBuffer = Time.time + 1f;
    }
    void PlayerDied()
    {
        livesText.text = "Lives: " + PlayerScript.playerLives.ToString();
        GetComponent<WriteNewScore>().GetNewScore();
        GetComponent<WriteNewScore>().UpdateScore();
        Instantiate(Explosion, transform.position, transform.rotation);
        gameObject.SetActive(false);
        //send score event
    }
    IEnumerator UpdateText() //convert to event
    {
        while (PlayerScript.playerLives > 0)
        {
            scoreText.text = "Score: " + PlayerScript.score.ToString();
            livesText.text = "Lives: " + PlayerScript.playerLives.ToString();
            yield return new WaitForSeconds(0.2f);
        }
    }
    IEnumerator FlashDamage(SpriteRenderer sr, int times) 
    {
        for (int i = 0; i < times; i++) 
        {
            sr.color = new Color (1f, 1f, 1f, 0.3f); //Red, Green, Blue, Alpha/Transparency
            yield return new WaitForSeconds (.1f);
            sr.color = Color.white; //White is the default "color" for the sprite, if you're curious.
            yield return new WaitForSeconds (.1f);
        }
    }
}