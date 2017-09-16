using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMover : MonoBehaviour
{
    public static float landSpeed; //static so that it can be accessed from other scripts
    void Awake()
    {
        landSpeed = -2;
    }
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().transform.up * landSpeed;
    }
}
