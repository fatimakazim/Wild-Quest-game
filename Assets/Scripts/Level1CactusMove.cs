using UnityEngine;

public class Level1CactusMove : MonoBehaviour
{
    public float frontPoint = 10.5f;    // Min Z
    public float backPoint = 22.7f;     // Max Z
    public int direction = 1;           // 1 = forward, 2 = backward
    public float speed = 2f;

    void Update()
    {
        if (direction == 1)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);

            if (transform.position.z > backPoint)
            {
                direction = 2;
            }
        }
        else if (direction == 2)
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime, Space.World);

            if (transform.position.z < frontPoint)
            {
                direction = 1;
            }
        }
    }
}
