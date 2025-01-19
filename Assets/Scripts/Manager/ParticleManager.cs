using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : SingletonBase<ParticleManager>
{
    [Header("Particle Effects")]
    [SerializeField] private GameObject ExplosionEffect;
    [SerializeField] private GameObject HealEffect;

    private Dictionary<string, ObjectPool> _particlePools;

    public override void Awake()
    {
        base.Awake();
        _particlePools = new Dictionary<string, ObjectPool>();
        ExplosionEffect = Resources.Load<GameObject>("Particle/ExplosionEffect");
        HealEffect = Resources.Load<GameObject>("Particle/HealEffect");
    }

    public override void Start()
    {
        // 각 파티클 효과에 대해 풀을 초기화
        InitializeParticlePool(ExplosionEffect, 3);
        InitializeParticlePool(HealEffect, 15);
    }

    // 파티클 풀을 초기화하고 딕셔너리에 추가
    private void InitializeParticlePool(GameObject prefab, int poolSize)
    {
        if (_particlePools.ContainsKey(prefab.name)) return;

        ObjectPool pool = new GameObject(prefab.name + "Pool").AddComponent<ObjectPool>();
        pool.transform.SetParent(this.transform);
        pool.Initialize(prefab.gameObject, poolSize);
        _particlePools.Add(prefab.name, pool);
    }

    // 파티클 효과를 풀에서 가져와서 재생
    public void PlayParticleEffect(string effectName, Vector3 position)
    {
        if (_particlePools.ContainsKey(effectName))
        {
            ObjectPool pool = _particlePools[effectName];
            GameObject particleObject = pool.GetObject();

            if (particleObject != null)
            {
                ParticleSystem particleSystem = particleObject.GetComponent<ParticleSystem>();
                if (particleSystem != null)
                {
                    particleSystem.transform.position = position;
                    particleSystem.Play();  // 파티클 효과 재생

                    // 파티클이 끝난 후 풀로 반환
                    StartCoroutine(ReturnParticleToPoolAfterPlay(particleObject, particleSystem, effectName));
                }
            }
        }
    }

    // 파티클 효과가 끝난 후 풀에 반환하는 코루틴
    private IEnumerator<WaitForSeconds> ReturnParticleToPoolAfterPlay(GameObject particleObject, ParticleSystem particleSystem, string poolName)
    {
        yield return new WaitForSeconds(particleSystem.main.duration);
        _particlePools[poolName].ReturnObject(particleObject);
    }
}
