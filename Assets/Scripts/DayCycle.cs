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

    // ���� Ž��
    public List<GameObject> monsters;   // ������ ���͸� ��� ����Ʈ
    private bool isNight = false;       // ������ ������ Ȯ���ϴ� �÷���

    void Update()
    {
        // ���� �� üũ
        if (!isNight)
        {
            // ���� ���
            currentTime += Time.deltaTime / dayDuration;
            if (currentTime >= 1f)
            {
                currentTime = 0f;   // �Ϸ簡 ������ 0���� ����
                isNight = true;     // ������ ��ȯ
            }
        }
        else
        {
            // ���� ��� (���Ͱ� ��� �׾����� ������ ��ȯ)
            if (AllMonstersDead())
            {
                currentTime = 0f;   // ������ ����
                isNight = false;    // ������ ��ȯ
            }
        }

        // ���� ���� ���� �� ��� ����
        float lerpValue = Mathf.Sin(currentTime * Mathf.PI);    // Sin �Լ��� ����Ͽ� �ε巴�� ���ϰ�
        directionalLight.color = Color.Lerp(nightColor, dayColor, lerpValue);   // ���� ����
        directionalLight.intensity = Mathf.Lerp(nightIntensity, dayIntensity, lerpValue);   // ��� ����
    }

    // ���Ͱ� ��� �׾����� Ȯ��
    bool AllMonstersDead()
    {
        foreach (GameObject monster in monsters)
            if (monster != null && monster.activeInHierarchy)
                return false;
        return true;
    }
}
