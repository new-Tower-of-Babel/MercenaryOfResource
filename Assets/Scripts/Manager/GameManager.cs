using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public CharacterPlayData conditionData;

    public GameObject UITab;
    public GameObject UIPause;

    public bool isPause = false;
    public bool isClear = false;
    public bool isGameOver = false;

    private void OnEnable()
    {
        isPause = false;
        isClear = false;
        isGameOver = false;
        Time.timeScale = 1.0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }

        conditionData = PlayDataManager.Instance.characterPlayData;
    }

    // Update is called once per frame
    void Update()
    {
        if(isClear || isGameOver)
        {
            GameOver();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (UITab.activeInHierarchy)
            {
                TimeToggle();
                UITab.SetActive(isPause);

                return;
            }

            if (UIPause.activeInHierarchy)
            {
                if(UIManager.Instance._uiStack.Count > 0)
                {
                    UIManager.Instance.CloseLastUI();
                }
                else
                {
                    TimeToggle();
                    UIPause.SetActive(isPause);
                }
            }
            else
            {
                TimeToggle();
                UIPause.SetActive(isPause);
            }
        }

        if (!UIPause.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                TimeToggle();
                UITab.SetActive(isPause);
            }
        }

        isGameOver = conditionData.nowHealth <= 0;
    }

    public void TimeToggle()
    {
        Time.timeScale = isPause ? 1.0f : 0.0f;
        isPause = !isPause;
        PauseAllZombies(isPause);
    }

    private void PauseAllZombies(bool isPause)
    {
        foreach (var zombie in ZombieManager.Instance.ActiveZombieList)
        {
            zombie.SetActive(!isPause);
        }
    }

    private void GameOver()
    {
        Time.timeScale = 0.0f;
        SceneManager.LoadScene("ResultScene");
        ResultData();
    }

    private void ResultData()
    {
        Coin coinResounce = Coin.instance;
        ResourcePlayData resourcePlayData = PlayDataManager.Instance.resourcePlayData;
        coinResounce.resultWood = resourcePlayData.wood;
        coinResounce.resultStone = resourcePlayData.stone;
        coinResounce.resultSkull = resourcePlayData.skull;
        coinResounce.resultGold = resourcePlayData.gold;
    }
}
