using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponChoice : MonoBehaviour
{
    public Button[] weaponBtn;
    public TextMeshProUGUI weaponInfoText;
    public Button unLockBtn;
    public Button nextBtn;

    private Button selectedBtn;
    private int currentNeedCoin;
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        nextBtn.gameObject.SetActive(false);
        unLockBtn.gameObject.SetActive(false);
        foreach (Button btn in weaponBtn)
        {
            btn.onClick.AddListener(() => OnWeaponButtonClicked(btn));
        }
    }

    void OnWeaponButtonClicked(Button clickedButton)
    {
        if (selectedBtn != null)
        {
            selectedBtn.GetComponent<Image>().color = Color.white;
        }
        if (UpgradeSceneData.instance.WeaponOpenCheck[clickedButton.name])
        {
            clickedButton.GetComponent<Image>().color = Color.green;
            unLockBtn.gameObject.SetActive(false);
            nextBtn.gameObject.SetActive(true);
            SelectList.instance.selectedWeapon = clickedButton.name;
        }
        else
        {
            clickedButton.GetComponent<Image>().color = Color.red;
            unLockBtn.gameObject.SetActive(true);
            nextBtn.gameObject.SetActive(false);
        }
        selectedBtn = clickedButton;
        string characterInfo = selectedBtn.GetComponentInChildren<TextMeshProUGUI>().text;
        weaponInfoText.text = characterInfo;
    }
    public void UnLockBtn()
    {
        var weaponSO  = SelectList.instance.WeaponDataDic[selectedBtn.name];
        if (Coin.instance.coin >= weaponSO.needCoin)
        {
            UpgradeSceneData.instance.WeaponOpenCheck[selectedBtn.name] = true;
            selectedBtn.GetComponent<Image>().color = Color.green;
            unLockBtn.gameObject.SetActive(false);
            nextBtn.gameObject.SetActive(true);
            SelectList.instance.selectedWeapon = selectedBtn.name;
            Coin.instance.coin -= weaponSO.needCoin;
        }
    }
}
