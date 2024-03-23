using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using Steamworks;

public class TutorialChecker : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private TextMeshProUGUI tutorialInfoText;

    [SerializeField]
    private GameObject exitButton, confirmTutorialInfoButton, declineTutorialInfoButton, pageCountPanel, glossaryPanel, uiBlocker = null;

    [SerializeField]
    private Button prevInfoButton, nextInfoButton, confirmTutorial, declineTutorial;

    [SerializeField]
    private Image prevInfoButtonArrow, nextInfoButtonArrow, confirmTutorialButton, declineTutorialButton;

    [SerializeField]
    private TextMeshProUGUI pageCountText, confirmTutorialText, declineTutorialText;

    private BattleSystem battleSystem;

    private TutorialImages tutorialImages;

    [SerializeField]
    private string baseTutorialMessageTown, baseTutorialMessageWorldMap, baseTutorialMessageFirstStage, baseTutorialMessageBattle, baseTutorialMessageCards, baseTutorialMessageStickers, baseTutorialMessageSecretStage;

    [SerializeField][TextArea][Header("Current Scene Info")]
    private List<string> information, playStationInfo, xboxInfo, steamDeckInfo;

    [SerializeField][TextArea][Header("World Map")]
    private string[] worldMapInformation, playStationWorldMap, xboxWorldMap;

    [SerializeField][TextArea][Header("Towns")]
    private string[] townInformation;

    [SerializeField][TextArea][Header("Basic Controls")]
    private string[] basicControlsInformation, playStationBasicControlsInfo, xboxBasicControlsInfo, steamDeckBasicControlsInfo;

    [SerializeField][TextArea][Header("Battle")]
    private List<string> battleInformation, playStationBattleInfo, xboxBattleInfo, steamDeckBattleInfo;

    [SerializeField][TextArea][Header("Cards")]
    private string[] cardInformation;

    [SerializeField][TextArea][Header("Stickers")]
    private string[] stickerInformation;

    [SerializeField][TextArea][Header("Secret Areas")]
    private string[] secretAreaInformation;

    [SerializeField]
    private bool isInSettingsMenu, isInDeckMenu, isInStickerMenu, switchToControllerInfo;

    private bool viewingWorldMapTutorial, viewingTownTutorial, viewingBasicControlsTutorial, viewingBattleTutorial, viewingCardTutorial, viewingStickerTutorial, viewingSecretStageTutorial, 
                 isDeckTutorial, isStickerTutorial;

    private Scene scene;

    [SerializeField]
    private int infoIndex;

    public List<string> BattleInformation
    {
        get
        {
            return battleInformation;
        }
        set
        {
            battleInformation = value;
        }
    }

    public List<string> PlayStationBattleInfo
    {
        get
        {
            return playStationBattleInfo;
        }
        set
        {
            playStationBattleInfo = value;
        }
    }

    public List<string> XboxBattleInfo
    {
        get
        {
            return xboxBattleInfo;
        }
        set
        {
            xboxBattleInfo = value;
        }
    }

    public List<string> SteamDeckBattleInfo
    {
        get
        {
            return steamDeckBattleInfo;
        }
        set
        {
            steamDeckBattleInfo = value;
        }
    }

    public int InfoIndex => infoIndex;

    public Button NextInfoButton => nextInfoButton;

    public Button PreviousInfoButtton => prevInfoButton;

    private void Start()
    {
        if(GetComponent<TutorialImages>())
        {
            tutorialImages = GetComponent<TutorialImages>();
        }

        if(!isInSettingsMenu && !isInDeckMenu && !isInStickerMenu)
        {
            scene = SceneManager.GetActiveScene();

            if(SceneManager.GetSceneByName("Battle").isLoaded)
            {
                battleSystem = FindObjectOfType<BattleSystem>();

                if(SteamUtils.IsSteamRunningOnSteamDeck())
                {
                    if (NodeManager.instance.HasBattleTutorialOne && NodeManager.instance.HasBattleTutorialTwo && NodeManager.instance.HasBattleTutorialThree)
                    {
                        return;
                    }
                    else
                    {
                        BattleSystem battleSystem = FindObjectOfType<BattleSystem>();

                        battleSystem.TutorialPanel.SetActive(true);

                        battleSystem.IsTutorial = true;

                        StartCoroutine("WaitForSceneToFadeIn");
                    }
                }
                else
                {
                    if (PlayerPrefs.GetInt("BattleTutorialOne") == 1 && PlayerPrefs.GetInt("BattleTutorialTwo") == 1 && PlayerPrefs.GetInt("BattleTutorialThree") == 1)
                    {
                        NodeManager.instance.HasBattleTutorialOne = true;
                        NodeManager.instance.HasBattleTutorialTwo = true;
                        NodeManager.instance.HasBattleTutorialThree = true;

                        return;
                    }
                    else
                    {
                        BattleSystem battleSystem = FindObjectOfType<BattleSystem>();

                        battleSystem.TutorialPanel.SetActive(true);

                        battleSystem.IsTutorial = true;

                        StartCoroutine("WaitForSceneToFadeIn");
                    }
                }
            }
            else
            {
                if (CheckIfTutorialWasRead())
                {
                    return;
                }
                else
                {
                    if (scene.name.Contains("Town") || scene.name.Equals("Forest_1") || scene.name.Equals("Secret_Wood_1"))
                    {
                        GameManager.instance.IsTutorial = true;
                    }
                    if (scene.name.Contains("Field"))
                    {
                        WorldMapMovement wmm = FindObjectOfType<WorldMapMovement>();

                        wmm.IsTutorial = true;

                        wmm.SetBlockers();
                    }
                    StartCoroutine("WaitForSceneToFadeIn");
                }
            }

            prevInfoButton.interactable = false;

            prevInfoButtonArrow.color = new Color(1, 1, 1, 0.7f);
            prevInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.7f);

            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                switch (SceneManager.GetSceneAt(i).name)
                {
                    case ("ForestTown"):
                        tutorialInfoText.text = baseTutorialMessageTown;
                        break;
                    case ("ForestField"):
                        tutorialInfoText.text = baseTutorialMessageWorldMap;
                        break;
                    case ("Forest_1"):
                        tutorialInfoText.text = baseTutorialMessageFirstStage;
                        break;
                    case ("Secret_Wood_1"):
                        tutorialInfoText.text = baseTutorialMessageSecretStage;
                        break;
                    case ("Secret_Wood_2"):
                        tutorialInfoText.text = baseTutorialMessageSecretStage;
                        break;
                    case ("Secret_Wood_3"):
                        tutorialInfoText.text = baseTutorialMessageSecretStage;
                        break;
                    case ("Battle"):
                        tutorialInfoText.text = baseTutorialMessageBattle;
                        break;
                }
            }
        }
    }

    private IEnumerator WaitForSceneToFadeIn()
    {
        yield return new WaitForSecondsRealtime(0.75f);

        if(InputManager.instance.ControllerPluggedIn)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        AudioManager.instance.PlaySoundEffect(AudioManager.instance.OpenMenuAudio);

        scene = SceneManager.GetActiveScene();

        if(!string.IsNullOrEmpty(scene.name))
        {
            if (scene.name.Contains("Battle"))
            {
                BattleSystem battleSystem = FindObjectOfType<BattleSystem>();

                if(SteamUtils.IsSteamRunningOnSteamDeck())
                {
                    if (!NodeManager.instance.HasBattleTutorialOne)
                    {
                        battleSystem.BattleTutorialOneAnimator.Play("Open");

                        battleSystem._InputManager.FirstSelectedObject = battleSystem.TutorialMenuConfirmBattleOne;
                    }
                    else if (!NodeManager.instance.HasBattleTutorialTwo)
                    {
                        battleSystem.BattleTutorialTwoAnimator.Play("Open");

                        battleSystem._InputManager.FirstSelectedObject = battleSystem.TutorialMenuConfirmBattleTwo;
                    }
                    else if (!NodeManager.instance.HasBattleTutorialThree)
                    {
                        battleSystem.BattleTutorialThreeAnimator.Play("Open");

                        battleSystem._InputManager.FirstSelectedObject = battleSystem.TutorialMenuConfirmBattleThree;
                    }
                }
                else
                {
                    if (PlayerPrefs.GetInt("BattleTutorialOne") == 0)
                    {
                        battleSystem.BattleTutorialOneAnimator.Play("Open");

                        battleSystem._InputManager.FirstSelectedObject = battleSystem.TutorialMenuConfirmBattleOne;

                        NodeManager.instance.HasBattleTutorialOne = true;
                    }
                    else if (PlayerPrefs.GetInt("BattleTutorialTwo") == 0)
                    {
                        battleSystem.BattleTutorialTwoAnimator.Play("Open");

                        battleSystem._InputManager.FirstSelectedObject = battleSystem.TutorialMenuConfirmBattleTwo;

                        NodeManager.instance.HasBattleTutorialTwo = true;
                    }
                    else if (PlayerPrefs.GetInt("BattleTutorialThree") == 0)
                    {
                        battleSystem.BattleTutorialThreeAnimator.Play("Open");

                        battleSystem._InputManager.FirstSelectedObject = battleSystem.TutorialMenuConfirmBattleThree;

                        NodeManager.instance.HasBattleTutorialThree = true;
                    }
                }
            }
            else
            {
                InputManager.instance.FirstSelectedObject = confirmTutorialInfoButton;

                animator.Play("Open");
            }
        }

        if (isStickerTutorial)
        {
            declineTutorial.onClick.AddListener(AddInputToSticker);
            exitButton.GetComponent<Button>().onClick.AddListener(AddInputToSticker);

            prevInfoButtonArrow.color = new Color(1, 1, 1, 0.7f);
            prevInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.7f);

            NodeManager.instance.HasStickersTutorial = true;
        }
        else if(isDeckTutorial)
        {
            declineTutorial.onClick.AddListener(AddInputToCard);
            exitButton.GetComponent<Button>().onClick.AddListener(AddInputToCard);

            prevInfoButtonArrow.color = new Color(1, 1, 1, 0.7f);
            prevInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.7f);

            NodeManager.instance.HasCardsTutorial = true;
        }
    }

    public void OpenMessagePopUp()
    {
        glossaryPanel.GetComponent<Animator>().Play("Show");
        
        if(PlayerPrefs.HasKey("SoundEffects"))
        {
            glossaryPanel.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SoundEffects");
            glossaryPanel.GetComponent<AudioSource>().Play();
        }
        else
        {
            glossaryPanel.GetComponent<AudioSource>().Play();
        }
    }

    public bool CheckIfTutorialWasRead()
    {
        bool alreadyCompleted = false;

        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            switch (SceneManager.GetSceneAt(i).name)
            {
                case ("ForestTown"):
                    if(SteamUtils.IsSteamRunningOnSteamDeck())
                    {
                        if(NodeManager.instance.HasTownTutorial)
                        {
                            alreadyCompleted = true;
                        }
                    }
                    else
                    {
                        if (PlayerPrefs.GetInt("TownsTutorial") == 1)
                        {
                            NodeManager.instance.HasTownTutorial = true;
                            alreadyCompleted = true;
                        }
                    }
                    break;
                case ("ForestField"):
                    if(SteamUtils.IsSteamRunningOnSteamDeck())
                    {
                        if(NodeManager.instance.HasWorldMapTutorial)
                        {
                            alreadyCompleted = true;
                        }
                    }
                    else
                    {
                        if (PlayerPrefs.GetInt("WorldMapTutorial") == 1)
                        {
                            NodeManager.instance.HasWorldMapTutorial = true;
                            alreadyCompleted = true;
                        }
                    }
                    break;
                case ("Forest_1"):
                    if(SteamUtils.IsSteamRunningOnSteamDeck())
                    {
                        if(NodeManager.instance.HasBasicControlsTutorial)
                        {
                            alreadyCompleted = true;
                        }
                    }
                    else
                    {
                        if (PlayerPrefs.GetInt("BasicControlsTutorial") == 1)
                        {
                            NodeManager.instance.HasBasicControlsTutorial = true;
                            alreadyCompleted = true;
                        }
                    }
                    break;
                case ("Secret_Wood_1"):
                    if(SteamUtils.IsSteamRunningOnSteamDeck())
                    {
                        if(NodeManager.instance.HasSecretAreaTutorial)
                        {
                            alreadyCompleted = true;
                        }
                    }
                    else
                    {
                        if (PlayerPrefs.GetInt("SecretStageTutorial") == 1)
                        {
                            NodeManager.instance.HasSecretAreaTutorial = true;
                            alreadyCompleted = true;
                        }
                    }
                    break;
                default:
                    alreadyCompleted = true;
                    break;
            }
        }

        return alreadyCompleted;
    }

    public void CheckIfCardTutorialWasCompleted()
    {
        if(SteamUtils.IsSteamRunningOnSteamDeck())
        {
            if (!NodeManager.instance.HasCardsTutorial)
            {
                uiBlocker.SetActive(true);
                tutorialInfoText.text = baseTutorialMessageCards;

                isDeckTutorial = true;

                NodeManager.instance.HasCardsTutorial = true;

                StartCoroutine(WaitForSceneToFadeIn());
            }
        }
        else
        {
            if (!PlayerPrefs.HasKey("CardTutorial"))
            {
                uiBlocker.SetActive(true);
                tutorialInfoText.text = baseTutorialMessageCards;

                isDeckTutorial = true;

                NodeManager.instance.HasCardsTutorial = true;

                StartCoroutine(WaitForSceneToFadeIn());
            }
            else
            {
                NodeManager.instance.HasCardsTutorial = true;
            }
        }
    }

    public void CheckIfStickerTutorialWasCompleted()
    {
        if(SteamUtils.IsSteamRunningOnSteamDeck())
        {
            if (!NodeManager.instance.HasStickersTutorial)
            {
                uiBlocker.SetActive(true);
                tutorialInfoText.text = baseTutorialMessageStickers;

                isStickerTutorial = true;

                NodeManager.instance.HasStickersTutorial = true;

                StartCoroutine(WaitForSceneToFadeIn());
            }
        }
        else
        {
            if (!PlayerPrefs.HasKey("StickerTutorial"))
            {
                uiBlocker.SetActive(true);
                tutorialInfoText.text = baseTutorialMessageStickers;

                isStickerTutorial = true;

                NodeManager.instance.HasStickersTutorial = true;

                StartCoroutine(WaitForSceneToFadeIn());
            }
            else
            {
                NodeManager.instance.HasStickersTutorial = true;
            }
        }
    }

    public void AddInputToSticker()
    {
        InputManager.instance.SetSelectedObject(MenuController.instance._StickerMenu.StickerList.Count > 0 ? MenuController.instance._StickerMenu.StickerList[0].gameObject :
                                                MenuController.instance._StickerMenu.StickerBackArrow);

        isStickerTutorial = false;

        declineTutorial.onClick.RemoveListener(AddInputToSticker);
        exitButton.GetComponent<Button>().onClick.RemoveListener(AddInputToSticker);
    }

    public void AddInputToCard()
    {
        InputManager.instance.SetSelectedObject(MenuController.instance._DeckMenu.DeckParent.GetChild(0).gameObject);

        isDeckTutorial = false;

        declineTutorial.onClick.RemoveListener(AddInputToCard);
        exitButton.GetComponent<Button>().onClick.RemoveListener(AddInputToCard);
    }

    public void SetWorldMapInfo()
    {
        infoIndex = 0;

        MenuController.instance.TutorialTitleText.text = "World Map";

        viewingWorldMapTutorial = true;
        viewingBattleTutorial = false;
        viewingTownTutorial = false;
        viewingBasicControlsTutorial = false;
        viewingCardTutorial = false;
        viewingStickerTutorial = false;
        viewingSecretStageTutorial = false;

        prevInfoButton.interactable = false;
        nextInfoButton.interactable = true;

        prevInfoButton.image.raycastTarget = false;

        prevInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.7f);
        nextInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);

        if(InputManager.instance.ControllerPluggedIn)
        {
            if(InputManager.instance.ControllerName == "xbox")
            {
                tutorialInfoText.text = xboxWorldMap[infoIndex];

                pageCountText.text = 1 + "/" + xboxWorldMap.Length;
            }
            else
            {
                tutorialInfoText.text = playStationWorldMap[infoIndex];

                pageCountText.text = 1 + "/" + playStationWorldMap.Length;
            }
        }
        else
        {
            tutorialInfoText.text = worldMapInformation[infoIndex];

            pageCountText.text = 1 + "/" + worldMapInformation.Length;
        }
    }

    public void SetTownInfo()
    {
        infoIndex = 0;

        MenuController.instance.TutorialTitleText.text = "Towns";

        viewingWorldMapTutorial = false;
        viewingBattleTutorial = false;
        viewingTownTutorial = true;
        viewingBasicControlsTutorial = false;
        viewingCardTutorial = false;
        viewingStickerTutorial = false;
        viewingSecretStageTutorial = false;

        prevInfoButton.interactable = false;
        nextInfoButton.interactable = true;

        prevInfoButton.image.raycastTarget = false;

        prevInfoButtonArrow.color = new Color(1, 1, 1, 0.7f);
        nextInfoButtonArrow.color = new Color(1, 1, 1, 1);

        nextInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);

        tutorialInfoText.text = townInformation[infoIndex];

        pageCountText.text = 1 + "/" + townInformation.Length;
    }

    public void SetBasicControlsInfo()
    {
        infoIndex = 0;

        MenuController.instance.TutorialTitleText.text = "Basic Controls";

        viewingWorldMapTutorial = false;
        viewingBattleTutorial = false;
        viewingTownTutorial = false;
        viewingBasicControlsTutorial = true;
        viewingCardTutorial = false;
        viewingStickerTutorial = false;
        viewingSecretStageTutorial = false;

        prevInfoButton.interactable = false;
        nextInfoButton.interactable = true;

        prevInfoButton.image.raycastTarget = false;

        prevInfoButtonArrow.color = new Color(1, 1, 1, 0.7f);
        nextInfoButtonArrow.color = new Color(1, 1, 1, 1);

        nextInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);

        if(InputManager.instance.ControllerPluggedIn)
        {
            if(InputManager.instance.ControllerName == "xbox")
            {
                if(SteamUtils.IsSteamRunningOnSteamDeck())
                {
                    tutorialInfoText.text = steamDeckBasicControlsInfo[infoIndex];

                    pageCountText.text = 1 + "/" + steamDeckBasicControlsInfo.Length;
                }
                else
                {
                    tutorialInfoText.text = xboxBasicControlsInfo[infoIndex];

                    pageCountText.text = 1 + "/" + xboxBasicControlsInfo.Length;
                }
            }
            else
            {
                tutorialInfoText.text = playStationBasicControlsInfo[infoIndex];

                pageCountText.text = 1 + "/" + playStationBasicControlsInfo.Length;
            }
        }
        else
        {
            tutorialInfoText.text = basicControlsInformation[infoIndex];

            pageCountText.text = 1 + "/" + basicControlsInformation.Length;
        }
    }

    public void SetBattleInfo()
    {
        infoIndex = 0;

        MenuController.instance.TutorialTitleText.text = "Battles";

        viewingWorldMapTutorial = false;
        viewingBattleTutorial = true;
        viewingTownTutorial = false;
        viewingBasicControlsTutorial = false;
        viewingCardTutorial = false;
        viewingStickerTutorial = false;
        viewingSecretStageTutorial = false;

        prevInfoButton.interactable = false;
        nextInfoButton.interactable = true;

        prevInfoButton.image.raycastTarget = false;

        prevInfoButtonArrow.color = new Color(1, 1, 1, 0.7f);
        nextInfoButtonArrow.color = new Color(1, 1, 1, 1);

        nextInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);

        if(InputManager.instance.ControllerPluggedIn)
        {
            if(InputManager.instance.ControllerName == "xbox")
            {
                if(SteamUtils.IsSteamRunningOnSteamDeck())
                {
                    tutorialInfoText.text = steamDeckBattleInfo[infoIndex];

                    pageCountText.text = 1 + "/" + steamDeckBattleInfo.Count;
                }
                else
                {
                    tutorialInfoText.text = xboxBattleInfo[infoIndex];

                    pageCountText.text = 1 + "/" + xboxBattleInfo.Count;
                }
            }
            else
            {
                tutorialInfoText.text = playStationBattleInfo[infoIndex];

                pageCountText.text = 1 + "/" + playStationBattleInfo.Count;
            }
        }
        else
        {
            tutorialInfoText.text = battleInformation[infoIndex];

            pageCountText.text = 1 + "/" + battleInformation.Count;
        }
    }

    public void SetCardInfo()
    {
        infoIndex = 0;

        MenuController.instance.TutorialTitleText.text = "Cards";

        viewingWorldMapTutorial = false;
        viewingBattleTutorial = false;
        viewingTownTutorial = false;
        viewingBasicControlsTutorial = false;
        viewingCardTutorial = true;
        viewingStickerTutorial = false;
        viewingSecretStageTutorial = false;

        prevInfoButton.interactable = false;
        nextInfoButton.interactable = true;

        prevInfoButton.image.raycastTarget = false;

        prevInfoButtonArrow.color = new Color(1, 1, 1, 0.7f);
        nextInfoButtonArrow.color = new Color(1, 1, 1, 1);

        nextInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);

        tutorialInfoText.text = cardInformation[infoIndex];

        pageCountText.text = 1 + "/" + cardInformation.Length;
    }

    public void SetStickerInfo()
    {
        infoIndex = 0;

        MenuController.instance.TutorialTitleText.text = "Stickers";

        viewingWorldMapTutorial = false;
        viewingBattleTutorial = false;
        viewingTownTutorial = false;
        viewingBasicControlsTutorial = false;
        viewingCardTutorial = false;
        viewingStickerTutorial = true;
        viewingSecretStageTutorial = false;

        prevInfoButton.interactable = false;
        nextInfoButton.interactable = true;

        prevInfoButton.image.raycastTarget = false;

        prevInfoButtonArrow.color = new Color(1, 1, 1, 0.7f);
        nextInfoButtonArrow.color = new Color(1, 1, 1, 1);

        nextInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);

        tutorialInfoText.text = stickerInformation[infoIndex];

        pageCountText.text = 1 + "/" + stickerInformation.Length;
    }

    public void SetSecretStageInfo()
    {
        infoIndex = 0;

        MenuController.instance.TutorialTitleText.text = "Secret Areas";

        viewingWorldMapTutorial = false;
        viewingBattleTutorial = false;
        viewingTownTutorial = false;
        viewingBasicControlsTutorial = false;
        viewingCardTutorial = false;
        viewingStickerTutorial = false;
        viewingSecretStageTutorial = true;

        prevInfoButton.interactable = false;
        nextInfoButton.interactable = true;

        prevInfoButton.image.raycastTarget = false;

        prevInfoButtonArrow.color = new Color(1, 1, 1, 0.7f);
        nextInfoButtonArrow.color = new Color(1, 1, 1, 1);

        nextInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);

        tutorialInfoText.text = secretAreaInformation[infoIndex];

        pageCountText.text = 1 + "/" + secretAreaInformation.Length;
    }

    public void SetTutorialInfo()
    {
        exitButton.SetActive(true);

        DisableConfirmAndDeclineTutorialButtons();

        prevInfoButton.gameObject.SetActive(true);
        nextInfoButton.gameObject.SetActive(true);

        prevInfoButton.image.raycastTarget = false;

        pageCountPanel.SetActive(true);

        if(scene.name.Contains("Battle"))
        {
            if(!battleSystem._InputManager.ControllerPluggedIn)
            {
                switchToControllerInfo = false;
            }
        }
        else
        {
            if(!InputManager.instance.ControllerPluggedIn)
            {
                switchToControllerInfo = false;
            }
        }

        if(switchToControllerInfo)
        {
            if(InputManager.instance.ControllerPluggedIn)
            {
                if(InputManager.instance.ControllerName == "xbox")
                {
                    if(SteamUtils.IsSteamRunningOnSteamDeck())
                    {
                        tutorialInfoText.text = steamDeckInfo[infoIndex];

                        pageCountText.text = 1 + "/" + steamDeckInfo.Count;
                    }
                    else
                    {
                        tutorialInfoText.text = xboxInfo[infoIndex];

                        pageCountText.text = 1 + "/" + xboxInfo.Count;
                    }
                }
                else
                {
                    tutorialInfoText.text = playStationInfo[infoIndex];

                    pageCountText.text = 1 + "/" + playStationInfo.Count;
                }
            }
        }
        else
        {
            tutorialInfoText.text = information[infoIndex];

            pageCountText.text = 1 + "/" + information.Count;
        }
    }

    private void DisableConfirmAndDeclineTutorialButtons()
    {
        confirmTutorialButton.enabled = false;
        confirmTutorialText.enabled = false;
        confirmTutorial.interactable = false;

        declineTutorialButton.enabled = false;
        declineTutorialText.enabled = false;
        declineTutorial.interactable = false;
    }

    public void NextInfoPage()
    {
        if(isInSettingsMenu)
        {
            if(viewingWorldMapTutorial)
            {
                if(InputManager.instance.ControllerPluggedIn)
                {
                    if(InputManager.instance.ControllerName == "xbox")
                    {
                        if (infoIndex < xboxWorldMap.Length)
                        {
                            infoIndex++;

                            if (tutorialImages != null)
                            {
                                tutorialImages.CheckTutorialImageMasterMenu(0);
                            }

                            pageCountText.text = (1 + infoIndex) + "/" + xboxWorldMap.Length;
                        }

                        if (infoIndex > 0)
                        {
                            prevInfoButton.interactable = true;

                            prevInfoButton.image.raycastTarget = true;

                            prevInfoButtonArrow.color = new Color(1, 1, 1, 1);
                            prevInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);

                            Navigation nextNav = nextInfoButton.GetComponent<Selectable>().navigation;

                            nextNav.selectOnLeft = prevInfoButton;
                            nextNav.selectOnRight = prevInfoButton;

                            nextInfoButton.GetComponent<Selectable>().navigation = nextNav;

                            Navigation prevNav = prevInfoButton.GetComponent<Selectable>().navigation;

                            prevNav.selectOnLeft = nextInfoButton;
                            prevNav.selectOnRight = nextInfoButton;

                            prevInfoButton.GetComponent<Selectable>().navigation = prevNav;
                        }

                        if (infoIndex >= xboxWorldMap.Length - 1)
                        {
                            nextInfoButton.interactable = false;

                            nextInfoButton.image.raycastTarget = false;

                            nextInfoButtonArrow.color = new Color(1, 1, 1, 0.7f);
                            nextInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.7f);

                            InputManager.instance.SetSelectedObject(prevInfoButton.gameObject);

                            nextInfoButton.GetComponent<Animator>().ResetTrigger("Selected");
                            nextInfoButton.GetComponent<Animator>().SetTrigger("Normal");

                            Navigation prevNav = prevInfoButton.GetComponent<Selectable>().navigation;

                            prevNav.selectOnLeft = null;
                            prevNav.selectOnRight = null;

                            prevInfoButton.GetComponent<Selectable>().navigation = prevNav;
                        }
                        else
                        {
                            nextInfoButton.interactable = true;

                            nextInfoButton.image.raycastTarget = true;

                            nextInfoButtonArrow.color = new Color(1, 1, 1, 1);
                            nextInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                        }

                        if(tutorialImages != null)
                        {
                            tutorialInfoText.text = tutorialImages.ObjectEnabled ? "\n\n\n" + xboxWorldMap[infoIndex] : xboxWorldMap[infoIndex];
                        }
                        else
                        {
                            tutorialInfoText.text = xboxWorldMap[infoIndex];
                        }
                    }
                    else
                    {
                        if (infoIndex < playStationWorldMap.Length)
                        {
                            infoIndex++;

                            if (tutorialImages != null)
                            {
                                tutorialImages.CheckTutorialImageMasterMenu(0);
                            }

                            pageCountText.text = (1 + infoIndex) + "/" + playStationWorldMap.Length;
                        }

                        if (infoIndex > 0)
                        {
                            prevInfoButton.interactable = true;

                            prevInfoButton.image.raycastTarget = true;

                            prevInfoButtonArrow.color = new Color(1, 1, 1, 1);
                            prevInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);

                            Navigation nextNav = nextInfoButton.GetComponent<Selectable>().navigation;

                            nextNav.selectOnLeft = prevInfoButton;
                            nextNav.selectOnRight = prevInfoButton;

                            nextInfoButton.GetComponent<Selectable>().navigation = nextNav;

                            Navigation prevNav = prevInfoButton.GetComponent<Selectable>().navigation;

                            prevNav.selectOnLeft = nextInfoButton;
                            prevNav.selectOnRight = nextInfoButton;

                            prevInfoButton.GetComponent<Selectable>().navigation = prevNav;
                        }

                        if (infoIndex >= playStationWorldMap.Length - 1)
                        {
                            nextInfoButton.interactable = false;

                            nextInfoButton.image.raycastTarget = false;

                            nextInfoButtonArrow.color = new Color(1, 1, 1, 0.7f);
                            nextInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.7f);

                            InputManager.instance.SetSelectedObject(prevInfoButton.gameObject);

                            nextInfoButton.GetComponent<Animator>().ResetTrigger("Selected");
                            nextInfoButton.GetComponent<Animator>().SetTrigger("Normal");

                            Navigation prevNav = prevInfoButton.GetComponent<Selectable>().navigation;

                            prevNav.selectOnLeft = null;
                            prevNav.selectOnRight = null;

                            prevInfoButton.GetComponent<Selectable>().navigation = prevNav;
                        }
                        else
                        {
                            nextInfoButton.interactable = true;

                            nextInfoButton.image.raycastTarget = true;

                            nextInfoButtonArrow.color = new Color(1, 1, 1, 1);
                            nextInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                        }

                        if (tutorialImages != null)
                        {
                            tutorialInfoText.text = tutorialImages.ObjectEnabled ? "\n\n\n" + playStationWorldMap[infoIndex] : playStationWorldMap[infoIndex];
                        }
                        else
                        {
                            tutorialInfoText.text = playStationWorldMap[infoIndex];
                        }
                    }
                }
                else
                {
                    if (infoIndex < worldMapInformation.Length)
                    {
                        infoIndex++;

                        if (tutorialImages != null)
                        {
                            tutorialImages.CheckTutorialImageMasterMenu(0);
                        }

                        pageCountText.text = (1 + infoIndex) + "/" + worldMapInformation.Length;
                    }

                    if (infoIndex > 0)
                    {
                        prevInfoButton.interactable = true;

                        prevInfoButton.image.raycastTarget = true;

                        prevInfoButtonArrow.color = new Color(1, 1, 1, 1);
                        prevInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);

                        Navigation nextNav = nextInfoButton.GetComponent<Selectable>().navigation;

                        nextNav.selectOnLeft = prevInfoButton;
                        nextNav.selectOnRight = prevInfoButton;

                        nextInfoButton.GetComponent<Selectable>().navigation = nextNav;

                        Navigation prevNav = prevInfoButton.GetComponent<Selectable>().navigation;

                        prevNav.selectOnLeft = nextInfoButton;
                        prevNav.selectOnRight = nextInfoButton;

                        prevInfoButton.GetComponent<Selectable>().navigation = prevNav;
                    }

                    if (infoIndex >= worldMapInformation.Length - 1)
                    {
                        nextInfoButton.interactable = false;

                        nextInfoButton.image.raycastTarget = false;

                        nextInfoButtonArrow.color = new Color(1, 1, 1, 0.7f);
                        nextInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.7f);

                        Navigation prevNav = prevInfoButton.GetComponent<Selectable>().navigation;

                        prevNav.selectOnLeft = null;
                        prevNav.selectOnRight = null;

                        prevInfoButton.GetComponent<Selectable>().navigation = prevNav;
                    }
                    else
                    {
                        nextInfoButton.interactable = true;

                        nextInfoButton.image.raycastTarget = true;

                        nextInfoButtonArrow.color = new Color(1, 1, 1, 1);
                        nextInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    }

                    if (tutorialImages != null)
                    {
                        tutorialInfoText.text = tutorialImages.ObjectEnabled ? "\n\n\n" + worldMapInformation[infoIndex] : worldMapInformation[infoIndex];
                    }
                    else
                    {
                        tutorialInfoText.text = worldMapInformation[infoIndex];
                    }
                }
            }
            if(viewingTownTutorial)
            {
                if (infoIndex < townInformation.Length)
                {
                    infoIndex++;

                    pageCountText.text = (1 + infoIndex) + "/" + townInformation.Length;
                }

                if (infoIndex > 0)
                {
                    prevInfoButton.interactable = true;

                    prevInfoButton.image.raycastTarget = true;

                    prevInfoButtonArrow.color = new Color(1, 1, 1, 1);
                    prevInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);

                    Navigation nextNav = nextInfoButton.GetComponent<Selectable>().navigation;

                    nextNav.selectOnLeft = prevInfoButton;
                    nextNav.selectOnRight = prevInfoButton;

                    nextInfoButton.GetComponent<Selectable>().navigation = nextNav;

                    Navigation prevNav = prevInfoButton.GetComponent<Selectable>().navigation;

                    prevNav.selectOnLeft = nextInfoButton;
                    prevNav.selectOnRight = nextInfoButton;

                    prevInfoButton.GetComponent<Selectable>().navigation = prevNav;
                }

                if (infoIndex >= townInformation.Length - 1)
                {
                    nextInfoButton.interactable = false;

                    nextInfoButton.image.raycastTarget = false;

                    nextInfoButtonArrow.color = new Color(1, 1, 1, 0.7f);
                    nextInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.7f);

                    InputManager.instance.SetSelectedObject(prevInfoButton.gameObject);

                    nextInfoButton.GetComponent<Animator>().ResetTrigger("Selected");
                    nextInfoButton.GetComponent<Animator>().SetTrigger("Normal");

                    Navigation prevNav = prevInfoButton.GetComponent<Selectable>().navigation;

                    prevNav.selectOnLeft = null;
                    prevNav.selectOnRight = null;

                    prevInfoButton.GetComponent<Selectable>().navigation = prevNav;
                }
                else
                {
                    nextInfoButton.interactable = true;

                    nextInfoButton.image.raycastTarget = true;

                    nextInfoButtonArrow.color = new Color(1, 1, 1, 1);
                    nextInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                }

                tutorialInfoText.text = townInformation[infoIndex];
            }
            if(viewingBasicControlsTutorial)
            {
                if(InputManager.instance.ControllerPluggedIn)
                {
                    if(InputManager.instance.ControllerName == "xbox")
                    {
                        if(SteamUtils.IsSteamRunningOnSteamDeck())
                        {
                            if (infoIndex < steamDeckBasicControlsInfo.Length)
                            {
                                infoIndex++;

                                if (tutorialImages != null)
                                {
                                    tutorialImages.CheckTutorialImageMasterMenu(1);
                                }

                                pageCountText.text = (1 + infoIndex) + "/" + steamDeckBasicControlsInfo.Length;
                            }

                            if (infoIndex > 0)
                            {
                                prevInfoButton.interactable = true;

                                prevInfoButton.image.raycastTarget = true;

                                prevInfoButtonArrow.color = new Color(1, 1, 1, 1);
                                prevInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);

                                Navigation nextNav = nextInfoButton.GetComponent<Selectable>().navigation;

                                nextNav.selectOnLeft = prevInfoButton;
                                nextNav.selectOnRight = prevInfoButton;

                                nextInfoButton.GetComponent<Selectable>().navigation = nextNav;

                                Navigation prevNav = prevInfoButton.GetComponent<Selectable>().navigation;

                                prevNav.selectOnLeft = nextInfoButton;
                                prevNav.selectOnRight = nextInfoButton;

                                prevInfoButton.GetComponent<Selectable>().navigation = prevNav;
                            }

                            if (infoIndex >= steamDeckBasicControlsInfo.Length - 1)
                            {
                                nextInfoButton.interactable = false;

                                nextInfoButton.image.raycastTarget = false;

                                nextInfoButtonArrow.color = new Color(1, 1, 1, 0.7f);
                                nextInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.7f);

                                InputManager.instance.SetSelectedObject(prevInfoButton.gameObject);

                                nextInfoButton.GetComponent<Animator>().ResetTrigger("Selected");
                                nextInfoButton.GetComponent<Animator>().SetTrigger("Normal");

                                Navigation prevNav = prevInfoButton.GetComponent<Selectable>().navigation;

                                prevNav.selectOnLeft = null;
                                prevNav.selectOnRight = null;

                                prevInfoButton.GetComponent<Selectable>().navigation = prevNav;
                            }
                            else
                            {
                                nextInfoButton.interactable = true;

                                nextInfoButton.image.raycastTarget = true;

                                nextInfoButtonArrow.color = new Color(1, 1, 1, 1);
                                nextInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                            }

                            if (tutorialImages != null)
                            {
                                tutorialInfoText.text = tutorialImages.ObjectEnabled ? "\n\n\n" + steamDeckBasicControlsInfo[infoIndex] : steamDeckBasicControlsInfo[infoIndex];
                            }
                            else
                            {
                                tutorialInfoText.text = steamDeckBasicControlsInfo[infoIndex];
                            }
                        }
                        else
                        {
                            if (infoIndex < xboxBasicControlsInfo.Length)
                            {
                                infoIndex++;

                                if (tutorialImages != null)
                                {
                                    tutorialImages.CheckTutorialImageMasterMenu(1);
                                }

                                pageCountText.text = (1 + infoIndex) + "/" + xboxBasicControlsInfo.Length;
                            }

                            if (infoIndex > 0)
                            {
                                prevInfoButton.interactable = true;

                                prevInfoButton.image.raycastTarget = true;

                                prevInfoButtonArrow.color = new Color(1, 1, 1, 1);
                                prevInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);

                                Navigation nextNav = nextInfoButton.GetComponent<Selectable>().navigation;

                                nextNav.selectOnLeft = prevInfoButton;
                                nextNav.selectOnRight = prevInfoButton;

                                nextInfoButton.GetComponent<Selectable>().navigation = nextNav;

                                Navigation prevNav = prevInfoButton.GetComponent<Selectable>().navigation;

                                prevNav.selectOnLeft = nextInfoButton;
                                prevNav.selectOnRight = nextInfoButton;

                                prevInfoButton.GetComponent<Selectable>().navigation = prevNav;
                            }

                            if (infoIndex >= xboxBasicControlsInfo.Length - 1)
                            {
                                nextInfoButton.interactable = false;

                                nextInfoButton.image.raycastTarget = false;

                                nextInfoButtonArrow.color = new Color(1, 1, 1, 0.7f);
                                nextInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.7f);

                                InputManager.instance.SetSelectedObject(prevInfoButton.gameObject);

                                nextInfoButton.GetComponent<Animator>().ResetTrigger("Selected");
                                nextInfoButton.GetComponent<Animator>().SetTrigger("Normal");

                                Navigation prevNav = prevInfoButton.GetComponent<Selectable>().navigation;

                                prevNav.selectOnLeft = null;
                                prevNav.selectOnRight = null;

                                prevInfoButton.GetComponent<Selectable>().navigation = prevNav;
                            }
                            else
                            {
                                nextInfoButton.interactable = true;

                                nextInfoButton.image.raycastTarget = true;

                                nextInfoButtonArrow.color = new Color(1, 1, 1, 1);
                                nextInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                            }

                            if (tutorialImages != null)
                            {
                                tutorialInfoText.text = tutorialImages.ObjectEnabled ? "\n\n\n" + xboxBasicControlsInfo[infoIndex] : xboxBasicControlsInfo[infoIndex];
                            }
                            else
                            {
                                tutorialInfoText.text = xboxBasicControlsInfo[infoIndex];
                            }
                        }
                    }
                    else
                    {
                        if (infoIndex < playStationBasicControlsInfo.Length)
                        {
                            infoIndex++;

                            if (tutorialImages != null)
                            {
                                tutorialImages.CheckTutorialImageMasterMenu(1);
                            }

                            pageCountText.text = (1 + infoIndex) + "/" + playStationBasicControlsInfo.Length;
                        }

                        if (infoIndex > 0)
                        {
                            prevInfoButton.interactable = true;

                            prevInfoButton.image.raycastTarget = true;

                            prevInfoButtonArrow.color = new Color(1, 1, 1, 1);
                            prevInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);

                            Navigation nextNav = nextInfoButton.GetComponent<Selectable>().navigation;

                            nextNav.selectOnLeft = prevInfoButton;
                            nextNav.selectOnRight = prevInfoButton;

                            nextInfoButton.GetComponent<Selectable>().navigation = nextNav;

                            Navigation prevNav = prevInfoButton.GetComponent<Selectable>().navigation;

                            prevNav.selectOnLeft = nextInfoButton;
                            prevNav.selectOnRight = nextInfoButton;

                            prevInfoButton.GetComponent<Selectable>().navigation = prevNav;
                        }

                        if (infoIndex >= playStationBasicControlsInfo.Length - 1)
                        {
                            nextInfoButton.interactable = false;

                            nextInfoButton.image.raycastTarget = false;

                            nextInfoButtonArrow.color = new Color(1, 1, 1, 0.7f);
                            nextInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.7f);

                            InputManager.instance.SetSelectedObject(prevInfoButton.gameObject);

                            nextInfoButton.GetComponent<Animator>().ResetTrigger("Selected");
                            nextInfoButton.GetComponent<Animator>().SetTrigger("Normal");

                            Navigation prevNav = prevInfoButton.GetComponent<Selectable>().navigation;

                            prevNav.selectOnLeft = null;
                            prevNav.selectOnRight = null;

                            prevInfoButton.GetComponent<Selectable>().navigation = prevNav;
                        }
                        else
                        {
                            nextInfoButton.interactable = true;

                            nextInfoButton.image.raycastTarget = true;

                            nextInfoButtonArrow.color = new Color(1, 1, 1, 1);
                            nextInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                        }

                        if (tutorialImages != null)
                        {
                            tutorialInfoText.text = tutorialImages.ObjectEnabled ? "\n\n\n" + playStationBasicControlsInfo[infoIndex] : playStationBasicControlsInfo[infoIndex];
                        }
                        else
                        {
                            tutorialInfoText.text = playStationBasicControlsInfo[infoIndex];
                        }
                    }
                }
                else
                {
                    if (infoIndex < basicControlsInformation.Length)
                    {
                        infoIndex++;

                        if (tutorialImages != null)
                        {
                            tutorialImages.CheckTutorialImageMasterMenu(1);
                        }

                        pageCountText.text = (1 + infoIndex) + "/" + basicControlsInformation.Length;
                    }

                    if (infoIndex > 0)
                    {
                        prevInfoButton.interactable = true;

                        prevInfoButton.image.raycastTarget = true;

                        prevInfoButtonArrow.color = new Color(1, 1, 1, 1);
                        prevInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);

                        Navigation nextNav = nextInfoButton.GetComponent<Selectable>().navigation;

                        nextNav.selectOnLeft = prevInfoButton;
                        nextNav.selectOnRight = prevInfoButton;

                        nextInfoButton.GetComponent<Selectable>().navigation = nextNav;

                        Navigation prevNav = prevInfoButton.GetComponent<Selectable>().navigation;

                        prevNav.selectOnLeft = nextInfoButton;
                        prevNav.selectOnRight = nextInfoButton;

                        prevInfoButton.GetComponent<Selectable>().navigation = prevNav;
                    }

                    if (infoIndex >= basicControlsInformation.Length - 1)
                    {
                        nextInfoButton.interactable = false;

                        nextInfoButton.image.raycastTarget = false;

                        nextInfoButtonArrow.color = new Color(1, 1, 1, 0.7f);
                        nextInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.7f);

                        Navigation prevNav = prevInfoButton.GetComponent<Selectable>().navigation;

                        prevNav.selectOnLeft = null;
                        prevNav.selectOnRight = null;

                        prevInfoButton.GetComponent<Selectable>().navigation = prevNav;
                    }
                    else
                    {
                        nextInfoButton.interactable = true;

                        nextInfoButton.image.raycastTarget = true;

                        nextInfoButtonArrow.color = new Color(1, 1, 1, 1);
                        nextInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    }

                    if (tutorialImages != null)
                    {
                        tutorialInfoText.text = tutorialImages.ObjectEnabled ? "\n\n\n" + basicControlsInformation[infoIndex] : basicControlsInformation[infoIndex];
                    }
                    else
                    {
                        tutorialInfoText.text = basicControlsInformation[infoIndex];
                    }
                }
            }
            if(viewingBattleTutorial)
            {
                if(InputManager.instance.ControllerPluggedIn)
                {
                    if(InputManager.instance.ControllerName == "xbox")
                    {
                        if(SteamUtils.IsSteamRunningOnSteamDeck())
                        {
                            if (infoIndex < steamDeckBattleInfo.Count)
                            {
                                infoIndex++;

                                if (tutorialImages != null)
                                {
                                    if (xboxBattleInfo.Count > 5 && steamDeckBattleInfo.Count < 9)
                                    {
                                        tutorialImages.CheckTutorialImageMasterMenu(2);
                                    }
                                    else if (xboxBattleInfo.Count > 10)
                                    {
                                        tutorialImages.CheckTutorialImageMasterMenu(3);
                                    }
                                }

                                pageCountText.text = (1 + infoIndex) + "/" + steamDeckBattleInfo.Count;
                            }

                            if (infoIndex > 0)
                            {
                                prevInfoButton.interactable = true;

                                prevInfoButton.image.raycastTarget = true;

                                prevInfoButtonArrow.color = new Color(1, 1, 1, 1);
                                prevInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);

                                Navigation nextNav = nextInfoButton.GetComponent<Selectable>().navigation;

                                nextNav.selectOnLeft = prevInfoButton;
                                nextNav.selectOnRight = prevInfoButton;

                                nextInfoButton.GetComponent<Selectable>().navigation = nextNav;

                                Navigation prevNav = prevInfoButton.GetComponent<Selectable>().navigation;

                                prevNav.selectOnLeft = nextInfoButton;
                                prevNav.selectOnRight = nextInfoButton;

                                prevInfoButton.GetComponent<Selectable>().navigation = prevNav;
                            }

                            if (infoIndex >= steamDeckBattleInfo.Count - 1)
                            {
                                nextInfoButton.interactable = false;

                                nextInfoButton.image.raycastTarget = false;

                                nextInfoButtonArrow.color = new Color(1, 1, 1, 0.7f);
                                nextInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.7f);

                                InputManager.instance.SetSelectedObject(prevInfoButton.gameObject);

                                nextInfoButton.GetComponent<Animator>().ResetTrigger("Selected");
                                nextInfoButton.GetComponent<Animator>().SetTrigger("Normal");

                                Navigation prevNav = prevInfoButton.GetComponent<Selectable>().navigation;

                                prevNav.selectOnLeft = null;
                                prevNav.selectOnRight = null;

                                prevInfoButton.GetComponent<Selectable>().navigation = prevNav;
                            }
                            else
                            {
                                nextInfoButton.interactable = true;

                                nextInfoButton.image.raycastTarget = true;

                                nextInfoButtonArrow.color = new Color(1, 1, 1, 1);
                                nextInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                            }

                            if (tutorialImages != null)
                            {
                                tutorialInfoText.text = tutorialImages.ObjectEnabled ? "\n\n\n" + steamDeckBattleInfo[infoIndex] : steamDeckBattleInfo[infoIndex];
                            }
                            else
                            {
                                tutorialInfoText.text = steamDeckBattleInfo[infoIndex];
                            }
                        }
                        else
                        {
                            if (infoIndex < xboxBattleInfo.Count)
                            {
                                infoIndex++;

                                if (tutorialImages != null)
                                {
                                    if (xboxBattleInfo.Count > 5 && xboxBattleInfo.Count < 9)
                                    {
                                        tutorialImages.CheckTutorialImageMasterMenu(2);
                                    }
                                    else if (xboxBattleInfo.Count > 10)
                                    {
                                        tutorialImages.CheckTutorialImageMasterMenu(3);
                                    }
                                }

                                pageCountText.text = (1 + infoIndex) + "/" + xboxBattleInfo.Count;
                            }

                            if (infoIndex > 0)
                            {
                                prevInfoButton.interactable = true;

                                prevInfoButton.image.raycastTarget = true;

                                prevInfoButtonArrow.color = new Color(1, 1, 1, 1);
                                prevInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);

                                Navigation nextNav = nextInfoButton.GetComponent<Selectable>().navigation;

                                nextNav.selectOnLeft = prevInfoButton;
                                nextNav.selectOnRight = prevInfoButton;

                                nextInfoButton.GetComponent<Selectable>().navigation = nextNav;

                                Navigation prevNav = prevInfoButton.GetComponent<Selectable>().navigation;

                                prevNav.selectOnLeft = nextInfoButton;
                                prevNav.selectOnRight = nextInfoButton;

                                prevInfoButton.GetComponent<Selectable>().navigation = prevNav;
                            }

                            if (infoIndex >= xboxBattleInfo.Count - 1)
                            {
                                nextInfoButton.interactable = false;

                                nextInfoButton.image.raycastTarget = false;

                                nextInfoButtonArrow.color = new Color(1, 1, 1, 0.7f);
                                nextInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.7f);

                                InputManager.instance.SetSelectedObject(prevInfoButton.gameObject);

                                nextInfoButton.GetComponent<Animator>().ResetTrigger("Selected");
                                nextInfoButton.GetComponent<Animator>().SetTrigger("Normal");

                                Navigation prevNav = prevInfoButton.GetComponent<Selectable>().navigation;

                                prevNav.selectOnLeft = null;
                                prevNav.selectOnRight = null;

                                prevInfoButton.GetComponent<Selectable>().navigation = prevNav;
                            }
                            else
                            {
                                nextInfoButton.interactable = true;

                                nextInfoButton.image.raycastTarget = true;

                                nextInfoButtonArrow.color = new Color(1, 1, 1, 1);
                                nextInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                            }

                            if (tutorialImages != null)
                            {
                                tutorialInfoText.text = tutorialImages.ObjectEnabled ? "\n\n\n" + xboxBattleInfo[infoIndex] : xboxBattleInfo[infoIndex];
                            }
                            else
                            {
                                tutorialInfoText.text = xboxBattleInfo[infoIndex];
                            }
                        }
                    }
                    else
                    {
                        if (infoIndex < playStationBattleInfo.Count)
                        {
                            infoIndex++;

                            if (tutorialImages != null)
                            {
                                if (playStationBattleInfo.Count > 5 && playStationBattleInfo.Count < 9)
                                {
                                    tutorialImages.CheckTutorialImageMasterMenu(2);
                                }
                                else if (playStationBattleInfo.Count > 10)
                                {
                                    tutorialImages.CheckTutorialImageMasterMenu(3);
                                }
                            }

                            pageCountText.text = (1 + infoIndex) + "/" + playStationBattleInfo.Count;
                        }

                        if (infoIndex > 0)
                        {
                            prevInfoButton.interactable = true;

                            prevInfoButton.image.raycastTarget = true;

                            prevInfoButtonArrow.color = new Color(1, 1, 1, 1);
                            prevInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);

                            Navigation nextNav = nextInfoButton.GetComponent<Selectable>().navigation;

                            nextNav.selectOnLeft = prevInfoButton;
                            nextNav.selectOnRight = prevInfoButton;

                            nextInfoButton.GetComponent<Selectable>().navigation = nextNav;

                            Navigation prevNav = prevInfoButton.GetComponent<Selectable>().navigation;

                            prevNav.selectOnLeft = nextInfoButton;
                            prevNav.selectOnRight = nextInfoButton;

                            prevInfoButton.GetComponent<Selectable>().navigation = prevNav;
                        }

                        if (infoIndex >= playStationBattleInfo.Count - 1)
                        {
                            nextInfoButton.interactable = false;

                            nextInfoButton.image.raycastTarget = false;

                            nextInfoButtonArrow.color = new Color(1, 1, 1, 0.7f);
                            nextInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.7f);

                            InputManager.instance.SetSelectedObject(prevInfoButton.gameObject);

                            nextInfoButton.GetComponent<Animator>().ResetTrigger("Selected");
                            nextInfoButton.GetComponent<Animator>().SetTrigger("Normal");

                            Navigation prevNav = prevInfoButton.GetComponent<Selectable>().navigation;

                            prevNav.selectOnLeft = null;
                            prevNav.selectOnRight = null;

                            prevInfoButton.GetComponent<Selectable>().navigation = prevNav;
                        }
                        else
                        {
                            nextInfoButton.interactable = true;

                            nextInfoButton.image.raycastTarget = true;

                            nextInfoButtonArrow.color = new Color(1, 1, 1, 1);
                            nextInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                        }

                        if (tutorialImages != null)
                        {
                            tutorialInfoText.text = tutorialImages.ObjectEnabled ? "\n\n\n" + playStationBattleInfo[infoIndex] : playStationBattleInfo[infoIndex];
                        }
                        else
                        {
                            tutorialInfoText.text = playStationBattleInfo[infoIndex];
                        }
                    }
                }
                else
                {
                    if (infoIndex < battleInformation.Count)
                    {
                        infoIndex++;

                        if (tutorialImages != null)
                        {
                            if (battleInformation.Count > 5 && battleInformation.Count < 9)
                            {
                                tutorialImages.CheckTutorialImageMasterMenu(2);
                            }
                            else if (battleInformation.Count > 10)
                            {
                                tutorialImages.CheckTutorialImageMasterMenu(3);
                            }
                        }

                        pageCountText.text = (1 + infoIndex) + "/" + battleInformation.Count;
                    }

                    if (infoIndex > 0)
                    {
                        prevInfoButton.interactable = true;

                        prevInfoButton.image.raycastTarget = true;

                        prevInfoButtonArrow.color = new Color(1, 1, 1, 1);
                        prevInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);

                        Navigation nextNav = nextInfoButton.GetComponent<Selectable>().navigation;

                        nextNav.selectOnLeft = prevInfoButton;
                        nextNav.selectOnRight = prevInfoButton;

                        nextInfoButton.GetComponent<Selectable>().navigation = nextNav;

                        Navigation prevNav = prevInfoButton.GetComponent<Selectable>().navigation;

                        prevNav.selectOnLeft = nextInfoButton;
                        prevNav.selectOnRight = nextInfoButton;

                        prevInfoButton.GetComponent<Selectable>().navigation = prevNav;
                    }

                    if (infoIndex >= battleInformation.Count - 1)
                    {
                        nextInfoButton.interactable = false;

                        nextInfoButton.image.raycastTarget = false;

                        nextInfoButtonArrow.color = new Color(1, 1, 1, 0.7f);
                        nextInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.7f);

                        Navigation prevNav = prevInfoButton.GetComponent<Selectable>().navigation;

                        prevNav.selectOnLeft = null;
                        prevNav.selectOnRight = null;

                        prevInfoButton.GetComponent<Selectable>().navigation = prevNav;
                    }
                    else
                    {
                        nextInfoButton.interactable = true;

                        nextInfoButton.image.raycastTarget = true;

                        nextInfoButtonArrow.color = new Color(1, 1, 1, 1);
                        nextInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    }

                    if (tutorialImages != null)
                    {
                        tutorialInfoText.text = tutorialImages.ObjectEnabled ? "\n\n\n" + battleInformation[infoIndex] : battleInformation[infoIndex];
                    }
                    else
                    {
                        tutorialInfoText.text = battleInformation[infoIndex];
                    }
                }
            }
            if (viewingCardTutorial)
            {
                if (infoIndex < cardInformation.Length)
                {
                    infoIndex++;

                    pageCountText.text = (1 + infoIndex) + "/" + cardInformation.Length;
                }

                if (infoIndex > 0)
                {
                    prevInfoButton.interactable = true;

                    prevInfoButton.image.raycastTarget = true;

                    prevInfoButtonArrow.color = new Color(1, 1, 1, 1);
                    prevInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);

                    Navigation nextNav = nextInfoButton.GetComponent<Selectable>().navigation;

                    nextNav.selectOnLeft = prevInfoButton;
                    nextNav.selectOnRight = prevInfoButton;

                    nextInfoButton.GetComponent<Selectable>().navigation = nextNav;

                    Navigation prevNav = prevInfoButton.GetComponent<Selectable>().navigation;

                    prevNav.selectOnLeft = nextInfoButton;
                    prevNav.selectOnRight = nextInfoButton;

                    prevInfoButton.GetComponent<Selectable>().navigation = prevNav;
                }

                if (infoIndex >= cardInformation.Length - 1)
                {
                    nextInfoButton.interactable = false;

                    nextInfoButton.image.raycastTarget = false;

                    nextInfoButtonArrow.color = new Color(1, 1, 1, 0.7f);
                    nextInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.7f);

                    InputManager.instance.SetSelectedObject(prevInfoButton.gameObject);

                    nextInfoButton.GetComponent<Animator>().ResetTrigger("Selected");
                    nextInfoButton.GetComponent<Animator>().SetTrigger("Normal");

                    Navigation prevNav = prevInfoButton.GetComponent<Selectable>().navigation;

                    prevNav.selectOnLeft = null;
                    prevNav.selectOnRight = null;

                    prevInfoButton.GetComponent<Selectable>().navigation = prevNav;
                }
                else
                {
                    nextInfoButton.interactable = true;

                    nextInfoButton.image.raycastTarget = true;

                    nextInfoButtonArrow.color = new Color(1, 1, 1, 1);
                    nextInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                }

                tutorialInfoText.text = cardInformation[infoIndex];
            }
            if (viewingStickerTutorial)
            {
                if (infoIndex < stickerInformation.Length)
                {
                    infoIndex++;

                    pageCountText.text = (1 + infoIndex) + "/" + stickerInformation.Length;
                }

                if (infoIndex > 0)
                {
                    prevInfoButton.interactable = true;

                    prevInfoButton.image.raycastTarget = true;

                    prevInfoButtonArrow.color = new Color(1, 1, 1, 1);
                    prevInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);

                    Navigation nextNav = nextInfoButton.GetComponent<Selectable>().navigation;

                    nextNav.selectOnLeft = prevInfoButton;
                    nextNav.selectOnRight = prevInfoButton;

                    nextInfoButton.GetComponent<Selectable>().navigation = nextNav;

                    Navigation prevNav = prevInfoButton.GetComponent<Selectable>().navigation;

                    prevNav.selectOnLeft = nextInfoButton;
                    prevNav.selectOnRight = nextInfoButton;

                    prevInfoButton.GetComponent<Selectable>().navigation = prevNav;
                }

                if (infoIndex >= stickerInformation.Length - 1)
                {
                    nextInfoButton.interactable = false;

                    nextInfoButton.image.raycastTarget = false;

                    nextInfoButtonArrow.color = new Color(1, 1, 1, 0.7f);
                    nextInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.7f);

                    InputManager.instance.SetSelectedObject(prevInfoButton.gameObject);

                    nextInfoButton.GetComponent<Animator>().ResetTrigger("Selected");
                    nextInfoButton.GetComponent<Animator>().SetTrigger("Normal");

                    Navigation prevNav = prevInfoButton.GetComponent<Selectable>().navigation;

                    prevNav.selectOnLeft = null;
                    prevNav.selectOnRight = null;

                    prevInfoButton.GetComponent<Selectable>().navigation = prevNav;
                }
                else
                {
                    nextInfoButton.interactable = true;

                    nextInfoButton.image.raycastTarget = true;

                    nextInfoButtonArrow.color = new Color(1, 1, 1, 1);
                    nextInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                }

                tutorialInfoText.text = stickerInformation[infoIndex];
            }
            if (viewingSecretStageTutorial)
            {
                if (infoIndex < secretAreaInformation.Length)
                {
                    infoIndex++;

                    pageCountText.text = (1 + infoIndex) + "/" + secretAreaInformation.Length;
                }

                if (infoIndex > 0)
                {
                    prevInfoButton.interactable = true;

                    prevInfoButton.image.raycastTarget = true;

                    prevInfoButtonArrow.color = new Color(1, 1, 1, 1f);
                    prevInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);

                    Navigation nextNav = nextInfoButton.GetComponent<Selectable>().navigation;

                    nextNav.selectOnLeft = prevInfoButton;
                    nextNav.selectOnRight = prevInfoButton;

                    nextInfoButton.GetComponent<Selectable>().navigation = nextNav;

                    Navigation prevNav = prevInfoButton.GetComponent<Selectable>().navigation;

                    prevNav.selectOnLeft = nextInfoButton;
                    prevNav.selectOnRight = nextInfoButton;

                    prevInfoButton.GetComponent<Selectable>().navigation = prevNav;
                }

                if (infoIndex >= secretAreaInformation.Length - 1)
                {
                    nextInfoButton.interactable = false;

                    nextInfoButton.image.raycastTarget = false;

                    nextInfoButtonArrow.color = new Color(1, 1, 1, 0.7f);
                    nextInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.7f);

                    InputManager.instance.SetSelectedObject(prevInfoButton.gameObject);

                    nextInfoButton.GetComponent<Animator>().ResetTrigger("Selected");
                    nextInfoButton.GetComponent<Animator>().SetTrigger("Normal");

                    Navigation prevNav = prevInfoButton.GetComponent<Selectable>().navigation;

                    prevNav.selectOnLeft = null;
                    prevNav.selectOnRight = null;

                    prevInfoButton.GetComponent<Selectable>().navigation = prevNav;
                }
                else
                {
                    nextInfoButton.interactable = true;

                    nextInfoButton.image.raycastTarget = true;

                    nextInfoButtonArrow.color = new Color(1, 1, 1, 1);
                    nextInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                }

                tutorialInfoText.text = secretAreaInformation[infoIndex];
            }

            if(!nextInfoButton.interactable)
            {
                MenuController.instance._SettingsMenu.AdjustButtonNavigations();
            }
        }
        else
        {
            if(InputManager.instance.ControllerPluggedIn)
            {
                if(switchToControllerInfo)
                {
                    if (InputManager.instance.ControllerName == "xbox")
                    {
                        if(SteamUtils.IsSteamRunningOnSteamDeck())
                        {
                            if (infoIndex < steamDeckInfo.Count)
                            {
                                infoIndex++;

                                if (tutorialImages != null)
                                {
                                    tutorialImages.CheckTutorialImage();
                                }

                                pageCountText.text = (1 + infoIndex) + "/" + steamDeckInfo.Count;
                            }

                            if (infoIndex > 0)
                            {
                                prevInfoButton.interactable = true;

                                prevInfoButton.image.raycastTarget = true;

                                prevInfoButtonArrow.color = new Color(1, 1, 1, 1);
                                prevInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                            }

                            if (infoIndex >= xboxInfo.Count - 1)
                            {
                                nextInfoButton.interactable = false;

                                nextInfoButton.image.raycastTarget = false;

                                nextInfoButtonArrow.color = new Color(1, 1, 1, 0.7f);
                                nextInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.7f);
                            }
                            else
                            {
                                nextInfoButton.interactable = true;

                                nextInfoButton.image.raycastTarget = true;

                                nextInfoButtonArrow.color = new Color(1, 1, 1, 1);
                                nextInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                            }

                            if (tutorialImages != null)
                            {
                                tutorialInfoText.text = tutorialImages.ObjectEnabled ? "\n\n\n" + steamDeckInfo[infoIndex] : steamDeckInfo[infoIndex];
                            }
                            else
                            {
                                tutorialInfoText.text = steamDeckInfo[infoIndex];
                            }

                            Navigation forwardButtonNav = nextInfoButton.GetComponent<Selectable>().navigation;
                            Navigation previousButtonNav = prevInfoButton.GetComponent<Selectable>().navigation;
                            Navigation exitButtonNav = exitButton.GetComponent<Selectable>().navigation;

                            if (infoIndex > 0 && infoIndex < steamDeckInfo.Count)
                            {
                                forwardButtonNav.selectOnLeft = prevInfoButton;
                                forwardButtonNav.selectOnRight = prevInfoButton;

                                nextInfoButton.GetComponent<Selectable>().navigation = forwardButtonNav;
                            }

                            if (infoIndex >= steamDeckInfo.Count - 1)
                            {
                                if (battleSystem != null)
                                {
                                    battleSystem._InputManager.SetSelectedObject(prevInfoButton.gameObject);
                                }
                                else
                                {
                                    InputManager.instance.SetSelectedObject(prevInfoButton.gameObject);
                                }

                                nextInfoButton.GetComponent<Animator>().ResetTrigger("Selected");
                                nextInfoButton.GetComponent<Animator>().SetTrigger("Normal");

                                previousButtonNav.selectOnLeft = null;
                                previousButtonNav.selectOnRight = null;

                                exitButtonNav.selectOnDown = prevInfoButton;

                                exitButton.GetComponent<Selectable>().navigation = exitButtonNav;
                                prevInfoButton.GetComponent<Selectable>().navigation = previousButtonNav;
                            }
                        }
                        else
                        {
                            if (infoIndex < xboxInfo.Count)
                            {
                                infoIndex++;

                                if (tutorialImages != null)
                                {
                                    tutorialImages.CheckTutorialImage();
                                }

                                pageCountText.text = (1 + infoIndex) + "/" + xboxInfo.Count;
                            }

                            if (infoIndex > 0)
                            {
                                prevInfoButton.interactable = true;

                                prevInfoButton.image.raycastTarget = true;

                                prevInfoButtonArrow.color = new Color(1, 1, 1, 1);
                                prevInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                            }

                            if (infoIndex >= xboxInfo.Count - 1)
                            {
                                nextInfoButton.interactable = false;

                                nextInfoButton.image.raycastTarget = false;

                                nextInfoButtonArrow.color = new Color(1, 1, 1, 0.7f);
                                nextInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.7f);
                            }
                            else
                            {
                                nextInfoButton.interactable = true;

                                nextInfoButton.image.raycastTarget = true;

                                nextInfoButtonArrow.color = new Color(1, 1, 1, 1);
                                nextInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                            }

                            if (tutorialImages != null)
                            {
                                tutorialInfoText.text = tutorialImages.ObjectEnabled ? "\n\n\n" + xboxInfo[infoIndex] : xboxInfo[infoIndex];
                            }
                            else
                            {
                                tutorialInfoText.text = xboxInfo[infoIndex];
                            }

                            Navigation forwardButtonNav = nextInfoButton.GetComponent<Selectable>().navigation;
                            Navigation previousButtonNav = prevInfoButton.GetComponent<Selectable>().navigation;
                            Navigation exitButtonNav = exitButton.GetComponent<Selectable>().navigation;

                            if (infoIndex > 0 && infoIndex < xboxInfo.Count)
                            {
                                forwardButtonNav.selectOnLeft = prevInfoButton;
                                forwardButtonNav.selectOnRight = prevInfoButton;

                                nextInfoButton.GetComponent<Selectable>().navigation = forwardButtonNav;
                            }

                            if (infoIndex >= xboxInfo.Count - 1)
                            {
                                if (battleSystem != null)
                                {
                                    battleSystem._InputManager.SetSelectedObject(prevInfoButton.gameObject);
                                }
                                else
                                {
                                    InputManager.instance.SetSelectedObject(prevInfoButton.gameObject);
                                }

                                nextInfoButton.GetComponent<Animator>().ResetTrigger("Selected");
                                nextInfoButton.GetComponent<Animator>().SetTrigger("Normal");

                                previousButtonNav.selectOnLeft = null;
                                previousButtonNav.selectOnRight = null;

                                exitButtonNav.selectOnDown = prevInfoButton;

                                exitButton.GetComponent<Selectable>().navigation = exitButtonNav;
                                prevInfoButton.GetComponent<Selectable>().navigation = previousButtonNav;
                            }
                        }
                    }
                    else
                    {
                        if (infoIndex < playStationInfo.Count)
                        {
                            infoIndex++;

                            if (tutorialImages != null)
                            {
                                tutorialImages.CheckTutorialImage();
                            }

                            pageCountText.text = (1 + infoIndex) + "/" + playStationInfo.Count;
                        }

                        if (infoIndex > 0)
                        {
                            prevInfoButton.interactable = true;

                            prevInfoButton.image.raycastTarget = true;

                            prevInfoButtonArrow.color = new Color(1, 1, 1, 1);
                            prevInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                        }

                        if (infoIndex >= playStationInfo.Count - 1)
                        {
                            nextInfoButton.interactable = false;

                            nextInfoButton.image.raycastTarget = false;

                            nextInfoButtonArrow.color = new Color(1, 1, 1, 0.7f);
                            nextInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.7f);
                        }
                        else
                        {
                            nextInfoButton.interactable = true;

                            nextInfoButton.image.raycastTarget = true;

                            nextInfoButtonArrow.color = new Color(1, 1, 1, 1);
                            nextInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                        }

                        if(tutorialImages != null)
                        {
                            tutorialInfoText.text = tutorialImages.ObjectEnabled ? "\n\n\n" + playStationInfo[infoIndex] : playStationInfo[infoIndex];
                        }
                        else
                        {
                            tutorialInfoText.text = playStationInfo[infoIndex];
                        }

                        Navigation forwardButtonNav = nextInfoButton.GetComponent<Selectable>().navigation;
                        Navigation previousButtonNav = prevInfoButton.GetComponent<Selectable>().navigation;
                        Navigation exitButtonNav = exitButton.GetComponent<Selectable>().navigation;

                        if (infoIndex > 0 && infoIndex < playStationInfo.Count)
                        {
                            forwardButtonNav.selectOnLeft = prevInfoButton;
                            forwardButtonNav.selectOnRight = prevInfoButton;

                            nextInfoButton.GetComponent<Selectable>().navigation = forwardButtonNav;
                        }

                        if (infoIndex >= playStationInfo.Count - 1)
                        {
                            if (battleSystem != null)
                            {
                                battleSystem._InputManager.SetSelectedObject(prevInfoButton.gameObject);
                            }
                            else
                            {
                                InputManager.instance.SetSelectedObject(prevInfoButton.gameObject);
                            }

                            nextInfoButton.GetComponent<Animator>().ResetTrigger("Selected");
                            nextInfoButton.GetComponent<Animator>().SetTrigger("Normal");

                            previousButtonNav.selectOnLeft = null;
                            previousButtonNav.selectOnRight = null;

                            exitButtonNav.selectOnDown = prevInfoButton;

                            exitButton.GetComponent<Selectable>().navigation = exitButtonNav;
                            prevInfoButton.GetComponent<Selectable>().navigation = previousButtonNav;
                        }
                    }
                }
                else
                {
                    if (infoIndex < information.Count)
                    {
                        infoIndex++;

                        if (tutorialImages != null)
                        {
                            tutorialImages.CheckTutorialImage();
                        }

                        pageCountText.text = (1 + infoIndex) + "/" + information.Count;
                    }

                    if (infoIndex > 0)
                    {
                        prevInfoButton.interactable = true;

                        prevInfoButton.image.raycastTarget = true;

                        prevInfoButtonArrow.color = new Color(1, 1, 1, 1);
                        prevInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    }

                    if (infoIndex >= information.Count - 1)
                    {
                        nextInfoButton.interactable = false;

                        nextInfoButton.image.raycastTarget = false;

                        nextInfoButtonArrow.color = new Color(1, 1, 1, 0.7f);
                        nextInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.7f);
                    }
                    else
                    {
                        nextInfoButton.interactable = true;

                        nextInfoButton.image.raycastTarget = true;

                        nextInfoButtonArrow.color = new Color(1, 1, 1, 1);
                        nextInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    }

                    if(tutorialImages != null)
                    {
                        tutorialInfoText.text = tutorialImages.ObjectEnabled ? "\n\n\n" + information[infoIndex] : information[infoIndex];
                    }
                    else
                    {
                        tutorialInfoText.text = information[infoIndex];
                    }

                    Navigation forwardButtonNav = nextInfoButton.GetComponent<Selectable>().navigation;
                    Navigation previousButtonNav = prevInfoButton.GetComponent<Selectable>().navigation;
                    Navigation exitButtonNav = exitButton.GetComponent<Selectable>().navigation;

                    if (infoIndex > 0 && infoIndex < information.Count)
                    {
                        forwardButtonNav.selectOnLeft = prevInfoButton;
                        forwardButtonNav.selectOnRight = prevInfoButton;

                        nextInfoButton.GetComponent<Selectable>().navigation = forwardButtonNav;
                    }

                    if (infoIndex >= information.Count - 1)
                    {
                        InputManager.instance.SetSelectedObject(prevInfoButton.gameObject);

                        nextInfoButton.GetComponent<Animator>().ResetTrigger("Selected");
                        nextInfoButton.GetComponent<Animator>().SetTrigger("Normal");

                        previousButtonNav.selectOnLeft = null;
                        previousButtonNav.selectOnRight = null;

                        exitButtonNav.selectOnDown = prevInfoButton;

                        exitButton.GetComponent<Selectable>().navigation = exitButtonNav;
                        prevInfoButton.GetComponent<Selectable>().navigation = previousButtonNav;
                    }
                }
            }
            else
            {
                if (infoIndex < information.Count)
                {
                    infoIndex++;

                    if(tutorialImages != null)
                    {
                        tutorialImages.CheckTutorialImage();
                    }

                    pageCountText.text = (1 + infoIndex) + "/" + information.Count;
                }

                if (infoIndex > 0)
                {
                    prevInfoButton.interactable = true;

                    prevInfoButton.image.raycastTarget = true;

                    prevInfoButtonArrow.color = new Color(1, 1, 1, 1);
                    prevInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                }

                if (infoIndex >= information.Count - 1)
                {
                    nextInfoButton.interactable = false;

                    nextInfoButton.image.raycastTarget = false;

                    nextInfoButtonArrow.color = new Color(1, 1, 1, 0.7f);
                    nextInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.7f);
                }
                else
                {
                    nextInfoButton.interactable = true;

                    nextInfoButton.image.raycastTarget = true;

                    nextInfoButtonArrow.color = new Color(1, 1, 1, 1);
                    nextInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                }

                if (tutorialImages != null)
                {
                    tutorialInfoText.text = tutorialImages.ObjectEnabled ? "\n\n\n" + information[infoIndex] : information[infoIndex];
                }
                else
                {
                    tutorialInfoText.text = information[infoIndex];
                }

                Navigation forwardButtonNav = nextInfoButton.GetComponent<Selectable>().navigation;
                Navigation previousButtonNav = prevInfoButton.GetComponent<Selectable>().navigation;
                Navigation exitButtonNav = exitButton.GetComponent<Selectable>().navigation;

                if (infoIndex > 0 && infoIndex < information.Count)
                {
                    forwardButtonNav.selectOnLeft = prevInfoButton;
                    forwardButtonNav.selectOnRight = prevInfoButton;

                    nextInfoButton.GetComponent<Selectable>().navigation = forwardButtonNav;
                }

                if (infoIndex >= information.Count - 1)
                {
                    InputManager.instance.SetSelectedObject(prevInfoButton.gameObject);

                    nextInfoButton.GetComponent<Animator>().ResetTrigger("Selected");
                    nextInfoButton.GetComponent<Animator>().SetTrigger("Normal");

                    previousButtonNav.selectOnLeft = null;
                    previousButtonNav.selectOnRight = null;

                    exitButtonNav.selectOnDown = prevInfoButton;

                    exitButton.GetComponent<Selectable>().navigation = exitButtonNav;
                    prevInfoButton.GetComponent<Selectable>().navigation = previousButtonNav;
                }
            }
        }
    }

    public void PreviousInfoPage()
    {
        if(isInSettingsMenu)
        {
            if (viewingWorldMapTutorial)
            {
                if(InputManager.instance.ControllerPluggedIn)
                {
                    if(InputManager.instance.ControllerName == "xbox")
                    {
                        if (infoIndex < xboxWorldMap.Length)
                        {
                            infoIndex--;

                            if (tutorialImages != null)
                            {
                                tutorialImages.DecrementImageIndex();
                                tutorialImages.CheckTutorialImageMasterMenu(0);
                            }

                            pageCountText.text = (1 + infoIndex) + "/" + xboxWorldMap.Length;
                        }

                        if (infoIndex <= 0)
                        {
                            prevInfoButton.interactable = false;

                            prevInfoButton.image.raycastTarget = false;

                            prevInfoButtonArrow.color = new Color(1, 1, 1, 0.7f);
                            prevInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.7f);

                            InputManager.instance.SetSelectedObject(nextInfoButton.gameObject);

                            Navigation nextNav = nextInfoButton.GetComponent<Selectable>().navigation;

                            nextNav.selectOnLeft = null;
                            nextNav.selectOnRight = null;

                            nextInfoButton.GetComponent<Selectable>().navigation = nextNav;
                        }
                        else
                        {
                            prevInfoButton.interactable = true;

                            prevInfoButton.image.raycastTarget = true;

                            prevInfoButtonArrow.color = new Color(1, 1, 1, 1);
                            prevInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);

                            Navigation nextNav = nextInfoButton.GetComponent<Selectable>().navigation;

                            nextNav.selectOnLeft = prevInfoButton;
                            nextNav.selectOnRight = prevInfoButton;

                            nextInfoButton.GetComponent<Selectable>().navigation = nextNav;

                            Navigation prevNav = prevInfoButton.GetComponent<Selectable>().navigation;

                            prevNav.selectOnLeft = nextInfoButton;
                            prevNav.selectOnRight = nextInfoButton;

                            prevInfoButton.GetComponent<Selectable>().navigation = prevNav;
                        }

                        if (infoIndex < xboxWorldMap.Length - 1)
                        {
                            nextInfoButton.interactable = true;

                            nextInfoButton.image.raycastTarget = true;

                            nextInfoButtonArrow.color = new Color(1, 1, 1, 1);
                            nextInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                        }

                        if (tutorialImages != null)
                        {
                            tutorialInfoText.text = tutorialImages.ObjectEnabled ? "\n\n\n" + xboxWorldMap[infoIndex] : xboxWorldMap[infoIndex];
                        }
                        else
                        {
                            tutorialInfoText.text = xboxWorldMap[infoIndex];
                        }
                    }
                    else
                    {
                        if (infoIndex < playStationWorldMap.Length)
                        {
                            infoIndex--;

                            if (tutorialImages != null)
                            {
                                tutorialImages.DecrementImageIndex();
                                tutorialImages.CheckTutorialImageMasterMenu(0);
                            }

                            pageCountText.text = (1 + infoIndex) + "/" + playStationWorldMap.Length;
                        }

                        if (infoIndex <= 0)
                        {
                            prevInfoButton.interactable = false;

                            prevInfoButton.image.raycastTarget = false;

                            prevInfoButtonArrow.color = new Color(1, 1, 1, 0.7f);
                            prevInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.7f);

                            InputManager.instance.SetSelectedObject(nextInfoButton.gameObject);

                            Navigation nextNav = nextInfoButton.GetComponent<Selectable>().navigation;

                            nextNav.selectOnLeft = null;
                            nextNav.selectOnRight = null;

                            nextInfoButton.GetComponent<Selectable>().navigation = nextNav;
                        }
                        else
                        {
                            prevInfoButton.interactable = true;

                            prevInfoButton.image.raycastTarget = true;

                            prevInfoButtonArrow.color = new Color(1, 1, 1, 1);
                            prevInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);

                            Navigation nextNav = nextInfoButton.GetComponent<Selectable>().navigation;

                            nextNav.selectOnLeft = prevInfoButton;
                            nextNav.selectOnRight = prevInfoButton;

                            nextInfoButton.GetComponent<Selectable>().navigation = nextNav;

                            Navigation prevNav = prevInfoButton.GetComponent<Selectable>().navigation;

                            prevNav.selectOnLeft = nextInfoButton;
                            prevNav.selectOnRight = nextInfoButton;

                            prevInfoButton.GetComponent<Selectable>().navigation = prevNav;
                        }

                        if (infoIndex < playStationWorldMap.Length - 1)
                        {
                            nextInfoButton.interactable = true;

                            nextInfoButton.image.raycastTarget = true;

                            nextInfoButtonArrow.color = new Color(1, 1, 1, 1);
                            nextInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                        }

                        if (tutorialImages != null)
                        {
                            tutorialInfoText.text = tutorialImages.ObjectEnabled ? "\n\n\n" + playStationWorldMap[infoIndex] : playStationWorldMap[infoIndex];
                        }
                        else
                        {
                            tutorialInfoText.text = playStationWorldMap[infoIndex];
                        }
                    }
                }
                else
                {
                    if (infoIndex < worldMapInformation.Length)
                    {
                        infoIndex--;

                        if (tutorialImages != null)
                        {
                            tutorialImages.DecrementImageIndex();
                            tutorialImages.CheckTutorialImageMasterMenu(0);
                        }

                        pageCountText.text = (1 + infoIndex) + "/" + worldMapInformation.Length;
                    }

                    if (infoIndex <= 0)
                    {
                        prevInfoButton.interactable = false;

                        prevInfoButton.image.raycastTarget = false;

                        prevInfoButtonArrow.color = new Color(1, 1, 1, 0.7f);
                        prevInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.7f);

                        InputManager.instance.SetSelectedObject(nextInfoButton.gameObject);

                        Navigation nextNav = nextInfoButton.GetComponent<Selectable>().navigation;

                        nextNav.selectOnLeft = null;
                        nextNav.selectOnRight = null;

                        nextInfoButton.GetComponent<Selectable>().navigation = nextNav;
                    }
                    else
                    {
                        prevInfoButton.interactable = true;

                        prevInfoButton.image.raycastTarget = true;

                        prevInfoButtonArrow.color = new Color(1, 1, 1, 1);
                        prevInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);

                        Navigation nextNav = nextInfoButton.GetComponent<Selectable>().navigation;

                        nextNav.selectOnLeft = prevInfoButton;
                        nextNav.selectOnRight = prevInfoButton;

                        nextInfoButton.GetComponent<Selectable>().navigation = nextNav;

                        Navigation prevNav = prevInfoButton.GetComponent<Selectable>().navigation;

                        prevNav.selectOnLeft = nextInfoButton;
                        prevNav.selectOnRight = nextInfoButton;

                        prevInfoButton.GetComponent<Selectable>().navigation = prevNav;
                    }

                    if (infoIndex < worldMapInformation.Length - 1)
                    {
                        nextInfoButton.interactable = true;

                        nextInfoButton.image.raycastTarget = true;

                        nextInfoButtonArrow.color = new Color(1, 1, 1, 1);
                        nextInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    }

                    if (tutorialImages != null)
                    {
                        tutorialInfoText.text = tutorialImages.ObjectEnabled ? "\n\n\n" + worldMapInformation[infoIndex] : worldMapInformation[infoIndex];
                    }
                    else
                    {
                        tutorialInfoText.text = worldMapInformation[infoIndex];
                    }
                }
            }
            if (viewingTownTutorial)
            {
                if (infoIndex < townInformation.Length)
                {
                    infoIndex--;

                    pageCountText.text = (1 + infoIndex) + "/" + townInformation.Length;
                }

                if (infoIndex <= 0)
                {
                    prevInfoButton.interactable = false;

                    prevInfoButton.image.raycastTarget = false;

                    prevInfoButtonArrow.color = new Color(1, 1, 1, 0.7f);
                    prevInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.7f);

                    InputManager.instance.SetSelectedObject(nextInfoButton.gameObject);

                    Navigation nextNav = nextInfoButton.GetComponent<Selectable>().navigation;

                    nextNav.selectOnLeft = null;
                    nextNav.selectOnRight = null;

                    nextInfoButton.GetComponent<Selectable>().navigation = nextNav;
                }
                else
                {
                    prevInfoButton.interactable = true;

                    prevInfoButton.image.raycastTarget = true;

                    prevInfoButtonArrow.color = new Color(1, 1, 1, 1);
                    prevInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);

                    Navigation nextNav = nextInfoButton.GetComponent<Selectable>().navigation;

                    nextNav.selectOnLeft = prevInfoButton;
                    nextNav.selectOnRight = prevInfoButton;

                    nextInfoButton.GetComponent<Selectable>().navigation = nextNav;

                    Navigation prevNav = prevInfoButton.GetComponent<Selectable>().navigation;

                    prevNav.selectOnLeft = nextInfoButton;
                    prevNav.selectOnRight = nextInfoButton;

                    prevInfoButton.GetComponent<Selectable>().navigation = prevNav;
                }

                if (infoIndex < townInformation.Length - 1)
                {
                    nextInfoButton.interactable = true;

                    nextInfoButton.image.raycastTarget = true;

                    nextInfoButtonArrow.color = new Color(1, 1, 1, 1);
                    nextInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                }

                tutorialInfoText.text = townInformation[infoIndex];
            }
            if (viewingBasicControlsTutorial)
            {
                if(InputManager.instance.ControllerPluggedIn)
                {
                    if(InputManager.instance.ControllerName == "xbox")
                    {
                        if(SteamUtils.IsSteamRunningOnSteamDeck())
                        {
                            if (infoIndex < steamDeckBasicControlsInfo.Length)
                            {
                                infoIndex--;

                                if (tutorialImages != null)
                                {
                                    tutorialImages.DecrementImageIndex();
                                    tutorialImages.CheckTutorialImageMasterMenu(1);
                                }

                                pageCountText.text = (1 + infoIndex) + "/" + steamDeckBasicControlsInfo.Length;
                            }

                            if (infoIndex <= 0)
                            {
                                prevInfoButton.interactable = false;

                                prevInfoButton.image.raycastTarget = false;

                                prevInfoButtonArrow.color = new Color(1, 1, 1, 0.7f);
                                prevInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.7f);

                                InputManager.instance.SetSelectedObject(nextInfoButton.gameObject);

                                Navigation nextNav = nextInfoButton.GetComponent<Selectable>().navigation;

                                nextNav.selectOnLeft = null;
                                nextNav.selectOnRight = null;

                                nextInfoButton.GetComponent<Selectable>().navigation = nextNav;
                            }
                            else
                            {
                                prevInfoButton.interactable = true;

                                prevInfoButton.image.raycastTarget = true;

                                prevInfoButtonArrow.color = new Color(1, 1, 1, 1);
                                prevInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);

                                Navigation nextNav = nextInfoButton.GetComponent<Selectable>().navigation;

                                nextNav.selectOnLeft = prevInfoButton;
                                nextNav.selectOnRight = prevInfoButton;

                                nextInfoButton.GetComponent<Selectable>().navigation = nextNav;

                                Navigation prevNav = prevInfoButton.GetComponent<Selectable>().navigation;

                                prevNav.selectOnLeft = nextInfoButton;
                                prevNav.selectOnRight = nextInfoButton;

                                prevInfoButton.GetComponent<Selectable>().navigation = prevNav;
                            }

                            if (infoIndex < steamDeckBasicControlsInfo.Length - 1)
                            {
                                nextInfoButton.interactable = true;

                                nextInfoButton.image.raycastTarget = true;

                                nextInfoButtonArrow.color = new Color(1, 1, 1, 1);
                                nextInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                            }

                            if (tutorialImages != null)
                            {
                                tutorialInfoText.text = tutorialImages.ObjectEnabled ? "\n\n\n" + steamDeckBasicControlsInfo[infoIndex] : steamDeckBasicControlsInfo[infoIndex];
                            }
                            else
                            {
                                tutorialInfoText.text = steamDeckBasicControlsInfo[infoIndex];
                            }
                        }
                        else
                        {
                            if (infoIndex < xboxBasicControlsInfo.Length)
                            {
                                infoIndex--;

                                if (tutorialImages != null)
                                {
                                    tutorialImages.DecrementImageIndex();
                                    tutorialImages.CheckTutorialImageMasterMenu(1);
                                }

                                pageCountText.text = (1 + infoIndex) + "/" + xboxBasicControlsInfo.Length;
                            }

                            if (infoIndex <= 0)
                            {
                                prevInfoButton.interactable = false;

                                prevInfoButton.image.raycastTarget = false;

                                prevInfoButtonArrow.color = new Color(1, 1, 1, 0.7f);
                                prevInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.7f);

                                InputManager.instance.SetSelectedObject(nextInfoButton.gameObject);

                                Navigation nextNav = nextInfoButton.GetComponent<Selectable>().navigation;

                                nextNav.selectOnLeft = null;
                                nextNav.selectOnRight = null;

                                nextInfoButton.GetComponent<Selectable>().navigation = nextNav;
                            }
                            else
                            {
                                prevInfoButton.interactable = true;

                                prevInfoButton.image.raycastTarget = true;

                                prevInfoButtonArrow.color = new Color(1, 1, 1, 1);
                                prevInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);

                                Navigation nextNav = nextInfoButton.GetComponent<Selectable>().navigation;

                                nextNav.selectOnLeft = prevInfoButton;
                                nextNav.selectOnRight = prevInfoButton;

                                nextInfoButton.GetComponent<Selectable>().navigation = nextNav;

                                Navigation prevNav = prevInfoButton.GetComponent<Selectable>().navigation;

                                prevNav.selectOnLeft = nextInfoButton;
                                prevNav.selectOnRight = nextInfoButton;

                                prevInfoButton.GetComponent<Selectable>().navigation = prevNav;
                            }

                            if (infoIndex < xboxBasicControlsInfo.Length - 1)
                            {
                                nextInfoButton.interactable = true;

                                nextInfoButton.image.raycastTarget = true;

                                nextInfoButtonArrow.color = new Color(1, 1, 1, 1);
                                nextInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                            }

                            if (tutorialImages != null)
                            {
                                tutorialInfoText.text = tutorialImages.ObjectEnabled ? "\n\n\n" + xboxBasicControlsInfo[infoIndex] : xboxBasicControlsInfo[infoIndex];
                            }
                            else
                            {
                                tutorialInfoText.text = xboxBasicControlsInfo[infoIndex];
                            }
                        }
                    }
                    else
                    {
                        if (infoIndex < playStationBasicControlsInfo.Length)
                        {
                            infoIndex--;

                            if (tutorialImages != null)
                            {
                                tutorialImages.DecrementImageIndex();
                                tutorialImages.CheckTutorialImageMasterMenu(1);
                            }

                            pageCountText.text = (1 + infoIndex) + "/" + playStationBasicControlsInfo.Length;
                        }

                        if (infoIndex <= 0)
                        {
                            prevInfoButton.interactable = false;

                            prevInfoButton.image.raycastTarget = false;

                            prevInfoButtonArrow.color = new Color(1, 1, 1, 0.7f);
                            prevInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.7f);

                            InputManager.instance.SetSelectedObject(nextInfoButton.gameObject);

                            Navigation nextNav = nextInfoButton.GetComponent<Selectable>().navigation;

                            nextNav.selectOnLeft = null;
                            nextNav.selectOnRight = null;

                            nextInfoButton.GetComponent<Selectable>().navigation = nextNav;
                        }
                        else
                        {
                            prevInfoButton.interactable = true;

                            prevInfoButton.image.raycastTarget = true;

                            prevInfoButtonArrow.color = new Color(1, 1, 1, 1);
                            prevInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);

                            Navigation nextNav = nextInfoButton.GetComponent<Selectable>().navigation;

                            nextNav.selectOnLeft = prevInfoButton;
                            nextNav.selectOnRight = prevInfoButton;

                            nextInfoButton.GetComponent<Selectable>().navigation = nextNav;

                            Navigation prevNav = prevInfoButton.GetComponent<Selectable>().navigation;

                            prevNav.selectOnLeft = nextInfoButton;
                            prevNav.selectOnRight = nextInfoButton;

                            prevInfoButton.GetComponent<Selectable>().navigation = prevNav;
                        }

                        if (infoIndex < playStationBasicControlsInfo.Length - 1)
                        {
                            nextInfoButton.interactable = true;

                            nextInfoButton.image.raycastTarget = true;

                            nextInfoButtonArrow.color = new Color(1, 1, 1, 1);
                            nextInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                        }

                        if (tutorialImages != null)
                        {
                            tutorialInfoText.text = tutorialImages.ObjectEnabled ? "\n\n\n" + playStationBasicControlsInfo[infoIndex] : playStationBasicControlsInfo[infoIndex];
                        }
                        else
                        {
                            tutorialInfoText.text = playStationBasicControlsInfo[infoIndex];
                        }
                    }
                }
                else
                {
                    if (infoIndex < basicControlsInformation.Length)
                    {
                        infoIndex--;

                        if (tutorialImages != null)
                        {
                            tutorialImages.DecrementImageIndex();
                            tutorialImages.CheckTutorialImageMasterMenu(1);
                        }

                        pageCountText.text = (1 + infoIndex) + "/" + basicControlsInformation.Length;
                    }

                    if (infoIndex <= 0)
                    {
                        prevInfoButton.interactable = false;

                        prevInfoButton.image.raycastTarget = false;

                        prevInfoButtonArrow.color = new Color(1, 1, 1, 0.7f);
                        prevInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.7f);

                        InputManager.instance.SetSelectedObject(nextInfoButton.gameObject);

                        Navigation nextNav = nextInfoButton.GetComponent<Selectable>().navigation;

                        nextNav.selectOnLeft = null;
                        nextNav.selectOnRight = null;

                        nextInfoButton.GetComponent<Selectable>().navigation = nextNav;
                    }
                    else
                    {
                        prevInfoButton.interactable = true;

                        prevInfoButton.image.raycastTarget = true;

                        prevInfoButtonArrow.color = new Color(1, 1, 1, 1);
                        prevInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);

                        Navigation nextNav = nextInfoButton.GetComponent<Selectable>().navigation;

                        nextNav.selectOnLeft = prevInfoButton;
                        nextNav.selectOnRight = prevInfoButton;

                        nextInfoButton.GetComponent<Selectable>().navigation = nextNav;

                        Navigation prevNav = prevInfoButton.GetComponent<Selectable>().navigation;

                        prevNav.selectOnLeft = nextInfoButton;
                        prevNav.selectOnRight = nextInfoButton;

                        prevInfoButton.GetComponent<Selectable>().navigation = prevNav;
                    }

                    if (infoIndex < basicControlsInformation.Length - 1)
                    {
                        nextInfoButton.interactable = true;

                        nextInfoButton.image.raycastTarget = true;

                        nextInfoButtonArrow.color = new Color(1, 1, 1, 1);
                        nextInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    }

                    if (tutorialImages != null)
                    {
                        tutorialInfoText.text = tutorialImages.ObjectEnabled ? "\n\n\n" + basicControlsInformation[infoIndex] : basicControlsInformation[infoIndex];
                    }
                    else
                    {
                        tutorialInfoText.text = basicControlsInformation[infoIndex];
                    }
                }
            }
            if (viewingBattleTutorial)
            {
                if(InputManager.instance.ControllerPluggedIn)
                {
                    if(InputManager.instance.ControllerName == "xbox")
                    {
                        if(SteamUtils.IsSteamRunningOnSteamDeck())
                        {
                            if (infoIndex < steamDeckBattleInfo.Count)
                            {
                                infoIndex--;

                                if (tutorialImages != null)
                                {
                                    if (steamDeckBattleInfo.Count > 5 && steamDeckBattleInfo.Count < 9)
                                    {
                                        tutorialImages.DecrementImageIndex();
                                        tutorialImages.CheckTutorialImageMasterMenu(2);
                                    }
                                    else if (steamDeckBattleInfo.Count > 10)
                                    {
                                        tutorialImages.DecrementImageIndex();
                                        tutorialImages.CheckTutorialImageMasterMenu(3);
                                    }
                                }

                                pageCountText.text = (1 + infoIndex) + "/" + steamDeckBattleInfo.Count;
                            }

                            if (infoIndex <= 0)
                            {
                                prevInfoButton.interactable = false;

                                prevInfoButton.image.raycastTarget = false;

                                prevInfoButtonArrow.color = new Color(1, 1, 1, 0.7f);
                                prevInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.7f);

                                InputManager.instance.SetSelectedObject(nextInfoButton.gameObject);

                                Navigation nextNav = nextInfoButton.GetComponent<Selectable>().navigation;

                                nextNav.selectOnLeft = null;
                                nextNav.selectOnRight = null;

                                nextInfoButton.GetComponent<Selectable>().navigation = nextNav;
                            }
                            else
                            {
                                prevInfoButton.interactable = true;

                                prevInfoButton.image.raycastTarget = true;

                                prevInfoButtonArrow.color = new Color(1, 1, 1, 1);
                                prevInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);

                                Navigation nextNav = nextInfoButton.GetComponent<Selectable>().navigation;

                                nextNav.selectOnLeft = prevInfoButton;
                                nextNav.selectOnRight = prevInfoButton;

                                nextInfoButton.GetComponent<Selectable>().navigation = nextNav;

                                Navigation prevNav = prevInfoButton.GetComponent<Selectable>().navigation;

                                prevNav.selectOnLeft = nextInfoButton;
                                prevNav.selectOnRight = nextInfoButton;

                                prevInfoButton.GetComponent<Selectable>().navigation = prevNav;
                            }

                            if (infoIndex < steamDeckBattleInfo.Count - 1)
                            {
                                nextInfoButton.interactable = true;

                                nextInfoButton.image.raycastTarget = true;

                                nextInfoButtonArrow.color = new Color(1, 1, 1, 1);
                                nextInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                            }

                            if (tutorialImages != null)
                            {
                                tutorialInfoText.text = tutorialImages.ObjectEnabled ? "\n\n\n" + steamDeckBattleInfo[infoIndex] : steamDeckBattleInfo[infoIndex];
                            }
                            else
                            {
                                tutorialInfoText.text = steamDeckBattleInfo[infoIndex];
                            }
                        }
                        else
                        {
                            if (infoIndex < xboxBattleInfo.Count)
                            {
                                infoIndex--;

                                if (tutorialImages != null)
                                {
                                    if (xboxBattleInfo.Count > 5 && xboxBattleInfo.Count < 9)
                                    {
                                        tutorialImages.DecrementImageIndex();
                                        tutorialImages.CheckTutorialImageMasterMenu(2);
                                    }
                                    else if (xboxBattleInfo.Count > 10)
                                    {
                                        tutorialImages.DecrementImageIndex();
                                        tutorialImages.CheckTutorialImageMasterMenu(3);
                                    }
                                }

                                pageCountText.text = (1 + infoIndex) + "/" + xboxBattleInfo.Count;
                            }

                            if (infoIndex <= 0)
                            {
                                prevInfoButton.interactable = false;

                                prevInfoButton.image.raycastTarget = false;

                                prevInfoButtonArrow.color = new Color(1, 1, 1, 0.7f);
                                prevInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.7f);

                                InputManager.instance.SetSelectedObject(nextInfoButton.gameObject);

                                Navigation nextNav = nextInfoButton.GetComponent<Selectable>().navigation;

                                nextNav.selectOnLeft = null;
                                nextNav.selectOnRight = null;

                                nextInfoButton.GetComponent<Selectable>().navigation = nextNav;
                            }
                            else
                            {
                                prevInfoButton.interactable = true;

                                prevInfoButton.image.raycastTarget = true;

                                prevInfoButtonArrow.color = new Color(1, 1, 1, 1);
                                prevInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);

                                Navigation nextNav = nextInfoButton.GetComponent<Selectable>().navigation;

                                nextNav.selectOnLeft = prevInfoButton;
                                nextNav.selectOnRight = prevInfoButton;

                                nextInfoButton.GetComponent<Selectable>().navigation = nextNav;

                                Navigation prevNav = prevInfoButton.GetComponent<Selectable>().navigation;

                                prevNav.selectOnLeft = nextInfoButton;
                                prevNav.selectOnRight = nextInfoButton;

                                prevInfoButton.GetComponent<Selectable>().navigation = prevNav;
                            }

                            if (infoIndex < xboxBattleInfo.Count - 1)
                            {
                                nextInfoButton.interactable = true;

                                nextInfoButton.image.raycastTarget = true;

                                nextInfoButtonArrow.color = new Color(1, 1, 1, 1);
                                nextInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                            }

                            if (tutorialImages != null)
                            {
                                tutorialInfoText.text = tutorialImages.ObjectEnabled ? "\n\n\n" + xboxBattleInfo[infoIndex] : xboxBattleInfo[infoIndex];
                            }
                            else
                            {
                                tutorialInfoText.text = xboxBattleInfo[infoIndex];
                            }
                        }
                    }
                    else
                    {
                        if (infoIndex < playStationBattleInfo.Count)
                        {
                            infoIndex--;

                            if (tutorialImages != null)
                            {
                                if (playStationBattleInfo.Count > 5 && playStationBattleInfo.Count < 9)
                                {
                                    tutorialImages.DecrementImageIndex();
                                    tutorialImages.CheckTutorialImageMasterMenu(2);
                                }
                                else if (playStationBattleInfo.Count > 10)
                                {
                                    tutorialImages.DecrementImageIndex();
                                    tutorialImages.CheckTutorialImageMasterMenu(3);
                                }
                            }

                            pageCountText.text = (1 + infoIndex) + "/" + playStationBattleInfo.Count;
                        }

                        if (infoIndex <= 0)
                        {
                            prevInfoButton.interactable = false;

                            prevInfoButton.image.raycastTarget = false;

                            prevInfoButtonArrow.color = new Color(1, 1, 1, 0.7f);
                            prevInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.7f);

                            InputManager.instance.SetSelectedObject(nextInfoButton.gameObject);

                            Navigation nextNav = nextInfoButton.GetComponent<Selectable>().navigation;

                            nextNav.selectOnLeft = null;
                            nextNav.selectOnRight = null;

                            nextInfoButton.GetComponent<Selectable>().navigation = nextNav;
                        }
                        else
                        {
                            prevInfoButton.interactable = true;

                            prevInfoButton.image.raycastTarget = true;

                            prevInfoButtonArrow.color = new Color(1, 1, 1, 1);
                            prevInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);

                            Navigation nextNav = nextInfoButton.GetComponent<Selectable>().navigation;

                            nextNav.selectOnLeft = prevInfoButton;
                            nextNav.selectOnRight = prevInfoButton;

                            nextInfoButton.GetComponent<Selectable>().navigation = nextNav;

                            Navigation prevNav = prevInfoButton.GetComponent<Selectable>().navigation;

                            prevNav.selectOnLeft = nextInfoButton;
                            prevNav.selectOnRight = nextInfoButton;

                            prevInfoButton.GetComponent<Selectable>().navigation = prevNav;
                        }

                        if (infoIndex < playStationBattleInfo.Count - 1)
                        {
                            nextInfoButton.interactable = true;

                            nextInfoButton.image.raycastTarget = true;

                            nextInfoButtonArrow.color = new Color(1, 1, 1, 1);
                            nextInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                        }

                        if (tutorialImages != null)
                        {
                            tutorialInfoText.text = tutorialImages.ObjectEnabled ? "\n\n\n" + playStationBattleInfo[infoIndex] : playStationBattleInfo[infoIndex];
                        }
                        else
                        {
                            tutorialInfoText.text = playStationBattleInfo[infoIndex];
                        }
                    }
                }
                else
                {
                    if (infoIndex < battleInformation.Count)
                    {
                        infoIndex--;

                        if (tutorialImages != null)
                        {
                            if (battleInformation.Count > 5 && battleInformation.Count < 9)
                            {
                                tutorialImages.DecrementImageIndex();
                                tutorialImages.CheckTutorialImageMasterMenu(2);
                            }
                            else if (battleInformation.Count > 10)
                            {
                                tutorialImages.DecrementImageIndex();
                                tutorialImages.CheckTutorialImageMasterMenu(3);
                            }
                        }

                        pageCountText.text = (1 + infoIndex) + "/" + battleInformation.Count;
                    }

                    if (infoIndex <= 0)
                    {
                        prevInfoButton.interactable = false;

                        prevInfoButton.image.raycastTarget = false;

                        prevInfoButtonArrow.color = new Color(1, 1, 1, 0.7f);
                        prevInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.7f);

                        InputManager.instance.SetSelectedObject(nextInfoButton.gameObject);

                        Navigation nextNav = nextInfoButton.GetComponent<Selectable>().navigation;

                        nextNav.selectOnLeft = null;
                        nextNav.selectOnRight = null;

                        nextInfoButton.GetComponent<Selectable>().navigation = nextNav;
                    }
                    else
                    {
                        prevInfoButton.interactable = true;

                        prevInfoButton.image.raycastTarget = true;

                        prevInfoButtonArrow.color = new Color(1, 1, 1, 1);
                        prevInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);

                        Navigation nextNav = nextInfoButton.GetComponent<Selectable>().navigation;

                        nextNav.selectOnLeft = prevInfoButton;
                        nextNav.selectOnRight = prevInfoButton;

                        nextInfoButton.GetComponent<Selectable>().navigation = nextNav;

                        Navigation prevNav = prevInfoButton.GetComponent<Selectable>().navigation;

                        prevNav.selectOnLeft = nextInfoButton;
                        prevNav.selectOnRight = nextInfoButton;

                        prevInfoButton.GetComponent<Selectable>().navigation = prevNav;
                    }

                    if (infoIndex < battleInformation.Count - 1)
                    {
                        nextInfoButton.interactable = true;

                        nextInfoButton.image.raycastTarget = true;

                        nextInfoButtonArrow.color = new Color(1, 1, 1, 1);
                        nextInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    }

                    if (tutorialImages != null)
                    {
                        tutorialInfoText.text = tutorialImages.ObjectEnabled ? "\n\n\n" + battleInformation[infoIndex] : battleInformation[infoIndex];
                    }
                    else
                    {
                        tutorialInfoText.text = battleInformation[infoIndex];
                    }
                }
            }
            if (viewingCardTutorial)
            {
                if (infoIndex < cardInformation.Length)
                {
                    infoIndex--;

                    pageCountText.text = (1 + infoIndex) + "/" + cardInformation.Length;
                }

                if (infoIndex <= 0)
                {
                    prevInfoButton.interactable = false;

                    prevInfoButton.image.raycastTarget = false;

                    prevInfoButtonArrow.color = new Color(1, 1, 1, 0.7f);
                    prevInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.7f);

                    InputManager.instance.SetSelectedObject(nextInfoButton.gameObject);

                    Navigation nextNav = nextInfoButton.GetComponent<Selectable>().navigation;

                    nextNav.selectOnLeft = null;
                    nextNav.selectOnRight = null;

                    nextInfoButton.GetComponent<Selectable>().navigation = nextNav;
                }
                else
                {
                    prevInfoButton.interactable = true;

                    prevInfoButton.image.raycastTarget = true;

                    prevInfoButtonArrow.color = new Color(1, 1, 1, 1);
                    prevInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);

                    Navigation nextNav = nextInfoButton.GetComponent<Selectable>().navigation;

                    nextNav.selectOnLeft = prevInfoButton;
                    nextNav.selectOnRight = prevInfoButton;

                    nextInfoButton.GetComponent<Selectable>().navigation = nextNav;

                    Navigation prevNav = prevInfoButton.GetComponent<Selectable>().navigation;

                    prevNav.selectOnLeft = nextInfoButton;
                    prevNav.selectOnRight = nextInfoButton;

                    prevInfoButton.GetComponent<Selectable>().navigation = prevNav;
                }

                if (infoIndex < cardInformation.Length - 1)
                {
                    nextInfoButton.interactable = true;

                    nextInfoButton.image.raycastTarget = true;

                    nextInfoButtonArrow.color = new Color(1, 1, 1, 1);
                    nextInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                }

                tutorialInfoText.text = cardInformation[infoIndex];
            }
            if (viewingStickerTutorial)
            {
                if (infoIndex < stickerInformation.Length)
                {
                    infoIndex--;

                    pageCountText.text = (1 + infoIndex) + "/" + stickerInformation.Length;
                }

                if (infoIndex <= 0)
                {
                    prevInfoButton.interactable = false;

                    prevInfoButton.image.raycastTarget = false;

                    prevInfoButtonArrow.color = new Color(1, 1, 1, 0.7f);
                    prevInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.7f);

                    InputManager.instance.SetSelectedObject(nextInfoButton.gameObject);

                    Navigation nextNav = nextInfoButton.GetComponent<Selectable>().navigation;

                    nextNav.selectOnLeft = null;
                    nextNav.selectOnRight = null;

                    nextInfoButton.GetComponent<Selectable>().navigation = nextNav;
                }
                else
                {
                    prevInfoButton.interactable = true;

                    prevInfoButton.image.raycastTarget = true;

                    prevInfoButtonArrow.color = new Color(1, 1, 1, 1);
                    prevInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);

                    Navigation nextNav = nextInfoButton.GetComponent<Selectable>().navigation;

                    nextNav.selectOnLeft = prevInfoButton;
                    nextNav.selectOnRight = prevInfoButton;

                    nextInfoButton.GetComponent<Selectable>().navigation = nextNav;

                    Navigation prevNav = prevInfoButton.GetComponent<Selectable>().navigation;

                    prevNav.selectOnLeft = nextInfoButton;
                    prevNav.selectOnRight = nextInfoButton;

                    prevInfoButton.GetComponent<Selectable>().navigation = prevNav;
                }

                if (infoIndex < stickerInformation.Length - 1)
                {
                    nextInfoButton.interactable = true;

                    nextInfoButton.image.raycastTarget = true;

                    nextInfoButtonArrow.color = new Color(1, 1, 1, 1);
                    nextInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                }

                tutorialInfoText.text = stickerInformation[infoIndex];
            }
            if (viewingSecretStageTutorial)
            {
                if (infoIndex < secretAreaInformation.Length)
                {
                    infoIndex--;

                    pageCountText.text = (1 + infoIndex) + "/" + secretAreaInformation.Length;
                }

                if (infoIndex <= 0)
                {
                    prevInfoButton.interactable = false;

                    prevInfoButton.image.raycastTarget = false;

                    prevInfoButtonArrow.color = new Color(1, 1, 1, 0.7f);
                    prevInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.7f);

                    InputManager.instance.SetSelectedObject(nextInfoButton.gameObject);

                    Navigation nextNav = nextInfoButton.GetComponent<Selectable>().navigation;

                    nextNav.selectOnLeft = null;
                    nextNav.selectOnRight = null;

                    nextInfoButton.GetComponent<Selectable>().navigation = nextNav;
                }
                else
                {
                    prevInfoButton.interactable = true;

                    prevInfoButton.image.raycastTarget = true;

                    prevInfoButtonArrow.color = new Color(1, 1, 1, 1);
                    prevInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);

                    Navigation nextNav = nextInfoButton.GetComponent<Selectable>().navigation;

                    nextNav.selectOnLeft = prevInfoButton;
                    nextNav.selectOnRight = prevInfoButton;

                    nextInfoButton.GetComponent<Selectable>().navigation = nextNav;

                    Navigation prevNav = prevInfoButton.GetComponent<Selectable>().navigation;

                    prevNav.selectOnLeft = nextInfoButton;
                    prevNav.selectOnRight = nextInfoButton;

                    prevInfoButton.GetComponent<Selectable>().navigation = prevNav;
                }

                if (infoIndex < secretAreaInformation.Length - 1)
                {
                    nextInfoButton.interactable = true;

                    nextInfoButton.image.raycastTarget = true;

                    nextInfoButtonArrow.color = new Color(1, 1, 1, 1);
                    nextInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                }

                tutorialInfoText.text = secretAreaInformation[infoIndex];
            }

            if (!prevInfoButton.interactable)
            {
                MenuController.instance._SettingsMenu.AdjustButtonNavigations();

                prevInfoButton.GetComponent<Animator>().ResetTrigger("Selected");
                prevInfoButton.GetComponent<Animator>().SetTrigger("Normal");
            }
        }
        else
        {
            if(InputManager.instance.ControllerPluggedIn)
            {
                if(switchToControllerInfo)
                {
                    if(InputManager.instance.ControllerName == "xbox")
                    {
                        if(SteamUtils.IsSteamRunningOnSteamDeck())
                        {
                            if (infoIndex <= steamDeckInfo.Count)
                            {
                                infoIndex--;

                                if (tutorialImages != null)
                                {
                                    tutorialImages.DecrementImageIndex();
                                    tutorialImages.CheckTutorialImage();
                                }

                                pageCountText.text = (1 + infoIndex) + "/" + steamDeckInfo.Count;
                            }

                            if (infoIndex <= 0)
                            {
                                prevInfoButton.interactable = false;

                                prevInfoButton.image.raycastTarget = false;

                                prevInfoButtonArrow.color = new Color(1, 1, 1, 0.7f);
                                prevInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.7f);
                            }
                            else
                            {
                                prevInfoButton.interactable = true;

                                prevInfoButton.image.raycastTarget = true;

                                prevInfoButtonArrow.color = new Color(1, 1, 1, 1);
                                prevInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                            }

                            if (infoIndex < information.Count - 1)
                            {
                                nextInfoButton.interactable = true;

                                nextInfoButton.image.raycastTarget = true;

                                nextInfoButtonArrow.color = new Color(1, 1, 1, 1);
                                nextInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                            }

                            if (tutorialImages != null)
                            {
                                tutorialInfoText.text = tutorialImages.ObjectEnabled ? "\n\n\n" + steamDeckInfo[infoIndex] : steamDeckInfo[infoIndex];
                            }
                            else
                            {
                                tutorialInfoText.text = steamDeckInfo[infoIndex];
                            }

                            Navigation forwardButtonNav = nextInfoButton.GetComponent<Selectable>().navigation;
                            Navigation previousButtonNav = prevInfoButton.GetComponent<Selectable>().navigation;
                            Navigation exitButtonNav = exitButton.GetComponent<Selectable>().navigation;

                            if (infoIndex < steamDeckInfo.Count - 1)
                            {
                                previousButtonNav.selectOnLeft = nextInfoButton;
                                previousButtonNav.selectOnRight = nextInfoButton;

                                prevInfoButton.GetComponent<Selectable>().navigation = previousButtonNav;
                            }

                            if (infoIndex <= 0)
                            {
                                if (battleSystem != null)
                                {
                                    battleSystem._InputManager.SetSelectedObject(nextInfoButton.gameObject);
                                }
                                else
                                {
                                    InputManager.instance.SetSelectedObject(nextInfoButton.gameObject);
                                }

                                prevInfoButton.GetComponent<Animator>().ResetTrigger("Selected");
                                prevInfoButton.GetComponent<Animator>().SetTrigger("Normal");

                                forwardButtonNav.selectOnLeft = null;
                                forwardButtonNav.selectOnRight = null;

                                exitButtonNav.selectOnDown = nextInfoButton;

                                exitButton.GetComponent<Selectable>().navigation = exitButtonNav;
                                nextInfoButton.GetComponent<Selectable>().navigation = forwardButtonNav;
                            }
                        }
                        else
                        {
                            if (infoIndex <= xboxInfo.Count)
                            {
                                infoIndex--;

                                if (tutorialImages != null)
                                {
                                    tutorialImages.DecrementImageIndex();
                                    tutorialImages.CheckTutorialImage();
                                }

                                pageCountText.text = (1 + infoIndex) + "/" + xboxInfo.Count;
                            }

                            if (infoIndex <= 0)
                            {
                                prevInfoButton.interactable = false;

                                prevInfoButton.image.raycastTarget = false;

                                prevInfoButtonArrow.color = new Color(1, 1, 1, 0.7f);
                                prevInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.7f);
                            }
                            else
                            {
                                prevInfoButton.interactable = true;

                                prevInfoButton.image.raycastTarget = true;

                                prevInfoButtonArrow.color = new Color(1, 1, 1, 1);
                                prevInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                            }

                            if (infoIndex < xboxInfo.Count - 1)
                            {
                                nextInfoButton.interactable = true;

                                nextInfoButton.image.raycastTarget = true;

                                nextInfoButtonArrow.color = new Color(1, 1, 1, 1);
                                nextInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                            }

                            if (tutorialImages != null)
                            {
                                tutorialInfoText.text = tutorialImages.ObjectEnabled ? "\n\n\n" + xboxInfo[infoIndex] : xboxInfo[infoIndex];
                            }
                            else
                            {
                                tutorialInfoText.text = xboxInfo[infoIndex];
                            }

                            Navigation forwardButtonNav = nextInfoButton.GetComponent<Selectable>().navigation;
                            Navigation previousButtonNav = prevInfoButton.GetComponent<Selectable>().navigation;
                            Navigation exitButtonNav = exitButton.GetComponent<Selectable>().navigation;

                            if (infoIndex < information.Count - 1)
                            {
                                previousButtonNav.selectOnLeft = nextInfoButton;
                                previousButtonNav.selectOnRight = nextInfoButton;

                                prevInfoButton.GetComponent<Selectable>().navigation = previousButtonNav;
                            }

                            if (infoIndex <= 0)
                            {
                                if (battleSystem != null)
                                {
                                    battleSystem._InputManager.SetSelectedObject(nextInfoButton.gameObject);
                                }
                                else
                                {
                                    InputManager.instance.SetSelectedObject(nextInfoButton.gameObject);
                                }

                                prevInfoButton.GetComponent<Animator>().ResetTrigger("Selected");
                                prevInfoButton.GetComponent<Animator>().SetTrigger("Normal");

                                forwardButtonNav.selectOnLeft = null;
                                forwardButtonNav.selectOnRight = null;

                                exitButtonNav.selectOnDown = nextInfoButton;

                                exitButton.GetComponent<Selectable>().navigation = exitButtonNav;
                                nextInfoButton.GetComponent<Selectable>().navigation = forwardButtonNav;
                            }
                        }
                    }
                    else
                    {
                        if (infoIndex <= playStationInfo.Count)
                        {
                            infoIndex--;

                            if (tutorialImages != null)
                            {
                                tutorialImages.DecrementImageIndex();
                                tutorialImages.CheckTutorialImage();
                            }

                            pageCountText.text = (1 + infoIndex) + "/" + playStationInfo.Count;
                        }

                        if (infoIndex <= 0)
                        {
                            prevInfoButton.interactable = false;

                            prevInfoButton.image.raycastTarget = false;

                            prevInfoButtonArrow.color = new Color(1, 1, 1, 0.7f);
                            prevInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.7f);
                        }
                        else
                        {
                            prevInfoButton.interactable = true;

                            prevInfoButton.image.raycastTarget = true;

                            prevInfoButtonArrow.color = new Color(1, 1, 1, 1);
                            prevInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                        }

                        if (infoIndex < playStationInfo.Count - 1)
                        {
                            nextInfoButton.interactable = true;

                            nextInfoButton.image.raycastTarget = true;

                            nextInfoButtonArrow.color = new Color(1, 1, 1, 1);
                            nextInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                        }

                        if (tutorialImages != null)
                        {
                            tutorialInfoText.text = tutorialImages.ObjectEnabled ? "\n\n\n" + playStationInfo[infoIndex] : playStationInfo[infoIndex];
                        }
                        else
                        {
                            tutorialInfoText.text = playStationInfo[infoIndex];
                        }

                        Navigation forwardButtonNav = nextInfoButton.GetComponent<Selectable>().navigation;
                        Navigation previousButtonNav = prevInfoButton.GetComponent<Selectable>().navigation;
                        Navigation exitButtonNav = exitButton.GetComponent<Selectable>().navigation;

                        if (infoIndex < playStationInfo.Count - 1)
                        {
                            previousButtonNav.selectOnLeft = nextInfoButton;
                            previousButtonNav.selectOnRight = nextInfoButton;

                            prevInfoButton.GetComponent<Selectable>().navigation = previousButtonNav;
                        }

                        if (infoIndex <= 0)
                        {
                            if (battleSystem != null)
                            {
                                battleSystem._InputManager.SetSelectedObject(nextInfoButton.gameObject);
                            }
                            else
                            {
                                InputManager.instance.SetSelectedObject(nextInfoButton.gameObject);
                            }

                            prevInfoButton.GetComponent<Animator>().ResetTrigger("Selected");
                            prevInfoButton.GetComponent<Animator>().SetTrigger("Normal");

                            forwardButtonNav.selectOnLeft = null;
                            forwardButtonNav.selectOnRight = null;

                            exitButtonNav.selectOnDown = nextInfoButton;

                            exitButton.GetComponent<Selectable>().navigation = exitButtonNav;
                            nextInfoButton.GetComponent<Selectable>().navigation = forwardButtonNav;
                        }
                    }
                }
                else
                {
                    if (infoIndex <= information.Count)
                    {
                        infoIndex--;

                        if (tutorialImages != null)
                        {
                            tutorialImages.CheckTutorialImage();
                        }

                        pageCountText.text = (1 + infoIndex) + "/" + information.Count;
                    }

                    if (infoIndex <= 0)
                    {
                        prevInfoButton.interactable = false;

                        prevInfoButton.image.raycastTarget = false;

                        prevInfoButtonArrow.color = new Color(1, 1, 1, 0.7f);
                        prevInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.7f);
                    }
                    else
                    {
                        prevInfoButton.interactable = true;

                        prevInfoButton.image.raycastTarget = true;

                        prevInfoButtonArrow.color = new Color(1, 1, 1, 1);
                        prevInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    }

                    if (infoIndex < information.Count - 1)
                    {
                        nextInfoButton.interactable = true;

                        nextInfoButton.image.raycastTarget = true;

                        nextInfoButtonArrow.color = new Color(1, 1, 1, 1);
                        nextInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    }

                    if (tutorialImages != null)
                    {
                        tutorialInfoText.text = tutorialImages.ObjectEnabled ? "\n\n\n" + information[infoIndex] : information[infoIndex];
                    }
                    else
                    {
                        tutorialInfoText.text = information[infoIndex];
                    }

                    Navigation forwardButtonNav = nextInfoButton.GetComponent<Selectable>().navigation;
                    Navigation previousButtonNav = prevInfoButton.GetComponent<Selectable>().navigation;
                    Navigation exitButtonNav = exitButton.GetComponent<Selectable>().navigation;

                    if (infoIndex < information.Count - 1)
                    {
                        previousButtonNav.selectOnLeft = nextInfoButton;
                        previousButtonNav.selectOnRight = nextInfoButton;

                        prevInfoButton.GetComponent<Selectable>().navigation = previousButtonNav;
                    }

                    if (infoIndex <= 0)
                    {
                        InputManager.instance.SetSelectedObject(nextInfoButton.gameObject);

                        prevInfoButton.GetComponent<Animator>().ResetTrigger("Selected");
                        prevInfoButton.GetComponent<Animator>().SetTrigger("Normal");

                        forwardButtonNav.selectOnLeft = null;
                        forwardButtonNav.selectOnRight = null;

                        exitButtonNav.selectOnDown = nextInfoButton;

                        exitButton.GetComponent<Selectable>().navigation = exitButtonNav;
                        nextInfoButton.GetComponent<Selectable>().navigation = forwardButtonNav;
                    }
                }
            }
            else
            {
                if (infoIndex <= information.Count)
                {
                    infoIndex--;

                    if (tutorialImages != null)
                    {
                        tutorialImages.CheckTutorialImage();
                    }

                    pageCountText.text = (1 + infoIndex) + "/" + information.Count;
                }

                if (infoIndex <= 0)
                {
                    prevInfoButton.interactable = false;

                    prevInfoButton.image.raycastTarget = false;

                    prevInfoButtonArrow.color = new Color(1, 1, 1, 0.7f);
                    prevInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.7f);
                }
                else
                {
                    prevInfoButton.interactable = true;

                    prevInfoButton.image.raycastTarget = true;

                    prevInfoButtonArrow.color = new Color(1, 1, 1, 1);
                    prevInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                }

                if (infoIndex < information.Count - 1)
                {
                    nextInfoButton.interactable = true;

                    nextInfoButton.image.raycastTarget = true;

                    nextInfoButtonArrow.color = new Color(1, 1, 1, 1);
                    nextInfoButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                }

                if (tutorialImages != null)
                {
                    tutorialInfoText.text = tutorialImages.ObjectEnabled ? "\n\n\n" + information[infoIndex] : information[infoIndex];
                }
                else
                {
                    tutorialInfoText.text = information[infoIndex];
                }

                Navigation forwardButtonNav = nextInfoButton.GetComponent<Selectable>().navigation;
                Navigation previousButtonNav = prevInfoButton.GetComponent<Selectable>().navigation;
                Navigation exitButtonNav = exitButton.GetComponent<Selectable>().navigation;

                if (infoIndex < information.Count - 1)
                {
                    previousButtonNav.selectOnLeft = nextInfoButton;
                    previousButtonNav.selectOnRight = nextInfoButton;

                    prevInfoButton.GetComponent<Selectable>().navigation = previousButtonNav;
                }

                if (infoIndex <= 0)
                {
                    InputManager.instance.SetSelectedObject(nextInfoButton.gameObject);

                    prevInfoButton.GetComponent<Animator>().ResetTrigger("Selected");
                    prevInfoButton.GetComponent<Animator>().SetTrigger("Normal");

                    forwardButtonNav.selectOnLeft = null;
                    forwardButtonNav.selectOnRight = null;

                    exitButtonNav.selectOnDown = nextInfoButton;

                    exitButton.GetComponent<Selectable>().navigation = exitButtonNav;
                    nextInfoButton.GetComponent<Selectable>().navigation = forwardButtonNav;
                }
            }
        }
    }

    public void AddTutorialToGlossary(string tutorial)
    {
        switch(tutorial)
        {
            case ("Cards"):
                MenuController.instance._SettingsMenu.TutorialButtons.Add(MenuController.instance.CardTutorialButton);
                break;
            case ("Stickers"):
                MenuController.instance._SettingsMenu.TutorialButtons.Add(MenuController.instance.StickerTutorialButton);
                break;
            case ("Town"):
                MenuController.instance._SettingsMenu.TutorialButtons.Add(MenuController.instance.TownsTutorialButton);
                break;
            case ("WorldMap"):
                MenuController.instance._SettingsMenu.TutorialButtons.Add(MenuController.instance.WorldMapTutorialButton);
                break;
            case ("BasicControls"):
                MenuController.instance._SettingsMenu.TutorialButtons.Add(MenuController.instance.BasicControlsTutorialButton);
                break;
            case ("Battle"):
                MenuController.instance._SettingsMenu.TutorialButtons.Add(MenuController.instance.BattleTutorialButton);
                break;
            case ("SecretAreas"):
                MenuController.instance._SettingsMenu.TutorialButtons.Add(MenuController.instance.SecretAreaTutorialButton);
                break;
        }
        MenuController.instance._SettingsMenu.RePositionButtonParentChildren();
        MenuController.instance._SettingsMenu.SetSettingsButtonNavigations();
    }

    public void CompletedTutorial()
    {
        if(!isInSettingsMenu)
        {
            switch (scene.name)
            {
                case ("ForestTown"):
                    GameManager.instance.ReEnableControls();

                    GameManager.instance.IsTutorial = false;

                    if(Cursor.visible)
                    {
                        Cursor.lockState = CursorLockMode.Locked;
                        Cursor.visible = false;
                    }

                    if (!PlayerPrefs.HasKey("TownsTutorial"))
                    {
                        PlayerPrefs.SetInt("TownsTutorial", 1);
                    }
                    MenuController.instance.TownsTutorialButton.gameObject.SetActive(true);
                    MenuController.instance.GlossaryTutorialButton.interactable = true;

                    NodeManager.instance.HasTownTutorial = true;
                    break;
                case ("Forest_1"):
                    GameManager.instance.ReEnableControls();

                    GameManager.instance.IsTutorial = false;

                    GameManager.instance.PlayerField.enabled = true;

                    if(Cursor.visible)
                    {
                        Cursor.lockState = CursorLockMode.Locked;
                        Cursor.visible = false;
                    }

                    if (!PlayerPrefs.HasKey("BasicControlsTutorial"))
                    {
                        PlayerPrefs.SetInt("BasicControlsTutorial", 1);
                    }
                    MenuController.instance.BasicControlsTutorialButton.gameObject.SetActive(true);
                    MenuController.instance.GlossaryTutorialButton.interactable = true;

                    NodeManager.instance.HasBasicControlsTutorial = true;
                    break;
                case ("Secret_Wood_1"):
                    GameManager.instance.ReEnableControls();

                    GameManager.instance.IsTutorial = false;

                    GameManager.instance.PlayerField.enabled = true;

                    if(Cursor.visible)
                    {
                        Cursor.lockState = CursorLockMode.Locked;
                        Cursor.visible = false;
                    }

                    if (!PlayerPrefs.HasKey("SecretStageTutorial"))
                    {
                        PlayerPrefs.SetInt("SecretStageTutorial", 1);
                    }
                    MenuController.instance.SecretAreaTutorialButton.gameObject.SetActive(true);
                    MenuController.instance.GlossaryTutorialButton.interactable = true;

                    NodeManager.instance.HasSecretAreaTutorial = true;
                    break;
                case ("ForestField"):
                    WorldMapMovement wwm = FindObjectOfType<WorldMapMovement>();

                    wwm.IsTutorial = false;

                    if (!PlayerPrefs.HasKey("WorldMapTutorial"))
                    {
                        PlayerPrefs.SetInt("WorldMapTutorial", 1);
                    }
                    MenuController.instance.WorldMapTutorialButton.gameObject.SetActive(true);
                    MenuController.instance.GlossaryTutorialButton.interactable = true;

                    NodeManager.instance.HasWorldMapTutorial = true;
                    break;
            }
        }

        if(isInDeckMenu)
        {
            if(!PlayerPrefs.HasKey("CardTutorial"))
            {
                PlayerPrefs.SetInt("CardTutorial", 1);
            }
            MenuController.instance.CardTutorialButton.gameObject.SetActive(true);
            MenuController.instance.GlossaryTutorialButton.interactable = true;

            NodeManager.instance.HasCardsTutorial = true;
        }

        if (isInStickerMenu)
        {
            if (!PlayerPrefs.HasKey("StickerTutorial"))
            {
                PlayerPrefs.SetInt("StickerTutorial", 1);
            }
            MenuController.instance.StickerTutorialButton.gameObject.SetActive(true);
            MenuController.instance.GlossaryTutorialButton.interactable = true;

            NodeManager.instance.HasStickersTutorial = true;
        }
    }

    public void CompleteBattleTutorial(int battleIndex)
    {
        BattleSystem battleSystem = FindObjectOfType<BattleSystem>();

        battleSystem.IsTutorial = false;

        switch(battleIndex)
        {
            case 0:
                if(SteamUtils.IsSteamRunningOnSteamDeck())
                {
                    if (!NodeManager.instance.HasBattleTutorialOne)
                    {
                        NodeManager.instance.HasBattleTutorialOne = true;
                    }
                }
                else
                {
                    if (!PlayerPrefs.HasKey("BattleTutorialOne"))
                    {
                        PlayerPrefs.SetInt("BattleTutorialOne", 1);
                    }
                }
                break;
            case 1:
                if (SteamUtils.IsSteamRunningOnSteamDeck())
                {
                    if (!NodeManager.instance.HasBattleTutorialTwo)
                    {
                        NodeManager.instance.HasBattleTutorialTwo = true;
                    }
                }
                else
                {
                    if (!PlayerPrefs.HasKey("BattleTutorialTwo"))
                    {
                        PlayerPrefs.SetInt("BattleTutorialTwo", 1);
                    }
                }
                break;
            case 2:
                if (SteamUtils.IsSteamRunningOnSteamDeck())
                {
                    if (!NodeManager.instance.HasBattleTutorialThree)
                    {
                        NodeManager.instance.HasBattleTutorialThree = true;
                    }
                }
                else
                {
                    if (!PlayerPrefs.HasKey("BattleTutorialThree"))
                    {
                        PlayerPrefs.SetInt("BattleTutorialThree", 1);
                    }
                }
                break;
        }

        AddBattleTutorialInfo();

        MenuController.instance.BattleTutorialButton.gameObject.SetActive(true);
        MenuController.instance.GlossaryTutorialButton.interactable = true;
    }

    private void AddBattleTutorialInfo()
    {
        for(int i = 0; i < information.Count; i++)
        {
            MenuController.instance._SettingsMenu._TutorialChecker.BattleInformation.Add(information[i]);
        }

        for(int i = 0; i < playStationInfo.Count; i++)
        {
            MenuController.instance._SettingsMenu._TutorialChecker.PlayStationBattleInfo.Add(playStationInfo[i]);
        }

        for(int i = 0; i < xboxInfo.Count; i++)
        {
            MenuController.instance._SettingsMenu._TutorialChecker.XboxBattleInfo.Add(xboxInfo[i]);
        }

        for (int i = 0; i < steamDeckInfo.Count; i++)
        {
            MenuController.instance._SettingsMenu._TutorialChecker.SteamDeckBattleInfo.Add(steamDeckInfo[i]);
        }
    }
}