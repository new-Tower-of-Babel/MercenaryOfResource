using System.Collections.Generic;
using System.Linq;
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
    public string selectedCharacter;
    public string selectedWeapon;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
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
        
    }

    public void SelectCharacter()
    {
        foreach (var pair in CharacterDic.ToList())
        {
            CharacterDic[pair.Key] = false;
        }
        if (CharacterDic.ContainsKey(selectedCharacter))
        {
            CharacterDic[selectedCharacter] = true;
        }
    }

    public void SelectWeapon()
    {
        foreach (var pair in WeaponDic.ToList())
        {
            WeaponDic[pair.Key] = false;
        }
        if (WeaponDic.ContainsKey(selectedWeapon))
        {
            WeaponDic[selectedWeapon] = true;
        }
    }

}
