//using UnityEngine;
//using UnityEngine.UI;
//using static System.Net.Mime.MediaTypeNames;

//var CoinDisplay :  GameObject;
//var InternalCoin :int;
//static var CoinCount : int;

//Function Update()
//{
//    InternalCoin = CoinCoun;
//    CoinDisplay.GetComponent.< Text >().text = "Coins"+ CoinCount
//}

using UnityEngine;
using UnityEngine.UI;

public class GlobalCoins : MonoBehaviour
{
    public GameObject CoinDisplay;
    public int InternalCoin;
    public static int CoinCount;
    void Update()
    {
        InternalCoin = CoinCount;

        if (CoinDisplay != null)
        {
            Text textComponent = CoinDisplay.GetComponent<Text>();
            if (textComponent != null)
            {
                textComponent.text = "Coins: " + CoinCount;
            }
        }
    }
    }