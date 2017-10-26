using UnityEngine;

public class EnemyShootScript : MonoBehaviour
{
    public GameObject player;
    private float bulletSpeed;
    public static float deathBuffer;
    void Awake()
    {
        player = GameObject.Find("Player");
        bulletSpeed = 8;
    }
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().transform.up * bulletSpeed;
    }
}
