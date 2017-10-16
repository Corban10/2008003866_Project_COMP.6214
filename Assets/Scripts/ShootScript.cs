using UnityEngine;

public class ShootScript : MonoBehaviour 
{
    private float bulletSpeed;
    void Awake()
    {
        bulletSpeed = 30;
    }
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().transform.up * bulletSpeed;
    }
}
