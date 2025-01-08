using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : SingletonBase<ParticleManager>
{
    [Header("Particle Effects")]
    [SerializeField] private ParticleSystem explosionEffect;
    [SerializeField] private ParticleSystem healEffect;

    private Dictionary<string, ObjectPool> _particlePools;

    public override void Awake()
    {
        _particlePools = new Dictionary<string, ObjectPool>();
    }

    public override void Start()
    {
        // 각 파티클 효과에 대해 풀을 초기화
        InitializeParticlePool(explosionEffect, 3);
        InitializeParticlePool(healEffect, 3);
    }

    // 파티클 풀을 초기화하고 딕셔너리에 추가
    private void InitializeParticlePool(ParticleSystem prefab, int poolSize)
    {
        ObjectPool pool = new ObjectPool();
        pool.Initialize(prefab.gameObject, poolSize);
        _particlePools.Add(prefab.name, pool);
    }

    // 파티클 효과를 풀에서 가져와서 재생
    public void PlayParticleEffect(string effectName, Vector3 position, Quaternion rotation)
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
                    particleSystem.transform.rotation = rotation;
                    particleSystem.Play();  // 파티클 효과 재생

                    // 파티클이 끝난 후 풀로 반환
                    StartCoroutine(ReturnParticleToPoolAfterPlay(particleSystem, effectName));
                }
            }
        }
    }

    // 파티클 효과가 끝난 후 풀에 반환하는 코루틴
    private IEnumerator<WaitForSeconds> ReturnParticleToPoolAfterPlay(ParticleSystem particleSystem, string poolName)
    {
        yield return new WaitForSeconds(particleSystem.main.duration);
        _particlePools[poolName].ReturnObject(particleSystem.gameObject);
    }
}
