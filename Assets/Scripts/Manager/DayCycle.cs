using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class DayCycle : MonoBehaviour
{
    public static DayCycle instance;
    public string[] normalZombieList = { "Actisdato", "Kurniawan", "Pedroso" };
    public string[] specialZombieList = { "Parasite_Starkie", "Whiteclown_Hallin" };

    [SerializeField] private Light directionalLight;
    public float dayDuration = 24f;
    public float currentTime = 0f;

    // ���� �� ���� �� ���?������
    private Color dayColor = new Color(1f, 1f, 1f);
    private Color nightColor = new Color(0.1f, 0.1f, 0.5f);

    private float dayIntensity = 1f;     // ���� ���?
    private float nightIntensity = 0.1f; // ���� ���?

    [SerializeField] private AudioClip _bgm;
    [SerializeField] private AudioClip day;
    [SerializeField] private AudioClip night;
    [SerializeField] private AudioClip dayClip;
    [SerializeField] private AudioClip nightClip;

    public bool isNight = false;           // ������ ������ Ȯ��
    private bool zombieSpawned = false;    // ���� ���� ���� Ȯ��

    public int round = 10;

    void init()
    {
        round = 1;
        currentTime = 0f;

        // ���Ͱ� ���?�׾��� �� ������ ��ȯ�ϴ� �̺�Ʈ ����
        ZombieManager.Instance.OnAllZombieDied += ChangeToDay;
    }

    void OnDisable()
    {

        // �̺�Ʈ ���� ����
        ZombieManager.Instance.OnAllZombieDied -= ChangeToDay;
    }

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        AudioManager.Instance.PlayBGM(day);
        init();
    }

    void Update()
    {
        if (!isNight)
        {
            // ���� ���?�ð��� �����?��ŭ currentTime�� ������Ŵ
            currentTime += Time.deltaTime;
            if (currentTime >= dayDuration)
            {
                isNight = true;
                AudioManager.Instance.PlayBGM(night);
                AudioManager.Instance.PlayClipOnce(nightClip);
                StartCoroutine(ChangeLightGradually(dayColor, nightColor, dayIntensity, nightIntensity));
            }
        }
    }

    private void ChangeToDay()
    {
        isNight = false;
        AudioManager.Instance.PlayBGM(day);
        AudioManager.Instance.PlayClipOnce(dayClip);
        // �ڷ�ƾ���� �㳷 ��ȭ ����
        StartCoroutine(ChangeLightGradually(nightColor, dayColor, nightIntensity, dayIntensity));

        currentTime = 0f;
        zombieSpawned = false;
        round++;
        if(round > 10)
        {
            StartCoroutine(WaitGameFinish());
        }
    }

    private IEnumerator WaitGameFinish()
    {
        yield return new WaitForSeconds(3f);
        GameManager.instance.isClear = true;
    }

    // �����?���?�ε巴�� ��ȯ�ϴ� �ڷ�ƾ
    private IEnumerator ChangeLightGradually(Color initialColor, Color targetColor, float initialIntensity, float targetIntensity)
    {
        float timeElapsed = 0f;
        float duration = 1f; // ���� �� ���?��ȯ�� �ɸ��� �ð�

        // ���� �����?��⿡��?��ǥ �����?���� ������ ����
        while (timeElapsed < duration)
        {
            directionalLight.color = Color.Lerp(initialColor, targetColor, timeElapsed / duration);
            directionalLight.intensity = Mathf.Lerp(initialIntensity, targetIntensity, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null; // �� ������ ���?
        }

        // ���� ��ǥ �����?���� ����
        directionalLight.color = targetColor;
        directionalLight.intensity = targetIntensity;

        // ������ ��ȯ�� �Ϸ�Ǿ���?�� ���͸� ����
        if (isNight && !zombieSpawned)
        {
            // ���� 5���� ����
            for (int i = 0; i < 6 + 1 * round; i++)  // ���� 5���� ����
            {
                ZombieManager.Instance.SpawnZombie(normalZombieList[Random.Range(0, 3)]);

                // special zombie spawn
                if(Random.Range(0, 130) < 45)
                {
                    ZombieManager.Instance.SpawnZombie(specialZombieList[0]);
                }

                if (Random.Range(0, 150) < 20)
                {
                    ZombieManager.Instance.SpawnZombie(specialZombieList[1]);
                }
            }
            zombieSpawned = true; // ���Ͱ� �����Ǿ����� ���?
        }
    }
}
