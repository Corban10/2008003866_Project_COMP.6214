using UnityEngine;

public class LandMover : MonoBehaviour
{
    //public GameObject player;
    private float landSpeed;
    void Awake()
    {
        //player = GameObject.Find("Player");
        landSpeed = -1.786f; 
        //controls how fast the object moves, set to negative value so that the transform.up method moves downwards instead of up
        GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().transform.up * landSpeed;
        //sets the objects velocity to transform.up * landspeed
    }
}
