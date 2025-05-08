using System.Collections;
using UnityEngine;

public class Pipe001Entry : MonoBehaviour
{
    public GameObject PipeEntry;
    public int StoodOn;
    public GameObject MainCam;
    public GameObject SecondCam;
    public GameObject MainPlayer;

    public GameObject FadeScreen;     // Changed from CanvasGroup to GameObject
    public AudioSource PipeSound;

    public void OnTriggerEnter(Collider col)
    {
        StoodOn = 1;
    }

    public void OnTriggerExit(Collider col)
    {
        StoodOn = 0;
    }

    public void Update()
    {
        if (Input.GetButtonDown("GoDown") && StoodOn == 1)
        {
            StartCoroutine(FadeAndSlide());
        }
    }

    IEnumerator FadeAndSlide()
    {
        // Show fade screen
        yield return StartCoroutine(FadeToBlack());

        // Play pipe slide sound
        PipeSound.Play();

        // Move avatar off-screen to simulate sliding
        this.transform.position = new Vector3(0, -1000, 0);

        // Enable animation to play
        PipeEntry.GetComponent<Animator>().enabled = true;

        // Simultaneously start switching cameras and move the player
        StartCoroutine(SwitchCameraAndMovePlayer());

        // Wait for the animation to finish
        yield return new WaitForSeconds(1.5f);

        // Disable pipe animation after it's done
        PipeEntry.GetComponent<Animator>().enabled = false;

        // Hide fade screen
        yield return StartCoroutine(FadeFromBlack());
    }

    IEnumerator SwitchCameraAndMovePlayer()
    {
        // Switch camera and move player immediately
        SecondCam.SetActive(true);
        MainCam.SetActive(false);
        MainPlayer.transform.position = new Vector3(1, -16, 17);
        yield return null;  // Allow other things to happen simultaneously
    }

    IEnumerator FadeToBlack()
    {
        FadeScreen.SetActive(true); // Just turn it on instantly
        yield return new WaitForSeconds(0.5f); // Simulate delay
    }

    IEnumerator FadeFromBlack()
    {
        yield return new WaitForSeconds(0.5f); // Simulate delay
        FadeScreen.SetActive(false); // Turn it off
    }
}
