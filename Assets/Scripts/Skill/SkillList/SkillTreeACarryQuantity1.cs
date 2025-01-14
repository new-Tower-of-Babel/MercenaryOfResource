using System.Collections.Generic;using UnityEngine;

public class SkillTreeACarryQuantity1 : MonoBehaviour, ISkill
{
    public int SkillKey { get; }  = 1201;
    public Dictionary<int,bool> AntecedentSkills { get; } = 
        new Dictionary<int,bool> { { 1101,false }}; //SkillTreeAMoveSpeed1

    public int needwood { get; } = 4;
    public int needstone { get; } = 4;
    public void Upgrade()
    {
        PlayDataManager.Instance.characterPlayData.resourceCarryLimit += 1;
    }
}