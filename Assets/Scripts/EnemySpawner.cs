using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<Enemy> enemies;

    public float spawnDelay = 5f;

    private float countdown = 3f;

    private void Update()
    {
        Spawner();
    }

    private void Spawner()
    {
        if (countdown <= 0f)
        {
            SpawnEnemy();
        }

        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0f, countdown);
    }
    private void SpawnEnemy()
    {
        var spawnPoints = SpawnPoints.spawnPoints;

        if (spawnPoints.Count is 0)
        {
            countdown = spawnDelay;
            return;
        }

        foreach (var enemy in enemies)
        {
            int randomSpawnPoint = GameManager.Instance.RandomNumberGenerate(0, spawnPoints.Count);

            GameObject enemyPrefab = Instantiate(enemy.prefab, spawnPoints[randomSpawnPoint].position, Quaternion.identity);

            spawnPoints.Remove(spawnPoints[randomSpawnPoint]);

            countdown = spawnDelay;
        }
    }
}

[System.Serializable]
public class Enemy
{
    public static Enemy instance;
    public GameObject prefab;
    public int id;
}