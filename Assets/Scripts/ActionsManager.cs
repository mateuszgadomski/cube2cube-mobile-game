using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsManager : MonoBehaviour
{
    public static ActionsManager instance;

    public PlayerEvents Player;

    private void Awake()
    {
        if (instance == null)
        {
            Player = new PlayerEvents();

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

    }


    public delegate void OnCollectCoin(float value);
    public OnCollectCoin OnCollectCoinCallBack;
    public void CallOnCollectCoin(float value)
    {
        OnCollectCoinCallBack?.Invoke(value);
    }

    public delegate void OnCollectPoints(float value);
    public OnCollectCoin OnCollectPointsCallBack;
    public void CallOnCollectPoints(float value)
    {
        OnCollectPointsCallBack?.Invoke(value);
    }
}
