using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowLevel2 : MonoBehaviour
{
    public Transform Player;
    public float DistanceFromPlayer = 5;
    public float DistanceInFrontOfPlayer = 10;
    public float HeightOffset = 2f; // Added to keep camera above player

    // Update is called once per frame
    void Update()
    {
        Vector3 temp = transform.position;
        temp.z = Player.position.z - DistanceFromPlayer;
        temp.x = Player.position.x + DistanceInFrontOfPlayer;
        temp.y = Player.position.y + HeightOffset; // Y follows player
        transform.position = temp;
    }
}
