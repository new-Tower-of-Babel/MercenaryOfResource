using System.Collections.Generic;using UnityEngine;

public class SkillTreeBCutlogSpeed2 : MonoBehaviour, ISkill
{
    public int SkillKey { get; }  = 2202;
    public Dictionary<int,bool> AntecedentSkills { get; } = 
        new Dictionary<int,bool>{{ 2102,false }};//SkillTreeBCollectPower2
    
    public int needwood { get; } = 4;
    public int needstone { get; } = 4;
    public void Upgrade()
    {
        PlayDataManager.Instance.characterPlayData.woodHarvestingSpeed -= 0.25f;
    }
}