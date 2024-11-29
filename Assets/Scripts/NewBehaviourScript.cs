using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject targetObject;     // ���� ������Ʈ
    public float detectionRadius = 5f;  // ���� ����

    private LineRenderer lineRenderer;
    public float circleRadius = 1f;     // ���� ������
    public int segments = 60;        // ���� ������ ���׸�Ʈ�� ��

    private void Start()
    {
        // targetObject �÷��̾� ����

        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = segments;
        lineRenderer.useWorldSpace = false;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, targetObject.transform.position);

        if (distance <= detectionRadius)
        {
            DrawCircle();
        }
        else
        {
            lineRenderer.positionCount = 0;
        }
    }

    private void DrawCircle()
    {
        float angleStep = 360f / segments;
        for (int i = 0; i < segments; i++)
        {
            float angle = Mathf.Deg2Rad * i * angleStep;
            Vector3 position = new Vector3(Mathf.Cos(angle) * circleRadius, 0, Mathf.Sin(angle) * circleRadius);
            lineRenderer.SetPosition(i, targetObject.transform.position + position);
        }
    }
}
