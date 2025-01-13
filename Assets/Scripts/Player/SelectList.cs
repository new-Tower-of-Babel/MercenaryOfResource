using System.Collections.Generic;
using UnityEngine;

public class SelectList : MonoBehaviour
{
    public static SelectList instance;
    public Dictionary<string, bool> CharacterDic = new Dictionary<string, bool>();
    public Dictionary<string, bool> WeaponDic = new Dictionary<string, bool>();
    public List<GunslingerStatsSO> characterSoList;
    public List<WeaponStatsSO> weaponSoList;
    public Dictionary<string, GunslingerStatsSO> CharacterDataDic = new Dictionary<string, GunslingerStatsSO>();
    public Dictionary<string, WeaponStatsSO> WeaponDataDic = new Dictionary<string, WeaponStatsSO>();
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
        for (int i = 0; i < characterSoList.Count; i++)
        {
            string characterName = characterSoList[i].name;
            CharacterDic.Add(characterName,false);
            UpgradeSceneData.instance.CharacterOpenCheck.Add(characterName,false);
            CharacterDataDic.Add(characterName, characterSoList[i]);
        }

        for (int i = 0; i < weaponSoList.Count; i++)
        {
            string weaponName = weaponSoList[i].name;
            WeaponDic.Add(weaponName,false);
            UpgradeSceneData.instance.WeaponOpenCheck.Add(weaponName,false);
            WeaponDataDic.Add(weaponName,weaponSoList[i]);
            
        }
        //select씬에서 캐릭터 및 무기 선택시 선택된 것들 
        SelectCharacter();
        SelectWeapon();
        
    }

    private void SelectCharacter()
    {
        if (CharacterDic.ContainsKey("OldMan"))
        {
            CharacterDic["OldMan"] = true;
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
