using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour {

   private Rigidbody2D rb;

   void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        dashTime = setDashTime;

    }


    void FixedUpdate()
    {

        Walk();

        Jump();
        
        Dash();

        Attack();

    }

    //Horizontal Movement
    public float moveSpeed;
    public float moveInput;

    void Walk()
    {
        if (attacking == false)
        {
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

    //Jumping
    public float jumpForce;
    public bool grounded;
    public Transform groundCheck1;
    public Transform groundCheck2;
    public float checkRadius;
    public LayerMask ground;
    public float fallmultiplier;
    public float lowJumpMultiplier;

    void Jump()
    {
        grounded = Physics2D.OverlapCircle(groundCheck1.position, checkRadius, ground) || Physics2D.OverlapCircle(groundCheck2.position, checkRadius, ground); //Checks if player is grounded

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
    }

    //Dashing
    public float dashSpeed;
    public float dashTime;
    public float setDashTime;
    public bool dashReady = true;
    public float dashCD;
    public float setDashCD;
    public bool dashing = false;
    private float dashDirec;

    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.Space) && dashReady == true && dashing == false)
        {
            dashing = true;
            if (FacingRight == true)    //Checks player direction and choses dash direction aproprietly
            {
                dashDirec = 1;
            } 
            else if (FacingRight == false)
            {
                dashDirec = -1;
            }
        }
        if (dashing == true)
        {
            dashTime -= Time.deltaTime;     //Keeps track of dash lenth
            rb.velocity = new Vector2(dashDirec * dashSpeed, 0);

            if (dashTime <= 0)      //Stops dash and puts it on cooldown
            {
                rb.velocity = new Vector2(0, 0);
                dashReady = false;
                dashCD = setDashCD;
                dashTime = setDashTime;
                dashing = false;

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

    //Melee Attack

    public float atkDmg;
    private bool attacking = false;
    public Collider2D hitBox;
    public float setAtkTimer;
    public float atkTimer;

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && attacking == false)
        {
            attacking = true;
            atkTimer = setAtkTimer;
            hitBox.enabled = true;  //Enables the hitbox
            if (grounded)  //Stops player movement while attacking unless already moving horizontally mid air
            {
                rb.velocity = new Vector2(0, 0);
            }
            
        }

        if (attacking)
        {
            if (atkTimer > 0)
            {
                atkTimer -= Time.deltaTime;  //The duration of tehr attack or active time of the hitbox
            }
            else
            {
                attacking = false;
                hitBox.enabled = false;
            }
        }
    }

    //Deals damage to the enemy
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.isTrigger == false && col.CompareTag("Enemy"))  //Calls damage function if the colision is with the enemy
        {
            col.gameObject.GetComponent<DamageTest>().TakeDmg(atkDmg);  //Calls teh TakeDmg() script component from whatever it colides with and sends the damage amoun
        }
            
    }
    
}
