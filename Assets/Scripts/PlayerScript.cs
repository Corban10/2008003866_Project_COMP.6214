using System;
using System.Collections;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax;
}

public class PlayerScript : MonoBehaviour
{
    #region Objects
    private PolygonCollider2D playerCollider;
    public Boundary boundary;
    public GameObject SingleShot;
    public Transform ShotSpawn;
    #endregion

    #region Player Values
    public static int playerLives;
    private float speed;
    private float fireRate;
    private float nextFire;
    private Vector2 minScale, maxScale, newScale;
    private float layerTransitionSpeed;
    public static bool onUpperLayer;
    #endregion

    void Awake()
    {
        #region Initializations
        playerCollider = GetComponent<PolygonCollider2D>();
        playerLives = 3;
        fireRate = 0.15F;
        layerTransitionSpeed = 5;
        speed = 12;
        onUpperLayer = true;
        maxScale = GetComponent<Transform>().transform.localScale;
        minScale = maxScale / 2;
        newScale = maxScale;
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
        if (Time.time > nextFire && Input.GetButton("Fire1") && Time.time > nextFire)
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
    
}