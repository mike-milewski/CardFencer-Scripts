using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Steamworks;

public class FountainMenu : MonoBehaviour
{
    [SerializeField]
    private FountainData fountainData;

    [SerializeField]
    private MainCharacterStats mainCharacterStats;

    [SerializeField]
    private Fountain fountain;

    [SerializeField]
    private PlayerMovement playerMovement;

    [SerializeField]
    private CameraFollow cameraFollow;

    [SerializeField]
    private MenuCard cardReward;

    [SerializeField]
    private Sticker stickerReward;

    [SerializeField]
    private Button offerButton, exitButton;

    [SerializeField]
    private ParticleSystem upgradeParticle;

    [SerializeField]
    private GameObject stickerAndCardInfoPanel, noStatusEffectInfoPanel, withStatusEffectInfoPanel, statusDurationPanel, objectSelectedForInputManager;

    [SerializeField]
    private Animator animator, errorMessageAnimator;

    [SerializeField]
    private Image statusEffectImage;

    [SerializeField]
    private TextMeshProUGUI moonStoneRequirementText, powerText, playerMoonStoneText, errorMessageText, offerButtonText, stickerAndCardTitleText, stickerAndCardInfoText, withStatusEffectText, statusEffectDescription,
                            statusDurationText, fountainLevelText;

    private bool menuAnimationEvent, canSetToggle;

    private void Awake()
    {
        UpdateFountainInformation();
        UpdateMoonStone();
    }

