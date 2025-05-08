using UnityEngine;

public class LedgeScroll : MonoBehaviour
{
    public float speed = 0.01f; // Small speed for smoother motion
    public float minY = 106.72f;
    public float maxY = 130.71f;

    private float targetY;
    private bool isMovingUp = true;

    public float ledgeOffset = 1.0f;  // Adjust this value to space out the ledges

    void Start()
    {
        // Set the starting position based on the offset
        targetY = maxY + ledgeOffset; // Apply the offset to avoid collisions
    }

    void Update()
    {
        // Move smoothly towards the target position
        float step = speed * Time.deltaTime; // Movement step per frame
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, targetY, transform.position.z), step);

        // Reverse direction when reaching the target Y position
        if (transform.position.y >= maxY + ledgeOffset && isMovingUp)
        {
            isMovingUp = false;
            targetY = minY + ledgeOffset; // Set target to minY when reaching maxY
        }
        else if (transform.position.y <= minY + ledgeOffset && !isMovingUp)
        {
            isMovingUp = true;
            targetY = maxY + ledgeOffset; // Set target to maxY when reaching minY
        }
    }
}
