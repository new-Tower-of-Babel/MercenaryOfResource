using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycle : MonoBehaviour
{
    public Light directionalLight;
    public float dayDuration = 24f;
    private float currentTime = 0f;

    // ���� �� ���� �� ��� ������
    public Color dayColor = new Color(1f, 1f, 1f);
    public Color nightColor = new Color(0.1f, 0.1f, 0.5f);

    public float dayIntensity = 1f;     // ���� ���
    public float nightIntensity = 0.1f; // ���� ���

    private bool isNight = false;       // ������ ������ Ȯ���ϴ� �÷���

    void Update()
    {
        // ���� �� üũ
        if (!isNight)
        {
            // ���� ���
            currentTime += Time.deltaTime;
            if (currentTime >= dayDuration)
            {
                // ������ ��ȯ
                isNight = true;
                directionalLight.color = nightColor;
                directionalLight.intensity = nightIntensity;
            }
        }
        else
        {
            // ���� ��� (���Ͱ� ��� �׾����� ������ ��ȯ)
            if (MonsterSpawner.Instance.MonsterCount == 0)
            {
                // ������ ��ȯ
                currentTime = 0f;
                isNight = false;
                directionalLight.color = dayColor;
                directionalLight.intensity = dayIntensity;
            }
        }
    }

    void ChangeLight()
    {
        // ���� ���� ���� �� ��� ����
        float lerpValue = Mathf.Sin(currentTime * Mathf.PI);    // Sin �Լ��� ����Ͽ� �ε巴�� ���ϰ�
        directionalLight.color = Color.Lerp(nightColor, dayColor, lerpValue);   // ���� ����
        directionalLight.intensity = Mathf.Lerp(nightIntensity, dayIntensity, lerpValue);   // ��� ����
    }
}
