using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GradeUpgradeUIBtn : MonoBehaviour
{    public void GreadeUINextBtn()
    {
        UpgradeSceneManager.instance.gradeUpgradeUI.SetActive(false);
        UpgradeSceneManager.instance.characterUpgradeUI.SetActive(true);
    }
}
