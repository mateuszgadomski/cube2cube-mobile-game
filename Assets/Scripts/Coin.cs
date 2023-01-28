using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private float addCoinValue = 1f;
    [SerializeField] private GameObject destroyCoinParticles;

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

    public void DestroyCoin()
    {
        ObjectPoolManager.instance.SpawnGameObject(destroyCoinParticles, transform.position, transform.rotation);
        ObjectPoolManager.instance.DespawnGameObject(gameObject);
    }

    public void AddCoin()
    {
        EventManager.PlayerEvents.CallOnCollectCoin(addCoinValue);
        SoundManager.instance.PlaySound("AddCoin");
        ObjectPoolManager.instance.SpawnGameObject(destroyCoinParticles, transform.position, transform.rotation);
        ObjectPoolManager.instance.DespawnGameObject(gameObject);
    }
}