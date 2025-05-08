using System.Collections;
using UnityEngine;
using UnityEngine.UI;  // Make sure this is included for the Image component

public class Pipe001Exit : MonoBehaviour
{
    public GameObject PipeEntry;
    public GameObject MainCam;
    public GameObject SecondCam;
    public GameObject MainPlayer;
    public GameObject FadeScreen;   // FadeScreen is now a GameObject
    public AudioSource PipeSound;

    private bool isTransitioning = false;

    private Image fadeImage;   // Reference to the Image component for the fade effect

    private void Start()
    {
        fadeImage = FadeScreen.GetComponent<Image>();  // Get the Image component on the GameObject
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTransitioning)
        {
            StartCoroutine(FadeAndExitPipe());
        }
    }

    IEnumerator FadeAndExitPipe()
    {
        isTransitioning = true;

        // Play sound effect
        PipeSound.Play();

        // Start fading to black and move player at the same time
        yield return StartCoroutine(FadeToBlack());

        yield return new WaitForSeconds(0.5f);

        // Move player to new location (outside the pipe)
        MainPlayer.transform.position = new Vector3(0.5f, 1f, 24f); // <-- Adjust as needed

        // Optional wait for dramatic effect
       // yield return new WaitForSeconds(0.3f);

        // Switch cameras immediately
        MainCam.SetActive(true);
        SecondCam.SetActive(false);

        // Fade back in after everything is set
        yield return StartCoroutine(FadeFromBlack());

        isTransitioning = false;
    }

    IEnumerator FadeToBlack()
    {
        FadeScreen.SetActive(true);  // Ensure the FadeScreen GameObject is active

        // Fade in the Image component
        float elapsedTime = 0f;
        while (elapsedTime < 1f)  // 1 second fade-in duration
        {
            elapsedTime += Time.deltaTime * 2f;  // Adjust fade speed here
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, Mathf.Lerp(0f, 1f, elapsedTime));
            yield return null;
        }

        // Ensure the fade is fully opaque
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 1f);
    }

    IEnumerator FadeFromBlack()
    {
        // Fade out the Image component
        float elapsedTime = 0f;
        while (elapsedTime < 1f)  // 1 second fade-out duration
        {
            elapsedTime += Time.deltaTime * 2f;  // Adjust fade speed here
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, Mathf.Lerp(1f, 0f, elapsedTime));
            yield return null;
        }

        // Ensure the fade is fully transparent
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 0f);

        // Disable the GameObject when the fade is done
        FadeScreen.SetActive(false);
    }
}
