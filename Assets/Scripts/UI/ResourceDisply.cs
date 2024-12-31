using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceDisply : MonoBehaviour
{
    [SerializeField] private PlayData playData;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI skullText;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI stoneText;
    [SerializeField] private TextMeshProUGUI woodText;

    private void Update()
    {
        skullText.text = playData.skull.ToString();
        goldText.text = playData.gold.ToString();
        stoneText.text = playData.stone.ToString();
        woodText.text = playData.wood.ToString();
    }
}
