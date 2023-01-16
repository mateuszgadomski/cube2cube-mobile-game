using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceTower : MonoBehaviour
{
    public float health = 100f;
    public float rangeAttack = 5f;
    public float attackDamage = 5f;
    public float attackDelay = 5f;
    private float countdown = 3f;

    private void Update()
    {
        Attack();
    }

    public virtual void Attack()
    {
        if (GameManager.Instance.DelayToAction(ref countdown))
        {
            Collider[] enemies = Physics.OverlapSphere(transform.position, rangeAttack);

            foreach (var enemy in enemies)
            {
                if (enemy.gameObject.CompareTag("Enemy"))
                {
                    Enemy cube = enemy.GetComponent<Enemy>();
                    cube.TakeDamage(attackDamage);

                    countdown = attackDelay;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangeAttack);
#endif
    }
}
