using System.Collections.Generic;using UnityEngine;

public class SkillTreeBCollectPower1 : MonoBehaviour, ISkill
{
    public int SkillKey { get; }  = 2101;
    public Dictionary<int,bool> AntecedentSkills { get; } = new Dictionary<int,bool>{};
    
    public int needwood { get; } = 4;
    public int needstone { get; } = 4;
    public void Upgrade()
    {
        PlayDataManager.Instance.characterPlayData.axeDamage += 10;
        PlayDataManager.Instance.characterPlayData.pickaxeDamage += 10;
    }
}