using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float addCoin = 1f;

    private PlayerStats playerStats;

    private void Start()
    {
        playerStats = GameManager.Instance.GetComponent<PlayerStats>();
    }

    public void AddCoin()
    {
        playerStats.playerCoins += addCoin;
        Destroy(gameObject);
    }

}
