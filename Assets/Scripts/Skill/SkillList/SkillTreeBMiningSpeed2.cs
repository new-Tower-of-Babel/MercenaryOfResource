using System.Collections.Generic;using UnityEngine;

public class SkillTreeBMiningSpeed2 : MonoBehaviour, ISkill
{
    public int SkillKey { get; }  = 2302;
    public Dictionary<int,bool> AntecedentSkills { get; } = 
        new Dictionary<int,bool> {{ 2102,false }}; //SkillTreeBCollectPower2
    
    public int needwood { get; } = 4;
    public int needstone { get; } = 4;
    public void Upgrade()
    {
        PlayDataManager.Instance.characterPlayData.rockHarvestingSpeed -= 0.25f;
    }
}