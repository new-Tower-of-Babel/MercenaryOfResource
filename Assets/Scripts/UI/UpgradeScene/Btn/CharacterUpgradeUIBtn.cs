using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterUpgradeUIBtn : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinTxt;

    private void Update()
    {
        coinTxt.text = "Coin : " + Coin.instance.coin;
    }

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
