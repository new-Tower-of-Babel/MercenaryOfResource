using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeA : MonoBehaviour
{
    public List<GameObject> skillTreeA = new List<GameObject>();
    private void Start()
    {
        int childCount = this.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            skillTreeA.Add(transform.GetChild(i).GameObject());
        }
    }
}
