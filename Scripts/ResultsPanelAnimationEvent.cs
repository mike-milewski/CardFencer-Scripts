using UnityEngine;

public class ResultsPanelAnimationEvent : MonoBehaviour
{
    [SerializeField]
    private BattlePlayer battlePlayer;

    [SerializeField]
    private BattleResults battleResults;

    [SerializeField]
    private Animator bonusPanelAnimator;

    public void StartExperienceGain()
    {
        bonusPanelAnimator.Play("BonusFadeIn");

        battleResults.MoneyRoutine = StartCoroutine(battleResults.GainMoney());

        if(battleResults.GainedExperienceToReduce > 0)
        {
            if (battlePlayer._mainCharacterStats.level < battlePlayer._mainCharacterStats.maximumLevel)
                battleResults.ExpRoutine = StartCoroutine(battleResults.GainExperience());
        }
    }

    public void FadeOutBonusPanel()
    {
        bonusPanelAnimator.Play("BonusFadeOut");
    }
}