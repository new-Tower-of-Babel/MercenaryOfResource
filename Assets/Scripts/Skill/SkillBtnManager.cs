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

    [SerializeField] GameObject upgradeBtn;

    void Start()
    {
        upgradeBtn.SetActive(false);
        foreach (Button btn in SkillBtn)
        {
            btn.onClick.AddListener(() => OnSkillButtonClicked(btn));
        }
    }
    private string ToLowerFirstChar(string input) //첫글자 소문자로 변환
    {
        if (string.IsNullOrEmpty(input))
            return input;

        return char.ToLower(input[0]) + input.Substring(1);
    }

    void OnSkillButtonClicked(Button clickedButton)  //버튼선택
    {
        
        if (selectedBtn != null)
        {
            selectedBtn.GetComponent<Image>().color = Color.white;
        }

        clickedButton.GetComponent<Image>().color = Color.green;
        selectedBtn = clickedButton;

        string btnName = clickedButton.name;
        string skillName = ToLowerFirstChar(btnName);
        UpgradeCheck(skillName);
        //SkillDataManagaer.currentSkill = skillName;

        string skillInfo = clickedButton.GetComponentInChildren<TextMeshProUGUI>().text;
        
        skillInfoText.text = skillInfo;
        skillInfoPanel.SetActive(true);
    }

    void UpgradeCheck(string skillName)
    {
        // if (!SkillDataManagaer.Skills[skillName])
        // {
        //     upgradeBtn.SetActive(true);
        // }
        // else
        // {
        //     upgradeBtn.SetActive(false);
        // }
    }

    public void UpgradeBtn()
    {
        //todo.소유한 자본에 따라서 가능하게끔 변경할것 if
        //SkillDataManagaer.Skills[SkillDataManagaer.currentSkill] = true;
        upgradeBtn.SetActive(false);
    }
}
