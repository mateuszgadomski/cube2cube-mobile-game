using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private float addCoinValue = 1f;
    [SerializeField] private float destroyTime = 2f;
    private bool check;

    private void Start()
    {
        EventManager.PlayerEvents.OnGameObjectTouchedCallback += IsTouched;
    }

    private void Update()
    {
        DestroyCoin();
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

    private void DestroyCoin()
    {
        if (GameManager.Instance.DelayToAction(ref destroyTime) == true)
        {
            Destroy(gameObject);
        }
    }

    public void AddCoin()
    {
        EventManager.PlayerEvents.CallOnCollectCoin(addCoinValue);
        Destroy(gameObject);
    }
}