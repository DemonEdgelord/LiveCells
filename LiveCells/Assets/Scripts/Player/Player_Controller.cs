using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour {

    public Rigidbody2D rb;

    public float moveSpeed;
    public float jumpForce;
    public float moveInput;

    public bool grounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask ground;
    public float fallmultiplier;
    public float lowJumpMultiplier;


   void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallmultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.W))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }
    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, ground);

        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        if (FacingRight == false && moveInput > 0)
        {
            Flip();
        }
        else if (FacingRight == true && moveInput < 0)
        {
            Flip();
        }

        if (Input.GetKeyDown(KeyCode.W) && grounded == true)
        {
            rb.velocity = Vector2.up * jumpForce;
        }

    }

    private bool FacingRight = true;

    void Flip()
    {
        FacingRight = !FacingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    
}
