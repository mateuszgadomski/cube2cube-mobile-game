using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<EnemyCube> _enemies;

    [SerializeField] private float _spawnDelay = 5f;
    [SerializeField] private float _countdown = 3f;

    private void Update()
    {
        Spawner();
    }

    private void Spawner()
    {
        if (GameManager.Instance.DelayToAction(ref _countdown))
        {
            int cubePrefabNumber = GameManager.Instance.RandomNumberGenerate(1, _enemies.Count);
            SpawnEnemy(cubePrefabNumber);
        }
    }

    private void SpawnEnemy(int id)
    {
        var spawnPoints = SpawnPoints.spawnPoints;

        if (spawnPoints.Count is 0)
        {
            _countdown = _spawnDelay;
            return;
        }

        foreach (var enemy in _enemies)
        {
            if (enemy.Id == id)
            {
                int randomSpawnNumber = GameManager.Instance.RandomNumberGenerate(0, spawnPoints.Count);

                Instantiate(enemy.Prefab, spawnPoints[randomSpawnNumber].position, Quaternion.identity);
                spawnPoints.Remove(spawnPoints[randomSpawnNumber]);

                _countdown = _spawnDelay;
            }
        }
    }
}

[System.Serializable]
public class EnemyCube
{
    public static Enemy Instance;
    public GameObject Prefab;
    public int Id;
}