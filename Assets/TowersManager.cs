using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowersManager : MonoBehaviour
{
    public GameObject towerPrefab;

    private PlayerStats playerStats;

    public float towerCost = 100f;

    private void Start()
    {
        playerStats = GetComponent<PlayerStats>();
    }

    public void BuyTower()
    {
        if (playerStats.playerCoins < towerCost)
        {
            Debug.Log("You don't have money");
            return;
        }

        playerStats.TakeCoins(towerCost);
        Instantiate(towerPrefab, transform.position, Quaternion.identity, transform);
    }
}
