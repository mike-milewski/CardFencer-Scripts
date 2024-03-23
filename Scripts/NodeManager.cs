using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Steamworks;

public class NodeManager : MonoBehaviour
{
    public static NodeManager instance = null;

    [SerializeField]
    private List<StagePenalty> stagePenatly = new List<StagePenalty>();

    [SerializeField]
    private List<string> unlockedSecretStageNames;

    [SerializeField]
    private int currentNodeIndex, moonStoneStageIndex, treasureChestIndex;

    [SerializeField]
    private int forestStagesUnlocked = 1, desertStagesUnlocked, arcticStagesUnlocked, graveStagesUnlocked, castleStagesUnlocked, unlockedWorlds = 1;

    [Header("Forest Secret Stages")]
    [SerializeField]
    private int forestSecretOne;
    [SerializeField]
    private int forestSecretTwo;
    [SerializeField]
    private int forestSecretThree;

    [Header("Desert Secret Stages")]
    [SerializeField]
    private int desertSecretOne;
    [SerializeField]
    private int desertSecretTwo;
    [SerializeField]
    private int desertSecretThree;

    [Header("Arctic Secret Stages")]
    [SerializeField]
    private int arcticSecretOne;
    [SerializeField]
    private int arcticSecretTwo;
    [SerializeField]
    private int arcticSecretThree;

    [Header("Graveyard Secret Stages")]
    [SerializeField]
    private int graveyardSecretOne;
    [SerializeField]
    private int graveyardSecretTwo;
    [SerializeField]
    private int graveyardSecretThree;

    [Header("Castle Secret Stages")]
    [SerializeField]
    private int castleSecretOne;
    [SerializeField]
    private int castleSecretTwo;
    [SerializeField]
    private int castleSecretThree;

    [Header("Battle Tutorials")]
    [SerializeField]
    private bool hasBattleTutorialOne;
    [SerializeField]
    private bool hasBattleTutorialTwo;
    [SerializeField]
    private bool hasBattleTutorialThree;

    [Header("World Map Tutorial")]
    [SerializeField]
    private bool hasWorldMapTutorial;

    [Header("Basic Controls Tutorial")]
    [SerializeField]
    private bool hasBasicControlsTutorial;

    [Header("Town Tutorial")]
    [SerializeField]
    private bool hasTownTutorial;

    [Header("Secret Area Tutorial")]
    [SerializeField]
    private bool hasSecretAreaTutorial;

    [Header("Cards Tutorial")]
    [SerializeField]
    private bool hasCardsTutorial;

    [Header("Stickers Tutorial")]
    [SerializeField]
    private bool hasStickersTutorial;

    private int worldIndex;

    [SerializeField]
    private string sceneName;

    [SerializeField]
    private bool completedStage, unlockedSecretStage, worldBossDefeated;

    public List<StagePenalty> _StagePenalty => stagePenatly;

    public List<string> UnlockedSecretStageNames => unlockedSecretStageNames;

    public string SceneName
    {
        get => sceneName;
        set => sceneName = value;
    }

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

    public int TreasureChestIndex
    {
        get
        {
            return treasureChestIndex;
        }
        set
        {
            treasureChestIndex = value;
        }
    }

    public int ForestStagesUnlocked
    {
        get
        {
            return forestStagesUnlocked;
        }
        set
        {
            forestStagesUnlocked = value;
        }
    }

    public int DesertStagesUnlocked
    {
        get
        {
            return desertStagesUnlocked;
        }
        set
        {
            desertStagesUnlocked = value;
        }
    }

    public int ArcticStagesUnlocked
    {
        get
        {
            return arcticStagesUnlocked;
        }
        set
        {
            arcticStagesUnlocked = value;
        }
    }

    public int GraveStagesUnlocked
    {
        get
        {
            return graveStagesUnlocked;
        }
        set
        {
            graveStagesUnlocked = value;
        }
    }

    public int CastleStagesUnlocked
    {
        get
        {
            return castleStagesUnlocked;
        }
        set
        {
            castleStagesUnlocked = value;
        }
    }

