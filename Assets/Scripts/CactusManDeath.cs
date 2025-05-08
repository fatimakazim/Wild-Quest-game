

//var CactusMan : GameObject;

//Function OnTriggerEnter (Col : Collider){
//    this.GetComponent("BoxColider").enabled = false;
//CactusMan.GetComponent("CactusManMove").enabled = false;
//CactusMan.trasform.localScale -= (new Vector3(0, 0.8, 0);
//CactusMan.transform.localPosiotion -= new Vector3(0, 0.4, 0);
//yeild WaitForSeconds(1);
//CactusMan.transform.positon = Vector3(0, -1000, 0);
//}

using System.Collections;
using UnityEngine;

public class CactusStompHandler : MonoBehaviour
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
        CactusMan.transform.localScale -= new Vector3(0f, 0.8f, 0f);
        CactusMan.transform.localPosition -= new Vector3(0f, 0.4f, 0f);

        // Wait before despawning
        yield return new WaitForSeconds(1f);

        // Move cactus far offscreen
        CactusMan.transform.position = new Vector3(0f, -1000f, 0f);
    }
}
