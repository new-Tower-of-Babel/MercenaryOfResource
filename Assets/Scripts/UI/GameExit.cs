using UnityEngine;

public class GameExit : MonoBehaviour
{
    public void ExitGame()
    {
        // ���� ����
        Application.Quit();

        // �����Ϳ��� ���� ���� �� ���� ���� ó��
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
