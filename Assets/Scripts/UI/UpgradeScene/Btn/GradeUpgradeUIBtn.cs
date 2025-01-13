using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GradeUpgradeUIBtn : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinTxt;

    private void Update()
    {
        coinTxt.text = "Coin : " + Coin.instance.coin;
    }
    public void GreadeUINextBtn()
    {
        UpgradeSceneManager.instance.gradeUpgradeUI.SetActive(false);
        UpgradeSceneManager.instance.characterUpgradeUI.SetActive(true);
    }
}
