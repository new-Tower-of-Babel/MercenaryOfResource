using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class DayCycle : MonoBehaviour
{
    public static DayCycle instance;

    [SerializeField] private Light directionalLight;
    [SerializeField] private float dayDuration = 24f;
    private float currentTime = 0f;

    // 낮과 밤 색상 및 밝기 조정용
    private Color dayColor = new Color(1f, 1f, 1f);
    private Color nightColor = new Color(0.1f, 0.1f, 0.5f);

    private float dayIntensity = 1f;     // 낮의 밝기
    private float nightIntensity = 0.1f; // 밤의 밝기

    private bool isNight = false;           // 밤인지 낮인지 확인
    private bool monsterSpawned = false;    // 몬스터 스폰 여부 확인

    [SerializeField] private int round = 10;

    void OnEnable()
    {
        round = 0;
        currentTime = 0f;

        // 몬스터가 모두 죽었을 때 낮으로 전환하는 이벤트 구독
        MonsterSpawner.Instance.OnAllMonstersDied += ChangeToDay;
    }

    void OnDisable()
    {
        // 이벤트 구독 해제
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
            // 낮의 경우 시간이 경과한 만큼 currentTime을 증가시킴
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
        // 코루틴으로 밤낮 변화 시작
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

    // 색상과 밝기를 부드럽게 변환하는 코루틴
    private IEnumerator ChangeLightGradually(Color initialColor, Color targetColor, float initialIntensity, float targetIntensity)
    {
        float timeElapsed = 0f;
        float duration = 1f; // 색상 및 밝기 변환에 걸리는 시간

        // 현재 색상과 밝기에서 목표 색상과 밝기로 서서히 변경
        while (timeElapsed < duration)
        {
            directionalLight.color = Color.Lerp(initialColor, targetColor, timeElapsed / duration);
            directionalLight.intensity = Mathf.Lerp(initialIntensity, targetIntensity, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null; // 매 프레임 대기
        }

        // 최종 목표 색상과 밝기로 설정
        directionalLight.color = targetColor;
        directionalLight.intensity = targetIntensity;

        // 밤으로 변환이 완료되었을 때 몬스터를 생성
        if (isNight && !monsterSpawned)
        {
            // 몬스터 5마리 생성
            for (int i = 0; i < 6 + 1 * round; i++)  // 몬스터 5마리 생성
            {
                MonsterSpawner.Instance.SpawnMonster();
            }
            monsterSpawned = true; // 몬스터가 스폰되었음을 기록
        }
    }
}
