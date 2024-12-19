using UnityEngine;

public class ResourceFragment : MonoBehaviour
{
    public Resource.ResourceType type; // 파편의 자원 타입
    private bool isBeingPulled = false; // 파편이 끌리는 상태 확인
    private Transform player; // 플레이어 참조

    [Header("Rope Settings")]
    public float maxRopeLength = 3f; // 밧줄의 최대 길이
    public float pullSpeed = 10f; // 파편이 플레이어 쪽으로 끌리는 속도
    public float pullForce = 10f; // 파편을 끌어오는 물리적 힘

    private Rigidbody _rigidbody; // 파편의 Rigidbody
    private LineRenderer _lineRenderer; // LineRenderer 컴포넌트 (밧줄 시각화용)

    void Awake()
    {
        // 초기화
        _rigidbody = GetComponent<Rigidbody>();
        _lineRenderer = gameObject.AddComponent<LineRenderer>();
    }

    void Start()
    {
        //_lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

        _lineRenderer.positionCount = 2; // 두 점으로 연결

        // 밧줄 폭
        _lineRenderer.startWidth = 0.1f;
        _lineRenderer.endWidth = 0.1f;

        // 밧줄 색상
        _lineRenderer.startColor = new Color(63f / 255f, 31f / 255f, 15f / 255f);
        _lineRenderer.endColor = new Color(639f / 31f, 69f / 255f, 15f / 255f);

        _lineRenderer.enabled = false;
    }

    void Update()
    {
        if (isBeingPulled && player != null)
        {
            // 플레이어와 파편 간의 거리 계산
            float distance = Vector3.Distance(transform.position, player.position);

            // 밧줄을 플레이어와 파편 위치로 시각화
            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, player.position + Vector3.up);

            // 파편이 플레이어와 너무 멀어지면 당겨오기
            if (distance > maxRopeLength)
            {
                Vector3 direction = (player.position - transform.position).normalized;
                // 물리적인 힘을 가하여 파편을 플레이어 방향으로 끌어오기
                _rigidbody.AddForce(direction * pullForce, ForceMode.Force);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isBeingPulled)
        {
            // 플레이어와 충돌 시 연결
            player = collision.transform;
            isBeingPulled = true;
            _lineRenderer.enabled = true; // 충돌 후 밧줄 시각화 활성화
        }
    }
}
