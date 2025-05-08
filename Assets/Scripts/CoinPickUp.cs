using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public AudioSource CoinAudio;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Move coin out of view instead of destroying (object pooling)
            transform.position = new Vector3(0, -1000, 0);

            // Play coin sound
            if (CoinAudio != null)
                CoinAudio.Play();

            // Increment coin counter
            GlobalCoins.CoinCount += 1;
        }
    }
}
