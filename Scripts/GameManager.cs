using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Coffee.UIEffects;
using TMPro;
using System.Collections.Generic;
using Steamworks;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField]
    private List<EnemyStats> enemiesToLoad = new List<EnemyStats>();

    [SerializeField]
    private GameObject[] enemiesInArea;

    [SerializeField]
    private PlayerFieldSwordAndShield playerField;

    [SerializeField]
    private CheckPointManager checkPointManager;

    private BestiaryMenu bestiaryMenu;

    [SerializeField]
    private CameraFollow cameraFollow;

    [SerializeField]
    private StageObjectives stageObjectives;

    [SerializeField]
    private PlayerUserInterfaceController userInterFaceController;

    private UITransitionEffect screenTransition;

    [SerializeField]
    private EnemyFieldAI enemyObject = null;

    [SerializeField]
    private EnemyFieldAI secretEnemyObject = null;

    private GameObject attackParticle = null;

    [SerializeField]
    private GameObject noviceSword, veteranSword, eliteSword, noviceShield, veteranShield, eliteShield, noviceArmor, veteranArmor, eliteArmor;

    [SerializeField]
    private StagePenaltyPanel stagePenaltyPanel = null;

    [SerializeField]
    private BossData bossData;

    [SerializeField]
    private MainCharacterStats mainCharacterStats;

    [SerializeField]
    private PlayerMenuInfo playerMenuInfo;

    [SerializeField]
    private FountainData fountainData;

    [SerializeField]
    private FieldCardData fieldCardData;

    [SerializeField]
    private ShopInformation shopInformation;

    [SerializeField]
    private WorldEnvironmentData worldEnvironmentData;

    [SerializeField]
    private MoonstoneData moonStoneData;

    [SerializeField]
    private TreasureData treasureData;

    [SerializeField]
    private Animator gotItemAnimator;

    [SerializeField]
    private GameObject worldHolder;

    [SerializeField]
    private ParticleSystem rainParticle;

    private CardTypeCategory cardTypeCategory;

    private MenuCard menuCard;

    private Sticker sticker;

    [SerializeField]
    private ParticleSystem healParticle, healParticleChild, healParticleChildTwo;

    [SerializeField]
    private Color heartPickUpColor, cardPickUpColor;

    [SerializeField]
    private TextMeshProUGUI gotItemInfoText, itemTypeText;

    [SerializeField]
    private Transform player, checkPoint, worldParentHolder;

    private Transform stickerListParent;

    [SerializeField]
    private ParticleSystem gotItemParticlePrefab;

    [SerializeField]
    private bool isBossFight, isFinalBossFight, endsGameDemo, isAPreemptiveStrike, isAnAmbush;

    private bool isTutorial, isEnteringBattle;

    private int currentPlayerHealth, currentPlayerCardPoints;

    private string currentScene, firstLoadedLevel;

    private Scene scene;

    public GameObject[] EnemiesInArea => enemiesInArea;

    public List<EnemyStats> EnemiesToLoad => enemiesToLoad;

    public BestiaryMenu _BestiaryMenu => bestiaryMenu;

    public UITransitionEffect ScreenTransition => screenTransition;

    public Animator GotItemAnimator => gotItemAnimator;

    public CameraFollow _cameraFollow => cameraFollow;

    public PlayerFieldSwordAndShield PlayerField => playerField;

    public PlayerUserInterfaceController UserInterFaceController => userInterFaceController;

    public PlayerMenuInfo _PlayerMenuInfo => playerMenuInfo;

    public StageObjectives _StageObjectives => stageObjectives;

    public EnemyFieldAI SecretEnemyObject => secretEnemyObject;

    public EnemyFieldAI EnemyObject
    {
        get
        {
            return enemyObject;
        }
        set
        {
            enemyObject = value;
        }
    }

    public GameObject AttackParticle
    {
        get
        {
            return attackParticle;
        }
        set
        {
            attackParticle = value;
        }
    }

    public StagePenaltyPanel StagePenaltyPanel
    {
        get => stagePenaltyPanel;
        set => stagePenaltyPanel = value;
    }

    public MenuCard _MenuCard => menuCard;

    public Sticker _Sticker => sticker;

    public TextMeshProUGUI GotItemInfoText => gotItemInfoText;

    public TextMeshProUGUI ItemTypeText => itemTypeText;

    public CardTypeCategory _CardTypeCategory => cardTypeCategory;

    public Transform StickerListParent => stickerListParent;

    public WorldEnvironmentData _WorldEnvironmentData => worldEnvironmentData;

    public string CurrentScene => currentScene;

    public bool IsEnteringBattle => isEnteringBattle;

    public bool EndsGameDemo
    {
        get
        {
            return endsGameDemo;
        }
        set
        {
            endsGameDemo = value;
        }
    }

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

    public bool IsBossFight
    {
        get
        {
            return isBossFight;
        }
        set
        {
            isBossFight = value;
        }
    }

    public bool IsFinalBossFight
    {
        get
        {
            return isFinalBossFight;
        }
        set
        {
            isFinalBossFight = value;
        }
    }

    public bool IsAPreemptiveStrike
    {
        get
        {
            return isAPreemptiveStrike;
        }
        set
        {
            isAPreemptiveStrike = value;
        }
    }

    public bool IsAnAmbush
    {
        get
        {
            return isAnAmbush;
        }
        set
        {
            isAnAmbush = value;
        }
    }

    public int CurrentPlayerHealth => currentPlayerHealth;

    public int CurrentPlayerCardPoints => currentPlayerCardPoints;

    private void Awake()
    {
        #region Singleton
        if (instance == null)
        {
            instance = this;
        }
        #endregion

        scene = SceneManager.GetActiveScene();

        if(scene.name.Contains("Town"))
        {
            playerField.enabled = false;
        }
        else
        {
            playerField.enabled = true;

            if(!scene.name.Contains("Boss"))
                MenuController.instance._FieldTreasurePanel.gameObject.SetActive(true);
        }

        screenTransition = ScreenFade.instance._ScreenTransitionController.GetComponent<UITransitionEffect>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        stickerListParent = MenuController.instance.StickerList;

        cardTypeCategory = MenuController.instance._CardTypeCategory;

        menuCard = MenuController.instance._MenuCard;

        sticker = MenuController.instance._Sticker;

        bestiaryMenu = MenuController.instance._BestiaryMenu;

        StartCoroutine(WaitToLoadScene());

        SetPlayerFieldEquipment();

        MenuController.instance._CameraFollow = cameraFollow;

        MenuController.instance.GotItemAnimator = gotItemAnimator;

        MenuController.instance.UICamera.SetActive(true);

        MenuController.instance.CheckCurrentScene();

        MenuController.instance.ToggleExitAreaButton(true);

        MenuController.instance.ToggleSaveButton(false);

        MenuController.instance.CanToggleMenu = false;

        MenuController.instance.GetMenuButtonNavigations();

        playerField.enabled = false;

        player.GetComponent<PlayerMovement>().enabled = false;

        if(worldEnvironmentData.changedWeather)
        {
            if(rainParticle != null)
            {
                rainParticle.gameObject.SetActive(true);

                rainParticle.Play();
            }
        }

        StartCoroutine(WaitToLoadIntoScene());

        if(enemiesInArea.Length > 0)
        {
            if(stageObjectives._Objectives == Objectives.DefeatEnemies)
            {
                if(stageObjectives.DefeatAllEnemies)
                {
                    stageObjectives.EnemiesToDefeat = enemiesInArea.Length;
                }
            }
            else if(stageObjectives.SpecialObjective == Objectives.DefeatEnemies)
            {
                if(stageObjectives.DefeatAllEnemiesSecret)
                {
                    stageObjectives.EnemiesToDefeat = enemiesInArea.Length;
                }
            }
        }
    }

    private IEnumerator WaitToLoadIntoScene()
    {
        yield return new WaitForSecondsRealtime(1f);

        if(!isTutorial)
        {
            player.GetComponent<PlayerMovement>().enabled = true;

            MenuController.instance.CanToggleMenu = true;
            MenuController.instance._CameraFollow.enabled = true;

            if(stageObjectives != null)
            {
                stageObjectives.PlayStageObjectiveAnimator();

                if (PlayerPrefs.HasKey("SoundEffects"))
                {
                    if (stageObjectives.ObjectivePanelAnimator.gameObject.activeSelf)
                    {
                        stageObjectives.ObjectivePanelAnimator.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SoundEffects");
                        stageObjectives.ObjectivePanelAnimator.GetComponent<AudioSource>().Play();
                    }
                }
                else
                {
                    if (stageObjectives.ObjectivePanelAnimator.gameObject.activeSelf)
                        stageObjectives.ObjectivePanelAnimator.GetComponent<AudioSource>().Play();
                }
            }

            if(!scene.name.Contains("Town"))
               playerField.enabled = true;
        }
        MenuController.instance.CheckStagePenalties();
    }

    private IEnumerator WaitToLoadScene()
    {
        yield return new WaitUntil(() => screenTransition.effectFactor <= 0);

        if(!isTutorial)
        {
            MenuController.instance.CanToggleMenu = true;

            if (InputManager.instance.FirstSelectedObject.transform.parent.GetComponent<TutorialChecker>())
            {
                InputManager.instance.FirstSelectedObject = null;
            }
        }  

        scene = SceneManager.GetActiveScene();

        currentScene = scene.name;
    }

    public void SetPlayerFieldEquipment()
    {
        switch(playerMenuInfo.weaponIndex)
        {
            case 0:
                playerField.SwordObject = noviceSword;
                break;
            case 1:
                playerField.SwordObject = veteranSword;
                break;
            case 2:
                playerField.SwordObject = eliteSword;
                break;
        }

        switch (playerMenuInfo.shieldIndex)
        {
            case 0:
                playerField.ShieldObject = noviceShield;
                break;
            case 1:
                playerField.ShieldObject = veteranShield;
                break;
            case 2:
                playerField.ShieldObject = eliteShield;
                break;
        }

        switch (playerMenuInfo.armorIndex)
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
    }

    public void ReEnableControls()
    {
        player.GetComponent<PlayerMovement>().enabled = true;

        MenuController.instance._CameraFollow.enabled = true;
    }

    public void RetryBattle()
    {
        StartCoroutine(ReloadScene());
    }

    public void EnterBattle()
    {
        if(enemiesToLoad.Count > 0)
        {
            MenuController.instance.gameObject.SetActive(false);

            healParticle.gameObject.SetActive(false);

            cameraFollow.enabled = false;

            if (enemyObject != null)
            {
                checkPointManager.SetCheckPointPosition(enemyObject);
            }

            if (!isBossFight)
            {
                enemyObject.IsDead = false;
            }

            ScreenFade.instance._ScreenTransitionController.SetScreenTransitionSprite(ScreenFade.instance._ScreenTransitionController.BattleTransition, true);

            SetPlayerStats();

            AudioManager.instance.PlaySoundEffect(AudioManager.instance.BattleTransitionAudio);
            AudioManager.instance.BackgroundMusic.volume = 0;

            screenTransition.Show();

            StartCoroutine(WaitToLoadBattleScene());

            isEnteringBattle = true;

            Time.timeScale = 0;
        }
    }

    public void ResetFieldPlayerAnimator()
    {
        playerField.ResetEquipment();
    }

    public void EnableMenuButtonRaycasting()
    {
        MenuController.instance.EnableButtonRaycast();
    }

    public void EnableMenuCanvasGroupValues()
    {
        MenuController.instance.EnableCanvasGroupValues();
    }

    public void ShowUI()
    {
        userInterFaceController._CanvasGroup.alpha = 1;
    }

    public void HideUI()
    {
        userInterFaceController._CanvasGroup.alpha = 0;
        userInterFaceController.HidePlayerInformation(true);
    }

    private IEnumerator WaitToLoadBattleScene()
    {
        yield return new WaitUntil(() => screenTransition.effectFactor >= 0.6f);

        if (gotItemAnimator != null)
        {
            gotItemAnimator.Play("Idle", -1, 0);
        }

        yield return new WaitUntil(() => screenTransition.effectFactor >= 0.9f);

        screenTransition.effectFactor = 1;

        if(attackParticle != null)
        {
            Destroy(attackParticle);
            attackParticle = null;
        }

        if (stagePenaltyPanel != null)
        {
            stagePenaltyPanel.ResetPenaltyAnimator();
        }

        if (stageObjectives != null)
        {
            stageObjectives.ResetObjectivesAnimator();
        }

        worldHolder.SetActive(false);

        GetComponent<AudioListener>().enabled = true;

        isEnteringBattle = false;

        Time.timeScale = 1;

        string battleSceneName = "Battle";

        AsyncOperation async = SceneManager.LoadSceneAsync(battleSceneName, LoadSceneMode.Additive);

        async.allowSceneActivation = false;

        while(!async.isDone)
        {
            if(async.progress >= 0.9f)
            {
                async.allowSceneActivation = true;
            }
            yield return null;
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(battleSceneName));

        scene = SceneManager.GetActiveScene();

        AudioManager.instance._backGroundMusic.PlayBackgroundMusic();
    }

    public IEnumerator WaitToLoadFieldScene(bool returnToCheckPoint)
    {
        yield return new WaitUntil(() => screenTransition.effectFactor >= 0.9f);

        enemiesToLoad.Clear();

        isAnAmbush = false;
        isAPreemptiveStrike = false;

        SteamOverlayPause.instance.BattleEventSystem = null;

        string battleName = "Battle";

        AsyncOperation async = SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName(battleName));

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(currentScene));

        async.allowSceneActivation = false;

        while (!async.isDone)
        {
            if (async.progress >= 0.9f)
            {
                async.allowSceneActivation = true;
            }
            yield return null;
        }

        ScreenFade.instance._ScreenTransitionController.GetComponent<UITransitionEffect>().Hide();

        GetComponent<AudioListener>().enabled = false;

        worldHolder.SetActive(true);

        MenuController.instance.gameObject.SetActive(true);

        ResetFieldPlayerAnimator();

        AudioManager.instance._backGroundMusic.PlayBackgroundMusic();

        if (returnToCheckPoint)
        {
            ResetPlayerStats();
            SetPlayerToCheckPoint();
        }

        cameraFollow.enabled = true;

        if(enemyObject != null)
        {
            if(enemyObject.enabled)
            {
                enemyObject.FieldEnemyAnimator._EnemyTriggerZone.DisableTrigger();
                enemyObject.HasAttacked = false;
            }
        }

        if (IsBossFight)
        {
            if (enemyObject != null)
            {
                if(isBossFight)
                   KillEnemy(true);
            }
        }

        yield return new WaitUntil(() => screenTransition.effectFactor <= 0.3f);

        if(enemyObject != null)
        {
            if(!isBossFight)
               KillEnemy(false);
        }

        yield return new WaitUntil(() => screenTransition.effectFactor <= 0);

        if(!isBossFight)
            MenuController.instance.CanToggleMenu = true;
    }

    private IEnumerator ReloadScene()
    {
        AudioManager.instance.PlaySoundEffect(AudioManager.instance.BattleTransitionAudio);

        AudioManager.instance.FadeOutBGMRoutine = StartCoroutine(AudioManager.instance.FadeOutBGM());

        ScreenFade.instance._ScreenTransitionController.GetComponent<UITransitionEffect>().Show();

        yield return new WaitUntil(() => ScreenFade.instance._ScreenTransitionController.GetComponent<UITransitionEffect>().effectFactor >= 1);

        string battleName = "Battle";

        AsyncOperation async = SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName(battleName));

        async.allowSceneActivation = false;

        while (!async.isDone)
        {
            if (async.progress >= 0.9f)
            {
                async.allowSceneActivation = true;
            }
            yield return null;
        }

        AsyncOperation asyncTwo = SceneManager.LoadSceneAsync(battleName, LoadSceneMode.Additive);

        asyncTwo.allowSceneActivation = false;

        while (!asyncTwo.isDone)
        {
            if (asyncTwo.progress >= 0.9f)
            {
                asyncTwo.allowSceneActivation = true;
            }
            yield return null;
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(battleName));

        scene = SceneManager.GetActiveScene();

        if(AudioManager.instance.FadeOutBGMRoutine != null)
        {
            StopCoroutine(AudioManager.instance.FadeOutBGMRoutine);
            AudioManager.instance.FadeOutBGMRoutine = null;
        }

        AudioManager.instance._backGroundMusic.PlayBackgroundMusic();
    }

    public void WaitToLoadFieldSceneButton()
    {
        ScreenFade.instance._ScreenTransitionController.SetScreenTransitionSprite(ScreenFade.instance._ScreenTransitionController.NonBattleTransition, false);

        screenTransition.Show();
        StartCoroutine(WaitToLoadFieldScene(true));
    }

    private void ShowGotItemPanel(string itemName)
    {
        gotItemAnimator.Play("GotItem", -1, 0);

        AudioManager.instance.PlaySoundEffect(AudioManager.instance.GotItemAudio);

        gotItemInfoText.text = "Obtained\n" + string.Format("\"{0}\"", itemName);

        itemTypeText.text = "Item";
    }

    private void KillEnemy(bool foughtBoss)
    {
        if(enemyObject.IsDead)
        {
            if(foughtBoss)
            {
                EquipmentUpgrade equipmentUpgrade = FindObjectOfType<EquipmentUpgrade>();

                equipmentUpgrade.SetUpgradeInformation();

                enemyObject.gameObject.SetActive(false);

                stageObjectives.CheckEnemyObjectiveMain();

                NodeManager.instance.WorldBossDefeated = true;

                scene = SceneManager.GetActiveScene();

                switch(scene.name)
                {
                    case "Forest_Boss":
                        bossData.forestBossDefeated = true;
                        break;
                    case "Desert_Boss":
                        bossData.desertBossDefeated = true;
                        break;
                    case "Arctic_Boss":
                        bossData.arcticBossDefeated = true;
                        break;
                    case "Graveyard_Boss":
                        bossData.graveBossDefeated = true;
                        break;
                }
            }
            else
            {
                enemyObject.GetComponent<EnemyTriggerZone>().enabled = false;

                enemyObject.GetComponent<EnemyTriggerZone>().FoundTargetParticle.gameObject.SetActive(false);

                stageObjectives.CheckEnemyObjectiveMain();

                enemyObject.EnemyAnimator.Play("Dead_Field");

                if (enemyObject.TargetSymbol != null)
                {
                    enemyObject.TargetSymbol.SetActive(false);
                }
                if (enemyObject.HasMoonStone)
                {
                    ShowGotItemPanel("Moonstone");

                    mainCharacterStats.moonStone++;

                    enemyObject.SetMoonStoneData();
                    enemyObject.MoonStoneSymbol.SetActive(false);

                    MenuController.instance._FieldTreasurePanel.DecrementFieldMoonstoneText();
                }

                if (enemyObject.IsSecretObjective && stageObjectives.SpecialObjective != Objectives.NONE)
                {
                    stageObjectives.UpdateSecretObjective();
                }

                stageObjectives.CheckAllEnemiesDefeatedObjectiveSecret();
                stageObjectives.CheckEnemiesDefeatedObjectiveSecret();

                if(enemyObject.IsOptionalBoss)
                {
                    Scene scene = SceneManager.GetActiveScene();

                    switch(scene.name)
                    {
                        case "Secret_Wood_3":
                            bossData.forestSecretBossDefeated = true;
                            break;
                        case "Secret_Desert_2":
                            bossData.desertSecretBossDefeated = true;
                            break;
                        case "Secret_Arctic_3":
                            bossData.arcticSecretBossDefeated = true;
                            break;
                        case "Secret_Graveyard_3":
                            bossData.graveSecretBossDefeated = true;
                            break;
                        case "Secret_Castle_3":
                            bossData.castleSecretBossDefeated = true;
                            break;
                    }

                    if(bossData.forestSecretBossDefeated && bossData.desertSecretBossDefeated && bossData.arcticSecretBossDefeated && bossData.graveSecretBossDefeated && bossData.castleSecretBossDefeated)
                    {
                        if(SteamManager.Initialized)
                        {
                            SteamUserStats.GetAchievement("ACH_ELIMINATE_THE_ELITE", out bool achievementCompleted);

                            if(!achievementCompleted)
                            {
                                SteamUserStats.SetAchievement("ACH_ELIMINATE_THE_ELITE");
                                SteamUserStats.StoreStats();
                            }
                        }
                    }
                }
            }
        }
        else
        {
            enemyObject.StartTriggerRoutine();
            StartCoroutine("ReEnableEnemyCollider");
        }
    }

    public void PlayHealParticle(HealingFieldItem healingItem)
    {
        healParticle.gameObject.SetActive(true);

        ParticleSystem pmain = healParticle;

        var col = pmain.colorOverLifetime;

        Gradient grad = new Gradient();

        ParticleSystem pChild = healParticleChild;

        var colTwo = pChild.colorOverLifetime;

        Gradient gradTwo = new Gradient();

        ParticleSystem.MainModule pChildTwo = healParticleChildTwo.main;

        if(healingItem._HealType == HealType.HP)
        {
            grad.SetKeys(new GradientColorKey[] { new GradientColorKey(Color.white, 0.0f), new GradientColorKey(Color.red, 1.0f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) });

            col.color = grad;

            gradTwo.SetKeys(new GradientColorKey[] { new GradientColorKey(Color.white, 0.0f), new GradientColorKey(Color.red, 1.0f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) });

            colTwo.color = grad;

            pChildTwo.startColor = heartPickUpColor;
        }
        else
        {
            grad.SetKeys(new GradientColorKey[] { new GradientColorKey(Color.white, 0.0f), new GradientColorKey(Color.yellow, 1.0f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) });

            col.color = grad;

            gradTwo.SetKeys(new GradientColorKey[] { new GradientColorKey(Color.white, 0.0f), new GradientColorKey(Color.yellow, 1.0f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) });

            colTwo.color = grad;

            pChildTwo.startColor = cardPickUpColor;
        }

        healParticle.Play();
    }

    private IEnumerator ReEnableEnemyCollider()
    {
        yield return new WaitForSeconds(1f);
        enemyObject._BoxCollider.enabled = true;
        enemyObject = null;
    }

    public void SetPlayerToCheckPoint()
    {
        player.position = checkPoint.position;

        if(gameObject.GetComponent<BossEventReset>())
        {
            gameObject.GetComponent<BossEventReset>().ResetBossEvent();
        }

        SetPlayerStats();
    }

    public void SetPlayerStats()
    {
        currentPlayerHealth = mainCharacterStats.currentPlayerHealth;
        currentPlayerCardPoints = mainCharacterStats.currentPlayerCardPoints;
    }

    public void ResetPlayerStats()
    {
        mainCharacterStats.currentPlayerHealth = currentPlayerHealth;
        mainCharacterStats.currentPlayerCardPoints = currentPlayerCardPoints;
    }

    public void CreateItemPickUpParticle(Vector3 pos)
    {
        var pickUp = Instantiate(gotItemParticlePrefab);

        pickUp.transform.position = new Vector3(pos.x, pos.y, pos.z);

        if (PlayerPrefs.HasKey("SoundEffects"))
        {
            pickUp.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SoundEffects");
            pickUp.GetComponent<AudioSource>().Play();
        }
        else
        {
            pickUp.GetComponent<AudioSource>().Play();
        }

        Destroy(pickUp.gameObject, 2f);
    }
}