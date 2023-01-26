using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<EnemyCube> enemies;

    [SerializeField] private float spawnDelay = 5f;
    [SerializeField] private float countdown = 3f;

    private void Update()
    {
        Spawner();
    }

    private void Spawner()
    {
        if (GameManager.instance.DelayToAction(ref countdown))
        {
            int cubePrefabNumber = GameManager.instance.RandomNumberGenerate(1, enemies.Count);
            SpawnEnemy(cubePrefabNumber);
        }
    }

    private void SpawnEnemy(int id)
    {
        var _spawnPoints = SpawnPoints.spawnPoints;

        if (_spawnPoints.Count is 0)
        {
            countdown = spawnDelay;
            return;
        }

        foreach (var enemy in enemies)
        {
            if (enemy.id == id)
            {
                int _randomSpawnPointNumber = GameManager.instance.RandomNumberGenerate(0, _spawnPoints.Count);

                Instantiate(enemy.prefab, _spawnPoints[_randomSpawnPointNumber].position, Quaternion.identity);
                _spawnPoints.Remove(_spawnPoints[_randomSpawnPointNumber]);

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