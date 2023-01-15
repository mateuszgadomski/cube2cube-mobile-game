using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class PlayerStats : MonoBehaviour
{
    public float playerHealth = 100f;
    public float playerCoins;
    [HideInInspector] public float playerPoints;


    private void Start()
    {
        ActionsManager.instance.Player.OnCollectCoinCallBack += AddCoins;
        ActionsManager.instance.Player.OnCollectPointsCallBack += AddPoints;
        ActionsManager.instance.EnemyCube.OnCubeTakeDamageCallBack += RemovePlayerHealth;
    }

    private void OnDestroy()
    {
        ActionsManager.instance.Player.OnCollectCoinCallBack += AddCoins;
        ActionsManager.instance.Player.OnCollectPointsCallBack += AddPoints;
        ActionsManager.instance.EnemyCube.OnCubeTakeDamageCallBack -= RemovePlayerHealth;
    }

    public void AddPoints(float addPointsValue) => playerPoints += addPointsValue;

    public void AddCoins(float addCoinsValue) => playerCoins += addCoinsValue;

    public void RemovePlayerHealth(float removeHealthValue) => playerHealth -= removeHealthValue;
}
