using UnityEngine;
public class Coin : MonoBehaviour
{
    public float addCoinValue = 1f;

    private void Start()
    {
        EventManager.PlayerEvents.OnGameObjectTouchedCallback += IsTouched;
    }

    private void OnDestroy()
    {
        EventManager.PlayerEvents.OnGameObjectTouchedCallback -= IsTouched;
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

    public void AddCoin()
    {
        EventManager.PlayerEvents.CallOnCollectCoin(addCoinValue);
        Destroy(gameObject);
    }
}