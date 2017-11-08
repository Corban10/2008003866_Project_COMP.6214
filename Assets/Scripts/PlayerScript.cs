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
    void FixedUpdate() //logic that needs to be called every frame is here.
    {
        shootController();
        layerController();
        movementController();
    }
    void shootController()
    {
        // if time passed is greater than the nextFire wait buffer (0.15 seconds)
        // and the fire button is pressed
        if (Time.time > nextFire && Input.GetButton("Fire1")) 
        {
            nextFire = Time.time + fireRate;
            //shoot by instantiating two bullets
            Instantiate(SingleShot, ShotSpawn.position - new Vector3(0.3f, 0, 0), ShotSpawn.rotation); 
            Instantiate(SingleShot, ShotSpawn.position - new Vector3(-0.3f, 0, 0), ShotSpawn.rotation);
        }
    }
    void layerController()
    {
        if (Input.GetKeyDown(KeyCode.F) && onUpperLayer) // if F is pressed then move to lower layer
        {
            newScale = minScale;
            onUpperLayer = false; // this boolean value tells us whether or not player is on a certain layer
        }
        else if (Input.GetKeyDown(KeyCode.R) && !onUpperLayer)  // if r is pressed then move to Upper layer
        {
            newScale = maxScale;
            onUpperLayer = true;
        }
            // Lerp function to smooth the transition-resize animation
            GetComponent<Rigidbody2D>().transform.localScale = Vector2.Lerp(transform.localScale, newScale, Time.deltaTime * layerTransitionSpeed);
            boundaryClamper(); //clamp player to boundary after resize
    }
    void movementController() // this method gets user input and translates it into movement velocity
    {
        // Input.GetAxis gets user input and converts it to a value from 0 to 1
        float moveHorizontal = (float)Math.Round(Input.GetAxis("Horizontal"), 2); //rounding it to 2 decimal values
        float moveVertical = (float)Math.Round(Input.GetAxis("Vertical"), 2); //to make animation less smooth
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        GetComponent<Rigidbody2D>().velocity = movement * speed;
        boundaryClamper();
    }
    void boundaryClamper()
    {
        //clamps player rigidbody2d to boundary (values set in project editor)
        GetComponent<Rigidbody2D>().position = new Vector2 
        (
            Mathf.Clamp(GetComponent<Rigidbody2D>().position.x, boundary.xMin, boundary.xMax),
            Mathf.Clamp(GetComponent<Rigidbody2D>().position.y, boundary.yMin, boundary.yMax)
        );
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        //if player is hit by an enemy or obstacle and time since last death is greater than 1 second
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
    void PlayerTookDamage(Collider2D collision) //method for when player takes damage from any source.
    {
        playerLives--; //decrement playerlives
        StartCoroutine (FlashDamage(GetComponent<SpriteRenderer>(), 3)); //flashes sprite 3 times
        if (PlayerScript.playerLives < 1) //if player lives < 1 the player has died.
            PlayerDied();
        transform.position = new Vector2(0, -5); //reset player position
        deathBuffer = Time.time + 1f; //player cant die in quick succession, addds 1 second to death buffer
    }
    void PlayerDied()
    {
        livesText.text = "Lives: " + PlayerScript.playerLives.ToString(); //updates displayed lives to 0
        GetComponent<WriteNewScore>().GetNewScore(); 
        GetComponent<WriteNewScore>().UpdateScore(); //gets new score and adds it to highscores if high enough
        Instantiate(Explosion, transform.position, transform.rotation); //creat explosion
        gameObject.SetActive(false); //disable player game object on death
    }
    IEnumerator UpdateText()
    {
        while (PlayerScript.playerLives > 0) // if player is alive, update text
        {
            scoreText.text = "Score: " + PlayerScript.score.ToString(); //static variables from Playerscript class.
            livesText.text = "Lives: " + PlayerScript.playerLives.ToString();
            yield return new WaitForSeconds(0.2f); // 5 times per second
        }
    }
    IEnumerator FlashDamage(SpriteRenderer sr, int times) 
    {
        for (int i = 0; i < times; i++) 
        {
            sr.color = new Color (1f, 1f, 1f, 0.3f); //Red, Green, Blue, Alpha/Transparency
            yield return new WaitForSeconds (.1f);
            sr.color = Color.white; //White is the default colour
            yield return new WaitForSeconds (.1f);
        }
    }
}