using UnityEngine;
using TMPro;

public class LevelUpTextAnimationEvent : MonoBehaviour
{
    [SerializeField]
    private PlayerMenuInfo playerMenuInfo;

    [SerializeField]
    private Coffee.UIEffects.UIShiny uiShiny, uiShinyTwo;

    [SerializeField]
    private TextMeshProUGUI deckSizeText;

    [SerializeField]
    private ParticleSystem levelTextShine, levelTextShineTwo;

    [SerializeField]
    private Animator increasedDeckSizeAnimator;

    public void PlayShinyEffects()
    {
        uiShiny.Play();
        uiShinyTwo.Play();

        increasedDeckSizeAnimator.Play("DeckSize");

        if(playerMenuInfo.currentDeckLimit < playerMenuInfo.maximumDeckLimit)
        {
            deckSizeText.text = "+1 To Deck Size";
        }
        else
        {
            deckSizeText.text = "       Deck Limit Reached";
        }
    }
}