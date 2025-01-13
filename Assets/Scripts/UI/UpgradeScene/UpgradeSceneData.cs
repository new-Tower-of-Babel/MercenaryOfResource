using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSceneData : MonoBehaviour
{
    public static UpgradeSceneData instance;
    public Dictionary<string, bool> CharacterOpenCheck = new Dictionary<string, bool>();
    public Dictionary<string, bool> WeaponOpenCheck = new Dictionary<string, bool>();
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (!CharacterOpenCheck["OldMan"]) CharacterOpenCheck["OldMan"] = true;

        if (!WeaponOpenCheck["Revolver"]) WeaponOpenCheck["Revolver"] = true;
    }
}
