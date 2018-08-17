using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour {

    private Rigidbody2D rb;

    //Horizontal Movement
    public float moveSpeed;
    public float moveInput;

    //Jumping
    public float jumpForce;
    public bool grounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask ground;
    public float fallmultiplier;
    public float lowJumpMultiplier;

    //Dashing
    public float dashSpeed;
    public float dashTime;
    public float setDashTime;
    public bool dashReady = true;
    public float dashCD;
    public float setDashCD;


   void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        dashTime = setDashTime;
    }


    void FixedUpdate()
    {
        //Horizontal Movement
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        //Checks if sprite needs to be fliped
        if (FacingRight == false && moveInput > 0)
        {
            Flip();
        }
        else if (FacingRight == true && moveInput < 0)
        {
            Flip();
        }

        //Jump
        grounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, ground); //Checks if player is grounded

        if (Input.GetKeyDown(KeyCode.W) && grounded == true) 
        {
            rb.velocity = Vector2.up * jumpForce;   //Makes player Jump
        }

        if (rb.velocity.y < 0) 
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallmultiplier - 1) * Time.deltaTime;    //Increases descent speed compared to ascend
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.W))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;     //Variable Jump height
        }

        //Dashing
        if (Input.GetKey(KeyCode.Space) && dashReady == true && moveInput != 0)
        {
            if (dashTime <= 0)
            {
                rb.velocity = new Vector2(0,0);
                dashReady = false;
                dashCD = setDashCD;
                dashTime = setDashTime;

            }
            else
            {
                dashTime -= Time.deltaTime;
                rb.velocity = new Vector2(rb.velocity.x * dashSpeed, 0);
            }
        }

        //Dash Cool Down
        if (dashCD > 0)
        {

            dashCD -= Time.deltaTime;
            if (dashCD <= 0)
            {
                dashReady = true;
            }
        }

    }

    //Flips Sprite
    private bool FacingRight = true;

    void Flip()
    {
        FacingRight = !FacingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
  
    
}
