using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonBase<AudioManager>
{
    private AudioSource _bgmSource;

    [Header("BGM")]
    [SerializeField] private AudioClip bgmClip;

    [Header("SFX")]
    [SerializeField] private AudioClip fireSFX;

    private Dictionary<string, ObjectPool<AudioSource>> _audioPools;

    public override void Awake()
    {
        _audioPools = new Dictionary<string, ObjectPool<AudioSource>>();
    }

    public override void Start()
    {
        SetBGMSource();
        InitializeAudioPool(fireSFX, 10);
    }

    private void InitializeAudioPool(AudioClip clip, int poolSize)
    {
        AudioSource audioSourcePrefab = new GameObject(clip.name + "AudioSource").AddComponent<AudioSource>();
        ObjectPool<AudioSource> pool = new ObjectPool<AudioSource>();
        pool.Initialize(audioSourcePrefab, poolSize);
        _audioPools.Add(clip.name, pool);
    }

    private void SetBGMSource()
    {
        // Add audioSource component
        _bgmSource = gameObject.AddComponent<AudioSource>();

        // Set bgm property
        _bgmSource.loop = true;
        _bgmSource.volume = 0.15f;
    }

    public void PlayBGM(AudioClip clip)
    {
        if (_bgmSource.clip != clip)
        {
            _bgmSource.Stop();
            _bgmSource.clip = clip;
            _bgmSource.Play();
        }
    }

    private void PlaySFX(AudioClip clip)
    {
        string poolName = clip.name;

        AudioSource audioSource;
        if (_audioPools.ContainsKey(poolName))
        {
            // Get audioSource from pool
            ObjectPool<AudioSource> pool = _audioPools[poolName];
            audioSource = pool.GetObject();

            // Play one time
            audioSource.PlayOneShot(clip);

            // Return to pool after SfX end
            StartCoroutine(ReturnAudioSourceAfterPlay(audioSource, poolName));
        }
    }

    private IEnumerator ReturnAudioSourceAfterPlay(AudioSource audioSource, string poolName)
    {
        yield return new WaitForSeconds(audioSource.clip.length);
        _audioPools[poolName].ReturnObject(audioSource);
    }

    // examples
    public void PlayFireSFX() => PlaySFX(fireSFX);
}
