using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetWaitAudio : MonoBehaviour
{
    [SerializeField] private AudioClip waitBGM;

    void Start()
    {
        AudioManager.Instance.PlayBGM(waitBGM);
    }
}
