using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleGenerator : MonoBehaviour
{
    private GameObject targetObject;     // ���� ������Ʈ
    private float detectionRadius = 5f;  // ���� ����

    private LineRenderer lineRenderer;
    private float circleRadius = 1.5f;     // ���� ������
    private int segments = 60;        // ���� ������ ���׸�Ʈ�� ��

    private void Start()
    {
        // targetObject �÷��̾� ����

        lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.positionCount = segments;

        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.white;

        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
    }

    private void Update()
    {
        //float distance = Vector3.Distance(transform.position, targetObject.transform.position);

        //if (distance <= detectionRadius)
        //{
        //    DrawCircle();
        //}
        //else
        //{
        //    lineRenderer.positionCount = 0;
        //}

        DrawCircle();
    }

    private void DrawCircle()
    {
        float angleStep = 360f / segments;
        for (int i = 0; i < segments; i++)
        {
            float angle = Mathf.Deg2Rad * i * angleStep;
            Vector3 position = new Vector3(Mathf.Cos(angle) * circleRadius, 0, Mathf.Sin(angle) * circleRadius);
            lineRenderer.SetPosition(i, transform.position + position);
        }
    }
}