    public int UnlockedWorlds
    {
        get
        {
            return unlockedWorlds;
        }
        set
        {
            unlockedWorlds = value;
        }
    }

    public int MoonStoneStageIndex
    {
        get
        {
            return moonStoneStageIndex;
        }
        set
        {
            moonStoneStageIndex = value;
        }
    }

    public int ForestSecretStageOne
    {
        get
        {
            return forestSecretOne;
        }
        set
        {
            forestSecretOne = value;
        }
    }

    public int ForestSecretStageTwo
    {
        get
        {
            return forestSecretTwo;
        }
        set
        {
            forestSecretTwo = value;
        }
    }

    public int ForestSecretStageThree
    {
        get
        {
            return forestSecretThree;
        }
        set
        {
            forestSecretThree = value;
        }
    }

    public int DesertSecretStageOne
    {
        get
        {
            return desertSecretOne;
        }
        set
        {
            desertSecretOne = value;
        }
    }

    public int DesertSecretStageTwo
    {
        get
        {
            return desertSecretTwo;
        }
        set
        {
            desertSecretTwo = value;
        }
    }

    public int DesertSecretStageThree
    {
        get
        {
            return desertSecretThree;
        }
        set
        {
            desertSecretThree = value;
        }
    }

    public int ArcticSecretStageOne
    {
        get
        {
            return arcticSecretOne;
        }
        set
        {
            arcticSecretOne = value;
        }
    }

    public int ArcticSecretStageTwo
    {
        get
        {
            return arcticSecretTwo;
        }
        set
        {
            arcticSecretTwo = value;
        }
    }

    public int ArcticSecretStageThree
    {
        get
        {
            return arcticSecretThree;
        }
        set
        {
            arcticSecretThree = value;
        }
    }

    public int GraveyardSecretStageOne
    {
        get
        {
            return graveyardSecretOne;
        }
        set
        {
            graveyardSecretOne = value;
        }
    }

    public int GraveyardSecretStageTwo
    {
        get
        {
            return graveyardSecretTwo;
        }
        set
        {
            graveyardSecretTwo = value;
        }
    }

    public int GraveyardSecretStageThree
    {
        get
        {
            return graveyardSecretThree;
        }
        set
        {
            graveyardSecretThree = value;
        }
    }

    public int CastleSecretStageOne
    {
        get
        {
            return castleSecretOne;
        }
        set
        {
            castleSecretOne = value;
        }
    }

    public int CastleSecretStageTwo
    {
        get
        {
            return castleSecretTwo;
        }
        set
        {
            castleSecretTwo = value;
        }
    }

    public int CastleSecretStageThree
    {
        get
        {
            return castleSecretThree;
        }
        set
        {
            castleSecretThree = value;
        }
    }

