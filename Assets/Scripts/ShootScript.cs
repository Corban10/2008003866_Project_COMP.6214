using UnityEngine;

public class ShootScript : MonoBehaviour {
    public GameObject enemy;
    public static float bulletSpeed; //static so that it can be accessed from other scripts
    void Awake()
    {
        bulletSpeed = 15;
    }
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().transform.up * bulletSpeed;
    }
    
}
