using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUpgradeUIBtn : MonoBehaviour
{
    public void CharacterUIPrevBtn()
    {
        UpgradeSceneManager.instance.gradeUpgradeUI.SetActive(true);
        UpgradeSceneManager.instance.characterUpgradeUI.SetActive(false);
    }

    public void CharacterUINextBtn()
    {
        UpgradeSceneManager.instance.characterUpgradeUI.SetActive(false);
        UpgradeSceneManager.instance.weaponUpgradeUI.SetActive(true);
    }

}
