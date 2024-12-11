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

    void OnSkillButtonClicked(Button clickedButton)  //버튼선택
    {
        
        if (selectedBtn != null)
        {
            selectedBtn.GetComponent<Image>().color = Color.white;
        }

        clickedButton.GetComponent<Image>().color = Color.green;
        selectedBtn = clickedButton;

        string btnName = clickedButton.name;

        string skillInfo = clickedButton.GetComponentInChildren<TextMeshProUGUI>().text;
        
        skillInfoText.text = skillInfo;
        skillInfoPanel.SetActive(true);
    }

    void UpgradeCheck(string skillName)
    {
        if (true)
        {
            upgradeBtn.SetActive(true);
        }
        else
        {
            upgradeBtn.SetActive(false);
        }
    }

    public void UpgradeBtn()
    {
        //todo.소유한 자본에 따라서 가능하게끔 변경할것 if
        //SkillDataManagaer.Skills[SkillDataManagaer.currentSkill] = true;
        upgradeBtn.SetActive(false);
    }
}
