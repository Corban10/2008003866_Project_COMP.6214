using UnityEngine;

public class EnemyShootScript : MonoBehaviour
{
    private float bulletSpeed;
    void Awake()
    {
        bulletSpeed = 8;
    }
    void Start()
    {
        //sets bullet into motion
        GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().transform.up * bulletSpeed;
    }
}
