using System.Collections;
using UnityEngine;

public class AudioManager : SingletonBase<AudioManager>
{
    private AudioSource _bgmSource;
    private AudioSourcePool _audioSourcePool;

    [Header("BGM")]
    [SerializeField] private AudioClip bgmClip;

    [Header("SFX")]
    [SerializeField] private AudioClip clickSfx;


    public override void Awake()
    {
        base.Awake();
        _audioSourcePool = FindObjectOfType<AudioSourcePool>();
    }

    void Start()
    {
        SetBGMSource();
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

    public void PlaySFX(AudioClip clip)
    {
        // Get audioSource from pool
        AudioSource audioSource = _audioSourcePool.GetAudioSource();
        audioSource.PlayOneShot(clip);  // Play one time

        // Return to pool after SfX end
        StartCoroutine(ReturnAudioSourceAfterPlay(audioSource));
    }

    private IEnumerator ReturnAudioSourceAfterPlay(AudioSource audioSource)
    {
        // Wait for sound ending
        yield return new WaitForSeconds(audioSource.clip.length);
        _audioSourcePool.ReturnAudioSource(audioSource);    // return to pool
    }

    // examples
    public void PlayClickSFX() => PlaySFX(clickSfx);
    //public void PlayHitSFX() => PlaySFX(hitSfx);
    //public void PlayAttackSFX() => PlaySFX(attackSfx);
}