    public int WorldIndex
    {
        get
        {
            return worldIndex;
        }
        set
        {
            worldIndex = value;
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

    public bool UnlockedSecretStage
    {
        get
        {
            return unlockedSecretStage;
        }
        set
        {
            unlockedSecretStage = value;
        }
    }

    public bool WorldBossDefeated
    {
        get
        {
            return worldBossDefeated;
        }
        set
        {
            worldBossDefeated = value;
        }
    }

    public bool HasBattleTutorialOne
    {
        get
        {
            return hasBattleTutorialOne;
        }
        set
        {
            hasBattleTutorialOne = value;
        }
    }

    public bool HasBattleTutorialTwo
    {
        get
        {
            return hasBattleTutorialTwo;
        }
        set
        {
            hasBattleTutorialTwo = value;
        }
    }

    public bool HasBattleTutorialThree
    {
        get
        {
            return hasBattleTutorialThree;
        }
        set
        {
            hasBattleTutorialThree = value;
        }
    }

    public bool HasWorldMapTutorial
    {
        get
        {
            return hasWorldMapTutorial;
        }
        set
        {
            hasWorldMapTutorial = value;
        }
    }

    public bool HasSecretAreaTutorial
    {
        get
        {
            return hasSecretAreaTutorial;
        }
        set
        {
            hasSecretAreaTutorial = value;
        }
    }

    public bool HasCardsTutorial
    {
        get
        {
            return hasCardsTutorial;
        }
        set
        {
            hasCardsTutorial = value;
        }
    }

    public bool HasStickersTutorial
    {
        get
        {
            return hasStickersTutorial;
        }
        set
        {
            hasStickersTutorial = value;
        }
    }

    public bool HasTownTutorial
    {
        get
        {
            return hasTownTutorial;
        }
        set
        {
            hasTownTutorial = value;
        }
    }

    public bool HasBasicControlsTutorial
    {
        get
        {
            return hasBasicControlsTutorial;
        }
        set
        {
            hasBasicControlsTutorial = value;
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
    }

    public void CheckIfCompletedStage(WorldMapMovement worldMapMovement, StageInformation stageInformation, StageInformation secretStageInfo)
    {
        Scene scene = SceneManager.GetActiveScene();

        if (completedStage && !stageInformation.Unlocked)
        {
            StartCoroutine("CheckForNewStageUnlock");
        }
        else
        {
            completedStage = false;
        }
        
        if(unlockedSecretStage && !secretStageInfo.Unlocked)
        {
            StartCoroutine("CheckForSecretStageUnlock");
        }
        else
        {
            unlockedSecretStage = false;
        }

        if (!completedStage && !unlockedSecretStage)
        {
            if (scene.name.Contains("Field"))
            {
                if (!worldMapMovement.IsTutorial)
                {
                    MenuController.instance.CanToggleMenu = true;
                }
                else
                {
                    MenuController.instance.CanToggleMenu = false;
                }
            }
        }

        sceneName = scene.name;
    }

    public void CheckIfWorldBossDefeatedRoutine()
    {
        if(worldBossDefeated)
        {
            WorldMapMovement worldMapMovement = FindObjectOfType<WorldMapMovement>();

            worldMapMovement.StartCoroutine(worldMapMovement.MoveToNextWorld());

            Scene scene = SceneManager.GetActiveScene();

            switch(scene.name)
            {
                case ("ForestField"):
                    if(SteamManager.Initialized)
                    {
                        SteamUserStats.GetAchievement("ACH_FOREST_FROLICKER", out bool achievementCompleted);

                        if (!achievementCompleted)
                        {
                            SteamUserStats.SetAchievement("ACH_FOREST_FROLICKER");
                            SteamUserStats.StoreStats();
                        }
                    }
                    break;
                case ("DesertField"):
                    if (SteamManager.Initialized)
                    {
                        SteamUserStats.GetAchievement("ACH_DESERT_STRIDER", out bool achievementCompleted);

                        if (!achievementCompleted)
                        {
                            SteamUserStats.SetAchievement("ACH_DESERT_STRIDER");
                            SteamUserStats.StoreStats();
                        }
                    }
                    break;
                case ("ArcticField"):
                    if (SteamManager.Initialized)
                    {
                        SteamUserStats.GetAchievement("ACH_ARCTIC_MARCHER", out bool achievementCompleted);

                        if (!achievementCompleted)
                        {
                            SteamUserStats.SetAchievement("ACH_ARCTIC_MARCHER");
                            SteamUserStats.StoreStats();
                        }
                    }
                    break;
                case ("GraveyardField"):
                    if (SteamManager.Initialized)
                    {
                        SteamUserStats.GetAchievement("ACH_GRAVEYARD_PROMENADE", out bool achievementCompleted);

                        if (!achievementCompleted)
                        {
                            SteamUserStats.SetAchievement("ACH_GRAVEYARD_PROMENADE");
                            SteamUserStats.StoreStats();
                        }
                    }
                    break;
            }
        }
    }

    public IEnumerator CheckForNewStageUnlock()
    {
        InputManager.instance._EventSystem.sendNavigationEvents = false;

        WorldMapMovement worldMapMovement = FindObjectOfType<WorldMapMovement>();

        worldMapMovement.UIBlocker.SetActive(true);

        MenuController.instance.CanToggleMenu = false;

        StageInformation stageInformation = worldMapMovement.LevelNodes[currentNodeIndex + 1].GetComponent<StageInformation>();

        yield return new WaitForSeconds(1f);

        stageInformation.NodeAnimator.Play("UnlockedStage");
    }

    public IEnumerator CheckForSecretStageUnlock()
    {
        InputManager.instance._EventSystem.sendNavigationEvents = false;

        WorldMapMovement worldMapMovement = FindObjectOfType<WorldMapMovement>();

        worldMapMovement.UIBlocker.SetActive(true);

        MenuController.instance.CanToggleMenu = false;

        StageInformation stageInformation = worldMapMovement.LevelNodes[currentNodeIndex].GetComponent<StageInformation>().SecretStageInformation;

        unlockedSecretStageNames.Add(stageInformation.GetComponent<SceneNameToLoad>().SceneToLoad);

        yield return new WaitForSeconds(1f);

        stageInformation.NodeAnimator.Play("UnlockedStage");

        yield return new WaitForSeconds(1f);

        SecretRoadAchievement();
    }

    private void SecretRoadAchievement()
    {
        if (SteamManager.Initialized)
        {
            SteamUserStats.GetAchievement("ACH_SECRET_ROAD", out bool achievementCompleted);

            if (!achievementCompleted)
            {
                SteamUserStats.SetAchievement("ACH_SECRET_ROAD");
                SteamUserStats.StoreStats();
            }
        }
    }

    public void CuriousAdventurerAchievement()
    {
        if (SteamManager.Initialized)
        {
            if (forestSecretOne == 1 && forestSecretTwo == 1 && forestSecretThree == 1 && desertSecretOne == 1 && desertSecretTwo == 1 && desertSecretThree == 1 && arcticSecretOne == 1 && arcticSecretTwo == 1 &&
                arcticSecretThree == 1 && graveyardSecretOne == 1 && graveyardSecretTwo == 1 && graveyardSecretThree == 1 && castleSecretOne == 1 && castleSecretTwo == 1 && castleSecretThree == 1)
            {
                SteamUserStats.GetAchievement("ACH_CURIOUS_ADVENTURER", out bool achievementCompleted);

                if (!achievementCompleted)
                {
                    SteamUserStats.SetAchievement("ACH_CURIOUS_ADVENTURER");
                    SteamUserStats.StoreStats();
                }
            }
        }
    }

    public void CheckCurrentStageNodes()
    {
        WorldMapMovement worldMapMovement = FindObjectOfType<WorldMapMovement>();

        Scene scene = SceneManager.GetActiveScene();

        switch(scene.name)
        {
            case "ForestField":
                if (worldMapMovement.IsOnASecretStage)
                {
                    int jumpedStages = forestStagesUnlocked + 2;

                    for (int i = 0; i < jumpedStages; i++)
                    {
                        worldMapMovement.LevelNodes[i].GetComponent<StageInformation>().SetStageNode();
                    }
                }
                else
                {
                    int jumpedStages = forestStagesUnlocked + 1;

                    for (int i = 0; i < jumpedStages; i++)
                    {
                        worldMapMovement.LevelNodes[i].GetComponent<StageInformation>().SetStageNode();
                    }
                }

                if (forestSecretOne == 1)
                {
                    worldMapMovement.SecretStages[0].SetStageNode();
                }
                if (forestSecretTwo == 1)
                {
                    worldMapMovement.SecretStages[1].SetStageNode();
                }
                if (forestSecretThree == 1)
                {
                    worldMapMovement.SecretStages[2].SetStageNode();
                }
                break;
            case "DesertField":
                if (worldMapMovement.IsOnASecretStage)
                {
                    int jumpedStages = desertStagesUnlocked + 2;

                    for (int i = 0; i < jumpedStages; i++)
                    {
                        worldMapMovement.LevelNodes[i].GetComponent<StageInformation>().SetStageNode();
                    }
                }
                else
                {
                    int jumpedStages = desertStagesUnlocked + 1;

                    for (int i = 0; i < jumpedStages; i++)
                    {
                        worldMapMovement.LevelNodes[i].GetComponent<StageInformation>().SetStageNode();
                    }
                }

                if (desertSecretOne == 1)
                {
                    worldMapMovement.SecretStages[0].SetStageNode();
                }
                if (desertSecretTwo == 1)
                {
                    worldMapMovement.SecretStages[1].SetStageNode();
                }
                if (desertSecretThree == 1)
                {
                    worldMapMovement.SecretStages[2].SetStageNode();
                }
                break;
            case "ArcticField":
                if (worldMapMovement.IsOnASecretStage)
                {
                    int jumpedStages = arcticStagesUnlocked + 2;

                    for (int i = 0; i < jumpedStages; i++)
                    {
                        worldMapMovement.LevelNodes[i].GetComponent<StageInformation>().SetStageNode();
                    }
                }
                else
                {
                    int jumpedStages = arcticStagesUnlocked + 1;

                    for (int i = 0; i < jumpedStages; i++)
                    {
                        worldMapMovement.LevelNodes[i].GetComponent<StageInformation>().SetStageNode();
                    }
                }

                if(arcticSecretOne == 1)
                {
                    worldMapMovement.SecretStages[0].SetStageNode();
                }
                if(arcticSecretTwo == 1)
                {
                    worldMapMovement.SecretStages[1].SetStageNode();
                }
                if(arcticSecretThree == 1)
                {
                    worldMapMovement.SecretStages[2].SetStageNode();
                }
                break;
            case "GraveyardField":
                if (worldMapMovement.IsOnASecretStage)
                {
                    int jumpedStages = graveStagesUnlocked + 2;

                    for (int i = 0; i < jumpedStages; i++)
                    {
                        worldMapMovement.LevelNodes[i].GetComponent<StageInformation>().SetStageNode();
                    }
                }
                else
                {
                    int jumpedStages = graveStagesUnlocked + 1;

                    for (int i = 0; i < jumpedStages; i++)
                    {
                        worldMapMovement.LevelNodes[i].GetComponent<StageInformation>().SetStageNode();
                    }
                }

                if (graveyardSecretOne == 1)
                {
                    worldMapMovement.SecretStages[0].SetStageNode();
                }
                if (graveyardSecretTwo == 1)
                {
                    worldMapMovement.SecretStages[1].SetStageNode();
                }
                if (graveyardSecretThree == 1)
                {
                    worldMapMovement.SecretStages[2].SetStageNode();
                }
                break;
            case "CastleField":
                if (worldMapMovement.IsOnASecretStage)
                {
                    int jumpedStages = castleStagesUnlocked + 2;

                    for (int i = 0; i < jumpedStages; i++)
                    {
                        worldMapMovement.LevelNodes[i].GetComponent<StageInformation>().SetStageNode();
                    }
                }
                else
                {
                    int jumpedStages = castleStagesUnlocked + 1;

                    for (int i = 0; i < jumpedStages; i++)
                    {
                        worldMapMovement.LevelNodes[i].GetComponent<StageInformation>().SetStageNode();
                    }
                }

                if (castleSecretOne == 1)
                {
                    worldMapMovement.SecretStages[0].SetStageNode();
                }
                if (castleSecretTwo == 1)
                {
                    worldMapMovement.SecretStages[1].SetStageNode();
                }
                if (castleSecretThree == 1)
                {
                    worldMapMovement.SecretStages[2].SetStageNode();
                }
                break;
        }
    }

    public void SetWorldIndex(int index)
    {
        worldIndex = index;
    }

    public void ResetSecretStages()
    {
        forestSecretOne = 0;
        forestSecretTwo = 0;
        forestSecretThree = 0;

        desertSecretOne = 0;
        desertSecretTwo = 0;
        desertSecretThree = 0;

        arcticSecretOne = 0;
        arcticSecretTwo = 0;
        arcticSecretThree = 0;

        graveyardSecretOne = 0;
        graveyardSecretTwo = 0;
        graveyardSecretThree = 0;

        castleSecretOne = 0;
        castleSecretTwo = 0;
        castleSecretThree = 0;
    }
}