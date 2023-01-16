using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    public PlayerEvents Player;
    public TowerEvents Tower;

    private void Awake()
    {
        if (instance == null)
        {
            Player = new PlayerEvents();
            Tower = new TowerEvents();

            instance = this;
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
    }

    public class TowerEvents
    {
        public delegate void OnBuyTowerDefence();
        public static event OnBuyTowerDefence DestroyButtonCallback;
        public static void CallDestroyButton() => DestroyButtonCallback?.Invoke();  
    }
}
