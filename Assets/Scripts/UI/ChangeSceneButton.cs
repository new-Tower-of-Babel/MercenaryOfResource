using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneButton : MonoBehaviour
{
    public void LoadTitleScene()
    {
        SceneManager.LoadScene("TitleScene");
        if(Coin.instance != null) Coin.instance.ResultResourceReset();
    }

    public void LoadUpgradeScene()
    {
        SceneManager.LoadScene("UpgradeScene");
        if(Coin.instance != null) Coin.instance.ResultResourceReset();
    }

    public static void LoadMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void LoadResultScene()
    {
        SceneManager.LoadScene("ResultScene");
    }
}
