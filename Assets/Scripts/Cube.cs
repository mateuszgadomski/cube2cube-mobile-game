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
        if (countdown <= 0f)
        {
            health -= playerAttackDamage;

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

    public virtual void CubeAttackEffect()
    {
        playerStats.playerHealth -= cubeAttackDamage;
        Debug.Log(playerStats.playerHealth);
    }

}
