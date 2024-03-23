using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;
using Coffee.UIEffects;

public enum UpgradeType { Health, CardPoints, StickerPoints };

public class StatUpgrade : MonoBehaviour
{
    [SerializeField]
    private UpgradeType upgradeType;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioChecker audioChecker;

    [SerializeField]
    private UIShiny uiShiny;

    [SerializeField]
    private StatsUpgradeAnimationEvent statsUpgradeAnimationEvent;

    [SerializeField]
    private MainCharacterStats mainCharacterStat;

    [SerializeField]
    private BattleResults battleResults;

    [SerializeField]
    private BattleSystem battleSystem;

    [SerializeField]
    private Button statButton;

    [SerializeField]
    private Selectable healthStatSelectable, cardPointStatSelectable;

    [SerializeField]
    private Image statImage;

    [SerializeField]
    private ParticleSystem statParticle;

    [SerializeField]
    private TextMeshProUGUI currentStatText, upgradedStatText, maxStatReachedText;

    [SerializeField]
    private Animator currentStatAnimator, upgradedStatAnimator, healthUpgradeAnimator, cardPointsUpgradeAnimator, stickerPointsUpgradeAnimator;

    [SerializeField]
    private RectTransform middlePosition;

    [SerializeField]
    private int statToIncreaseBy;

    private bool isInTheMiddle;

    private void Awake()
    {
        switch(upgradeType)
        {
            case (UpgradeType.Health):
                HealthUpgrade();
                break;
            case (UpgradeType.CardPoints):
                CardPointsUpgrade();
                break;
            case (UpgradeType.StickerPoints):
                StickerPointsUpgrade();
                break;
        }

        if(GetComponent<MenuButtonNavigations>())
        {
            GetComponent<MenuButtonNavigations>().ChangeWorldSelectableButtons();
        }
    }

    private void HealthUpgrade()
    {
        int upgrade = mainCharacterStat.maximumHealth + statToIncreaseBy;

        upgradedStatText.text = upgrade.ToString();

        currentStatText.text = mainCharacterStat.maximumHealth + " -> ";
    }

    private void CardPointsUpgrade()
    {
        int upgrade = mainCharacterStat.maximumCardPoints + statToIncreaseBy;

        upgradedStatText.text = upgrade.ToString();

        currentStatText.text = mainCharacterStat.maximumCardPoints + " -> ";
    }

    private void StickerPointsUpgrade()
    {
        if(mainCharacterStat.maximumStickerPoints >= mainCharacterStat.stickerPointLimit)
        {
            statButton.interactable = false;
            statButton.GetComponent<Image>().raycastTarget = false;

            statButton.GetComponent<Animator>().SetTrigger("Disabled");
            statButton.GetComponent<Animator>().Play("Disabled");

            maxStatReachedText.gameObject.SetActive(true);
            upgradedStatText.gameObject.SetActive(false);
            currentStatText.gameObject.SetActive(false);

            statImage.raycastTarget = false;

            uiShiny.enabled = false;

            Navigation healthStatNav = healthStatSelectable.navigation;
            Navigation cardPointsNav = cardPointStatSelectable.navigation;

            healthStatNav.selectOnLeft = null;
            cardPointsNav.selectOnRight = null;

            healthStatSelectable.navigation = healthStatNav;
            cardPointStatSelectable.navigation = cardPointsNav;

            return;
        }

        int upgrade = mainCharacterStat.maximumStickerPoints + statToIncreaseBy;

        if(upgrade >= mainCharacterStat.stickerPointLimit)
        {
            upgrade = mainCharacterStat.stickerPointLimit;
        }

        upgradedStatText.text = upgrade.ToString();

        currentStatText.text = mainCharacterStat.maximumStickerPoints + " -> ";
    }

