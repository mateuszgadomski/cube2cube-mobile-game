using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Settings")]
public class EnemySettings : ScriptableObject
{
    public float Health;
    public float EnemySpeed;
    public float PlayerAttackDamage;
    public float EnemyAttackDamage;
    public float PlayerAttackDelay;
    public float PlayerPointsForAttack;
}