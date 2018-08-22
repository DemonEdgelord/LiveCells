using UnityEngine;

public class TurningPoint : MonoBehaviour
{

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Turningpoint"))
        {
            col.gameObject.GetComponent<Mvmnt>().Turn();
            Debug.Log("Colision detected");
        }
    }

}
