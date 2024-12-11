using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SkillDataManagaer : MonoBehaviour
{
    public static List<int> haveSkillKey = new List<int>();
    /*public static Dictionary<string, bool> Skills = new Dictionary<string, bool>
    {
        { "skill_AttackPower_Level", false },
        { "skill_HP_Level", false },
        { "skill_DefensePower_Level", false },
        { "skill_AttackSpeed_Level", false },
        { "skill_Grenade_Level", false },
        { "skill_Mine_Level", false }
    };

    public static string currentSkill;*/
}

public class SkillTreeAMoveSpeed1
{
    public static int skillKey { get; } = 1101;
}

public class SkillTreeAMoveSpeed2
{
    public static readonly int skillKey = 1102;
    public static readonly List<int> antecedentSkill = 
        new List<int>() { SkillTreeAMoveSpeed1.skillKey };
}

public class SkillTreeAMoveSpeed3
{
    public static readonly int skillKey = 1103;
    public static readonly List<int> antecedentSkill = 
        new List<int>() { SkillTreeAMoveSpeed2.skillKey };
}

public class SkillTreeACarryQuantity1
{
    public static readonly int skillKey = 1201;
    public static readonly List<int> antecedentSkill = 
        new List<int>() { SkillTreeAMoveSpeed1.skillKey };
}

public class SkillTreeACarryQuantity2
{
    public static readonly int skillKey = 1202;
    public static readonly List<int> antecedentSkill = 
        new List<int>() { SkillTreeACarryQuantity1.skillKey };
}

public class SkillTreeBCollectPower1
{
    public static readonly int skillKey = 2101;
}

public class SkillTreeBCollectPower2
{
    public static readonly int skillKey = 2102;
    public static readonly List<int> antecedentSkill = 
        new List<int>() { SkillTreeBCutlogSpeed1.skillKey, SkillTreeBMiningSpeed1.skillKey };
}

public class SkillTreeBCollectPower3
{
    public static readonly int skillKey = 2103;
    public static readonly List<int> antecedentSkill = 
        new List<int>() { SkillTreeBCutlogSpeed2.skillKey, SkillTreeBMiningSpeed2.skillKey };
}

public class SkillTreeBCutlogSpeed1
{
    public static readonly int skillKey = 2201;
    public static readonly List<int> antecedentSkill = 
        new List<int>() { SkillTreeBCollectPower1.skillKey };
}

public class SkillTreeBCutlogSpeed2
{
    public static readonly int skillKey = 2202;
    public static readonly List<int> antecedentSkill = 
        new List<int>() { SkillTreeBCollectPower2.skillKey };
}

public class SkillTreeBMiningSpeed1
{
    public static readonly int skillKey = 2301;
    public static readonly List<int> antecedentSkill = 
        new List<int>() { SkillTreeBCollectPower1.skillKey };
}

public class SkillTreeBMiningSpeed2
{
    public static readonly int skillKey = 2301;
    public static readonly List<int> antecedentSkill = 
        new List<int>() { SkillTreeBCollectPower2.skillKey };
}