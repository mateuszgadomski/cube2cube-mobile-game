using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;

    [Header("AttackSkill")]
    [SerializeField] private float attackDamage = 50f;

    [SerializeField] private float attackDelay = 5f;
    [SerializeField] private float attackSkillCost = 10f;
    private float _attackCountdown = 0f;

    [Header("HealthSkill")]
    [SerializeField] private float healthAmount = 50f;

    [SerializeField] private float healthDelay = 5f;
    [SerializeField] private float healthSkillCost = 10f;
    private float _healthCountdown = 0f;

    private void Update()
    {
        DelayToBuySkill(ref _attackCountdown);
        DelayToBuySkill(ref _healthCountdown);
    }

    public void AttackSkill()
    {
        if (_attackCountdown == 0 && CheckCoins(attackSkillCost))
        {
            if (SpawnPoints.spawnPoints.Count == 0)
            {
                Debug.Log("No enemies");
                return;
            }

            EventManager.EnemyEvents.CallOnEnemyTakeDamage(attackDamage);
            playerStats.ChangeCoins(attackSkillCost);
            _attackCountdown = attackDelay;
        }
    }

    public void HealthSkill()
    {
        if (_healthCountdown == 0 && CheckCoins(healthSkillCost))
        {
            if (playerStats.playerHealth < 100)
            {
                Debug.Log("You have max HP");
                return;
            }

            EventManager.PlayerEvents.CallOnPlayerAddHealth(healthAmount);
            EventManager.PlayerEvents.CallOnCollectCoin(-healthSkillCost);
            _healthCountdown = healthDelay;
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
        if (playerStats.playerCoins < skillCost)
        {
            Debug.Log("You don't have coins!");
            return false;
        }
        return true;
    }
}