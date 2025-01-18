using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOption : UIBase
{
    [SerializeField] private Button btnScreen;
    [SerializeField] private Button btnSound;
    [SerializeField] private Button btnControl;
    [SerializeField] private Button btnLanguage;

    private void Start()
    {
        btnScreen.onClick.AddListener(() =>
        {
            UIManager.Instance.OpenUI<UIScreen>();
        });

        btnSound.onClick.AddListener(() =>
        {
            UIManager.Instance.OpenUI<UISound>();
        });

        btnControl.onClick.AddListener(() =>
        {
            UIManager.Instance.OpenUI<UIControl>();
        });

        btnLanguage.onClick.AddListener(() =>
        {
            UIManager.Instance.OpenUI<UILanguage>();
        });
    }
}
