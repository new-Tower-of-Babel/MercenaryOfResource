using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUpgradeUIBtn : MonoBehaviour
{
    public void WeaponUIPrevBtn()
    {
        UpgradeSceneManager.instance.characterUpgradeUI.SetActive(true);
        UpgradeSceneManager.instance.weaponUpgradeUI.SetActive(false);
    }

    public void WeaponUINextBtn()
    {
        ChangeSceneButton.LoadMainScene();
    }
}
