using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SkillBtnManager : MonoBehaviour
{
    public Button[] SkillBtn;
    public TextMeshProUGUI skillInfoText;
    

    
    private Button selectedBtn;
    private int currentSkillKey;
    public Dictionary<int,bool> currentSkillAntecedentSkills;

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
        string skillInfo = clickedButton.GetComponentInChildren<TextMeshProUGUI>().text;
        skillInfoText.text = skillInfo;

        currentSkillKey = GetSkillKey<ISkill>(clickedButton.GameObject());
        currentSkillAntecedentSkills = getSkillAntecedentSkills<ISkill>(clickedButton.GameObject());
        UpgradeCheck(currentSkillKey);
    }

    public int GetSkillKey<T>(GameObject target) where T : ISkill
    {
        T skillComponent = target.GetComponent<T>();
        return skillComponent.SkillKey;
    }
    public Dictionary<int,bool> getSkillAntecedentSkills<T>(GameObject target) where T : ISkill
    {
        T skillComponet = target.GetComponent<T>();
        return skillComponet.AntecedentSkills;
    }
    

    void UpgradeCheck(int skillKey)
    {
        bool skillCheck = false;
        skillCheck = SkillDataManagaer.haveSkillCheck(skillKey);
        skillCheck = SkillDataManagaer.AntecedentSkillsCheck(skillCheck,currentSkillAntecedentSkills);
        if (!skillCheck)
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
        SkillDataManagaer.haveSkillKey.Add(currentSkillKey);
        upgradeBtn.SetActive(false);
    }
}
