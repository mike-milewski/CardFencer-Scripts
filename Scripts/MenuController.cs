using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Coffee.UIEffects;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Steamworks;
using System.Collections;
using UnityEngine.EventSystems;
using System.Security.Cryptography;

public class MenuController : MonoBehaviour, IUnit
{
    public static MenuController instance = null;

    [SerializeField]
    private PlayerMenuInfo playerMenuInfo;

    [SerializeField]
    private BossData bossData;

    [SerializeField]
    private FieldCardData fieldCardData;

    [SerializeField]
    private TreasureData treasureData;

    [SerializeField]
    private MoonstoneData moonStoneData;

    [SerializeField]
    private ShopInformation shopInformation;

    [SerializeField]
    private WorldEnvironmentData worldEnvironmentData;

    [SerializeField]
    private FountainData fountainData;

    [SerializeField]
    private BattleTutorialAdder battleTutorialAdder;

    [SerializeField]
    private List<Sticker> equippedStickers = new List<Sticker>();

    [SerializeField]
    private GameHandler gameHandler;

    [SerializeField]
    private MenuCard menuCardForDeckToCreate;

    [SerializeField]
    private StickerPowerManager stickerPowerManager;

    [SerializeField]
    private CardTypeCategory cardTypeCategory;

    [SerializeField]
    private StickerPage stickerPage;

    [SerializeField]
    private BestiaryMenu bestiaryMenu;

    [SerializeField]
    private CardCollection cardCollection;

    [SerializeField]
    private FieldTreasuresPanel fieldTreasurePanel;

    [SerializeField]
    private Animator[] buttonAnimators;

    [SerializeField]
    private Image[] menuButtonImages, closeMenuButtonImages, lockedStickerSlots, stickerMenuLockedStickerSlots, lockedItemSlots;

    [SerializeField]
    private GameObject[] gotRankObjects;

    [SerializeField]
    private CanvasGroup[] menuCanvasGroups;

    [SerializeField]
    private Button[] stickerButtons, stickerMenuStickerButtons, buttonsWithIllegalSelectables;

    [SerializeField]
    private Selectable[] selectablesToRemove, selectablesToReplace;

    [SerializeField]
    private Button exitAreaButton, mainMenuCloseButton, glossaryTutorialButton, worldMapTutorialButton, townTutorialButton, basicControlsTutorialButton, battleTutorialButton, cardTutorialButton, stickerTutorialButton,
                   secretAreaTutorialButton, closeMenuButton, saveGameButton;

    [SerializeField]
    private Transform[] stickerSlots;

    [SerializeField]
    private MenuButtonNavigations[] menuButtonNavigations;

    [SerializeField]
    private MainCharacterStats mainCharacterStats;

    [SerializeField]
    private StickerMenu stickerMenu;

    [SerializeField]
    private DeckMenu deckMenu;

    [SerializeField]
    private MenuCard menuCardPrefab, shopPreviewCard, stashCard, cardToWinInBattle;

    [SerializeField]
    private Sticker stickerPrefab, stickerPrefabToBuy, stickerToWinInBattle;

    [SerializeField]
    private SalvageCardPanel salvageCardPanel;

    [SerializeField]
    private PlayerUIFade playerUIFade;

    private CameraFollow cameraFollow = null;

    [SerializeField]
    private ScrollRectEx deckListScroll, cardListScroll, stickerListScroll;

    [SerializeField]
    private HorizontalLayoutGroup tutorialButtonsHorizontalLayout;

    [SerializeField]
    private CanvasGroup mainMenuCanvasGroup, menuButtonsCanvasGroup, stickerCanvasGroup;

    [SerializeField]
    private GameObject menuPlayer, preventDeckMenuCloseButton, uiCamera, deckUiBlocker, stageObjectivePanel, secretStageObjectivePanel, menuPlayerLights, newCardsBanner,
                       newStickersBanner, stagePenaltyPanel, firstSelectedObject, noviceArmor, veteranArmor, eliteArmor;

    [SerializeField]
    private Animator exitMenuButtonAnimator, errorMessageAnimator, menuAnimator, saveGameAnimator, saveGameTextAnimator;

    private Animator gotItemAnimator;

    [SerializeField]
    private Toggle backGroundMusicToggle, soundEffectsToggle;

    [SerializeField]
    private SettingsMenu settingsMenu;

    [SerializeField]
    private Image healthFill, cardPointFill, experienceBarFill, weaponImage, shieldImage, armorImage, rankImage, rankMenuRankImage;

    [SerializeField]
    private Transform deckListContent, cardListContent, stashContent, itemList, stickerList, stickerLayoutMainMenu, stickerLayoutStickerMenu, collectedCardsViewPort, stickerViewPort;

    [SerializeField]
    private TextMeshProUGUI playerName, level, health, cardPoints, stickerPoints, experiencePoints, money, moonStone, stickerMenuStickerPoints, deckLimit, errorMessage, weaponName, weaponDescription, shieldName, shieldDescription, armorName, armorDescription,
                            rank, bronzeRank, silverRank, goldRank, diamondRank, brilliantRank, strengthStat, defenseStat, exitAreaText, tutorialTitleText, stageObjectiveText, secretStageObjectiveText, stagePenaltyText,
                            salvageTextPrompt, saveGameText, cardPointHealText, cardPointTitleText, mysticCardsEquippedText, stashCountText, salvageAllText, organizeTextPrompt;

    [SerializeField]
    private TextMeshProUGUI[] rankLevelRequirements;

    private Scene scene;

    private bool isInDeckMenu, isInStickerMenu, isInRankMenu, isInBestiaryMenu, isInSettingsMenu, stageConnectsToSecret;

    private int stashCount;

    public int StashCount => stashCount;

    public bool IsInDeckMenu
    {
        get => isInDeckMenu;
        set => isInDeckMenu = value;
    }

    public bool IsInStickerMenu
    {
        get => isInStickerMenu;
        set => isInStickerMenu = value;
    }

    public bool IsInRankMenu
    {
        get => isInRankMenu;
        set => isInRankMenu = value;
    }

    public bool IsInBestiaryMenu
    {
        get => isInBestiaryMenu;
        set => isInBestiaryMenu = value;
    }

    public bool IsInSettingsMenu
    {
        get => isInSettingsMenu;
        set => isInSettingsMenu = value;
    }

    public bool StageConnectsToSecret
    {
        get => stageConnectsToSecret;
        set => stageConnectsToSecret = value;
    }

    public MainCharacterStats _MainCharacterStats => mainCharacterStats;

    public GameHandler _GameHandler => gameHandler;

    public StickerPowerManager _StickerPowerManager => stickerPowerManager;

    public CardCollection _CardCollection => cardCollection;

    public FieldTreasuresPanel _FieldTreasurePanel => fieldTreasurePanel;

    public MenuCard _MenuCard => menuCardPrefab;

    public MenuCard ShopPreviewCard => shopPreviewCard;

    public MenuCard CardToWinInBattle => cardToWinInBattle;

    public MenuCard StashCard => stashCard;

    public MenuCard MenuCardForDeckToCreate => menuCardForDeckToCreate;

    public StickerMenu _StickerMenu => stickerMenu;

    public DeckMenu _DeckMenu => deckMenu;

    public Sticker _Sticker => stickerPrefab;

    public Sticker StickerPrefabToBuy => stickerPrefabToBuy;

    public Sticker StickerToWinInBattle => stickerToWinInBattle;

    public SalvageCardPanel _SalvageCardPanel => salvageCardPanel;

    public GameObject DeckUiBlocker => deckUiBlocker;

    public GameObject StageObjectivePanel => stageObjectivePanel;

    public GameObject SecretStageObjectivePanel => secretStageObjectivePanel;

    public GameObject StagePenatlyPanel => stagePenaltyPanel;

    public GameObject MenuPlayerLights => menuPlayerLights;

    public GameObject FirstSelectedObject => firstSelectedObject;

    public CardTypeCategory _CardTypeCategory
    {
        get
        {
            return cardTypeCategory;
        }
        set
        {
            cardTypeCategory = value;
        }
    }

    public StickerPage _StickerPage
    {
        get
        {
            return stickerPage;
        }
        set
        {
            stickerPage = value;
        }
    }

    public BestiaryMenu _BestiaryMenu => bestiaryMenu;

    public SettingsMenu _SettingsMenu => settingsMenu;

    public CameraFollow _CameraFollow
    {
        get
        {
            return cameraFollow;
        }
        set
        {
            cameraFollow = value;
        }
    }

    public TextMeshProUGUI StageObjectiveText
    {
        get
        {
            return stageObjectiveText;
        }
        set
        {
            stageObjectiveText = value;
        }
    }

    public TextMeshProUGUI SecretStageObjectiveText
    {
        get
        {
            return secretStageObjectiveText;
        }
        set
        {
            secretStageObjectiveText = value;
        }
    }

    public TextMeshProUGUI StagePenaltyText => stagePenaltyText;

    public TextMeshProUGUI BronzeRank => bronzeRank;

    public TextMeshProUGUI SilverRank => silverRank;

    public TextMeshProUGUI GoldRank => goldRank;

    public TextMeshProUGUI DiamondRank => diamondRank;

    public TextMeshProUGUI BrilliantRank => brilliantRank;

    public TextMeshProUGUI TutorialTitleText => tutorialTitleText;

    public Transform DeckListContent => deckListContent;

    public Transform CardListContent => cardListContent;

    public Transform StickerList => stickerList;

    public Transform CollectedCardsViewPort => collectedCardsViewPort;

    public Transform[] StickerSlots => stickerSlots;

    public Transform ItemList => itemList;

    public Transform StashContent => stashContent;

    public ScrollRectEx DeckListScroll
    {
        get
        {
            return deckListScroll;
        }
        set
        {
            deckListScroll = value;
        }
    }

    public ScrollRectEx CardListScroll
    {
        get
        {
            return cardListScroll;
        }
        set
        {
            cardListScroll = value;
        }
    }

    public ScrollRectEx StickerListScroll => stickerListScroll;

    public List<Sticker> _equippedStickers => equippedStickers;

    public Button WorldMapTutorialButton => worldMapTutorialButton;

    public Button TownsTutorialButton => townTutorialButton;

    public Button BasicControlsTutorialButton => basicControlsTutorialButton;

    public Button BattleTutorialButton => battleTutorialButton;

    public Button CardTutorialButton => cardTutorialButton;

    public Button StickerTutorialButton => stickerTutorialButton;

    public Button SecretAreaTutorialButton => secretAreaTutorialButton;

    public Button GlossaryTutorialButton => glossaryTutorialButton;

    public Image WeaponImage => weaponImage;

    public Image ShieldImage => shieldImage;

    public GameObject UICamera => uiCamera;

    public Animator[] ButtonAnimators => buttonAnimators;

    public Sticker StickerPrefab
    {
        get
        {
            return stickerPrefab;
        }
        set
        {
            stickerPrefab = value;
        }
    }

    public Animator GotItemAnimator
    {
        get
        {
            return gotItemAnimator;
        }
        set
        {
            gotItemAnimator = value;
        }
    }

    [SerializeField]
    private bool openedMenu, canToggleMenu;

    private bool openedMenuAnimationEvent, hasControllerPluggedIn;

    public bool OpenedMenu
    {
        get
        {
            return openedMenu;
        }
        set
        {
            openedMenu = value;
        }
    }

    public bool CanToggleMenu
    {
        get
        {
            return canToggleMenu;
        }
        set
        {
            canToggleMenu = value;
        }
    }

    private void Awake()
    {
        #region Singleton
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

        SetDeckAndCardListParent();

        SetRankLevelRequirements();

        CheckCurrentScene();
    }

    private void Start()
    {
        CheckTutorialButtons();
    }

    private void Update()
    {
        if (SteamOverlayPause.instance.IsPaused) return;

        if(canToggleMenu)
        {
            if(InputManager.instance.ControllerPluggedIn)
            {
                if(InputManager.instance.ControllerName == "xbox")
                {
                    if(SteamUtils.IsSteamRunningOnSteamDeck())
                    {
                        if (Input.GetButtonDown("XboxMenu"))
                        {
                            if (!openedMenu)
                            {
                                OpenMenu();
                            }
                        }

                        if(openedMenu)
                        {
                            if(Input.GetButtonDown("XboxCancel"))
                            {
                                CloseMenu();

                                AudioManager.instance.PlaySoundEffect(AudioManager.instance.CloseMenuAudio);
                            }
                        }
                    }
                    else
                    {
                        if (Input.GetButtonDown("XboxMenu"))
                        {
                            if (!openedMenu)
                            {
                                OpenMenu();
                            }
                            else
                            {
                                CloseMenu();

                                AudioManager.instance.PlaySoundEffect(AudioManager.instance.CloseMenuAudio);
                            }
                        }
                    }
                }
                else
                {
                    if (Input.GetButtonDown("Ps4Menu"))
                    {
                        if (!openedMenu)
                        {
                            OpenMenu();
                        }
                        else
                        {
                            CloseMenu();

                            AudioManager.instance.PlaySoundEffect(AudioManager.instance.CloseMenuAudio);
                        }
                    }
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.Escape))
                {
                    if (!openedMenu)
                    {
                        OpenMenu();
                    }
                    else
                    {
                        CloseMenu();

                        AudioManager.instance.PlaySoundEffect(AudioManager.instance.CloseMenuAudio);
                    }
                }
            }
        }

