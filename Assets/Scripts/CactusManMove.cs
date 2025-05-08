using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float leftPoint = 10.5f;     // Min X
    public float rightPoint = 22.7f;    // Max X
    public int direction = 1;           // 1 = right, 2 = left
    public float speed = 2f;

    void Update()
    {
        if (direction == 1)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);

            if (transform.position.x > rightPoint)
            {
                direction = 2;
            }
        }
        else if (direction == 2)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);

            if (transform.position.x < leftPoint)
            {
                direction = 1;
            }
        }
    }
}
