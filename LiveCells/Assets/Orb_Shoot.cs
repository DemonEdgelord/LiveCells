using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb_Shoot : MonoBehaviour
{

    //needed variables for shooting
    Transform FirePoint; //to make bulltes come out of orb
    public float fireRate = 0;
    public float Damage = 10;
    float TimeToFire = 0;

    private void Awake()
    {
        FirePoint = transform.Find("Fire_point");
        if (FirePoint == null)
        {
            Debug.LogError("NO FIRE POINT");
        }
    }



    // Update is called once per frame
    void Update()
    {
        Shoot();

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



    //Shooting 
    public LayerMask WhatToHit;
    void Shoot()
    {
        Vector2 mousePostition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 FirePointPosition = new Vector2(FirePoint.position.x, FirePoint.position.y);
        RaycastHit2D hit = Physics2D.Raycast(FirePointPosition, mousePostition - FirePointPosition, 100, WhatToHit);
        Debug.DrawLine(FirePointPosition, (mousePostition - FirePointPosition) * 100, Color.cyan);
        if (hit.collider != null)
        {
            Debug.DrawLine(FirePointPosition, hit.point, Color.red);
            Debug.Log("we hit" + hit.collider.name + " and did " + Damage + "Damage thats a lot of Damge");
        }


    }
}
