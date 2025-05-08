using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level02Load1 : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(WaitForSceneLoad());
    }

    IEnumerator WaitForSceneLoad()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("level2try"); // Make sure this scene is in your Build Settings
    }
}
