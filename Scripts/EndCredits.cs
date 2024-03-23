using System.Collections;
using UnityEngine;
using TMPro;

public class EndCredits : MonoBehaviour
{
    [SerializeField]
    private InputManager inputManager;

    [SerializeField]
    private SceneNameToLoad sceneNameToLoad;

    [SerializeField]
    private RectTransform viewPortRectTransform;

    [SerializeField]
    private TextMeshProUGUI skipEndCreditsText;

    [SerializeField]
    private GameObject sleepingPlayerObject, battlePlayerObject, battleResultsHolderObject, expBarObject, moneyPanelObject;

    [SerializeField]
    private Animator skipCreditsTextAnimator, canvasGroupAnimator, endCreditsFade, gameCompletionAnimator, thankYouTextFade;

    private Coroutine startCreditsRoutine;

    [SerializeField]
    private float minOffSetTop, scrollSpeed;

    private bool canSkip;

    private void Update()
    {
        if(canSkip)
           SkipEndCredits();
    }

    private void SkipEndCredits()
    {
        if (inputManager.ControllerPluggedIn)
        {
            if (InputManager.instance.ControllerName == "xbox")
            {
                SetSkipEndCreditsText("A");

                if(Input.GetButtonDown("XboxAttack"))
                {
                    ReturnBackToTitleMenu();
                }
            }
            else
            {
                SetSkipEndCreditsText("X");

                if(Input.GetButtonDown("Ps4Attack"))
                {
                    ReturnBackToTitleMenu();
                }
            }
        }
        else
        {
            SetSkipEndCreditsText("SPACEBAR");

            if(Input.GetKeyDown(KeyCode.Space))
            {
                ReturnBackToTitleMenu();
            }
        }
    }

    private void SetSkipEndCreditsText(string buttonText)
    {
        skipEndCreditsText.text = "Skip: " + buttonText;
    }

    private void ReturnBackToTitleMenu()
    {
        if(startCreditsRoutine != null)
        {
            StopCoroutine(startCreditsRoutine);

            startCreditsRoutine = null;
        }

        sceneNameToLoad.FadeOutScene();

        canSkip = false;
    }

    public void BeginCreditScrollCoroutine()
    {
        if(startCreditsRoutine == null)
        {
            startCreditsRoutine = StartCoroutine(StartCreditScroll());
        }
    }

    private IEnumerator StartCreditScroll()
    {
        yield return new WaitForSeconds(5f);

        battlePlayerObject.SetActive(false);
        battleResultsHolderObject.SetActive(false);
        expBarObject.SetActive(false);

        AudioManager.instance.PlayBGM(AudioManager.instance.EndingTheme);

        canSkip = true;

        skipEndCreditsText.gameObject.SetActive(true);

        skipCreditsTextAnimator.Play("FadeIn");

        float currentYMin = viewPortRectTransform.offsetMax.y;

        viewPortRectTransform.offsetMin = new Vector2(0, currentYMin);

        while (-viewPortRectTransform.offsetMax.y > minOffSetTop)
        {
            currentYMin += Time.deltaTime * scrollSpeed;

            viewPortRectTransform.offsetMax = new Vector2(0, currentYMin);

            yield return null;
        }

        canSkip = false;

        skipCreditsTextAnimator.Play("FadeOut");

        StartCoroutine(EndCreditsScreen());
    }

    private IEnumerator EndCreditsScreen()
    {
        gameCompletionAnimator.Play("Fading");

        yield return new WaitForSeconds(2f);

        sleepingPlayerObject.SetActive(true);

        endCreditsFade.Play("FadeOut");

        yield return new WaitForSeconds(2.5f);

        thankYouTextFade.Play("FadeIn");

        yield return new WaitForSeconds(5f);

        endCreditsFade.Play("Fading");
        thankYouTextFade.Play("FadeOut");

        yield return new WaitForSeconds(1.5f);

        sceneNameToLoad.FadeOutScene();

        if (startCreditsRoutine != null)
        {
            StopCoroutine(startCreditsRoutine);

            startCreditsRoutine = null;
        }
    }
}