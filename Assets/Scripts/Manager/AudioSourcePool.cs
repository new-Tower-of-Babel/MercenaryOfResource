using System.Collections.Generic;
using UnityEngine;

public class AudioSourcePool : MonoBehaviour
{
    [Header("AudioSource Pool Settings")]
    [SerializeField] private int poolSize = 10;     // AudioSource default count in pool
    private Queue<AudioSource> _audioSourcePool;    // Queue that manage audioSource

    private void Awake()
    {
        _audioSourcePool = new Queue<AudioSource>();

        // ObjectPool initialize: create audioSource of specified size
        for (int i = 0; i < poolSize; i++)
        {
            AudioSource audioSource = new GameObject("AudioSource_" + i).AddComponent<AudioSource>();
            audioSource.playOnAwake = false;    // Set: not to play auto
            _audioSourcePool.Enqueue(audioSource);  // Add in queue(pool)
        }
    }

    // Method: get audioSource from pool
    public AudioSource GetAudioSource()
    {
        if (_audioSourcePool.Count > 0)
        {
            AudioSource audioSource = _audioSourcePool.Dequeue();
            return audioSource;
        }
        else
        {
            // If not have audioSource => Create new audioSource
            AudioSource audioSource = new GameObject("NewAudioSource").AddComponent<AudioSource>();
            return audioSource;
        }
    }

    // Method: Return audioSource to pool
    public void ReturnAudioSource(AudioSource audioSource)
    {
        audioSource.Stop();     // Stop if audio is playing
        _audioSourcePool.Enqueue(audioSource);  // Return to pool
    }
}
