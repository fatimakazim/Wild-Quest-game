
using UnityEngine;

public class LifePickup : MonoBehaviour
{
    public AudioSource lifeSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            transform.position = new Vector3(0f, -1000f, 0f);
            lifeSound.Play();
            GlobalLives.LivesAmount += 1;
        }
    }
}

