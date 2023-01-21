using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector] public EnemySettings settings;
    [SerializeField] private List<EnemySettings> enemySettings;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private EnemyUI enemyUI;

    private Vector3 _startPos;
    private Transform _spawnPos;

    private float _countdown = 0f;

    private void Awake()
    {
        SaveSpawnPosition();
    }

    private void Start()
    {
        SetRandomEnemySettings();
        SetStartTouchCountdown(settings.playerAttackDelay);
        EventManager.PlayerEvents.OnGameObjectTouchedCallback += IsTouched;
        EventManager.EnemyEvents.OnEnemyTakeDamageCallback += TakeDamage;
    }

    private void OnDestroy()
    {
        EventManager.PlayerEvents.OnGameObjectTouchedCallback -= IsTouched;
        EventManager.EnemyEvents.OnEnemyTakeDamageCallback -= TakeDamage;
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
                enemyUI.CountdownBarChange(_countdown);

                if (GameManager.Instance.DelayToAction(ref _countdown))
                {
                    TakeDamage(settings.playerAttackDamage);
                    GameManager.Instance.VibratePhone();
                    EventManager.PlayerEvents.CallOnCollectPoints(5f);
                    _countdown = settings.playerAttackDelay;
                }
            }
        }
    }

    public void SaveSpawnPosition()
    {
        _startPos = transform.position;

        foreach (var spawnPoint in SpawnPoints.spawnPoints)
        {
            if (_startPos == spawnPoint.position)
            {
                _spawnPos = spawnPoint;
            }
        }
    }

    public void TakeDamage(float amount)
    {
        settings.health -= amount;
        enemyUI.HealthBarChange(settings.health);
        Destroy();
        Debug.Log("ELO");
    }

    private void Destroy()
    {
        if (settings.health <= 0)
        {
            SpawnPoints.spawnPoints.Add(_spawnPos);
            CreateCoint(coinPrefab);
            Destroy(gameObject);
        }
    }

    private void CreateCoint(GameObject prefab)
    {
        Instantiate(prefab, transform.position, Quaternion.identity);
    }

    private void SetRandomEnemySettings()
    {
        int _randomNumberSettings = GameManager.Instance.RandomNumberGenerate(0, enemySettings.Count);
        EnemySettings _enemySettings = Instantiate(enemySettings[_randomNumberSettings]);
        settings = _enemySettings;
    }

    private void SetStartTouchCountdown(float playerAttackDelay) => _countdown = playerAttackDelay;

    public void Attack() => EventManager.PlayerEvents.CallOnPlayerDamaged(settings.enemyAttackDamage);
}