using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turning : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Turningpoint"))
        {
            col.GetComponent<controllergoomba>().turn();
        }
    }

}
