using System.Collections.Generic;using UnityEngine;

public class SkillTreeAMoveSpeed1 : MonoBehaviour, ISkill
{
    public int SkillKey { get; } = 1101;
    public Dictionary<int,bool> AntecedentSkills { get; } = new Dictionary<int,bool> { };
}