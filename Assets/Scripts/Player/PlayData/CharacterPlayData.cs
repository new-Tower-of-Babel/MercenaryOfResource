using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CharacterPlayData : MonoBehaviour
{
    public int maxHealth;
    public int nowHealth;
    public float moveSpeed;
    public int axeDamage;
    public int pickaxeDamage;
    public float rockHarvestingSpeed;
    public float woodHarvestingSpeed;
    public int resourceCarryLimit;
    
    private void Start()
    {
        startCharacterStatsSeting();
    }
    private void startCharacterStatsSeting()
    {
        string _key = SelectList.Instance.characterDic.FirstOrDefault(x => x.Value == true).Key;
        GunslingerStatsSO selectingCharacter =
            SelectList.Instance.characterSoList.Find(item => item.CharacterName == _key);
        maxHealth = selectingCharacter.maxHealth;
        moveSpeed = selectingCharacter.moveSpeed;
        axeDamage = selectingCharacter.axeDamage;
        pickaxeDamage = selectingCharacter.pickaxeDamage;
        rockHarvestingSpeed = selectingCharacter.rockHarvestingSpeed;
        woodHarvestingSpeed = selectingCharacter.woodHarvestingSpeed;
        resourceCarryLimit = selectingCharacter.resourceCarryLimit;
    }
}
