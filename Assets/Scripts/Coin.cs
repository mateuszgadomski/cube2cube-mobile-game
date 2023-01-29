using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private float _addCoinValue = 1f;
    [SerializeField] private GameObject _destroyCoinParticles;

    private readonly string _addCoinSoundName = "AddCoin";

    private void Start()
    {
        EventManager.PlayerEvents.OnGameObjectTouchedCallback += IsTouched;
    }

    private void OnDestroy()
    {
        EventManager.PlayerEvents.OnGameObjectTouchedCallback -= IsTouched;
    }

    public void AddCoin()
    {
        SoundManager.instance.PlaySound(_addCoinSoundName);
        EventManager.PlayerEvents.CallOnCollectCoin(_addCoinValue);
        ObjectPoolManager.instance.SpawnGameObject(_destroyCoinParticles, transform.position, transform.rotation);
        ObjectPoolManager.instance.DespawnGameObject(this.gameObject);
    }

    public void DestroyCoin()
    {
        ObjectPoolManager.instance.SpawnGameObject(_destroyCoinParticles, transform.position, transform.rotation);
        ObjectPoolManager.instance.DespawnGameObject(this.gameObject);
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