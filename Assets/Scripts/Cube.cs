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
    public float pointsForDestruction = 5f;

    [HideInInspector] public float countdown = 0f;

    public GameObject coinPrefab;
    private Vector3 startPos;
    private Transform spawnPos;

    private void Awake() => CubeSpawnCheck();

    private void Start()
    {
        ActionsManager.instance.Player.OnGameObjectTouchedCallback += IsTouched;
    }

    private void OnDestroy()
    {
        ActionsManager.instance.Player.OnGameObjectTouchedCallback -= IsTouched;
    }

    private void Update() => Destroy();

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
                    DamageToCube();
                }
            }
        }
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
    public void DamageToCube()
    {
        health -= playerAttackDamage;
        ActionsManager.instance.Player.OnCollectPointsCallBack(pointsForDestruction);
        ActionsManager.instance.EnemyCube.OnCubeDamagedCallBack();
        countdown = playerAttackDelay;
    }

    public void DamageToPlayer()
    {
        ActionsManager.instance.EnemyCube.OnCubeTakeDamageCallBack(cubeAttackDamage);
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
}

