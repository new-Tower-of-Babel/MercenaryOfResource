using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> : MonoBehaviour where T : Component
{
    private Queue<T> _pool;     // basic pool
    private T _prefab;

    // Pool and pool size Initialize
    public void Initialize(T prefab, int poolSize)
    {
        _pool = new Queue<T>();
        this._prefab = prefab;

        // Create object as much as pool size
        for (int i = 0; i < poolSize; i++)
        {
            T obj = CreateObject();
            _pool.Enqueue(obj);
        }
    }

    // Get object from pool
    public T GetObject()
    {
        // If pool have object => Send
        if (_pool.Count > 0)
        {
            T obj = _pool.Dequeue();
            obj.gameObject.SetActive(true);
            return obj;
        }
        // Pool is empty => Create
        else
        {
            return CreateObject();
        }
    }

    // Return object to pool
    public void ReturnObject(T obj)
    {
        obj.gameObject.SetActive(false);
        _pool.Enqueue(obj);
    }

    // Create object
    private T CreateObject()
    {
        T obj = Instantiate(_prefab);
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(transform);
        return obj;
    }
}
