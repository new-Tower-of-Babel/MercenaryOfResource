using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface ISkill
{
    int SkillKey { get; }
    Dictionary<int,bool> AntecedentSkills { get; }
}
public class SkillDataManagaer
{
    public static List<int> haveSkillKey = new List<int>();
    
    public static bool haveSkillCheck(int skillKey)
    {
        Debug.Log($"1");
        for (int i = 0; i < haveSkillKey.Count; i++)
        {
            Debug.Log($"2");
            if (haveSkillKey[i] == skillKey)
            {
                Debug.Log($"3");
                return true;
            }
        }
        Debug.Log($"4");
        return false;
    }

    public static bool AntecedentSkillsCheck(bool skillCheck, Dictionary<int,bool> currentAntecedentSkills)
    {
        Debug.Log($"5");
        if (skillCheck) return true;
        else
        {
            Debug.Log($"6");
            var skillList = currentAntecedentSkills.Keys.ToList();
            foreach (var skillkey in skillList)
            {
                Debug.Log($"7");
                for (int i = 0; i < haveSkillKey.Count; i++)
                {
                    Debug.Log($"8");
                    if (skillkey==haveSkillKey[i])
                    {
                        Debug.Log($"9");
                        currentAntecedentSkills[skillkey] = true;
                    }
                }
            }
            foreach (var checking in currentAntecedentSkills)
            {
                if (!checking.Value) return true;
            }
            Debug.Log($"10");
            return false;
        }
    }
}