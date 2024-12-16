using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public Resource currentResource;

    private void Update()
    {
        // 자원과 상호작용 가능한지 체크
        if (currentResource != null && Input.GetKeyDown(KeyCode.E))
        {
            // E 키를 눌렀을 때 자원의 체력을 깎기
            currentResource.TakeDamage(10);  // 자원에 10만큼 피해를 줌
        }
    }

    // 자원과 근접했을 때 상호작용할 자원을 설정
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Resource>(out var resource))  // 자원 객체에만 반응
        {
            currentResource = other.GetComponent<Resource>();  // 자원 스크립트 참조
        }
    }

    private void OnTriggerExit(Collider other)
    {
        currentResource = null;  // 상호작용할 자원을 null로 설정
    }
}
