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

    [SerializeField] private AudioClip _bgm;

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
        AudioManager.Instance.PlayBGM(_bgm);
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
            if (!UITab.activeInHierarchy)
            {
                TimeToggle();
            }

            UIPause.SetActive(!UIPause.activeInHierarchy);
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

    private void TimeToggle()
    {
        Time.timeScale = isPause ? 1.0f : 0.0f;
        isPause = !isPause;
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
