using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterChoice : MonoBehaviour
{
    public Button[] characterBtn;
    public TextMeshProUGUI characterInfoText;
    public Button unLockBtn;
    public Button nextBtn;

    private Button selectedBtn;
    private int currentNeedCoin;
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        unLockBtn.gameObject.SetActive(false);
        foreach (Button btn in characterBtn)
        {
            btn.onClick.AddListener(() => OnCharacterButtonClicked(btn));
        }
    }

    void OnCharacterButtonClicked(Button clickedButton)
    {
        if (selectedBtn != null)
        {
            selectedBtn.GetComponent<Image>().color = Color.white;
        }
        if (UpgradeSceneData.instance.CharacterOpenCheck[clickedButton.name])
        {
            //Debug.Log(clickedButton.name+"   "+UpgradeSceneData.instance.CharacterOpenCheck.ContainsKey(clickedButton.name));
            clickedButton.GetComponent<Image>().color = Color.green;
            unLockBtn.gameObject.SetActive(false);
            nextBtn.gameObject.SetActive(true);
        }
        else
        {
            clickedButton.GetComponent<Image>().color = Color.red;
            unLockBtn.gameObject.SetActive(true);
            nextBtn.gameObject.SetActive(false);
        }
        selectedBtn = clickedButton;
        string characterInfo = selectedBtn.GetComponentInChildren<TextMeshProUGUI>().text;
        characterInfoText.text = characterInfo;
    }
    public void UnLockBtn()
    {
        var characterSO = SelectList.instance.CharacterDataDic[selectedBtn.name];
        if (Coin.instance.coin >= characterSO.needCoin)
        {
            UpgradeSceneData.instance.CharacterOpenCheck[selectedBtn.name] = true;
            selectedBtn.GetComponent<Image>().color = Color.green;
            unLockBtn.gameObject.SetActive(false);
            nextBtn.gameObject.SetActive(true);

        }
    }
}
