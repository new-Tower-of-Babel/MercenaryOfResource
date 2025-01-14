using System.Collections.Generic;using UnityEngine;

public class SkillTreeAMoveSpeed3 : MonoBehaviour, ISkill
{
    public int SkillKey { get; }  = 1103;
    public Dictionary<int,bool> AntecedentSkills { get; } = 
        new Dictionary<int,bool> {{ 1102,false }}; //SkillTreeAMoveSpeed2
    
    public int needwood { get; } = 5;
    public int needstone { get; } = 5;
    public void Upgrade()
    {
        PlayDataManager.Instance.characterPlayData.moveSpeed += 1f;
    }
}