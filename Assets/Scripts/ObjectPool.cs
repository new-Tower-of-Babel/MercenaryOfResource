using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance { get; private set; }
    
    
    [System.Serializable]
    private class PoolInfo
    {
        public string name;
        public GameObject prefab;
        public int initialSize;
    }

    private class Pool
    {
        public GameObject prefab;
        public Queue<GameObject> objects;
    }
    

    [SerializeField] private PoolInfo[] poolInfoArray;

    private Dictionary<string, Pool> poolDic;

    
    private void Awake()
    {
        Instance = this;

        poolDic = new Dictionary<string, Pool>();
        foreach (PoolInfo poolInfo in poolInfoArray)
        {
            Pool newPool = new Pool();
            newPool.prefab = poolInfo.prefab;
            newPool.objects = new Queue<GameObject>();
            poolDic.Add (poolInfo.name, newPool);

            for (int i = 0; i < poolInfo.initialSize; i++)
            {
                var newObj = CreateNewObject (poolInfo.name, poolInfo.prefab);
                newPool.objects.Enqueue (newObj);
            }
        }
    }

    
    public GameObject Spawn (string name)
    {
        if (poolDic.TryGetValue (name, out Pool pool))
        {
            if (pool.objects.Count == 0)
                return CreateNewObject (name, pool.prefab);
            else
                return pool.objects.Dequeue();
        }

        return null;
    }

    
    public void Despawn (GameObject obj)
    {
        if (poolDic.TryGetValue (obj.name, out Pool pool))
            pool.objects.Enqueue (obj);
    }

    
    private GameObject CreateNewObject(string objName, GameObject prefab)
    {
        var obj = Instantiate (prefab);
        obj.name = objName;
        obj.SetActive (false);
        return obj;
    }
}
