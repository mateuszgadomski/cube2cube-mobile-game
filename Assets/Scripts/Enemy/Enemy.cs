using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector] public EnemySettings Settings;

    [SerializeField] private List<EnemySettings> _enemySettings;
    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private EnemyUI _enemyUI;
    [SerializeField] private Animator _animator;

    [SerializeField] private GameObject _takeDamageParticles;
    [SerializeField] private GameObject _destroyEnemyParticles;

    private Vector3 _startPos;
    private Transform _spawnPos;

    private float _countdown = 0f;

    private readonly string _takeDamageAnimationState = "TakeDamage";
    private readonly string _touchAnimationState = "StandardTouch";

    private void Awake()
    {
        SaveSpawnPosition();
        SetRandomEnemySettings();
        SetStartTouchCountdown(Settings.PlayerAttackDelay);
    }

    private void Start()
    {
        EventManager.PlayerEvents.OnGameObjectTouchedCallback += IsTouched;
        EventManager.EnemyEvents.OnEnemyTakeDamageCallback += TakeDamage;
        EventManager.LevelEvents.OnEndGameStateCallback += OnGameEndDestroyEnemies;
    }

    private void OnDestroy()
    {
        EventManager.PlayerEvents.OnGameObjectTouchedCallback -= IsTouched;
        EventManager.EnemyEvents.OnEnemyTakeDamageCallback -= TakeDamage;
        EventManager.LevelEvents.OnEndGameStateCallback -= OnGameEndDestroyEnemies;
    }

    public void Attack() => EventManager.PlayerEvents.CallOnPlayerDamaged(Settings.EnemyAttackDamage);

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

    public void IsTouched(GameObject touchedObject, Touch touch, string enemyTag)
    {
        if (touchedObject != this.gameObject)
        {
            ChangeAnimationState(_takeDamageAnimationState, false);
            return;
        }

        if (touchedObject.CompareTag(enemyTag))
        {
            if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
            {
                _enemyUI.CountdownBarChange(_countdown);
                ChangeAnimationState(_takeDamageAnimationState, true);
                if (GameManager.Instance.DelayToAction(ref _countdown))
                {
                    TakeDamage(Settings.PlayerAttackDamage);
                    EventManager.PlayerEvents.CallOnCollectPoints(Settings.PlayerPointsForAttack);
                    SoundManager.instance.PlaySound(_touchAnimationState);

                    _countdown = Settings.PlayerAttackDelay;
                }
            }
        }
        else
        {
            ChangeAnimationState(_takeDamageAnimationState, false);
        }
    }

    public void TakeDamage(float amount)
    {
        Settings.Health -= amount;
        _enemyUI.HealthBarChange(Settings.Health);
        _enemyUI.TakeDamageText(amount);
        ObjectPoolManager.instance.SpawnGameObject(_takeDamageParticles, transform.position, Quaternion.identity);
        Destroy();
    }

    public void OnGameEndDestroyEnemies()
    {
        InstantiateDestroyParticles();
        Destroy(this.gameObject);
    }

    private void SetStartTouchCountdown(float playerAttackDelay) => _countdown = playerAttackDelay;

    private void ChangeAnimationState(string animationName, bool state) => _animator.SetBool(animationName, state);

    private void InstantiateDestroyParticles() => ObjectPoolManager.instance.SpawnGameObject(_destroyEnemyParticles, transform.position, Quaternion.identity);

    private void SetRandomEnemySettings()
    {
        int randomNumberSettings = GameManager.Instance.RandomNumberGenerate(0, _enemySettings.Count);
        EnemySettings enemySettings = Instantiate(_enemySettings[randomNumberSettings]);
        Settings = enemySettings;
    }

    private void CreateCoin(GameObject prefab)
    {
        ObjectPoolManager.instance.SpawnGameObject(prefab, transform.position, Quaternion.identity);
    }

    private void Destroy()
    {
        if (Settings.Health <= 0)
        {
            SpawnPoints.spawnPoints.Add(_spawnPos);
            InstantiateDestroyParticles();
            CreateCoin(_coinPrefab);
            Destroy(this.gameObject);
        }
    }
}