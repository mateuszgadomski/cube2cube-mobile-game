using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [HideInInspector] public float playerPoints;

    [SerializeField] private float playerHealth = 100f;
    [SerializeField] private float playerCoins = 0f;

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

    public void AddPoints(float addPointsValue)
    {
        playerPoints += addPointsValue;
        EventManager.PlayerEvents.CallOnPlayerPointsValueChange(playerPoints);
    }

    public void AddCoins(float addCoinsValue)
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

    private void CheckPlayerHealth()
    {
        if (playerHealth <= 0)
        {
            playerHealth = 0;
            Debug.Log("Game Over");
        }
    }
}