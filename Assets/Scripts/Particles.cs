using UnityEngine;

public class Particles : MonoBehaviour
{
    public void DestroyParticle()
    {
        ObjectPoolManager.instance.DespawnGameObject(this.gameObject);
    }
}