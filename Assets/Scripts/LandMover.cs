using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMover : MonoBehaviour
{
    public GameObject player;
    private float landSpeed;
    void Awake()
    {
        player = GameObject.Find("Player");
        //landSpeed = -2.68f;
        landSpeed = -1.786666666666667f;
        GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().transform.up * landSpeed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player && PlayerScript.onUpperLayer == false)
        {
            Debug.Log(collision);
            PlayerScript.playerLives--;
            player.transform.position = new Vector2(0, -5);
            Debug.Log(PlayerScript.playerLives);
            if (PlayerScript.playerLives < 1)
            {
                Destroy(player);
            }
        }
    }
}
