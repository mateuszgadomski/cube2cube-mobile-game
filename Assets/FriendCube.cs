using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendCube : MonoBehaviour
{
    public float health = 100f;
    public float rangeAttack = 5f;
    public float attackDamage = 5f;
    public float attackDelay = 5f;
    private float countdown = 3f;

    GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    private void Update()
    {
        gameManager.Timer(ref countdown, CubeEvent);
    }

    public virtual void CubeEvent()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, rangeAttack);

        foreach (var enemy in enemies)
        {
            if (enemy.gameObject.CompareTag("Enemy"))
            {
                Cube cube = enemy.GetComponent<Cube>();
                cube.health -= attackDamage;

                countdown = attackDelay;
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
