using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBase
{
    protected readonly string sceneName;

    public SceneBase (string sceneName)
    {
        this.sceneName = sceneName;
    }
    
    public virtual void PrepareScene()
    {
    }

    public virtual void StartScene()
    {
        if (Application.CanStreamedLevelBeLoaded (sceneName))
            SceneManager.LoadScene (sceneName);
    }

    public virtual void ExitScene()
    {
    }
}
