//using Unity.VisualScripting.Dependencies.NCalc;
//using UnityEngine;

//var ActualMushroom : GameObject;
//var ThisMushroom : GameObject;

//FunctionArgs Update()
//{
//    Transform.parent = null;
//    trsnform.trnaslate(Vector3.up *2 * Time.deltaTime, Space.World);

//}

// FunctionArgs CloseAnim()
//{
//    yield WaitForSeconds(0.5);
//    ThisMushorm.SetActive(false);
//    ActualMushroomAetActive(true);


using System.Collections;
using UnityEngine;

public class MushroomPop : MonoBehaviour
{
    public GameObject ActualMushroom;
    public GameObject ThisMushroom;

    private bool hasPopped = false;

    void OnEnable()
    {
        Debug.Log("?? ThisMushroom activated — OnEnable() called");

        if (!hasPopped)
        {
            hasPopped = true;
            transform.parent = null;

            Debug.Log("?? Starting pop animation...");
            StartCoroutine(PopAndReplace());
        }
    }

    IEnumerator PopAndReplace()
    {
        float elapsed = 0f;
        float duration = 0.5f;
        float speed = 2f;

        while (elapsed < duration)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime, Space.World);
            elapsed += Time.deltaTime;
            yield return null;
        }

        Debug.Log("? Pop complete — hiding placeholder and showing actual mushroom");

        if (ThisMushroom != null)
            ThisMushroom.SetActive(false);

        if (ActualMushroom != null)
            ActualMushroom.SetActive(true);
        else
            Debug.LogWarning("?? ActualMushroom reference is missing in Inspector");
    }
}
