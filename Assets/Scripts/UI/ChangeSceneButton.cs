using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneButton : MonoBehaviour
{
    public void LoadTitleScene()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void LoadUpgradeScene()
    {
        SceneManager.LoadScene("UpgradeScene");
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
