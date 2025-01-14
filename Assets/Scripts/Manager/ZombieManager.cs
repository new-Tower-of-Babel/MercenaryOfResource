using System.Collections.Generic;
using UnityEngine;

public class ZombieManager : SingletonBase<ZombieManager>
{
    [SerializeField] private GameObject zombiePrefabA;
    [SerializeField] private GameObject zombiePrefabB;

    private Dictionary<string, ObjectPool> _zombiePools;

    public override void Awake()
    {
        _zombiePools = new Dictionary<string, ObjectPool>();
    }

    public override void Start()
    {
        // 각 좀비 타입에 대해 풀을 초기화
        InitializeZombiePool(zombiePrefabA, 10);
        InitializeZombiePool(zombiePrefabB, 5);
    }

    private void InitializeZombiePool(GameObject prefab, int poolSize)
    {
        // GameObject를 관리하는 풀 초기화
        ObjectPool pool = new GameObject(prefab.name + "Pool").AddComponent<ObjectPool>();
        pool.transform.SetParent(this.transform);
        pool.Initialize(prefab, poolSize);
        _zombiePools.Add(prefab.name, pool);
    }

    // 풀 이름으로 좀비를 가져오고 활성화하는 메서드
    public void SpawnZombie(string zombieType, Vector3 position)
    {
        if (_zombiePools.ContainsKey(zombieType))
        {
            ObjectPool pool = _zombiePools[zombieType];
            GameObject zombie = pool.GetObject();

            if (zombie != null)
            {
                zombie.SetActive(true);
                zombie.transform.position = position;
            }
        }
    }

    // 좀비를 풀로 반환하는 메서드
    public void ReturnZombie(GameObject zombie, string zombieType)
    {
        if (_zombiePools.ContainsKey(zombieType))
        {
            ObjectPool pool = _zombiePools[zombieType];
            pool.ReturnObject(zombie);
            zombie.SetActive(false);
        }
    }
}
