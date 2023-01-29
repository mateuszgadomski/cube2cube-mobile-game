using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private PlayerStats _playerStats;

    [Header("AttackSkill")]
    [SerializeField] private float _attackDamage = 50f;

    [SerializeField] private float _attackDelay = 5f;
    [SerializeField] private float _attackSkillCost = 10f;
    private float _attackCountdown = 0f;

    [Header("HealthSkill")]
    [SerializeField] private float _healthAmount = 50f;

    [SerializeField] private float _healthDelay = 5f;
    [SerializeField] private float _healthSkillCost = 10f;
    private float _healthCountdown = 0f;

    private readonly string _buyAttackSkillSoundName = "AttackSkill";
    private readonly string _buyHealthSkillSoundName = "HealthSkill";

    private readonly string _maxHealthAlert = "You have max HP";
    private readonly string _NoCoinsAlert = "You don't have coins";

    private void Update()
    {
        DelayToBuySkill(ref _attackCountdown);
        DelayToBuySkill(ref _healthCountdown);
    }

    public void AttackSkill()
    {
        if (_attackCountdown == 0 && CheckCoins(_attackSkillCost))
        {
            EventManager.EnemyEvents.CallOnEnemyTakeDamage(_attackDamage);
            SoundManager.instance.PlaySound(_buyAttackSkillSoundName);
            _playerStats.ChangeCoins(-_attackSkillCost);
            _attackCountdown = _attackDelay;
        }
    }

    public void HealthSkill()
    {
        if (_healthCountdown == 0 && CheckCoins(_healthSkillCost))
        {
            if (_playerStats.PlayerHealth >= 100)
            {
                EventManager.LevelEvents.CallOnNotificationInScene(_maxHealthAlert);
                return;
            }
            EventManager.PlayerEvents.CallOnPlayerAddHealth(_healthAmount);
            EventManager.PlayerEvents.CallOnCollectCoin(-_healthSkillCost);
            EventManager.PlayerEvents.CallOnPlayerHealthChange(_playerStats.PlayerHealth);
            SoundManager.instance.PlaySound(_buyHealthSkillSoundName);
            _healthCountdown = _healthDelay;
        }
    }

    private void DelayToBuySkill(ref float countdown)
    {
        if (countdown != 0)
        {
            GameManager.Instance.DelayToAction(ref countdown);
        }
    }

    private bool CheckCoins(float skillCost)
    {
        if (_playerStats.PlayerCoins < skillCost)
        {
            EventManager.LevelEvents.CallOnNotificationInScene(_NoCoinsAlert);
            return false;
        }
        return true;
    }
}