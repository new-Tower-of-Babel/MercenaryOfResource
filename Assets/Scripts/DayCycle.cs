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

    // 몬스터 탐색
    public List<GameObject> monsters;   // 생존한 몬스터를 담는 리스트
    private bool isNight = false;       // 밤인지 낮인지 확인하는 플래그

    void Update()
    {
        // 낮과 밤 체크
        if (!isNight)
        {
            // 낮의 경우
            currentTime += Time.deltaTime / dayDuration;
            if (currentTime >= 1f)
            {
                currentTime = 0f;   // 하루가 끝나면 0으로 리셋
                isNight = true;     // 밤으로 전환
            }
        }
        else
        {
            // 밤의 경우 (몬스터가 모두 죽었으면 낮으로 전환)
            if (AllMonstersDead())
            {
                currentTime = 0f;   // 낮으로 리셋
                isNight = false;    // 낮으로 전환
            }
        }

        // 낮과 밤의 색상 및 밝기 변경
        float lerpValue = Mathf.Sin(currentTime * Mathf.PI);    // Sin 함수를 사용하여 부드럽게 변하게
        directionalLight.color = Color.Lerp(nightColor, dayColor, lerpValue);   // 색상 변경
        directionalLight.intensity = Mathf.Lerp(nightIntensity, dayIntensity, lerpValue);   // 밝기 변경
    }

    // 몬스터가 모두 죽었는지 확인
    bool AllMonstersDead()
    {
        foreach (GameObject monster in monsters)
            if (monster != null && monster.activeInHierarchy)
                return false;
        return true;
    }
}
