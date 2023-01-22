using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float playerHealth = 100f;
    public float playerCoins = 0f;

    public float playerPoints;

    private void Start()
    {
        EventManager.PlayerEvents.OnCollectCoinCallback += ChangeCoins;
        EventManager.PlayerEvents.OnCollectPointsCallback += AddPoints;
        EventManager.PlayerEvents.OnPlayerDamagedCallback += TakeDamage;
        EventManager.PlayerEvents.OnPlayerAddHealthCallback += AddHealth;
    }

    private void OnDestroy()
    {
        EventManager.PlayerEvents.OnCollectCoinCallback -= ChangeCoins;
        EventManager.PlayerEvents.OnCollectPointsCallback -= AddPoints;
        EventManager.PlayerEvents.OnPlayerDamagedCallback -= TakeDamage;
        EventManager.PlayerEvents.OnPlayerAddHealthCallback -= AddHealth;
    }

    public void AddPoints(float addPointsValue)
    {
        playerPoints += addPointsValue;
        EventManager.PlayerEvents.CallOnPlayerPointsValueChange(playerPoints);
    }

    public void ChangeCoins(float addCoinsValue)
    {
        playerCoins += addCoinsValue;
        EventManager.PlayerEvents.CallOnPlayerCoinsValueChange(playerCoins);
    }

    public void TakeDamage(float amount)
    {
        playerHealth -= amount;
        CheckPlayerHealth();
        EventManager.PlayerEvents.CallOnPlayerHealthChange(playerHealth);
    }

    public void AddHealth(float amount)
    {
        playerHealth += amount;
        EventManager.PlayerEvents.CallOnPlayerHealthChange(playerHealth);

        if (playerHealth >= 100f)
        {
            playerHealth = 100f;
        }
    }

    private void CheckPlayerHealth()
    {
        if (playerHealth <= 0)
        {
            playerHealth = 0;
            Debug.Log("Game Over");
        }
    }
}