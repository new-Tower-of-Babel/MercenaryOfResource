using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    [ReadOnly] public Resource currentResource;
    [SerializeField] private Animator m_Animator;

    private bool isInteracting = false;

    private void Update()
    {
        CharacterPlayData damageData = PlayDataManager.Instance.characterPlayData;

        bool canInteract = !DayCycle.instance.isNight;
        if (canInteract && currentResource != null && Input.GetKeyDown(KeyCode.E) && !isInteracting)
        {
            StartInteraction();
        }

        if (isInteracting)
        {
            if (currentResource != null && currentResource.curHealth <= 0)
            {
                EndInteraction();
            }
        }

        // �ڿ��� ��ȣ�ۿ� �������� üũ
        //if (currentResource != null && Input.GetKeyDown(KeyCode.E))
        //{
        //    // E Ű�� ������ �� �ڿ��� ü���� ���?
        //    if (currentResource.type == Resource.ResourceType.Stone)
        //    {
        //        m_Animator.SetTrigger ("Pickaxing");
        //        currentResource.TakeDamage(damageData.pickaxeDamage);
        //    }
        //    else if (currentResource.type == Resource.ResourceType.Wood)
        //    {
        //        m_Animator.SetTrigger ("Axing");
        //        currentResource.TakeDamage (damageData.axeDamage);
        //    }

        //}
    }

    private void StartInteraction()
    {
        isInteracting = true;

        if (currentResource.type == Resource.ResourceType.Stone)
        {
            m_Animator.SetBool("Pickaxing", true);
        }
        else if (currentResource.type == Resource.ResourceType.Wood)
        {
            m_Animator.SetBool("Axing", true);
        }

        StartCoroutine(ConsumeResource());
    }

    private void EndInteraction()
    {
        isInteracting = false;

        m_Animator.SetBool("Pickaxing", false);
        m_Animator.SetBool("Axing", false);

        currentResource = null;
    }

    private IEnumerator ConsumeResource()
    {
        while (isInteracting && currentResource != null && currentResource.curHealth > 0)
        {
            yield return new WaitForSeconds(m_Animator.GetCurrentAnimatorStateInfo(0).length);

            if (currentResource.type == Resource.ResourceType.Stone)
            {
                currentResource.TakeDamage(PlayDataManager.Instance.characterPlayData.pickaxeDamage);
            }
            else if (currentResource.type == Resource.ResourceType.Wood)
            {
                currentResource.TakeDamage(PlayDataManager.Instance.characterPlayData.axeDamage);
            }
        }

        EndInteraction();
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
        EndInteraction();
        currentResource = null;  // ��ȣ�ۿ��� �ڿ��� null�� ����
    }
}
