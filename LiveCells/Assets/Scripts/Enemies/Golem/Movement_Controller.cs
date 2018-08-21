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

    void FindPlayer()
    {
        Physics2D.Raycast(rayOrigin.position, rayOrigin.forward, rayHit, 5);
        Debug.DrawRay();
    }

}
