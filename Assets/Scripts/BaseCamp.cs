using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCamp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ResourceFragment>(out var rf))
        {
            // �ڿ� ȹ��
            //other.gameObject.SetActive(false);

            Debug.Log(rf.type);
            Destroy(other.gameObject); // ���� ����
        }
    }
}
