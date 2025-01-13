using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponUpgradeUIBtn : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinTxt;

    private void Update()
    {
        coinTxt.text = "Coin : " + Coin.instance.coin;
    }
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
