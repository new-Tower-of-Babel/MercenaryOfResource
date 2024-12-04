using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public Resource currentResource;

    private void Update()
    {
        // �ڿ��� ��ȣ�ۿ� �������� üũ
        if (currentResource != null && Input.GetKeyDown(KeyCode.E))
        {
            // E Ű�� ������ �� �ڿ��� ü���� ���
            currentResource.TakeDamage(10);  // �ڿ��� 10��ŭ ���ظ� ��
        }
    }

    // �ڿ��� �������� �� ��ȣ�ۿ��� �ڿ��� ����
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Tree")  // �ڿ� ��ü���� ����
        {
            currentResource = other.GetComponent<Resource>();  // �ڿ� ��ũ��Ʈ ����
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Tree")  // �ڿ� ��ü���� �����
        {
            currentResource = null;  // ��ȣ�ۿ��� �ڿ��� null�� ����
        }
    }
}