        if(InputManager.instance.ControllerPluggedIn)
        {
            if(InputManager.instance.ControllerName == "xbox")
            {
                if (Input.GetButtonDown("XboxCancel"))
                {
                    if (IsInDeckMenu)
                    {
                        if (PlayerPrefs.HasKey("CardTutorial"))
                        {
                            if (deckListContent.childCount >= playerMenuInfo.minimumDeckSize)
                            {
                                if(!deckUiBlocker.activeSelf)
                                {
                                    closeMenuButtonImages[0].GetComponent<Button>().onClick.Invoke();
                                    isInDeckMenu = false;
                                }
                            }
                            else
                            {
                                ErrorMessage("Your deck must contain at least " + playerMenuInfo.minimumDeckSize + " cards!");
                            }
                        }
                    }
                    if (isInStickerMenu)
                    {
                        if (PlayerPrefs.HasKey("StickerTutorial"))
                        {
                            closeMenuButtonImages[1].GetComponent<Button>().onClick.Invoke();
                            isInStickerMenu = false;
                        }
                    }
                    if (isInRankMenu)
                    {
                        closeMenuButtonImages[2].GetComponent<Button>().onClick.Invoke();
                        IsInRankMenu = false;
                    }
                    if (isInBestiaryMenu)
                    {
                        closeMenuButtonImages[3].GetComponent<Button>().onClick.Invoke();
                        isInBestiaryMenu = false;
                    }
                    if (isInSettingsMenu)
                    {
                        closeMenuButtonImages[4].GetComponent<Button>().onClick.Invoke();
                        isInSettingsMenu = false;
                    }
                }

                if(SteamUtils.IsSteamRunningOnSteamDeck())
                {
                    salvageTextPrompt.text = "Salvage: R1";
                    salvageAllText.text = "Salvage All: L1";
                    organizeTextPrompt.text = "Organize: X";
                }
                else
                {
                    salvageTextPrompt.text = "Salvage: RB";
                    salvageAllText.text = "Salvage All: LB";
                    organizeTextPrompt.text = "Organize: X";
                }
            }
            else
            {
                if (Input.GetButtonDown("Ps4Cancel"))
                {
                    if (IsInDeckMenu)
                    {
                        if (PlayerPrefs.HasKey("CardTutorial"))
                        {
                            if (deckListContent.childCount >= playerMenuInfo.minimumDeckSize)
                            {
                                if(!deckUiBlocker.activeSelf)
                                {
                                    closeMenuButtonImages[0].GetComponent<Button>().onClick.Invoke();
                                    isInDeckMenu = false;
                                }
                            }
                            else
                            {
                                ErrorMessage("Your deck must contain at least " + playerMenuInfo.minimumDeckSize + " cards!");
                            }
                        }
                    }
                    if (isInStickerMenu)
                    {
                        if (PlayerPrefs.HasKey("StickerTutorial"))
                        {
                            closeMenuButtonImages[1].GetComponent<Button>().onClick.Invoke();
                            isInStickerMenu = false;
                        }
                    }
                    if (isInRankMenu)
                    {
                        closeMenuButtonImages[2].GetComponent<Button>().onClick.Invoke();
                        IsInRankMenu = false;
                    }
                    if (isInBestiaryMenu)
                    {
                        closeMenuButtonImages[3].GetComponent<Button>().onClick.Invoke();
                        isInBestiaryMenu = false;
                    }
                    if (isInSettingsMenu)
                    {
                        closeMenuButtonImages[4].GetComponent<Button>().onClick.Invoke();
                        isInSettingsMenu = false;
                    }
                }

                salvageTextPrompt.text = "Salvage: R2";
                salvageAllText.text = "Salvage All: L2";
                organizeTextPrompt.text = "Organize: Square";
            }
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.Escape))
            {
                EventSystem eventSystem = FindObjectOfType<EventSystem>();

                if (IsInDeckMenu)
                {
                    if (PlayerPrefs.HasKey("CardTutorial"))
                    {
                        if(deckListContent.childCount >= playerMenuInfo.minimumDeckSize)
                        {
                            if(!deckUiBlocker.activeSelf)
                            {
                                eventSystem.SetSelectedGameObject(null);

                                closeMenuButtonImages[0].GetComponent<Button>().onClick.Invoke();
                                isInDeckMenu = false;
                            }
                        }
                        else
                        {
                            ErrorMessage("Your deck must contain at least " + playerMenuInfo.minimumDeckSize + " cards!");
                        }
                    }
                }
                if (isInStickerMenu)
                {
                    if (PlayerPrefs.HasKey("StickerTutorial"))
                    {
                        eventSystem.SetSelectedGameObject(null);

                        closeMenuButtonImages[1].GetComponent<Button>().onClick.Invoke();
                        isInStickerMenu = false;
                    }
                }
                if (isInRankMenu)
                {
                    eventSystem.SetSelectedGameObject(null);

                    closeMenuButtonImages[2].GetComponent<Button>().onClick.Invoke();
                    IsInRankMenu = false;
                }
                if (isInBestiaryMenu)
                {
                    eventSystem.SetSelectedGameObject(null);

                    closeMenuButtonImages[3].GetComponent<Button>().onClick.Invoke();
                    isInBestiaryMenu = false;
                }
                if (isInSettingsMenu)
                {
                    eventSystem.SetSelectedGameObject(null);

                    closeMenuButtonImages[4].GetComponent<Button>().onClick.Invoke();
                    isInSettingsMenu = false;
                }
            }

            salvageTextPrompt.text = "Salvage: \nRight Mouse";
            salvageAllText.text = "Salvage All:\nMiddle Mouse";
            organizeTextPrompt.text = "Organize: S";
        }
    }

    public void SetSubMenuNavigation(GameObject selected)
    {
        InputManager.instance.SetSelectedObject(selected);
    }

    public void GetMenuButtonNavigations()
    {
        foreach(MenuButtonNavigations menu in menuButtonNavigations)
        {
            menu.ChangeSelectableButtons();
        }
    }

    public void PlayEquipmentParticleOnSelect(ParticleSystem particle)
    {
        if(InputManager.instance.ControllerPluggedIn)
        {
            particle.gameObject.SetActive(true);
            particle.Play();
        }
    }

    public void StopEquipmentParticleOnSelect(ParticleSystem particle)
    {
        particle.Stop();
        particle.gameObject.SetActive(false);
    }

    public void CheckTutorialButtons()
    {
        if(settingsMenu._TutorialChecker.BattleInformation.Count > 0)
        {
            settingsMenu._TutorialChecker.BattleInformation.Clear();
        }

        if(settingsMenu._TutorialChecker.PlayStationBattleInfo.Count > 0)
        {
            settingsMenu._TutorialChecker.PlayStationBattleInfo.Clear();
        }

        if(settingsMenu._TutorialChecker.XboxBattleInfo.Count > 0)
        {
            settingsMenu._TutorialChecker.XboxBattleInfo.Clear();
        }

        if(settingsMenu._TutorialChecker.SteamDeckBattleInfo.Count > 0)
        {
            settingsMenu._TutorialChecker.SteamDeckBattleInfo.Clear();
        }

        if(PlayerPrefs.HasKey("WorldMapTutorial") || NodeManager.instance.HasWorldMapTutorial)
        {
            worldMapTutorialButton.gameObject.SetActive(true);
        }
        if(PlayerPrefs.HasKey("TownsTutorial") || NodeManager.instance.HasTownTutorial)
        {
            townTutorialButton.gameObject.SetActive(true);
        }
        if(PlayerPrefs.HasKey("BasicControlsTutorial") || NodeManager.instance.HasBasicControlsTutorial)
        {
            basicControlsTutorialButton.gameObject.SetActive(true);
        }
        if(PlayerPrefs.HasKey("BattleTutorialOne") || NodeManager.instance.HasBattleTutorialOne)
        {
            battleTutorialButton.gameObject.SetActive(true);

            battleTutorialAdder.AddBattleTutorialInfo(0);
        }
        if(PlayerPrefs.HasKey("BattleTutorialTwo") || NodeManager.instance.HasBattleTutorialTwo)
        {
            battleTutorialAdder.AddBattleTutorialInfo(1);
        }
        if (PlayerPrefs.HasKey("BattleTutorialThree") || NodeManager.instance.HasBattleTutorialThree)
        {
            battleTutorialAdder.AddBattleTutorialInfo(2);
        }
        if (PlayerPrefs.HasKey("CardTutorial") || NodeManager.instance.HasCardsTutorial)
        {
            cardTutorialButton.gameObject.SetActive(true);
        }
        if (PlayerPrefs.HasKey("StickerTutorial") || NodeManager.instance.HasSecretAreaTutorial)
        {
            stickerTutorialButton.gameObject.SetActive(true);
        }
        if (PlayerPrefs.HasKey("SecretStageTutorial") || NodeManager.instance.HasSecretAreaTutorial)
        {
            secretAreaTutorialButton.gameObject.SetActive(true);
        }

        if(SteamUtils.IsSteamRunningOnSteamDeck())
        {
            if (!NodeManager.instance.HasWorldMapTutorial && !NodeManager.instance.HasTownTutorial && !NodeManager.instance.HasBasicControlsTutorial && !NodeManager.instance.HasBattleTutorialOne &&
                !NodeManager.instance.HasCardsTutorial && !NodeManager.instance.HasStickersTutorial)
            {
                glossaryTutorialButton.interactable = false;
            }
        }
        else
        {
            if (!PlayerPrefs.HasKey("WorldMapTutorial") && !PlayerPrefs.HasKey("TownsTutorial") && !PlayerPrefs.HasKey("BasicControlsTutorial") && !PlayerPrefs.HasKey("BattleTutorialOne") &&
            !PlayerPrefs.HasKey("CardTutorial") && !PlayerPrefs.HasKey("StickerTutorial"))
            {
                glossaryTutorialButton.interactable = false;
            }
        }

        settingsMenu.SetDefaultButtonList();
    }

    public void CheckIfButtonsAreActive()
    {
        for (int i = 0; i < selectablesToRemove.Length; i++)
        {
            for (int j = 0; j < buttonsWithIllegalSelectables.Length; j++)
            {
                if (!buttonsWithIllegalSelectables[j].interactable)
                {
                    Navigation nav = selectablesToRemove[i].navigation;

                    if (nav.selectOnUp != null)
                    {
                        if (nav.selectOnUp == buttonsWithIllegalSelectables[j].gameObject)
                        {
                            nav.selectOnUp = null;
                        }
                    }
                    if (nav.selectOnDown != null)
                    {
                        if (nav.selectOnDown.gameObject == buttonsWithIllegalSelectables[j].gameObject)
                        {
                            nav.selectOnDown = null;
                        }
                    }
                    if (nav.selectOnLeft != null)
                    {
                        if (nav.selectOnLeft.gameObject == buttonsWithIllegalSelectables[j].gameObject)
                        {
                            nav.selectOnLeft = null;
                        }
                    }
                    if (nav.selectOnRight != null)
                    {
                        if (nav.selectOnRight.gameObject == buttonsWithIllegalSelectables[j].gameObject)
                        {
                            nav.selectOnRight = null;
                        }
                    }

                    selectablesToRemove[i].navigation = nav;
                }
            }
        }
    }

    public void SetDefaultTutorialInfo()
    {
        for(int i = 0; i < tutorialButtonsHorizontalLayout.transform.childCount; i++)
        {
            if(tutorialButtonsHorizontalLayout.transform.GetChild(i).gameObject.activeSelf)
            {
                Button button = tutorialButtonsHorizontalLayout.transform.GetChild(i).GetComponent<Button>();

                button.onClick.Invoke();
            }
        }
    }

    public void ToggleUiCamera(bool toggle)
    {
        uiCamera.SetActive(toggle);
    }

    public void FadeInPlayer()
    {
        menuPlayer.SetActive(true);

        playerUIFade.StartFadeInCoroutine();
    }

    public void FadeOutPlayer()
    {
        playerUIFade.StartFadeOutCoroutine();
    }

    private void NormalButtonAnimations()
    {
        exitMenuButtonAnimator.Play("Normal");

        foreach (Animator anim in buttonAnimators)
        {
            anim.Play("Normal");
        }
    }

    public void EnableButtonRaycast()
    {
        foreach (Image img in menuButtonImages)
        {
            img.raycastTarget = true;
        }
    }

    public void DisableButtonRaycast()
    {
        foreach (Image img in menuButtonImages)
        {
            img.raycastTarget = false;
        }
    }

    private void DisableSubMenuCloseButtonRaycasting()
    {
        foreach (Image img in closeMenuButtonImages)
        {
            img.raycastTarget = false;
        }
    }

    public void CheckCurrentScene()
    {
        scene = SceneManager.GetActiveScene();
    }

    public void UpdateStashCount()
    {
        int count = 0;

        foreach(MenuCard card in stashContent.GetComponentsInChildren<MenuCard>(true))
        {
            count++;
        }

        stashCount = count;

        stashCountText.text = count >= playerMenuInfo.stashLimit ? "Limit: " + "<color=red>" + count + "</color>" + "/" + playerMenuInfo.stashLimit : "Limit: " + count + "/" + playerMenuInfo.stashLimit;
    }

    public void OpenMenu()
    {
        scene = SceneManager.GetActiveScene();

        InputManager.instance.SetSelectedObject(FirstSelectedObject);

        canToggleMenu = false;
        openedMenu = true;

        UnMuteButtons();

        if(!scene.name.Contains("Field"))
        {
            if(InputManager.instance.ControllerPluggedIn)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
        else
        {
            PlayerInformation playerInformation = FindObjectOfType<PlayerInformation>(true);

            playerInformation.ToggleObject(false, 0);

            WorldMapMovement worldMapMovement = FindObjectOfType<WorldMapMovement>(true);

            worldMapMovement.StageInformationAnimator.Play("Idle");

            worldMapMovement.DisableNodeHover();

            worldMapMovement.enabled = false;
        }

        AudioManager.instance.PlaySoundEffect(AudioManager.instance.OpenMenuAudio);

        DisableSubMenuCloseButtonRaycasting();

        FadeInPlayer();
        menuAnimator.Play("FadeIn");

        NormalButtonAnimations();

        CheckNewCards();
        CheckNewStickers();

        if(gotItemAnimator != null)
           gotItemAnimator.Play("Idle");

        if(cameraFollow != null)
           cameraFollow.enabled = false;

        UpdatePlayerInformation();
        UpdatePlayerEquipment();
        UpdatePlayerRank();
        UpdateMysticCardCount();

        if(GameManager.instance != null)
           GameManager.instance.HideUI();

        if (!scene.name.Contains("Field"))
            Time.timeScale = 0;

        ResetMenu();
        CheckIfButtonsAreActive();
    }

    public void CloseMenu()
    {
        Scene scene = SceneManager.GetActiveScene();

        if (!scene.name.Contains("Field"))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            if (GameManager.instance.StagePenaltyPanel != null)
            {
                GameManager.instance.StagePenaltyPanel.ShowPenaltyPanel();
            }

            InputManager.instance._EventSystem.SetSelectedGameObject(null);
        }
        else
        {
            PlayerInformation playerInformation = FindObjectOfType<PlayerInformation>(true);

            playerInformation.ToggleObject(true, 1);

            playerInformation.UpdatePlayerInformation();

            WorldMapMovement worldMapMovement = FindObjectOfType<WorldMapMovement>();

            worldMapMovement.enabled = true;

            worldMapMovement.CheckNodeHover();

            InputManager.instance.CurrentSelectedObject = worldMapMovement.LevelNodes[worldMapMovement.CurrentNodeIndex].gameObject;

            if (InputManager.instance.ControllerPluggedIn)
            {
                InputManager.instance._EventSystem.SetSelectedGameObject(worldMapMovement.LevelNodes[worldMapMovement.CurrentNodeIndex].gameObject);

                worldMapMovement.LevelNodes[worldMapMovement.CurrentNodeIndex].GetComponent<StageInformation>().ShowInfoOnMenuClose();
            }
        }

        if (!InputManager.instance.ControllerPluggedIn)
        {
            AudioManager.instance.PlaySoundEffect(AudioManager.instance.SelectedAudio);
        }

        if (GameManager.instance != null)
        {
            GameManager.instance.ResetFieldPlayerAnimator();
            GameManager.instance.ShowUI();
        }

        MuteButtons();
        FadeOutPlayer();

        menuAnimator.Play("FadeOut");

        if (cameraFollow != null)
            cameraFollow.enabled = true;

        if (!scene.name.Contains("Field"))
            Time.timeScale = 1;
    }

    private void MuteButtons()
    {
        closeMenuButton.GetComponent<AudioSource>().volume = 0;

        for(int i = 0; i < menuButtonImages.Length; i++)
        {
            menuButtonImages[i].GetComponent<AudioSource>().volume = 0;
        }

        foreach(Button buttons in stickerButtons)
        {
            buttons.GetComponent<AudioSource>().volume = 0;
        }
    }

    private void UnMuteButtons()
    {
        closeMenuButton.GetComponent<AudioSource>().volume = 1;

        for (int i = 0; i < menuButtonImages.Length; i++)
        {
            menuButtonImages[i].GetComponent<AudioSource>().volume = 1;
        }

        foreach (Button buttons in stickerButtons)
        {
            buttons.GetComponent<AudioSource>().volume = 1;
        }
    }

    public void CheckNewCards()
    {
        int newCardCount = 0;

        foreach (MenuCard card in stashContent.GetComponentsInChildren<MenuCard>(true))
        {
            if (card.NewCardText.gameObject.activeSelf)
            {
                newCardCount++;
            }
        }

        if(newCardCount > 0)
        {
            newCardsBanner.SetActive(true);
        }
        else
        {
            newCardsBanner.SetActive(false);
        }
    }

    public void CheckStickerAchievement()
    {
        if (SteamManager.Initialized)
        {
            var allStickers = Resources.LoadAll("Stickers/");

            int stickerCount = 0;

            SteamUserStats.GetStat("ACH_STICK_COUNT", out stickerCount);
            stickerCount++;
            SteamUserStats.SetStat("ACH_STICK_COUNT", stickerCount);
            SteamUserStats.StoreStats();

            SteamUserStats.GetAchievement("ACH_STICKER_CONNOISSEUR", out bool achievementCompleted);

            if (!achievementCompleted)
            {
                if (stickerCount >= allStickers.Length - 1)
                {
                    SteamUserStats.SetAchievement("ACH_STICKER_CONNOISSEUR");
                    SteamUserStats.StoreStats();
                }
            }
        }
    }

    public void CheckNewStickers()
    {
        int newStickerCount = 0;

        foreach (Sticker sticker in stickerViewPort.GetComponentsInChildren<Sticker>(true))
        {
            if (sticker.NewStickerText.gameObject.activeSelf)
            {
                newStickerCount++;
            }
        }

        if(newStickerCount > 0)
        {
            newStickersBanner.SetActive(true);
        }
        else
        {
            newStickersBanner.SetActive(false);
        }
    }

    public void CreateCard(CardTemplate cardTemplate)
    {
        var card = Instantiate(stashCard);

        card._cardTemplate = cardTemplate;

        card.gameObject.SetActive(true);

        card.NewCardText.SetActive(true);

        card.UpdateCardInformation();

        card.SetDefaultScrollView();

        cardTypeCategory.SetCardParent(card);

        UpdateStashCount();

        cardCollection.CheckCurrentList(card._cardTemplate);
    }

    public void CreateSticker(StickerInformation stickerInfo)
    {
        var sticker = Instantiate(stickerPrefab);

        sticker._stickerInformation = stickerInfo;

        sticker.gameObject.SetActive(true);

        sticker.NewStickerText.SetActive(true);

        sticker.SetUpStickerInformation();

        stickerPage.SetStickerCategory(sticker);
    }

    public void ResetAnimationEvent()
    {
        openedMenuAnimationEvent = false;
    }

    public void SetOpenedMenuAnimationEvent()
    {
        openedMenuAnimationEvent = true;
    }

    public void SetCanOpen()
    {
        canToggleMenu = true;
    }

    public void ToggleMenuOpenedAnimationEvent()
    {
        if(openedMenuAnimationEvent)
        {
            openedMenu = false;
            canToggleMenu = true;
            openedMenuAnimationEvent = false;
        }
    }

    public void CloseMenuThroughExitButton()
    {
        mainMenuCanvasGroup.alpha = 0;
        mainMenuCanvasGroup.interactable = false;
        mainMenuCanvasGroup.blocksRaycasts = false;

        EnableCanvasGroupValues();

        EnableButtonRaycast();

        playerUIFade.gameObject.SetActive(false);

        menuAnimator.Play("Idle");

        Time.timeScale = 1;
    }

    public void UpdatePlayerInformation()
    {
        playerName.text = mainCharacterStats.playerName;
        level.text = "L<size=15>v</size>: " + mainCharacterStats.level.ToString();
        health.text = mainCharacterStats.currentPlayerHealth + "/" + mainCharacterStats.maximumHealth;
        cardPoints.text = mainCharacterStats.currentPlayerCardPoints + "/" + mainCharacterStats.maximumCardPoints;
        stickerPoints.text = mainCharacterStats.currentPlayerStickerPoints + "/" + mainCharacterStats.maximumStickerPoints;

        if (mainCharacterStats.level < mainCharacterStats.maximumLevel)
        {
            experiencePoints.text = "EXP: " + mainCharacterStats.currentExp + "/" + mainCharacterStats.nextExpToLevel;
            experienceBarFill.fillAmount = mainCharacterStats.currentExp / (float)mainCharacterStats.nextExpToLevel;
        }
        else
        {
            experiencePoints.text = "EXP: ---/---";
            experienceBarFill.fillAmount = 1;
        }

        money.text = mainCharacterStats.money.ToString();
        moonStone.text = "<size=15>x</size> " + mainCharacterStats.moonStone;
        strengthStat.text = mainCharacterStats.strength.ToString();
        defenseStat.text = mainCharacterStats.defense.ToString();

        healthFill.fillAmount = mainCharacterStats.currentPlayerHealth / (float)mainCharacterStats.maximumHealth;
        cardPointFill.fillAmount = mainCharacterStats.currentPlayerCardPoints / (float)mainCharacterStats.maximumCardPoints;
        
    }

    public void UpdatePlayerEquipment()
    {
        weaponImage.sprite = playerMenuInfo.weaponSprite[playerMenuInfo.weaponIndex];

        weaponName.text = playerMenuInfo.weaponName[playerMenuInfo.weaponIndex];

        weaponDescription.text = playerMenuInfo.weaponDescription[playerMenuInfo.weaponIndex];

        shieldImage.sprite = playerMenuInfo.shieldSprite[playerMenuInfo.shieldIndex];

        shieldName.text = playerMenuInfo.shieldName[playerMenuInfo.shieldIndex];

        shieldDescription.text = playerMenuInfo.shieldDescription[playerMenuInfo.shieldIndex];

        armorImage.sprite = playerMenuInfo.armorSprite[playerMenuInfo.armorIndex];

        armorName.text = playerMenuInfo.armorName[playerMenuInfo.armorIndex];

        armorDescription.text = playerMenuInfo.armorDescription[playerMenuInfo.armorIndex];

        switch(playerMenuInfo.armorIndex)
        {
            case 0:
                noviceArmor.SetActive(true);
                veteranArmor.SetActive(false);
                eliteArmor.SetActive(false);
                break;
            case 1:
                noviceArmor.SetActive(false);
                veteranArmor.SetActive(true);
                eliteArmor.SetActive(false);
                break;
            case 2:
                noviceArmor.SetActive(false);
                veteranArmor.SetActive(false);
                eliteArmor.SetActive(true);
                break;
        }

        UpdateCardPointHealInfo();
    }

    public void UpdateCardPointHealInfo()
    {
        float healPercentage = mainCharacterStats.cardPointHealPercentage * 100;
        float cardPointHeal = Mathf.RoundToInt(stickerPowerManager.CheckFieldStickerPower(StickerPower.BloodSucker) ? mainCharacterStats.cardPointHealPercentage * mainCharacterStats.maximumHealth :
                                                                                                                      mainCharacterStats.cardPointHealPercentage * mainCharacterStats.maximumCardPoints);

        cardPointTitleText.text = stickerPowerManager.CheckFieldStickerPower(StickerPower.BloodSucker) ? "HP Heal" : "CP Heal";
        cardPointHealText.text = healPercentage + "%" + "(" + cardPointHeal + ")";
    }

    public void UpdatePlayerRank()
    {
        if(playerMenuInfo.rankIndex > -1)
        {
            RankTextGradient();

            UIShadow uiShadow = rankImage.GetComponent<UIShadow>();

            UIShiny uiShiny = rankImage.GetComponent<UIShiny>();

            rank.fontSize = 23;

            rank.text = playerMenuInfo.rankName[playerMenuInfo.rankIndex];

            rankImage.sprite = playerMenuInfo.rankSprites[playerMenuInfo.rankIndex];

            rankImage.color = new Color(1, 1, 1, 1);

            uiShadow.enabled = true;
            uiShiny.enabled = true;
        }
        else
        {
            UIShadow uiShadow = rankImage.GetComponent<UIShadow>();

            UIShiny uiShiny = rankImage.GetComponent<UIShiny>();

            rank.fontSize = 12;

            rank.text = "First Rank Unlocked At LV " + playerMenuInfo.levelRequirement[playerMenuInfo.levelIndex];

            rank.enableVertexGradient = false;

            rankImage.color = new Color(0, 0, 0, 0.5607843f);

            rankImage.sprite = playerMenuInfo.rankSprites[0];

            uiShadow.enabled = false;
            uiShiny.enabled = false;
        }
    }

    public void UpdateMysticCardCount()
    {
        mysticCardsEquippedText.text = playerMenuInfo.currentlyEquippedMysticCards >= playerMenuInfo.maximumMysticCards ? "<color=#A433D2>Mystic</color> Cards: " + "<color=red>" + 
                                       playerMenuInfo.currentlyEquippedMysticCards + "</color>" + "/" + playerMenuInfo.maximumMysticCards : "<color=#A433D2>Mystic</color> Cards: " + 
                                       playerMenuInfo.currentlyEquippedMysticCards + "/" + playerMenuInfo.maximumMysticCards;
    }

    public void UpdateRankMenuPlayerRank()
    {
        if (playerMenuInfo.rankIndex > -1)
        {
            UIShadow uiShadow = rankMenuRankImage.GetComponent<UIShadow>();

            UIShiny uiShiny = rankMenuRankImage.GetComponent<UIShiny>();

            rankMenuRankImage.sprite = playerMenuInfo.rankSprites[playerMenuInfo.rankIndex];

            rankMenuRankImage.color = new Color(1, 1, 1, 1);

            uiShadow.enabled = true;
            uiShiny.enabled = true;

            for(int i = 0; i <= playerMenuInfo.rankIndex; i++)
            {
                gotRankObjects[i].SetActive(true);
            }
        }
        else
        {
            UIShadow uiShadow = rankMenuRankImage.GetComponent<UIShadow>();

            UIShiny uiShiny = rankMenuRankImage.GetComponent<UIShiny>();

            rankMenuRankImage.color = new Color(0, 0, 0, 0.5607843f);

            uiShadow.enabled = false;
            uiShiny.enabled = false;

            for (int i = 0; i < gotRankObjects.Length; i++)
            {
                gotRankObjects[i].SetActive(false);
            }
        }
    }

    private void RankTextGradient()
    {
        rank.enableVertexGradient = true;

        VertexGradient textGradient = rank.colorGradient;

        switch(playerMenuInfo.rankIndex)
        {
            case (0):
                textGradient.bottomLeft = bronzeRank.colorGradient.bottomLeft;
                textGradient.bottomRight = bronzeRank.colorGradient.bottomRight;
                textGradient.topLeft = bronzeRank.colorGradient.topLeft;
                textGradient.topRight = bronzeRank.colorGradient.topRight;
                break;
            case (1):
                textGradient.bottomLeft = silverRank.colorGradient.bottomLeft;
                textGradient.bottomRight = silverRank.colorGradient.bottomRight;
                textGradient.topLeft = silverRank.colorGradient.topLeft;
                textGradient.topRight = silverRank.colorGradient.topRight;
                break;
            case (2):
                textGradient.bottomLeft = goldRank.colorGradient.bottomLeft;
                textGradient.bottomRight = goldRank.colorGradient.bottomRight;
                textGradient.topLeft = goldRank.colorGradient.topLeft;
                textGradient.topRight = goldRank.colorGradient.topRight;
                break;
            case (3):
                textGradient.bottomLeft = diamondRank.colorGradient.bottomLeft;
                textGradient.bottomRight = diamondRank.colorGradient.bottomRight;
                textGradient.topLeft = diamondRank.colorGradient.topLeft;
                textGradient.topRight = diamondRank.colorGradient.topRight;
                break;
            case (4):
                textGradient.bottomLeft = brilliantRank.colorGradient.bottomLeft;
                textGradient.bottomRight = brilliantRank.colorGradient.bottomRight;
                textGradient.topLeft = brilliantRank.colorGradient.topLeft;
                textGradient.topRight = brilliantRank.colorGradient.topRight;
                break;
        }

        rank.colorGradient = textGradient;
    }

    public void UpdateStickerPointsValue()
    {
        stickerMenuStickerPoints.text = mainCharacterStats.currentPlayerStickerPoints + "/" + mainCharacterStats.maximumStickerPoints;
    }

    public void DisplayStickerInfo(Transform stickerLayout)
    {
        if(stickerLayout.childCount > 0)
        {
            if(!stickerLayout.GetChild(0).gameObject.activeSelf)
            {
                if(stickerLayout.childCount > 1)
                {
                    Sticker sticker = stickerLayout.GetChild(1).GetComponent<Sticker>();

                    sticker.DisplayStickerInfo();
                }
            }
            else
            {
                Sticker sticker = stickerLayout.GetChild(0).GetComponent<Sticker>();

                sticker.DisplayStickerInfo();
            }
        }
    }

    public void HideStickerInfo(Transform stickerLayout)
    {
        if (stickerLayout.childCount > 0)
        {
            if (!stickerLayout.GetChild(0).gameObject.activeSelf)
            {
                if(stickerLayout.childCount > 1)
                {
                    Sticker sticker = stickerLayout.GetChild(1).GetComponent<Sticker>();

                    sticker.HideStickerPanel();
                }
            }
            else
            {
                Sticker sticker = stickerLayout.GetChild(0).GetComponent<Sticker>();

                sticker.HideStickerPanel();
            }
        }
    }

    public void UpdateDeckLimit()
    {
        deckLimit.text = deckListContent.childCount + "/" + playerMenuInfo.currentDeckLimit;

        if(deckListContent.childCount < playerMenuInfo.minimumDeckSize)
        {
            preventDeckMenuCloseButton.SetActive(true);
        }
        else
        {
            preventDeckMenuCloseButton.SetActive(false);
        }
    }

    private void SetRankLevelRequirements()
    {
        for(int i = 0; i < rankLevelRequirements.Length; i++)
        {
            rankLevelRequirements[i].text = "L<size=8>V</size>: " + playerMenuInfo.levelRequirement[i];
        }
    }

    public void CheckDeckList()
    {
        if(deckListContent.childCount < playerMenuInfo.minimumDeckSize)
        {
            ErrorMessage("Your deck must contain at least " + playerMenuInfo.minimumDeckSize + " cards!");
        }
    }

    public void SetDefaultDeck()
    {
        ResetMenuInfo();

        mainCharacterStats.currentCards.Clear();
        mainCharacterStats.currentCards = new List<CardTemplate>(new CardTemplate[mainCharacterStats.startingCards.Count]);

        if(deckListContent.childCount > 0)
        {
            foreach (MenuCard card in deckListContent.transform.GetComponentsInChildren<MenuCard>(true))
            {
                Destroy(card.gameObject);
            }
        }

        for(int i = 0; i < mainCharacterStats.currentCards.Count; i++)
        {
            var card = Instantiate(menuCardForDeckToCreate, deckListContent);

            card.InDeckList = true;

            card.gameObject.SetActive(true);

            card._cardTemplate = mainCharacterStats.startingCards[i];

            mainCharacterStats.currentCards[i] = mainCharacterStats.startingCards[i];

            card.CardIndex = i;

            card.UpdateCardInformation();
        }

        if(mainCharacterStats.startingItem != null)
        {
            var card = Instantiate(menuCardPrefab, itemList);

            card._Animator.enabled = true;

            card.gameObject.SetActive(true);

            card._cardTemplate = mainCharacterStats.startingItem;

            card._propogateDrag.scrollView = cardListScroll;

            card.UpdateCardInformation();

            card.EquipItemCard();
        }

        if(NodeManager.instance != null)
           CheckStagePenalties();
    }

    private void ResetMenuInfo()
    {
        playerMenuInfo.rankIndex = -1;

        playerMenuInfo.levelIndex = 0;

        playerMenuInfo.currentHandSize = 3;

        playerMenuInfo.currentStickerSlotSize = 3;

        playerMenuInfo.currentItemSlotSize = 1;

        playerMenuInfo.currentDeckLimit = 10;

        playerMenuInfo.currentEquippedItems.Clear();

        equippedStickers.Clear();

        playerMenuInfo.equippedStickers = 0;

        playerMenuInfo.equippedItems = 0;

        playerMenuInfo.weaponIndex = 0;

        playerMenuInfo.shieldIndex = 0;

        playerMenuInfo.armorIndex = 0;

        bestiaryMenu.ResetBestiaryMenu();

        LockStickerSlots();
        LockItemSlots();
        UpdatePlayerRank();
        UpdateRankMenuPlayerRank();

        if(deckMenu.ItemCards.Count > 0)
           deckMenu.ItemCards.Clear();

        for (int i = 0; i < itemList.childCount; i++)
        {
            if (itemList.GetChild(i).GetComponent<MenuCard>())
            {
                Destroy(itemList.GetChild(i).gameObject);
            }
        }

        stickerMenu.ResetStickerArrowNavigations();

        DestroyAllStickers();

        foreach (MenuCard menuCard in stashContent.GetComponentsInChildren<MenuCard>(true))
        {
            Destroy(menuCard.gameObject);
        }

        UpdateStashCount();
    }

    private void DestroyAllStickers()
    {
        foreach (Sticker sticker in stickerLayoutMainMenu.GetComponentsInChildren<Sticker>())
        {
            sticker.LoseStickerAbility();

            Destroy(sticker.gameObject);
        }

        foreach (Sticker sticker in stickerLayoutStickerMenu.GetComponentsInChildren<Sticker>())
        {
            Destroy(sticker.gameObject);
        }

        foreach (Sticker sticker in stickerList.GetComponentsInChildren<Sticker>())
        {
            Destroy(sticker.gameObject);
        }
    }

    public void ShowSaveWarningPanel()
    {
        saveGameAnimator.Play("Show");
    }

    public void SaveGame()
    {
        gameHandler.Save();

        PlaySaveGameTextAnimation();
    }

    public void AutoSave()
    {
        gameHandler.Save();
    }

    private void PlaySaveGameTextAnimation()
    {
        saveGameTextAnimator.Play("SaveText", -1, 0);
    }

    public void CheckStagePenalties()
    {
        if(NodeManager.instance._StagePenalty.Count > 0)
        {
            foreach(MenuCard menuCard in deckListContent.GetComponentsInChildren<MenuCard>())
            {
                for(int i = 0; i < NodeManager.instance._StagePenalty.Count; i++)
                {
                    switch(NodeManager.instance._StagePenalty[i])
                    {
                        case (StagePenalty.Power):
                            if(menuCard._cardTemplate.cardType == CardType.Action)
                            {
                                menuCard.ForbiddenIcon.SetActive(true);
                            }
                            break;
                        case (StagePenalty.Magic):
                            if (menuCard._cardTemplate.cardType == CardType.Magic)
                            {
                                menuCard.ForbiddenIcon.SetActive(true);
                            }
                            break;
                        case (StagePenalty.Support):
                            if (menuCard._cardTemplate.cardType == CardType.Support)
                            {
                                menuCard.ForbiddenIcon.SetActive(true);
                            }
                            break;
                        case (StagePenalty.Mystic):
                            if (menuCard._cardTemplate.cardType == CardType.Mystic)
                            {
                                menuCard.ForbiddenIcon.SetActive(true);
                            }
                            break;
                        case (StagePenalty.Item):
                            if (menuCard._cardTemplate.cardType == CardType.Item)
                            {
                                menuCard.ForbiddenIcon.SetActive(true);
                            }
                            break;
                    }
                }
            }

            foreach (MenuCard menuCard in stashContent.GetComponentsInChildren<MenuCard>(true))
            {
                for (int i = 0; i < NodeManager.instance._StagePenalty.Count; i++)
                {
                    switch (NodeManager.instance._StagePenalty[i])
                    {
                        case (StagePenalty.Power):
                            if (menuCard._cardTemplate.cardType == CardType.Action)
                            {
                                menuCard.ForbiddenIcon.SetActive(true);
                            }
                            break;
                        case (StagePenalty.Magic):
                            if (menuCard._cardTemplate.cardType == CardType.Magic)
                            {
                                menuCard.ForbiddenIcon.SetActive(true);
                            }
                            break;
                        case (StagePenalty.Support):
                            if (menuCard._cardTemplate.cardType == CardType.Support)
                            {
                                menuCard.ForbiddenIcon.SetActive(true);
                            }
                            break;
                        case (StagePenalty.Mystic):
                            if (menuCard._cardTemplate.cardType == CardType.Mystic)
                            {
                                menuCard.ForbiddenIcon.SetActive(true);
                            }
                            break;
                        case (StagePenalty.Item):
                            if (menuCard._cardTemplate.cardType == CardType.Item)
                            {
                                menuCard.ForbiddenIcon.SetActive(true);
                            }
                            break;
                    }
                }
            }

            foreach (MenuCard menuCard in itemList.GetComponentsInChildren<MenuCard>())
            {
                for (int i = 0; i < NodeManager.instance._StagePenalty.Count; i++)
                {
                    switch (NodeManager.instance._StagePenalty[i])
                    {
                        case (StagePenalty.Item):
                            if (menuCard._cardTemplate.cardType == CardType.Item)
                            {
                                menuCard.ForbiddenIcon.SetActive(true);
                            }
                            break;
                    }
                }
            }
        }

        if(!string.IsNullOrEmpty(scene.name))
        {
            if (!scene.name.Contains("Field") && !scene.name.Contains("Menu"))
            {
                if (GameManager.instance.StagePenaltyPanel != null)
                    GameManager.instance.StagePenaltyPanel.ShowPenaltyPanel();
            }
        }
    }

    public void ClearStagePenalties()
    {
        if (NodeManager.instance._StagePenalty.Count > 0)
        {
            foreach (MenuCard menuCard in deckListContent.GetComponentsInChildren<MenuCard>(true))
            {
                menuCard.ForbiddenIcon.SetActive(false);
            }

            foreach (MenuCard menuCard in stashContent.GetComponentsInChildren<MenuCard>(true))
            {
                menuCard.ForbiddenIcon.SetActive(false);
            }

            foreach (MenuCard menuCard in itemList.GetComponentsInChildren<MenuCard>(true))
            {
                menuCard.ForbiddenIcon.SetActive(false);
            }
        }
    }

    public void ReOrganizeCardIndex()
    {
        for (int i = 0; i < deckListContent.childCount; i++)
        {
            MenuCard card = deckListContent.GetChild(i).GetComponent<MenuCard>();
            card.CardIndex = i;
        }
    }

    public void EnableCanvasGroupValues()
    {
        menuButtonsCanvasGroup.interactable = true;

        stickerCanvasGroup.interactable = true;

        mainMenuCloseButton.interactable = true;
    }

    public string ErrorMessage(string message)
    {
        errorMessageAnimator.Play("Message", -1, 0);

        AudioManager.instance.PlaySoundEffect(AudioManager.instance.ErrorMessageAudio);

        errorMessage.text = message;

        return message;
    }

    public void ToggleExitAreaButton(bool toggle)
    {
        exitAreaButton.interactable = toggle;

        if (!toggle)
        {
            exitAreaButton.transition = Selectable.Transition.ColorTint;

            exitAreaText.color = new Color(1, 1, 1, 0.7f);
        }
        else
        {
            exitAreaButton.transition = Selectable.Transition.Animation;

            exitAreaText.color = new Color(1, 1, 1, 1);

            exitAreaButton.gameObject.SetActive(false);
            exitAreaButton.gameObject.SetActive(true);
        }
    }

    public void ToggleSaveButton(bool toggle)
    {
        saveGameButton.interactable = toggle;

        Animator animator = saveGameText.GetComponent<Animator>();

        if (!toggle)
        {
            saveGameButton.transition = Selectable.Transition.ColorTint;

            animator.enabled = false;

            saveGameText.color = new Color(1, 1, 1, 0.7f);
        }
        else
        {
            saveGameButton.transition = Selectable.Transition.Animation;

            animator.enabled = true;

            saveGameText.color = new Color(1, 1, 1, 1);

            saveGameButton.gameObject.SetActive(false);
            saveGameButton.gameObject.SetActive(true);
        }
    }

    public void OpenExitAreaPanel()
    {
        ExitArea.instance.ExitAreaPanel();

        ExitArea.instance.WarningText.text = "All progress made here will be lost!";

        ExitArea.instance.ShouldMovePlayer = false;
    }

    private void SetDeckAndCardListParent()
    {
        if(deckListContent.childCount > 0)
        {
            foreach (MenuCard menuCard in deckListContent.GetComponentsInChildren<MenuCard>())
            {
                menuCard.DeckListParent = deckListContent;

                menuCard._propogateDrag.scrollView = deckListScroll;

                menuCard.InDeckList = true;
            }
        }
        
        if(cardListContent.childCount > 0)
        {
            foreach (MenuCard menuCard in cardListContent.GetComponentsInChildren<MenuCard>())
            {
                menuCard.DeckListParent = deckListContent;

                menuCard._propogateDrag.scrollView = cardListScroll;

                menuCard.InDeckList = false;
            }
        }
    }

    private void LockStickerSlots()
    {
        lockedStickerSlots[0].gameObject.SetActive(true);
        lockedStickerSlots[0].enabled = true;
        stickerButtons[0].GetComponent<Image>().raycastTarget = false;
        stickerButtons[0].interactable = false;

        stickerMenuLockedStickerSlots[0].gameObject.SetActive(true);
        stickerMenuStickerButtons[0].interactable = false;
        stickerMenuStickerButtons[0].GetComponent<Image>().raycastTarget = false;

        lockedStickerSlots[1].gameObject.SetActive(true);
        lockedStickerSlots[1].enabled = true;
        stickerButtons[1].GetComponent<Image>().raycastTarget = false;
        stickerButtons[1].interactable = false;

        stickerMenuLockedStickerSlots[1].gameObject.SetActive(true);
        stickerMenuStickerButtons[1].interactable = false;
        stickerMenuStickerButtons[1].GetComponent<Image>().raycastTarget = false;
    }

    private void LockItemSlots()
    {
        lockedItemSlots[0].gameObject.SetActive(true);
        lockedItemSlots[0].enabled = true;
        lockedItemSlots[0].transform.parent.gameObject.SetActive(true);

        lockedItemSlots[1].gameObject.SetActive(true);
        lockedItemSlots[1].enabled = true;
        lockedItemSlots[1].transform.parent.gameObject.SetActive(true);
    }

    public void UnlockStickerSlot()
    {
        playerMenuInfo.currentStickerSlotSize++;

        if(lockedStickerSlots[0].enabled)
        {
            lockedStickerSlots[0].gameObject.SetActive(false);
            lockedStickerSlots[0].enabled = false;
            stickerButtons[0].GetComponent<Image>().raycastTarget = true;
            stickerButtons[0].interactable = true;

            stickerMenuLockedStickerSlots[0].gameObject.SetActive(false);
            stickerMenuStickerButtons[0].interactable = true;
            stickerMenuStickerButtons[0].GetComponent<Image>().raycastTarget = true;
        }
        else if(!lockedStickerSlots[0].enabled)
        {
            lockedStickerSlots[1].gameObject.SetActive(false);
            lockedStickerSlots[1].enabled = false;
            stickerButtons[1].GetComponent<Image>().raycastTarget = true;
            stickerButtons[1].interactable = true;

            stickerMenuLockedStickerSlots[1].gameObject.SetActive(false);
            stickerMenuStickerButtons[1].interactable = true;
            stickerMenuStickerButtons[1].GetComponent<Image>().raycastTarget = true;
        }

        GetMenuButtonNavigations();
    }

    public void UnlockItemSlot()
    {
        playerMenuInfo.currentItemSlotSize++;

        if (lockedItemSlots[0].enabled)
        {
            lockedItemSlots[0].gameObject.SetActive(false);
            lockedItemSlots[0].enabled = false;
        }
        else if (!lockedItemSlots[0].enabled)
        {
            lockedItemSlots[1].gameObject.SetActive(false);
            lockedItemSlots[0].enabled = false;
        }
    }

    public void UnlockHandSize()
    {
        playerMenuInfo.currentHandSize++;
    }

    public void UnlockArmor()
    {
        playerMenuInfo.armorIndex++;
    }

    public void UnlockCard(CardTemplate card)
    {
        CreateCard(card);
    }

    public void UnlockSticker(StickerInformation stickerInfo)
    {
        CreateSticker(stickerInfo);
    }

    private void ResetMenu()
    {
        for(int i = 0; i < menuCanvasGroups.Length; i++)
        {
            if (menuCanvasGroups[i].alpha > 0)
            {
                menuCanvasGroups[i].GetComponent<Animator>().Play("FadeOut", -1, 0);
            }
        }

        for(int j = 0; j < closeMenuButtonImages.Length; j++)
        {
            closeMenuButtonImages[j].raycastTarget = false;
        }

        EnableButtonRaycast();

        menuButtonsCanvasGroup.interactable = true;

        stickerCanvasGroup.interactable = true;

        closeMenuButton.interactable = true;
    }

    public void SetSettingsToggleValues(float backGround, float soundEffects, float cameraSensitivity, int windowed)
    {
        Scene scene = SceneManager.GetActiveScene();

        if (scene.name.Contains("ForestField"))
        {
            if (!worldEnvironmentData.changedDay)
            {
                AudioManager.instance.BackgroundMusic.volume = backGround;
            }
        }
        else
        {
            AudioManager.instance.BackgroundMusic.volume = backGround;
        }
        
        AudioManager.instance.SoundEffects.volume = soundEffects;

        settingsMenu.BackgroundMusicSlider.value = backGround;
        settingsMenu.SoundEffectsSlider.value = soundEffects;
        settingsMenu.CameraSensitivitySlider.value = cameraSensitivity;

        if(!SteamUtils.IsSteamRunningOnSteamDeck())
        {
            if (windowed == 1)
            {
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                Screen.fullScreen = true;
                settingsMenu.ToggleCheckMark.SetActive(true);

                int width = Screen.currentResolution.width;
                int height = Screen.currentResolution.height;

                Screen.SetResolution(width, height, true);
            }
            else
            {
                Screen.fullScreenMode = FullScreenMode.Windowed;
                Screen.fullScreen = false;
                settingsMenu.ToggleCheckMark.SetActive(false);

                Screen.SetResolution(1900, 1080, false);
            }
        }
    }

    public void PlaySoundEffect(AudioSource audioSource)
    {
        if (PlayerPrefs.HasKey("SoundEffects"))
        {
            audioSource.volume = PlayerPrefs.GetFloat("SoundEffects");
        }
        else
        {
            audioSource.volume = 1.0f;
        }

        audioSource.Play();
    }

    public int GetDeckCount()
    {
        return mainCharacterStats.currentCards.Count;
    }

    public int SetDeckCount(int deckCount)
    {
        mainCharacterStats.currentCards = new List<CardTemplate>(new CardTemplate[deckCount]);

        return deckCount;
    }

    public int[] GetDeckCardId()
    {
        int[] cardIds = new int[mainCharacterStats.currentCards.Count];

        for(int i = 0; i < cardIds.Length; i++)
        {
            cardIds[i] = mainCharacterStats.currentCards[i].cardId;
        }

        return cardIds;
    }

    public int[] SetDeckCardId(int[] cardId)
    {
        mainCharacterStats.currentCards.Clear();

        if (deckListContent.childCount > 0)
        {
            foreach (MenuCard card in deckListContent.transform.GetComponentsInChildren<MenuCard>(true))
            {
                Destroy(card.gameObject);
            }
        }

        StartCoroutine(WaitToDestroyDeckCards(cardId));

        return cardId;
    }

    private IEnumerator WaitToDestroyDeckCards(int[] cardId)
    {
        yield return new WaitForSeconds(0.5f);

        CardTemplate[] cardTemplate = Resources.LoadAll<CardTemplate>("Cards");

        playerMenuInfo.currentlyEquippedMysticCards = 0;

        for (int i = 0; i < cardTemplate.Length; i++)
        {
            for (int j = 0; j < cardId.Length; j++)
            {
                if (cardTemplate[i].cardId == cardId[j])
                {
                    mainCharacterStats.currentCards.Add(cardTemplate[i]);

                    if (cardTemplate[i].cardType == CardType.Mystic)
                    {
                        playerMenuInfo.currentlyEquippedMysticCards++;

                        UpdateMysticCardCount();
                    }

                    var card = Instantiate(menuCardForDeckToCreate, deckListContent);

                    card.InDeckList = true;

                    card.gameObject.SetActive(true);

                    card._cardTemplate = cardTemplate[i];

                    card.UpdateCardInformation();

                    ReOrganizeCardIndex();
                }
            }
        }
    }

    public int[] GetCardCollection()
    {
        int[] cardCollectionIds = new int[cardCollection.CardTemplates.Count];

        for (int i = 0; i < cardCollectionIds.Length; i++)
        {
            cardCollectionIds[i] = cardCollection.CardTemplates[i].cardId;
        }

        return cardCollectionIds;
    }

    public int[] SetCardCollection(int[] collection)
    {
        for(int i = 0; i < cardCollection.CardCollectionParents.Length; i++)
        {
            for(int j = 0; j < cardCollection.CardCollectionParents[i].childCount; j++)
            {
                Destroy(cardCollection.CardCollectionParents[i].GetChild(j).gameObject);
            }
        }

        cardCollection.ClearList();

        cardCollection.SetDefaultCardCollectionView();

        CardTemplate[] cardTemplate = Resources.LoadAll<CardTemplate>("Cards");

        for (int i = 0; i < cardTemplate.Length; i++)
        {
            for (int j = 0; j < collection.Length; j++)
            {
                if (cardTemplate[i].cardId == collection[j])
                {
                    cardCollection.LoadCardCollection(cardTemplate[i]);
                }
            }
        }

        return collection;
    }

    private void ReEnableItemList()
    {
        for(int i = 0; i < itemList.childCount; i++)
        {
            itemList.GetChild(i).gameObject.SetActive(true);
        }
    }

    public int[] GetStashCardId()
    {
        int stashCount = 0;

        foreach(MenuCard card in stashContent.GetComponentsInChildren<MenuCard>(true))
        {
            stashCount++;
        }

        int[] stashCardIds = new int[stashCount];

        if(stashCount > 0)
        {
            deckMenu._CardCategories[0].GetComponent<Button>().onClick.Invoke();

            for (int i = 0; i < deckMenu.StashParents.Length; i++)
            {
                for (int j = 0; j < deckMenu.StashParents[i].childCount; j++)
                {
                    if(deckMenu.StashParents[i].childCount > 0)
                    {
                        MenuCard card = deckMenu.StashParents[i].GetChild(j).GetComponent<MenuCard>();

                        stashCardIds[j] = card._cardTemplate.cardId;
                    }
                }
            }
        }

        return stashCardIds;
    }

    public int[] SetStashCardId(int[] stashId)
    {
        foreach (MenuCard card in stashContent.GetComponentsInChildren<MenuCard>(true))
        {
            Destroy(card.gameObject);
        }

        deckMenu.SetDefaultStashParent();

        StartCoroutine(WaitToDestroyStashCards(stashId));

        return stashId;
    }

    private IEnumerator WaitToDestroyStashCards(int[] stashId)
    {
        yield return new WaitForSeconds(0.5f);

        RectTransform stash = cardTypeCategory.AllCardsCategory;

        CardTemplate[] cardTemplate = Resources.LoadAll<CardTemplate>("Cards");

        for (int i = 0; i < cardTemplate.Length; i++)
        {
            for (int j = 0; j < stashId.Length; j++)
            {
                if (cardTemplate[i].cardId == stashId[j])
                {
                    var card = Instantiate(menuCardPrefab, stash);

                    card._cardTemplate = cardTemplate[i];

                    card.gameObject.SetActive(true);

                    card.SetDefaultScrollView();

                    card.UpdateCardInformation();

                    UpdateStashCount();
                }
            }
        }
    }

    public int[] GetEquippedItemId()
    {
        int[] itemCardIds = new int[playerMenuInfo.currentEquippedItems.Count];

        for(int i = 0; i < itemCardIds.Length; i++)
        {
            itemCardIds[i] = playerMenuInfo.currentEquippedItems[i].cardId;
        }

        return itemCardIds;
    }

    public int[] SetEquippedItemId(int[] itemCardId)
    {
        playerMenuInfo.currentEquippedItems.Clear();

        deckMenu.ItemCards.Clear();

        foreach(MenuCard menuCard in itemList.GetComponentsInChildren<MenuCard>())
        {
            Destroy(menuCard.gameObject);
        }

        ReEnableItemList();

        if(itemCardId.Length > 0)
        {
            CardTemplate[] cardTemplate = Resources.LoadAll<CardTemplate>("Cards");

            for(int i = 0; i < cardTemplate.Length; i++)
            {
                for(int j = 0; j < itemCardId.Length; j++)
                {
                    if (cardTemplate[i].cardId == itemCardId[j])
                    {
                        var card = Instantiate(menuCardPrefab, itemList);

                        card._cardTemplate = cardTemplate[i];

                        card._Animator.enabled = true;

                        card.gameObject.SetActive(true);

                        card._propogateDrag.scrollView = cardListScroll;

                        card.UpdateCardInformation();

                        card.EquipItemCard();
                    }
                }
            }
        }

        return itemCardId;
    }

    public int[] GetStickerListId()
    {
        int stickerListCount = 0;

        StickerInformation[] stickerInfo = new StickerInformation[0];

        stickerPage.ResetStickerPage();

        foreach (Sticker s in stickerViewPort.GetComponentsInChildren<Sticker>(true))
        {
            stickerPage.SetDefaultStickerCategory(s);

            stickerListCount++;
        }

        stickerInfo = new StickerInformation[stickerListCount];
        int[] stickerIds = new int[stickerListCount];

        if (stickerListCount > 0)
        {
            for (int i = 0; i < stickerMenu.StickerParents.Length; i++)
            {
                for (int j = 0; j < stickerMenu.StickerParents[i].childCount; j++)
                {
                    Sticker sticker = stickerMenu.StickerParents[i].GetChild(j).GetComponent<Sticker>();

                    stickerInfo[j] = sticker._stickerInformation;
                    stickerIds[j] = sticker._stickerInformation.stickerId;
                }
            }
        }

        return stickerIds;
    }

    public int[] SetStickerListId(int[] stickerId)
    {
        StickerInformation[] stickerInfo = Resources.LoadAll<StickerInformation>("Stickers");

        for (int i = 0; i < stickerInfo.Length; i++)
        {
            for (int j = 0; j < stickerId.Length; j++)
            {
                if (stickerInfo[i].stickerId == stickerId[j])
                {
                    var sticker = Instantiate(stickerPrefab);

                    sticker._stickerInformation = stickerInfo[i];

                    sticker.gameObject.SetActive(true);

                    sticker.SetUpStickerInformation();

                    sticker._StickerPage.SetDefaultStickerCategory(sticker);
                }
            }
        }

        return stickerId;
    }

    public int[] GetEquippedStickers()
    {
        int[] stickerIds = new int[playerMenuInfo.equippedStickers];

        for (int i = 0; i < stickerIds.Length; i++)
        {
            stickerIds[i] = equippedStickers[i]._stickerInformation.stickerId;
        }

        return stickerIds;
    }

    public int[] SetEquippedStickers(int[] stickerId)
    {
        stickerMenu.StickerList.Clear();

        playerMenuInfo.equippedStickers = 0;

        stickerPowerManager.ClearStickerLists();

        equippedStickers.Clear();

        DestroyAllStickers();

        mainCharacterStats.currentPlayerStickerPoints = mainCharacterStats.maximumStickerPoints;

        if(stickerId.Length > 0)
        {
            StickerInformation[] sticker = Resources.LoadAll<StickerInformation>("Stickers");

            for (int i = 0; i < sticker.Length; i++)
            {
                for (int j = 0; j < stickerId.Length; j++)
                {
                    if (sticker[i].stickerId == stickerId[j])
                    {
                        var equippedSticker = Instantiate(stickerPrefab);

                        equippedSticker.gameObject.SetActive(true);

                        equippedSticker._stickerInformation = sticker[i];

                        equippedSticker.SetUpStickerInformation();

                        equippedSticker._StickerPage.SetDefaultStickerCategory(equippedSticker);

                        equippedSticker.EquipSticker();
                    }
                }
            }
        }

        return stickerId;
    }

    public bool GetBattleTutorialOne()
    {
        return NodeManager.instance.HasBattleTutorialOne;
    }

    public bool SetBattleTutorialOne(bool hasTut)
    {
        NodeManager.instance.HasBattleTutorialOne = hasTut;

        return hasTut;
    }

    public bool GetBattleTutorialTwo()
    {
        return NodeManager.instance.HasBattleTutorialTwo;
    }

    public bool SetBattleTutorialTwo(bool hasTut)
    {
        NodeManager.instance.HasBattleTutorialTwo = hasTut;

        return hasTut;
    }

    public bool GetBattleTutorialThree()
    {
        return NodeManager.instance.HasBattleTutorialThree;
    }

    public bool SetBattleTutorialThree(bool hasTut)
    {
        NodeManager.instance.HasBattleTutorialThree = hasTut;

        return hasTut;
    }

    public bool GetWorldMapTutorial()
    {
        return NodeManager.instance.HasWorldMapTutorial;
    }

    public bool SetWorldMapTutorial(bool hasTut)
    {
        NodeManager.instance.HasWorldMapTutorial = hasTut;

        return hasTut;
    }

    public bool GetBasicControlsTutorial()
    {
        return NodeManager.instance.HasWorldMapTutorial;
    }

    public bool SetBasicControlsTutorial(bool hasTut)
    {
        NodeManager.instance.HasBasicControlsTutorial = hasTut;

        return hasTut;
    }

    public bool GetTownTutorial()
    {
        return NodeManager.instance.HasWorldMapTutorial;
    }

    public bool SetTownTutorial(bool hasTut)
    {
        NodeManager.instance.HasTownTutorial = hasTut;

        return hasTut;
    }

    public bool GetSecretAreaTutorial()
    {
        return NodeManager.instance.HasSecretAreaTutorial;
    }

    public bool SetSecretAreaTutorial(bool hasTut)
    {
        NodeManager.instance.HasSecretAreaTutorial = hasTut;

        return hasTut;
    }

    public bool GetCardsTutorial()
    {
        return NodeManager.instance.HasCardsTutorial;
    }

    public bool SetCardsTutorial(bool hasTut)
    {
        NodeManager.instance.HasCardsTutorial = hasTut;

        return hasTut;
    }

    public bool GetStickersTutorial()
    {
        return NodeManager.instance.HasStickersTutorial;
    }

    public bool SetStickersTutorial(bool hasTut)
    {
        NodeManager.instance.HasStickersTutorial = hasTut;

        return hasTut;
    }

    public bool GetForestBossDefeated()
    {
        return bossData.forestBossDefeated;
    }

    public bool SetForestBossDefeated(bool isBossDead)
    {
        bossData.forestBossDefeated = isBossDead;

        return isBossDead;
    }

    public bool GetDesertBossDefeated()
    {
        return bossData.desertBossDefeated;
    }

    public bool SetDesertBossDefeated(bool isBossDead)
    {
        bossData.desertBossDefeated = isBossDead;

        return isBossDead;
    }

    public bool GetArcticBossDefeated()
    {
        return bossData.arcticBossDefeated;
    }

    public bool SetArcticBossDefeated(bool isBossDead)
    {
        bossData.arcticBossDefeated = isBossDead;

        return isBossDead;
    }

    public bool GetGraveBossDefeated()
    {
        return bossData.graveBossDefeated;
    }

    public bool SetGraveBossDefeated(bool isBossDead)
    {
        bossData.graveBossDefeated = isBossDead;

        return isBossDead;
    }

    public bool GetSecretBossForestDefeated()
    {
        return bossData.forestSecretBossDefeated;
    }

    public bool SetSecretBossForestDefeated(bool isDead)
    {
        bossData.forestSecretBossDefeated = isDead;

        return isDead;
    }

    public bool GetSecretBossDesertDefeated()
    {
        return bossData.desertSecretBossDefeated;
    }

    public bool SetSecretBossDesertDefeated(bool isDead)
    {
        bossData.desertSecretBossDefeated = isDead;

        return isDead;
    }

    public bool GetSecretBossArcticDefeated()
    {
        return bossData.arcticSecretBossDefeated;
    }

    public bool SetSecretBossArcticDefeated(bool isDead)
    {
        bossData.arcticSecretBossDefeated = isDead;

        return isDead;
    }

    public bool GetSecretBossGraveDefeated()
    {
        return bossData.graveSecretBossDefeated;
    }

    public bool SetSecretBossGraveDefeated(bool isDead)
    {
        bossData.graveSecretBossDefeated = isDead;

        return isDead;
    }

    public bool GetSecretBossCastleDefeated()
    {
        return bossData.castleSecretBossDefeated;
    }

    public bool SetSecretBossCastleDefeated(bool isDead)
    {
        bossData.castleSecretBossDefeated = isDead;

        return isDead;
    }

    public int GetItemCount()
    {
        return playerMenuInfo.equippedItems;
    }

    public int SetItemCount(int items)
    {
        playerMenuInfo.equippedItems = 0;

        return items;
    }

    public string GetPlayerName()
    {
        return mainCharacterStats.playerName;
    }

    public string SetPlayerName(string playerName)
    {
        mainCharacterStats.playerName = playerName;

        return playerName;
    }

    public int GetLevel()
    {
        return mainCharacterStats.level;
    }

    public int SetLevel(int playerLevel)
    {
        mainCharacterStats.level = playerLevel;

        return playerLevel;
    }

    public int GetCurrentHealth()
    {
        return mainCharacterStats.currentPlayerHealth;
    }

    public int SetCurrentHealth(int currentHealth)
    {
        mainCharacterStats.currentPlayerHealth = currentHealth;

        return currentHealth;
    }

    public int GetMaxHealth()
    {
        return mainCharacterStats.maximumHealth;
    }

    public int SetMaxHealth(int maxHealth)
    {
        mainCharacterStats.maximumHealth = maxHealth;

        return maxHealth;
    }

    public int GetCurrentCardPoints()
    {
        return mainCharacterStats.currentPlayerCardPoints;
    }

    public int SetCurrentCardPoints(int currentCardPoints)
    {
        mainCharacterStats.currentPlayerCardPoints = currentCardPoints;

        return currentCardPoints;
    }

    public int GetMaxCardPoints()
    {
        return mainCharacterStats.maximumCardPoints;
    }

    public int SetMaxCardPoints(int maxCardPoints)
    {
        mainCharacterStats.maximumCardPoints = maxCardPoints;

        return maxCardPoints;
    }

    public int GetCurrentStickerPoints()
    {
        return mainCharacterStats.currentPlayerStickerPoints;
    }

    public int SetCurrentStickerPoints(int currentStickerPoints)
    {
        mainCharacterStats.currentPlayerStickerPoints = currentStickerPoints;

        return currentStickerPoints;
    }

    public int GetMaxStickerPoints()
    {
        return mainCharacterStats.maximumStickerPoints;
    }

    public int SetMaxStickerPoints(int maxStickerPoints)
    {
        mainCharacterStats.maximumStickerPoints = maxStickerPoints;

        return maxStickerPoints;
    }

    public int GetStrength()
    {
        return mainCharacterStats.strength;
    }

    public int SetStrength(int strength)
    {
        mainCharacterStats.strength = strength;

        return strength;
    }

    public int GetDefense()
    {
        return mainCharacterStats.defense;
    }

    public int SetDefense(int defense)
    {
        mainCharacterStats.defense = defense;

        return defense;
    }

    public int GetMoonStones()
    {
        return mainCharacterStats.moonStone;
    }

    public int SetMoonStones(int moonStones)
    {
        mainCharacterStats.moonStone = moonStones;

        return moonStones;
    }

    public float GetMoney()
    {
        return mainCharacterStats.money;
    }

    public float SetMoney(float money)
    {
        mainCharacterStats.money = money;

        return money;
    }

    public float GetCurrentExp()
    {
        return mainCharacterStats.currentExp;
    }

    public float SetCurrentExp(float currentExp)
    {
        mainCharacterStats.currentExp = currentExp;

        return currentExp;
    }

    public float GetNextToLevel()
    {
        return mainCharacterStats.nextExpToLevel;
    }

    public float SetNextToLevel(float nextToLevel)
    {
        mainCharacterStats.nextExpToLevel = nextToLevel;

        return nextToLevel;
    }

    public float GetGuardGauge()
    {
        return mainCharacterStats.guardGauge;
    }

    public float SetGuardGauge(float guardGauge)
    {
        mainCharacterStats.guardGauge = guardGauge;

        return guardGauge;
    }

    public int GetCurrentDeckLimit()
    {
        return playerMenuInfo.currentDeckLimit;
    }

    public int SetCurrentDeckLimit(int deckLimit)
    {
        playerMenuInfo.currentDeckLimit = deckLimit;

        return deckLimit;
    }

    public int GetCurrentHandSize()
    {
        return playerMenuInfo.currentHandSize;
    }

    public int SetCurrentHandSize(int handSize)
    {
        playerMenuInfo.currentHandSize = handSize;

        return handSize;
    }

    public int GetCurrentStickerSlotSize()
    {
        return playerMenuInfo.currentStickerSlotSize;
    }

    public int SetCurrentStickerSlotSize(int stickerSlotSize)
    {
        playerMenuInfo.currentStickerSlotSize = stickerSlotSize;

        if(stickerSlotSize == 4)
        {
            lockedStickerSlots[0].gameObject.SetActive(false);
            lockedStickerSlots[0].enabled = false;
            stickerButtons[0].GetComponent<Image>().raycastTarget = true;
            stickerButtons[0].interactable = true;

            stickerMenuLockedStickerSlots[0].gameObject.SetActive(false);
            stickerMenuStickerButtons[0].interactable = true;
            stickerMenuStickerButtons[0].GetComponent<Image>().raycastTarget = true;
        }

        if(stickerSlotSize == 5)
        {
            lockedStickerSlots[0].gameObject.SetActive(false);
            lockedStickerSlots[0].enabled = false;
            stickerButtons[0].GetComponent<Image>().raycastTarget = true;
            stickerButtons[0].interactable = true;

            stickerMenuLockedStickerSlots[0].gameObject.SetActive(false);
            stickerMenuStickerButtons[0].interactable = true;
            stickerMenuStickerButtons[0].GetComponent<Image>().raycastTarget = true;

            lockedStickerSlots[1].gameObject.SetActive(false);
            lockedStickerSlots[1].enabled = false;
            stickerButtons[1].GetComponent<Image>().raycastTarget = true;
            stickerButtons[1].interactable = true;

            stickerMenuLockedStickerSlots[1].gameObject.SetActive(false);
            stickerMenuStickerButtons[1].interactable = true;
            stickerMenuStickerButtons[1].GetComponent<Image>().raycastTarget = true;
        }

        return stickerSlotSize;
    }

    public int GetCurrentItemSlotSize()
    {
        return playerMenuInfo.currentItemSlotSize;
    }

    public int SetCurrentItemSlotSize(int itemSize)
    {
        playerMenuInfo.currentItemSlotSize = itemSize;

        if(itemSize == 2)
        {
            lockedItemSlots[0].gameObject.SetActive(false);
            lockedItemSlots[0].enabled = false;
            lockedItemSlots[0].transform.parent.gameObject.SetActive(false);
        }

        if(itemSize == 3)
        {
            lockedItemSlots[0].gameObject.SetActive(false);
            lockedItemSlots[0].enabled = false;
            lockedItemSlots[0].transform.parent.gameObject.SetActive(false);

            lockedItemSlots[1].gameObject.SetActive(false);
            lockedItemSlots[1].enabled = false;
            lockedItemSlots[1].transform.parent.gameObject.SetActive(false);
        }

        return itemSize;
    }

    public int GetWeaponIndex()
    {
        return playerMenuInfo.weaponIndex;
    }

    public int SetWeaponIndex(int weaponIndex)
    {
        playerMenuInfo.weaponIndex = weaponIndex;

        return weaponIndex;
    }

    public int GetShieldIndex()
    {
        return playerMenuInfo.shieldIndex;
    }

    public int SetShieldIndex(int shieldIndex)
    {
        playerMenuInfo.shieldIndex = shieldIndex;

        return shieldIndex;
    }

    public int GetArmorIndex()
    {
        return playerMenuInfo.armorIndex;
    }

    public int SetArmorIndex(int armorIndex)
    {
        playerMenuInfo.armorIndex = armorIndex;

        return armorIndex;
    }

    public int GetRankIndex()
    {
        return playerMenuInfo.rankIndex;
    }

    public int SetRankIndex(int rankIndex)
    {
        playerMenuInfo.rankIndex = rankIndex;

        return rankIndex;
    }

    public int GetLevelIndex()
    {
        return playerMenuInfo.levelIndex;
    }

    public int SetLevelIndex(int levelIndex)
    {
        playerMenuInfo.levelIndex = levelIndex;

        return levelIndex;
    }

    public bool GetChangedDay()
    {
        return worldEnvironmentData.changedDay;
    }

    public bool SetChangedDay(bool changedDay)
    {
        worldEnvironmentData.changedDay = changedDay;

        return changedDay;
    }

    public bool GetChangedWeather()
    {
        return worldEnvironmentData.changedWeather;
    }

    public bool SetChangedWeather(bool changedWeather)
    {
        worldEnvironmentData.changedWeather = changedWeather;

        return changedWeather;
    }

    public int GetFountainPowerIndex()
    {
        return fountainData.powerIndex;
    }

    public int SetFountainPowerIndex(int powerIndex)
    {
        fountainData.powerIndex = powerIndex;

        return powerIndex;
    }

    public string GetWorldScene()
    {
        return NodeManager.instance.SceneName;
    }

    public string SetWorldScene(string worldScene)
    {
        NodeManager.instance.SceneName = worldScene;

        return worldScene;
    }

    public int GetWorldIndex()
    {
        return NodeManager.instance.WorldIndex;
    }

    public int SetWorldIndex(int worldIndex)
    {
        NodeManager.instance.WorldIndex = worldIndex;

        return worldIndex;
    }

    public int GetCurrentNodeIndex()
    {
        WorldMapMovement wmm = FindObjectOfType<WorldMapMovement>();

        StageInformation stageInfo = wmm.LevelNodes[wmm.CurrentNodeIndex].GetComponent<StageInformation>();

        if (stageInfo.IsSecretStage)
        {
            if(stageInfo.SecretStageIndex == -1)
            {
                NodeManager.instance.CurrentNodeIndex = -1;
            }
            else if(stageInfo.SecretStageIndex == -2)
            {
                NodeManager.instance.CurrentNodeIndex = -2;
            }
            else
            {
                NodeManager.instance.CurrentNodeIndex = -3;
            }
        }

        return NodeManager.instance.CurrentNodeIndex;
    }

    public int SetCurrentNodeIndex(int currentNodeIndex)
    {
        NodeManager.instance.CurrentNodeIndex = currentNodeIndex;

        return currentNodeIndex;
    }

    public int GetForestStagesUnlocked()
    {
        return NodeManager.instance.ForestStagesUnlocked;
    }

    public int SetForestStagesUnlocked(int stages)
    {
        NodeManager.instance.ForestStagesUnlocked = stages;

        return stages;
    }

    public int GetForestSecretStageOne()
    {
        return NodeManager.instance.ForestSecretStageOne;
    }

    public int SetForestSecretStageOne(int stages)
    {
        NodeManager.instance.ForestSecretStageOne = stages;

        return stages;
    }

    public int GetForestSecretStageTwo()
    {
        return NodeManager.instance.ForestSecretStageTwo;
    }

    public int SetForestSecretStageTwo(int stages)
    {
        NodeManager.instance.ForestSecretStageTwo = stages;

        return stages;
    }

    public int GetForestSecretStageThree()
    {
        return NodeManager.instance.ForestSecretStageThree;
    }

    public int SetForestSecretStageThree(int stages)
    {
        NodeManager.instance.ForestSecretStageThree = stages;

        return stages;
    }

    public int GetDesertStagesUnlocked()
    {
        return NodeManager.instance.DesertStagesUnlocked;
    }

    public int SetDesertStagesUnlocked(int stages)
    {
        NodeManager.instance.DesertStagesUnlocked = stages;

        return stages;
    }

    public int GetDesertSecretStageOne()
    {
        return NodeManager.instance.DesertSecretStageOne;
    }

    public int SetDesertSecretStageOne(int stages)
    {
        NodeManager.instance.DesertSecretStageOne = stages;

        return stages;
    }

    public int GetDesertSecretStageTwo()
    {
        return NodeManager.instance.DesertSecretStageTwo;
    }

    public int SetDesertSecretStageTwo(int stages)
    {
        NodeManager.instance.DesertSecretStageTwo = stages;

        return stages;
    }

    public int GetDesertSecretStageThree()
    {
        return NodeManager.instance.DesertSecretStageThree;
    }

    public int SetDesertSecretStageThree(int stages)
    {
        NodeManager.instance.DesertSecretStageThree = stages;

        return stages;
    }

    public int GetArcticStagesUnlocked()
    {
        return NodeManager.instance.ArcticStagesUnlocked;
    }

    public int SetArcticStagesUnlocked(int stages)
    {
        NodeManager.instance.ArcticStagesUnlocked = stages;

        return stages;
    }

    public int GetArcticSecretStageOne()
    {
        return NodeManager.instance.ArcticSecretStageOne;
    }

    public int SetArcticSecretStageOne(int stages)
    {
        NodeManager.instance.ArcticSecretStageOne = stages;

        return stages;
    }

    public int GetArcticSecretStageTwo()
    {
        return NodeManager.instance.ArcticSecretStageTwo;
    }

    public int SetArcticSecretStageTwo(int stages)
    {
        NodeManager.instance.ArcticSecretStageTwo = stages;

        return stages;
    }

    public int GetArcticSecretStageThree()
    {
        return NodeManager.instance.ArcticSecretStageThree;
    }

    public int SetArcticSecretStageThree(int stages)
    {
        NodeManager.instance.ArcticSecretStageThree = stages;

        return stages;
    }

    public int GetGraveStagesUnlocked()
    {
        return NodeManager.instance.GraveStagesUnlocked;
    }

    public int SetGraveStagesUnlocked(int stages)
    {
        NodeManager.instance.GraveStagesUnlocked = stages;

        return stages;
    }

    public int GetGraveSecretStageOne()
    {
        return NodeManager.instance.GraveyardSecretStageOne;
    }

    public int SetGraveSecretStageOne(int stages)
    {
        NodeManager.instance.GraveyardSecretStageOne = stages;

        return stages;
    }

    public int GetGraveSecretStageTwo()
    {
        return NodeManager.instance.GraveyardSecretStageTwo;
    }

    public int SetGraveSecretStageTwo(int stages)
    {
        NodeManager.instance.GraveyardSecretStageTwo = stages;

        return stages;
    }

    public int GetGraveSecretStageThree()
    {
        return NodeManager.instance.GraveyardSecretStageThree;
    }

    public int SetGraveSecretStageThree(int stages)
    {
        NodeManager.instance.GraveyardSecretStageThree = stages;

        return stages;
    }

    public int GetCastleStagesUnlocked()
    {
        return NodeManager.instance.CastleStagesUnlocked;
    }

    public int SetCastleStagesUnlocked(int stages)
    {
        NodeManager.instance.CastleStagesUnlocked = stages;

        return stages;
    }

    public int GetCastleSecretStageOne()
    {
        return NodeManager.instance.CastleSecretStageOne;
    }

    public int SetCastleSecretStageOne(int stages)
    {
        NodeManager.instance.CastleSecretStageOne = stages;

        return stages;
    }

    public int GetCastleSecretStageTwo()
    {
        return NodeManager.instance.CastleSecretStageTwo;
    }

    public int SetCastleSecretStageTwo(int stages)
    {
        NodeManager.instance.CastleSecretStageTwo = stages;

        return stages;
    }

    public int GetCastleSecretStageThree()
    {
        return NodeManager.instance.CastleSecretStageThree;
    }

    public int SetCastleSecretStageThree(int stages)
    {
        NodeManager.instance.CastleSecretStageThree = stages;

        return stages;
    }

    public int GetUnlockedWorlds()
    {
        return NodeManager.instance.UnlockedWorlds;
    }

    public int SetUnlockedWorlds(int worlds)
    {
        NodeManager.instance.UnlockedWorlds = worlds;

        return worlds;
    }

    public int[] GetWorldOneStageOneTreasures()
    {
        int[] treasures = new int[treasureData.worldTreasures[0].stageTreasures[0].treasures.Length];

        for (int i = 0; i < treasureData.worldTreasures[0].stageTreasures[0].treasures.Length; i++)
        {
            treasures[i] = treasureData.worldTreasures[0].stageTreasures[0].treasures[i];
        }

        return treasures;
    }

    public int[] SetWorldOneStageOneTreasures(int[] treasures)
    {
        for (int i = 0; i < treasureData.worldTreasures[0].stageTreasures[0].treasures.Length; i++)
        {
            treasureData.worldTreasures[0].stageTreasures[0].treasures[i] = treasures[i];
        }

        return treasures;
    }

    public int[] GetWorldOneStageTwoTreasures()
    {
        int[] treasures = new int[treasureData.worldTreasures[0].stageTreasures[1].treasures.Length];

        for (int i = 0; i < treasureData.worldTreasures[0].stageTreasures[1].treasures.Length; i++)
        {
            treasures[i] = treasureData.worldTreasures[0].stageTreasures[1].treasures[i];
        }

        return treasures;
    }

    public int[] SetWorldOneStageTwoTreasures(int[] treasures)
    {
        for (int i = 0; i < treasureData.worldTreasures[0].stageTreasures[1].treasures.Length; i++)
        {
            treasureData.worldTreasures[0].stageTreasures[1].treasures[i] = treasures[i];
        }

        return treasures;
    }

    public int[] GetWorldOneStageThreeTreasures()
    {
        int[] treasures = new int[treasureData.worldTreasures[0].stageTreasures[2].treasures.Length];

        for (int i = 0; i < treasureData.worldTreasures[0].stageTreasures[2].treasures.Length; i++)
        {
            treasures[i] = treasureData.worldTreasures[0].stageTreasures[2].treasures[i];
        }

        return treasures;
    }

    public int[] SetWorldOneStageThreeTreasures(int[] treasures)
    {
        for (int i = 0; i < treasureData.worldTreasures[0].stageTreasures[2].treasures.Length; i++)
        {
            treasureData.worldTreasures[0].stageTreasures[2].treasures[i] = treasures[i];
        }

        return treasures;
    }

    public int[] GetWorldOneStageFourTreasures()
    {
        int[] treasures = new int[treasureData.worldTreasures[0].stageTreasures[3].treasures.Length];

        for (int i = 0; i < treasureData.worldTreasures[0].stageTreasures[3].treasures.Length; i++)
        {
            treasures[i] = treasureData.worldTreasures[0].stageTreasures[3].treasures[i];
        }

        return treasures;
    }

    public int[] SetWorldOneStageFourTreasures(int[] treasures)
    {
        for (int i = 0; i < treasureData.worldTreasures[0].stageTreasures[3].treasures.Length; i++)
        {
            treasureData.worldTreasures[0].stageTreasures[3].treasures[i] = treasures[i];
        }

        return treasures;
    }

    public int[] GetWorldOneStageFiveTreasures()
    {
        int[] treasures = new int[treasureData.worldTreasures[0].stageTreasures[4].treasures.Length];

        for (int i = 0; i < treasureData.worldTreasures[0].stageTreasures[4].treasures.Length; i++)
        {
            treasures[i] = treasureData.worldTreasures[0].stageTreasures[4].treasures[i];
        }

        return treasures;
    }

    public int[] SetWorldOneStageFiveTreasures(int[] treasures)
    {
        for (int i = 0; i < treasureData.worldTreasures[0].stageTreasures[4].treasures.Length; i++)
        {
            treasureData.worldTreasures[0].stageTreasures[4].treasures[i] = treasures[i];
        }

        return treasures;
    }

    public int[] GetWorldTwoStageOneTreasures()
    {
        int[] treasures = new int[treasureData.worldTreasures[1].stageTreasures[0].treasures.Length];

        for (int i = 0; i < treasureData.worldTreasures[1].stageTreasures[0].treasures.Length; i++)
        {
            treasures[i] = treasureData.worldTreasures[1].stageTreasures[0].treasures[i];
        }

        return treasures;
    }

    public int[] SetWorldTwoStageOneTreasures(int[] treasures)
    {
        for (int i = 0; i < treasureData.worldTreasures[1].stageTreasures[0].treasures.Length; i++)
        {
            treasureData.worldTreasures[1].stageTreasures[0].treasures[i] = treasures[i];
        }

        return treasures;
    }

    public int[] GetWorldTwoStageTwoTreasures()
    {
        int[] treasures = new int[treasureData.worldTreasures[1].stageTreasures[1].treasures.Length];

        for (int i = 0; i < treasureData.worldTreasures[1].stageTreasures[1].treasures.Length; i++)
        {
            treasures[i] = treasureData.worldTreasures[1].stageTreasures[1].treasures[i];
        }

        return treasures;
    }

    public int[] SetWorldTwoStageTwoTreasures(int[] treasures)
    {
        for (int i = 0; i < treasureData.worldTreasures[1].stageTreasures[1].treasures.Length; i++)
        {
            treasureData.worldTreasures[1].stageTreasures[1].treasures[i] = treasures[i];
        }

        return treasures;
    }

    public int[] GetWorldTwoStageThreeTreasures()
    {
        int[] treasures = new int[treasureData.worldTreasures[1].stageTreasures[2].treasures.Length];

        for (int i = 0; i < treasureData.worldTreasures[1].stageTreasures[2].treasures.Length; i++)
        {
            treasures[i] = treasureData.worldTreasures[1].stageTreasures[2].treasures[i];
        }

        return treasures;
    }

    public int[] SetWorldTwoStageThreeTreasures(int[] treasures)
    {
        for (int i = 0; i < treasureData.worldTreasures[1].stageTreasures[2].treasures.Length; i++)
        {
            treasureData.worldTreasures[1].stageTreasures[2].treasures[i] = treasures[i];
        }

        return treasures;
    }

    public int[] GetWorldTwoStageFourTreasures()
    {
        int[] treasures = new int[treasureData.worldTreasures[1].stageTreasures[3].treasures.Length];

        for (int i = 0; i < treasureData.worldTreasures[1].stageTreasures[3].treasures.Length; i++)
        {
            treasures[i] = treasureData.worldTreasures[1].stageTreasures[3].treasures[i];
        }

        return treasures;
    }

    public int[] SetWorldTwoStageFourTreasures(int[] treasures)
    {
        for (int i = 0; i < treasureData.worldTreasures[1].stageTreasures[3].treasures.Length; i++)
        {
            treasureData.worldTreasures[1].stageTreasures[3].treasures[i] = treasures[i];
        }

        return treasures;
    }

    public int[] GetWorldTwoStageFiveTreasures()
    {
        int[] treasures = new int[treasureData.worldTreasures[1].stageTreasures[4].treasures.Length];

        for (int i = 0; i < treasureData.worldTreasures[1].stageTreasures[4].treasures.Length; i++)
        {
            treasures[i] = treasureData.worldTreasures[1].stageTreasures[4].treasures[i];
        }

        return treasures;
    }

    public int[] SetWorldTwoStageFiveTreasures(int[] treasures)
    {
        for (int i = 0; i < treasureData.worldTreasures[1].stageTreasures[4].treasures.Length; i++)
        {
            treasureData.worldTreasures[1].stageTreasures[4].treasures[i] = treasures[i];
        }

        return treasures;
    }

    public int[] GetWorldThreeStageOneTreasures()
    {
        int[] treasures = new int[treasureData.worldTreasures[2].stageTreasures[0].treasures.Length];

        for (int i = 0; i < treasureData.worldTreasures[2].stageTreasures[0].treasures.Length; i++)
        {
            treasures[i] = treasureData.worldTreasures[2].stageTreasures[0].treasures[i];
        }

        return treasures;
    }

    public int[] SetWorldThreeStageOneTreasures(int[] treasures)
    {
        for (int i = 0; i < treasureData.worldTreasures[2].stageTreasures[0].treasures.Length; i++)
        {
            treasureData.worldTreasures[2].stageTreasures[0].treasures[i] = treasures[i];
        }

        return treasures;
    }

    public int[] GetWorldThreeStageTwoTreasures()
    {
        int[] treasures = new int[treasureData.worldTreasures[2].stageTreasures[1].treasures.Length];

        for (int i = 0; i < treasureData.worldTreasures[2].stageTreasures[1].treasures.Length; i++)
        {
            treasures[i] = treasureData.worldTreasures[2].stageTreasures[1].treasures[i];
        }

        return treasures;
    }

    public int[] SetWorldThreeStageTwoTreasures(int[] treasures)
    {
        for (int i = 0; i < treasureData.worldTreasures[2].stageTreasures[1].treasures.Length; i++)
        {
            treasureData.worldTreasures[2].stageTreasures[1].treasures[i] = treasures[i];
        }

        return treasures;
    }

    public int[] GetWorldThreeStageThreeTreasures()
    {
        int[] treasures = new int[treasureData.worldTreasures[2].stageTreasures[2].treasures.Length];

        for (int i = 0; i < treasureData.worldTreasures[2].stageTreasures[2].treasures.Length; i++)
        {
            treasures[i] = treasureData.worldTreasures[2].stageTreasures[2].treasures[i];
        }

        return treasures;
    }

    public int[] SetWorldThreeStageThreeTreasures(int[] treasures)
    {
        for (int i = 0; i < treasureData.worldTreasures[2].stageTreasures[2].treasures.Length; i++)
        {
            treasureData.worldTreasures[2].stageTreasures[2].treasures[i] = treasures[i];
        }

        return treasures;
    }

    public int[] GetWorldThreeStageFourTreasures()
    {
        int[] treasures = new int[treasureData.worldTreasures[2].stageTreasures[3].treasures.Length];

        for (int i = 0; i < treasureData.worldTreasures[2].stageTreasures[3].treasures.Length; i++)
        {
            treasures[i] = treasureData.worldTreasures[2].stageTreasures[3].treasures[i];
        }

        return treasures;
    }

    public int[] SetWorldThreeStageFourTreasures(int[] treasures)
    {
        for (int i = 0; i < treasureData.worldTreasures[2].stageTreasures[3].treasures.Length; i++)
        {
            treasureData.worldTreasures[2].stageTreasures[3].treasures[i] = treasures[i];
        }

        return treasures;
    }

    public int[] GetWorldThreeStageFiveTreasures()
    {
        int[] treasures = new int[treasureData.worldTreasures[2].stageTreasures[4].treasures.Length];

        for (int i = 0; i < treasureData.worldTreasures[2].stageTreasures[4].treasures.Length; i++)
        {
            treasures[i] = treasureData.worldTreasures[2].stageTreasures[4].treasures[i];
        }

        return treasures;
    }

    public int[] SetWorldThreeStageFiveTreasures(int[] treasures)
    {
        for (int i = 0; i < treasureData.worldTreasures[2].stageTreasures[4].treasures.Length; i++)
        {
            treasureData.worldTreasures[2].stageTreasures[4].treasures[i] = treasures[i];
        }

        return treasures;
    }

    public int[] GetWorldFourStageOneTreasures()
    {
        int[] treasures = new int[treasureData.worldTreasures[3].stageTreasures[0].treasures.Length];

        for (int i = 0; i < treasureData.worldTreasures[3].stageTreasures[0].treasures.Length; i++)
        {
            treasures[i] = treasureData.worldTreasures[3].stageTreasures[0].treasures[i];
        }

        return treasures;
    }

    public int[] SetWorldFourStageOneTreasures(int[] treasures)
    {
        for (int i = 0; i < treasureData.worldTreasures[3].stageTreasures[0].treasures.Length; i++)
        {
            treasureData.worldTreasures[3].stageTreasures[0].treasures[i] = treasures[i];
        }

        return treasures;
    }

    public int[] GetWorldFourStageTwoTreasures()
    {
        int[] treasures = new int[treasureData.worldTreasures[3].stageTreasures[1].treasures.Length];

        for (int i = 0; i < treasureData.worldTreasures[3].stageTreasures[1].treasures.Length; i++)
        {
            treasures[i] = treasureData.worldTreasures[3].stageTreasures[1].treasures[i];
        }

        return treasures;
    }

    public int[] SetWorldFourStageTwoTreasures(int[] treasures)
    {
        for (int i = 0; i < treasureData.worldTreasures[3].stageTreasures[1].treasures.Length; i++)
        {
            treasureData.worldTreasures[3].stageTreasures[1].treasures[i] = treasures[i];
        }

        return treasures;
    }

    public int[] GetWorldFourStageThreeTreasures()
    {
        int[] treasures = new int[treasureData.worldTreasures[3].stageTreasures[2].treasures.Length];

        for (int i = 0; i < treasureData.worldTreasures[3].stageTreasures[2].treasures.Length; i++)
        {
            treasures[i] = treasureData.worldTreasures[3].stageTreasures[2].treasures[i];
        }

        return treasures;
    }

    public int[] SetWorldFourStageThreeTreasures(int[] treasures)
    {
        for (int i = 0; i < treasureData.worldTreasures[3].stageTreasures[2].treasures.Length; i++)
        {
            treasureData.worldTreasures[3].stageTreasures[2].treasures[i] = treasures[i];
        }

        return treasures;
    }

    public int[] GetWorldFourStageFourTreasures()
    {
        int[] treasures = new int[treasureData.worldTreasures[3].stageTreasures[3].treasures.Length];

        for (int i = 0; i < treasureData.worldTreasures[3].stageTreasures[3].treasures.Length; i++)
        {
            treasures[i] = treasureData.worldTreasures[3].stageTreasures[3].treasures[i];
        }

        return treasures;
    }

    public int[] SetWorldFourStageFourTreasures(int[] treasures)
    {
        for (int i = 0; i < treasureData.worldTreasures[3].stageTreasures[3].treasures.Length; i++)
        {
            treasureData.worldTreasures[3].stageTreasures[3].treasures[i] = treasures[i];
        }

        return treasures;
    }

    public int[] GetWorldFourStageFiveTreasures()
    {
        int[] treasures = new int[treasureData.worldTreasures[3].stageTreasures[4].treasures.Length];

        for (int i = 0; i < treasureData.worldTreasures[3].stageTreasures[4].treasures.Length; i++)
        {
            treasures[i] = treasureData.worldTreasures[3].stageTreasures[4].treasures[i];
        }

        return treasures;
    }

    public int[] SetWorldFourStageFiveTreasures(int[] treasures)
    {
        for (int i = 0; i < treasureData.worldTreasures[3].stageTreasures[4].treasures.Length; i++)
        {
            treasureData.worldTreasures[3].stageTreasures[4].treasures[i] = treasures[i];
        }

        return treasures;
    }

    public int[] GetWorldFiveStageOneTreasures()
    {
        int[] treasures = new int[treasureData.worldTreasures[4].stageTreasures[0].treasures.Length];

        for (int i = 0; i < treasureData.worldTreasures[4].stageTreasures[0].treasures.Length; i++)
        {
            treasures[i] = treasureData.worldTreasures[4].stageTreasures[0].treasures[i];
        }

        return treasures;
    }

    public int[] SetWorldFiveStageOneTreasures(int[] treasures)
    {
        for (int i = 0; i < treasureData.worldTreasures[4].stageTreasures[0].treasures.Length; i++)
        {
            treasureData.worldTreasures[4].stageTreasures[0].treasures[i] = treasures[i];
        }

        return treasures;
    }

    public int[] GetWorldFiveStageTwoTreasures()
    {
        int[] treasures = new int[treasureData.worldTreasures[4].stageTreasures[1].treasures.Length];

        for (int i = 0; i < treasureData.worldTreasures[4].stageTreasures[1].treasures.Length; i++)
        {
            treasures[i] = treasureData.worldTreasures[4].stageTreasures[1].treasures[i];
        }

        return treasures;
    }

    public int[] SetWorldFiveStageTwoTreasures(int[] treasures)
    {
        for (int i = 0; i < treasureData.worldTreasures[4].stageTreasures[1].treasures.Length; i++)
        {
            treasureData.worldTreasures[4].stageTreasures[1].treasures[i] = treasures[i];
        }

        return treasures;
    }

    public int[] GetWorldFiveStageThreeTreasures()
    {
        int[] treasures = new int[treasureData.worldTreasures[4].stageTreasures[2].treasures.Length];

        for (int i = 0; i < treasureData.worldTreasures[4].stageTreasures[2].treasures.Length; i++)
        {
            treasures[i] = treasureData.worldTreasures[4].stageTreasures[2].treasures[i];
        }

        return treasures;
    }

    public int[] SetWorldFiveStageThreeTreasures(int[] treasures)
    {
        for (int i = 0; i < treasureData.worldTreasures[4].stageTreasures[2].treasures.Length; i++)
        {
            treasureData.worldTreasures[4].stageTreasures[2].treasures[i] = treasures[i];
        }

        return treasures;
    }

    public int[] GetWorldFiveStageFourTreasures()
    {
        int[] treasures = new int[treasureData.worldTreasures[4].stageTreasures[3].treasures.Length];

        for (int i = 0; i < treasureData.worldTreasures[4].stageTreasures[3].treasures.Length; i++)
        {
            treasures[i] = treasureData.worldTreasures[4].stageTreasures[3].treasures[i];
        }

        return treasures;
    }

    public int[] SetWorldFiveStageFourTreasures(int[] treasures)
    {
        for (int i = 0; i < treasureData.worldTreasures[4].stageTreasures[3].treasures.Length; i++)
        {
            treasureData.worldTreasures[4].stageTreasures[3].treasures[i] = treasures[i];
        }

        return treasures;
    }

    public int[] GetWorldFiveStageFiveTreasures()
    {
        int[] treasures = new int[treasureData.worldTreasures[4].stageTreasures[4].treasures.Length];

        for (int i = 0; i < treasureData.worldTreasures[4].stageTreasures[4].treasures.Length; i++)
        {
            treasures[i] = treasureData.worldTreasures[4].stageTreasures[4].treasures[i];
        }

        return treasures;
    }

    public int[] SetWorldFiveStageFiveTreasures(int[] treasures)
    {
        for (int i = 0; i < treasureData.worldTreasures[4].stageTreasures[4].treasures.Length; i++)
        {
            treasureData.worldTreasures[4].stageTreasures[4].treasures[i] = treasures[i];
        }

        return treasures;
    }

    public int[] GetWorldOneSecretStageOneTreasures()
    {
        int[] treasures = new int[treasureData.worldTreasures[0].secretStageTreasures[0].treasures.Length];

        for (int i = 0; i < treasureData.worldTreasures[0].secretStageTreasures[0].treasures.Length; i++)
        {
            treasures[i] = treasureData.worldTreasures[0].secretStageTreasures[0].treasures[i];
        }

        return treasures;
    }

    public int[] SetWorldOneSecretStageOneTreasures(int[] treasures)
    {
        for (int i = 0; i < treasureData.worldTreasures[0].secretStageTreasures[0].treasures.Length; i++)
        {
            treasureData.worldTreasures[0].secretStageTreasures[0].treasures[i] = treasures[i];
        }

        return treasures;
    }

    public int[] GetWorldOneSecretStageTwoTreasures()
    {
        int[] treasures = new int[treasureData.worldTreasures[0].secretStageTreasures[1].treasures.Length];

        for (int i = 0; i < treasureData.worldTreasures[0].secretStageTreasures[1].treasures.Length; i++)
        {
            treasures[i] = treasureData.worldTreasures[0].secretStageTreasures[1].treasures[i];
        }

        return treasures;
    }

    public int[] SetWorldOneSecretStageTwoTreasures(int[] treasures)
    {
        for (int i = 0; i < treasureData.worldTreasures[0].secretStageTreasures[1].treasures.Length; i++)
        {
            treasureData.worldTreasures[0].secretStageTreasures[1].treasures[i] = treasures[i];
        }

        return treasures;
    }

    public int[] GetWorldOneSecretStageThreeTreasures()
    {
        int[] treasures = new int[treasureData.worldTreasures[0].secretStageTreasures[2].treasures.Length];

        for (int i = 0; i < treasureData.worldTreasures[0].secretStageTreasures[2].treasures.Length; i++)
        {
            treasures[i] = treasureData.worldTreasures[0].secretStageTreasures[2].treasures[i];
        }

        return treasures;
    }

    public int[] SetWorldOneSecretStageThreeTreasures(int[] treasures)
    {
        for (int i = 0; i < treasureData.worldTreasures[0].secretStageTreasures[2].treasures.Length; i++)
        {
            treasureData.worldTreasures[0].secretStageTreasures[2].treasures[i] = treasures[i];
        }

        return treasures;
    }

    public int[] GetWorldTwoSecretStageOneTreasures()
    {
        int[] treasures = new int[treasureData.worldTreasures[1].secretStageTreasures[0].treasures.Length];

        for (int i = 0; i < treasureData.worldTreasures[1].secretStageTreasures[0].treasures.Length; i++)
        {
            treasures[i] = treasureData.worldTreasures[1].secretStageTreasures[0].treasures[i];
        }

        return treasures;
    }

    public int[] SetWorldTwoSecretStageOneTreasures(int[] treasures)
    {
        for (int i = 0; i < treasureData.worldTreasures[1].secretStageTreasures[0].treasures.Length; i++)
        {
            treasureData.worldTreasures[1].secretStageTreasures[0].treasures[i] = treasures[i];
        }

        return treasures;
    }

    public int[] GetWorldTwoSecretStageTwoTreasures()
    {
        int[] treasures = new int[treasureData.worldTreasures[1].secretStageTreasures[1].treasures.Length];

        for (int i = 0; i < treasureData.worldTreasures[1].secretStageTreasures[1].treasures.Length; i++)
        {
            treasures[i] = treasureData.worldTreasures[1].secretStageTreasures[1].treasures[i];
        }

        return treasures;
    }

    public int[] SetWorldTwoSecretStageTwoTreasures(int[] treasures)
    {
        for (int i = 0; i < treasureData.worldTreasures[1].secretStageTreasures[1].treasures.Length; i++)
        {
            treasureData.worldTreasures[1].secretStageTreasures[1].treasures[i] = treasures[i];
        }

        return treasures;
    }

    public int[] GetWorldTwoSecretStageThreeTreasures()
    {
        int[] treasures = new int[treasureData.worldTreasures[1].secretStageTreasures[2].treasures.Length];

        for (int i = 0; i < treasureData.worldTreasures[1].secretStageTreasures[2].treasures.Length; i++)
        {
            treasures[i] = treasureData.worldTreasures[1].secretStageTreasures[2].treasures[i];
        }

        return treasures;
    }

    public int[] SetWorldTwoSecretStageThreeTreasures(int[] treasures)
    {
        for (int i = 0; i < treasureData.worldTreasures[1].secretStageTreasures[2].treasures.Length; i++)
        {
            treasureData.worldTreasures[1].secretStageTreasures[2].treasures[i] = treasures[i];
        }

        return treasures;
    }

    public int[] GetWorldThreeSecretStageOneTreasures()
    {
        int[] treasures = new int[treasureData.worldTreasures[2].secretStageTreasures[0].treasures.Length];

        for (int i = 0; i < treasureData.worldTreasures[2].secretStageTreasures[0].treasures.Length; i++)
        {
            treasures[i] = treasureData.worldTreasures[2].secretStageTreasures[0].treasures[i];
        }

        return treasures;
    }

    public int[] SetWorldThreeSecretStageOneTreasures(int[] treasures)
    {
        for (int i = 0; i < treasureData.worldTreasures[2].secretStageTreasures[0].treasures.Length; i++)
        {
            treasureData.worldTreasures[2].secretStageTreasures[0].treasures[i] = treasures[i];
        }

        return treasures;
    }

    public int[] GetWorldThreeSecretStageTwoTreasures()
    {
        int[] treasures = new int[treasureData.worldTreasures[2].secretStageTreasures[1].treasures.Length];

        for (int i = 0; i < treasureData.worldTreasures[2].secretStageTreasures[1].treasures.Length; i++)
        {
            treasures[i] = treasureData.worldTreasures[2].secretStageTreasures[1].treasures[i];
        }

        return treasures;
    }

    public int[] SetWorldThreeSecretStageTwoTreasures(int[] treasures)
    {
        for (int i = 0; i < treasureData.worldTreasures[2].secretStageTreasures[1].treasures.Length; i++)
        {
            treasureData.worldTreasures[2].secretStageTreasures[1].treasures[i] = treasures[i];
        }

        return treasures;
    }

    public int[] GetWorldThreeSecretStageThreeTreasures()
    {
        int[] treasures = new int[treasureData.worldTreasures[2].secretStageTreasures[2].treasures.Length];

        for (int i = 0; i < treasureData.worldTreasures[2].secretStageTreasures[2].treasures.Length; i++)
        {
            treasures[i] = treasureData.worldTreasures[2].secretStageTreasures[2].treasures[i];
        }

        return treasures;
    }

    public int[] SetWorldThreeSecretStageThreeTreasures(int[] treasures)
    {
        for (int i = 0; i < treasureData.worldTreasures[2].secretStageTreasures[2].treasures.Length; i++)
        {
            treasureData.worldTreasures[2].secretStageTreasures[2].treasures[i] = treasures[i];
        }

        return treasures;
    }

    public int[] GetWorldFourSecretStageOneTreasures()
    {
        int[] treasures = new int[treasureData.worldTreasures[3].secretStageTreasures[0].treasures.Length];

        for (int i = 0; i < treasureData.worldTreasures[3].secretStageTreasures[0].treasures.Length; i++)
        {
            treasures[i] = treasureData.worldTreasures[3].secretStageTreasures[0].treasures[i];
        }

        return treasures;
    }

    public int[] SetWorldFourSecretStageOneTreasures(int[] treasures)
    {
        for (int i = 0; i < treasureData.worldTreasures[3].secretStageTreasures[0].treasures.Length; i++)
        {
            treasureData.worldTreasures[3].secretStageTreasures[0].treasures[i] = treasures[i];
        }

        return treasures;
    }

    public int[] GetWorldFourSecretStageTwoTreasures()
    {
        int[] treasures = new int[treasureData.worldTreasures[3].secretStageTreasures[1].treasures.Length];

        for (int i = 0; i < treasureData.worldTreasures[3].secretStageTreasures[1].treasures.Length; i++)
        {
            treasures[i] = treasureData.worldTreasures[3].secretStageTreasures[1].treasures[i];
        }

        return treasures;
    }

    public int[] SetWorldFourSecretStageTwoTreasures(int[] treasures)
    {
        for (int i = 0; i < treasureData.worldTreasures[3].secretStageTreasures[1].treasures.Length; i++)
        {
            treasureData.worldTreasures[3].secretStageTreasures[1].treasures[i] = treasures[i];
        }

        return treasures;
    }

    public int[] GetWorldFourSecretStageThreeTreasures()
    {
        int[] treasures = new int[treasureData.worldTreasures[3].secretStageTreasures[2].treasures.Length];

        for (int i = 0; i < treasureData.worldTreasures[3].secretStageTreasures[2].treasures.Length; i++)
        {
            treasures[i] = treasureData.worldTreasures[3].secretStageTreasures[2].treasures[i];
        }

        return treasures;
    }

    public int[] SetWorldFourSecretStageThreeTreasures(int[] treasures)
    {
        for (int i = 0; i < treasureData.worldTreasures[3].secretStageTreasures[2].treasures.Length; i++)
        {
            treasureData.worldTreasures[3].secretStageTreasures[2].treasures[i] = treasures[i];
        }

        return treasures;
    }

    public int[] GetWorldFiveSecretStageOneTreasures()
    {
        int[] treasures = new int[treasureData.worldTreasures[4].secretStageTreasures[0].treasures.Length];

        for (int i = 0; i < treasureData.worldTreasures[4].secretStageTreasures[0].treasures.Length; i++)
        {
            treasures[i] = treasureData.worldTreasures[4].secretStageTreasures[0].treasures[i];
        }

        return treasures;
    }

    public int[] SetWorldFiveSecretStageOneTreasures(int[] treasures)
    {
        for (int i = 0; i < treasureData.worldTreasures[4].secretStageTreasures[0].treasures.Length; i++)
        {
            treasureData.worldTreasures[4].secretStageTreasures[0].treasures[i] = treasures[i];
        }

        return treasures;
    }

    public int[] GetWorldFiveSecretStageTwoTreasures()
    {
        int[] treasures = new int[treasureData.worldTreasures[4].secretStageTreasures[1].treasures.Length];

        for (int i = 0; i < treasureData.worldTreasures[4].secretStageTreasures[1].treasures.Length; i++)
        {
            treasures[i] = treasureData.worldTreasures[4].secretStageTreasures[1].treasures[i];
        }

        return treasures;
    }

    public int[] SetWorldFiveSecretStageTwoTreasures(int[] treasures)
    {
        for (int i = 0; i < treasureData.worldTreasures[4].secretStageTreasures[1].treasures.Length; i++)
        {
            treasureData.worldTreasures[4].secretStageTreasures[1].treasures[i] = treasures[i];
        }

        return treasures;
    }

    public int[] GetWorldFiveSecretStageThreeTreasures()
    {
        int[] treasures = new int[treasureData.worldTreasures[4].secretStageTreasures[2].treasures.Length];

        for (int i = 0; i < treasureData.worldTreasures[4].secretStageTreasures[2].treasures.Length; i++)
        {
            treasures[i] = treasureData.worldTreasures[4].secretStageTreasures[2].treasures[i];
        }

        return treasures;
    }

    public int[] SetWorldFiveSecretStageThreeTreasures(int[] treasures)
    {
        for (int i = 0; i < treasureData.worldTreasures[4].secretStageTreasures[2].treasures.Length; i++)
        {
            treasureData.worldTreasures[4].secretStageTreasures[2].treasures[i] = treasures[i];
        }

        return treasures;
    }

    public int[] GetWorldOneStageOneFieldCards()
    {
        int[] fieldCards = new int[fieldCardData.worldCards[0].stageCards[0].fieldCards.Length];

        for (int i = 0; i < fieldCardData.worldCards[0].stageCards[0].fieldCards.Length; i++)
        {
            fieldCards[i] = fieldCardData.worldCards[0].stageCards[0].fieldCards[i];
        }

        return fieldCards;
    }

    public int[] SetWorldOneStageOneFieldCards(int[] fieldCards)
    {
        for (int i = 0; i < fieldCardData.worldCards[0].stageCards[0].fieldCards.Length; i++)
        {
            fieldCardData.worldCards[0].stageCards[0].fieldCards[i] = fieldCards[i];
        }

        return fieldCards;
    }

    public int[] GetWorldOneStageTwoFieldCards()
    {
        int[] fieldCards = new int[fieldCardData.worldCards[0].stageCards[1].fieldCards.Length];

        for (int i = 0; i < fieldCardData.worldCards[0].stageCards[1].fieldCards.Length; i++)
        {
            fieldCards[i] = fieldCardData.worldCards[0].stageCards[1].fieldCards[i];
        }

        return fieldCards;
    }

    public int[] SetWorldOneStageTwoFieldCards(int[] fieldCards)
    {
        for (int i = 0; i < fieldCardData.worldCards[0].stageCards[1].fieldCards.Length; i++)
        {
            fieldCardData.worldCards[0].stageCards[1].fieldCards[i] = fieldCards[i];
        }

        return fieldCards;
    }

    public int[] GetWorldOneStageThreeFieldCards()
    {
        int[] fieldCards = new int[fieldCardData.worldCards[0].stageCards[2].fieldCards.Length];

        for (int i = 0; i < fieldCardData.worldCards[0].stageCards[2].fieldCards.Length; i++)
        {
            fieldCards[i] = fieldCardData.worldCards[0].stageCards[2].fieldCards[i];
        }

        return fieldCards;
    }

    public int[] SetWorldOneStageThreeFieldCards(int[] fieldCards)
    {
        for (int i = 0; i < fieldCardData.worldCards[0].stageCards[2].fieldCards.Length; i++)
        {
            fieldCardData.worldCards[0].stageCards[2].fieldCards[i] = fieldCards[i];
        }

        return fieldCards;
    }

    public int[] GetWorldOneStageFourFieldCards()
    {
        int[] fieldCards = new int[fieldCardData.worldCards[0].stageCards[3].fieldCards.Length];

        for (int i = 0; i < fieldCardData.worldCards[0].stageCards[3].fieldCards.Length; i++)
        {
            fieldCards[i] = fieldCardData.worldCards[0].stageCards[3].fieldCards[i];
        }

        return fieldCards;
    }

    public int[] SetWorldOneStageFourFieldCards(int[] fieldCards)
    {
        for (int i = 0; i < fieldCardData.worldCards[0].stageCards[3].fieldCards.Length; i++)
        {
            fieldCardData.worldCards[0].stageCards[3].fieldCards[i] = fieldCards[i];
        }

        return fieldCards;
    }

    public int[] GetWorldOneStageFiveFieldCards()
    {
        int[] fieldCards = new int[fieldCardData.worldCards[0].stageCards[4].fieldCards.Length];

        for (int i = 0; i < fieldCardData.worldCards[0].stageCards[4].fieldCards.Length; i++)
        {
            fieldCards[i] = fieldCardData.worldCards[0].stageCards[4].fieldCards[i];
        }

        return fieldCards;
    }

    public int[] SetWorldOneStageFiveFieldCards(int[] fieldCards)
    {
        for (int i = 0; i < fieldCardData.worldCards[0].stageCards[4].fieldCards.Length; i++)
        {
            fieldCardData.worldCards[0].stageCards[4].fieldCards[i] = fieldCards[i];
        }

        return fieldCards;
    }

    public int[] GetWorldOneSecretStageOneFieldCards()
    {
        int[] fieldCards = new int[fieldCardData.worldCards[0].secretStageCards[0].fieldCards.Length];

        for (int i = 0; i < fieldCardData.worldCards[0].secretStageCards[0].fieldCards.Length; i++)
        {
            fieldCards[i] = fieldCardData.worldCards[0].secretStageCards[0].fieldCards[i];
        }

        return fieldCards;
    }

    public int[] SetWorldOneSecretStageOneFieldCards(int[] fieldCards)
    {
        for (int i = 0; i < fieldCardData.worldCards[0].secretStageCards[0].fieldCards.Length; i++)
        {
            fieldCardData.worldCards[0].secretStageCards[0].fieldCards[i] = fieldCards[i];
        }

        return fieldCards;
    }

    public int[] GetWorldOneSecretStageTwoFieldCards()
    {
        int[] fieldCards = new int[fieldCardData.worldCards[0].secretStageCards[1].fieldCards.Length];

        for (int i = 0; i < fieldCardData.worldCards[0].secretStageCards[1].fieldCards.Length; i++)
        {
            fieldCards[i] = fieldCardData.worldCards[0].secretStageCards[1].fieldCards[i];
        }

        return fieldCards;
    }

    public int[] SetWorldOneSecretStageTwoFieldCards(int[] fieldCards)
    {
        for (int i = 0; i < fieldCardData.worldCards[0].secretStageCards[1].fieldCards.Length; i++)
        {
            fieldCardData.worldCards[0].secretStageCards[1].fieldCards[i] = fieldCards[i];
        }

        return fieldCards;
    }

    public int[] GetWorldTwoStageOneFieldCards()
    {
        int[] fieldCards = new int[fieldCardData.worldCards[1].stageCards[0].fieldCards.Length];

        for (int i = 0; i < fieldCardData.worldCards[1].stageCards[0].fieldCards.Length; i++)
        {
            fieldCards[i] = fieldCardData.worldCards[1].stageCards[0].fieldCards[i];
        }

        return fieldCards;
    }

    public int[] SetWorldTwoStageOneFieldCards(int[] fieldCards)
    {
        for (int i = 0; i < fieldCardData.worldCards[1].stageCards[0].fieldCards.Length; i++)
        {
            fieldCardData.worldCards[1].stageCards[0].fieldCards[i] = fieldCards[i];
        }

        return fieldCards;
    }

    public int[] GetWorldTwoStageTwoFieldCards()
    {
        int[] fieldCards = new int[fieldCardData.worldCards[1].stageCards[1].fieldCards.Length];

        for (int i = 0; i < fieldCardData.worldCards[1].stageCards[1].fieldCards.Length; i++)
        {
            fieldCards[i] = fieldCardData.worldCards[1].stageCards[1].fieldCards[i];
        }

        return fieldCards;
    }

    public int[] SetWorldTwoStageTwoFieldCards(int[] fieldCards)
    {
        for (int i = 0; i < fieldCardData.worldCards[1].stageCards[1].fieldCards.Length; i++)
        {
            fieldCardData.worldCards[1].stageCards[1].fieldCards[i] = fieldCards[i];
        }

        return fieldCards;
    }

    public int[] GetWorldTwoStageThreeFieldCards()
    {
        int[] fieldCards = new int[fieldCardData.worldCards[1].stageCards[2].fieldCards.Length];

        for (int i = 0; i < fieldCardData.worldCards[1].stageCards[2].fieldCards.Length; i++)
        {
            fieldCards[i] = fieldCardData.worldCards[1].stageCards[2].fieldCards[i];
        }

        return fieldCards;
    }

    public int[] SetWorldTwoStageThreeFieldCards(int[] fieldCards)
    {
        for (int i = 0; i < fieldCardData.worldCards[1].stageCards[2].fieldCards.Length; i++)
        {
            fieldCardData.worldCards[1].stageCards[2].fieldCards[i] = fieldCards[i];
        }

        return fieldCards;
    }

    public int[] GetWorldTwoStageFourFieldCards()
    {
        int[] fieldCards = new int[fieldCardData.worldCards[1].stageCards[3].fieldCards.Length];

        for (int i = 0; i < fieldCardData.worldCards[1].stageCards[3].fieldCards.Length; i++)
        {
            fieldCards[i] = fieldCardData.worldCards[1].stageCards[3].fieldCards[i];
        }

        return fieldCards;
    }

    public int[] SetWorldTwoStageFourFieldCards(int[] fieldCards)
    {
        for (int i = 0; i < fieldCardData.worldCards[1].stageCards[3].fieldCards.Length; i++)
        {
            fieldCardData.worldCards[1].stageCards[3].fieldCards[i] = fieldCards[i];
        }

        return fieldCards;
    }

    public int[] GetWorldTwoStageFiveFieldCards()
    {
        int[] fieldCards = new int[fieldCardData.worldCards[1].stageCards[4].fieldCards.Length];

        for (int i = 0; i < fieldCardData.worldCards[1].stageCards[4].fieldCards.Length; i++)
        {
            fieldCards[i] = fieldCardData.worldCards[1].stageCards[4].fieldCards[i];
        }

        return fieldCards;
    }

    public int[] SetWorldTwoStageFiveFieldCards(int[] fieldCards)
    {
        for (int i = 0; i < fieldCardData.worldCards[1].stageCards[4].fieldCards.Length; i++)
        {
            fieldCardData.worldCards[1].stageCards[4].fieldCards[i] = fieldCards[i];
        }

        return fieldCards;
    }

    public int[] GetWorldTwoSecretStageOneFieldCards()
    {
        int[] fieldCards = new int[fieldCardData.worldCards[1].secretStageCards[0].fieldCards.Length];

        for (int i = 0; i < fieldCardData.worldCards[1].secretStageCards[0].fieldCards.Length; i++)
        {
            fieldCards[i] = fieldCardData.worldCards[1].secretStageCards[0].fieldCards[i];
        }

        return fieldCards;
    }

    public int[] SetWorldTwoSecretStageOneFieldCards(int[] fieldCards)
    {
        for (int i = 0; i < fieldCardData.worldCards[1].secretStageCards[0].fieldCards.Length; i++)
        {
            fieldCardData.worldCards[1].secretStageCards[0].fieldCards[i] = fieldCards[i];
        }

        return fieldCards;
    }

    public int[] GetWorldTwoSecretStageThreeFieldCards()
    {
        int[] fieldCards = new int[fieldCardData.worldCards[1].secretStageCards[2].fieldCards.Length];

        for (int i = 0; i < fieldCardData.worldCards[1].secretStageCards[2].fieldCards.Length; i++)
        {
            fieldCards[i] = fieldCardData.worldCards[1].secretStageCards[2].fieldCards[i];
        }

        return fieldCards;
    }

    public int[] SetWorldTwoSecretStageThreeFieldCards(int[] fieldCards)
    {
        for (int i = 0; i < fieldCardData.worldCards[1].secretStageCards[2].fieldCards.Length; i++)
        {
            fieldCardData.worldCards[1].secretStageCards[2].fieldCards[i] = fieldCards[i];
        }

        return fieldCards;
    }

    public int[] GetWorldThreeStageOneFieldCards()
    {
        int[] fieldCards = new int[fieldCardData.worldCards[2].stageCards[0].fieldCards.Length];

        for (int i = 0; i < fieldCardData.worldCards[2].stageCards[0].fieldCards.Length; i++)
        {
            fieldCards[i] = fieldCardData.worldCards[2].stageCards[0].fieldCards[i];
        }

        return fieldCards;
    }

    public int[] SetWorldThreeStageOneFieldCards(int[] fieldCards)
    {
        for (int i = 0; i < fieldCardData.worldCards[2].stageCards[0].fieldCards.Length; i++)
        {
            fieldCardData.worldCards[2].stageCards[0].fieldCards[i] = fieldCards[i];
        }

        return fieldCards;
    }

    public int[] GetWorldThreeStageTwoFieldCards()
    {
        int[] fieldCards = new int[fieldCardData.worldCards[2].stageCards[1].fieldCards.Length];

        for (int i = 0; i < fieldCardData.worldCards[2].stageCards[1].fieldCards.Length; i++)
        {
            fieldCards[i] = fieldCardData.worldCards[2].stageCards[1].fieldCards[i];
        }

        return fieldCards;
    }

    public int[] SetWorldThreeStageTwoFieldCards(int[] fieldCards)
    {
        for (int i = 0; i < fieldCardData.worldCards[2].stageCards[1].fieldCards.Length; i++)
        {
            fieldCardData.worldCards[2].stageCards[1].fieldCards[i] = fieldCards[i];
        }

        return fieldCards;
    }

    public int[] GetWorldThreeStageThreeFieldCards()
    {
        int[] fieldCards = new int[fieldCardData.worldCards[2].stageCards[2].fieldCards.Length];

        for (int i = 0; i < fieldCardData.worldCards[2].stageCards[2].fieldCards.Length; i++)
        {
            fieldCards[i] = fieldCardData.worldCards[2].stageCards[2].fieldCards[i];
        }

        return fieldCards;
    }

    public int[] SetWorldThreeStageThreeFieldCards(int[] fieldCards)
    {
        for (int i = 0; i < fieldCardData.worldCards[2].stageCards[2].fieldCards.Length; i++)
        {
            fieldCardData.worldCards[2].stageCards[2].fieldCards[i] = fieldCards[i];
        }

        return fieldCards;
    }

    public int[] GetWorldThreeStageFourFieldCards()
    {
        int[] fieldCards = new int[fieldCardData.worldCards[2].stageCards[3].fieldCards.Length];

        for (int i = 0; i < fieldCardData.worldCards[2].stageCards[3].fieldCards.Length; i++)
        {
            fieldCards[i] = fieldCardData.worldCards[2].stageCards[3].fieldCards[i];
        }

        return fieldCards;
    }

    public int[] SetWorldThreeStageFourFieldCards(int[] fieldCards)
    {
        for (int i = 0; i < fieldCardData.worldCards[2].stageCards[3].fieldCards.Length; i++)
        {
            fieldCardData.worldCards[2].stageCards[3].fieldCards[i] = fieldCards[i];
        }

        return fieldCards;
    }

    public int[] GetWorldThreeStageFiveFieldCards()
    {
        int[] fieldCards = new int[fieldCardData.worldCards[2].stageCards[4].fieldCards.Length];

        for (int i = 0; i < fieldCardData.worldCards[2].stageCards[4].fieldCards.Length; i++)
        {
            fieldCards[i] = fieldCardData.worldCards[2].stageCards[4].fieldCards[i];
        }

        return fieldCards;
    }

    public int[] SetWorldThreeStageFiveFieldCards(int[] fieldCards)
    {
        for (int i = 0; i < fieldCardData.worldCards[2].stageCards[4].fieldCards.Length; i++)
        {
            fieldCardData.worldCards[2].stageCards[4].fieldCards[i] = fieldCards[i];
        }

        return fieldCards;
    }

    public int[] GetWorldThreeSecretStageOneFieldCards()
    {
        int[] fieldCards = new int[fieldCardData.worldCards[2].secretStageCards[0].fieldCards.Length];

        for (int i = 0; i < fieldCardData.worldCards[2].secretStageCards[0].fieldCards.Length; i++)
        {
            fieldCards[i] = fieldCardData.worldCards[2].secretStageCards[0].fieldCards[i];
        }

        return fieldCards;
    }

    public int[] SetWorldThreeSecretStageOneFieldCards(int[] fieldCards)
    {
        for (int i = 0; i < fieldCardData.worldCards[2].secretStageCards[0].fieldCards.Length; i++)
        {
            fieldCardData.worldCards[2].secretStageCards[0].fieldCards[i] = fieldCards[i];
        }

        return fieldCards;
    }

    public int[] GetWorldThreeSecretStageTwoFieldCards()
    {
        int[] fieldCards = new int[fieldCardData.worldCards[2].secretStageCards[1].fieldCards.Length];

        for (int i = 0; i < fieldCardData.worldCards[2].secretStageCards[1].fieldCards.Length; i++)
        {
            fieldCards[i] = fieldCardData.worldCards[2].secretStageCards[1].fieldCards[i];
        }

        return fieldCards;
    }

    public int[] SetWorldThreeSecretStageTwoFieldCards(int[] fieldCards)
    {
        for (int i = 0; i < fieldCardData.worldCards[2].secretStageCards[1].fieldCards.Length; i++)
        {
            fieldCardData.worldCards[2].secretStageCards[1].fieldCards[i] = fieldCards[i];
        }

        return fieldCards;
    }

    public int[] GetWorldFourStageOneFieldCards()
    {
        int[] fieldCards = new int[fieldCardData.worldCards[3].stageCards[0].fieldCards.Length];

        for (int i = 0; i < fieldCardData.worldCards[3].stageCards[0].fieldCards.Length; i++)
        {
            fieldCards[i] = fieldCardData.worldCards[3].stageCards[0].fieldCards[i];
        }

        return fieldCards;
    }

    public int[] SetWorldFourStageOneFieldCards(int[] fieldCards)
    {
        for (int i = 0; i < fieldCardData.worldCards[3].stageCards[0].fieldCards.Length; i++)
        {
            fieldCardData.worldCards[3].stageCards[0].fieldCards[i] = fieldCards[i];
        }

        return fieldCards;
    }

    public int[] GetWorldFourStageTwoFieldCards()
    {
        int[] fieldCards = new int[fieldCardData.worldCards[3].stageCards[1].fieldCards.Length];

        for (int i = 0; i < fieldCardData.worldCards[3].stageCards[1].fieldCards.Length; i++)
        {
            fieldCards[i] = fieldCardData.worldCards[3].stageCards[1].fieldCards[i];
        }

        return fieldCards;
    }

    public int[] SetWorldFourStageTwoFieldCards(int[] fieldCards)
    {
        for (int i = 0; i < fieldCardData.worldCards[3].stageCards[1].fieldCards.Length; i++)
        {
            fieldCardData.worldCards[3].stageCards[1].fieldCards[i] = fieldCards[i];
        }

        return fieldCards;
    }

    public int[] GetWorldFourStageThreeFieldCards()
    {
        int[] fieldCards = new int[fieldCardData.worldCards[3].stageCards[2].fieldCards.Length];

        for (int i = 0; i < fieldCardData.worldCards[3].stageCards[2].fieldCards.Length; i++)
        {
            fieldCards[i] = fieldCardData.worldCards[3].stageCards[2].fieldCards[i];
        }

        return fieldCards;
    }

    public int[] SetWorldFourStageThreeFieldCards(int[] fieldCards)
    {
        for (int i = 0; i < fieldCardData.worldCards[3].stageCards[2].fieldCards.Length; i++)
        {
            fieldCardData.worldCards[3].stageCards[2].fieldCards[i] = fieldCards[i];
        }

        return fieldCards;
    }

    public int[] GetWorldFourStageFourFieldCards()
    {
        int[] fieldCards = new int[fieldCardData.worldCards[3].stageCards[3].fieldCards.Length];

        for (int i = 0; i < fieldCardData.worldCards[3].stageCards[3].fieldCards.Length; i++)
        {
            fieldCards[i] = fieldCardData.worldCards[3].stageCards[3].fieldCards[i];
        }

        return fieldCards;
    }

    public int[] SetWorldFourStageFourFieldCards(int[] fieldCards)
    {
        for (int i = 0; i < fieldCardData.worldCards[3].stageCards[3].fieldCards.Length; i++)
        {
            fieldCardData.worldCards[3].stageCards[3].fieldCards[i] = fieldCards[i];
        }

        return fieldCards;
    }

    public int[] GetWorldFourStageFiveFieldCards()
    {
        int[] fieldCards = new int[fieldCardData.worldCards[3].stageCards[4].fieldCards.Length];

        for (int i = 0; i < fieldCardData.worldCards[3].stageCards[4].fieldCards.Length; i++)
        {
            fieldCards[i] = fieldCardData.worldCards[3].stageCards[4].fieldCards[i];
        }

        return fieldCards;
    }

    public int[] SetWorldFourStageFiveFieldCards(int[] fieldCards)
    {
        for (int i = 0; i < fieldCardData.worldCards[3].stageCards[4].fieldCards.Length; i++)
        {
            fieldCardData.worldCards[3].stageCards[4].fieldCards[i] = fieldCards[i];
        }

        return fieldCards;
    }

    public int[] GetWorldFourSecretStageOneFieldCards()
    {
        int[] fieldCards = new int[fieldCardData.worldCards[3].secretStageCards[0].fieldCards.Length];

        for (int i = 0; i < fieldCardData.worldCards[3].secretStageCards[0].fieldCards.Length; i++)
        {
            fieldCards[i] = fieldCardData.worldCards[3].secretStageCards[0].fieldCards[i];
        }

        return fieldCards;
    }

    public int[] SetWorldFourSecretStageOneFieldCards(int[] fieldCards)
    {
        for (int i = 0; i < fieldCardData.worldCards[3].secretStageCards[0].fieldCards.Length; i++)
        {
            fieldCardData.worldCards[3].secretStageCards[0].fieldCards[i] = fieldCards[i];
        }

        return fieldCards;
    }

    public int[] GetWorldFourSecretStageTwoFieldCards()
    {
        int[] fieldCards = new int[fieldCardData.worldCards[3].secretStageCards[1].fieldCards.Length];

        for (int i = 0; i < fieldCardData.worldCards[3].secretStageCards[1].fieldCards.Length; i++)
        {
            fieldCards[i] = fieldCardData.worldCards[3].secretStageCards[1].fieldCards[i];
        }

        return fieldCards;
    }

    public int[] SetWorldFourSecretStageTwoFieldCards(int[] fieldCards)
    {
        for (int i = 0; i < fieldCardData.worldCards[3].secretStageCards[1].fieldCards.Length; i++)
        {
            fieldCardData.worldCards[3].secretStageCards[1].fieldCards[i] = fieldCards[i];
        }

        return fieldCards;
    }

    public int[] GetWorldFiveStageOneFieldCards()
    {
        int[] fieldCards = new int[fieldCardData.worldCards[4].stageCards[0].fieldCards.Length];

        for (int i = 0; i < fieldCardData.worldCards[4].stageCards[0].fieldCards.Length; i++)
        {
            fieldCards[i] = fieldCardData.worldCards[4].stageCards[0].fieldCards[i];
        }

        return fieldCards;
    }

    public int[] SetWorldFiveStageOneFieldCards(int[] fieldCards)
    {
        for (int i = 0; i < fieldCardData.worldCards[4].stageCards[0].fieldCards.Length; i++)
        {
            fieldCardData.worldCards[4].stageCards[0].fieldCards[i] = fieldCards[i];
        }

        return fieldCards;
    }

    public int[] GetWorldFiveStageTwoFieldCards()
    {
        int[] fieldCards = new int[fieldCardData.worldCards[4].stageCards[1].fieldCards.Length];

        for (int i = 0; i < fieldCardData.worldCards[4].stageCards[1].fieldCards.Length; i++)
        {
            fieldCards[i] = fieldCardData.worldCards[4].stageCards[1].fieldCards[i];
        }

        return fieldCards;
    }

    public int[] SetWorldFiveStageTwoFieldCards(int[] fieldCards)
    {
        for (int i = 0; i < fieldCardData.worldCards[4].stageCards[1].fieldCards.Length; i++)
        {
            fieldCardData.worldCards[4].stageCards[1].fieldCards[i] = fieldCards[i];
        }

        return fieldCards;
    }

    public int[] GetWorldFiveStageThreeFieldCards()
    {
        int[] fieldCards = new int[fieldCardData.worldCards[4].stageCards[2].fieldCards.Length];

        for (int i = 0; i < fieldCardData.worldCards[4].stageCards[2].fieldCards.Length; i++)
        {
            fieldCards[i] = fieldCardData.worldCards[4].stageCards[2].fieldCards[i];
        }

        return fieldCards;
    }

    public int[] SetWorldFiveStageThreeFieldCards(int[] fieldCards)
    {
        for (int i = 0; i < fieldCardData.worldCards[4].stageCards[2].fieldCards.Length; i++)
        {
            fieldCardData.worldCards[4].stageCards[2].fieldCards[i] = fieldCards[i];
        }

        return fieldCards;
    }

    public int[] GetWorldFiveStageFourFieldCards()
    {
        int[] fieldCards = new int[fieldCardData.worldCards[4].stageCards[3].fieldCards.Length];

        for (int i = 0; i < fieldCardData.worldCards[4].stageCards[3].fieldCards.Length; i++)
        {
            fieldCards[i] = fieldCardData.worldCards[4].stageCards[3].fieldCards[i];
        }

        return fieldCards;
    }

    public int[] SetWorldFiveStageFourFieldCards(int[] fieldCards)
    {
        for (int i = 0; i < fieldCardData.worldCards[4].stageCards[3].fieldCards.Length; i++)
        {
            fieldCardData.worldCards[4].stageCards[3].fieldCards[i] = fieldCards[i];
        }

        return fieldCards;
    }

    public int[] GetWorldFiveStageFiveFieldCards()
    {
        int[] fieldCards = new int[fieldCardData.worldCards[4].stageCards[4].fieldCards.Length];

        for (int i = 0; i < fieldCardData.worldCards[4].stageCards[4].fieldCards.Length; i++)
        {
            fieldCards[i] = fieldCardData.worldCards[4].stageCards[4].fieldCards[i];
        }

        return fieldCards;
    }

    public int[] SetWorldFiveStageFiveFieldCards(int[] fieldCards)
    {
        for (int i = 0; i < fieldCardData.worldCards[4].stageCards[4].fieldCards.Length; i++)
        {
            fieldCardData.worldCards[4].stageCards[4].fieldCards[i] = fieldCards[i];
        }

        return fieldCards;
    }

    public int[] GetWorldFiveSecretStageOneFieldCards()
    {
        int[] fieldCards = new int[fieldCardData.worldCards[4].secretStageCards[0].fieldCards.Length];

        for (int i = 0; i < fieldCardData.worldCards[4].secretStageCards[0].fieldCards.Length; i++)
        {
            fieldCards[i] = fieldCardData.worldCards[4].secretStageCards[0].fieldCards[i];
        }

        return fieldCards;
    }

    public int[] SetWorldFiveSecretStageOneFieldCards(int[] fieldCards)
    {
        for (int i = 0; i < fieldCardData.worldCards[4].secretStageCards[0].fieldCards.Length; i++)
        {
            fieldCardData.worldCards[4].secretStageCards[0].fieldCards[i] = fieldCards[i];
        }

        return fieldCards;
    }

    public int[] GetWorldFiveSecretStageTwoFieldCards()
    {
        int[] fieldCards = new int[fieldCardData.worldCards[4].secretStageCards[1].fieldCards.Length];

        for (int i = 0; i < fieldCardData.worldCards[4].secretStageCards[1].fieldCards.Length; i++)
        {
            fieldCards[i] = fieldCardData.worldCards[4].secretStageCards[1].fieldCards[i];
        }

        return fieldCards;
    }

    public int[] SetWorldFiveSecretStageTwoFieldCards(int[] fieldCards)
    {
        for (int i = 0; i < fieldCardData.worldCards[4].secretStageCards[1].fieldCards.Length; i++)
        {
            fieldCardData.worldCards[4].secretStageCards[1].fieldCards[i] = fieldCards[i];
        }

        return fieldCards;
    }

    public int[] GetWorldOneStageTwoMoonstones()
    {
        int[] moonStones = new int[moonStoneData.worldMoonStones[0].stageMoonstones[0].moonStones.Length];

        for (int i = 0; i < moonStoneData.worldMoonStones[0].stageMoonstones[0].moonStones.Length; i++)
        {
            moonStones[i] = moonStoneData.worldMoonStones[0].stageMoonstones[0].moonStones[i];
        }

        return moonStones;
    }

    public int[] SetWorldOneStageTwoMoonstones(int[] stones)
    {
        for (int i = 0; i < moonStoneData.worldMoonStones[0].stageMoonstones[0].moonStones.Length; i++)
        {
            moonStoneData.worldMoonStones[0].stageMoonstones[0].moonStones[i] = stones[i];
        }

        return stones;
    }

    public int[] GetWorldOneStageThreeMoonstones()
    {
        int[] moonStones = new int[moonStoneData.worldMoonStones[0].stageMoonstones[1].moonStones.Length];

        for (int i = 0; i < moonStoneData.worldMoonStones[0].stageMoonstones[1].moonStones.Length; i++)
        {
            moonStones[i] = moonStoneData.worldMoonStones[0].stageMoonstones[1].moonStones[i];
        }

        return moonStones;
    }

    public int[] SetWorldOneStageThreeMoonstones(int[] stones)
    {
        for (int i = 0; i < moonStoneData.worldMoonStones[0].stageMoonstones[1].moonStones.Length; i++)
        {
            moonStoneData.worldMoonStones[0].stageMoonstones[1].moonStones[i] = stones[i];
        }

        return stones;
    }

    public int[] GetWorldOneStageFourMoonstones()
    {
        int[] moonStones = new int[moonStoneData.worldMoonStones[0].stageMoonstones[2].moonStones.Length];

        for (int i = 0; i < moonStoneData.worldMoonStones[0].stageMoonstones[2].moonStones.Length; i++)
        {
            moonStones[i] = moonStoneData.worldMoonStones[0].stageMoonstones[2].moonStones[i];
        }

        return moonStones;
    }

    public int[] SetWorldOneStageFourMoonstones(int[] stones)
    {
        for (int i = 0; i < moonStoneData.worldMoonStones[0].stageMoonstones[2].moonStones.Length; i++)
        {
            moonStoneData.worldMoonStones[0].stageMoonstones[2].moonStones[i] = stones[i];
        }

        return stones;
    }

    public int[] GetWorldOneStageFiveMoonstones()
    {
        int[] moonStones = new int[moonStoneData.worldMoonStones[0].stageMoonstones[3].moonStones.Length];

        for (int i = 0; i < moonStoneData.worldMoonStones[0].stageMoonstones[3].moonStones.Length; i++)
        {
            moonStones[i] = moonStoneData.worldMoonStones[0].stageMoonstones[3].moonStones[i];
        }

        return moonStones;
    }

    public int[] SetWorldOneStageFiveMoonstones(int[] stones)
    {
        for (int i = 0; i < moonStoneData.worldMoonStones[0].stageMoonstones[3].moonStones.Length; i++)
        {
            moonStoneData.worldMoonStones[0].stageMoonstones[3].moonStones[i] = stones[i];
        }

        return stones;
    }

    public int[] GetWorldTwoStageOneMoonstones()
    {
        int[] moonStones = new int[moonStoneData.worldMoonStones[1].stageMoonstones[0].moonStones.Length];

        for (int i = 0; i < moonStoneData.worldMoonStones[1].stageMoonstones[0].moonStones.Length; i++)
        {
            moonStones[i] = moonStoneData.worldMoonStones[1].stageMoonstones[0].moonStones[i];
        }

        return moonStones;
    }

    public int[] SetWorldTwoStageOneMoonstones(int[] stones)
    {
        for (int i = 0; i < moonStoneData.worldMoonStones[1].stageMoonstones[0].moonStones.Length; i++)
        {
            moonStoneData.worldMoonStones[1].stageMoonstones[0].moonStones[i] = stones[i];
        }

        return stones;
    }

    public int[] GetWorldTwoStageTwoMoonstones()
    {
        int[] moonStones = new int[moonStoneData.worldMoonStones[1].stageMoonstones[1].moonStones.Length];

        for (int i = 0; i < moonStoneData.worldMoonStones[1].stageMoonstones[1].moonStones.Length; i++)
        {
            moonStones[i] = moonStoneData.worldMoonStones[1].stageMoonstones[1].moonStones[i];
        }

        return moonStones;
    }

    public int[] SetWorldTwoStageTwoMoonstones(int[] stones)
    {
        for (int i = 0; i < moonStoneData.worldMoonStones[1].stageMoonstones[1].moonStones.Length; i++)
        {
            moonStoneData.worldMoonStones[1].stageMoonstones[1].moonStones[i] = stones[i];
        }

        return stones;
    }

    public int[] GetWorldTwoStageThreeMoonstones()
    {
        int[] moonStones = new int[moonStoneData.worldMoonStones[1].stageMoonstones[2].moonStones.Length];

        for (int i = 0; i < moonStoneData.worldMoonStones[1].stageMoonstones[2].moonStones.Length; i++)
        {
            moonStones[i] = moonStoneData.worldMoonStones[1].stageMoonstones[2].moonStones[i];
        }

        return moonStones;
    }

    public int[] SetWorldTwoStageThreeMoonstones(int[] stones)
    {
        for (int i = 0; i < moonStoneData.worldMoonStones[1].stageMoonstones[2].moonStones.Length; i++)
        {
            moonStoneData.worldMoonStones[1].stageMoonstones[2].moonStones[i] = stones[i];
        }

        return stones;
    }

    public int[] GetWorldTwoStageFourMoonstones()
    {
        int[] moonStones = new int[moonStoneData.worldMoonStones[1].stageMoonstones[3].moonStones.Length];

        for (int i = 0; i < moonStoneData.worldMoonStones[1].stageMoonstones[3].moonStones.Length; i++)
        {
            moonStones[i] = moonStoneData.worldMoonStones[1].stageMoonstones[3].moonStones[i];
        }

        return moonStones;
    }

    public int[] SetWorldTwoStageFourMoonstones(int[] stones)
    {
        for (int i = 0; i < moonStoneData.worldMoonStones[1].stageMoonstones[3].moonStones.Length; i++)
        {
            moonStoneData.worldMoonStones[1].stageMoonstones[3].moonStones[i] = stones[i];
        }

        return stones;
    }

    public int[] GetWorldTwoStageFiveMoonstones()
    {
        int[] moonStones = new int[moonStoneData.worldMoonStones[1].stageMoonstones[4].moonStones.Length];

        for (int i = 0; i < moonStoneData.worldMoonStones[1].stageMoonstones[4].moonStones.Length; i++)
        {
            moonStones[i] = moonStoneData.worldMoonStones[1].stageMoonstones[4].moonStones[i];
        }

        return moonStones;
    }

    public int[] SetWorldTwoStageFiveMoonstones(int[] stones)
    {
        for (int i = 0; i < moonStoneData.worldMoonStones[1].stageMoonstones[4].moonStones.Length; i++)
        {
            moonStoneData.worldMoonStones[1].stageMoonstones[4].moonStones[i] = stones[i];
        }

        return stones;
    }

    public int[] GetWorldThreeStageOneMoonstones()
    {
        int[] moonStones = new int[moonStoneData.worldMoonStones[2].stageMoonstones[0].moonStones.Length];

        for (int i = 0; i < moonStoneData.worldMoonStones[2].stageMoonstones[0].moonStones.Length; i++)
        {
            moonStones[i] = moonStoneData.worldMoonStones[2].stageMoonstones[0].moonStones[i];
        }

        return moonStones;
    }

    public int[] SetWorldThreeStageOneMoonstones(int[] stones)
    {
        for (int i = 0; i < moonStoneData.worldMoonStones[2].stageMoonstones[0].moonStones.Length; i++)
        {
            moonStoneData.worldMoonStones[2].stageMoonstones[0].moonStones[i] = stones[i];
        }

        return stones;
    }

    public int[] GetWorldThreeStageTwoMoonstones()
    {
        int[] moonStones = new int[moonStoneData.worldMoonStones[2].stageMoonstones[1].moonStones.Length];

        for (int i = 0; i < moonStoneData.worldMoonStones[2].stageMoonstones[1].moonStones.Length; i++)
        {
            moonStones[i] = moonStoneData.worldMoonStones[2].stageMoonstones[1].moonStones[i];
        }

        return moonStones;
    }

    public int[] SetWorldThreeStageTwoMoonstones(int[] stones)
    {
        for (int i = 0; i < moonStoneData.worldMoonStones[2].stageMoonstones[1].moonStones.Length; i++)
        {
            moonStoneData.worldMoonStones[2].stageMoonstones[1].moonStones[i] = stones[i];
        }

        return stones;
    }

    public int[] GetWorldThreeStageThreeMoonstones()
    {
        int[] moonStones = new int[moonStoneData.worldMoonStones[2].stageMoonstones[2].moonStones.Length];

        for (int i = 0; i < moonStoneData.worldMoonStones[2].stageMoonstones[2].moonStones.Length; i++)
        {
            moonStones[i] = moonStoneData.worldMoonStones[2].stageMoonstones[2].moonStones[i];
        }

        return moonStones;
    }

    public int[] SetWorldThreeStageThreeMoonstones(int[] stones)
    {
        for (int i = 0; i < moonStoneData.worldMoonStones[2].stageMoonstones[2].moonStones.Length; i++)
        {
            moonStoneData.worldMoonStones[2].stageMoonstones[2].moonStones[i] = stones[i];
        }

        return stones;
    }

    public int[] GetWorldThreeStageFourMoonstones()
    {
        int[] moonStones = new int[moonStoneData.worldMoonStones[2].stageMoonstones[3].moonStones.Length];

        for (int i = 0; i < moonStoneData.worldMoonStones[2].stageMoonstones[3].moonStones.Length; i++)
        {
            moonStones[i] = moonStoneData.worldMoonStones[2].stageMoonstones[3].moonStones[i];
        }

        return moonStones;
    }

    public int[] SetWorldThreeStageFourMoonstones(int[] stones)
    {
        for (int i = 0; i < moonStoneData.worldMoonStones[2].stageMoonstones[3].moonStones.Length; i++)
        {
            moonStoneData.worldMoonStones[2].stageMoonstones[3].moonStones[i] = stones[i];
        }

        return stones;
    }

    public int[] GetWorldThreeStageFiveMoonstones()
    {
        int[] moonStones = new int[moonStoneData.worldMoonStones[2].stageMoonstones[4].moonStones.Length];

        for (int i = 0; i < moonStoneData.worldMoonStones[2].stageMoonstones[4].moonStones.Length; i++)
        {
            moonStones[i] = moonStoneData.worldMoonStones[2].stageMoonstones[4].moonStones[i];
        }

        return moonStones;
    }

    public int[] SetWorldThreeStageFiveMoonstones(int[] stones)
    {
        for (int i = 0; i < moonStoneData.worldMoonStones[2].stageMoonstones[4].moonStones.Length; i++)
        {
            moonStoneData.worldMoonStones[2].stageMoonstones[4].moonStones[i] = stones[i];
        }

        return stones;
    }

    public int[] GetWorldFourStageOneMoonstones()
    {
        int[] moonStones = new int[moonStoneData.worldMoonStones[3].stageMoonstones[0].moonStones.Length];

        for (int i = 0; i < moonStoneData.worldMoonStones[3].stageMoonstones[0].moonStones.Length; i++)
        {
            moonStones[i] = moonStoneData.worldMoonStones[3].stageMoonstones[0].moonStones[i];
        }

        return moonStones;
    }

    public int[] SetWorldFourStageOneMoonstones(int[] stones)
    {
        for (int i = 0; i < moonStoneData.worldMoonStones[3].stageMoonstones[0].moonStones.Length; i++)
        {
            moonStoneData.worldMoonStones[3].stageMoonstones[0].moonStones[i] = stones[i];
        }

        return stones;
    }

    public int[] GetWorldFourStageTwoMoonstones()
    {
        int[] moonStones = new int[moonStoneData.worldMoonStones[3].stageMoonstones[1].moonStones.Length];

        for (int i = 0; i < moonStoneData.worldMoonStones[3].stageMoonstones[1].moonStones.Length; i++)
        {
            moonStones[i] = moonStoneData.worldMoonStones[3].stageMoonstones[1].moonStones[i];
        }

        return moonStones;
    }

    public int[] SetWorldFourStageTwoMoonstones(int[] stones)
    {
        for (int i = 0; i < moonStoneData.worldMoonStones[3].stageMoonstones[1].moonStones.Length; i++)
        {
            moonStoneData.worldMoonStones[3].stageMoonstones[1].moonStones[i] = stones[i];
        }

        return stones;
    }

    public int[] GetWorldFourStageThreeMoonstones()
    {
        int[] moonStones = new int[moonStoneData.worldMoonStones[3].stageMoonstones[2].moonStones.Length];

        for (int i = 0; i < moonStoneData.worldMoonStones[3].stageMoonstones[2].moonStones.Length; i++)
        {
            moonStones[i] = moonStoneData.worldMoonStones[3].stageMoonstones[2].moonStones[i];
        }

        return moonStones;
    }

    public int[] SetWorldFourStageThreeMoonstones(int[] stones)
    {
        for (int i = 0; i < moonStoneData.worldMoonStones[3].stageMoonstones[2].moonStones.Length; i++)
        {
            moonStoneData.worldMoonStones[3].stageMoonstones[2].moonStones[i] = stones[i];
        }

        return stones;
    }

    public int[] GetWorldFourStageFourMoonstones()
    {
        int[] moonStones = new int[moonStoneData.worldMoonStones[3].stageMoonstones[3].moonStones.Length];

        for (int i = 0; i < moonStoneData.worldMoonStones[3].stageMoonstones[3].moonStones.Length; i++)
        {
            moonStones[i] = moonStoneData.worldMoonStones[3].stageMoonstones[3].moonStones[i];
        }

        return moonStones;
    }

    public int[] SetWorldFourStageFourMoonstones(int[] stones)
    {
        for (int i = 0; i < moonStoneData.worldMoonStones[3].stageMoonstones[3].moonStones.Length; i++)
        {
            moonStoneData.worldMoonStones[3].stageMoonstones[3].moonStones[i] = stones[i];
        }

        return stones;
    }

    public int[] GetWorldFourStageFiveMoonstones()
    {
        int[] moonStones = new int[moonStoneData.worldMoonStones[3].stageMoonstones[4].moonStones.Length];

        for (int i = 0; i < moonStoneData.worldMoonStones[3].stageMoonstones[4].moonStones.Length; i++)
        {
            moonStones[i] = moonStoneData.worldMoonStones[3].stageMoonstones[4].moonStones[i];
        }

        return moonStones;
    }

    public int[] SetWorldFourStageFiveMoonstones(int[] stones)
    {
        for (int i = 0; i < moonStoneData.worldMoonStones[3].stageMoonstones[4].moonStones.Length; i++)
        {
            moonStoneData.worldMoonStones[3].stageMoonstones[4].moonStones[i] = stones[i];
        }

        return stones;
    }

    public int[] GetWorldFiveStageOneMoonstones()
    {
        int[] moonStones = new int[moonStoneData.worldMoonStones[4].stageMoonstones[0].moonStones.Length];

        for (int i = 0; i < moonStoneData.worldMoonStones[4].stageMoonstones[0].moonStones.Length; i++)
        {
            moonStones[i] = moonStoneData.worldMoonStones[4].stageMoonstones[0].moonStones[i];
        }

        return moonStones;
    }

    public int[] SetWorldFiveStageOneMoonstones(int[] stones)
    {
        for (int i = 0; i < moonStoneData.worldMoonStones[4].stageMoonstones[0].moonStones.Length; i++)
        {
            moonStoneData.worldMoonStones[4].stageMoonstones[0].moonStones[i] = stones[i];
        }

        return stones;
    }

    public int[] GetWorldFiveStageTwoMoonstones()
    {
        int[] moonStones = new int[moonStoneData.worldMoonStones[4].stageMoonstones[1].moonStones.Length];

        for (int i = 0; i < moonStoneData.worldMoonStones[4].stageMoonstones[1].moonStones.Length; i++)
        {
            moonStones[i] = moonStoneData.worldMoonStones[4].stageMoonstones[1].moonStones[i];
        }

        return moonStones;
    }

    public int[] SetWorldFiveStageTwoMoonstones(int[] stones)
    {
        for (int i = 0; i < moonStoneData.worldMoonStones[4].stageMoonstones[1].moonStones.Length; i++)
        {
            moonStoneData.worldMoonStones[4].stageMoonstones[1].moonStones[i] = stones[i];
        }

        return stones;
    }

    public int[] GetWorldFiveStageThreeMoonstones()
    {
        int[] moonStones = new int[moonStoneData.worldMoonStones[4].stageMoonstones[2].moonStones.Length];

        for (int i = 0; i < moonStoneData.worldMoonStones[4].stageMoonstones[2].moonStones.Length; i++)
        {
            moonStones[i] = moonStoneData.worldMoonStones[4].stageMoonstones[2].moonStones[i];
        }

        return moonStones;
    }

    public int[] SetWorldFiveStageThreeMoonstones(int[] stones)
    {
        for (int i = 0; i < moonStoneData.worldMoonStones[4].stageMoonstones[2].moonStones.Length; i++)
        {
            moonStoneData.worldMoonStones[4].stageMoonstones[2].moonStones[i] = stones[i];
        }

        return stones;
    }

    public int[] GetWorldFiveStageFourMoonstones()
    {
        int[] moonStones = new int[moonStoneData.worldMoonStones[4].stageMoonstones[3].moonStones.Length];

        for (int i = 0; i < moonStoneData.worldMoonStones[4].stageMoonstones[3].moonStones.Length; i++)
        {
            moonStones[i] = moonStoneData.worldMoonStones[4].stageMoonstones[3].moonStones[i];
        }

        return moonStones;
    }

    public int[] SetWorldFiveStageFourMoonstones(int[] stones)
    {
        for (int i = 0; i < moonStoneData.worldMoonStones[4].stageMoonstones[3].moonStones.Length; i++)
        {
            moonStoneData.worldMoonStones[4].stageMoonstones[3].moonStones[i] = stones[i];
        }

        return stones;
    }

    public int[] GetWorldFiveStageFiveMoonstones()
    {
        int[] moonStones = new int[moonStoneData.worldMoonStones[4].stageMoonstones[4].moonStones.Length];

        for (int i = 0; i < moonStoneData.worldMoonStones[4].stageMoonstones[4].moonStones.Length; i++)
        {
            moonStones[i] = moonStoneData.worldMoonStones[4].stageMoonstones[4].moonStones[i];
        }

        return moonStones;
    }

    public int[] SetWorldFiveStageFiveMoonstones(int[] stones)
    {
        for (int i = 0; i < moonStoneData.worldMoonStones[4].stageMoonstones[4].moonStones.Length; i++)
        {
            moonStoneData.worldMoonStones[4].stageMoonstones[4].moonStones[i] = stones[i];
        }

        return stones;
    }

    public int[] GetWorldOneSecretStageOneMoonstones()
    {
        int[] moonStones = new int[moonStoneData.worldMoonStones[0].secretStageMoonstones[0].moonStones.Length];

        for (int i = 0; i < moonStoneData.worldMoonStones[0].secretStageMoonstones[0].moonStones.Length; i++)
        {
            moonStones[i] = moonStoneData.worldMoonStones[0].secretStageMoonstones[0].moonStones[i];
        }

        return moonStones;
    }

    public int[] SetWorldOneSecretStageOneMoonstones(int[] stones)
    {
        for (int i = 0; i < moonStoneData.worldMoonStones[0].secretStageMoonstones[0].moonStones.Length; i++)
        {
            moonStoneData.worldMoonStones[0].secretStageMoonstones[0].moonStones[i] = stones[i];
        }

        return stones;
    }

    public int[] GetWorldOneSecretStageTwoMoonstones()
    {
        int[] moonStones = new int[moonStoneData.worldMoonStones[0].secretStageMoonstones[1].moonStones.Length];

        for (int i = 0; i < moonStoneData.worldMoonStones[0].secretStageMoonstones[1].moonStones.Length; i++)
        {
            moonStones[i] = moonStoneData.worldMoonStones[0].secretStageMoonstones[1].moonStones[i];
        }

        return moonStones;
    }

    public int[] SetWorldOneSecretStageTwoMoonstones(int[] stones)
    {
        for (int i = 0; i < moonStoneData.worldMoonStones[0].secretStageMoonstones[1].moonStones.Length; i++)
        {
            moonStoneData.worldMoonStones[0].secretStageMoonstones[1].moonStones[i] = stones[i];
        }

        return stones;
    }

    public int[] GetWorldTwoSecretStageOneMoonstones()
    {
        int[] moonStones = new int[moonStoneData.worldMoonStones[1].secretStageMoonstones[0].moonStones.Length];

        for (int i = 0; i < moonStoneData.worldMoonStones[1].secretStageMoonstones[0].moonStones.Length; i++)
        {
            moonStones[i] = moonStoneData.worldMoonStones[1].secretStageMoonstones[0].moonStones[i];
        }

        return moonStones;
    }

    public int[] SetWorldTwoSecretStageOneMoonstones(int[] stones)
    {
        for (int i = 0; i < moonStoneData.worldMoonStones[1].secretStageMoonstones[0].moonStones.Length; i++)
        {
            moonStoneData.worldMoonStones[1].secretStageMoonstones[0].moonStones[i] = stones[i];
        }

        return stones;
    }

    public int[] GetWorldTwoSecretStageTwoMoonstones()
    {
        int[] moonStones = new int[moonStoneData.worldMoonStones[1].secretStageMoonstones[1].moonStones.Length];

        for (int i = 0; i < moonStoneData.worldMoonStones[1].secretStageMoonstones[1].moonStones.Length; i++)
        {
            moonStones[i] = moonStoneData.worldMoonStones[1].secretStageMoonstones[1].moonStones[i];
        }

        return moonStones;
    }

    public int[] SetWorldTwoSecretStageTwoMoonstones(int[] stones)
    {
        for (int i = 0; i < moonStoneData.worldMoonStones[1].secretStageMoonstones[1].moonStones.Length; i++)
        {
            moonStoneData.worldMoonStones[1].secretStageMoonstones[1].moonStones[i] = stones[i];
        }

        return stones;
    }

    public int[] GetWorldTwoSecretStageThreeMoonstones()
    {
        int[] moonStones = new int[moonStoneData.worldMoonStones[1].secretStageMoonstones[2].moonStones.Length];

        for (int i = 0; i < moonStoneData.worldMoonStones[1].secretStageMoonstones[2].moonStones.Length; i++)
        {
            moonStones[i] = moonStoneData.worldMoonStones[1].secretStageMoonstones[2].moonStones[i];
        }

        return moonStones;
    }

    public int[] SetWorldTwoSecretStageThreeMoonstones(int[] stones)
    {
        for (int i = 0; i < moonStoneData.worldMoonStones[1].secretStageMoonstones[2].moonStones.Length; i++)
        {
            moonStoneData.worldMoonStones[1].secretStageMoonstones[2].moonStones[i] = stones[i];
        }

        return stones;
    }

    public int[] GetWorldThreeSecretStageOneMoonstones()
    {
        int[] moonStones = new int[moonStoneData.worldMoonStones[2].secretStageMoonstones[0].moonStones.Length];

        for (int i = 0; i < moonStoneData.worldMoonStones[2].secretStageMoonstones[0].moonStones.Length; i++)
        {
            moonStones[i] = moonStoneData.worldMoonStones[2].secretStageMoonstones[0].moonStones[i];
        }

        return moonStones;
    }

    public int[] SetWorldThreeSecretStageOneMoonstones(int[] stones)
    {
        for (int i = 0; i < moonStoneData.worldMoonStones[2].secretStageMoonstones[0].moonStones.Length; i++)
        {
            moonStoneData.worldMoonStones[2].secretStageMoonstones[0].moonStones[i] = stones[i];
        }

        return stones;
    }

    public int[] GetWorldThreeSecretStageTwoMoonstones()
    {
        int[] moonStones = new int[moonStoneData.worldMoonStones[2].secretStageMoonstones[1].moonStones.Length];

        for (int i = 0; i < moonStoneData.worldMoonStones[2].secretStageMoonstones[1].moonStones.Length; i++)
        {
            moonStones[i] = moonStoneData.worldMoonStones[2].secretStageMoonstones[1].moonStones[i];
        }

        return moonStones;
    }

    public int[] SetWorldThreeSecretStageTwoMoonstones(int[] stones)
    {
        for (int i = 0; i < moonStoneData.worldMoonStones[2].secretStageMoonstones[1].moonStones.Length; i++)
        {
            moonStoneData.worldMoonStones[2].secretStageMoonstones[1].moonStones[i] = stones[i];
        }

        return stones;
    }

    public int[] GetWorldThreeSecretStageThreeMoonstones()
    {
        int[] moonStones = new int[moonStoneData.worldMoonStones[2].secretStageMoonstones[2].moonStones.Length];

        for (int i = 0; i < moonStoneData.worldMoonStones[2].secretStageMoonstones[2].moonStones.Length; i++)
        {
            moonStones[i] = moonStoneData.worldMoonStones[2].secretStageMoonstones[2].moonStones[i];
        }

        return moonStones;
    }

    public int[] SetWorldThreeSecretStageThreeMoonstones(int[] stones)
    {
        for (int i = 0; i < moonStoneData.worldMoonStones[2].secretStageMoonstones[2].moonStones.Length; i++)
        {
            moonStoneData.worldMoonStones[2].secretStageMoonstones[2].moonStones[i] = stones[i];
        }

        return stones;
    }

    public int[] GetWorldFourSecretStageOneMoonstones()
    {
        int[] moonStones = new int[moonStoneData.worldMoonStones[3].secretStageMoonstones[0].moonStones.Length];

        for (int i = 0; i < moonStoneData.worldMoonStones[3].secretStageMoonstones[0].moonStones.Length; i++)
        {
            moonStones[i] = moonStoneData.worldMoonStones[3].secretStageMoonstones[0].moonStones[i];
        }

        return moonStones;
    }

    public int[] SetWorldFourSecretStageOneMoonstones(int[] stones)
    {
        for (int i = 0; i < moonStoneData.worldMoonStones[3].secretStageMoonstones[0].moonStones.Length; i++)
        {
            moonStoneData.worldMoonStones[3].secretStageMoonstones[0].moonStones[i] = stones[i];
        }

        return stones;
    }

    public int[] GetWorldFourSecretStageTwoMoonstones()
    {
        int[] moonStones = new int[moonStoneData.worldMoonStones[3].secretStageMoonstones[1].moonStones.Length];

        for (int i = 0; i < moonStoneData.worldMoonStones[3].secretStageMoonstones[1].moonStones.Length; i++)
        {
            moonStones[i] = moonStoneData.worldMoonStones[3].secretStageMoonstones[1].moonStones[i];
        }

        return moonStones;
    }

    public int[] SetWorldFourSecretStageTwoMoonstones(int[] stones)
    {
        for (int i = 0; i < moonStoneData.worldMoonStones[3].secretStageMoonstones[1].moonStones.Length; i++)
        {
            moonStoneData.worldMoonStones[3].secretStageMoonstones[1].moonStones[i] = stones[i];
        }

        return stones;
    }

    public int[] GetWorldFourSecretStageThreeMoonstones()
    {
        int[] moonStones = new int[moonStoneData.worldMoonStones[3].secretStageMoonstones[2].moonStones.Length];

        for (int i = 0; i < moonStoneData.worldMoonStones[3].secretStageMoonstones[2].moonStones.Length; i++)
        {
            moonStones[i] = moonStoneData.worldMoonStones[3].secretStageMoonstones[2].moonStones[i];
        }

        return moonStones;
    }

    public int[] SetWorldFourSecretStageThreeMoonstones(int[] stones)
    {
        for (int i = 0; i < moonStoneData.worldMoonStones[3].secretStageMoonstones[2].moonStones.Length; i++)
        {
            moonStoneData.worldMoonStones[3].secretStageMoonstones[2].moonStones[i] = stones[i];
        }

        return stones;
    }

    public int[] GetWorldFiveSecretStageOneMoonstones()
    {
        int[] moonStones = new int[moonStoneData.worldMoonStones[4].secretStageMoonstones[0].moonStones.Length];

        for (int i = 0; i < moonStoneData.worldMoonStones[4].secretStageMoonstones[0].moonStones.Length; i++)
        {
            moonStones[i] = moonStoneData.worldMoonStones[4].secretStageMoonstones[0].moonStones[i];
        }

        return moonStones;
    }

    public int[] SetWorldFiveSecretStageOneMoonstones(int[] stones)
    {
        for (int i = 0; i < moonStoneData.worldMoonStones[4].secretStageMoonstones[0].moonStones.Length; i++)
        {
            moonStoneData.worldMoonStones[4].secretStageMoonstones[0].moonStones[i] = stones[i];
        }

        return stones;
    }

    public int[] GetWorldFiveSecretStageTwoMoonstones()
    {
        int[] moonStones = new int[moonStoneData.worldMoonStones[4].secretStageMoonstones[1].moonStones.Length];

        for (int i = 0; i < moonStoneData.worldMoonStones[4].secretStageMoonstones[1].moonStones.Length; i++)
        {
            moonStones[i] = moonStoneData.worldMoonStones[4].secretStageMoonstones[1].moonStones[i];
        }

        return moonStones;
    }

    public int[] SetWorldFiveSecretStageTwoMoonstones(int[] stones)
    {
        for (int i = 0; i < moonStoneData.worldMoonStones[4].secretStageMoonstones[1].moonStones.Length; i++)
        {
            moonStoneData.worldMoonStones[4].secretStageMoonstones[1].moonStones[i] = stones[i];
        }

        return stones;
    }

    public int[] GetWorldFiveSecretStageThreeMoonstones()
    {
        int[] moonStones = new int[moonStoneData.worldMoonStones[4].secretStageMoonstones[2].moonStones.Length];

        for (int i = 0; i < moonStoneData.worldMoonStones[4].secretStageMoonstones[2].moonStones.Length; i++)
        {
            moonStones[i] = moonStoneData.worldMoonStones[4].secretStageMoonstones[2].moonStones[i];
        }

        return moonStones;
    }

    public int[] SetWorldFiveSecretStageThreeMoonstones(int[] stones)
    {
        for (int i = 0; i < moonStoneData.worldMoonStones[4].secretStageMoonstones[2].moonStones.Length; i++)
        {
            moonStoneData.worldMoonStones[4].secretStageMoonstones[2].moonStones[i] = stones[i];
        }

        return stones;
    }

    public int[] GetBestiary()
    {
        bestiaryMenu.SetAllMonsters();

        int bestiaryCount = 0;

        foreach (BestiaryButton bestiary in bestiaryMenu.EnemyHolder.GetComponentsInChildren<BestiaryButton>(true))
        {
            bestiaryCount++;
        }

        int[] monsterCount = new int[bestiaryCount];

        if (bestiaryCount > 0)
        {
            for (int i = 0; i < bestiaryMenu.EnemyHolder.childCount; i++)
            {
                BestiaryButton bestiaryButton = bestiaryMenu.EnemyHolder.GetChild(i).GetComponent<BestiaryButton>();

                monsterCount[i] = bestiaryButton._EnemyStats.enemyId;
            }
        }

        StartCoroutine(ResetAllBestiaryButtons());

        return monsterCount;
    }

    private IEnumerator ResetAllBestiaryButtons()
    {
        yield return new WaitForSeconds(0.2f);

        bestiaryMenu.ResetAllMonsters();
    }

    public int[] SetBestiary(int[] bestiaryId)
    {
        bestiaryMenu.ResetBestiaryMenu();

        StartCoroutine(PopulateBestiary(bestiaryId));

        return bestiaryId;
    }

    private IEnumerator PopulateBestiary(int[] bestiaryId)
    {
        yield return new WaitForSeconds(0.3f);

        if (bestiaryId.Length > 0)
        {
            EnemyStats[] enemies = Resources.LoadAll<EnemyStats>("Enemies");

            for (int i = 0; i < enemies.Length; i++)
            {
                for (int j = 0; j < bestiaryId.Length; j++)
                {
                    if (enemies[i].enemyId == bestiaryId[j])
                    {
                        BestiaryButton bestiaryButton = Instantiate(bestiaryMenu._BestiaryButtonPrefab);

                        bestiaryButton._EnemyStats = enemies[i];

                        switch (bestiaryButton._EnemyStats.worldLocation)
                        {
                            case WorldLocation.Forest:
                                bestiaryButton.transform.SetParent(bestiaryMenu.EnemyContainers[0], false);
                                break;
                            case WorldLocation.Desert:
                                bestiaryButton.transform.SetParent(bestiaryMenu.EnemyContainers[1], false);
                                break;
                            case WorldLocation.Arctic:
                                bestiaryButton.transform.SetParent(bestiaryMenu.EnemyContainers[2], false);
                                break;
                            case WorldLocation.Graveyard:
                                bestiaryButton.transform.SetParent(bestiaryMenu.EnemyContainers[3], false);
                                break;
                            case WorldLocation.Castle:
                                bestiaryButton.transform.SetParent(bestiaryMenu.EnemyContainers[4], false);
                                break;
                        }

                        bestiaryButton.EnemyNameText.text = bestiaryButton._EnemyStats.enemyName;

                        bestiaryButton.gameObject.SetActive(true);
                    }
                }
            }
        }
    }

    public int[] GetForestShopCards()
    {
        int[] forestCardId = new int[shopInformation.cardShopForest.Count];

        if(forestCardId.Length > 0)
        {
            for(int i = 0; i < shopInformation.cardShopForest.Count; i++)
            {
                forestCardId[i] = shopInformation.cardShopForest[i].cardId;
            }
        }

        return forestCardId;
    }

    public int[] SetForestShopCards(int[] cardId)
    {
        shopInformation.cardShopForest.Clear();

        if(cardId.Length > 0)
        {
            CardTemplate[] cardTemplate = Resources.LoadAll<CardTemplate>("Cards");

            for (int i = 0; i < cardTemplate.Length; i++)
            {
                for(int j = 0; j < cardId.Length; j++)
                {
                    if(cardTemplate[i].cardId == cardId[j])
                    {
                        shopInformation.cardShopForest.Add(cardTemplate[i]);
                    }
                }
            }
        }

        return cardId;
    }

    public int[] GetForestShopStickers()
    {
        int[] forestStickerId = new int[shopInformation.stickerShopForest.Count];

        if (forestStickerId.Length > 0)
        {
            for (int i = 0; i < shopInformation.stickerShopForest.Count; i++)
            {
                forestStickerId[i] = shopInformation.stickerShopForest[i].stickerId;
            }
        }

        return forestStickerId;
    }

    public int[] SetForestShopStickers(int[] stickerId)
    {
        shopInformation.stickerShopForest.Clear();

        if (stickerId.Length > 0)
        {
            StickerInformation[] stickerInformation = Resources.LoadAll<StickerInformation>("Stickers");

            for (int i = 0; i < stickerInformation.Length; i++)
            {
                for (int j = 0; j < stickerId.Length; j++)
                {
                    if (stickerInformation[i].stickerId == stickerId[j])
                    {
                        shopInformation.stickerShopForest.Add(stickerInformation[i]);
                    }
                }
            }
        }

        return stickerId;
    }

    public int[] GetDesertShopCards()
    {
        int[] desertCardId = new int[shopInformation.cardShopDesert.Count];

        if (desertCardId.Length > 0)
        {
            for (int i = 0; i < shopInformation.cardShopDesert.Count; i++)
            {
                desertCardId[i] = shopInformation.cardShopDesert[i].cardId;
            }
        }

        return desertCardId;
    }

    public int[] SetDesertShopCards(int[] cardId)
    {
        shopInformation.cardShopDesert.Clear();

        if (cardId.Length > 0)
        {
            CardTemplate[] cardTemplate = Resources.LoadAll<CardTemplate>("Cards");

            for (int i = 0; i < cardTemplate.Length; i++)
            {
                for (int j = 0; j < cardId.Length; j++)
                {
                    if (cardTemplate[i].cardId == cardId[j])
                    {
                        shopInformation.cardShopDesert.Add(cardTemplate[i]);
                    }
                }
            }
        }

        return cardId;
    }

    public int[] GetDesertShopStickers()
    {
        int[] desertStickerId = new int[shopInformation.stickerShopDesert.Count];

        if (desertStickerId.Length > 0)
        {
            for (int i = 0; i < shopInformation.stickerShopDesert.Count; i++)
            {
                desertStickerId[i] = shopInformation.stickerShopDesert[i].stickerId;
            }
        }

        return desertStickerId;
    }

    public int[] SetDesertShopStickers(int[] stickerId)
    {
        shopInformation.stickerShopDesert.Clear();

        if (stickerId.Length > 0)
        {
            StickerInformation[] stickerInformation = Resources.LoadAll<StickerInformation>("Stickers");

            for (int i = 0; i < stickerInformation.Length; i++)
            {
                for (int j = 0; j < stickerId.Length; j++)
                {
                    if (stickerInformation[i].stickerId == stickerId[j])
                    {
                        shopInformation.stickerShopDesert.Add(stickerInformation[i]);
                    }
                }
            }
        }

        return stickerId;
    }

    public int[] GetArcticShopCards()
    {
        int[] arcticCardId = new int[shopInformation.cardShopArctic.Count];

        if (arcticCardId.Length > 0)
        {
            for (int i = 0; i < shopInformation.cardShopArctic.Count; i++)
            {
                arcticCardId[i] = shopInformation.cardShopArctic[i].cardId;
            }
        }

        return arcticCardId;
    }

    public int[] SetArcticShopCards(int[] cardId)
    {
        shopInformation.cardShopArctic.Clear();

        if (cardId.Length > 0)
        {
            CardTemplate[] cardTemplate = Resources.LoadAll<CardTemplate>("Cards");

            for (int i = 0; i < cardTemplate.Length; i++)
            {
                for (int j = 0; j < cardId.Length; j++)
                {
                    if (cardTemplate[i].cardId == cardId[j])
                    {
                        shopInformation.cardShopArctic.Add(cardTemplate[i]);
                    }
                }
            }
        }

        return cardId;
    }

    public int[] GetArcticShopStickers()
    {
        int[] arcticStickerId = new int[shopInformation.stickerShopArctic.Count];

        if (arcticStickerId.Length > 0)
        {
            for (int i = 0; i < shopInformation.stickerShopArctic.Count; i++)
            {
                arcticStickerId[i] = shopInformation.stickerShopArctic[i].stickerId;
            }
        }

        return arcticStickerId;
    }

    public int[] SetArcticShopStickers(int[] stickerId)
    {
        shopInformation.stickerShopArctic.Clear();

        if (stickerId.Length > 0)
        {
            StickerInformation[] stickerInformation = Resources.LoadAll<StickerInformation>("Stickers");

            for (int i = 0; i < stickerInformation.Length; i++)
            {
                for (int j = 0; j < stickerId.Length; j++)
                {
                    if (stickerInformation[i].stickerId == stickerId[j])
                    {
                        shopInformation.stickerShopArctic.Add(stickerInformation[i]);
                    }
                }
            }
        }

        return stickerId;
    }

    public int[] GetCemeteryShopCards()
    {
        int[] cemeteryShopId = new int[shopInformation.cardShopGraveyard.Count];

        if (cemeteryShopId.Length > 0)
        {
            for (int i = 0; i < shopInformation.cardShopGraveyard.Count; i++)
            {
                cemeteryShopId[i] = shopInformation.cardShopGraveyard[i].cardId;
            }
        }

        return cemeteryShopId;
    }

    public int[] SetCemeteryShopCards(int[] cardId)
    {
        shopInformation.cardShopGraveyard.Clear();

        if (cardId.Length > 0)
        {
            CardTemplate[] cardTemplate = Resources.LoadAll<CardTemplate>("Cards");

            for (int i = 0; i < cardTemplate.Length; i++)
            {
                for (int j = 0; j < cardId.Length; j++)
                {
                    if (cardTemplate[i].cardId == cardId[j])
                    {
                        shopInformation.cardShopGraveyard.Add(cardTemplate[i]);
                    }
                }
            }
        }

        return cardId;
    }

    public int[] GetCemeteryShopStickers()
    {
        int[] cemeteryStickerId = new int[shopInformation.stickerShopGraveyard.Count];

        if (cemeteryStickerId.Length > 0)
        {
            for (int i = 0; i < shopInformation.stickerShopGraveyard.Count; i++)
            {
                cemeteryStickerId[i] = shopInformation.stickerShopGraveyard[i].stickerId;
            }
        }

        return cemeteryStickerId;
    }

    public int[] SetCemeteryShopStickers(int[] stickerId)
    {
        shopInformation.stickerShopGraveyard.Clear();

        if (stickerId.Length > 0)
        {
            StickerInformation[] stickerInformation = Resources.LoadAll<StickerInformation>("Stickers");

            for (int i = 0; i < stickerInformation.Length; i++)
            {
                for (int j = 0; j < stickerId.Length; j++)
                {
                    if (stickerInformation[i].stickerId == stickerId[j])
                    {
                        shopInformation.stickerShopGraveyard.Add(stickerInformation[i]);
                    }
                }
            }
        }

        return stickerId;
    }

    public int[] GetCastleShopCards()
    {
        int[] castleShopId = new int[shopInformation.cardShopCastle.Count];

        if (castleShopId.Length > 0)
        {
            for (int i = 0; i < shopInformation.cardShopCastle.Count; i++)
            {
                castleShopId[i] = shopInformation.cardShopCastle[i].cardId;
            }
        }

        return castleShopId;
    }

    public int[] SetCastleShopCards(int[] cardId)
    {
        shopInformation.cardShopCastle.Clear();

        if (cardId.Length > 0)
        {
            CardTemplate[] cardTemplate = Resources.LoadAll<CardTemplate>("Cards");

            for (int i = 0; i < cardTemplate.Length; i++)
            {
                for (int j = 0; j < cardId.Length; j++)
                {
                    if (cardTemplate[i].cardId == cardId[j])
                    {
                        shopInformation.cardShopCastle.Add(cardTemplate[i]);
                    }
                }
            }
        }

        return cardId;
    }

    public int[] GetCastleShopStickers()
    {
        int[] castleStickerId = new int[shopInformation.stickerShopCastle.Count];

        if (castleStickerId.Length > 0)
        {
            for (int i = 0; i < shopInformation.stickerShopCastle.Count; i++)
            {
                castleStickerId[i] = shopInformation.stickerShopCastle[i].stickerId;
            }
        }

        return castleStickerId;
    }

    public int[] SetCastleShopStickers(int[] stickerId)
    {
        shopInformation.stickerShopCastle.Clear();

        if (stickerId.Length > 0)
        {
            StickerInformation[] stickerInformation = Resources.LoadAll<StickerInformation>("Stickers");

            for (int i = 0; i < stickerInformation.Length; i++)
            {
                for (int j = 0; j < stickerId.Length; j++)
                {
                    if (stickerInformation[i].stickerId == stickerId[j])
                    {
                        shopInformation.stickerShopCastle.Add(stickerInformation[i]);
                    }
                }
            }
        }

        return stickerId;
    }
}