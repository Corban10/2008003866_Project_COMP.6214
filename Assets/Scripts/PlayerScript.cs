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
        #endregion
    }
    void Start()
    {
        StartCoroutine(UpdateText());
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
    void layerController() //Controls the pseudo Z axis layer system, changes player scale/speed
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
        if (Time.time > deathBuffer)
        {
            if (collision.tag == EnemyShot.tag || collision.tag == Enemy.tag)
            {
                PlayerTookDamage(collision);
                Destroy(collision.gameObject);
            }
        }
        if (Time.time > deathBuffer && !onUpperLayer)
        {
            if (collision.tag == Land1.tag || collision.tag == Land2.tag)
            {
                PlayerTookDamage(collision);
            }
        }
    }
    void PlayerTookDamage(Collider2D collision)
    {
        playerLives--;
        Debug.Log(playerLives);
        transform.position = new Vector2(0, -5);
        if (PlayerScript.playerLives < 1)
            PlayerDied();
        //Debug.Log(string.Format("{0} > {1} = {2}", Time.time, deathBuffer, Time.time > deathBuffer));
        deathBuffer = Time.time + 1f;
    }
    void PlayerDied()
    {
        Destroy(gameObject);
        //gameover and restart screen
    }
    IEnumerator UpdateText()
    {
        while (true)
        {
            scoreText.text = "Score: " + score.ToString();
            livesText.text = "Lives: " + playerLives.ToString();
            yield return new WaitForSeconds(0.2f);
        }
    }
}