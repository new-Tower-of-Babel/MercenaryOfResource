using System.Collections.Generic;using UnityEngine;

public class SkillTreeBMiningSpeed2 : MonoBehaviour, ISkill
{
    public int SkillKey { get; }  = 2301;
    public Dictionary<int,bool> AntecedentSkills { get; } = 
        new Dictionary<int,bool> {{ 2302,false }}; //SkillTreeBCollectPower2
    
    public int needwood { get; } = 1;
    public int needstone { get; } = 1;
    public void Upgrade()
    {
    }
}