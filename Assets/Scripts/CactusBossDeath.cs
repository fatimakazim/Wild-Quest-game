using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // Needed for scene loading

public class CactusBossDeath : MonoBehaviour
{
    public GameObject CactusMan;

    private void OnTriggerEnter(Collider col)
    {
        StartCoroutine(HandleStomp());
    }

    IEnumerator HandleStomp()
    {
        // Disable this collider so it doesn't trigger again
        GetComponent<BoxCollider>().enabled = false;

        // Disable movement script on the cactus
        if (CactusMan.TryGetComponent(out MonoBehaviour cactusMoveScript))
        {
            cactusMoveScript.enabled = false;
        }

        // Shrink cactus visually (squish)
        CactusMan.transform.localScale -= new Vector3(0f, 1.8f, 0f);
        CactusMan.transform.localPosition -= new Vector3(0f, 1.4f, 0f);

        // Wait before despawning
        yield return new WaitForSeconds(1f);

        // Move cactus far offscreen
        CactusMan.transform.position = new Vector3(0f, -1000f, 0f);

        // Wait before changing scene
        yield return new WaitForSeconds(2f);

        // Load scene index 5
        SceneManager.LoadScene("GameVictory");
    }
}
