using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private Queue<GameObject> _pool;     // basic pool
    private GameObject _prefab;

    // Pool and pool size Initialize
    public void Initialize(GameObject prefab, int size)
    {
        _pool = new Queue<GameObject>();
        _prefab = prefab;

        // Create object as much as pool size
        for (int i = 0; i < size; i++)
        {
            GameObject obj = CreateObject();
            obj.SetActive(false);
            _pool.Enqueue(obj);
        }
    }

    // Get object from pool
    public GameObject GetObject()
    {
        // If pool have object => Send
        if (_pool.Count > 0)
        {
            GameObject obj = _pool.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        // Pool is empty => Create
        else
        {
            return CreateObject();
        }
    }

    // Return object to pool
    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        _pool.Enqueue(obj);
    }

    // Create object
    private GameObject CreateObject()
    {
        GameObject obj = Instantiate(_prefab);
        obj.SetActive(false);
        obj.transform.SetParent(transform);
        return obj;
    }
}
