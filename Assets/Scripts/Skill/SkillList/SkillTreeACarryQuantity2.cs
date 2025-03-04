using System.Collections.Generic;using UnityEngine;

public class SkillTreeACarryQuantity2 : MonoBehaviour, ISkill
{
    public int SkillKey { get; }  = 1202;
    public Dictionary<int,bool> AntecedentSkills { get; } = 
        new Dictionary<int,bool> {{ 1201,false }};//SkillTreeACarryQuantity1
    
    public int needwood { get; } = 5;
    public int needstone { get; } = 5;
    public void Upgrade()
    {
        PlayDataManager.Instance.characterPlayData.resourceCarryLimit += 1;
    }
}