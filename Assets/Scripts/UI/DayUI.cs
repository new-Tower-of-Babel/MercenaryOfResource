using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DayUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dayNightText;
    [SerializeField] private Image dayNightImage;

    [SerializeField] private Sprite day;
    [SerializeField] private Sprite night;

    [SerializeField] private TextMeshProUGUI time;
    [SerializeField] private Image timeCircle;

    [SerializeField] private TextMeshProUGUI curRound;

    private void Update()
    {
        dayNightImage.sprite = DayCycle.instance.isNight ? night : day;
        curRound.text = $"{DayCycle.instance.round.ToString()} : 10";

        // Time calculate
        if (DayCycle.instance.isNight)
        {
            time.text = "00 : 00";
        }
    }
}
