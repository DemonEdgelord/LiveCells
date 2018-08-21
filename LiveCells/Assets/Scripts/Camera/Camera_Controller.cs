using UnityEngine;

public class Camera_Controller : MonoBehaviour {

    public Transform target;

    public float smoothFactor;
    public Vector2 offset;

    private void FixedUpdate()
    {
        Vector2 desiredPosition = (Vector2)target.position + offset;  //Calculates the offset the camera has to be on in relation to the player (X, Y axis only)
        Vector2 smoothedPosition = Vector2.Lerp(transform.position, desiredPosition, smoothFactor); //Calculates a posiyion for the camera to cmoothly follow the player

        transform.position = smoothedPosition;  //Changes the camera postion
    }
    private void LateUpdate()  //Works like regular Update() function but runs after it, thus LateUpdate()
    {
        transform.position = new Vector3(transform.position.x,transform.position.y, -10);  //Moves camera back on the Z axis so the camera isn't places directly onto the player
    }

}
