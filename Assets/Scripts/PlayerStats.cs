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
        EventManager.PlayerEvents.OnCollectCoinCallback += AddCoins;
        EventManager.PlayerEvents.OnCollectPointsCallback += AddPoints;
        EventManager.PlayerEvents.OnPlayerDamagedCallback += TakeDamage;
    }

    private void OnDestroy()
    {
        EventManager.PlayerEvents.OnCollectCoinCallback -= AddCoins;
        EventManager.PlayerEvents.OnCollectPointsCallback -= AddPoints;
        EventManager.PlayerEvents.OnPlayerDamagedCallback -= TakeDamage;
    }

    public void AddPoints(float addPointsValue) => playerPoints += addPointsValue;

    public void AddCoins(float addCoinsValue) => playerCoins += addCoinsValue;

    public void TakeDamage(float amount) => playerHealth -= amount;

    public void TakeCoins(float cost) => playerCoins -= cost;
    
}
