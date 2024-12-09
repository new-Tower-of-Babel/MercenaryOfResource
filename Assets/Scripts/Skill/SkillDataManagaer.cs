using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDataManagaer : MonoBehaviour
{
    public static Dictionary<string, bool> Skills = new Dictionary<string, bool>
    {
        { "skill_TreeA_MoveSpeed_Level_1", false },
        { "skill_TreeA_MoveSpeed_Level_2", false },
        { "skill_TreeA_MoveSpeed_Level_3", false },
        { "skill_TreeA_CarryQuantity_Level_1", false },
        { "skill_TreeA_CarryQuantity_Level_2", false },
        { "skill_TreeB_CollectPower_Level_1", false },
        { "skill_TreeB_CollectPower_Level_2", false },
        { "skill_TreeB_CollectPower_Level_3", false },
        { "skill_TreeB_CutlogSpeed_Level_1", false },
        { "skill_TreeB_CutlogSpeed_Level_2", false },
        { "skill_TreeB_MiningSpeed_Level_1", false },
        { "skill_TreeB_MiningSpeed_Level_2", false },
        { "skill_AttackPower_Level", false },
        { "skill_HP_Level", false },
        { "skill_DefensePower_Level", false },
        { "skill_AttackSpeed_Level", false },
        { "skill_Grenade_Level", false },
        { "skill_Mine_Level", false }
    };

    public static string currentSkill;
}
