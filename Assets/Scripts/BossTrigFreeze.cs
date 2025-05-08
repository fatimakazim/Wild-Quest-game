using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public GameObject mainCam;
    public GameObject backWall;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //mainCam.GetComponent<CameraFollow>().enabled = false;
            backWall.SetActive(true);
        }
    }
}
