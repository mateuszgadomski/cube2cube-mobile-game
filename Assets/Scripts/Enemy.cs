using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Enemy : MonoBehaviour
{
    public float health = 100;
    public float enemySpeed = 1f;
    public float playerAttackDamage = 25f; // need to change
    public float playerAttackDelay = 1f;
    public float enemyAttackDamage = 2f;

    public float countdown = 0f;

    public GameObject coinPrefab;
    private EnemyUI enemyUI;
    private Vector3 startPos;
    private Transform spawnPos;

    private void Awake()
    {
        enemyUI = transform.GetChild(0).gameObject.GetComponent<EnemyUI>();
        SaveSpawnPosition();
    }

    private void Start()
    {
        EventManager.PlayerEvents.OnGameObjectTouchedCallback += IsTouched;

        countdown = playerAttackDelay;
    }

    private void OnDestroy()
    {
        EventManager.PlayerEvents.OnGameObjectTouchedCallback -= IsTouched;
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

                enemyUI.CountdownBarChange(countdown);

                if (GameManager.Instance.DelayToAction(ref countdown))
                {
                    TakeDamage(playerAttackDamage);
                    GameManager.Instance.VibratePhone();
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
        enemyUI.HealthBarChange(health);
        Destroy();
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
