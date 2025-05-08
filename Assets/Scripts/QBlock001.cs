//using UnityEngine;

//var QBlock : GameObject;
//var DeadBlock : GameObject;
//var Mushroom: GameObject;

//TransferFunction OnTriggerEnter (Col : Collider){
//    QBlock.SetActive(false);
//DeadBlock.setActive(true);
//yeild WaitForSeconds(0.2);
//Mushroom.SetActive(true);

using System.Collections;
using UnityEngine;

public class QBlock001 : MonoBehaviour
{
    public GameObject QBlock;
    public GameObject DeadBlock;
    public GameObject Mushroom;

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            StartCoroutine(ActivateBlock());
        }
    }

    IEnumerator ActivateBlock()
    {
        // Hide QBlock and show DeadBlock
        QBlock.SetActive(false);
        DeadBlock.SetActive(true);

        // Wait a short delay before showing mushroom
        yield return new WaitForSeconds(0.2f);

        // Reveal mushroom
        Mushroom.SetActive(true);
    }
}
