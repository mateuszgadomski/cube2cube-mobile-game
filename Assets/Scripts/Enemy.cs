using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector] public EnemySettings settings;
    [SerializeField] private List<EnemySettings> enemySettings;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private EnemyUI enemyUI;
    [SerializeField] private Animator animator;

    [SerializeField] private GameObject takeDamageParticles;
    [SerializeField] private GameObject destroyEnemyParticles;

    private Vector3 _startPos;
    private Transform _spawnPos;

    private float _countdown = 0f;

    private void Awake()
    {
        SaveSpawnPosition();
        SetRandomEnemySettings();
        SetStartTouchCountdown(settings.playerAttackDelay);
    }

    private void Start()
    {
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
            ChangeAnimationState("TakeDamage", false);
            return;
        }

        if (touchedObject.CompareTag(enemyTag))
        {
            if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
            {
                enemyUI.CountdownBarChange(_countdown);
                ChangeAnimationState("TakeDamage", true);
                if (GameManager.instance.DelayToAction(ref _countdown))
                {
                    TakeDamage(settings.playerAttackDamage);
                    EventManager.PlayerEvents.CallOnCollectPoints(settings.playerPointsForAttack);

                    _countdown = settings.playerAttackDelay;
                }
            }
        }
        else
        {
            ChangeAnimationState("TakeDamage", false);
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
        enemyUI.TakeDamageText(amount);
        ObjectPoolManager.instance.SpawnGameObject(takeDamageParticles, transform.position, Quaternion.identity);
        Destroy();
    }

    private void Destroy()
    {
        if (settings.health <= 0)
        {
            SpawnPoints.spawnPoints.Add(_spawnPos);
            ObjectPoolManager.instance.SpawnGameObject(destroyEnemyParticles, transform.position, Quaternion.identity);
            CreateCoint(coinPrefab);
            Destroy(gameObject);
        }
    }

    private void CreateCoint(GameObject prefab)
    {
        ObjectPoolManager.instance.SpawnGameObject(prefab, transform.position, Quaternion.identity);
    }

    private void SetRandomEnemySettings()
    {
        int _randomNumberSettings = GameManager.instance.RandomNumberGenerate(0, enemySettings.Count);
        EnemySettings _enemySettings = Instantiate(enemySettings[_randomNumberSettings]);
        settings = _enemySettings;
    }

    private void SetStartTouchCountdown(float playerAttackDelay) => _countdown = playerAttackDelay;

    private void ChangeAnimationState(string animationName, bool state) => animator.SetBool(animationName, state);

    public void Attack() => EventManager.PlayerEvents.CallOnPlayerDamaged(settings.enemyAttackDamage);
}