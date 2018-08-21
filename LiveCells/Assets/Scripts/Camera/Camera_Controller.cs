using UnityEngine;

public class Camera_Controller : MonoBehaviour {

    public Transform target;

    public float smoothFactor;
    public Vector2 offset;

    private void FixedUpdate()
    {
        Vector2 desiredPosition = (Vector2)target.position + offset;
        Vector2 smoothedPosition = Vector2.Lerp(transform.position, desiredPosition, smoothFactor);

        transform.position = smoothedPosition;
    }
    private void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x,transform.position.y, -10);
    }

}
