using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class PlayerStats : MonoBehaviour
{
    public float playerHealth = 100f;
    public float playerPoints; //
    public float playerCoins;

    private void Start()
    {
        ActionsManager.instance.OnCollectCoinCallBack += AddCoins;
        ActionsManager.instance.OnCollectPointsCallBack += AddPoints;
    }

    private void OnDestroy()
    {
        ActionsManager.instance.OnCollectCoinCallBack += AddCoins;
        ActionsManager.instance.OnCollectPointsCallBack += AddPoints;
    }

    public void AddPoints(float addPointsValue)
    {
        playerPoints += addPointsValue;
    }

    public void AddCoins(float addCoinsValue)
    {
        playerCoins += addCoinsValue;
    }


}
