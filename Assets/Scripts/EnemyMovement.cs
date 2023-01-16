using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Enemy enemy;
    Vector3 direction;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        direction = Vector3.up;
    }

    private void Update()
    {
        CubeMove(direction, enemy.enemySpeed);
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject)
        {
            enemy.EnemyAttack();

            if (direction != Vector3.down)
            {
                direction = Vector3.down;
            }
            else
            {
                direction = Vector3.up;
            }
        }
    }

    private void CubeMove(Vector3 direction, float cubeSpeed)
    {
        transform.Translate(direction * cubeSpeed * Time.deltaTime, Space.World);
    }

}
