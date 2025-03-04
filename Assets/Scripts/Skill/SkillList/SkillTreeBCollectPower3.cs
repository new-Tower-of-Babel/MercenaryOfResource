using System.Collections.Generic;using UnityEngine;

public class SkillTreeBCollectPower3 : MonoBehaviour, ISkill
{
    public int SkillKey { get; }  = 2103;
    public Dictionary<int,bool>AntecedentSkills { get; } = 
        new Dictionary<int,bool>
        {
            { 2202,false },
            { 2302,false }
        };//SkillTreeBCutlogSpeed2,SkillTreeBMiningSpeed2
    public int needwood { get; } = 6;
    public int needstone { get; } = 6;
    public void Upgrade()
    {
        PlayDataManager.Instance.characterPlayData.axeDamage += 10;
        PlayDataManager.Instance.characterPlayData.pickaxeDamage += 10;
    }
}