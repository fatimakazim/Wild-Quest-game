using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CountingDown : MonoBehaviour
{
    public int TotalTime = 120;
    public Text TimeDisplay;
    public int CalculatingTime;
    public AudioSource DeathSound;
    public GameObject LevelMusic;
    public GameObject CamFollow;
    public GameObject ThePlayer;
    public int KeepZero;
    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        if (KeepZero == 0)
        {
            TimeDisplay.text =
            "" + TotalTime;
        }
        if (CalculatingTime == 0)
        {
            StartCoroutine(DeductTime());
        }
        if (TotalTime == 0)
        {
            //StartCoroutine(TimeUp());
            SceneManager.LoadScene("GameOver");
        }
    }
    IEnumerator DeductTime()
    {
        CalculatingTime = 1;
        TotalTime -= 1;
        yield return new WaitForSeconds(1);
        CalculatingTime = 0;
    }
    IEnumerator TimeUp()
    {
        TotalTime -= 1;
        KeepZero = 1;
        TimeDisplay.text = "0";
        GlobalLives.LivesAmount -= 1;
        LevelMusic.GetComponent<AudioSource>().enabled = false;
        DeathSound.GetComponent<AudioSource>().Play();
        CamFollow.GetComponent<CameraFollow>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        ThePlayer.GetComponent<CharacterController>().enabled = false;
        ThePlayer.transform.localScale -= new Vector3(0.0f, 0.7f, 0.0f);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("GameOver");
    }
}