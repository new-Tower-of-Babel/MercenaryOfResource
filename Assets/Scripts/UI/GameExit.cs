using UnityEngine;

public class GameExit : MonoBehaviour
{
    public void ExitGame()
    {
        // 게임 종료
        Application.Quit();

        // 에디터에서 실행 중일 때 게임 종료 처리
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
