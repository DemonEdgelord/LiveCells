using UnityEngine;

public class Mvmnt : MonoBehaviour
{

    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public float speed;
    public float direction = 1;

    void FixedUpdate()
    {
        rb.velocity = new Vector2(5 * direction, rb.velocity.y);
    }
    public void Turn()
    {
        direction = direction * -1;
    }

}
