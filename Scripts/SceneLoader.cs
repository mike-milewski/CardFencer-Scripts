using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public void LoadIntoScene()
    {
        if (!string.IsNullOrEmpty(ScreenFade.instance.SceneToLoad))
            ScreenFade.instance.BeginFade();
    }
}