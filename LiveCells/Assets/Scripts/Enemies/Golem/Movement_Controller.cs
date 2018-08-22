using UnityEngine;

public class Movement_Controller : MonoBehaviour
{

    public Transform playerLocation;

    private void Update()
    {
        FindPlayer();
    }

    private RaycastHit2D rayHit;
    public Transform rayOrigin;
    public float range;
    public LayerMask rayIgnore;

    void FindPlayer()
    {
        rayHit = Physics2D.Raycast(rayOrigin.position, Vector2.right, range, rayIgnore);
        Debug.DrawLine(rayOrigin.position, Vector2.right*range, Color.white);
        if (rayHit.collider != null)
        {
            Debug.DrawLine(rayOrigin.position, rayHit.collider.transform.position, Color.green);
            Debug.Log(rayHit.transform.name);
        }
    }


}
