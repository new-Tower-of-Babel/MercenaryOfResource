using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetResolution : MonoBehaviour
{
    private void Awake()
    {
        Screen.SetResolution(1920,1080,true);
    }
}
