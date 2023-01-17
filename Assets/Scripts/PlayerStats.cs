using UnityEngine;

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

    public void AddCoins(float addCoinsValue)
    {
        playerCoins += addCoinsValue;
        EventManager.PlayerEvents.CallOnPlayerCoinsValueChange(playerCoins);
    }

    public void TakeDamage(float amount)
    {
        playerHealth -= amount;

        if (playerHealth <= 0)
        {
            playerHealth = 0;
            Debug.Log("Game Over");
        }

        EventManager.PlayerEvents.CallOnPlayerHealthChange(playerHealth);
    }
}