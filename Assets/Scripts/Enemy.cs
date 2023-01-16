using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    public float health = 100;
    public float enemySpeed = 1f;
    public float playerAttackDamage = 25f; // need to change
    public float playerAttackDelay = 1f;
    public float enemyAttackDamage = 2f;

    [HideInInspector] public float countdown = 0f;

    public GameObject coinPrefab;
    private Vector3 startPos;
    private Transform spawnPos;

    private void Awake()
    {
        SaveSpawnPosition();
    }

    private void Start()
    {

        EventManager.PlayerEvents.OnGameObjectTouchedCallback += IsTouched;
    }

    private void OnDestroy()
    {
        EventManager.PlayerEvents.OnGameObjectTouchedCallback -= IsTouched;
    }

    private void Update()
    {
        Destroy();
    }

    public void IsTouched(GameObject touchedObject, Touch touch, string enemyTag)
    {
        if (touchedObject != this.gameObject)
        {
            return;
        }

        if (touchedObject.CompareTag(enemyTag))
        {
            if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
            {
                if (GameManager.Instance.DelayToAction(ref playerAttackDelay))
                {
                    TakeDamage(playerAttackDamage);
                    EventManager.PlayerEvents.CallOnCollectPoints(5f);

                    countdown = playerAttackDelay;
                }
            }
        }
    }

    public void SaveSpawnPosition()
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
    public void TakeDamage(float amount)
    {
        health -= amount;
        EventManager.TouchEvents.CallOnScreenTouched();
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

    public virtual void EnemyAttack()
    {
        EventManager.PlayerEvents.CallOnPlayerDamaged(enemyAttackDamage);
    }

}
