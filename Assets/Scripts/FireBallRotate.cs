using UnityEngine;

public class FireBallRotate : MonoBehaviour
{
    
    void Update()
    {
        transform.Rotate(0, 0, -1, Space.World);
    }
    
}