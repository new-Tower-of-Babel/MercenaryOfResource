using System.Collections.Generic;using UnityEngine;

public class SkillTreeBCollectPower1 : MonoBehaviour, ISkill
{
    public int SkillKey { get; }  = 2101;
    public Dictionary<int,bool> AntecedentSkills { get; } = new Dictionary<int,bool>{};
}