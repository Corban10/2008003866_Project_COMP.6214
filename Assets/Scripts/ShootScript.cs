using UnityEngine;

public class ShootScript : MonoBehaviour {

    public float speed;
    void Start()
    {
        speed = 12;
        GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().transform.up * speed;
    }
}
