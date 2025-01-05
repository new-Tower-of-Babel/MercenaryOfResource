using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface ISkill
{
    int SkillKey { get; }
    int needwood { get; }
    int needstone { get; }
    Dictionary<int,bool> AntecedentSkills { get; }
    void Upgrade();

}
public class SkillDataManagaer
{
    public static List<int> haveSkillKey = new List<int>();
    
    public static bool haveSkillCheck(int skillKey)
    {
        for (int i = 0; i < haveSkillKey.Count; i++)
        {
            if (haveSkillKey[i] == skillKey)
            {
                return true;
            }
        }
        return false;
    }

    public static bool AntecedentSkillsCheck(bool skillCheck, Dictionary<int,bool> currentAntecedentSkills)
    {
        if (skillCheck) return true;
        else
        {
            var skillList = currentAntecedentSkills.Keys.ToList();
            foreach (var skillkey in skillList)
            {
                for (int i = 0; i < haveSkillKey.Count; i++)
                {
                    if (skillkey==haveSkillKey[i])
                    {
                        currentAntecedentSkills[skillkey] = true;
                    }
                }
            }
            foreach (var checking in currentAntecedentSkills)
            {
                if (!checking.Value) return true;
            }
            return false;
        }
    }
}