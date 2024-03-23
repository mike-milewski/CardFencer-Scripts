using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void CloseGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}