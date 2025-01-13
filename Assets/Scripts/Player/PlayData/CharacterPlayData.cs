
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
        SelectList.instance.SelectCharacter();
        StartCharacterStatsSeting();
        nowHealth = maxHealth;
    }
    private void StartCharacterStatsSeting()
    {
        string key = SelectList.instance.CharacterDic.FirstOrDefault(x => x.Value == true).Key;
        GunslingerStatsSO selectingCharacter =
            SelectList.instance.characterSoList.Find(item => item.CharacterName == key);
        maxHealth = selectingCharacter.maxHealth;
        moveSpeed = selectingCharacter.moveSpeed;
        axeDamage = selectingCharacter.axeDamage;
        pickaxeDamage = selectingCharacter.pickaxeDamage;
        rockHarvestingSpeed = selectingCharacter.rockHarvestingSpeed;
        woodHarvestingSpeed = selectingCharacter.woodHarvestingSpeed;
        resourceCarryLimit = selectingCharacter.resourceCarryLimit;
    }
}
