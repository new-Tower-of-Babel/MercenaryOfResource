using System.Collections.Generic;
using UnityEngine;

public enum SceneList
{
    TitleScene,
    MainScene,
    SIZE,
}

public static class SceneChanger
{
    private static readonly SceneBase[] sceneArray;
    private static SceneBase currentScene = null;
    
    static SceneChanger()
    {
        sceneArray = new SceneBase[(int)SceneList.SIZE];
        sceneArray[(int)SceneList.TitleScene] = new TitleScene();
        sceneArray[(int)SceneList.MainScene] = new MainScene();
    }

    public static void ChangeScene(SceneList scene)
    {
        currentScene?.ExitScene();
        currentScene = sceneArray[(int)scene];
        currentScene.PrepareScene();
        currentScene.StartScene();
    }
}
