using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : SingletonBase<AudioManager>
{
    private AudioSource _bgm;
    private AudioSource _dayChange;

    [Header("BGM")]
    [SerializeField] private AudioClip bgm;

    [Header("SFX")]
    [SerializeField] private AudioClip dayChange;
    [SerializeField] private AudioClip fire;

    [Header("UI")]
    [SerializeField] private AudioClip click;

    public float globalVolume = 100;
    public float bgmVolume = 50;
    public float sfxVolume = 50;
    public float uiVolume = 50;

    private Dictionary<string, ObjectPool> _audioPools;

    private void SetClip()
    {
        fire = Resources.Load<AudioClip>("Sound/GunFire");
        click = Resources.Load<AudioClip>("Sound/Click");
    }

    public override void Awake()
    {
        base.Awake();
        _audioPools = new Dictionary<string, ObjectPool>();
        SetClip();
        SetBGMSource();
    }

    public override void Start()
    {
        if (fire != null) InitializeAudioPool(fire, 10);
        else Debug.LogWarning("Fire SFX clip is missing!");

        if (click != null) InitializeAudioPool(click, 5);
        else Debug.LogWarning("Click SFX clip is missing!");
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
        _bgm = gameObject.AddComponent<AudioSource>();
        _dayChange = gameObject.AddComponent<AudioSource>();

        // Set bgm property
        _bgm.loop = true;
        _dayChange.loop = false;

        // Set volume
        _bgm.volume = globalVolume * (bgmVolume / 100f);
        _dayChange.volume = globalVolume * (sfxVolume / 100f);
    }

    public void PlayBGM(AudioClip clip)
    {
        if (_bgm.clip != clip)
        {
            _bgm.Stop();
            _bgm.clip = clip;
            _bgm.Play();
        }

        _bgm.volume = globalVolume * (bgmVolume / 100f);
    }

    public void PlayClipOnce(AudioClip clip)
    {
        _dayChange.volume = globalVolume * (sfxVolume / 100f);
        _dayChange.PlayOneShot(clip);
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

            audioSource.volume = globalVolume * (sfxVolume / 100f);
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

    public void PlayFireSFX() => PlaySFX(fire);
}
