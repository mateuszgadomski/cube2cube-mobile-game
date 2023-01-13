using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HudUI : MonoBehaviour
{
    public TextMeshProUGUI HealthText;
    public TextMeshProUGUI CoinText;

    private PlayerStats playerStats;

    private void Start()
    {
        playerStats = GameManager.Instance.GetComponent<PlayerStats>();
    }
    private void Update()
    {
        HealthText.text = $"HEALTH {playerStats.playerHealth:0}";
        CoinText.text = $"COINS {playerStats.playerCoins:0}";
    }

}
