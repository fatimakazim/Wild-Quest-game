using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GlobalLives : MonoBehaviour
{
    public static int LivesAmount = 3;
    public int InternalLives;
    public GameObject LifeTextBox;

    // Update is called once per frame
    void Update()
    {
        InternalLives = LivesAmount;
        LifeTextBox.GetComponent<Text>().text = InternalLives.ToString();

        if (InternalLives < 1)
        {
            StartCoroutine(GameOverScene());
        }
    }

    IEnumerator GameOverScene()
    {
        yield return new WaitForSeconds(2.9f);
        SceneManager.LoadScene(3);
    }
}
