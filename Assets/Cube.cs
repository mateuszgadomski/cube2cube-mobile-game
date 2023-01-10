using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public float health = 100;
    public float time = 0f;
    public float attackDelay = 1f;

    private void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }

        if (Input.GetMouseButtonDown(0))
        {
            ObstacleEvent();
        }
    }

    public void Dead()
    {
        health -= 25;
    }

    public virtual void ObstacleEvent()
    {
        Debug.Log("Default");
    }
}
