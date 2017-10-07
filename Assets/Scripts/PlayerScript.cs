using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax;
}

public class PlayerScript : MonoBehaviour
{
    public Boundary boundary;
    public GameObject SingleShot;
    public Transform ShotSpawn;
    public static int playerLives;

    private float speed;
    private float fireRate;
    private float nextFire;
    private Vector2 minScale, maxScale, newScale;
    private float layerTransitionSpeed;
    private bool onUpperLayer;
    
    void Awake()
    {
        playerLives = 3;
        fireRate = 0.1F;
        nextFire = 0.0F;
        layerTransitionSpeed = 5;
        speed = 8;
        onUpperLayer = true;
        maxScale = GetComponent<Transform>().transform.localScale;
        minScale = maxScale / 2;
        newScale = maxScale;
    }
    void Update()
    {
        deathCheck();
        shootController();
    }
    private void FixedUpdate()
    {
        layerController();
        movementController();
    }
    void shootController()
    {
        if (Time.time > nextFire) //Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(SingleShot, ShotSpawn.position, ShotSpawn.rotation);
        }
    }
    void layerController() //Controls the pseudo Z axis layer system, changes player scale/speed
    {
        if (Input.GetKeyDown(KeyCode.F) && onUpperLayer)
        {
            newScale = minScale;
            speed = 8;
            ShootScript.bulletSpeed = 15; 
            // change variable from another script: bulletSpeed variable from ShootScript, maybe take this out later
            onUpperLayer = false;
        }
        else if (Input.GetKeyDown(KeyCode.R) && !onUpperLayer)
        {
            newScale = maxScale;
            speed = 10;
            ShootScript.bulletSpeed = 20;
            onUpperLayer = true;
        }
            GetComponent<Rigidbody2D>().transform.localScale = Vector2.Lerp(transform.localScale, newScale, Time.deltaTime * layerTransitionSpeed);
            boundaryClamper();
    }
    void movementController()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

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
    void deathCheck() //maybe make this an event?
    {
        if (playerLives < 1)
        {
            Destroy(gameObject);
        }
    }
}