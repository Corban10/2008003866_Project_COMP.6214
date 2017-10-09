using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax;
}

public class PlayerScript : MonoBehaviour
{
    private CapsuleCollider2D playerCollider;
    public Boundary boundary;
    public GameObject SingleShot;
    public Transform ShotSpawn;
    public static int playerLives;

    private float speed;
    private float fireRate;
    private float nextFire;
    private Vector2 minScale, maxScale, newScale;
    private float layerTransitionSpeed;
    public bool onUpperLayer;
    
    void Awake()
    {
        playerCollider = GetComponent<CapsuleCollider2D>();
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
    void Start()
    {
        StartCoroutine("LayerCheck");
    }
    void FixedUpdate()
    {
        shootController();
        layerController();
        movementController();
    }
    void shootController()
    {
        if (Time.time > nextFire) //Input.GetButton("Fire1") && Time.time > nextFire)
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
    IEnumerator LayerCheck() //disable collider on land objects (2), add turrets to this later
    {
        while (true) //while loop instead of StartCoroutine(Spawner()); Maybe less memory use, and optional break;
        {
            if (onUpperLayer && GameObject.Find("Circle(Clone)"))
            {
                Physics2D.IgnoreCollision(GameObject.Find("Circle(Clone)").GetComponent<CircleCollider2D>(), playerCollider, true);
            }
            if (onUpperLayer && GameObject.Find("Square(Clone)"))
            {
                Physics2D.IgnoreCollision(GameObject.Find("Square(Clone)").GetComponent<BoxCollider2D>(), playerCollider, true);
            }

            if (onUpperLayer == false && GameObject.Find("Circle(Clone)"))
            {
                Physics2D.IgnoreCollision(GameObject.Find("Circle(Clone)").GetComponent<CircleCollider2D>(), playerCollider, false);
            }
            if (onUpperLayer == false && GameObject.Find("Square(Clone)"))
            {
                Physics2D.IgnoreCollision(GameObject.Find("Square(Clone)").GetComponent<BoxCollider2D>(), playerCollider, false);
            }
            yield return new WaitForSeconds(0.5f);
            Debug.Log("Repeat");
        }
        //StartCoroutine("LayerCheck");
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
}