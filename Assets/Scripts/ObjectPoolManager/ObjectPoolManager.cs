using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager instance;

    [SerializeField] private int _initialCapacity = 32;

    private readonly Dictionary<string, Queue<GameObject>> _pool = new();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public void SetInitialCapacity(int initialCapacity)
    {
        _initialCapacity = initialCapacity;
    }

    public GameObject SpawnGameObject(GameObject prefab, bool setActive = true)
    {
        if (instance == null) return null;

        GameObject go = instance.DequeGameObject(prefab);
        if (go != null)
        {
            go.SetActive(setActive);
        }
        else
        {
            go = instance.InstantiateGameObject(prefab, setActive);
        }

        return go;
    }

    public GameObject SpawnGameObject(GameObject prefab, Vector3 position, Quaternion rotation,
        bool setActive = true)
    {
        if (instance == null) return null;
        GameObject go = instance.DequeGameObject(prefab);
        if (go != null)
        {
            go.transform.position = position;
            go.transform.rotation = rotation;
            go.SetActive(setActive);
        }
        else
        {
            go = instance.InstantiateGameObject(prefab, position, rotation, setActive);
        }

        return go;
    }

    public void DespawnGameObject(GameObject go)
    {
        if (go == null) return;
        go.SetActive(false);
        go.GetComponent<IDespawnedPoolObject>()?.ReturnedToPool();
        var pool = instance.GetPool(go);
        pool.Enqueue(go);
    }

    public void PermanentlyDestroyGameObjectsOfType(GameObject prefab)
    {
        if (instance == null) return;
        var queue = instance.GetPool(prefab);
        GameObject go;
        while (queue?.Count > 0)
        {
            go = queue.Dequeue();
            if (go != null)
            {
                if (go.activeSelf)
                {
                    go.SetActive(false);
                }

                Destroy(go);
            }
        }
    }

    public void EmptyPool(GameObject prefab = null)
    {
        if (instance == null) return;
        if (prefab != null)
        {
            var pool = instance.GetPool(prefab);
            if (pool == null) return;
            while (pool.Count > 0)
            {
                var go = pool.Dequeue();
                if (go != null)
                {
                    Destroy(go);
                }
            }
            pool.Clear();
            return;
        }
        foreach (Queue<GameObject> pool in instance._pool.Values)
        {
            while (pool.Count > 0)
            {
                GameObject go = pool.Dequeue();
                if (go != null)
                {
                    Destroy(go);
                }
            }
        }

        instance._pool.Clear();
    }

    private GameObject DequeGameObject(GameObject prefab)
    {
        var queue = GetPool(prefab);
        if (queue.Count < 1) return null;
        GameObject go = queue.Dequeue();
        if (go == null)
        {
            Debug.LogWarning("Dequeued null gameObject (" + prefab.name + ") from pool.");
        }
        go.GetComponent<IRetrievedPoolObject>()?.RetrievedFromPool(prefab);
        return go;
    }

    private GameObject InstantiateGameObject(GameObject prefab, bool setActive)
    {
        var queue = GetPool(prefab);
        var go = Instantiate(prefab);
        DontDestroyOnLoad(go);
        go.SetActive(setActive);
        go.name = prefab.name;
        return go;
    }

    private GameObject InstantiateGameObject(GameObject prefab, Vector3 position, Quaternion rotation,
        bool setActive)
    {
        GameObject go = Instantiate(prefab, position, rotation);
        DontDestroyOnLoad(go);
        go.SetActive(setActive);
        go.name = prefab.name;
        return go;
    }

    private Queue<GameObject> GetPool(GameObject prefab)
    {
        Queue<GameObject> pool;

        if (_pool.ContainsKey(prefab.name))
        {
            pool = _pool[prefab.name];
        }
        else
        {
            pool = new Queue<GameObject>(_initialCapacity);
            _pool.Add(prefab.name, pool);
        }

        return pool;
    }
}