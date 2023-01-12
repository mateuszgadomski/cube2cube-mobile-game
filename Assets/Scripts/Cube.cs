using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Cube : MonoBehaviour
{
    public float health = 100;
    [HideInInspector] public float countdown = 0f;
    public float attackDelay = 1f;
    public float cubeSpeed = 1f;

    private void Update()
    {
        if (health <= 0)
        {
            SpawnPoints.spawnPoints.Add(SpawnPoints.freeSpawnPoints[0]);
            SpawnPoints.freeSpawnPoints.Remove(SpawnPoints.freeSpawnPoints[0]);
            Destroy(gameObject);
        }
    }
    public void Dead()
    {
        if (countdown <= 0f)
        {
            health -= 25;
            Handheld.Vibrate();

            countdown = attackDelay;
        }

        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0f, countdown);
    }

    public virtual void ObstacleEvent()
    {
        Debug.Log("Default");
    }

}
