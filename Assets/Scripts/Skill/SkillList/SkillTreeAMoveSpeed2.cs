using System.Collections.Generic;
using UnityEngine;

public class SkillTreeAMoveSpeed2 : ISkill
{
    public int SkillKey { get; } = 1102;
    public Dictionary<int,bool> AntecedentSkills { get; } = 
        new Dictionary<int,bool>{ { 1101,false }}; //SkillTreeAMoveSpeed1
    public int needwood { get; } = 1;
    public int needstone { get; } = 1;
}