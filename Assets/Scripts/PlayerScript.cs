using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax;
}

public class PlayerScript : MonoBehaviour
{
    public float speed;
    public bool onUpperLayer;
    public Boundary boundary;

    public GameObject SingleShot;
    public Transform ShotSpawn;

    public float fireRate = 0.5F;
    private float nextFire = 0.0F;

    private Vector2 minScale;
    private Vector2 maxScale;
    private Vector2 newScale;
    public float layerTransitionSpeed = 5;
    void Start()
    {
        speed = 8;
        onUpperLayer = true;
        maxScale = GetComponent<Transform>().transform.localScale;
        minScale = maxScale / 2;
        newScale = maxScale;
    }
    void Update()
    {
        shootController();
        movementController();
        layerController();
    }
    void shootController()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
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
            speed = 10;
            onUpperLayer = false;
        }
        else if (Input.GetKeyDown(KeyCode.R) && !onUpperLayer)
        {
            newScale = maxScale;
            speed = 8;
            onUpperLayer = true;
        }
            transform.localScale = Vector2.Lerp(transform.localScale, newScale, Time.deltaTime * layerTransitionSpeed);
    }
    void movementController() //Controls movement with WASD
    {
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