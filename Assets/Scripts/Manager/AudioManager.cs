using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : SingletonBase<AudioManager>
{
    private AudioSource _bgmSource;

    [Header("BGM")]
    [SerializeField] private AudioClip bgmClip;

    [Header("SFX")]
    [SerializeField] private AudioClip fireSFX;

    private Dictionary<string, ObjectPool> _audioPools;

    public override void Awake()
    {
        _audioPools = new Dictionary<string, ObjectPool>();
    }

    public override void Start()
    {
        SetBGMSource();
        InitializeAudioPool(fireSFX, 10);
    }

    // 오디오 풀을 초기화하고 딕셔너리에 추가
    private void InitializeAudioPool(AudioClip clip, int size)
    {
        AudioSource audioSourcePrefab = new GameObject(clip.name + "AudioSource").AddComponent<AudioSource>();
        ObjectPool pool = new GameObject(clip.name + "Pool").AddComponent<ObjectPool>();
        pool.Initialize(audioSourcePrefab.gameObject, size);
        _audioPools.Add(clip.name + "Pool", pool);
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

    // SFX 사운드를 지정된 풀에서 재생
    public void PlaySFX(AudioClip clip)
    {
        string poolName = clip.name + "Pool";

        if (_audioPools.ContainsKey(poolName))
        {
            ObjectPool pool = _audioPools[poolName];
            GameObject audioSourceObject = pool.GetObject();
            AudioSource audioSource = audioSourceObject.GetComponent<AudioSource>();
            audioSource.PlayOneShot(clip);

            StartCoroutine(ReturnAudioSourceAfterPlay(audioSourceObject, poolName));
        }
    }

    private IEnumerator ReturnAudioSourceAfterPlay(GameObject audioSourceObject, string poolName)
    {
        AudioSource audioSource = audioSourceObject.GetComponent<AudioSource>();
        yield return new WaitForSeconds(audioSource.clip.length);
        _audioPools[poolName].ReturnObject(audioSourceObject);
    }

    // examples
    public void PlayFireSFX() => PlaySFX(fireSFX);
}
