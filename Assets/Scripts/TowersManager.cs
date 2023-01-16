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
}
