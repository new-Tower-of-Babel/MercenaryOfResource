using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseUI : MonoBehaviour
{
    public void PlayContinue()
    {
        transform.root.gameObject.SetActive(false);
        GameManager.instance.TimeToggle();
    }
}
