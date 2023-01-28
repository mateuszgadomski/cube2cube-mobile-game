using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float playerPoints;

    public float playerHealth = 100f;
    public float playerCoins = 0f;

    public float highestScore = 0f;

    private void Start()
    {
        EventManager.PlayerEvents.OnCollectCoinCallback += ChangeCoins;
        EventManager.PlayerEvents.OnCollectPointsCallback += AddPoints;
        EventManager.PlayerEvents.OnPlayerDamagedCallback += TakeDamage;
        EventManager.PlayerEvents.OnPlayerAddHealthCallback += AddHealth;
        EventManager.LevelEvents.OnChangeHighestScoreCallback += GetHighestPoints;
    }

    private void OnDestroy()
    {
        EventManager.PlayerEvents.OnCollectCoinCallback -= ChangeCoins;
        EventManager.PlayerEvents.OnCollectPointsCallback -= AddPoints;
        EventManager.PlayerEvents.OnPlayerDamagedCallback -= TakeDamage;
        EventManager.PlayerEvents.OnPlayerAddHealthCallback -= AddHealth;
        EventManager.LevelEvents.OnChangeHighestScoreCallback -= GetHighestPoints;
    }

    public void AddPoints(float addPointsValue)
    {
        playerPoints += addPointsValue;

        if (playerPoints > PlayerPrefs.GetFloat("highestScore", highestScore))
        {
            PlayerPrefs.SetFloat("highestScore", playerPoints);
        }

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
        Handheld.Vibrate();
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

    public float GetHighestPoints() => PlayerPrefs.GetFloat("highestScore", highestScore);

    private void CheckPlayerHealth()
    {
        if (playerHealth <= 0)
        {
            playerHealth = 0;
            SoundManager.instance.PlaySound("EndGame");
            EventManager.LevelEvents.CallOnEndGameState();
        }
    }
}