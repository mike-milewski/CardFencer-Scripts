using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class WorldMapMovement : MonoBehaviour
{
    [SerializeField]
    private List<Transform> levelNodes = new List<Transform>();

    [SerializeField]
    private List<StageInformation> secretStages = new List<StageInformation>();

    private StageInformation currentStage;

    [SerializeField]
    private Button[] stageButtons, worldButtons;

    [SerializeField]
    private MenuButtonNavigations[] menuButtonNavigations, worldButtonNavigations, playerInformationNavigations;

    [SerializeField]
    private SecretSymbol[] secretSymbols;

    [SerializeField]
    private WorldEnvironmentData worldEnvironmentData;

    [SerializeField]
    private FountainData fountainData;

    [SerializeField]
    private FieldCardData fieldCardData;

    [SerializeField]
    private ShopInformation shopInformation;

    [SerializeField]
    private LightManager lightManager;

    [SerializeField]
    private WeatherManager weatherManager;

    [SerializeField]
    private Transform player, worldExitTransform;

    [SerializeField]
    private SceneNameToLoad sceneNameToLoad;

    [SerializeField]
    private PlayerAnimationController playerAnimationController;

    [SerializeField]
    private CapsuleCollider capsuleCollider;

    [SerializeField]
    private Rigidbody playerRigidBody;

    [SerializeField]
    private Button enterAreaButton, worldsButton;

    [SerializeField]
    private Image worldsButtonImage;

    [SerializeField]
    private GameObject uiBlocker;

    [SerializeField]
    private Animator playerAnimator, stageInformationAnimator, enterAreaPanelAnimator, enterAreaYesButton, enterAreaNoButton, autoSaveAnimator;

    [SerializeField]
    private TextMeshProUGUI enterLocationText, worldButtonText;

    [SerializeField]
    private float moveSpeed, rotationSpeed;

    private Quaternion playerRotation;

    private Coroutine coroutine;

    [SerializeField]
    private int currentNodeIndex, targetNodeIndex;

    [SerializeField]
    private bool isOnASecretStage;

    [SerializeField]
    private bool moving, openedEnterAreaPanel, isTutorial, checkedSecretStage, isOnBossStage;

    public bool Moving => moving;

    public bool IsTutorial
    {
        get
        {
            return isTutorial;
        }
        set
        {
            isTutorial = value;
        }
    }

    public bool IsOnASecretStage => isOnASecretStage;

    public bool CheckedSecretStage
    {
        get
        {
            return checkedSecretStage;
        }
        set
        {
            checkedSecretStage = value;
        }
    }

    public List<Transform> LevelNodes
    {
        get
        {
            return levelNodes;
        }
        set
        {
            levelNodes = value;
        }
    }

    public StageInformation CurrentStage => currentStage;

    public LightManager _LightManager => lightManager;

    public WorldEnvironmentData _WorldEnvironmentData => worldEnvironmentData;

    public GameObject UIBlocker => uiBlocker;

    public Animator PlayerAnimator => playerAnimator;

    public Animator StageInformationAnimator => stageInformationAnimator;

    public List<StageInformation> SecretStages => secretStages;

    public MenuButtonNavigations[] PlayerInformationNavigations => playerInformationNavigations;

    public Coroutine _coroutine
    {
        get
        {
            return coroutine;
        }
        set
        {
            coroutine = value;
        }
    }

    public Transform Player => player;

    public int CurrentNodeIndex
    {
        get
        {
            return currentNodeIndex;
        }
        set
        {
            currentNodeIndex = value;
        }
    }

    public int TargetNodeIndex
    {
        get
        {
            return targetNodeIndex;
        }
        set
        {
            targetNodeIndex = value;
        }
    }

    public bool OpenedEnterAreaPanel
    {
        get
        {
            return openedEnterAreaPanel;
        }
        set
        {
            openedEnterAreaPanel = value;
        }
    }

    private void Awake()
    {
        MenuController.instance.OpenedMenu = false;
    }

    private void Start()
    {
        playerRigidBody.isKinematic = true;
        capsuleCollider.enabled = false;

        NodeManager.instance._StagePenalty.Clear();

        SetWorldButtonInteractability();
        SetWorldButtons();

        foreach(MenuButtonNavigations navigations in worldButtonNavigations)
        {
            navigations.ChangeWorldSelectableButtons();
        }

        if (!NewGameChecker.instance.IsANewGame)
        {
            if(NodeManager.instance.CurrentNodeIndex == -1)
            {
                player.position = secretStages[0].transform.position;

                isOnASecretStage = true;

                levelNodes.Insert(4, SecretStages[0].transform);

                NodeManager.instance.CurrentNodeIndex = 4;

                checkedSecretStage = false;

                ReCalibrateNodes();
            }
            else if(NodeManager.instance.CurrentNodeIndex == -2)
            {
                player.position = secretStages[1].transform.position;

                isOnASecretStage = true;

                levelNodes.Insert(5, SecretStages[1].transform);

                NodeManager.instance.CurrentNodeIndex = 5;

                checkedSecretStage = false;

                ReCalibrateNodes();
            }
            else if(NodeManager.instance.CurrentNodeIndex == -3)
            {
                player.position = secretStages[2].transform.position;

                isOnASecretStage = true;

                levelNodes.Insert(6, SecretStages[2].transform);

                NodeManager.instance.CurrentNodeIndex = 6;

                checkedSecretStage = false;

                ReCalibrateNodes();
            }
            else
            {
                isOnASecretStage = false;

                player.position = levelNodes[NodeManager.instance.CurrentNodeIndex].position;

                currentStage = levelNodes[NodeManager.instance.CurrentNodeIndex].GetComponent<StageInformation>();

                if(levelNodes[NodeManager.instance.CurrentNodeIndex].GetComponent<StageInformation>().IsBossStage)
                {
                    isOnBossStage = true;
                }
            }

            currentNodeIndex = NodeManager.instance.CurrentNodeIndex;

            StageInformation stageInformation = levelNodes[NodeManager.instance.CurrentNodeIndex].GetComponent<StageInformation>();

            currentStage = stageInformation;

            if (stageInformation.IsSecretStage)
            {
                if (stageInformation._StagePenalty.Count > 0)
                {
                    foreach (StagePenalty stagepenalty in stageInformation._StagePenalty)
                    {
                        NodeManager.instance._StagePenalty.Add(stagepenalty);
                    }
                }
            }

            if(worldEnvironmentData.changedDay)
            {
                MenuController.instance.MenuPlayerLights.SetActive(true);
            }
            else
            {
                MenuController.instance.MenuPlayerLights.SetActive(false);
            }
        }
        else
        {
            currentNodeIndex = 0;

            currentStage = levelNodes[0].GetComponent<StageInformation>();
        }

        MenuController.instance.UICamera.SetActive(true);

        MenuController.instance.ClearStagePenalties();

        MenuController.instance.ToggleExitAreaButton(false);

        MenuController.instance.ToggleSaveButton(true);

        MenuController.instance.CloseMenuThroughExitButton();

        MenuController.instance.StageObjectivePanel.SetActive(false);

        MenuController.instance.SecretStageObjectivePanel.SetActive(false);

        MenuController.instance.StagePenatlyPanel.SetActive(false);

        MenuController.instance._FieldTreasurePanel.ResetTreasuresText();

        MenuController.instance.StageConnectsToSecret = false;

        MenuController.instance.ResetAnimationEvent();

        MenuController.instance.GetMenuButtonNavigations();

        NodeManager.instance.CheckCurrentStageNodes();

        int secretIndex = levelNodes[currentNodeIndex].GetComponent<StageInformation>().ConnectedSecretStageIndex;
        
        NodeManager.instance.CheckIfCompletedStage(this, levelNodes[currentNodeIndex + 1 >= levelNodes.Count ? levelNodes.Count - 1 : currentNodeIndex + 1].GetComponent<StageInformation>(), secretStages[secretIndex]);

        NodeManager.instance.CheckIfWorldBossDefeatedRoutine();

        NewGameChecker.instance.IsANewGame = false;

        SetStageNavigations();
        ChangeWeather();
        ChangeDayTime();
        SetStageIndex();

        foreach (MenuButtonNavigations navigations in playerInformationNavigations)
        {
            navigations.ChangeWorldSelectableButtons();
        }
        foreach (MenuButtonNavigations navigations in menuButtonNavigations)
        {
            navigations.ChangeWorldSelectableButtons();
        }

        MenuController.instance.AutoSave();

        autoSaveAnimator.Play("AutoSave");
    }

    private void Update()
    {
        if(!moving && !openedEnterAreaPanel)
        {
            if(Input.GetKeyDown(KeyCode.Return))
            {
                EnterAreaPrompt();
            }
        }
    }

    private void SetStageIndex()
    {
        for(int i = 0; i < levelNodes.Count; i++)
        {
            levelNodes[i].GetComponent<StageInformation>().StageIndex = i;
        }
    }

    public void SetCurrentNodeIndex()
    {
        if(currentStage != null)
        {
            currentNodeIndex = currentStage.StageIndex;
        }
    }

    private void SetWorldButtons()
    {
        if(worldButtons.Length > 0)
        {
            for (int i = 0; i < worldButtons.Length; i++)
            {
                worldButtons[i].GetComponent<Animator>().SetTrigger("Disabled");
                worldButtons[i].GetComponent<Animator>().Play("Disabled");

                worldButtons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 0.5f);

                worldButtons[i].GetComponent<Image>().raycastTarget = false;
            }

            switch (NodeManager.instance.UnlockedWorlds)
            {
                case 2:
                    worldButtons[0].interactable = true;
                    worldButtons[0].GetComponent<Animator>().ResetTrigger("Disabled");
                    worldButtons[0].GetComponent<Animator>().Play("Normal");
                    worldButtons[0].transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 1);
                    worldButtons[0].GetComponent<Image>().raycastTarget = true;
                    break;
                case 3:
                    worldButtons[0].interactable = true;
                    worldButtons[0].GetComponent<Animator>().ResetTrigger("Disabled");
                    worldButtons[0].GetComponent<Animator>().Play("Normal");
                    worldButtons[0].transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 1);
                    worldButtons[0].GetComponent<Image>().raycastTarget = true;
                    worldButtons[1].interactable = true;
                    worldButtons[1].GetComponent<Animator>().ResetTrigger("Disabled");
                    worldButtons[1].GetComponent<Animator>().Play("Normal");
                    worldButtons[1].transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 1);
                    worldButtons[1].GetComponent<Image>().raycastTarget = true;
                    break;
                case 4:
                    worldButtons[0].interactable = true;
                    worldButtons[0].GetComponent<Animator>().ResetTrigger("Disabled");
                    worldButtons[0].GetComponent<Animator>().Play("Normal");
                    worldButtons[0].transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 1);
                    worldButtons[0].GetComponent<Image>().raycastTarget = true;
                    worldButtons[1].interactable = true;
                    worldButtons[1].GetComponent<Animator>().ResetTrigger("Disabled");
                    worldButtons[1].GetComponent<Animator>().Play("Normal");
                    worldButtons[1].transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 1);
                    worldButtons[1].GetComponent<Image>().raycastTarget = true;
                    worldButtons[2].interactable = true;
                    worldButtons[2].GetComponent<Animator>().ResetTrigger("Disabled");
                    worldButtons[2].GetComponent<Animator>().Play("Normal");
                    worldButtons[2].transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 1);
                    worldButtons[2].GetComponent<Image>().raycastTarget = true;
                    break;
                case 5:
                    worldButtons[0].interactable = true;
                    worldButtons[0].GetComponent<Animator>().ResetTrigger("Disabled");
                    worldButtons[0].GetComponent<Animator>().Play("Normal");
                    worldButtons[0].transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 1);
                    worldButtons[0].GetComponent<Image>().raycastTarget = true;
                    worldButtons[1].interactable = true;
                    worldButtons[1].GetComponent<Animator>().ResetTrigger("Disabled");
                    worldButtons[1].GetComponent<Animator>().Play("Normal");
                    worldButtons[1].transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 1);
                    worldButtons[1].GetComponent<Image>().raycastTarget = true;
                    worldButtons[2].interactable = true;
                    worldButtons[2].GetComponent<Animator>().ResetTrigger("Disabled");
                    worldButtons[2].GetComponent<Animator>().Play("Normal");
                    worldButtons[2].transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 1);
                    worldButtons[2].GetComponent<Image>().raycastTarget = true;
                    worldButtons[3].interactable = true;
                    worldButtons[3].GetComponent<Animator>().ResetTrigger("Disabled");
                    worldButtons[3].GetComponent<Animator>().Play("Normal");
                    worldButtons[3].transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 1);
                    worldButtons[3].GetComponent<Image>().raycastTarget = true;
                    break;
            }
        }
    }

    private void SetWorldButtonInteractability()
    {
        if(NodeManager.instance.UnlockedWorlds > 1)
        {
            worldsButton.interactable = true;
            worldsButtonImage.raycastTarget = true;
            worldButtonText.color = new Color(1, 1, 1, 1);
        }
        else
        {
            worldsButton.interactable = false;
            worldsButtonImage.raycastTarget = false;
            worldButtonText.color = new Color(1, 1, 1, 0.5f);
            worldsButton.GetComponent<Animator>().SetTrigger("Disabled");
            worldsButton.GetComponent<Animator>().Play("Disabled");
        }
    }

    private void SetStageNavigations()
    {
        for(int i = 0; i < levelNodes.Count; i++)
        {
            if(levelNodes[i].GetComponent<MenuButtonNavigations>())
            {
                if(levelNodes[i].GetComponent<Button>().interactable)
                {
                    levelNodes[i].GetComponent<MenuButtonNavigations>().ChangeWorldSelectableButtons();
                }
            }
        }

        for(int i = 0; i < secretStages.Count; i++)
        {
            if(secretStages[i].GetComponent<MenuButtonNavigations>())
            {
                if(secretStages[i].GetComponent<Button>().interactable)
                {
                    secretStages[i].GetComponent<MenuButtonNavigations>().ChangeWorldSelectableButtons();
                }
            }
        }
    }

    public void ReverseStageInformationAnimator()
    {
        if(stageInformationAnimator.IsInTransition(0))
        {
            stageInformationAnimator.Play("Reverse");
        }
    }

    public void ReverseStageInformationAnimatorNotInTransition()
    {
        if(InputManager.instance.ControllerPluggedIn)
        {
            stageInformationAnimator.Play("Reverse");
        }
    }

    public void PlayStatusPartticleOnSelect(ParticleSystem particle)
    {
        if(InputManager.instance.ControllerPluggedIn)
        {
            particle.gameObject.SetActive(true);
            particle.Play();
        }
    }

    public void StopStatusParticleOnSelect(ParticleSystem particle)
    {
        particle.Stop();
        particle.gameObject.SetActive(false);
    }

    public void SetBlockers()
    {
        MenuController.instance.CanToggleMenu = false;
        uiBlocker.SetActive(true);
    }

    public void EnterAreaPrompt()
    {
        MenuController.instance.CanToggleMenu = false;

        AudioManager.instance.PlaySoundEffect(AudioManager.instance.SelectedAudio);

        enterAreaPanelAnimator.Play("Open");

        InputManager.instance.SetSelectedObject(enterAreaYesButton.gameObject);

        enterAreaYesButton.Play("Normal");
        enterAreaNoButton.Play("Normal");

        if (levelNodes[currentNodeIndex].GetComponent<StageInformation>().IsTown)
        {
            enterLocationText.text = "Enter Town?";
        }
        else
        {
            enterLocationText.text = "Enter This Area?";
        }

        AddButtonListener();

        uiBlocker.SetActive(true);

        openedEnterAreaPanel = true;
    }

    public void CheckNode()
    {
        if (!levelNodes[currentNodeIndex].GetComponent<StageInformation>().IsTown)
            stageInformationAnimator.Play("Reverse");
    }

    public void SetNodeIndex()
    {
        if (currentNodeIndex < targetNodeIndex)
        {
            currentNodeIndex++;
        }
        else
        {
            currentNodeIndex--;
        }
    }

    private void StageNodeLightOn()
    {
        for(int i = 0; i < levelNodes.Count; i++)
        {
            levelNodes[i].GetComponent<StageInformation>().TurnOnLights();
        }

        for(int i = 0; i < secretStages.Count; i++)
        {
            secretStages[i].GetComponent<StageInformation>().TurnOnLights();
        }
    }

    private void StageNodeLightOff()
    {
        for (int i = 0; i < levelNodes.Count; i++)
        {
            levelNodes[i].GetComponent<StageInformation>().TurnOffLights();
        }

        for (int i = 0; i < secretStages.Count; i++)
        {
            secretStages[i].GetComponent<StageInformation>().TurnOffLights();
        }
    }

    private void SecretStageSymbolLightOn()
    {
        if(secretSymbols != null)
        {
            for(int i = 0; i < secretSymbols.Length; i++)
            {
                secretSymbols[i].TurnOnLight();
            }
        }
    }

    private void SecretStageSymbolLightOff()
    {
        if (secretSymbols != null)
        {
            for (int i = 0; i < secretSymbols.Length; i++)
            {
                secretSymbols[i].TurnOffLight();
            }
        }
    }

    private void StopSecretSymbolLightOnRoutine()
    {
        if (secretSymbols != null)
        {
            for (int i = 0; i < secretSymbols.Length; i++)
            {
                secretSymbols[i].StopLightEnableRoutine();
            }
        }
    }

    private void StopSecretSymbolLightOffRoutine()
    {
        if (secretSymbols != null)
        {
            for (int i = 0; i < secretSymbols.Length; i++)
            {
                secretSymbols[i].StopLightDisableRoutine();
            }
        }
    }

    private void StopStageLightOnRoutine()
    {
        for (int i = 0; i < levelNodes.Count; i++)
        {
            levelNodes[i].GetComponent<StageInformation>().StopEnableLightRoutine();
        }

        for (int i = 0; i < secretStages.Count; i++)
        {
            secretStages[i].GetComponent<StageInformation>().StopEnableLightRoutine();
        }
    }

    private void StopStageLightOffRoutine()
    {
        for (int i = 0; i < levelNodes.Count; i++)
        {
            levelNodes[i].GetComponent<StageInformation>().StopDisableLightRoutine();
        }

        for (int i = 0; i < secretStages.Count; i++)
        {
            secretStages[i].GetComponent<StageInformation>().StopDisableLightRoutine();
        }
    }

    public void ResetCurrentNodeIndex()
    {
        NodeManager.instance.CurrentNodeIndex = 0;
    }

    public void WorldIndex(int index)
    {
        NodeManager.instance.SetWorldIndex(index);
    }

    public IEnumerator MoveToNextWorld()
    {
        InputManager.instance._EventSystem.SetSelectedGameObject(null);
        NodeManager.instance.UnlockedWorlds++;

        MenuController.instance.CanToggleMenu = false;
        InputManager.instance._EventSystem.sendNavigationEvents = false;

        ResetCurrentNodeIndex();

        moving = true;

        playerRigidBody.isKinematic = false;
        capsuleCollider.enabled = true;

        playerAnimationController.ChangeController(true);

        yield return new WaitForSeconds(3f);

        playerAnimationController.ChangeController(false);

        playerAnimator.Play("Run");

        float distanceToNode = Vector3.Distance(player.position, worldExitTransform.position);

        while (distanceToNode > 0.5f)
        {
            Vector3 distance = new Vector3(worldExitTransform.position.x - player.position.x, 0,
                                           worldExitTransform.position.z - player.position.z).normalized;

            distanceToNode = Vector3.Distance(player.position, worldExitTransform.position);

            player.position += distance * moveSpeed * Time.deltaTime;

            playerRotation = Quaternion.LookRotation(distance);

            player.rotation = Quaternion.Slerp(player.rotation, playerRotation, rotationSpeed * Time.deltaTime);

            yield return null;
        }

        NodeManager.instance.WorldIndex++;

        NodeManager.instance.WorldBossDefeated = false;

        sceneNameToLoad.FadeOutScene();
    }

    public void ChangeWeather()
    {
        if(levelNodes[currentNodeIndex].GetComponent<StageInformation>().ChangesWeather)
        {
            weatherManager.ChangeWeather(true, true);

            worldEnvironmentData.changedWeather = true;
        }
        else
        {
            weatherManager.ChangeWeather(false, false);

            worldEnvironmentData.changedWeather = false;
        }
    }

    public void ChangeDayTime()
    {
        if(levelNodes[currentNodeIndex].GetComponent<StageInformation>().ChangesToNight)
        {
            if (!lightManager.IsNight)
            {
                StageNodeLightOn();
                SecretStageSymbolLightOn();

                lightManager.NightProfile();

                worldEnvironmentData.changedDay = true;
            }
        }
        else if(levelNodes[currentNodeIndex].GetComponent<StageInformation>().ChangesToDay)
        {
            if (!lightManager.IsDay)
            {
                StageNodeLightOff();
                SecretStageSymbolLightOff();

                lightManager.DayProfile();

                worldEnvironmentData.changedDay = false;
            }
        }
    }

    public IEnumerator MoveToNode()
    {
        moving = true;

        playerRigidBody.isKinematic = false;
        capsuleCollider.enabled = true;

        checkedSecretStage = false;

        ChangeWeather();
        ChangeDayTime();

        float distanceToNode = Vector3.Distance(player.position, levelNodes[currentNodeIndex].position);

        MenuController.instance.CanToggleMenu = false;

        uiBlocker.SetActive(true);

        InputManager.instance._EventSystem.sendNavigationEvents = false;

        while (distanceToNode > 0.5f)
        {
            Vector3 distance = new Vector3(levelNodes[currentNodeIndex].position.x - player.position.x, 0,
                                           levelNodes[currentNodeIndex].position.z - player.position.z).normalized;

            distanceToNode = Vector3.Distance(player.position, levelNodes[currentNodeIndex].position);

            player.position += distance * moveSpeed * Time.deltaTime;

            playerRotation = Quaternion.LookRotation(distance);

            player.rotation = Quaternion.Slerp(player.rotation, playerRotation, rotationSpeed * Time.deltaTime);

            yield return null;
        }

        uiBlocker.SetActive(false);

        if (currentNodeIndex == targetNodeIndex)
        {
            StageInformation stage = levelNodes[currentNodeIndex].GetComponent<StageInformation>();

            playerAnimator.Play("IdlePose");

            player.position = new Vector3(stage.transform.position.x, player.position.y, stage.transform.position.z);
            player.rotation = Quaternion.Euler(0, 180, 0);

            if(worldEnvironmentData.changedDay)
            {
                StopSecretSymbolLightOnRoutine();
                StopStageLightOnRoutine();
            }
            else
            {
                StopSecretSymbolLightOffRoutine();
                StopStageLightOffRoutine();
            }

            if (!stage.IsHoveredOver)
                stage.NodeAnimator.Play("Unhovered");

            CheckNodeHover();

            moving = false;

            playerRigidBody.isKinematic = true;
            capsuleCollider.enabled = false;

            currentStage = stage;

            if (!stage.IsSecretStage)
            {
                RemoveSecretStageFromList(false);
            }
            else
            {
                RemoveSecretStageFromList(true);

                if (stage.SecretStageIndex == -3)
                {
                    int levelIndex = LevelNodes.IndexOf(stage.transform);

                    if (isOnBossStage)
                    {
                        levelNodes.Remove(stage.transform);
                        levelNodes.Insert(levelIndex + 1, stage.transform);

                        ReCalibrateNodes();
                    }
                }
            }

            currentNodeIndex = stage.StageIndex;
            targetNodeIndex = currentNodeIndex;

            if (stage.IsBossStage)
            {
                isOnBossStage = true;
            }
            else
            {
                isOnBossStage = false;
            }

            MenuController.instance.CanToggleMenu = true;

            InputManager.instance._EventSystem.sendNavigationEvents = true;

            AddButtonListener();

            coroutine = null;
        }
        else
        {
            RestartCoroutine();
        }
    }

    public void DisableNodeHover()
    {
        for (int i = 0; i < levelNodes.Count; i++)
        {
            if(levelNodes[i].GetComponent<StageInformation>().IsHoveredOver)
            {
                levelNodes[i].GetComponent<StageInformation>().DisableNodeHover();
            }
        }
    }

    public void CheckNodeHover()
    {
        for (int i = 0; i < levelNodes.Count; i++)
        {
            levelNodes[i].GetComponent<StageInformation>().CheckIfNodeIsHovered();
        }
    }

    public void TurnOffMenuCamera()
    {
        MenuController.instance.ToggleUiCamera(false);
    }

    public void OpenPlayerMenu()
    {
        MenuController.instance.OpenMenu();
    }

    public void CheckMenuToggle(bool toggle)
    {
        MenuController.instance.CanToggleMenu = toggle;
    }

    public void ResetStageInfoPanel()
    {
        if(stageInformationAnimator.GetCurrentAnimatorStateInfo(0).IsName("StageInfo"))
           stageInformationAnimator.Play("Reverse");
    }

    private void RestartCoroutine()
    {
        if(coroutine != null)
        {
            StopCoroutine(coroutine);

            coroutine = null;

            SetNodeIndex();

            coroutine = StartCoroutine(MoveToNode());
        }
    }

    public void ReCalibrateNodes()
    {
        SetStageIndex();
    }

    private void RemoveSecretStageFromList(bool ignoreCurrentIndex)
    {
        if(ignoreCurrentIndex)
        {
            for (int i = 0; i < levelNodes.Count; i++)
            {
                if (levelNodes[i].GetComponent<StageInformation>().IsSecretStage)
                {
                    if(levelNodes[i].GetComponent<StageInformation>() != currentStage)
                    {
                        levelNodes.RemoveAt(i);
                    }

                    ReCalibrateNodes();
                }
            }
        }
        else
        {
            for (int i = 0; i < levelNodes.Count; i++)
            {
                if (levelNodes[i].GetComponent<StageInformation>().IsSecretStage)
                {
                    levelNodes.RemoveAt(i);

                    ReCalibrateNodes();
                }
            }
        }
    }

    public void MuteAmbientSounds()
    {
        AudioManager.instance.Ambient.volume = 0;
        AudioManager.instance.AmbientForeground.volume = 0;

        AudioManager.instance.Ambient.Stop();
        AudioManager.instance.AmbientForeground.Stop();
    }

    private void AddButtonListener()
    {
        SceneNameToLoad sceneNameToLoad = levelNodes[currentNodeIndex].GetComponent<SceneNameToLoad>();

        enterAreaButton.onClick.RemoveListener(sceneNameToLoad.FadeOutScene);

        enterAreaButton.onClick.AddListener(sceneNameToLoad.FadeOutScene);

        NodeManager.instance.MoonStoneStageIndex = levelNodes[currentNodeIndex].GetComponent<StageInformation>().MoonStoneStageIndex;

        NodeManager.instance.TreasureChestIndex = levelNodes[currentNodeIndex].GetComponent<StageInformation>().TreasureStageIndex;

        if(levelNodes[currentNodeIndex].GetComponent<StageInformation>().IsSecretStage)
        {
            NodeManager.instance.CurrentNodeIndex = levelNodes[currentNodeIndex].GetComponent<StageInformation>().SecretStageIndex;
        }
        else
        {
            NodeManager.instance.CurrentNodeIndex = currentNodeIndex;
        }
    }
}