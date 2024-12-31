using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCamp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ResourceFragment>(out var rf))
        {
            // ÀÚ¿ø È¹µæ
            //other.gameObject.SetActive(false);

            Debug.Log("resource collected!");
            Destroy(other.gameObject); // ÆÄÆí Á¦°Å
        }
    }
}
