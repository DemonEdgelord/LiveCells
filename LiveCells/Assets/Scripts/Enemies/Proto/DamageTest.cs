using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTest : MonoBehaviour {

    public float maxHp;
    public float curHp;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        curHp = maxHp;
    }

    private void Update()
    {
        if (curHp <= 0)
        {
            Destroy(gameObject);
        }
    }

    public float hitStunForce;
    public void TakeDmg(float dmg)  //This function is called by the player when doing damage
    {
        curHp -= dmg;
        rb.AddForce (new Vector2(hitStunForce,0));
    }

}
