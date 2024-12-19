using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycle : MonoBehaviour
{
    public Light directionalLight;
    public float dayDuration = 24f;
    private float currentTime = 0f;

    // 낮과 밤 색상 및 밝기 조정용
    public Color dayColor = new Color(1f, 1f, 1f);
    public Color nightColor = new Color(0.1f, 0.1f, 0.5f);

    public float dayIntensity = 1f;     // 낮의 밝기
    public float nightIntensity = 0.1f; // 밤의 밝기

    private bool isNight = false;       // 밤인지 낮인지 확인하는 플래그

    void Update()
    {
        // 낮과 밤 체크
        if (!isNight)
        {
            // 낮의 경우
            currentTime += Time.deltaTime;
            if (currentTime >= dayDuration)
            {
                // 밤으로 전환
                isNight = true;
                directionalLight.color = nightColor;
                directionalLight.intensity = nightIntensity;
            }
        }
        else
        {
            // 밤의 경우 (몬스터가 모두 죽었으면 낮으로 전환)
            if (MonsterSpawner.Instance.MonsterCount == 0)
            {
                // 낮으로 전환
                currentTime = 0f;
                isNight = false;
                directionalLight.color = dayColor;
                directionalLight.intensity = dayIntensity;
            }
        }
    }

    void ChangeLight()
    {
        // 낮과 밤의 색상 및 밝기 변경
        float lerpValue = Mathf.Sin(currentTime * Mathf.PI);    // Sin 함수를 사용하여 부드럽게 변하게
        directionalLight.color = Color.Lerp(nightColor, dayColor, lerpValue);   // 색상 변경
        directionalLight.intensity = Mathf.Lerp(nightIntensity, dayIntensity, lerpValue);   // 밝기 변경
    }
}
