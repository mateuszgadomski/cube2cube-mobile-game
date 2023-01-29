using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    public PlayerEvents Player;
    public LevelEvents Level;
    public EnemyEvents Enemy;

    private void Awake()
    {
        if (Instance == null)
        {
            Player = new PlayerEvents();
            Level = new LevelEvents();
            Enemy = new EnemyEvents();

            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    public class PlayerEvents
    {
        public delegate void OnGameObjectTouched(GameObject touchedObject, Touch touch, string touchedObjectTag);

        public static event OnGameObjectTouched OnGameObjectTouchedCallback;

        public static void CallOnGameObjectTouched(GameObject touchedObject, Touch touch, string enemyTag) => OnGameObjectTouchedCallback?.Invoke(touchedObject, touch, enemyTag);

        public delegate void OnPlayerStatsChanged(float value);

        public static event OnPlayerStatsChanged OnCollectCoinCallback;

        public static void CallOnCollectCoin(float value) => OnCollectCoinCallback?.Invoke(value);

        public static event OnPlayerStatsChanged OnCollectPointsCallback;

        public static void CallOnCollectPoints(float value) => OnCollectPointsCallback?.Invoke(value);

        public static event OnPlayerStatsChanged OnPlayerDamagedCallback;

        public static void CallOnPlayerDamaged(float value) => OnPlayerDamagedCallback?.Invoke(value);

        public static event OnPlayerStatsChanged OnPlayerHealthChangeCallback;

        public static void CallOnPlayerHealthChange(float value) => OnPlayerHealthChangeCallback?.Invoke(value);

        public static event OnPlayerStatsChanged OnPlayerAddHealthCallback;

        public static void CallOnPlayerAddHealth(float value) => OnPlayerAddHealthCallback?.Invoke(value);

        public static event OnPlayerStatsChanged OnPlayerCoinsValueChangeCallback;

        public static void CallOnPlayerCoinsValueChange(float value) => OnPlayerCoinsValueChangeCallback?.Invoke(value);

        public static event OnPlayerStatsChanged OnPlayerPointsValueChangeCallBack;

        public static void CallOnPlayerPointsValueChange(float value) => OnPlayerPointsValueChangeCallBack?.Invoke(value);
    }

    public class LevelEvents
    {
        public delegate void OnLevelChange(Color32 color);

        public static event OnLevelChange OnLevelChangeLightColorsCallback;

        public static void CallOnLevelChangeLightColors(Color32 color) => OnLevelChangeLightColorsCallback?.Invoke(color);

        public static event OnLevelChange OnlevelChangeDarkColorsCallback;

        public static void CallOnLevelChangeDarkColors(Color32 color) => OnlevelChangeDarkColorsCallback?.Invoke(color);

        public delegate void OnNotificationInScene(string alertText);

        public static event OnNotificationInScene OnNotificationInSceneCallback;

        public static void CallOnNotificationInScene(string alertText) => OnNotificationInSceneCallback?.Invoke(alertText);

        public delegate void OnEndGameState();

        public static event OnEndGameState OnEndGameStateCallback;

        public static void CallOnEndGameState() => OnEndGameStateCallback?.Invoke();

        public delegate float OnEndGameChangeHighestScore();

        public static event OnEndGameChangeHighestScore OnChangeHighestScoreCallback;

        public static float CallOnChangeHighestScore() => (float)(OnChangeHighestScoreCallback?.Invoke());
    }

    public class EnemyEvents
    {
        public delegate void OnEnemyTakeDamage(float amount);

        public static event OnEnemyTakeDamage OnEnemyTakeDamageCallback;

        public static void CallOnEnemyTakeDamage(float amount) => OnEnemyTakeDamageCallback?.Invoke(amount);
    }
}