using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScreenFade : MonoBehaviour
{
    public static ScreenFade instance = null;

    [SerializeField]
    private Animator fadeScreenAnimator;

    [SerializeField]
    private ScreenTransitionController screenTransitionController;

    private Coroutine routine;

    private string sceneToLoad;

    public Animator FadeScreenAnimator => fadeScreenAnimator;

    public ScreenTransitionController _ScreenTransitionController
    {
        get
        {
            return screenTransitionController;
        }
        set
        {
            screenTransitionController = value;
        }
    }

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

    private void Awake()
    {
        #region Singelton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        #endregion
    }

    public void BeginFade()
    {
        StartCoroutine("LoadScene");
    }

    private IEnumerator LoadScene()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneToLoad);

        yield return new WaitUntil(() => async.progress >= 1);

        FadeIn();

        if(routine != null)
           StopCoroutine(routine);

        AudioManager.instance._backGroundMusic.PlayBackgroundMusic();
    }

    public void FadeIn()
    {
        sceneToLoad = "";

        fadeScreenAnimator.Play("FadeIn");
    }

    public void FadeOut()
    {
        Time.timeScale = 1;

        routine = StartCoroutine(AudioManager.instance.FadeOutBGM());

        fadeScreenAnimator.Play("FadeOut");
    }
}