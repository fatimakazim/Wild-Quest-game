using UnityEngine;

public class MushroomRedMoveUpperLevel : MonoBehaviour
{
    public float leftPoint = 0f;   // Min X
    public float rightPoint = 8f;  // Max X
    public int direction = 1;      // 1 = right, 2 = left
    public float speed = 2f;

    private Rigidbody rb;

    void Start()
    {
        transform.parent = null;

        rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            // Freeze rotation so it doesn't tip over
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }
    }

    void FixedUpdate()
    {
        if (rb == null) return;

        // Move mushroom along X-axis
        Vector3 movement = (direction == 1 ? Vector3.right : Vector3.left) * speed;
        rb.velocity = new Vector3(movement.x, rb.velocity.y, 0f);

        // Flip direction at bounds and clamp position
        if (transform.position.x >= rightPoint)
        {
            direction = 2;
            transform.position = new Vector3(rightPoint, transform.position.y, transform.position.z);
        }
        else if (transform.position.x <= leftPoint)
        {
            direction = 1;
            transform.position = new Vector3(leftPoint, transform.position.y, transform.position.z);
        }
    }
}
