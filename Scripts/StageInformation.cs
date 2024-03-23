using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

public enum StagePenalty { Power, Magic, Support, Mystic, Item };

public class StageInformation : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    [SerializeField]
    private List<StagePenalty> stagePenalty = new List<StagePenalty>();

    [SerializeField]
    private BossData bossData;

    [SerializeField]
    private TreasureData treasureData;

    [SerializeField]
    private MoonstoneData moonStoneData;

    [SerializeField]
    private FieldCardData fieldCardData;

    [SerializeField]
    private StageInformation secretStageInformation;

    [SerializeField]
    private WorldEnvironmentData worldEnvironmentData;

    [SerializeField]
    private WorldMapMovement worldMapMovement;

    [SerializeField]
    private GameObject penaltyInfoPanel, powerTextPenalty, magicTextPenalty, supportTextPenalty, mysticTextPenalty, itemTextPenalty, unlockedStageParticle, bossStageParticle, moonStoneImageObject, bossIcon,
                       fieldCardImageObject;

    [SerializeField]
    private Transform treasureChestLayout;

    [SerializeField]
    private TextMeshProUGUI treasureChestText, progressionText, moonStoneAmountText, fieldCardAmountText;

    [SerializeField]
    private Animator nodeAnimator, stageInformationAnimator;

    [SerializeField]
    private Light pointLight;

    [SerializeField]
    private Text shinyText;

    [SerializeField]
    private Sprite closedChest, openedChest;

    [SerializeField]
    private MeshRenderer meshRenderer;

    [SerializeField]
    private Material lockedStage, unlockedStage, bossStage;

    [SerializeField]
    private int treasureChestAmount, treasuresObtained, treasureStageIndex, moonStoneStageIndex, fieldCardIndex, moonStones, secretStageIndex, fieldCards, connectedSecretStageIndex;

    private int stageIndex;

    [SerializeField]
    private bool completedStage, isTown, isSecretStage, unlocked, isBossStage, changesWeather, stopsWeather, changesToDay, changesToNight, shouldIgnoreList;

    [SerializeField]
    private Color stageLightColor, lockedStageLightColor;

    [SerializeField]
    private bool isHoveredOver;

    private Coroutine lightOnRoutine, lightOffRoutine;

    public List<StagePenalty> _StagePenalty => stagePenalty;

    public Animator NodeAnimator => nodeAnimator;

    public StageInformation SecretStageInformation
    {
        get
        {
            return secretStageInformation;
        }
        set
        {
            secretStageInformation = value;
        }
    }

    public int TreasureStageIndex => treasureStageIndex;

    public bool IsHoveredOver
    {
        get
        {
            return isHoveredOver;
        }
        set
        {
            isHoveredOver = value;
        }
    }

    public bool IsSecretStage => isSecretStage;

    public bool IsTown => isTown;

    public bool ChangesWeather => changesWeather;

    public bool IsBossStage => isBossStage;

    public bool ChangesToDay => changesToDay;

    public bool ChangesToNight => changesToNight;

    public bool StopsWeather => stopsWeather;

    public bool ShouldIgnoreList => shouldIgnoreList;

    public int MoonStoneStageIndex => moonStoneStageIndex;

    public int SecretStageIndex => secretStageIndex;

    public int ConnectedSecretStageIndex => connectedSecretStageIndex;

    public int StageIndex
    {
        get
        {
            return stageIndex;
        }
        set
        {
            stageIndex = value;
        }
    }

    public bool Unlocked
    {
        get
        {
            return unlocked;
        }
        set
        {
            unlocked = value;
        }
    }

    public bool CompletedStage
    {
        get
        {
            return completedStage;
        }
        set
        {
            completedStage = value;
        }
    }

    private void Awake()
    {
        if(isBossStage)
        {
            pointLight.color = lockedStageLightColor;
        }
        else
        {
            pointLight.color = stageLightColor;
        }

        if(unlocked)
        {
            if(isBossStage)
            {
                meshRenderer.material = bossStage;
            }
            else
            {
                meshRenderer.material = unlockedStage;
            }
        }
        else
        {
            meshRenderer.material = lockedStage;

            GetComponent<Button>().interactable = false;
        }

        if(worldEnvironmentData.changedDay)
        {
            pointLight.enabled = true;
            pointLight.range = 7;
        }
        else
        {
            pointLight.enabled = false;
            pointLight.range = 0;
        }

        CheckTreasures();
        CheckMoonStones();
        CheckFieldCards();
    }

    public void UpdateStageInformation()
    {
        RectTransform rectTransform = progressionText.GetComponent<RectTransform>();

        for (int i = 0; i < treasureChestLayout.childCount; i++)
        {
            treasureChestLayout.GetChild(i).gameObject.SetActive(false);
        }

        if (treasureChestAmount > 0)
        {
            progressionText.alignment = TextAlignmentOptions.Top;

            treasureChestText.gameObject.SetActive(true);

            for(int i = 0; i < treasureChestAmount; i++)
            {
                treasureChestLayout.GetChild(i).gameObject.SetActive(true);

                treasureChestLayout.GetChild(i).GetComponent<Image>().sprite = closedChest;
            }

            for(int j = 0; j < treasuresObtained; j++)
            {
                treasureChestLayout.GetChild(j).GetComponent<Image>().sprite = openedChest;
            }
        }
        else
        {
            treasureChestText.gameObject.SetActive(false);

            progressionText.alignment = TextAlignmentOptions.Center;
        }

        if(isTown)
        {
            progressionText.gameObject.SetActive(true);

            moonStoneImageObject.SetActive(false);

            fieldCardImageObject.SetActive(false);

            progressionText.text = "Town";

            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, 0);

            progressionText.color = new Color(1, 1, 1, 1);

            progressionText.fontSize = 35;
        }
        else if(isBossStage)
        {
            progressionText.gameObject.SetActive(true);

            moonStoneImageObject.SetActive(false);

            fieldCardImageObject.SetActive(false);

            progressionText.text = "Boss";

            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, 0);

            progressionText.color = new Color(1, 1, 1, 1);

            progressionText.fontSize = 35;
        }
        else
        {
            progressionText.gameObject.SetActive(false);

            moonStoneImageObject.SetActive(true);

            fieldCardImageObject.SetActive(true);

            moonStoneAmountText.text = "x" + moonStones;
            fieldCardAmountText.text = "x" + fieldCards;
        }
    }

    private void CheckTreasures()
    {
        if(treasureChestAmount > 0)
        {
            if(!isSecretStage)
            {
                for (int i = 0; i < treasureData.worldTreasures[NodeManager.instance.WorldIndex].stageTreasures[treasureStageIndex].treasures.Length; i++)
                {
                    if (treasureData.worldTreasures[NodeManager.instance.WorldIndex].stageTreasures[treasureStageIndex].treasures[i] == 1)
                    {
                        treasuresObtained++;
                    }
                }
            }

            if(isSecretStage)
            {
                switch(secretStageIndex)
                {
                    case -1:
                        for (int i = 0; i < treasureData.worldTreasures[NodeManager.instance.WorldIndex].secretStageTreasures[0].treasures.Length; i++)
                        {
                            if (treasureData.worldTreasures[NodeManager.instance.WorldIndex].secretStageTreasures[0].treasures[i] == 1)
                            {
                                treasuresObtained++;
                            }
                        }
                        break;
                    case -2:
                        for (int i = 0; i < treasureData.worldTreasures[NodeManager.instance.WorldIndex].secretStageTreasures[1].treasures.Length; i++)
                        {
                            if (treasureData.worldTreasures[NodeManager.instance.WorldIndex].secretStageTreasures[1].treasures[i] == 1)
                            {
                                treasuresObtained++;
                            }
                        }
                        break;
                    case -3:
                        for (int i = 0; i < treasureData.worldTreasures[NodeManager.instance.WorldIndex].secretStageTreasures[2].treasures.Length; i++)
                        {
                            if (treasureData.worldTreasures[NodeManager.instance.WorldIndex].secretStageTreasures[2].treasures[i] == 1)
                            {
                                treasuresObtained++;
                            }
                        }
                        break;
                }
            }
        }
    }

    private void CheckMoonStones()
    {
        if (moonStones > 0)
        {
            if(!isSecretStage)
            {
                for (int i = 0; i < moonStoneData.worldMoonStones[NodeManager.instance.WorldIndex].stageMoonstones[moonStoneStageIndex].moonStones.Length; i++)
                {
                    if (moonStoneData.worldMoonStones[NodeManager.instance.WorldIndex].stageMoonstones[moonStoneStageIndex].moonStones[i] == 1)
                    {
                        moonStones--;
                    }
                }
            }

            if(isSecretStage)
            {
                switch(secretStageIndex)
                {
                    case -1:
                        for (int i = 0; i < moonStoneData.worldMoonStones[NodeManager.instance.WorldIndex].secretStageMoonstones[0].moonStones.Length; i++)
                        {
                            if (moonStoneData.worldMoonStones[NodeManager.instance.WorldIndex].secretStageMoonstones[0].moonStones[i] == 1)
                            {
                                moonStones--;
                            }
                        }
                        break;
                    case -2:
                        for (int i = 0; i < moonStoneData.worldMoonStones[NodeManager.instance.WorldIndex].secretStageMoonstones[1].moonStones.Length; i++)
                        {
                            if (moonStoneData.worldMoonStones[NodeManager.instance.WorldIndex].secretStageMoonstones[1].moonStones[i] == 1)
                            {
                                moonStones--;
                            }
                        }
                        break;
                    case -3:
                        for (int i = 0; i < moonStoneData.worldMoonStones[NodeManager.instance.WorldIndex].secretStageMoonstones[2].moonStones.Length; i++)
                        {
                            if (moonStoneData.worldMoonStones[NodeManager.instance.WorldIndex].secretStageMoonstones[2].moonStones[i] == 1)
                            {
                                moonStones--;
                            }
                        }
                        break;
                }
            }
        }
    }

    private void CheckFieldCards()
    {
        if (fieldCards > 0)
        {
            if (!isSecretStage)
            {
                for (int i = 0; i < fieldCardData.worldCards[NodeManager.instance.WorldIndex].stageCards[fieldCardIndex].fieldCards.Length; i++)
                {
                    if (fieldCardData.worldCards[NodeManager.instance.WorldIndex].stageCards[fieldCardIndex].fieldCards[i] == 1)
                    {
                        fieldCards--;
                    }
                }
            }

            if (isSecretStage)
            {
                switch(secretStageIndex)
                {
                    case -1:
                        for (int i = 0; i < fieldCardData.worldCards[NodeManager.instance.WorldIndex].secretStageCards[0].fieldCards.Length; i++)
                        {
                            if (fieldCardData.worldCards[NodeManager.instance.WorldIndex].secretStageCards[0].fieldCards[i] == 1)
                            {
                                fieldCards--;
                            }
                        }
                        break;
                    case -2:
                        for (int i = 0; i < fieldCardData.worldCards[NodeManager.instance.WorldIndex].secretStageCards[1].fieldCards.Length; i++)
                        {
                            if (fieldCardData.worldCards[NodeManager.instance.WorldIndex].secretStageCards[1].fieldCards[i] == 1)
                            {
                                fieldCards--;
                            }
                        }
                        break;
                    case -3:
                        for (int i = 0; i < fieldCardData.worldCards[NodeManager.instance.WorldIndex].secretStageCards[2].fieldCards.Length; i++)
                        {
                            if (fieldCardData.worldCards[NodeManager.instance.WorldIndex].secretStageCards[2].fieldCards[i] == 1)
                            {
                                fieldCards--;
                            }
                        }
                        break;
                }
            }
        }
    }

    public bool IsOnSameNode()
    {
        return gameObject.name == worldMapMovement.LevelNodes[worldMapMovement.CurrentNodeIndex].name;
    }

    public void ButtonNodeController(bool canEnterStage)
    {
        if (!worldMapMovement.Moving && !worldMapMovement.OpenedEnterAreaPanel && !MenuController.instance.OpenedMenu)
        {
            if (unlocked)
            {
                if(canEnterStage)
                {
                    AudioManager.instance.PlaySoundEffect(AudioManager.instance.SelectedAudio);

                    if (gameObject.name == worldMapMovement.LevelNodes[worldMapMovement.CurrentNodeIndex].name)
                    {
                        worldMapMovement.EnterAreaPrompt();

                        return;
                    }
                }

                if (IsSecretStage)
                {
                    if (!worldMapMovement.CheckedSecretStage)
                    {
                        if (!HasSameNodes())
                        {
                            SecretStageConnection stageConnection = GetComponent<SecretStageConnection>();

                            if (worldMapMovement.LevelNodes[worldMapMovement.CurrentNodeIndex].GetComponent<StageInformation>().isBossStage)
                            {
                                worldMapMovement.LevelNodes.Insert(stageConnection.ConnectedStage.StageIndex, transform);
                            }
                            else
                            {
                                Scene scene = SceneManager.GetActiveScene();

                                if (stageConnection.ShouldMoveUp)
                                {
                                    switch (scene.name)
                                    {
                                        case "ArcticField":
                                            ArcticNodesSecretStage(stageConnection);
                                            break;
                                        case "GraveyardField":
                                            GraveyardNodesSecretStage(stageConnection);
                                            break;
                                        case "CastleField":
                                            CastleNodesSecretStage(stageConnection);
                                            break;
                                    }
                                }
                                else
                                {
                                    worldMapMovement.LevelNodes.Insert(stageConnection.ConnectedStage.StageIndex + stageConnection.IncrementBy, transform);
                                }
                            }

                            worldMapMovement.ReCalibrateNodes();

                            worldMapMovement.CheckedSecretStage = true;
                        }
                    }
                }
                else
                {
                    StageInformation stage = worldMapMovement.LevelNodes[worldMapMovement.CurrentNodeIndex].GetComponent<StageInformation>();

                    if (stage.GetComponent<SecretStageConnection>())
                    {
                        SecretStageConnection stageConnection = stage.GetComponent<SecretStageConnection>();

                        if (stageConnection.ShouldMoveUp)
                        {
                            Scene scene = SceneManager.GetActiveScene();

                            if (stage.GetComponent<SecretStageConnection>())
                            {
                                switch (scene.name)
                                {
                                    case "ArcticField":
                                        ArcticNodesStage(stage);
                                        break;
                                    case "GraveyardField":
                                        GraveyardNodesStage(stage);
                                        break;
                                    case "CastleField":
                                        CastleNodesStage(stage);
                                        break;
                                }

                                worldMapMovement.ReCalibrateNodes();
                            }
                        }
                        else
                        {
                            if (isBossStage)
                            {
                                if (worldMapMovement.LevelNodes[worldMapMovement.CurrentNodeIndex].name.Contains("SecretStageThree"))
                                {
                                    int levelIndex = worldMapMovement.LevelNodes.IndexOf(stage.transform);

                                    worldMapMovement.LevelNodes.Remove(stage.transform);
                                    worldMapMovement.LevelNodes.Insert(levelIndex - 1, stage.transform);

                                    worldMapMovement.ReCalibrateNodes();
                                }
                            }
                        }
                    }
                    else
                    {
                        if (isBossStage)
                        {
                            if (worldMapMovement.LevelNodes[worldMapMovement.CurrentNodeIndex].name.Contains("SecretStageThree"))
                            {
                                int levelIndex = worldMapMovement.LevelNodes.IndexOf(stage.transform);

                                worldMapMovement.LevelNodes.Remove(stage.transform);
                                worldMapMovement.LevelNodes.Insert(levelIndex - 1, stage.transform);

                                worldMapMovement.ReCalibrateNodes();
                            }
                        }
                    }

                    worldMapMovement.CheckedSecretStage = false;
                }

                worldMapMovement.TargetNodeIndex = stageIndex;

                worldMapMovement.SetCurrentNodeIndex();
                worldMapMovement.SetNodeIndex();

                worldMapMovement.PlayerAnimator.Play("Run");

                worldMapMovement._coroutine = StartCoroutine(worldMapMovement.MoveToNode());

                NodeManager.instance._StagePenalty.Clear();

                InputManager.instance.SetSelectedObject(gameObject);

                if (isSecretStage)
                {
                    if (stagePenalty.Count > 0)
                    {
                        foreach (StagePenalty stagepenalty in stagePenalty)
                        {
                            NodeManager.instance._StagePenalty.Add(stagepenalty);
                        }
                    }
                }
            }
        }
    }

    private void ArcticNodesSecretStage(SecretStageConnection stageConnection)
    {
        StageInformation stage = worldMapMovement.LevelNodes[worldMapMovement.CurrentNodeIndex].GetComponent<StageInformation>();

        int levelIndex = worldMapMovement.LevelNodes.IndexOf(stage.transform);

        switch (stage.name)
        {
            case "TownNode":
                worldMapMovement.LevelNodes.Insert(stageConnection.ConnectedStage.StageIndex + stageConnection.IncrementBy, transform);
                break;
            case "StageOne":
                worldMapMovement.LevelNodes.Insert(stageConnection.ConnectedStage.StageIndex + stageConnection.IncrementBy, transform);
                break;
            case "StageTwo":
                worldMapMovement.LevelNodes.Insert(stageConnection.ConnectedStage.StageIndex + stageConnection.IncrementBy, transform);
                break;
            case "StageThree":
                if (stageConnection.ConnectedStage.name == "StageFour")
                {
                    worldMapMovement.LevelNodes.Insert(stageConnection.ConnectedStage.StageIndex + stageConnection.IncrementBy, transform);
                }
                else if (stageConnection.ConnectedStage.name == "StageFive")
                {
                    worldMapMovement.LevelNodes.Insert(stageConnection.ConnectedStage.StageIndex + stageConnection.IncrementBy, transform);
                }
                else
                {
                    worldMapMovement.LevelNodes.Insert(stageConnection.ConnectedStage.StageIndex, transform);
                }
                break;
            case "StageFour":
                worldMapMovement.LevelNodes.Insert(stageConnection.ConnectedStage.name == "StageFive" ? stageConnection.ConnectedStage.StageIndex + stageConnection.IncrementBy :
                                                   stageConnection.ConnectedStage.StageIndex, transform);
                break;
            case "StageFive":
                worldMapMovement.LevelNodes.Insert(stageConnection.ConnectedStage.StageIndex, transform);
                break;
            case "BossStage":
                worldMapMovement.LevelNodes.Insert(stageConnection.ConnectedStage.StageIndex, transform);
                break;
            case "SecretStageOne":
                worldMapMovement.LevelNodes.RemoveAt(levelIndex);
                worldMapMovement.LevelNodes.Insert(2, stage.transform);
                worldMapMovement.LevelNodes.Insert(stageConnection.ConnectedStage.StageIndex + stageConnection.IncrementBy, transform);
                break;
            case "SecretStageTwo":
                if (stageConnection.ConnectedStage.name == "StageFive")
                {
                    worldMapMovement.LevelNodes.RemoveAt(levelIndex);
                    worldMapMovement.LevelNodes.Insert(4, stage.transform);
                    worldMapMovement.LevelNodes.Insert(stageConnection.ConnectedStage.StageIndex + stageConnection.IncrementBy, transform);
                }
                else
                {
                    worldMapMovement.LevelNodes.Insert(stageConnection.ConnectedStage.StageIndex, transform);
                }
                break;
            case "SecretStageThree":
                if (stageConnection.ConnectedStage.name == "StageFour")
                {
                    worldMapMovement.LevelNodes.RemoveAt(levelIndex);
                    worldMapMovement.LevelNodes.Insert(6, stage.transform);
                    worldMapMovement.LevelNodes.Insert(4, transform);
                }
                else if(stageConnection.ConnectedStage.name == "StageTwo")
                {
                    worldMapMovement.LevelNodes.RemoveAt(levelIndex);
                    worldMapMovement.LevelNodes.Insert(6, stage.transform);
                    worldMapMovement.LevelNodes.Insert(2, transform);
                }
                else
                {
                    worldMapMovement.LevelNodes.Insert(stageConnection.ConnectedStage.StageIndex, transform);
                }
                break;
        }
    }

    private void ArcticNodesStage(StageInformation stage)
    {
        switch (stage.secretStageIndex)
        {
            case -1:
                switch (gameObject.name)
                {
                    case "TownNode":
                        worldMapMovement.LevelNodes.Remove(stage.transform);
                        worldMapMovement.LevelNodes.Insert(3, stage.transform);
                        break;
                    case "StageOne":
                        worldMapMovement.LevelNodes.Remove(stage.transform);
                        worldMapMovement.LevelNodes.Insert(3, stage.transform);
                        break;
                    case "StageTwo":
                        worldMapMovement.LevelNodes.Remove(stage.transform);
                        worldMapMovement.LevelNodes.Insert(3, stage.transform);
                        break;
                    case "StageThree":
                        worldMapMovement.LevelNodes.Remove(stage.transform);
                        worldMapMovement.LevelNodes.Insert(2, stage.transform);
                        break;
                    case "StageFour":
                        worldMapMovement.LevelNodes.Remove(stage.transform);
                        worldMapMovement.LevelNodes.Insert(2, stage.transform);
                        break;
                    case "StageFive":
                        worldMapMovement.LevelNodes.Remove(stage.transform);
                        worldMapMovement.LevelNodes.Insert(2, stage.transform);
                        break;
                    case "BossStage":
                        worldMapMovement.LevelNodes.Remove(stage.transform);
                        worldMapMovement.LevelNodes.Insert(2, stage.transform);
                        break;
                }
                break;
            case -2:
                switch (gameObject.name)
                {
                    case "StageThree":
                        worldMapMovement.LevelNodes.Remove(stage.transform);
                        worldMapMovement.LevelNodes.Insert(5, stage.transform);
                        break;
                    case "StageFive":
                        worldMapMovement.LevelNodes.Remove(stage.transform);
                        worldMapMovement.LevelNodes.Insert(4, stage.transform);
                        break;
                    case "BossStage":
                        worldMapMovement.LevelNodes.Remove(stage.transform);
                        worldMapMovement.LevelNodes.Insert(4, stage.transform);
                        break;
                }
                break;
            case -3:
                switch (gameObject.name)
                {
                    case "TownNode":
                        worldMapMovement.LevelNodes.Remove(stage.transform);
                        worldMapMovement.LevelNodes.Insert(6, stage.transform);
                        break;
                    case "StageOne":
                        worldMapMovement.LevelNodes.Remove(stage.transform);
                        worldMapMovement.LevelNodes.Insert(6, stage.transform);
                        break;
                    case "StageTwo":
                        worldMapMovement.LevelNodes.Remove(stage.transform);
                        worldMapMovement.LevelNodes.Insert(6, stage.transform);
                        break;
                    case "StageThree":
                        worldMapMovement.LevelNodes.Remove(stage.transform);
                        worldMapMovement.LevelNodes.Insert(6, stage.transform);
                        break;
                    case "StageFour":
                        worldMapMovement.LevelNodes.Remove(stage.transform);
                        worldMapMovement.LevelNodes.Insert(6, stage.transform);
                        break;
                    case "StageFive":
                        worldMapMovement.LevelNodes.Remove(stage.transform);
                        worldMapMovement.LevelNodes.Insert(6, stage.transform);
                        break;
                    case "BossStage":
                        worldMapMovement.LevelNodes.Remove(stage.transform);
                        worldMapMovement.LevelNodes.Insert(5, stage.transform);
                        break;
                }
                break;
        }
    }

    private void GraveyardNodesStage(StageInformation stage)
    {
        switch (stage.secretStageIndex)
        {
            case -1:
                switch (gameObject.name)
                {
                    case "TownNode":
                        worldMapMovement.LevelNodes.Remove(stage.transform);
                        worldMapMovement.LevelNodes.Insert(3, stage.transform);
                        break;
                    case "StageOne":
                        worldMapMovement.LevelNodes.Remove(stage.transform);
                        worldMapMovement.LevelNodes.Insert(3, stage.transform);
                        break;
                    case "StageTwo":
                        worldMapMovement.LevelNodes.Remove(stage.transform);
                        worldMapMovement.LevelNodes.Insert(3, stage.transform);
                        break;
                    case "StageFour":
                        worldMapMovement.LevelNodes.Remove(stage.transform);
                        worldMapMovement.LevelNodes.Insert(3, stage.transform);
                        break;
                    case "StageFive":
                        worldMapMovement.LevelNodes.Remove(stage.transform);
                        worldMapMovement.LevelNodes.Insert(3, stage.transform);
                        break;
                    case "BossStage":
                        worldMapMovement.LevelNodes.Remove(stage.transform);
                        worldMapMovement.LevelNodes.Insert(3, stage.transform);
                        break;
                }
                break;
            case -2:
                switch (gameObject.name)
                {
                    case "StageThree":
                        worldMapMovement.LevelNodes.Remove(stage.transform);
                        worldMapMovement.LevelNodes.Insert(5, stage.transform);
                        break;
                    case "StageFive":
                        worldMapMovement.LevelNodes.Remove(stage.transform);
                        worldMapMovement.LevelNodes.Insert(4, stage.transform);
                        break;
                    case "BossStage":
                        worldMapMovement.LevelNodes.Remove(stage.transform);
                        worldMapMovement.LevelNodes.Insert(4, stage.transform);
                        break;
                }
                break;
            case -3:
                switch (gameObject.name)
                {
                    case "TownNode":
                        worldMapMovement.LevelNodes.Remove(stage.transform);
                        worldMapMovement.LevelNodes.Insert(6, stage.transform);
                        break;
                    case "StageOne":
                        worldMapMovement.LevelNodes.Remove(stage.transform);
                        worldMapMovement.LevelNodes.Insert(6, stage.transform);
                        break;
                    case "StageTwo":
                        worldMapMovement.LevelNodes.Remove(stage.transform);
                        worldMapMovement.LevelNodes.Insert(6, stage.transform);
                        break;
                    case "StageThree":
                        worldMapMovement.LevelNodes.Remove(stage.transform);
                        worldMapMovement.LevelNodes.Insert(6, stage.transform);
                        break;
                    case "StageFour":
                        worldMapMovement.LevelNodes.Remove(stage.transform);
                        worldMapMovement.LevelNodes.Insert(6, stage.transform);
                        break;
                    case "StageFive":
                        worldMapMovement.LevelNodes.Remove(stage.transform);
                        worldMapMovement.LevelNodes.Insert(6, stage.transform);
                        break;
                    case "BossStage":
                        worldMapMovement.LevelNodes.Remove(stage.transform);
                        worldMapMovement.LevelNodes.Insert(5, stage.transform);
                        break;
                }
                break;
        }
    }

    private void GraveyardNodesSecretStage(SecretStageConnection stageConnection)
    {
        StageInformation stage = worldMapMovement.LevelNodes[worldMapMovement.CurrentNodeIndex].GetComponent<StageInformation>();

        int levelIndex = worldMapMovement.LevelNodes.IndexOf(stage.transform);

        switch (stage.name)
        {
            case "TownNode":
                worldMapMovement.LevelNodes.Insert(stageConnection.ConnectedStage.StageIndex + stageConnection.IncrementBy, transform);
                break;
            case "StageOne":
                worldMapMovement.LevelNodes.Insert(stageConnection.ConnectedStage.StageIndex + stageConnection.IncrementBy, transform);
                break;
            case "StageTwo":
                worldMapMovement.LevelNodes.Insert(stageConnection.ConnectedStage.StageIndex + stageConnection.IncrementBy, transform);
                break;
            case "StageThree":
                if (stageConnection.ConnectedStage.name == "StageFour")
                {
                    worldMapMovement.LevelNodes.Insert(stageConnection.ConnectedStage.StageIndex + stageConnection.IncrementBy, transform);
                }
                else if (stageConnection.ConnectedStage.name == "StageFive")
                {
                    worldMapMovement.LevelNodes.Insert(stageConnection.ConnectedStage.StageIndex + stageConnection.IncrementBy, transform);
                }
                else
                {
                    worldMapMovement.LevelNodes.Insert(stageConnection.ConnectedStage.StageIndex, transform);
                }
                break;
            case "StageFour":
                worldMapMovement.LevelNodes.Insert(stageConnection.ConnectedStage.name == "StageFive" ? stageConnection.ConnectedStage.StageIndex + stageConnection.IncrementBy :
                                                   stageConnection.ConnectedStage.StageIndex, transform);
                break;
            case "StageFive":
                worldMapMovement.LevelNodes.Insert(stageConnection.ConnectedStage.StageIndex, transform);
                break;
            case "BossStage":
                worldMapMovement.LevelNodes.Insert(stageConnection.ConnectedStage.StageIndex, transform);
                break;
            case "SecretStageOne":
                if (stageConnection.ConnectedStage.name == "StageFour")
                {
                    worldMapMovement.LevelNodes.RemoveAt(levelIndex);
                    worldMapMovement.LevelNodes.Insert(3, stage.transform);
                    worldMapMovement.LevelNodes.Insert(stageConnection.ConnectedStage.StageIndex + stageConnection.IncrementBy, transform);
                }
                else if(stageConnection.ConnectedStage.name == "StageFive")
                {
                    worldMapMovement.LevelNodes.RemoveAt(levelIndex);
                    worldMapMovement.LevelNodes.Insert(3, stage.transform);
                    worldMapMovement.LevelNodes.Insert(stageConnection.ConnectedStage.StageIndex + stageConnection.IncrementBy, transform);
                }
                else
                {
                    worldMapMovement.LevelNodes.Insert(stageConnection.ConnectedStage.StageIndex + stageConnection.IncrementBy, transform);
                }
                break;
            case "SecretStageTwo":
                if (stageConnection.ConnectedStage.name == "StageThree")
                {
                    worldMapMovement.LevelNodes.RemoveAt(levelIndex);
                    worldMapMovement.LevelNodes.Insert(5, stage.transform);
                    worldMapMovement.LevelNodes.Insert(stageConnection.ConnectedStage.StageIndex, transform);
                }
                else if (stageConnection.ConnectedStage.name == "StageFive")
                {
                    worldMapMovement.LevelNodes.RemoveAt(levelIndex);
                    worldMapMovement.LevelNodes.Insert(4, stage.transform);
                    worldMapMovement.LevelNodes.Insert(stageConnection.ConnectedStage.StageIndex + stageConnection.IncrementBy, transform);
                }
                else
                {
                    worldMapMovement.LevelNodes.Insert(stageConnection.ConnectedStage.StageIndex, transform);
                }
                break;
            case "SecretStageThree":
                if (stageConnection.ConnectedStage.name == "StageFour")
                {
                    worldMapMovement.LevelNodes.RemoveAt(levelIndex);
                    worldMapMovement.LevelNodes.Insert(6, stage.transform);
                    worldMapMovement.LevelNodes.Insert(stageConnection.ConnectedStage.StageIndex, transform);
                }
                else if (stageConnection.ConnectedStage.name == "StageThree")
                {
                    worldMapMovement.LevelNodes.RemoveAt(levelIndex);
                    worldMapMovement.LevelNodes.Insert(6, stage.transform);
                    worldMapMovement.LevelNodes.Insert(stageConnection.ConnectedStage.StageIndex, transform);
                }
                else
                {
                    worldMapMovement.LevelNodes.Insert(stageConnection.ConnectedStage.StageIndex, transform);
                }
                break;
        }
    }

    private void CastleNodesStage(StageInformation stage)
    {
        switch (stage.secretStageIndex)
        {
            case -1:
                switch (gameObject.name)
                {
                    case "TownNode":
                        worldMapMovement.LevelNodes.Remove(stage.transform);
                        worldMapMovement.LevelNodes.Insert(4, stage.transform);
                        break;
                    case "StageOne":
                        worldMapMovement.LevelNodes.Remove(stage.transform);
                        worldMapMovement.LevelNodes.Insert(4, stage.transform);
                        break;
                    case "StageTwo":
                        worldMapMovement.LevelNodes.Remove(stage.transform);
                        worldMapMovement.LevelNodes.Insert(4, stage.transform);
                        break;
                    case "StageThree":
                        worldMapMovement.LevelNodes.Remove(stage.transform);
                        worldMapMovement.LevelNodes.Insert(4, stage.transform);
                        break;
                    case "StageFour":
                        worldMapMovement.LevelNodes.Remove(stage.transform);
                        worldMapMovement.LevelNodes.Insert(4, stage.transform);
                        break;
                    case "StageFive":
                        worldMapMovement.LevelNodes.Remove(stage.transform);
                        worldMapMovement.LevelNodes.Insert(4, stage.transform);
                        break;
                    case "BossStage":
                        worldMapMovement.LevelNodes.Remove(stage.transform);
                        worldMapMovement.LevelNodes.Insert(4, stage.transform);
                        break;
                }
                break;
            case -2:
                switch (gameObject.name)
                {
                    case "StageThree":
                        worldMapMovement.LevelNodes.Remove(stage.transform);
                        worldMapMovement.LevelNodes.Insert(5, stage.transform);
                        break;
                    case "StageFive":
                        worldMapMovement.LevelNodes.Remove(stage.transform);
                        worldMapMovement.LevelNodes.Insert(4, stage.transform);
                        break;
                    case "BossStage":
                        worldMapMovement.LevelNodes.Remove(stage.transform);
                        worldMapMovement.LevelNodes.Insert(4, stage.transform);
                        break;
                }
                break;
            case -3:
                switch (gameObject.name)
                {
                    case "TownNode":
                        worldMapMovement.LevelNodes.Remove(stage.transform);
                        worldMapMovement.LevelNodes.Insert(6, stage.transform);
                        break;
                    case "StageOne":
                        worldMapMovement.LevelNodes.Remove(stage.transform);
                        worldMapMovement.LevelNodes.Insert(6, stage.transform);
                        break;
                    case "StageTwo":
                        worldMapMovement.LevelNodes.Remove(stage.transform);
                        worldMapMovement.LevelNodes.Insert(6, stage.transform);
                        break;
                    case "StageThree":
                        worldMapMovement.LevelNodes.Remove(stage.transform);
                        worldMapMovement.LevelNodes.Insert(6, stage.transform);
                        break;
                    case "StageFour":
                        worldMapMovement.LevelNodes.Remove(stage.transform);
                        worldMapMovement.LevelNodes.Insert(6, stage.transform);
                        break;
                    case "StageFive":
                        worldMapMovement.LevelNodes.Remove(stage.transform);
                        worldMapMovement.LevelNodes.Insert(6, stage.transform);
                        break;
                    case "BossStage":
                        worldMapMovement.LevelNodes.Remove(stage.transform);
                        worldMapMovement.LevelNodes.Insert(5, stage.transform);
                        break;
                }
                break;
        }
    }

    private void CastleNodesSecretStage(SecretStageConnection stageConnection)
    {
        StageInformation stage = worldMapMovement.LevelNodes[worldMapMovement.CurrentNodeIndex].GetComponent<StageInformation>();

        int levelIndex = worldMapMovement.LevelNodes.IndexOf(stage.transform);

        switch (stage.name)
        {
            case "TownNode":
                worldMapMovement.LevelNodes.Insert(stageConnection.ConnectedStage.StageIndex + stageConnection.IncrementBy, transform);
                break;
            case "StageOne":
                worldMapMovement.LevelNodes.Insert(stageConnection.ConnectedStage.StageIndex + stageConnection.IncrementBy, transform);
                break;
            case "StageTwo":
                worldMapMovement.LevelNodes.Insert(stageConnection.ConnectedStage.StageIndex + stageConnection.IncrementBy, transform);
                break;
            case "StageThree":
                if (stageConnection.ConnectedStage.name == "StageFour")
                {
                    worldMapMovement.LevelNodes.Insert(stageConnection.ConnectedStage.StageIndex + stageConnection.IncrementBy, transform);
                }
                else if (stageConnection.ConnectedStage.name == "StageFive")
                {
                    worldMapMovement.LevelNodes.Insert(stageConnection.ConnectedStage.StageIndex + stageConnection.IncrementBy, transform);
                }
                else
                {
                    worldMapMovement.LevelNodes.Insert(stageConnection.ConnectedStage.StageIndex, transform);
                }
                break;
            case "StageFour":
                worldMapMovement.LevelNodes.Insert(stageConnection.ConnectedStage.name == "StageFive" ? stageConnection.ConnectedStage.StageIndex + stageConnection.IncrementBy :
                                                   stageConnection.ConnectedStage.StageIndex, transform);
                break;
            case "StageFive":
                worldMapMovement.LevelNodes.Insert(stageConnection.ConnectedStage.StageIndex, transform);
                break;
            case "BossStage":
                worldMapMovement.LevelNodes.Insert(stageConnection.ConnectedStage.StageIndex, transform);
                break;
            case "SecretStageOne":
                worldMapMovement.LevelNodes.RemoveAt(levelIndex);
                worldMapMovement.LevelNodes.Insert(4, stage.transform);
                worldMapMovement.LevelNodes.Insert(stageConnection.ConnectedStage.StageIndex + stageConnection.IncrementBy, transform);
                break;
            case "SecretStageTwo":
                if (stageConnection.ConnectedStage.name == "StageFive")
                {
                    worldMapMovement.LevelNodes.RemoveAt(levelIndex);
                    worldMapMovement.LevelNodes.Insert(4, stage.transform);
                    worldMapMovement.LevelNodes.Insert(stageConnection.ConnectedStage.StageIndex + stageConnection.IncrementBy, transform);
                }
                else if (stageConnection.ConnectedStage.name == "StageThree")
                {
                    worldMapMovement.LevelNodes.RemoveAt(levelIndex);
                    worldMapMovement.LevelNodes.Insert(5, stage.transform);
                    worldMapMovement.LevelNodes.Insert(stageConnection.ConnectedStage.StageIndex + stageConnection.IncrementBy, transform);
                }
                else
                {
                    worldMapMovement.LevelNodes.Insert(stageConnection.ConnectedStage.StageIndex, transform);
                }
                break;
            case "SecretStageThree":
                if (stageConnection.ConnectedStage.name == "StageFour")
                {
                    worldMapMovement.LevelNodes.Insert(4, transform);
                }
                else
                {
                    worldMapMovement.LevelNodes.Insert(stageConnection.ConnectedStage.StageIndex, transform);
                }
                break;
        }
    }

    private bool HasSameNodes()
    {
        bool hasNode = false;

        for(int i = 0; i < worldMapMovement.LevelNodes.Count; i++)
        {
            if(worldMapMovement.LevelNodes[i].name == this.name)
            {
                hasNode = true;
            }
        }

        return hasNode;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(!MenuController.instance.OpenedMenu)
        {
            isHoveredOver = true;

            if (unlocked && !worldMapMovement.Moving)
            {
                DisableAllPenaltyTexts();

                AudioManager.instance.PlaySoundEffect(AudioManager.instance.CursorAudio);

                nodeAnimator.Play("Hovered", -1, 0);

                UpdateStageInformation();

                stageInformationAnimator.Play("StageInfo");

                if (isSecretStage)
                {
                    penaltyInfoPanel.SetActive(true);
                    CheckPenalties();
                }
                else
                {
                    penaltyInfoPanel.SetActive(false);
                }
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(!MenuController.instance.OpenedMenu)
        {
            isHoveredOver = false;

            if (unlocked)
            {
                if (!worldMapMovement.Moving)
                {
                    nodeAnimator.Play("Unhovered", -1, 0);
                }

                stageInformationAnimator.Play("Reverse");
            }
        }
    }

    public void ShowInfoOnControllerSelect()
    {
        if (!MenuController.instance.OpenedMenu)
        {
            isHoveredOver = true;

            if (unlocked && !worldMapMovement.Moving)
            {
                DisableAllPenaltyTexts();

                AudioManager.instance.PlaySoundEffect(AudioManager.instance.CursorAudio);

                nodeAnimator.Play("Hovered", -1, 0);

                UpdateStageInformation();

                stageInformationAnimator.Play("StageInfo");

                if (isSecretStage)
                {
                    penaltyInfoPanel.SetActive(true);
                    CheckPenalties();
                }
                else
                {
                    penaltyInfoPanel.SetActive(false);
                }
            }
        }
    }

    public void ShowInfoOnMenuClose()
    {
        isHoveredOver = true;

        if (unlocked && !worldMapMovement.Moving)
        {
            DisableAllPenaltyTexts();

            AudioManager.instance.PlaySoundEffect(AudioManager.instance.CursorAudio);

            nodeAnimator.Play("Hovered", -1, 0);

            UpdateStageInformation();

            stageInformationAnimator.Play("StageInfo");

            if (isSecretStage)
            {
                penaltyInfoPanel.SetActive(true);
                CheckPenalties();
            }
            else
            {
                penaltyInfoPanel.SetActive(false);
            }
        }
    }

    public void HideInfoOnControllerDeselect()
    {
        if (!MenuController.instance.OpenedMenu)
        {
            stageInformationAnimator.Play("Reverse", -1, 0);
        }
    }

    public void DisableNodeHover()
    {
        isHoveredOver = false;

        if (unlocked)
        {
            if (!worldMapMovement.Moving)
            {
                nodeAnimator.Play("Unhovered", -1, 0);
            }

            stageInformationAnimator.Play("Reverse");
        }
    }

    private void CheckPenalties()
    {
        for(int i = 0; i < stagePenalty.Count; i++)
        {
            switch (stagePenalty[i])
            {
                case (StagePenalty.Power):
                    powerTextPenalty.SetActive(true);
                    break;
                case (StagePenalty.Magic):
                    magicTextPenalty.SetActive(true);
                    break;
                case (StagePenalty.Support):
                    supportTextPenalty.SetActive(true);
                    break;
                case (StagePenalty.Mystic):
                    mysticTextPenalty.SetActive(true);
                    break;
                case (StagePenalty.Item):
                    itemTextPenalty.SetActive(true);
                    break;
            }
        }
    }

    private void DisableAllPenaltyTexts()
    {
        powerTextPenalty.SetActive(false);
        magicTextPenalty.SetActive(false);
        supportTextPenalty.SetActive(false);
        mysticTextPenalty.SetActive(false);
        itemTextPenalty.SetActive(false);
    }

    public void CheckIfNodeIsHovered()
    {
        if(isHoveredOver && unlocked)
        {
            nodeAnimator.Play("Hovered", -1, 0);

            stageInformationAnimator.Play("StageInfo");

            UpdateStageInformation();

            AudioManager.instance.PlaySoundEffect(AudioManager.instance.CursorAudio);
        }
    }

    public void UnlockStage()
    {
        Scene scene = SceneManager.GetActiveScene();

        if(!isSecretStage)
        {
            if (!isBossStage)
            {
                meshRenderer.material = unlockedStage;

                var unlockedParticle = Instantiate(unlockedStageParticle, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), unlockedStageParticle.transform.rotation);

                ParticleSystem particle = unlockedParticle.GetComponent<ParticleSystem>();

                particle.Play();

                AudioManager.instance.PlaySoundEffect(AudioManager.instance.ItemAppearAudio);
            }
            else
            {
                meshRenderer.material = bossStage;

                bossIcon.SetActive(true);

                var unlockedParticle = Instantiate(bossStageParticle, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), Quaternion.identity);

                ParticleSystem particle = unlockedParticle.GetComponent<ParticleSystem>();

                particle.Play();
            }

            switch(scene.name)
            {
                case "ForestField":
                    NodeManager.instance.ForestStagesUnlocked++;
                    break;
                case "DesertField":
                    NodeManager.instance.DesertStagesUnlocked++;
                    break;
                case "ArcticField":
                    NodeManager.instance.ArcticStagesUnlocked++;
                    break;
                case "GraveyardField":
                    NodeManager.instance.GraveStagesUnlocked++;
                    break;
                case "CastleField":
                    NodeManager.instance.CastleStagesUnlocked++;
                    break;
            }

            MenuController.instance.AutoSave();
        }
        else
        {
            if (!isBossStage)
            {
                meshRenderer.material = unlockedStage;

                var unlockedParticle = Instantiate(unlockedStageParticle, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), unlockedStageParticle.transform.rotation);

                ParticleSystem particle = unlockedParticle.GetComponent<ParticleSystem>();

                particle.Play();

                AudioManager.instance.PlaySoundEffect(AudioManager.instance.ItemAppearAudio);
            }
            else
            {
                meshRenderer.material = bossStage;

                bossIcon.SetActive(true);

                var unlockedParticle = Instantiate(bossStageParticle, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), Quaternion.identity);

                ParticleSystem particle = unlockedParticle.GetComponent<ParticleSystem>();

                particle.Play();
            }

            switch (scene.name)
            {
                case "ForestField":
                    switch(secretStageIndex)
                    {
                        case -1:
                            NodeManager.instance.ForestSecretStageOne = 1;
                            break;
                        case -2:
                            NodeManager.instance.ForestSecretStageTwo = 1;
                            break;
                        case -3:
                            NodeManager.instance.ForestSecretStageThree = 1;
                            break;
                    }
                    break;
                case "DesertField":
                    switch (secretStageIndex)
                    {
                        case -1:
                            NodeManager.instance.DesertSecretStageOne = 1;
                            break;
                        case -2:
                            NodeManager.instance.DesertSecretStageTwo = 1;
                            break;
                        case -3:
                            NodeManager.instance.DesertSecretStageThree = 1;
                            break;
                    }
                    break;
                case "ArcticField":
                    switch (secretStageIndex)
                    {
                        case -1:
                            NodeManager.instance.ArcticSecretStageOne = 1;
                            break;
                        case -2:
                            NodeManager.instance.ArcticSecretStageTwo = 1;
                            break;
                        case -3:
                            NodeManager.instance.ArcticSecretStageThree = 1;
                            break;
                    }
                    break;
                case "GraveyardField":
                    switch (secretStageIndex)
                    {
                        case -1:
                            NodeManager.instance.GraveyardSecretStageOne = 1;
                            break;
                        case -2:
                            NodeManager.instance.GraveyardSecretStageTwo = 1;
                            break;
                        case -3:
                            NodeManager.instance.GraveyardSecretStageThree = 1;
                            break;
                    }
                    break;
                case "CastleField":
                    switch (secretStageIndex)
                    {
                        case -1:
                            NodeManager.instance.CastleSecretStageOne = 1;
                            break;
                        case -2:
                            NodeManager.instance.CastleSecretStageTwo = 1;
                            break;
                        case -3:
                            NodeManager.instance.CastleSecretStageThree = 1;
                            break;
                    }
                    break;
            }

            MenuController.instance.AutoSave();
        }

        unlocked = true;

        GetComponent<Button>().interactable = true;

        MenuController.instance.CanToggleMenu = true;

        NodeManager.instance.CuriousAdventurerAchievement();
    }

    public void ReEnableControls()
    {
        worldMapMovement.UIBlocker.SetActive(false);

        NodeManager.instance.CompletedStage = false;
        NodeManager.instance.UnlockedSecretStage = false;

        InputManager.instance._EventSystem.sendNavigationEvents = true;

        worldMapMovement.LevelNodes[worldMapMovement.CurrentNodeIndex].GetComponent<Button>().interactable = true;

        worldMapMovement.LevelNodes[worldMapMovement.CurrentNodeIndex].GetComponent<MenuButtonNavigations>().ChangeWorldSelectableButtons();

        foreach(MenuButtonNavigations navs in worldMapMovement.PlayerInformationNavigations)
        {
            navs.ChangeWorldSelectableButtons();
        }
    }

    public void SetStageNode()
    {
        if(!isBossStage)
        {
            meshRenderer.material = unlockedStage;

            unlocked = true;

            GetComponent<Button>().interactable = true;

            bossIcon.SetActive(false);
        }
        else
        {
            meshRenderer.material = bossStage;

            unlocked = true;

            GetComponent<Button>().interactable = true;

            Scene scene = SceneManager.GetActiveScene();

            switch(scene.name)
            {
                case "ForestField":
                    if(!bossData.forestBossDefeated)
                    {
                        bossIcon.SetActive(true);
                    }
                    break;
                case "DesertField":
                    if (!bossData.desertBossDefeated)
                    {
                        bossIcon.SetActive(true);
                    }
                    break;
                case "ArcticField":
                    if (!bossData.arcticBossDefeated)
                    {
                        bossIcon.SetActive(true);
                    }
                    break;
                case "GraveyardField":
                    if (!bossData.graveBossDefeated)
                    {
                        bossIcon.SetActive(true);
                    }
                    break;
            }
        }
    }

    public void TurnOnLights()
    {
        if(lightOnRoutine != null)
        {
            StopCoroutine(lightOnRoutine);
        }

        lightOnRoutine = null;

        lightOnRoutine = StartCoroutine("EnableLight");
    }

    public void TurnOffLights()
    {
        if(lightOffRoutine != null)
        {
            StopCoroutine(lightOffRoutine);
        }

        lightOffRoutine = null;

        lightOffRoutine = StartCoroutine("DisableLight");
    }

    private IEnumerator EnableLight()
    {
        float t = 0;

        pointLight.enabled = true;

        pointLight.range = 0;

        while(t < 1)
        {
            t += Time.deltaTime;

            if(pointLight.range < 7)
            {
                pointLight.range += 10 * Time.deltaTime;
            }

            yield return null;
        }
        pointLight.range = 7;
    }

    private IEnumerator DisableLight()
    {
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime;

            pointLight.range -= 10 * Time.deltaTime;

            yield return null;
        }
        pointLight.enabled = false;
    }

    public void StopEnableLightRoutine()
    {
        if(lightOnRoutine != null)
        {
            StopCoroutine(lightOnRoutine);

            lightOnRoutine = null;

            pointLight.enabled = true;

            pointLight.range = 7;
        }
    }

    public void StopDisableLightRoutine()
    {
        if(lightOffRoutine != null)
        {
            StopCoroutine(lightOffRoutine);

            lightOffRoutine = null;

            pointLight.range = 0;

            pointLight.enabled = false;
        }
    }
}