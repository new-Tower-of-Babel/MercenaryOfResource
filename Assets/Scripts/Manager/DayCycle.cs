using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class DayCycle : MonoBehaviour
{
    public static DayCycle instance;

    [SerializeField] private Light directionalLight;
    [SerializeField] private float dayDuration = 24f;
    private float currentTime = 0f;

    // ���� �� ���� �� ��� ������
    private Color dayColor = new Color(1f, 1f, 1f);
    private Color nightColor = new Color(0.1f, 0.1f, 0.5f);

    private float dayIntensity = 1f;     // ���� ���
    private float nightIntensity = 0.1f; // ���� ���

    private bool isNight = false;           // ������ ������ Ȯ��
    private bool monsterSpawned = false;    // ���� ���� ���� Ȯ��

    [SerializeField] private int round = 10;

    void OnEnable()
    {
        round = 0;
        currentTime = 0f;

        // ���Ͱ� ��� �׾��� �� ������ ��ȯ�ϴ� �̺�Ʈ ����
        MonsterSpawner.Instance.OnAllMonstersDied += ChangeToDay;
    }

    void OnDisable()
    {
        // �̺�Ʈ ���� ����
        MonsterSpawner.Instance.OnAllMonstersDied -= ChangeToDay;
    }

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Update()
    {
        if (!isNight)
        {
            // ���� ��� �ð��� ����� ��ŭ currentTime�� ������Ŵ
            currentTime += Time.deltaTime;
            if (currentTime >= dayDuration)
            {
                isNight = true;
                StartCoroutine(ChangeLightGradually(dayColor, nightColor, dayIntensity, nightIntensity));
            }
        }
    }

    private void ChangeToDay()
    {
        // �ڷ�ƾ���� �㳷 ��ȭ ����
        StartCoroutine(ChangeLightGradually(nightColor, dayColor, nightIntensity, dayIntensity));

        isNight = false;
        currentTime = 0f;
        monsterSpawned = false;
        
        if(round++ > 10)
        {
            StartCoroutine(WaitGameFinish());
        }
    }

    private IEnumerator WaitGameFinish()
    {
        yield return new WaitForSeconds(3f);
        GameManager.instance.isClear = true;
    }

    // ����� ��⸦ �ε巴�� ��ȯ�ϴ� �ڷ�ƾ
    private IEnumerator ChangeLightGradually(Color initialColor, Color targetColor, float initialIntensity, float targetIntensity)
    {
        float timeElapsed = 0f;
        float duration = 1f; // ���� �� ��� ��ȯ�� �ɸ��� �ð�

        // ���� ����� ��⿡�� ��ǥ ����� ���� ������ ����
        while (timeElapsed < duration)
        {
            directionalLight.color = Color.Lerp(initialColor, targetColor, timeElapsed / duration);
            directionalLight.intensity = Mathf.Lerp(initialIntensity, targetIntensity, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null; // �� ������ ���
        }

        // ���� ��ǥ ����� ���� ����
        directionalLight.color = targetColor;
        directionalLight.intensity = targetIntensity;

        // ������ ��ȯ�� �Ϸ�Ǿ��� �� ���͸� ����
        if (isNight && !monsterSpawned)
        {
            // ���� 5���� ����
            for (int i = 0; i < 6 + 1 * round; i++)  // ���� 5���� ����
            {
                MonsterSpawner.Instance.SpawnMonster();
            }
            monsterSpawned = true; // ���Ͱ� �����Ǿ����� ���
        }
    }
}
