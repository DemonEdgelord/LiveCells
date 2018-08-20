using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTest : MonoBehaviour {

    public float maxHp;
    public float curHp;

    private void Start()
    {
        curHp = maxHp;
    }

    private void Update()
    {
        if (curHp <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDmg(float dmg)
    {
        curHp -= dmg;
    }

}
