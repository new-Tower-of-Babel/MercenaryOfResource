using System.Collections.Generic;using UnityEngine;

public class SkillTreeBCollectPower2 : MonoBehaviour, ISkill
{
    public int SkillKey { get; }  = 2102;
    public Dictionary<int,bool> AntecedentSkills { get; } = 
        new Dictionary<int,bool>
        {
            { 2201, false },
            { 2301, false }
        };//SkillTreeBCutlogSpeed1,SkillTreeBMiningSpeed1
    
    public int needwood { get; } = 5;
    public int needstone { get; } = 5;
    public void Upgrade()
    {
        PlayDataManager.Instance.characterPlayData.axeDamage += 10;
        PlayDataManager.Instance.characterPlayData.pickaxeDamage += 10;
    }
}