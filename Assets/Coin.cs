using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float addCoinValue = 1f;

    private void Start()
    {
        ActionsManager.PlayerEvents.OnGameObjectTouchedCallback += IsTouched;
    }

    private void OnDestroy()
    {
        ActionsManager.PlayerEvents.OnGameObjectTouchedCallback -= IsTouched;
    }

    public void AddCoin()
    {
        ActionsManager.instance.OnCollectCoinCallBack(addCoinValue);
        Destroy(gameObject);
    }

    private void IsTouched(GameObject touchedObject, Touch touch, string coinTag)
    {
        if (touchedObject != this.gameObject)
        {
            return;
        }

        if (touch.phase == TouchPhase.Began && touchedObject.CompareTag(coinTag))
        {
            AddCoin();
        }
    }
}
