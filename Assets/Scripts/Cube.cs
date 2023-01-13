using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Cube : MonoBehaviour
{
    public float health = 100;
    public float cubeSpeed = 1f;
    public float playerAttackDamage = 25f; // need to change
    public float cubeAttackDamage = 2f;
    public float playerAttackDelay = 1f;

    [HideInInspector] public float countdown = 0f;

    public GameObject coinPrefab;
    private Vector3 startPos;
    public Transform spawnPos;
    private PlayerStats playerStats;

    private void Awake()
    {
        CubeSpawnCheck();
    }

    private void Start()
    {
        playerStats = GameManager.Instance.gameObject.GetComponent<PlayerStats>();
    }

    private void Update()
    {
        Destroy();
    }

    public void CubeSpawnCheck()
    {
        startPos = transform.position;

        foreach (var spawnPoint in SpawnPoints.spawnPoints)
        {
            if (startPos == spawnPoint.position)
            {
                spawnPos = spawnPoint;
            }
        }
    }
    public void Damage()
    {
        health -= playerAttackDamage;
        playerStats.addPoints(5f);
        Handheld.Vibrate();

        countdown = playerAttackDelay;
    }

    private void Destroy()
    {
        if (health <= 0)
        {
            SpawnPoints.spawnPoints.Add(spawnPos);
            GameObject coin = Instantiate(coinPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public virtual void CubeAttackEffect()
    {
        playerStats.playerHealth -= cubeAttackDamage;
    }

}
