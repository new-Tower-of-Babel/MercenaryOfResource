using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MonsterSpawner : MonoBehaviour
{
    public static MonsterSpawner Instance { get; private set; }
    
    private List<GameObject> activeMonsters = new();

    public IReadOnlyList<GameObject> ActiveMonsters => activeMonsters;

    public int MonsterCount => activeMonsters.Count;

    public event Action OnAllMonstersDied;

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnMonster()
    {
        GameObject monster = ObjectPool.Instance.Spawn ("StandardMonster");
        monster.transform.position = GetSpawnPosition();
        monster.GetComponent<ZombieBase>().OnDied.AddListener (MonsterBase_OnDied);
        monster.SetActive (true);
        activeMonsters.Add (monster);
    }

    private Vector3 GetSpawnPosition()
    {
        float x = Random.Range (-40f, 40f);
        float z = Random.Range (-40f, 40f);
        return new Vector3 (x, 0f, z);
    }
    
    private void MonsterBase_OnDied(GameObject obj)
    {
        activeMonsters.Remove (obj);
        PlayDataManager.Instance.resourcePlayData.skull++;

        if (activeMonsters.Count <= 0)
        {
            OnAllMonstersDied?.Invoke();
        }
    }
}
