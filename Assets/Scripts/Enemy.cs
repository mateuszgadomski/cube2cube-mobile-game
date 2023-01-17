using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 100;
    public float enemySpeed = 1f;
    public float playerAttackDamage = 25f;
    public float enemyAttackDamage = 2f;

    [HideInInspector] public float countdown = 0f;

    private float playerAttackDelay = 1f;

    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private EnemyUI enemyUI;

    private Vector3 startPos;
    private Transform spawnPos;

    private void Awake()
    {
        SaveSpawnPosition();
    }

    private void Start()
    {
        countdown = playerAttackDelay;
        EventManager.PlayerEvents.OnGameObjectTouchedCallback += IsTouched;
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
            Instantiate(coinPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public virtual void Attack() => EventManager.PlayerEvents.CallOnPlayerDamaged(enemyAttackDamage);
}