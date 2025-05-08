//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;

//public class Level02Death : MonoBehaviour
//{
//    public AudioSource DeathSound;
//    public AudioSource LevelMusic;
//    public GameObject CamFollow;
//    public GameObject ThePlayer;

//    void OnTriggerEnter (Collider col)
//    {
//        DeathSound.GetComponent<AudioSource>().Play();
//        StartCoroutine(WaitForDeath());
//    }

//    IEnumerator WaitForDeath()
//    {
//        GlobalLives.LivesAmount -= 1;
//        LevelMusic.GetComponent<AudioSource>().enabled = false;
//        CamFollow.GetComponent<CameraFollow>().enabled = false;
//        yield return new WaitForSeconds(0.5f);
//        ThePlayer.GetComponent<CharacterController>().enabled = false;
//        ThePlayer.transform.localScale -= new Vector3(0.0f, 0.7f, 0.0f);
//        yield return new WaitForSeconds(1.5f);
//        SceneManager.LoadScene("Level02Preload");

//    }
//}

//using System.Collections;
//using UnityEngine;
//using UnityEngine.SceneManagement;

//public class Level02Death : MonoBehaviour
//{
//    public AudioSource DeathSound;

//    void OnTriggerEnter(Collider col)
//    {
//        StartCoroutine(WaitForDeath());
//    }

//    IEnumerator WaitForDeath()
//    {
//        yield return new WaitForSeconds(1.5f);
//        SceneManager.LoadScene("Level02Preload");
//        //GlobalLives.LivesAmount -= 1;
//        //GlobalCoins.CoinCount = 0;
//    }
//}

//}
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.ThirdPerson;

public class Level02Death : MonoBehaviour
{
    public AudioSource DeathSound;

    void OnTriggerEnter(Collider col)
    {
        // Play death sound
        if (DeathSound != null)
            DeathSound.Play();

        // Trigger collapse if the character has the script
        ThirdPersonCharacter character = col.GetComponent<ThirdPersonCharacter>();
        if (character != null)
        {
            character.Collapse();  // <- trigger physical fall
        }

        StartCoroutine(WaitForDeath());
    }

    IEnumerator WaitForDeath()
    {
        yield return new WaitForSeconds(2f); // Wait longer so collapse is visible
        // SceneManager.LoadScene("Level01Preload");
        // GlobalLives.LivesAmount -= 1;
        // GlobalCoins.CoinCount = 0;
        GlobalLives.LivesAmount = Mathf.Max(0, GlobalLives.LivesAmount - 1);
        GlobalCoins.CoinCount = 0;

        if (GlobalLives.LivesAmount == 0)
        {
            StartCoroutine(GameOverScene());
        }

        else
        {
            //GlobalLives.LivesAmount -= 1;
            SceneManager.LoadScene("Level02Preload");
            //GlobalCoins.CoinCount = 0;
        }
    }

    IEnumerator GameOverScene()
    {
        yield return new WaitForSeconds(2.9f);
        SceneManager.LoadScene("GameOver");
    }
}