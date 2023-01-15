using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsManager : MonoBehaviour
{
    public static ActionsManager instance;

    public PlayerActions Player;
    public EnemyCubeActions EnemyCube;

    private void Awake()
    {
        if (instance == null)
        {
            Player = new PlayerActions();
            EnemyCube = new EnemyCubeActions();

            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    public class PlayerActions
    {
        public delegate void OnGameObjectTouched(GameObject touchedObject, Touch touch, string touchedObjectTag);
        public OnGameObjectTouched OnGameObjectTouchedCallback;
        public void CallOnGameObjectTouched(GameObject touchedObject, Touch touch, string enemyTag)
        {
            OnGameObjectTouchedCallback?.Invoke(touchedObject, touch, enemyTag);
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

    public class EnemyCubeActions
    {
        public delegate void OnCubeDamaged();
        public OnCubeDamaged OnCubeDamagedCallBack;
        public void CallOnCubeDamaged()
        {
            OnCubeDamagedCallBack?.Invoke();
        }

        public delegate void OnCubeTakeDamage(float damageValue);
        public OnCubeTakeDamage OnCubeTakeDamageCallBack;
        public void CallOnCubeTakeDamage(float damageValue)
        {
            OnCubeTakeDamageCallBack?.Invoke(damageValue);
        }
    }

}
