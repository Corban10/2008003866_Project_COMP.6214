using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax;
}

public class PlayerScript : MonoBehaviour
{
    public float lockRotation = 0;
    public float speed;
    public bool onUpperLayer;
    public Boundary boundary;

    public GameObject SingleShot;
    public Transform ShotSpawn;

    public float fireRate = 0.5F;
    private float nextFire = 0.0F;
    void Start()
    {
        speed = 8;
        boundary.xMin = -5;
        boundary.xMax = 5;
        boundary.yMin = -8;
        boundary.yMax = 8;
        onUpperLayer = true;
    }
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(SingleShot, ShotSpawn.position, ShotSpawn.rotation);
        }
    }
    void FixedUpdate()
    {
        movementController();
        layerController();
    }
    void layerController()
    {
        Vector2 scale = GetComponent<Transform>().transform.localScale;
        if (Input.GetKeyDown(KeyCode.F) && onUpperLayer)
        {
            GetComponent<Transform>().transform.localScale = scale / 2;
            speed = 10;
            onUpperLayer = false;
        }
        else if (Input.GetKeyDown(KeyCode.R) && !onUpperLayer)
        {
            GetComponent<Transform>().transform.localScale = scale * 2;
            speed = 8;
            onUpperLayer = true;
        }
    }
    void movementController()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, lockRotation, lockRotation);
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        GetComponent<Rigidbody2D>().velocity = movement * speed;
        GetComponent<Rigidbody2D>().position = new Vector2
        (
            Mathf.Clamp(GetComponent<Rigidbody2D>().position.x, boundary.xMin, boundary.xMax),
            Mathf.Clamp(GetComponent<Rigidbody2D>().position.y, boundary.yMin, boundary.yMax)
        );
    }
}