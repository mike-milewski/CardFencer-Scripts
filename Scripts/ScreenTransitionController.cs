using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using Coffee.UIEffects;

public class ScreenTransitionController : MonoBehaviour
{
    [SerializeField]
    private Image fadeScreen;

    [SerializeField]
    private Sprite nonBattleTransition, battleTransition;

    public Sprite NonBattleTransition => nonBattleTransition;

    public Sprite BattleTransition => battleTransition;

    private void Start()
    {
        ScreenFade.instance.FadeIn();
    }

    public IEnumerator FadeIn()
    {
        fadeScreen.GetComponent<UITransitionEffect>().effectFactor = 1;

        SetScreenTransitionSprite(nonBattleTransition, false);

        yield return new WaitForSeconds(0.3f);

        fadeScreen.GetComponent<UITransitionEffect>().Hide();
    }

    public void SetScreenTransitionSprite(Sprite sprite, bool isBattle)
    {
        fadeScreen.sprite = sprite;

        if(!isBattle)
        {
            fadeScreen.color = new Color(0, 0, 0, 1);
        }
        else
        {
            fadeScreen.color = new Color(1, 1, 1, 1);
        }
    }
}