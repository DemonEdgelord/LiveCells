using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Stat_Tracker : MonoBehaviour {

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
        if (curHp > maxHp)
        {
            curHp = maxHp;
        }
    }

}