    public void SelectedUpgrade()
    {
        statsUpgradeAnimationEvent.RemoveEventTriggers();

        battleSystem._InputManager.SetSelectedObject(null);

        switch (upgradeType)
        {
            case (UpgradeType.Health):
                CheckIfObjectIsNotInTheMiddlePosition();
                FadeAwayCardPointsUpgrade();
                FadeAwayStickerPointsUpgrade();
                mainCharacterStat.maximumHealth += statToIncreaseBy;
                mainCharacterStat.strength++;
                break;
            case (UpgradeType.CardPoints):
                CheckIfObjectIsNotInTheMiddlePosition();
                FadeAwayHealthUpgrade();
                FadeAwayStickerPointsUpgrade();
                mainCharacterStat.maximumCardPoints += statToIncreaseBy;
                break;
            case (UpgradeType.StickerPoints):
                CheckIfObjectIsNotInTheMiddlePosition();
                FadeAwayHealthUpgrade();
                FadeAwayCardPointsUpgrade();
                CalculateCurrentStickerPoints();
                break;
        }
        battleResults.StatUpgradeTextAnimator.Play("Reverse");

        mainCharacterStat.currentPlayerHealth = mainCharacterStat.maximumHealth;
        mainCharacterStat.currentPlayerCardPoints = mainCharacterStat.maximumCardPoints;
    }

    public void ControllerSelect()
    {
        if(battleSystem._InputManager.ControllerPluggedIn)
        {
            animator.Play("Highlighted");

            audioSource.clip = AudioManager.instance.CursorAudio;

            audioChecker.CheckSettingsVolume();
        }
    }

    public void ControllerDeselect()
    {
        if(battleSystem._InputManager.ControllerPluggedIn)
        {
            animator.Play("Pressed");
        }
    }

    private void CalculateCurrentStickerPoints()
    {
        int difference = mainCharacterStat.maximumStickerPoints - mainCharacterStat.currentPlayerStickerPoints;

        mainCharacterStat.maximumStickerPoints += statToIncreaseBy;

        if (difference > 0)
        {
            if(mainCharacterStat.maximumStickerPoints >= mainCharacterStat.stickerPointLimit)
            {
                mainCharacterStat.currentPlayerStickerPoints = mainCharacterStat.stickerPointLimit - difference;
            }
            else
            {
                mainCharacterStat.currentPlayerStickerPoints = mainCharacterStat.maximumStickerPoints - difference;
            }
        }
        else
        {
            if(mainCharacterStat.maximumStickerPoints >= mainCharacterStat.stickerPointLimit)
            {
                mainCharacterStat.maximumStickerPoints = mainCharacterStat.stickerPointLimit;
                mainCharacterStat.currentPlayerStickerPoints = mainCharacterStat.stickerPointLimit;
            }
            else
            {
                mainCharacterStat.currentPlayerStickerPoints = mainCharacterStat.maximumStickerPoints;
            }
        }
    }

    private void CheckIfObjectIsNotInTheMiddlePosition()
    {
        if(!isInTheMiddle)
        {
            statButton.interactable = false;

            StartCoroutine("MoveToCenter");
        }
        else
        {
            statButton.interactable = false;

            currentStatAnimator.Play("CurrentStat");
            upgradedStatAnimator.Play("UpgradedStat");

            statParticle.gameObject.SetActive(true);

            statParticle.Play();

            AudioManager.instance.PlaySoundEffect(AudioManager.instance.ApplauseAudio);

            StartCoroutine("WaitToSkipResults");
        }
    }

    private void FadeAwayHealthUpgrade()
    {
        healthUpgradeAnimator.Play("Fade");
    }

    private void FadeAwayCardPointsUpgrade()
    {
        cardPointsUpgradeAnimator.Play("Fade");
    }

    private void FadeAwayStickerPointsUpgrade()
    {
        stickerPointsUpgradeAnimator.Play("Fade");
    }

    private IEnumerator WaitToSkipResults()
    {
        yield return new WaitForSeconds(0.5f);
        battleResults.CanSkipResults = true;
    }

    private IEnumerator MoveToCenter()
    {
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();

        float distance = Vector2.Distance(rectTransform.anchoredPosition, middlePosition.anchoredPosition);

        while (distance > 0.4)
        {
            rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, middlePosition.anchoredPosition, 1.5f);

            distance = Vector2.Distance(rectTransform.anchoredPosition, middlePosition.anchoredPosition);

            yield return new WaitForFixedUpdate();
        }
        rectTransform.anchoredPosition = middlePosition.anchoredPosition;

        currentStatAnimator.Play("CurrentStat");
        upgradedStatAnimator.Play("UpgradedStat");

        statParticle.gameObject.SetActive(true);

        AudioManager.instance.PlaySoundEffect(AudioManager.instance.ApplauseAudio);

        statParticle.Play();

        battleResults.CanSkipResults = true;
    }
}