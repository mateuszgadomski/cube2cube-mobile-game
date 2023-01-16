using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HudUI : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI coinText;

    [SerializeField] private PlayerStats playerStats;

    private void Start()
    {
        EventManager.PlayerEvents.OnPlayerHealthChangeCallback += OnHealthValueChange;
    }

    private void OnDestroy()
    {
        EventManager.PlayerEvents.OnPlayerHealthChangeCallback -= OnHealthValueChange;
    }

    public void OnHealthValueChange(float playerHealth) => healthText.text = $"HEALTH {playerHealth}";
    public void OnCoinValueChange(float playerCoins) => coinText.text = $"COINS {playerCoins}";

}
