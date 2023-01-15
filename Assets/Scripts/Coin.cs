using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int addCoinMinValue = 1;
    public int addCoinMaxValue = 5;

    private void Start()
    {
        ActionsManager.instance.Player.OnGameObjectTouchedCallback += IsTouched;

    }

    private void OnDestroy()
    {
        ActionsManager.instance.Player.OnGameObjectTouchedCallback -= IsTouched;
    }

    public void AddCoin()
    {
        float addCoinValues = GameManager.Instance.RandomNumberGenerate(addCoinMinValue, addCoinMaxValue);
        Debug.Log(addCoinValues);
        ActionsManager.instance.Player.OnCollectCoinCallBack(addCoinValues);
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
