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

            if (rf.type == Resource.ResourceType.Wood)
                PlayDataManager.Instance.resourcePlayData.wood++;
            else if (rf.type == Resource.ResourceType.Gold)
                PlayDataManager.Instance.resourcePlayData.gold++;
            else if (rf.type == Resource.ResourceType.Stone)
                PlayDataManager.Instance.resourcePlayData.stone++;
            Destroy(other.gameObject); // ���� ����
            PlayDataManager.Instance.characterPlayData.resourceCarryLimit++;
        }
    }
}
