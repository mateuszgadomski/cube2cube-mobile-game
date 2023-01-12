using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Cube : MonoBehaviour
{
    public float health = 100;
    public float cubeSpeed = 1f;
    public float playerAttackDamage = 25f;
    public float playerAttackDelay = 1f;

    [HideInInspector] public float countdown = 0f;

    private Vector3 startPos;
    private Transform spawnPos;

    private void Start()
    {
        cubeSpawnCheck();
    }

    private void Update()
    {
        Destroy();
    }

    public void cubeSpawnCheck()
    {
        startPos = transform.position;

        foreach (var spawnPoint in SpawnPoints.spawnPoints)
        {
            if (startPos == spawnPoint.position)
            {
                Debug.Log("SPAWNSAVE");
                spawnPos = spawnPoint;
                //SpawnPoints.spawnPoints.Remove(spawnPoint);
                //SpawnPoints.spawnPoints.TrimExcess();
            }
        }
    }
    public void Damage()
    {
        if (countdown <= 0f)
        {
            health -= playerAttackDamage;

            PlayerStats playerStats = GameManager.Instance.gameObject.GetComponent<PlayerStats>();
            playerStats.addPoints(5f);

            Handheld.Vibrate();

            countdown = playerAttackDelay;
        }

        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0f, countdown);
    }

    private void Destroy()
    {
        if (health <= 0)
        {
            SpawnPoints.spawnPoints.Add(spawnPos);
            Destroy(gameObject);
        }
    }

    public virtual void ObstacleEvent()
    {
        Debug.Log("Default");
    }

}
