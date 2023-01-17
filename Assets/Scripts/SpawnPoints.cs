using System.Collections.Generic;
using UnityEngine;

public class SpawnPoints : MonoBehaviour
{
    public static List<Transform> spawnPoints;
    public static List<Transform> freeSpawnPoints;

    private void Awake()
    {
        spawnPoints = new List<Transform>();
        freeSpawnPoints = new List<Transform>();

        for (int i = 0; i < transform.childCount; i++)
        {
            spawnPoints.Add(transform.GetChild(i));
        }
    }
}