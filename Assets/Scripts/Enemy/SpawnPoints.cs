using System.Collections.Generic;
using UnityEngine;

public class SpawnPoints : MonoBehaviour
{
    public static List<Transform> spawnPoints;

    private void Awake()
    {
        spawnPoints = new List<Transform>();

        for (int i = 0; i < transform.childCount; i++)
        {
            spawnPoints.Add(transform.GetChild(i));
        }
    }
}