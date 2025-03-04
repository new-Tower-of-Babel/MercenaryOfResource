using System.Collections.Generic;using UnityEngine;

public class SkillTreeBMiningSpeed1 : MonoBehaviour, ISkill
{
    public int SkillKey { get; }  = 2301;
    public Dictionary<int,bool> AntecedentSkills { get; } = 
        new Dictionary<int,bool> {{ 2101,false }};//SkillTreeBCollectPower1
    
    public int needwood { get; } = 3;
    public int needstone { get; } = 3;
    public void Upgrade()
    {
        PlayDataManager.Instance.characterPlayData.rockHarvestingSpeed -= 0.25f;
    }
}