using System.Collections.Generic;
using UnityEngine;

public class ZombieManager : SingletonBase<ZombieManager>
{
    [SerializeField] private GameObject zombiePrefabA;
    [SerializeField] private GameObject zombiePrefabB;

    private Dictionary<string, ObjectPool<Component>> _zombiePools = new Dictionary<string, ObjectPool<Component>>();

    public override void Start()
    {
        // 각 좀비 타입에 대해 풀을 초기화
        InitializeZombiePool(zombiePrefabA, 10);
        InitializeZombiePool(zombiePrefabB, 5);
    }

    private void InitializeZombiePool(GameObject prefab, int size)
    {
        ObjectPool<Component> pool = new ObjectPool<Component>();
        pool.Initialize(prefab.GetComponent<Component>(), size);
        _zombiePools.Add(prefab.name, pool);
    }

    // 풀 이름으로 좀비를 가져오고 활성화하는 메서드
    public void SpawnZombie(string zombieType, Vector3 position)
    {
        if (_zombiePools.ContainsKey(zombieType))
        {
            ObjectPool<Component> pool = _zombiePools[zombieType];
            Component zombie = pool.GetObject();

            if (zombie != null)
            {
                zombie.gameObject.SetActive(true);
                zombie.transform.position = position;
            }
        }
    }

    // 좀비를 풀로 반환하는 메서드
    public void ReturnZombie(Component zombie, string zombieType)
    {
        if (_zombiePools.ContainsKey(zombieType))
        {
            ObjectPool<Component> pool = _zombiePools[zombieType];
            pool.ReturnObject(zombie);
            zombie.gameObject.SetActive(false);
        }
    }
}
