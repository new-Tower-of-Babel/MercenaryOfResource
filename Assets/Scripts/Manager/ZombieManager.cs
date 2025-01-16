using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ZombieManager : SingletonBase<ZombieManager>
{
    [Header("Normal Zombie")]
    [SerializeField] private GameObject Actisdato;
    [SerializeField] private GameObject Kurniawan;
    [SerializeField] private GameObject Pedroso;

    [Header("Special Zombie")]
    [SerializeField] private GameObject Parasite_Starkie;
    [SerializeField] private GameObject Whiteclown_Hallin;

    private Dictionary<string, ObjectPool> _zombiePools;

    private List<GameObject> activeZombieList = new();
    public IReadOnlyList<GameObject> ActiveZombieList => activeZombieList;
    public int activeZombieCount => activeZombieList.Count;
    public event Action OnAllZombieDied;

    public override void Awake()
    {
        _zombiePools = new Dictionary<string, ObjectPool>();
    }

    public override void Start()
    {
        // 각 좀비 타입에 대해 풀을 초기화
        InitializeZombiePool(Actisdato, 10);
        InitializeZombiePool(Kurniawan, 10);
        InitializeZombiePool(Pedroso, 10);

        InitializeZombiePool(Parasite_Starkie, 5);
        InitializeZombiePool(Whiteclown_Hallin, 3);

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
    public void SpawnZombie(string zombieType)
    {
        if (_zombiePools.ContainsKey(zombieType))
        {
            ObjectPool pool = _zombiePools[zombieType];
            GameObject zombie = pool.GetObject();

            zombie.GetComponent<ZombieBase>().OnDied.AddListener(ZombieBase_OnDied);
            zombie.SetActive(false);
            zombie.transform.position = GetSpawnPosition();
            
            zombie.SetActive(true);
            
            activeZombieList.Add(zombie);
        }
    }

    private Vector3 GetSpawnPosition()
    {
        float x = Random.Range(-40f, 40f);
        float z = Random.Range(-40f, 40f);

        return new Vector3(x, 0f, z);
    }

    private void ZombieBase_OnDied(GameObject obj)
    {
        obj.GetComponent<ZombieBase>().OnDied.RemoveListener(ZombieBase_OnDied);
        activeZombieList.Remove(obj);
        PlayDataManager.Instance.resourcePlayData.skull++;
        if (activeZombieList.Count <= 0)
        {
            OnAllZombieDied?.Invoke();
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
