using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : SingletonBase<AudioManager>
{
    private AudioSource _bgm;
    private AudioSource _dayChange;

    [Header("BGM")]
    private AudioClip bgm;

    [Header("SFX")]
    private AudioClip dayChange;
    private AudioClip fire;
    private AudioClip playerHit;
    private AudioClip zombieAttack;

    private AudioClip open;
    private AudioClip locked;

    [Header("UI")]
    private AudioClip click;

    public float globalVolume = 100;
    public float bgmVolume = 50;
    public float sfxVolume = 50;
    public float uiVolume = 50;

    private Dictionary<string, ObjectPool> _audioPools;

    private void SetClip()
    {
        fire = Resources.Load<AudioClip>("Sound/GunFire");
        click = Resources.Load<AudioClip>("Sound/Click");
        playerHit = Resources.Load<AudioClip>("Sound/PlayerHit");
        zombieAttack = Resources.Load<AudioClip>("Sound/ZombieAttack");

        open = Resources.Load<AudioClip>("Sound/Open");
        locked = Resources.Load<AudioClip>("Sound/locked");
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
        InitializeAudioPool(fire, 10);
        InitializeAudioPool(click, 5);
        InitializeAudioPool(playerHit, 5);
        InitializeAudioPool(zombieAttack, 10);

        InitializeAudioPool(open, 3);
        InitializeAudioPool(locked, 3);
    }

    // �����?Ǯ�� �ʱ�ȭ�ϰ� ��ųʸ���?�߰�
    private void InitializeAudioPool(AudioClip clip, int size)
    {
        AudioSource audioSourcePrefab = new GameObject(clip.name + "AudioSource").AddComponent<AudioSource>();
        ObjectPool pool = new GameObject(clip.name + "Pool").AddComponent<ObjectPool>();

        pool.transform.SetParent(this.transform);
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
        _bgm.volume = (globalVolume / 100f) * (bgmVolume / 100f);
        _dayChange.volume = (globalVolume / 100f) * (sfxVolume / 100f);
    }

    public void SetBGMVolume()
    {
        _bgm.volume = (globalVolume / 100f) * (bgmVolume / 100f);
    }

    public void PlayBGM(AudioClip clip)
    {
        if (_bgm.clip != clip)
        {
            _bgm.Stop();
            _bgm.clip = clip;
            _bgm.Play();
        }

        _bgm.volume = (globalVolume / 100f) * (bgmVolume / 100f);
    }

    public void PlayClipOnce(AudioClip clip)
    {
        _dayChange.volume = (globalVolume / 100f) * (sfxVolume / 100f);
        _dayChange.PlayOneShot(clip);
    }

    // SFX ���带 ������ Ǯ���� ���?
    public void PlaySFX(AudioClip clip)
    {
        string poolName = clip.name + "Pool";

        if (_audioPools.ContainsKey(poolName))
        {
            ObjectPool pool = _audioPools[poolName];
            GameObject audioSourceObject = pool.GetObject();
            AudioSource audioSource = audioSourceObject.GetComponent<AudioSource>();

            audioSource.volume = (globalVolume / 100f) * (sfxVolume / 100f);
            audioSource.PlayOneShot(clip);

            StartCoroutine(ReturnAudioSourceAfterPlay(audioSourceObject, poolName, clip.length));
        }
    }

    private IEnumerator ReturnAudioSourceAfterPlay(GameObject audioSourceObject, string poolName, float clipLength)
    {
        yield return new WaitForSeconds(clipLength);
        _audioPools[poolName].ReturnObject(audioSourceObject);
    }

    public void PlayClickSFX() => PlaySFX(click);
    public void PlayFireSFX() => PlaySFX(fire);
    public void PlayHitSFX() => PlaySFX(playerHit);
    public void PlayZombieAttackSFX() => PlaySFX(zombieAttack);
    public void PlayOpenSFX() => PlaySFX(open);
    public void PlayLockedSFX() => PlaySFX(locked);
}
