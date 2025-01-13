using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTitleAudio : MonoBehaviour
{
    [SerializeField] private AudioClip titleBGM;

    void Start()
    {
        AudioManager.Instance.PlayBGM(titleBGM);
    }
}
