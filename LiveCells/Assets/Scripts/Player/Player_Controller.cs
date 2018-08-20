using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public float projectileVelocity;
    private Rigidbody2D rb;

    //needed variables for shooting
    Transform FirePoint; //to make bulltes come out of orb
    public float fireRate = 0;
    public float Damage = 10;
    float TimeToFire = 0;



    void Start()
    {
          projectileVelocity = 3;
    rb = GetComponent<Rigidbody2D>();

        dashTime = setDashTime;

        FirePoint = transform.Find("Fire_point");
        if (FirePoint == null)
        {
            Debug.LogError("NO FIRE POINT");
        }
        
}



    void FixedUpdate()
    {

        Walk();

        Jump();

        Dash();

        if (fireRate == 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1") && (Time.time > TimeToFire))
            {
                TimeToFire = Time.time + 1 / fireRate;
                Shoot();
            }
        }

    }

    //Horizontal Movement
    public float moveSpeed;
    public float moveInput;

    void Walk()
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
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask ground;
    public float fallmultiplier;
    public float lowJumpMultiplier;

    void Jump()
    {
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


    //projectile
    private List<GameObject> Projectiles = new List<GameObject>();
    
    public GameObject projectilePrefab;

    //Shooting 
    public LayerMask WhatToHit;
    void Shoot()
    {
        Vector2 mousePostition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 FirePointPosition = new Vector2(FirePoint.position.x, FirePoint.position.y);
        GameObject bullet = (GameObject)Instantiate(projectilePrefab, transform.position, transform.rotation);
        Projectiles.Add(bullet);

        for(int i = 0; i < Projectiles.Count; i++) {
            GameObject goBullet = Projectiles[i];
            if(goBullet != null){
                goBullet.transform.Translate(new Vector3(0, 1) * Time.deltaTime * projectileVelocity);

                Vector3 bulletScreenPos = Camera.main.WorldToScreenPoint(goBullet.transform.position);
                if (bulletScreenPos.y >=Screen.height || bulletScreenPos.y <= 0)
                {
                    Object.Destroy(goBullet);
                    Projectiles.Remove(goBullet);
                }

            }

        }



    }

}


