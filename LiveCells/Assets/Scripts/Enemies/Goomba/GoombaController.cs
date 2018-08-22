using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaController : MonoBehaviour {

    private Rigidbody2D rb;

    void starrt()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public float speed;
    void Update()
    {
        rb.velocity = new Vector2(5, 0);
    }
   
}
