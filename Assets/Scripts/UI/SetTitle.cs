using UnityEngine;

public class SetTitle : MonoBehaviour
{
    [SerializeField] private AudioClip titleBGM;

    void Start()
    {
        UIManager.Instance.Start();
        AudioManager.Instance.PlayBGM(titleBGM);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (UIManager.Instance._uiStack.Count > 0)
            {
                UIManager.Instance.CloseLastUI();
            }
        }
    }
}
