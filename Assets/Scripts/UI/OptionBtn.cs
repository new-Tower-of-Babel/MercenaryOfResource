using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionBtn : MonoBehaviour
{
    public void ClickOptionBtn()
    {
        UIManager.Instance.OpenUI<UIOption>();
    }
}
