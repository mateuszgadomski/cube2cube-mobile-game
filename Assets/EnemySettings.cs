using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Settings")]
public class EnemySettings : ScriptableObject
{
    public float health;
    public float enemySpeed;
    public float playerAttackDamage;
    public float enemyAttackDamage;
}
