using UnityEngine;

public class SceneNameToLoad : MonoBehaviour
{
    [SerializeField]
    private string sceneToLoad;

    public string SceneToLoad
    {
        get
        {
            return sceneToLoad;
        }
        set
        {
            sceneToLoad = value;
        }
    }

    public void FadeOutScene()
    {
        ScreenFade.instance.SceneToLoad = sceneToLoad;

        ScreenFade.instance.FadeOut();
    }
}