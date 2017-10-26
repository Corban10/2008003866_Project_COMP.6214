using UnityEngine;

public class LandMover : MonoBehaviour
{
    public GameObject player;
    private float landSpeed;
    void Awake()
    {
        player = GameObject.Find("Player");
        //landSpeed = -2.68f;
        landSpeed = -1.786f;
        GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().transform.up * landSpeed;
    }
}
