using System.Collections.Generic;using UnityEngine;

public class SkillTreeBCutlogSpeed1 : MonoBehaviour, ISkill
{
    public int SkillKey { get; }  = 2201;
    public Dictionary<int,bool> AntecedentSkills { get; } = 
        new Dictionary<int,bool>{{ 2101 ,false }};//SkillTreeBCollectPower1
}