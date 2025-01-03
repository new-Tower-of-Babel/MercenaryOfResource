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

    public bool isPause = false;
    public bool isClear = false;
    public bool isGameOver = false;

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
            TimeToggle();
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
        //SceneManager.LoadScene();
    }
}
