using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetWait : MonoBehaviour
{
    [SerializeField] private AudioClip waitBGM;

    void Start()
    {
        AudioManager.Instance.PlayBGM(waitBGM);
    }
}
