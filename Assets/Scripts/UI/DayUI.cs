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

    private float timeSep = 0.0f;

    private void Update()
    {
        timeSep = DayCycle.instance.dayDuration / 90;
        dayNightImage.sprite = DayCycle.instance.isNight ? night : day;
        curRound.text = $"{DayCycle.instance.round} / 10";

        // Time calculate
        if (DayCycle.instance.isNight)
        {
            time.text = "00 : 00";
        }
        else
        {
            int minute = (int)(DayCycle.instance.currentTime / timeSep);
            int totalMinutes = minute * 10;

            int hours = 6 + (totalMinutes / 60);
            int minutes = totalMinutes % 60;

            time.text = $"{hours:D2} : {minutes:D2}";
        }

        RotateTimeCircle();
    }

    private void RotateTimeCircle()
    {
        float rotationAngle = (DayCycle.instance.currentTime / DayCycle.instance.dayDuration) * 240f;
        timeCircle.transform.rotation = Quaternion.Euler(0f, 0f, -rotationAngle);
    }
}
