using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ResultScalculation : MonoBehaviour
{
    public TextMeshProUGUI resultDisplay;
    private Coin resourceResult;

    private void Start()
    {
        resourceResult = Coin.instance;
        ResultDisplay();
    }


    private void ResultDisplay()
    {
        int IncomCoin = resourceResult.resultWood+resourceResult.resultStone+
                        resourceResult.resultSkull+(resourceResult.resultGold*2);

        resultDisplay.text =
            $"Wood " + resourceResult.resultWood + " -> " + "Coin " + resourceResult.resultWood + "\n" +
            "Stone " + resourceResult.resultStone + " -> " + "Coin " + resourceResult.resultStone + "\n" +
            "Skull " + resourceResult.resultSkull + " -> " + "Coin " + resourceResult.resultSkull + "\n" +
            "Gold " + resourceResult.resultGold + " -> " + "Coin " + resourceResult.resultGold * 2 + "\n" +
            "Total Income Coin = " + IncomCoin;
        resourceResult.coin += IncomCoin;
    }
}
