using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleGenerator : MonoBehaviour
{
    private GameObject targetObject;     // 근접 오브젝트
    private float detectionRadius = 5f;  // 감지 범위

    private LineRenderer lineRenderer;
    private float circleRadius = 1.5f;     // 원의 반지름
    private int segments = 60;        // 원을 구성할 세그먼트의 수

    private void Start()
    {
        // targetObject 플레이어 설정

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
