using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<EnemyCube> enemies;

    public float spawnDelay = 5f;

    private float countdown = 3f;


    private void Update()
    {
        Spawner();
    }

    private void Spawner()
    {
        if (GameManager.Instance.DelayToAction(ref countdown))
        {
            int cubePrefabNumber = GameManager.Instance.RandomNumberGenerate(1, enemies.Count + 1);
            SpawnEnemy(cubePrefabNumber);
        }
    }
    private void SpawnEnemy(int id)
    {
        var spawnPoints = SpawnPoints.spawnPoints;

        if (spawnPoints.Count is 0)
        {
            countdown = spawnDelay;
            return;
        }

        foreach (var enemy in enemies)
        {
            if (enemy.id == id)
            {
                int randomSpawnPoint = GameManager.Instance.RandomNumberGenerate(0, spawnPoints.Count);

                GameObject enemyPrefab = Instantiate(enemy.prefab, spawnPoints[randomSpawnPoint].position, Quaternion.identity);

                spawnPoints.Remove(spawnPoints[randomSpawnPoint]);

                countdown = spawnDelay;
            }
        }
    }
}

[System.Serializable]
public class EnemyCube
{
    public static Enemy instance;
    public GameObject prefab;
    public int id;
}
