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

    void FindPlayer()
    {
        rayHit = Physics2D.Raycast(rayOrigin.position, rayOrigin.forward, range);
        Debug.DrawRay();
    }

}
