using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillBtnManager : MonoBehaviour
{
    public Button[] SkillBtn;
    public GameObject skillInfoPanel;
    public TextMeshProUGUI skillInfoText;

    private Button selectedBtn;

    void Start()
    {
        foreach (Button btn in SkillBtn)
        {
            btn.onClick.AddListener(() => OnSkillButtonClicked(btn));
        }

    }

    void OnSkillButtonClicked(Button clickedButton)
    {
        if (selectedBtn != null)
        {
            selectedBtn.GetComponent<Image>().color = Color.white;
        }

        clickedButton.GetComponent<Image>().color = Color.green;
        selectedBtn = clickedButton;

        string skillInfo = clickedButton.GetComponentInChildren<TextMeshProUGUI>().text; // 예시
        SkillDataManagaer.currentSkill = clickedButton.ToString();
        Debug.Log(SkillDataManagaer.currentSkill);
        skillInfoText.text = skillInfo;
        skillInfoPanel.SetActive(true);
    }
}
