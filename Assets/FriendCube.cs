using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendCube : MonoBehaviour
{
  public float health = 100f;
    public float rangeAttack = 5f;

    public float attackDelay = 5f;

    private float countdown = 3f;

    private void Update()
    {
        Attack();
    }

    public virtual void Attack()
    {
     

    if (countdown <= 0f)
        {
            Collider[] enemies = Physics.OverlapSphere(transform.position, rangeAttack);

            foreach (var enemy in enemies)
            {
                if (enemy.gameObject.CompareTag("Enemy"))
                {
                    Debug.Log(enemy);
                }
            }
        }

countdown -= Time.deltaTime;
countdown = Mathf.Clamp(countdown, 0f, countdown);
    }

    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangeAttack);
#endif
    }

}
