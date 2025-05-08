using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End1_1 : MonoBehaviour
{
    public GameObject FadeScreen;
    public GameObject ThePlayer;

    void OnTriggerEnter()
    {
        StartCoroutine(EndFadeScreen());
    }

    IEnumerator EndFadeScreen()
    {
        FadeScreen.SetActive(true);
        FadeScreen.GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(0.495f);
        FadeScreen.GetComponent<Animator>().enabled = false;
        SceneManager.LoadScene("level2try");
    }
}