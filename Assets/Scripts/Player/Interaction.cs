using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    [ReadOnly] public Resource currentResource;
    [SerializeField] private Animator m_Animator;

    private void Update()
    {
        CharacterPlayData damageData = PlayDataManager.Instance.characterPlayData;

        // �ڿ��� ��ȣ�ۿ� �������� üũ
        if (currentResource != null && Input.GetKeyDown(KeyCode.E))
        {
            // E Ű�� ������ �� �ڿ��� ü���� ���?
            if (currentResource.type == Resource.ResourceType.Stone)
            {
                m_Animator.SetTrigger ("Pickaxing");
                currentResource.TakeDamage(damageData.pickaxeDamage);
            }
            else if (currentResource.type == Resource.ResourceType.Wood)
            {
                m_Animator.SetTrigger ("Axing");
                currentResource.TakeDamage (damageData.axeDamage);
            }
            
        }
    }

    // �ڿ��� �������� �� ��ȣ�ۿ��� �ڿ��� ����
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Resource>(out var resource))  // �ڿ� ��ü���� ����
        {
            currentResource = other.GetComponent<Resource>();  // �ڿ� ��ũ��Ʈ ����
        }
    }

    private void OnTriggerExit(Collider other)
    {
        currentResource = null;  // ��ȣ�ۿ��� �ڿ��� null�� ����
    }
}
