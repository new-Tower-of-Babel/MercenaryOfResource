using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SelectList : MonoBehaviour
{
    public static SelectList Instance;
    public Dictionary<string, bool> characterDic = new Dictionary<string, bool>();
    public Dictionary<string, bool> WeaponDic = new Dictionary<string, bool>();
    public List<GunslingerStatsSO> characterSoList;
    public List<WeaponStatsSO> weaponSoList;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        for (int i = 0; i < characterSoList.Count; i++)
        {
            characterDic.Add(characterSoList[i].name,false);
        }

        for (int i = 0; i < weaponSoList.Count; i++)
        {
            WeaponDic.Add(weaponSoList[i].name,false);
        }
        //select씬에서 캐릭터 및 무기 선택시 선택된 것들 
        SelectCharacter();
        SelectWeapon();
        
    }

    private void SelectCharacter()
    {
        if (characterDic.ContainsKey("OldMan"))
        {
            characterDic["OldMan"] = true;
        }
    }

    private void SelectWeapon()
    {
        if (WeaponDic.ContainsKey("Revolver"))
        {
            WeaponDic["Revolver"] = true;
        }
    }

}