    private void Update()
    {
        if (SteamOverlayPause.instance.IsPaused) return;

        if(InputManager.instance.ControllerPluggedIn)
        {
            if (fountain.OpenedFountainMenu)
            {
                if(InputManager.instance.ControllerName == "xbox")
                {
                    if (Input.GetButtonDown("XboxCancel"))
                    {
                        CloseMenu();

                        AudioManager.instance.PlaySoundEffect(AudioManager.instance.CloseMenuAudio);
                    }
                }
                else
                {
                    if (Input.GetButtonDown("Ps4Cancel"))
                    {
                        CloseMenu();

                        AudioManager.instance.PlaySoundEffect(AudioManager.instance.CloseMenuAudio);
                    }
                }
            }
        }
        else
        {
            if(fountain.OpenedFountainMenu)
            {
                if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Tab))
                {
                    CloseMenu();

                    AudioManager.instance.PlaySoundEffect(AudioManager.instance.CloseMenuAudio);
                }
            }

        }
    }

    public void OpenMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        MenuController.instance.CanToggleMenu = false;

        AudioManager.instance.PlaySoundEffect(AudioManager.instance.OpenMenuAudio);

        playerMovement.PlayerAnimator.Play("IdlePose");

        playerMovement.enabled = false;

        cameraFollow.enabled = false;

        InputManager.instance.SetSelectedObject(null);
        InputManager.instance.ForceCursorOn = true;

        animator.Play("FadeIn");
    }

    public void CloseMenu()
    {
        if(Cursor.visible)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        fountain.OpenedFountainMenu = false;

        playerMovement.enabled = true;

        cameraFollow.enabled = true;

        animator.Play("FadeOut");

        InputManager.instance.SetSelectedObject(null);
    }

    public void SetClosedMenu()
    {
        menuAnimationEvent = true;

        if(!canSetToggle)
        {
            if(offerButton.interactable)
            {
                InputManager.instance.SetSelectedObject(offerButton.gameObject);
            }
            else
            {
                InputManager.instance.SetSelectedObject(exitButton.gameObject);

                SetExiButtonNavigation();
            }

            canSetToggle = true;
        }
    }

    public void SetCanTogglePlayerMenu()
    {
        if (menuAnimationEvent)
        {
            MenuController.instance.CanToggleMenu = true;
            menuAnimationEvent = false;

            if(canSetToggle)
            {
                InputManager.instance.SetSelectedObject(null);

                canSetToggle = false;
            }
        }
    }

    public void OfferMoonStones()
    {
        if(mainCharacterStats.moonStone >= fountainData.fountainPower[fountainData.powerIndex].moonStonesRequired)
        {
            mainCharacterStats.moonStone -= fountainData.fountainPower[fountainData.powerIndex].moonStonesRequired;

            UpdateMoonStone();

            AudioManager.instance.PlaySoundEffect(AudioManager.instance.FountainAudio);

            if (fountainData.fountainPower[fountainData.powerIndex].fountainPowers == FountainPowers.StickerReward)
            {
                MenuController.instance.CreateSticker(fountainData.fountainPower[fountainData.powerIndex].stickerInfo);
                MenuController.instance.CheckStickerAchievement();
            }
            else if(fountainData.fountainPower[fountainData.powerIndex].fountainPowers == FountainPowers.CardReward)
            {
                MenuController.instance.CreateCard(fountainData.fountainPower[fountainData.powerIndex].cardTemplate);
            }
            else
            {
                fountainData.fountainPower[fountainData.powerIndex].GainFountainPower();
            }

            fountainData.powerIndex++;

            upgradeParticle.gameObject.SetActive(true);
            upgradeParticle.Play();

            UpdateFountainInformation();
        }
        else
        {
            ErrorMessage("Not enough Moonstones!");
        }
    }

    private void UpdateFountainInformation()
    {
        UpdateFountainLevel();

        if(fountainData.powerIndex < fountainData.maxFountainLevel)
        {
            moonStoneRequirementText.text = "x" + fountainData.fountainPower[fountainData.powerIndex].moonStonesRequired;

            if(fountainData.fountainPower[fountainData.powerIndex].fountainPowers == FountainPowers.StickerReward)
            {
                powerText.gameObject.SetActive(false);
                cardReward.gameObject.SetActive(false);
                stickerReward.gameObject.SetActive(true);
                stickerAndCardInfoPanel.SetActive(true);

                stickerAndCardTitleText.text = fountainData.fountainPower[fountainData.powerIndex].stickerInfo.stickerName;

                if(fountainData.fountainPower[fountainData.powerIndex].stickerInfo.appliesStatusEffect)
                {
                    withStatusEffectInfoPanel.SetActive(true);
                    noStatusEffectInfoPanel.SetActive(false);
                    statusDurationPanel.SetActive(true);

                    statusDurationText.text = fountainData.fountainPower[fountainData.powerIndex].stickerInfo.statusDuration == 0 ? "Duration: Infinite" : "Duration: " +
                                              fountainData.fountainPower[fountainData.powerIndex].stickerInfo.statusDuration + " Turn(s)";

                    statusEffectDescription.text = fountainData.fountainPower[fountainData.powerIndex].stickerInfo.statusEffectDescription;

                    withStatusEffectText.text = fountainData.fountainPower[fountainData.powerIndex].stickerInfo.stickerDescription;

                    statusEffectImage.sprite = fountainData.fountainPower[fountainData.powerIndex].stickerInfo.statusEffectSprite;
                }
                else
                {
                    noStatusEffectInfoPanel.SetActive(true);
                    withStatusEffectInfoPanel.SetActive(false);
                    statusDurationPanel.SetActive(false);

                    stickerAndCardInfoText.text = fountainData.fountainPower[fountainData.powerIndex].stickerInfo.stickerDescription;
                }

                stickerReward._stickerInformation = fountainData.fountainPower[fountainData.powerIndex].stickerInfo;
                stickerReward.SetUpStickerInformation();
            }
            else if(fountainData.fountainPower[fountainData.powerIndex].fountainPowers == FountainPowers.CardReward)
            {
                powerText.gameObject.SetActive(false);
                stickerReward.gameObject.SetActive(false);
                cardReward.gameObject.SetActive(true);

                if(fountainData.fountainPower[fountainData.powerIndex].cardTemplate.cardStatus != CardStatus.NONE)
                {
                    statusDurationPanel.SetActive(true);
                    stickerAndCardInfoPanel.SetActive(true);
                    noStatusEffectInfoPanel.SetActive(true);
                    withStatusEffectInfoPanel.SetActive(false);

                    stickerAndCardTitleText.text = fountainData.fountainPower[fountainData.powerIndex].cardTemplate.statusEffectName;

                    stickerAndCardInfoText.text = fountainData.fountainPower[fountainData.powerIndex].cardTemplate.cardInformation;

                    statusDurationText.text = "Duration: " + fountainData.fountainPower[fountainData.powerIndex].cardTemplate.statusEffectTime + " Turn(s)";
                }
                else
                {
                    statusDurationPanel.SetActive(false);
                    stickerAndCardInfoPanel.SetActive(false);
                }

                cardReward._cardTemplate = fountainData.fountainPower[fountainData.powerIndex].cardTemplate;
                cardReward.UpdateCardInformation();
            }
            else
            {
                powerText.gameObject.SetActive(true);
                stickerReward.gameObject.SetActive(false);
                cardReward.gameObject.SetActive(false);
                stickerAndCardInfoPanel.SetActive(false);

                powerText.text = fountainData.fountainPower[fountainData.powerIndex].powerInformation;
            }
        }
        else
        {
            moonStoneRequirementText.gameObject.SetActive(false);

            cardReward.gameObject.SetActive(false);
            stickerReward.gameObject.SetActive(false);
            stickerAndCardInfoPanel.SetActive(false);
            powerText.gameObject.SetActive(true);

            powerText.text = "Max Moonstone offers made.";

            offerButton.GetComponent<Image>().raycastTarget = false;

            offerButton.transition = Selectable.Transition.ColorTint;

            offerButtonText.color = new Color(1, 1, 1, 0.7f);

            offerButton.interactable = false;

            InputManager.instance.SetSelectedObject(exitButton.gameObject);

            SetExiButtonNavigation();

            if(SteamManager.Initialized)
            {
                SteamUserStats.GetAchievement("ACH_MOON_KISSED", out bool achievementCompleted);

                if(!achievementCompleted)
                {
                    SteamUserStats.SetAchievement("ACH_MOON_KISSED");
                    SteamUserStats.StoreStats();
                }
            }
        }
    }

    private void UpdateMoonStone()
    {
        playerMoonStoneText.text = mainCharacterStats.moonStone.ToString();
    }

    private void UpdateFountainLevel()
    {
        fountainLevelText.text = "Rank: " + fountainData.powerIndex + "/" + fountainData.maxFountainLevel;
    }

    private void ErrorMessage(string message)
    {
        errorMessageText.text = message;

        errorMessageAnimator.Play("Message", -1, 0);

        AudioManager.instance.PlaySoundEffect(AudioManager.instance.ErrorMessageAudio);
    }

    private void SetExiButtonNavigation()
    {
        Navigation exitButtonNavigation = exitButton.GetComponent<Selectable>().navigation;

        if(!offerButton.interactable)
        {
            exitButtonNavigation.selectOnDown = null;
            exitButtonNavigation.selectOnRight = null;

            exitButton.GetComponent<Selectable>().navigation = exitButtonNavigation;
        }
    }
}