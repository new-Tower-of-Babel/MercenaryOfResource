using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleGenerator : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private float radius = 1.5f;     // 원의 반지름
    private int segments = 60;        // 원을 구성할 세그먼트의 수

    private void Awake()
    {
        if (!gameObject.GetComponent<LineRenderer>())
        {
            gameObject.AddComponent<LineRenderer>();
        }
    }

    private void Start()
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.positionCount = segments + 1;

        lineRenderer.useWorldSpace = false;     // 로컬 공간에서 그리기
        lineRenderer.loop = true;               // 원을 닫음

        // 원의 색 지정
        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.white;

        // 원의 선 너비 지정
        lineRenderer.startWidth = 0.3f;
        lineRenderer.endWidth = 0.3f;

        DrawCircle();
    }

    private void DrawCircle()
    {
        float angleStep = 360f / segments;
        for (int i = 0; i <= segments; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;
            lineRenderer.SetPosition(i, new Vector3(x, 0f, y));
        }
    }
}
