using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceDisply : MonoBehaviour
{
    //[SerializeField] private PlayData playData;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI skullText;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI stoneText;
    [SerializeField] private TextMeshProUGUI woodText;
    [SerializeField] private TextMeshProUGUI healthText;

    private void Update()
    {
        skullText.text = PlayDataManager.Instance.resourcePlayData.skull.ToString();
        goldText.text = PlayDataManager.Instance.resourcePlayData.gold.ToString();
        stoneText.text = PlayDataManager.Instance.resourcePlayData.stone.ToString();
        woodText.text = PlayDataManager.Instance.resourcePlayData.wood.ToString();
        healthText.text = " x " + PlayDataManager.Instance.characterPlayData.nowHealth.ToString();

    }
}
