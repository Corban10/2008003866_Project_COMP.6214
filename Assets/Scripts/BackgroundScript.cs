using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour {

    private float Speed = 0.1f;
    private Renderer _renderer;
    private void Start()
    {
        _renderer = GetComponent<Renderer>();
    }
    private void Update()
    {
        _renderer.material.mainTextureOffset = new Vector2(0, Time.time * Speed);
    }
}
