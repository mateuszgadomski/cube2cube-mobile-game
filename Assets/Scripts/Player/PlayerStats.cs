using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float PlayerPoints;
    public float PlayerHealth = 100f;
    public float PlayerCoins = 0f;
    public float HighestScore = 0f;

    private readonly string _playerPrefsHighestScore = "highestScore";
    private readonly string _endGameSoundName = "EndGame";

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

    public float GetHighestPoints() => PlayerPrefs.GetFloat(_playerPrefsHighestScore, HighestScore);

    public void AddPoints(float addPointsValue)
    {
        PlayerPoints += addPointsValue;

        if (PlayerPoints > PlayerPrefs.GetFloat(_playerPrefsHighestScore, HighestScore))
        {
            PlayerPrefs.SetFloat(_playerPrefsHighestScore, PlayerPoints);
        }

        EventManager.PlayerEvents.CallOnPlayerPointsValueChange(PlayerPoints);
    }

    public void ChangeCoins(float addCoinsValue)
    {
        PlayerCoins += addCoinsValue;
        EventManager.PlayerEvents.CallOnPlayerCoinsValueChange(PlayerCoins);
    }

    public void TakeDamage(float amount)
    {
        PlayerHealth -= amount;
        CheckPlayerHealth();
        EventManager.PlayerEvents.CallOnPlayerHealthChange(PlayerHealth);
        Handheld.Vibrate();
    }

    public void AddHealth(float amount)
    {
        PlayerHealth += amount;
        EventManager.PlayerEvents.CallOnPlayerHealthChange(PlayerHealth);

        if (PlayerHealth >= 100f)
        {
            PlayerHealth = 100f;
        }
    }

    private void CheckPlayerHealth()
    {
        if (PlayerHealth <= 0)
        {
            PlayerHealth = 0;
            SoundManager.instance.PlaySound(_endGameSoundName);
            EventManager.LevelEvents.CallOnEndGameState();
        }
    }
}