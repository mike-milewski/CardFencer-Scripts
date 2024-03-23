using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;
using Steamworks;
using System.Security.Cryptography;

public class BattleSystem : MonoBehaviour
{
    [SerializeField]
    private List<BattleEnemy> enemies = new List<BattleEnemy>();

    [SerializeField]
    private List<Card> itemCards = new List<Card>();

    [SerializeField]
    private List<Card> charityCards = new List<Card>();

    [SerializeField]
    private EnemyChecker enemyChecker;

    private BattleEnemy enemyTarget;

    [SerializeField]
    private PlayerMenuInfo playerMenuInfo;

    [SerializeField]
    private Transform[] startingEnemyPositions;

    [SerializeField]
    private GameObject[] targetCircles;

    [SerializeField]
    private CardTemplate cardTemplate = null;

    [SerializeField]
    private ParticleEffectsManager particleEffectManager;

    [SerializeField]
    private Card usedCard = null, itemCard;

    private Card cardToAddForMysticEffect;

    [SerializeField]
    private Deck playerDeck;

    [SerializeField]
    private BattleDeck battleDeck;

    [SerializeField]
    private BattlePlayer battlePlayer;

    [SerializeField]
    private BattleResults battleResults;

    [SerializeField]
    private ComboCards comboCards;

    [SerializeField]
    private StickerPowerHolder stickerPowerHolder;

    [SerializeField]
    private StatusEffectManager statusEffectManager;

    [SerializeField]
    private EventSystem eventSystem;

    [SerializeField]
    private InputManager _inputManager;

    [SerializeField]
    private CanvasGroup battleUICanvasgroup;

    [SerializeField]
    private ParticleSystem cardParticle = null;

    [SerializeField]
    private PostProcessLayer mainCameraPostProcessLayer, uiCameraPostProcessLayer;

    [SerializeField]
    private HandCards handCards;

    [SerializeField]
    private Animator battleMessageAnimator, battleInformationAnimator, counterMeasurePromptAnimator, battleUIAnimator, victoryScreenAnimator, battleResultsPanelAnimator, defaultAttackAnimator, gameOverScreenAnimator,
                     stickerPowerMessagePanelAnimator, charityPanelAnimator, graveyardAnimator, deckAnimator, settingsButtonAnimator, battleTutorialOneAnimator, battleTutorialTwoAnimator, battleTutorialThreeAnimator;

    [SerializeField]
    private ScrollRectEx deckScroll, graveScroll;

    [SerializeField]
    private Button fleeButton, charityButton, deckButton, settingsButton;

    [SerializeField]
    private GameObject defaultSelectedObject, lastSelectedObject, gameOverScreen, victoryScreen, battleNumberParent, playerStatsHolder, targetCircle,
                       postGameOverSelectedObject, itemLayout, enemyAttackImage, enemyHealthBar, charityBlockers, counterAttackBar;

    [SerializeField]
    private GameObject objectHoveredOver = null, currentHoveredObject = null, tutorialPanel, statusEffectPanel, mulliganMenuConfirm, gameOverRetryObject, deckExitObject, graveExitObject,
                       noviceSword, veteranSword, eliteSword, noviceShield, veteranShield, eliteShield, noviceArmor, veteranArmor, eliteArmor, tutorialMenuConfirmBattleOne, tutorialMenuConfirmBattleTwo,
                       tutorialMenuConfirmBattleThree;

    [SerializeField]
    private GameObject[] itemSlots, itemLocks;

    [SerializeField]
    private TextMeshProUGUI battleNumber, statusEffectText, battleMessageText, stickerPowerMessageText, charityButtonText, charityPanelInfoText;

    [SerializeField]
    private Image battleInformationFrame, battleCardHolderImage;

    [SerializeField]
    private Sprite battleCardExtensionOne, battleCardExtensionTwo;

    [SerializeField]
    private Color playerActionColor, enemyActionColor, enemyTargetColor, playerTargetColor;

    [SerializeField]
    private TextMeshProUGUI battleInformationText, counterMeasurePromptText, counterMeasureButtonText, basicAttackDamageText;

    [SerializeField]
    private Transform victoryPosition, enemyAttackPositionPhysical, enemyAttackPositionRanged, graveyardPosition, worldCanvasParent, itemTransform, particlesPosition, cardHolder, deckParent, graveParent;

    private Transform playerPosition;

    [SerializeField]
    private RectTransform[] itemSlotPositions;

    [SerializeField]
    private LayerMask layerMaskToSwitchTo;

    [Header("Player Battle Number Position & Rotation")]
    [SerializeField]
    private Vector3 playerBattleNumberPosition;
    [SerializeField]
    private Quaternion playerBattleNumberRotation;

    private Navigation itemCardsNavigation;

    [SerializeField]
    private int targetIndex, enemyIndex, earlyTurnCountBonus;

    private int cardComboSelectedIndex, turnCount;

    [SerializeField]
    private bool isSelectingTarget, playersTurn, firstTurn, hittingAllEnemies, confirmedTarget, canOnlyHoverOverEnemies, canHoverOverTargets, hoverOverAllEnemies, checkedWin, selectedNormalAttack, usingCharity, 
                 usedCharity, usingMysticCard, retainCards, isDeckOpen, isGraveOpen, didPlayerTakeDamage, hasMysticFavoredPower, hasTheMagician, hasTheKnight, hasThePriest, hasPowerPatron, hasMagicMonster,
                 hasSupportStar, hasDoubleDamage, hasDoubleCast, hasFreeCast, hasStoppedEnemies, turnWasSkipped;

    [SerializeField]
    private bool isTutorial;

    private bool navagationDisabled;

    private Scene scene;

    public BattleDeck _BattleDeck => battleDeck;

    public InputManager _InputManager => _inputManager;

    public StatusEffectManager _StatusEffectManager => statusEffectManager;

    public ScrollRectEx DeckScroll
    {
        get
        {
            return deckScroll;
        }
        set
        {
            deckScroll = value;
        }
    }

    public ScrollRectEx GraveScroll
    {
        get
        {
            return graveScroll;
        }
        set
        {
            graveScroll = value;
        }
    }

    public ParticleEffectsManager _ParticleEffectManager => particleEffectManager;

    public BattlePlayer _battlePlayer => battlePlayer;

    public List<BattleEnemy> Enemies => enemies;

    public List<Card> CharityCards => charityCards;

    public PlayerMenuInfo _PlayerMenuInfo => playerMenuInfo;

    public BattleEnemy EnemyTarget
    {
        get
        {
            return enemyTarget;
        }
        set
        {
            enemyTarget = value;
        }
    }

    public StickerPowerHolder _StickerPowerHolder => stickerPowerHolder;

    public EventSystem _EventSystem => eventSystem;

    public Transform PlayerPosition => playerPosition;

    public Transform EnemyAttackPositionPhysical => enemyAttackPositionPhysical;

    public Transform EnemyAttackPositionRanged => enemyAttackPositionRanged;

    public Transform ParticlesPosition => particlesPosition;

    public Transform CardHolder => cardHolder;

    public Transform GraveyardPosition => graveyardPosition;

    public GameObject PlayerStatsHolder => playerStatsHolder;

    public GameObject LastSelectedObject => lastSelectedObject;

    public GameObject TutorialPanel => tutorialPanel;

    public GameObject TutorialMenuConfirmBattleOne => tutorialMenuConfirmBattleOne;

    public GameObject TutorialMenuConfirmBattleTwo => tutorialMenuConfirmBattleTwo;

    public GameObject TutorialMenuConfirmBattleThree => tutorialMenuConfirmBattleThree;

    public Animator BattleInformationAnimator => battleInformationAnimator;

    public Animator DefaultAttackAnimator => defaultAttackAnimator;

    public Animator BattleTutorialOneAnimator => battleTutorialOneAnimator;

    public Animator BattleTutorialTwoAnimator => battleTutorialTwoAnimator;

    public Animator BattleTutorialThreeAnimator => battleTutorialThreeAnimator;

    public Button CharityButton => charityButton;

    public CanvasGroup BattleUICanvasgroup => battleUICanvasgroup;

    public Card UsedCard
    {
        get
        {
            return usedCard;
        }
        set
        {
            usedCard = value;
        }
    }

    public Card CardToAddForMysticEffect
    {
        get
        {
            return cardToAddForMysticEffect;
        }
        set
        {
            cardToAddForMysticEffect = value;
        }
    }

    public CardTemplate _cardTemplate
    {
        get
        {
            return cardTemplate;
        }
        set
        {
            cardTemplate = value;
        }
    }

    public ParticleSystem CardParticle
    {
        get
        {
            return cardParticle;
        }
        set
        {
            cardParticle = value;
        }
    }

    public Animator CounterMeasurePromptAnimator
    {
        get
        {
            return counterMeasurePromptAnimator;
        }
        set
        {
            counterMeasurePromptAnimator = value;
        }
    }

    public GameObject ObjectHoveredOver
    {
        get
        {
            return objectHoveredOver;
        }
        set
        {
            objectHoveredOver = value;
        }
    }

    public GameObject CurrentHoveredObject
    {
        get
        {
            return currentHoveredObject;
        }
        set
        {
            currentHoveredObject = value;
        }
    }

    public Image BattleInformationFrame
    {
        get
        {
            return battleInformationFrame;
        }
        set
        {
            battleInformationFrame = value;
        }
    }

    public TextMeshProUGUI BattleInformationText
    {
        get
        {
            return battleInformationText;
        }
        set
        {
            battleInformationText = value;
        }
    }

    public TextMeshProUGUI CounterMeasurePromptText
    {
        get
        {
            return counterMeasurePromptText;
        }
        set
        {
            counterMeasurePromptText = value;
        }
    }

    public TextMeshProUGUI CounterMeasureButtonText
    {
        get
        {
            return counterMeasureButtonText;
        }
        set
        {
            counterMeasureButtonText = value;
        }
    }

    public Color BattleInformationPlayerActionColor => playerActionColor;

    public Color BattleInformationEnemyActionColor => enemyActionColor;

    public bool NavagationDisabled
    {
        get
        {
            return navagationDisabled;
        }
        set
        {
            navagationDisabled = value;
        }
    }

    public int TargetIndex
    {
        get
        {
            return targetIndex;
        }
        set
        {
            targetIndex = value;
        }
    }

    public bool UsingCharity
    {
        get
        {
            return usingCharity;
        }
        set
        {
            usingCharity = value;
        }
    }

    public bool HasDoubleDamage
    {
        get
        {
            return hasDoubleDamage;
        }
        set
        {
            hasDoubleDamage = value;
        }
    }

    public bool HasDoubleCast
    {
        get
        {
            return hasDoubleCast;
        }
        set
        {
            hasDoubleCast = value;
        }
    }

    public bool HasFreeCast
    {
        get
        {
            return hasFreeCast;
        }
        set
        {
            hasFreeCast = value;
        }
    }

    public bool HasStoppedEnemies
    {
        get
        {
            return hasStoppedEnemies;
        }
        set
        {
            hasStoppedEnemies = value;
        }
    }

    public bool HasMysticFavoredPower
    {
        get
        {
            return hasMysticFavoredPower;
        }
        set
        {
            hasMysticFavoredPower = value;
        }
    }

    public bool HasTheMagician
    {
        get
        {
            return hasTheMagician;
        }
        set
        {
            hasTheMagician = value;
        }
    }

    public bool HasTheKnight
    {
        get
        {
            return hasTheKnight;
        }
        set
        {
            hasTheKnight = value;
        }
    }

    public bool HasThePriest
    {
        get
        {
            return hasThePriest;
        }
        set
        {
            hasThePriest = value;
        }
    }

    public bool HasPowerPatron
    {
        get
        {
            return hasPowerPatron;
        }
        set
        {
            hasPowerPatron = value;
        }
    }

    public bool HasMagicMonster
    {
        get
        {
            return hasMagicMonster;
        }
        set
        {
            hasMagicMonster = value;
        }
    }

    public bool HasSupportStar
    {
        get
        {
            return hasSupportStar;
        }
        set
        {
            hasSupportStar = value;
        }
    }

    public bool IsDeckOpen
    {
        get
        {
            return isDeckOpen;
        }
        set
        {
            isDeckOpen = value;
        }
    }

    public bool IsGraveOpen
    {
        get
        {
            return isGraveOpen;
        }
        set
        {
            isGraveOpen = value;
        }
    }

    public bool UsingMysticCard
    {
        get
        {
            return usingMysticCard;
        }
        set
        {
            usingMysticCard = value;
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

    public bool DidPlayerTakeDamage
    {
        get
        {
            return didPlayerTakeDamage;
        }
        set
        {
            didPlayerTakeDamage = value;
        }
    }

    public bool TurnWasSkipped
    {
        get
        {
            return turnWasSkipped;
        }
        set
        {
            turnWasSkipped = value;
        }
    }

    public bool PlayersTurn
    {
        get
        {
            return playersTurn;
        }
        set
        {
            playersTurn = value;
        }
    }

    public bool RetainCards
    {
        get
        {
            return retainCards;
        }
        set
        {
            retainCards = value;
        }
    }

    public int TurnCount => turnCount;

    public bool FirstTurn => firstTurn;

    public bool IsSelectingTarget
    {
        get
        {
            return isSelectingTarget;
        }
        set
        {
            isSelectingTarget = value;
        }
    }

    public bool CanHoverOverTargets
    {
        get
        {
            return canHoverOverTargets;
        }
        set
        {
            canHoverOverTargets = value;
        }
    }

    public bool CanOnlyHoverOverEnemies
    {
        get
        {
            return canOnlyHoverOverEnemies;
        }
        set
        {
            canOnlyHoverOverEnemies = value;
        }
    }

    public bool HoverOverAllEnemies
    {
        get
        {
            return hoverOverAllEnemies;
        }
        set
        {
            hoverOverAllEnemies = value;
        }
    }

    public bool ConfirmedTarget
    {
        get
        {
            return confirmedTarget;
        }
        set
        {
            confirmedTarget = value;
        }
    }

    public bool HittingAllEnemies
    {
        get
        {
            return hittingAllEnemies;
        }
        set
        {
            hittingAllEnemies = value;
        }
    }

    public int EnemyIndex
    {
        get
        {
            return enemyIndex;
        }
        set
        {
            enemyIndex = value;
        }
    }

    private void Start()
    {
        firstTurn = true;

        targetIndex = 0;

        enemyChecker.EnableEnemies();

        SetUpPlayer();
        SetUpEnemies();
        SetUpItems();
        SetUpBattleHolder();

        targetCircle.SetActive(false);

        playerPosition = battlePlayer.DefaultPosition;

        scene = SceneManager.GetActiveScene();

        StartCoroutine(WaitToStartBattle());

        ScreenFade.instance._ScreenTransitionController.GetComponent<Coffee.UIEffects.UITransitionEffect>().Hide();

        AudioManager.instance.BackgroundMusic.loop = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if(_inputManager.CheckForController())
        {
            _inputManager.ControllerPluggedIn = true;
        }

        if(MenuController.instance._StickerPowerManager.CheckStartOfBattleStickerPower(StickerPower.Charity))
        {
            charityButton.gameObject.SetActive(true);
            charityButton.interactable = false;
        }
        else
        {
            charityButton.gameObject.SetActive(false);
        }
    }

    private void SetUpPlayer()
    {
        Vector3 battleNumberPosition = new Vector3(battlePlayer.transform.position.x - 0.6f, battlePlayer.transform.position.y + 2, battlePlayer.transform.position.z - 0.6f);

        Quaternion battleNumberRotation = Quaternion.Euler(playerBattleNumberRotation.x, playerBattleNumberRotation.y, playerBattleNumberRotation.z);

        Vector3 statusEffectTextPosition = new Vector3(battlePlayer.transform.position.x + 0.55f, battlePlayer.transform.position.y + 2, battlePlayer.transform.position.z);

        Quaternion statusEffectTextRotation = Quaternion.Euler(playerBattleNumberRotation.x, playerBattleNumberRotation.y, playerBattleNumberRotation.z);

        var number = Instantiate(battleNumber, battleNumberPosition, battleNumberRotation);

        var statusPanel = Instantiate(statusEffectPanel, statusEffectTextPosition, statusEffectTextRotation);

        number.transform.SetParent(battleNumberParent.transform);

        statusPanel.transform.SetParent(battleNumberParent.transform);

        TextMeshProUGUI statusText = statusPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        statusText.fontSize = 0.28f;

        battlePlayer.BattleNumber = number;
        battlePlayer.StatusEffectText = statusText;

        number.name = "BattleNumber - Player";
        statusPanel.name = "Status Effect Text - Player";

        if(MenuController.instance._StickerPowerManager.CheckStartOfBattleStickerPower(StickerPower.IncreaedBasicAttack))
        {
            basicAttackDamageText.color = Color.green;
            basicAttackDamageText.text = (battlePlayer.PlayerStrength + 3).ToString();
        }

        if(MenuController.instance._StickerPowerManager.CheckStartOfBattleStickerPower(StickerPower.StrengthUp))
        {
            basicAttackDamageText.color = Color.green;
            battlePlayer.StrengthBoost += 2;
            basicAttackDamageText.text = battlePlayer.PlayerStrength.ToString();
        }
        else if(MenuController.instance._StickerPowerManager.CheckStartOfBattleStickerPower(StickerPower.Enrage))
        {
            basicAttackDamageText.color = Color.green;
            battlePlayer.StrengthBoost += 7;
            basicAttackDamageText.text = (battlePlayer.PlayerStrength + 7).ToString();
        }
        else if(MenuController.instance._StickerPowerManager.CheckStartOfBattleStickerPower(StickerPower.Enrage) && 
                MenuController.instance._StickerPowerManager.CheckStartOfBattleStickerPower(StickerPower.StrengthUp))
        {
            basicAttackDamageText.color = Color.green;
            battlePlayer.StrengthBoost += 7;
            basicAttackDamageText.text = (battlePlayer.PlayerStrength + 9).ToString();
        }
        else
        {
            basicAttackDamageText.text = battlePlayer.PlayerStrength.ToString();
        }

        SetUpPlayerEquipment();
    }

    private void SetUpEnemies()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies.Count == 1)
            {
                enemies[0].IndexedEnemy = 0;

                enemies[0].DefaultPosition = startingEnemyPositions[1];

                enemies[0].transform.position = new Vector3(startingEnemyPositions[1].transform.position.x, enemies[0]._EnemyStats.defaultBattlePositionY, startingEnemyPositions[1].transform.position.z);

                enemies[0].StartingPositionIndex = 1;
            }
            else if(enemies.Count == 2)
            {
                enemies[0].IndexedEnemy = 0;

                enemies[0].DefaultPosition = startingEnemyPositions[0];

                enemies[0].transform.position = new Vector3(startingEnemyPositions[0].transform.position.x, enemies[0]._EnemyStats.defaultBattlePositionY, startingEnemyPositions[0].transform.position.z);

                enemies[0].StartingPositionIndex = 0;

                enemies[1].IndexedEnemy = 2;

                enemies[1].DefaultPosition = startingEnemyPositions[2];

                enemies[1].transform.position = new Vector3(startingEnemyPositions[2].transform.position.x, enemies[1]._EnemyStats.defaultBattlePositionY, startingEnemyPositions[2].transform.position.z);

                enemies[1].StartingPositionIndex = 2;
            }
            else
            {
                enemies[i].IndexedEnemy = i;

                enemies[i].StartingPositionIndex = i;

                enemies[i].DefaultPosition = startingEnemyPositions[i];

                enemies[i].transform.position = new Vector3(startingEnemyPositions[i].transform.position.x, enemies[i]._EnemyStats.defaultBattlePositionY, startingEnemyPositions[i].transform.position.z);
            }

            Quaternion statusEffectTextRotation = Quaternion.Euler(playerBattleNumberRotation.x, playerBattleNumberRotation.y + 30, playerBattleNumberRotation.z);

            var number = Instantiate(battleNumber, new Vector3(enemies[i].transform.position.x - 1, enemies[i].transform.position.y + 1, enemies[i].transform.position.z), Quaternion.identity);

            var statusPanel = Instantiate(statusEffectPanel, new Vector3(enemies[i].transform.position.x + 1.2f, enemies[i].transform.position.y + 1, enemies[i].transform.position.z), statusEffectTextRotation);

            var attackImage = Instantiate(enemyAttackImage, new Vector3(enemies[i].transform.position.x - 0.15f, enemies[i].transform.position.y + enemies[i]._EnemyStats.attackImagePositionY, 
                                                                        enemies[i].transform.position.z), enemyAttackImage.transform.rotation);

            var counterAttack = Instantiate(counterAttackBar, new Vector3(enemies[i].transform.position.x - 0.15f, enemies[i].transform.position.y + enemies[i]._EnemyStats.attackImagePositionY + 0.7f,
                                                                          enemies[i].transform.position.z), counterAttackBar.transform.rotation);

            counterAttack.GetComponent<CounterAttack>()._BattlePlayer = battlePlayer;

            battlePlayer._CounterAttack = counterAttack.GetComponent<CounterAttack>();

            var healthBar = Instantiate(enemyHealthBar, worldCanvasParent);

            var healthFill = healthBar.transform.GetChild(1).GetComponent<Image>();

            enemies[i]._CounterAttack = counterAttack.GetComponent<CounterAttack>();

            enemies[i].EnemyHealthBar = healthBar;

            enemies[i].SetHealthBarTransform();

            enemies[i].HealthText = healthBar.GetComponentInChildren<TextMeshProUGUI>();

            enemies[i].HealthText.text = enemies[i].CurrentHealth + "/" + enemies[i].MaxHealth;

            enemies[i].HealthFill = healthFill;

            enemies[i].StatusEffectHolder = healthBar.transform.GetChild(5).gameObject;

            number.transform.SetParent(battleNumberParent.transform);
            statusPanel.transform.SetParent(battleNumberParent.transform);
            attackImage.transform.SetParent(battleNumberParent.transform);
            counterAttack.transform.SetParent(battleNumberParent.transform);

            TextMeshProUGUI statusText = statusPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

            enemies[i].BattleNumber = number;
            enemies[i].StatusEffectText = statusText;
            enemies[i].EnemyAttackImage = attackImage;

            number.fontSize = 0.55f;
            statusText.fontSize = 0.28f;

            number.name = "BattleNumber - " + enemies[i].name;
            statusPanel.name = "Status Effect Text - " + enemies[i].name;
            attackImage.name = "Attack Image - " + enemies[i].name;
            healthBar.name = "Health Bar - " + enemies[i].name;
            counterAttack.name = "Counterattack Bar - " + enemies[i].name;

            attackImage.SetActive(false);

            number.transform.rotation = Quaternion.Euler(number.transform.rotation.x, number.transform.rotation.y - 30, number.transform.rotation.z);
            statusPanel.transform.rotation = Quaternion.Euler(number.transform.rotation.x, number.transform.rotation.y - 30, number.transform.rotation.z);

            if(enemies.Count == 2)
            {
                enemies[i].IndexedEnemy = i;
            }
        }

        SetEnemyButtonNavigations();
        CutEnemyHealth();
    }

    private void CutEnemyHealth()
    {
        if(MenuController.instance._StickerPowerManager.CheckStartOfBattleStickerPower(StickerPower.Hunter))
        {
            stickerPowerHolder.CreateStickerMessage(StickerPower.Hunter);

            foreach (BattleEnemy enemy in enemies)
            {
                int percentage = Mathf.RoundToInt(enemy._EnemyStats.health * 0.10f);

                enemy.CurrentHealth -= percentage;
                enemy.CalculateHealth();
            }
        }
    }

    private void SetUpPlayerEquipment()
    {
        switch(playerMenuInfo.weaponIndex)
        {
            case 1:
                noviceSword.SetActive(false);
                veteranSword.SetActive(true);
                battlePlayer.WeaponHitBox = battlePlayer.VeteranSwordHitBox;
                break;
            case 2:
                noviceSword.SetActive(false);
                eliteSword.SetActive(true);
                battlePlayer.WeaponHitBox = battlePlayer.EliteSwordHitBox;
                break;
        }

        switch(playerMenuInfo.shieldIndex)
        {
            case 1:
                noviceShield.SetActive(false);
                veteranShield.SetActive(true);
                break;
            case 2:
                noviceShield.SetActive(false);
                eliteShield.SetActive(true);
                break;
        }

        switch(playerMenuInfo.armorIndex)
        {
            case 1:
                noviceArmor.SetActive(false);
                veteranArmor.SetActive(true);
                break;
            case 2:
                noviceArmor.SetActive(false);
                eliteArmor.SetActive(true);
                break;
        }
    }

    public void SetEnemyButtonNavigations()
    {
        if(enemies.Count > 0)
        {
            for(int i = 0; i < enemies.Count; i++)
            {
                Navigation enemyNav = enemies[i].GetComponent<Selectable>().navigation;

                enemyNav.selectOnLeft = null;
                enemyNav.selectOnRight = null;

                if(i == 0)
                {
                    if(enemies.Count > 1)
                    {
                        enemyNav.selectOnLeft = enemies[enemies.Count - 1].GetComponent<Selectable>();
                        enemyNav.selectOnRight = enemies[i + 1].GetComponent<Selectable>();
                    }
                }
                else if(i >= enemies.Count - 1)
                {
                    enemyNav.selectOnLeft = enemies[i - 1].GetComponent<Selectable>();
                    enemyNav.selectOnRight = enemies[0].GetComponent<Selectable>();
                }
                else
                {
                    enemyNav.selectOnLeft = enemies[i - 1].GetComponent<Selectable>();
                    enemyNav.selectOnRight = enemies[i + 1].GetComponent<Selectable>();
                }

                enemies[i].GetComponent<Selectable>().navigation = enemyNav;
            }
        }
    }

    private IEnumerator WaitToChangeCharityNavigations()
    {
        yield return new WaitForSeconds(0.5f);

        CheckCharityButtonNavigation();
    }

    public void StartWaitToChaneCharityNavigations()
    {
        StartCoroutine(WaitToChangeCharityNavigations());
    }

    public void CheckCharityButtonNavigation()
    {
        if(charityButton.gameObject.activeSelf && charityButton.interactable)
        {
            Navigation nav = charityButton.GetComponent<Selectable>().navigation;

            if(battlePlayer.StatusEffectHolder.transform.childCount > 0)
            {
                nav.selectOnUp = battlePlayer.StatusEffectHolder.transform.GetChild(0).GetComponent<Selectable>();
            }
            else
            {
                switch (enemies.Count)
                {
                    case (1):
                        if (enemies[0].StatusEffectHolder.transform.childCount > 0)
                        {
                            nav.selectOnUp = enemies[0].StatusEffectHolder.transform.GetChild(0).GetComponent<Selectable>();
                        }
                        break;
                    case (2):
                        if (enemies[0].StatusEffectHolder.transform.childCount > 0)
                        {
                            nav.selectOnUp = enemies[0].StatusEffectHolder.transform.GetChild(0).GetComponent<Selectable>();
                        }
                        if (enemies[0].StatusEffectHolder.transform.childCount <= 0 && enemies[1].StatusEffectHolder.transform.childCount > 0)
                        {
                            nav.selectOnUp = enemies[1].StatusEffectHolder.transform.GetChild(0).GetComponent<Selectable>();
                        }
                        break;
                    case (3):
                        if (enemies[0].StatusEffectHolder.transform.childCount > 0)
                        {
                            nav.selectOnUp = enemies[0].StatusEffectHolder.transform.GetChild(0).GetComponent<Selectable>();
                        }
                        if (enemies[0].StatusEffectHolder.transform.childCount <= 0 && enemies[1].StatusEffectHolder.transform.childCount > 0)
                        {
                            nav.selectOnUp = enemies[1].StatusEffectHolder.transform.GetChild(0).GetComponent<Selectable>();
                        }
                        if (enemies[1].StatusEffectHolder.transform.childCount <= 0 && enemies[2].StatusEffectHolder.transform.childCount > 0)
                        {
                            nav.selectOnUp = enemies[2].StatusEffectHolder.transform.GetChild(0).GetComponent<Selectable>();
                        }
                        break;
                }
            }

            nav.selectOnDown = defaultAttackAnimator.GetComponent<Selectable>();

            charityButton.GetComponent<Selectable>().navigation = nav;
        }
        else
        {
            Navigation nav = defaultAttackAnimator.GetComponent<Selectable>().navigation;

            SetDefaultAttackNavigation(nav);
        }
    }

    private void SetDefaultAttackNavigation(Navigation atkNavigation)
    {
        if (battlePlayer.StatusEffectHolder.transform.childCount > 0)
        {
            atkNavigation.selectOnUp = battlePlayer.StatusEffectHolder.transform.GetChild(0).GetComponent<Selectable>();
        }
        else
        {
            switch (enemies.Count)
            {
                case (1):
                    if(enemies[0].StatusEffectHolder.transform.childCount > 0)
                    {
                        atkNavigation.selectOnUp = enemies[0].StatusEffectHolder.transform.GetChild(0).GetComponent<Selectable>();
                    }
                    else
                    {
                        atkNavigation.selectOnUp = null;
                    }
                    break;
                case (2):
                    if (enemies[0].StatusEffectHolder.transform.childCount > 0)
                    {
                        atkNavigation.selectOnUp = enemies[0].StatusEffectHolder.transform.GetChild(0).GetComponent<Selectable>();
                    }
                    else if (enemies[0].StatusEffectHolder.transform.childCount <= 0 && enemies[1].StatusEffectHolder.transform.childCount > 0)
                    {
                        atkNavigation.selectOnUp = enemies[1].StatusEffectHolder.transform.GetChild(0).GetComponent<Selectable>();
                    }
                    else
                    {
                        atkNavigation.selectOnUp = null;
                    }
                    break;
                case (3):
                    if (enemies[0].StatusEffectHolder.transform.childCount > 0)
                    {
                        atkNavigation.selectOnUp = enemies[0].StatusEffectHolder.transform.GetChild(0).GetComponent<Selectable>();
                    }
                    else if (enemies[0].StatusEffectHolder.transform.childCount <= 0 && enemies[1].StatusEffectHolder.transform.childCount > 0)
                    {
                        atkNavigation.selectOnUp = enemies[1].StatusEffectHolder.transform.GetChild(0).GetComponent<Selectable>();
                    }
                    else if (enemies[1].StatusEffectHolder.transform.childCount <= 0 && enemies[2].StatusEffectHolder.transform.childCount > 0)
                    {
                        atkNavigation.selectOnUp = enemies[2].StatusEffectHolder.transform.GetChild(0).GetComponent<Selectable>();
                    }
                    else
                    {
                        atkNavigation.selectOnUp = null;
                    }
                    break;
            }
        }

        defaultAttackAnimator.GetComponent<Selectable>().navigation = atkNavigation;
    }

    private void SetUpItems()
    {
        for(int i = 0; i < playerMenuInfo.currentItemSlotSize; i++)
        {
            itemLocks[i].SetActive(false);
        }

        if(playerMenuInfo.currentEquippedItems.Count > 0)
        {
            for(int i = 0; i < playerMenuInfo.currentEquippedItems.Count; i++)
            {
                Card item = Instantiate(itemCard, itemTransform);

                item.gameObject.SetActive(true);

                item._cardTemplate = playerMenuInfo.currentEquippedItems[i];

                item.CheckCardType();

                item.CheckCardStatusEffect();

                item.CheckForEnrageStatusEffect();

                item.CheckStagePenalties();

                item.GetComponent<RectTransform>().anchoredPosition = itemSlotPositions[i].anchoredPosition;

                item.CardButton.interactable = true;

                itemCards.Add(item);

                itemSlots[i].SetActive(false);
            }
        }
    }

    private void SetUpBattleHolder()
    {
        RectTransform rectTransform = battleCardHolderImage.gameObject.GetComponent<RectTransform>();

        if(playerMenuInfo.currentHandSize == 4)
        {
            battleCardHolderImage.sprite = battleCardExtensionOne;

            rectTransform.anchoredPosition = new Vector2(99.8f, rectTransform.anchoredPosition.y);

            rectTransform.sizeDelta = new Vector2(310, rectTransform.sizeDelta.y);

            comboCards.CombinedCard.GetComponent<RectTransform>().anchoredPosition = new Vector2(148, comboCards.CombinedCard.GetComponent<RectTransform>().anchoredPosition.y);
        }
        else if(playerMenuInfo.currentHandSize == 5)
        {
            battleCardHolderImage.sprite = battleCardExtensionTwo;

            rectTransform.anchoredPosition = new Vector2(131.7f, rectTransform.anchoredPosition.y);

            rectTransform.sizeDelta = new Vector2(365, rectTransform.sizeDelta.y);

            comboCards.CombinedCard.GetComponent<RectTransform>().anchoredPosition = new Vector2(180, comboCards.CombinedCard.GetComponent<RectTransform>().anchoredPosition.y);
        }
    }

    private void Update()
    {
        if(gameOverScreen.activeInHierarchy)
        {
            if(_inputManager.ControllerPluggedIn)
            {
                if(_InputManager.ControllerName == "xbox")
                {
                    if (Input.GetButtonDown("XboxAttack"))
                    {
                        gameOverScreenAnimator.Play("GameOverScreen", -1, 1);
                    }
                }
                else
                {
                    if (Input.GetButtonDown("Ps4Attack"))
                    {
                        gameOverScreenAnimator.Play("GameOverScreen", -1, 1);
                    }
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    gameOverScreenAnimator.Play("GameOverScreen", -1, 1);
                }
            }
        }

        if (playersTurn)
        {
            CardSelection();

            if (isSelectingTarget)
            {
                if(confirmedTarget)
                {
                    if(!CheckControllerConnection())
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            if (SteamOverlayPause.instance.IsPaused) return;

                            canHoverOverTargets = false;

                            SelectedAction(cardTemplate);

                            DisableTargetCircle();
                            DisableTargetCircleForAll();

                            isSelectingTarget = false;
                        }
                    }
                }
            }

            if(CheckControllerConnection())
            {
                if(_InputManager.ControllerName == "xbox")
                {
                    if (Input.GetButtonDown("XboxCancel"))
                    {
                        if (confirmedTarget)
                        {
                            UndoAction();
                            if (CheckControllerConnection())
                            {
                                _inputManager.SetSelectedObject(lastSelectedObject);
                            }
                        }
                    }
                }
                else
                {
                    if (Input.GetButtonDown("Ps4Cancel"))
                    {
                        if (confirmedTarget)
                        {
                            UndoAction();
                            if (CheckControllerConnection())
                            {
                                _inputManager.SetSelectedObject(lastSelectedObject);
                            }
                        }
                    }
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(2))
                {
                    if (SteamOverlayPause.instance.IsPaused) return;

                    if (!confirmedTarget)
                        UndoAction();
                }
            }
        }
    }

    public void SelectTargetController()
    {
        if(isSelectingTarget)
        {
            if(confirmedTarget)
            {
                if (CheckControllerConnection())
                {
                    if(_InputManager.ControllerName == "xbox")
                    {
                        if (Input.GetButtonDown("XboxAttack"))
                        {
                            eventSystem.sendNavigationEvents = false;

                            navagationDisabled = true;

                            canHoverOverTargets = false;

                            SelectedAction(cardTemplate);

                            DisableTargetCircle();
                            DisableTargetCircleForAll();

                            isSelectingTarget = false;
                        }
                    }
                    else
                    {
                        if (Input.GetButtonDown("Ps4Attack"))
                        {
                            eventSystem.sendNavigationEvents = false;

                            navagationDisabled = true;

                            canHoverOverTargets = false;

                            SelectedAction(cardTemplate);

                            DisableTargetCircle();
                            DisableTargetCircleForAll();

                            isSelectingTarget = false;
                        }
                    }
                }
            }
        }
    }

    public void UndoAction()
    {
        if(!usingMysticCard)
        {
            if (usedCard != null)
            {
                usedCard.SelectedCardAnimator.Play("Idle");
            }

            if (lastSelectedObject != null)
            {
                if (lastSelectedObject.GetComponent<Card>())
                {
                    lastSelectedObject.GetComponent<Card>().SelectedCardAnimator.Play("Idle");
                }
            }

            DeselectTargets(true);
            DeselectDefaultAttack();

            cardTemplate = null;

            if (charityButton.gameObject.activeSelf && !usedCharity)
                EnableCharityButton();
        }
    }

    public void ChooseTarget(GameObject selected)
    {
        if(playersTurn)
        {
            isSelectingTarget = true;

            if(!CheckControllerConnection())
                eventSystem.firstSelectedGameObject = selected;

            if(!CheckControllerConnection())
                eventSystem.SetSelectedGameObject(eventSystem.firstSelectedGameObject);

            battleInformationAnimator.Play("BattleInformation");

            if (selected.GetComponent<BattleEnemy>())
            {
                if(!CheckControllerConnection())
                {
                    eventSystem.SetSelectedGameObject(enemies[targetIndex].gameObject);
                }

                EnableTargetCircle(new Vector3(enemies[targetIndex].transform.position.x, targetCircle.transform.position.y, enemies[targetIndex].transform.position.z));

                targetCircle.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(10, 10);

                if(selected.GetComponent<BattleEnemy>())
                {
                    if(!CheckControllerConnection())
                    {
                        battleInformationText.text = "<size=20>L</size><size=17>v</size><size=20>: " + eventSystem.currentSelectedGameObject.GetComponent<BattleEnemy>()._EnemyStats.level + " " + 
                                                     eventSystem.currentSelectedGameObject.GetComponent<BattleEnemy>()._EnemyStats.enemyName;
                    }
                    else
                    {
                        battleInformationText.text = "<size=20>L</size><size=17>v</size><size=20>: " + enemies[targetIndex]._EnemyStats.level + " " +
                                                     enemies[targetIndex]._EnemyStats.enemyName;
                    }
                }
            }
            else
            {
                EnableTargetCircle(battlePlayer.transform.position);

                targetCircle.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(5, 5);

                if(!CheckControllerConnection())
                {
                    battleInformationText.text = eventSystem.currentSelectedGameObject.GetComponent<BattlePlayer>()._mainCharacterStats.playerName;
                }
                else
                {
                    battleInformationText.text = _inputManager.CurrentSelectedObject.GetComponent<BattlePlayer>()._mainCharacterStats.playerName;
                }
            }
        }
    }

    public void SetEnemyIndex()
    {
        if(enemies.Count > 0)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].IndexedEnemy = i;
            }
        }
    }

    public void DeselectTargets(bool removeTargets)
    {
        SetSelectedObjectToNull();

        targetIndex = 0;

        if (battleInformationAnimator.GetCurrentAnimatorClipInfo(0).Length > 0)
            battleInformationAnimator.Play("Reverse");

        DisableTargetCircle();
        DisableTargetCircleForAll();

        confirmedTarget = false;
        if (removeTargets)
        {
            canHoverOverTargets = false;
            isSelectingTarget = false;
        }
    }

    public void ChooseAllEnemyTargets()
    {
        battleInformationAnimator.Play("BattleInformation");

        targetCircle.SetActive(false);

        isSelectingTarget = true;

        foreach(BattleEnemy enemy in enemies)
        {
            targetCircles[enemy.StartingPositionIndex].gameObject.SetActive(true);
        }

        if(enemies.Count > 1)
        {
            if(usedCard._cardTemplate.target == Target.RandomEnemy)
            {
                battleInformationText.text = "Random Enemy";
            }
            else
            {
                battleInformationText.text = "All Enemies";
            }
        }
        else
        {
            battleInformationText.text = enemies[0]._EnemyStats.enemyName;
        }
    }

    private void CheckLastSelectedObject()
    {
        if (lastSelectedObject.GetComponent<Card>())
        {
            lastSelectedObject.GetComponent<Card>().SelectedCardAnimator.Play("Idle");
        }
    }

    public void ChangeBasicAttackDamageValue()
    {
        int strength = MenuController.instance._StickerPowerManager.CheckStartOfBattleStickerPower(StickerPower.StrengthUp) ? (battlePlayer._mainCharacterStats.strength - 2) + battlePlayer.StrengthBoost :
                       battlePlayer._mainCharacterStats.strength + battlePlayer.StrengthBoost;

        if(strength > battlePlayer._mainCharacterStats.strength)
        {
            basicAttackDamageText.text = strength.ToString();
            basicAttackDamageText.color = Color.green;
        }
        else if(strength < battlePlayer._mainCharacterStats.strength)
        {
            basicAttackDamageText.text = strength.ToString();
            basicAttackDamageText.color = new Color(1, 0.4591194f, 0.4591194f, 1);
        }
        else
        {
            basicAttackDamageText.text = strength.ToString();
            basicAttackDamageText.color = Color.white;
        }
    }

    public void SetCardStrength(int value, bool increase)
    {
        foreach(Card card in battleDeck.CardsInHand)
        {
            if(increase)
            {
                card.ChangeCardStrength(false, value);
            }
            else
            {
                card.ChangeCardStrength(false, -value);
            }
        }

        if(battleDeck._Card.Count > 0)
        {
            foreach (Card crd in battleDeck._Card)
            {
                if(increase)
                {
                    crd.ChangeCardStrength(false, value);
                }
                else
                {
                    crd.ChangeCardStrength(false, -value);
                }
            }
        }

        comboCards.CombinedCard.ChangeCardStrength(false, value);

        Graveyard grave = graveyardPosition.GetComponent<Graveyard>();

        if(grave.cards.Count > 0)
        {
            foreach(Card c in grave.cards)
            {
                if(increase)
                {
                    c.ChangeCardStrength(false, value);
                }
                else
                {
                    c.ChangeCardStrength(false, -value);
                }
            }
        }
    }

    public void DoubleCardStrength()
    {
        foreach (Card card in battleDeck.CardsInHand)
        {
            card.DoubleCardStrength();
        }

        if (battleDeck._Card.Count > 0)
        {
            foreach (Card crd in battleDeck._Card)
            {
                crd.DoubleCardStrength();
            }
        }

        comboCards.CombinedCard.DoubleCardStrength();

        Graveyard grave = graveyardPosition.GetComponent<Graveyard>();

        if (grave.cards.Count > 0)
        {
            foreach (Card c in grave.cards)
            {
                c.DoubleCardStrength();
            }
        }
    }

    public void HalveCardStrength()
    {
        foreach (Card card in battleDeck.CardsInHand)
        {
            card.ResetCardStrength();
        }

        if (battleDeck._Card.Count > 0)
        {
            foreach (Card crd in battleDeck._Card)
            {
                crd.ResetCardStrength();
            }
        }

        Graveyard grave = graveyardPosition.GetComponent<Graveyard>();

        if (grave.cards.Count > 0)
        {
            foreach (Card c in grave.cards)
            {
                c.ResetCardStrength();
            }
        }
    }

    public void ResetCardStrength()
    {
        foreach (Card card in battleDeck.CardsInHand)
        {
            card.ResetCardStrength();
        }

        if (battleDeck._Card.Count > 0)
        {
            foreach (Card crd in battleDeck._Card)
            {
                crd.ResetCardStrength();
            }
        }

        Graveyard grave = graveyardPosition.GetComponent<Graveyard>();

        if (grave.cards.Count > 0)
        {
            foreach (Card c in grave.cards)
            {
                c.ResetCardStrength();
            }
        }
    }

    public void SelectedAction(CardTemplate ct = null)
    {
        DisableBattleUIInteracteability();

        if(!hittingAllEnemies)
            enemyTarget = enemies[targetIndex];

        if(ct != null)
        {
            switch (ct.cardType)
            {
                case (CardType.Action):
                    if(ct.isAProjectile || ct.isActionAOE)
                    {
                        battlePlayer._BattleActions = BattleActions.spell;
                    }
                    else
                    {
                        battlePlayer._BattleActions = BattleActions.closeUpAttack;
                    }
                    battlePlayer.PlayerStrength = usedCard.CardStrength;
                    break;
                case (CardType.Item):
                    battlePlayer._BattleActions = BattleActions.item;
                    battlePlayer.PlayerStrength = usedCard.CardStrength;
                    break;
                case (CardType.Magic):
                    battlePlayer._BattleActions = BattleActions.spell;
                    battlePlayer.PlayerStrength = usedCard.CardStrength;
                    break;
                case (CardType.Support):
                    battlePlayer._BattleActions = BattleActions.support;
                    battlePlayer.PlayerStrength = usedCard.CardStrength;
                    break;
                case (CardType.Mystic):
                    battlePlayer._BattleActions = BattleActions.helper;
                    break;
            }
            battlePlayer.Action();

            if(!hasFreeCast)
            {
                if (!hasMysticFavoredPower)
                {
                    battlePlayer.CalculateCardPoints(-usedCard.CardPoints);
                    battlePlayer._mainCharacterStats.currentPlayerCardPoints = battlePlayer.CurrentCardPoints;
                }
            }

            battleInformationAnimator.Play("BattleInformation");
            battleInformationText.text = ct.cardName;
        }
        else
        {
            hasMysticFavoredPower = false;

            ResetHandObjects();

            battlePlayer._BattleActions = BattleActions.closeUpAttack;
            battlePlayer.DisableBattleInformation();
            battlePlayer.Action();
        }
        AudioManager.instance.PlaySoundEffect(AudioManager.instance.SelectedAudio);
    }

    private void CardSelection()
    {
        if (objectHoveredOver != null)
        {
            if(objectHoveredOver.GetComponent<Card>())
            {
                if (CheckControllerConnection())
                {
                    if(!isDeckOpen && !isGraveOpen)
                    {
                        if (_inputManager._EventSystem.sendNavigationEvents)
                            ControllerCardSelection();
                    }
                }
                else
                {
                    MouseCardSelection();
                }
            }
        }
    }

    private void MouseCardSelection()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Card c = objectHoveredOver.GetComponent<Card>();
            currentHoveredObject = c.gameObject;

            if (c.CardButton.interactable && !usingCharity)
            {
                if (c.CanCombineWithOtherCards && !c.IsAcombinedCard && !c.CannotBeUsed && !c._cardTemplate.combination.CannotBeUsedToCombine)
                {
                    if (!c.ChosenForCombining)
                    {
                        DisableCharityButton();

                        if (cardComboSelectedIndex < 3)
                        {
                            c._Animator.Play("SlideUp");

                            AudioManager.instance.PlaySoundEffect(AudioManager.instance.CursorAudio);

                            cardComboSelectedIndex++;

                            c.ChosenForCombining = true;

                            c.SelectedIndexText.text = cardComboSelectedIndex.ToString();

                            comboCards._Card.Add(c);

                            comboCards.CurrentSelectedCards++;

                            comboCards.CheckCards(c);

                            c.SelectionObject.SetActive(true);

                            comboCards.CheckCombinations();

                            c.DefaultColorFrame();
                        }
                        else
                        {
                            ShowBattleMessage("You cannot combine anymore cards!");
                        }
                    }
                    else
                    {
                        c._Animator.Play("SlideDown");

                        AudioManager.instance.PlaySoundEffect(AudioManager.instance.CursorAudio);

                        cardComboSelectedIndex--;

                        c.ChosenForCombining = false;

                        c.PlayHoverAnimation();

                        c.SelectedIndexText.text = cardComboSelectedIndex.ToString();

                        comboCards.CurrentSelectedCards--;

                        comboCards._Card.Remove(c);

                        comboCards.CheckCards(c);

                        comboCards.CheckIncompatibleCards();

                        comboCards.CheckSelectedCardNumbers();

                        if (comboCards.CurrentSelectedCards <= 1)
                        {
                            comboCards.ResetCardNavigations();

                            if (comboCards.CombinedCardAnimator.GetCurrentAnimatorClipInfo(0).Length > 0)
                            {
                                comboCards.CombinedCardAnimator.Play("Reverse");
                                comboCards.CombinedCard.SelectedCardAnimator.Play("Idle");
                                comboCards.CombinedCard.CardButton.interactable = false;

                                comboCards.CombinedCard.HideStatusEffectPanel();
                            }

                            comboCards.ClearCardComboList();
                            comboCards.CheckCombinations();

                            DeselectTargets(true);

                            ResetSelectedCardAnimation();
                        }
                        else
                        {
                            comboCards.CheckCombinations();

                            DeselectTargets(true);
                            comboCards.CombinedCard.SelectedCardAnimator.Play("Idle");
                            comboCards.CombinedCard.CardButton.interactable = false;
                        }

                        if (cardComboSelectedIndex <= 0)
                        {
                            EnableCharityButton();

                            comboCards.DisableCombineFrames();
                            comboCards.ClearCardComboList();
                        }

                        c.SelectionObject.SetActive(false);
                    }
                }
                else
                {
                    if (!c.IsAcombinedCard)
                        ShowBattleMessage("This card cannot be combined!");
                }
            }
        }
    }

    private void ControllerCardSelection()
    {
        if(_InputManager.ControllerName == "xbox")
        {
            if(SteamUtils.IsSteamRunningOnSteamDeck())
            {
                if (Input.GetButtonDown("XboxOpenChest"))
                {
                    Card c = objectHoveredOver.GetComponent<Card>();
                    currentHoveredObject = c.gameObject;

                    if (c.CardButton.interactable && !usingCharity)
                    {
                        if (c.CanCombineWithOtherCards && !c.IsAcombinedCard && !c.CannotBeUsed && !c._cardTemplate.combination.CannotBeUsedToCombine)
                        {
                            if (!c.ChosenForCombining)
                            {
                                DisableCharityButton();

                                if (cardComboSelectedIndex < 3)
                                {
                                    AudioManager.instance.PlaySoundEffect(AudioManager.instance.CursorAudio);

                                    cardComboSelectedIndex++;

                                    c.ChosenForCombining = true;

                                    c.SelectedIndexText.text = cardComboSelectedIndex.ToString();

                                    comboCards._Card.Add(c);

                                    comboCards.CurrentSelectedCards++;

                                    comboCards.CheckCards(c);

                                    c.SelectionObject.SetActive(true);

                                    comboCards.CheckCombinations();

                                    c.DefaultColorFrame();
                                }
                                else
                                {
                                    ShowBattleMessage("You cannot combine anymore cards!");
                                }
                            }
                            else
                            {
                                AudioManager.instance.PlaySoundEffect(AudioManager.instance.CursorAudio);

                                cardComboSelectedIndex--;

                                c.ChosenForCombining = false;

                                c.PlayHoverAnimation();

                                c.SelectedIndexText.text = cardComboSelectedIndex.ToString();

                                comboCards.CurrentSelectedCards--;

                                comboCards._Card.Remove(c);

                                comboCards.CheckCards(c);

                                comboCards.CheckIncompatibleCards();

                                comboCards.CheckSelectedCardNumbers();

                                if (comboCards.CurrentSelectedCards <= 1)
                                {
                                    comboCards.ResetCardNavigations();

                                    if (comboCards.CombinedCardAnimator.GetCurrentAnimatorClipInfo(0).Length > 0)
                                    {
                                        comboCards.CombinedCardAnimator.Play("Reverse");
                                        comboCards.CombinedCard.SelectedCardAnimator.Play("Idle");
                                        comboCards.CombinedCard.CardButton.interactable = false;

                                        comboCards.CombinedCard.HideStatusEffectPanel();
                                    }

                                    comboCards.ClearCardComboList();
                                    comboCards.CheckCombinations();

                                    DeselectTargets(true);

                                    ResetSelectedCardAnimation();
                                }
                                else
                                {
                                    comboCards.CheckCombinations();

                                    DeselectTargets(true);
                                    comboCards.CombinedCard.SelectedCardAnimator.Play("Idle");
                                    comboCards.CombinedCard.CardButton.interactable = false;
                                }

                                if (cardComboSelectedIndex <= 0)
                                {
                                    EnableCharityButton();

                                    comboCards.DisableCombineFrames();
                                    comboCards.ClearCardComboList();
                                }

                                c.SelectionObject.SetActive(false);
                            }
                        }
                        else
                        {
                            if (!c.IsAcombinedCard)
                                ShowBattleMessage("This card cannot be combined!");
                        }
                    }
                }
            }
            else
            {
                if (Input.GetButtonDown("XboxOpenChest"))
                {
                    Card c = objectHoveredOver.GetComponent<Card>();
                    currentHoveredObject = c.gameObject;

                    if (c.CardButton.interactable && !usingCharity)
                    {
                        if (c.CanCombineWithOtherCards && !c.IsAcombinedCard && !c.CannotBeUsed && !c._cardTemplate.combination.CannotBeUsedToCombine)
                        {
                            if (!c.ChosenForCombining)
                            {
                                DisableCharityButton();

                                if (cardComboSelectedIndex < 3)
                                {
                                    AudioManager.instance.PlaySoundEffect(AudioManager.instance.CursorAudio);

                                    cardComboSelectedIndex++;

                                    c.ChosenForCombining = true;

                                    c.SelectedIndexText.text = cardComboSelectedIndex.ToString();

                                    comboCards._Card.Add(c);

                                    comboCards.CurrentSelectedCards++;

                                    comboCards.CheckCards(c);

                                    c.SelectionObject.SetActive(true);

                                    comboCards.CheckCombinations();

                                    c.DefaultColorFrame();
                                }
                                else
                                {
                                    ShowBattleMessage("You cannot combine anymore cards!");
                                }
                            }
                            else
                            {
                                AudioManager.instance.PlaySoundEffect(AudioManager.instance.CursorAudio);

                                cardComboSelectedIndex--;

                                c.ChosenForCombining = false;

                                c.PlayHoverAnimation();

                                c.SelectedIndexText.text = cardComboSelectedIndex.ToString();

                                comboCards.CurrentSelectedCards--;

                                comboCards._Card.Remove(c);

                                comboCards.CheckCards(c);

                                comboCards.CheckIncompatibleCards();

                                comboCards.CheckSelectedCardNumbers();

                                if (comboCards.CurrentSelectedCards <= 1)
                                {
                                    comboCards.ResetCardNavigations();

                                    if (comboCards.CombinedCardAnimator.GetCurrentAnimatorClipInfo(0).Length > 0)
                                    {
                                        comboCards.CombinedCardAnimator.Play("Reverse");
                                        comboCards.CombinedCard.SelectedCardAnimator.Play("Idle");
                                        comboCards.CombinedCard.CardButton.interactable = false;

                                        comboCards.CombinedCard.HideStatusEffectPanel();
                                    }

                                    comboCards.ClearCardComboList();
                                    comboCards.CheckCombinations();

                                    DeselectTargets(true);

                                    ResetSelectedCardAnimation();
                                }
                                else
                                {
                                    comboCards.CheckCombinations();

                                    DeselectTargets(true);
                                    comboCards.CombinedCard.SelectedCardAnimator.Play("Idle");
                                    comboCards.CombinedCard.CardButton.interactable = false;
                                }

                                if (cardComboSelectedIndex <= 0)
                                {
                                    EnableCharityButton();

                                    comboCards.DisableCombineFrames();
                                    comboCards.ClearCardComboList();
                                }

                                c.SelectionObject.SetActive(false);
                            }
                        }
                        else
                        {
                            if (!c.IsAcombinedCard)
                                ShowBattleMessage("This card cannot be combined!");
                        }
                    }
                }
            }
        }
        else
        {
            if (Input.GetButtonDown("Ps4OpenChest"))
            {
                Card c = objectHoveredOver.GetComponent<Card>();
                currentHoveredObject = c.gameObject;

                if (c.CardButton.interactable && !usingCharity)
                {
                    if (c.CanCombineWithOtherCards && !c.IsAcombinedCard && !c.CannotBeUsed && !c._cardTemplate.combination.CannotBeUsedToCombine)
                    {
                        if (!c.ChosenForCombining)
                        {
                            DisableCharityButton();

                            if (cardComboSelectedIndex < 3)
                            {
                                AudioManager.instance.PlaySoundEffect(AudioManager.instance.CursorAudio);

                                cardComboSelectedIndex++;

                                c.ChosenForCombining = true;

                                c.SelectedIndexText.text = cardComboSelectedIndex.ToString();

                                comboCards._Card.Add(c);

                                comboCards.CurrentSelectedCards++;

                                comboCards.CheckCards(c);

                                c.SelectionObject.SetActive(true);

                                comboCards.CheckCombinations();

                                c.DefaultColorFrame();
                            }
                            else
                            {
                                ShowBattleMessage("You cannot combine anymore cards!");
                            }
                        }
                        else
                        {
                            AudioManager.instance.PlaySoundEffect(AudioManager.instance.CursorAudio);

                            cardComboSelectedIndex--;

                            c.ChosenForCombining = false;

                            c.PlayHoverAnimation();

                            c.SelectedIndexText.text = cardComboSelectedIndex.ToString();

                            comboCards.CurrentSelectedCards--;

                            comboCards._Card.Remove(c);

                            comboCards.CheckCards(c);

                            comboCards.CheckIncompatibleCards();

                            comboCards.CheckSelectedCardNumbers();

                            if (comboCards.CurrentSelectedCards <= 1)
                            {
                                comboCards.ResetCardNavigations();

                                if (comboCards.CombinedCardAnimator.GetCurrentAnimatorClipInfo(0).Length > 0)
                                {
                                    comboCards.CombinedCardAnimator.Play("Reverse");
                                    comboCards.CombinedCard.SelectedCardAnimator.Play("Idle");
                                    comboCards.CombinedCard.CardButton.interactable = false;

                                    comboCards.CombinedCard.HideStatusEffectPanel();
                                }

                                comboCards.ClearCardComboList();
                                comboCards.CheckCombinations();

                                DeselectTargets(true);

                                ResetSelectedCardAnimation();
                            }
                            else
                            {
                                comboCards.CheckCombinations();

                                DeselectTargets(true);
                                comboCards.CombinedCard.SelectedCardAnimator.Play("Idle");
                                comboCards.CombinedCard.CardButton.interactable = false;
                            }

                            if (cardComboSelectedIndex <= 0)
                            {
                                EnableCharityButton();

                                comboCards.DisableCombineFrames();
                                comboCards.ClearCardComboList();
                            }

                            c.SelectionObject.SetActive(false);
                        }
                    }
                    else
                    {
                        if (!c.IsAcombinedCard)
                            ShowBattleMessage("This card cannot be combined!");
                    }
                }
            }
        }
    }

    public void ResetSelectedCardAnimation()
    {
        foreach(Card c in battleDeck.CardsInHand)
        {
            c.SelectedCardAnimator.Play("Idle");
        }
    }

    public void ResetItemCardAnimation()
    {
        foreach(Card c in itemLayout.GetComponentsInChildren<Card>())
        {
            c.SelectedCardAnimator.Play("Idle");
            c.ControllerDeselectCard();
        }
    }

    public void ResetAttackCommandAnimation()
    {
        defaultAttackAnimator.Play("Normal");
    }

    public void ResetHandObjectsGraveAndDeck()
    {
        if(comboCards.CurrentSelectedCards >= 1)
        {
            ResetHandObjects();
        }
    }

    public void ResetHandObjects()
    {
        foreach (Card c in battleDeck.CardsInHand)
        {
            if(!_inputManager.ControllerPluggedIn)
            {
                if (c.SelectionObject.activeInHierarchy)
                {
                    c._Animator.Play("SlideDown");
                }
            }
            c.ChosenForCombining = false;
            c.SelectionObject.SetActive(false);

            if(c._cardTemplate.cardType != CardType.Mystic)
               c.CanCombineWithOtherCards = true;

            comboCards.CurrentSelectedCards = 0;

            if(comboCards.CombinedCardAnimator.GetCurrentAnimatorClipInfo(0).Length > 0)
            {
                comboCards.CombinedCardAnimator.Play("Reverse");
                comboCards.CombinedCard.SelectedCardAnimator.Play("Idle");
                comboCards.CombinedCard.CardButton.interactable = false;

                comboCards.CombinedCard.HideStatusEffectPanel();
                comboCards.CombinedCard.ResetChildLifeTime();
            }

            comboCards.DisableCombineFrames();
        }

        cardComboSelectedIndex = 0;

        comboCards._Card.Clear();
        comboCards.ClearCardComboList();
    }

    public void DisableBattleUIInteracteability()
    {
        battleUICanvasgroup.interactable = false;
        battleUICanvasgroup.blocksRaycasts = false;

        settingsButton.interactable = false;

        _InputManager.SetSelectedObject(null);
    }

    public void EnableBattleUIInteracteability()
    {
        if(battleUICanvasgroup != null)
        {
            if (battlePlayer.CurrentHealth > 0)
            {
                battleUICanvasgroup.interactable = true;
                battleUICanvasgroup.blocksRaycasts = true;

                settingsButton.interactable = true;
            }
        }
    }

    public void ContinuePlayerTurn()
    {
        EnableBattleUIInteracteability();
        eventSystem.SetSelectedGameObject(defaultAttackAnimator.gameObject);

        SetInitialBattleUINavigations();

        EnableCharityButton();

        if (cardTemplate != null)
        {
            cardTemplate = null;
        }

        canOnlyHoverOverEnemies = false;
        isSelectingTarget = false;
        confirmedTarget = false;
    }

    private void OpenStickerMessagePanel(string message)
    {
        stickerPowerMessagePanelAnimator.Play("Open");

        stickerPowerMessageText.text = message;

        AudioManager.instance.PlaySoundEffect(AudioManager.instance.OpenMenuAudio);
    }

    public void EnableCharityButton()
    {
        if(MenuController.instance._StickerPowerManager.CheckStartOfBattleStickerPower(StickerPower.Charity) && !usedCharity)
        {
            charityButton.interactable = true;
            charityButtonText.color = new Color(1, 1, 1, 1);

            charityButtonText.text = "Charity";

            Navigation attackButtonNav = defaultAttackAnimator.GetComponent<Selectable>().navigation;

            attackButtonNav.selectOnUp = charityButton;

            defaultAttackAnimator.GetComponent<Selectable>().navigation = attackButtonNav;
        }
    }

    public void DisableCharityButton()
    {
        if(charityButton.gameObject.activeSelf)
        {
            charityButton.interactable = false;
            charityButtonText.color = new Color(1, 1, 1, 0.7f);

            charityButtonText.text = "Charity";

            Navigation charityNav = charityButton.GetComponent<Selectable>().navigation;

            charityNav.selectOnRight = null;

            charityButton.GetComponent<Selectable>().navigation = charityNav;

            Navigation attackButtonNav = defaultAttackAnimator.GetComponent<Selectable>().navigation;

            SetDefaultAttackNavigation(attackButtonNav);
        }
    }

    private void ActivateCharityButton()
    {
        charityButton.onClick.RemoveAllListeners();
        charityButton.onClick.AddListener(CancelCharityActivation);

        charityButton.GetComponent<ButtonListeners>().AttachListener();

        charityPanelInfoText.text = "Choose two cards to discard.";

        charityBlockers.SetActive(true);

        usingCharity = true;

        charityPanelAnimator.Play("Open", -1, 0);

        charityButtonText.text = "Cancel";

        SetHandCardNavigationsForCharity();
    }

    private void SetHandCardNavigationsForCharity()
    {
        Navigation charityNav = charityButton.GetComponent<Selectable>().navigation;

        charityNav.selectOnDown = null;
        charityNav.selectOnRight = battleDeck.CardsInHand[0].GetComponent<Selectable>();

        charityButton.GetComponent<Selectable>().navigation = charityNav;

        for (int i = 0; i < battleDeck.CardsInHand.Count; i++)
        {
            Navigation cardNav = battleDeck.CardsInHand[i].GetComponent<Selectable>().navigation;

            cardNav.selectOnLeft = null;
            cardNav.selectOnRight = null;
            cardNav.selectOnUp = null;
            cardNav.selectOnDown = null;

            if(i == 0)
            {
                cardNav.selectOnLeft = charityButton;
                cardNav.selectOnRight = battleDeck.CardsInHand[i + 1].GetComponent<Selectable>();
            }
            else if(i >= battleDeck.CardsInHand.Count - 1)
            {
                cardNav.selectOnRight = battleDeck.CardsInHand[0].GetComponent<Selectable>();
                cardNav.selectOnLeft = battleDeck.CardsInHand[i - 1].GetComponent<Selectable>();
            }
            else
            {
                cardNav.selectOnRight = battleDeck.CardsInHand[i + 1].GetComponent<Selectable>();
                cardNav.selectOnLeft = battleDeck.CardsInHand[i - 1].GetComponent<Selectable>();
            }

            battleDeck.CardsInHand[i].GetComponent<Selectable>().navigation = cardNav;
        }

        _inputManager.SetSelectedObject(battleDeck.CardsInHand[0].gameObject);
    }

    private void CancelCharityActivation()
    {
        charityButton.onClick.RemoveAllListeners();
        charityButton.onClick.AddListener(ActivateCharityButton);

        charityButton.GetComponent<ButtonListeners>().AttachListener();

        usingCharity = false;

        charityBlockers.SetActive(false);

        charityPanelAnimator.Play("Reverse", -1, 0);

        if(charityCards.Count > 0)
        {
            foreach(Card card in charityCards)
            {
                card.CharityCheckMark.SetActive(false);
                card.ChosenForCharity = false;
            }
        }

        charityCards.Clear();

        charityButtonText.text = "Charity";

        SetInitialBattleUINavigations();

        Navigation charityNav = charityButton.GetComponent<Selectable>().navigation;

        charityNav.selectOnDown = defaultAttackAnimator.GetComponent<Selectable>();
        charityNav.selectOnRight = null;

        charityButton.GetComponent<Selectable>().navigation = charityNav;
    }

    public void CheckCardsChosenForCharity()
    {
        if(charityCards.Count >= 2)
        {
            foreach(Card card in charityCards)
            {
                card.CharityCheckMark.SetActive(false);
                card.DisableCardImage();
                card.DissolveCard();
                card.SelectedCardAnimator.Play("Idle");

                battleDeck.CurrentHandSize--;

                StartCoroutine(WaitToRemoveCardFromHand(card.CardObject, card));

                card.ChosenForCharity = false;
            }

            charityButton.onClick.RemoveAllListeners();

            charityButton.onClick.AddListener(ActivateCharityButton);

            charityButton.GetComponent<ButtonListeners>().AttachListener();

            usedCharity = true;

            DisableBattleUIInteracteability();

            charityCards.Clear();

            DisableCharityButton();

            charityBlockers.SetActive(false);

            charityPanelAnimator.Play("Reverse", -1, 0);
        }
    }

    public void SendCardToGrave()
    {
        if(comboCards._Card.Count > 0)
        {
            foreach (Card c in comboCards._Card)
            {
                c._Animator.Play("SlideDown");
                c.SelectedCardAnimator.Play("Idle");
                c.CanCombineWith = false;
                c.SelectionObject.SetActive(false);
                c.CardButton.onClick.RemoveAllListeners();
                c.CardButton.onClick.AddListener(c.SelectCard);
                c.ControllerDeselectCardDeckItem(false);

                if(!retainCards)
                {
                    if(usedCard != null)
                    {
                        if (c.ChosenForCombining && usedCard.IsAcombinedCard)
                        {
                            c.DisableCardImage();

                            c.DissolveCard();

                            battleDeck.CurrentHandSize--;

                            StartCoroutine(WaitToRemoveCardFromHand(c.CardObject, c));
                        }
                    }
                }
                c.ChosenForCombining = false;
            }

            if(SteamManager.Initialized)
            {
                int fusedCards_Novice = 0;
                int fusedCards_Expert = 0;
                int fusedCards_Master = 0;

                SteamUserStats.GetStat("ACH_FUSION_NOVICE", out fusedCards_Novice);
                fusedCards_Novice++;
                SteamUserStats.SetStat("ACH_FUSION_NOVICE", fusedCards_Novice);
                SteamUserStats.StoreStats();

                SteamUserStats.GetStat("ACH_FUSION_EXPERT", out fusedCards_Expert);
                fusedCards_Expert++;
                SteamUserStats.SetStat("ACH_FUSION_EXPERT", fusedCards_Expert);
                SteamUserStats.StoreStats();

                SteamUserStats.GetStat("ACH_FUSION_MASTER", out fusedCards_Master);
                fusedCards_Master++;
                SteamUserStats.SetStat("ACH_FUSION_MASTER", fusedCards_Master);
                SteamUserStats.StoreStats();

                SteamUserStats.GetAchievement("ACH_COMBO_NOVICE", out bool achievementCompleted);

                if (!achievementCompleted)
                {
                    if (fusedCards_Novice >= 50)
                    {
                        SteamUserStats.SetAchievement("ACH_COMBO_NOVICE");
                        SteamUserStats.StoreStats();
                    }
                }

                SteamUserStats.GetAchievement("ACH_COMBO_EXPERT", out bool achievementCompleted2);

                if (!achievementCompleted2)
                {
                    if (fusedCards_Expert >= 150)
                    {
                        SteamUserStats.SetAchievement("ACH_COMBO_EXPERT");
                        SteamUserStats.StoreStats();
                    }
                }

                SteamUserStats.GetAchievement("ACH_COMBO_MASTER", out bool achievementCompleted3);

                if (!achievementCompleted3)
                {
                    if (fusedCards_Master >= 300)
                    {
                        SteamUserStats.SetAchievement("ACH_COMBO_MASTER");
                        SteamUserStats.StoreStats();
                    }
                }
            }
        }

        if (lastSelectedObject.GetComponent<CardMovement>())
        {
            if(!lastSelectedObject.GetComponent<Card>().IsAcombinedCard)
            {
                if(!retainCards)
                {
                    lastSelectedObject.GetComponent<Card>().CardButton.onClick.RemoveAllListeners();
                    lastSelectedObject.GetComponent<Card>().CardButton.onClick.AddListener(lastSelectedObject.GetComponent<Card>().SelectCard);
                    lastSelectedObject.GetComponent<Card>().DisableCardImage();
                    lastSelectedObject.GetComponent<Card>().DissolveCard();
                    lastSelectedObject.GetComponent<Card>().ControllerDeselectCardDeckItem(false);
                    lastSelectedObject.GetComponent<Card>().SelectedCardAnimator.Play("Idle");

                    battleDeck.CurrentHandSize--;

                    StartCoroutine(WaitToRemoveCardFromHand(lastSelectedObject.GetComponent<Card>().CardObject, lastSelectedObject.GetComponent<Card>()));
                }
                else
                {
                    if(lastSelectedObject.GetComponent<Card>()._cardTemplate.cardType == CardType.Mystic)
                    {
                        lastSelectedObject.GetComponent<Card>().CardButton.onClick.RemoveAllListeners();
                        lastSelectedObject.GetComponent<Card>().CardButton.onClick.AddListener(lastSelectedObject.GetComponent<Card>().SelectCard);
                        lastSelectedObject.GetComponent<Card>().DisableCardImage();
                        lastSelectedObject.GetComponent<Card>().DissolveCard();
                        lastSelectedObject.GetComponent<Card>().ControllerDeselectCardDeckItem(false);
                        lastSelectedObject.GetComponent<Card>().SelectedCardAnimator.Play("Idle");
                        
                        battleDeck.CurrentHandSize--;

                        StartCoroutine(WaitToRemoveCardFromHand(lastSelectedObject.GetComponent<Card>().CardObject, lastSelectedObject.GetComponent<Card>()));
                    }
                }
            }
        }
    }

    public void ReshuffleHandBackToDeck()
    {
        battleDeck.ClearCards();
    }

    public void AttackTarget()
    {
        canHoverOverTargets = true;
        canOnlyHoverOverEnemies = true;

        hittingAllEnemies = false;

        if(!selectedNormalAttack)
        {
            defaultAttackAnimator.Play("Selected");
            selectedNormalAttack = true;
        }

        CheckLastSelectedObject();

        if(CheckControllerConnection())
        {
            _inputManager._EventSystem.SetSelectedGameObject(null);
            _inputManager.SetSelectedObject(enemies[0].gameObject);
        }
    }

    public void PointerEnterOnDefaultAttack()
    {
        if (!selectedNormalAttack)
        {
            defaultAttackAnimator.Play("Highlighted");
        }
        objectHoveredOver = defaultAttackAnimator.gameObject;
    }

    public void PointerExitOnDefaultAttack()
    {
        if (!selectedNormalAttack)
        {
            defaultAttackAnimator.Play("Pressed");
        }
        SetObjectHoveredOverToNull();
    }

    public void ControllerSelectOnDefaultAttack()
    {
        if(CheckControllerConnection())
        {
            if (!selectedNormalAttack)
            {
                defaultAttackAnimator.Play("Highlighted");
            }
            objectHoveredOver = defaultAttackAnimator.gameObject;
        }
    }

    public void ControllerDeselectOnDefaultAttack()
    {
        if(CheckControllerConnection())
        {
            if (!selectedNormalAttack)
            {
                defaultAttackAnimator.Play("Pressed");
            }
            SetObjectHoveredOverToNull();

            SetSelectedObjectToNull();
        }
    }

    public void DeselectDefaultAttack()
    {
        if (selectedNormalAttack)
        {
            PlayDefaultAttackButtonHover();
            selectedNormalAttack = false;

            _inputManager.SetSelectedObject(defaultAttackAnimator.gameObject);
        }
    }

    public void DeselectDefaultAttackController()
    {
        if (selectedNormalAttack)
        {
            PlayDefaultAttackButtonHover();
            selectedNormalAttack = false;
        }
    }

    public void PlayDefaultAttackButtonHover()
    {
        if(objectHoveredOver == defaultAttackAnimator.gameObject)
        {
            defaultAttackAnimator.Play("Highlighted");
        }
        else
        {
            defaultAttackAnimator.Play("Normal");
        }
    }

    public void SetLastSelectedObject(GameObject selected)
    {
        lastSelectedObject = selected;
    }

    public void SetSelectedObjectToNull()
    {
        if(!CheckControllerConnection())
            eventSystem.SetSelectedGameObject(null);
    }

    public void EnableTargetCircle(Vector3 pos)
    {
        DisableTargetCircleForAll();
        targetCircle.SetActive(true);

        targetCircle.transform.position = pos;
    }

    public void EnableTargetCircleForAll()
    {
        DisableTargetCircle();
    }

    public void DisableTargetCircleForAll()
    {
        foreach(GameObject circles in targetCircles)
        {
            circles.SetActive(false);
        }
    }

    public void DisableTargetCircle()
    {
        targetCircle.SetActive(false);
    }

    public void SetUsedCard(Card card)
    {
        if(battlePlayer.CurrentCardPoints >= card._cardTemplate.cardPointCost)
        {
            if (!IsSelectingTarget && !usingMysticCard)
            {
                usedCard = card;
            }
        } 
    }

    public void RemoveCardTemplate()
    {
        cardTemplate = null;
    }

    public void RemoveUsedCard()
    {
        usedCard = null;
    }

    public void SetObjectHoveredOver(Card card)
    {
        objectHoveredOver = card.gameObject;
    }

    public void SetObjectHoveredOverToNull()
    {
        objectHoveredOver = null;
    }

    public void FadeAllEnemyHealthBars()
    {
        for(int i = 0; i < enemies.Count; i++)
        {
            enemies[i].FadeHealth();
        }
    }

    public void ReverseAllEnemyHealthBars()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].ReverseHealthFade();
        }
    }

    public void EnemyTurn()
    {
        PlayersTurn = false;
        usedCharity = false;
        retainCards = false;

        RemoveUsedCard();

        DisableBattleUIInteracteability();

        battlePlayer.DisableStatusEffectRaycast();

        if(enemies.Count > 0)
        {
            if (enemyIndex >= enemies.Count)
            {
                turnWasSkipped = false;

                CheckForPlayerStatus();

                enemyIndex = 0;
            }
            else if(hasStoppedEnemies)
            {
                StartCoroutine(WaitToStartTurn());
            }
            else
            {
                hasDoubleDamage = false;

                enemies[enemyIndex].TakingTurn = true;

                enemies[enemyIndex].CheckedSkippedTurn = false;

                if (enemies.Count >= 1)
                {
                    if(!enemies[enemyIndex].Damaged)
                        enemies[enemyIndex].ResetDamaged();
                }
            }
        }
    }

    private IEnumerator WaitToStartTurn()
    {
        yield return new WaitForSeconds(1f);

        turnWasSkipped = false;

        CheckForPlayerStatus();

        enemyIndex = 0;
    }

    public void EnemyCheckStatus()
    {
        if(enemies.Count > 0)
        {
            if(enemies[enemyIndex].TakingTurn)
            {
                if (enemies[enemyIndex].StatusEffectHolder.transform.childCount > 0)
                {
                    enemies[enemyIndex].CheckForStatusEffectsBeforeTurn();
                }
            }
        }
    }

    public void BeginEnemyActionCoroutine()
    {
        StartCoroutine("WaitForEnemyAction");
    }

    public IEnumerator WaitForEnemyAction()
    {
        yield return new WaitForSeconds(0.3f);
        if(enemies.Count > 0)
        {
            if (enemies[enemyIndex].StatusEffectHolder.transform.childCount <= 0)
                enemies[enemyIndex].ChooseAction();
        }
    }

    public void ResetEnemyStatusIndex()
    {
        if(enemies.Count > 0)
           enemies[enemyIndex].StatusEffectIndex = 0;
    }

    public void CheckEnemyStatus()
    {
        if(enemies[enemyIndex].CurrentHealth > 0)
        {
            if (enemies[enemyIndex].TakingTurn)
            {
                enemies[enemyIndex].CheckForStatusEffectsBeforeTurn();
            }
        }
    }

    public void CheckIfBattleIsWon()
    {
        if(enemies.Count <= 0 && !checkedWin)
        {
            DisableBattleUIInteracteability();

            battlePlayer.TurnOffPlayerGravity();

            if(battlePlayer.CurrentHealth <= battlePlayer.MaxHealth / 4)
            {
                battlePlayer.IsNearDeath = true;
            }

            foreach(StatusEffects status in battlePlayer.StatusEffectHolder.GetComponentsInChildren<StatusEffects>())
            {
                status.RemoveAllEffectsPostBattle();
            }

            if(!GameManager.instance.IsFinalBossFight)
            {
                if (turnCount <= earlyTurnCountBonus)
                {
                    battleResults.QuickVictoryBonusPanel.SetActive(true);
                }

                if (battlePlayer.IsNearDeath)
                {
                    battleResults.NearDeathEExperienceBonusPanel.SetActive(true);
                }

                if (!didPlayerTakeDamage)
                {
                    battleResults.TookNoDamageBonusPanel.SetActive(true);
                }
            }

            StartCoroutine(MovePlayerToVictoryPosition());

            mainCameraPostProcessLayer.enabled = false;
            uiCameraPostProcessLayer.enabled = true;

            if(GameManager.instance.EnemyObject != null)
               GameManager.instance.EnemyObject.IsDead = true;

            battlePlayer.HideStatsBar();

            checkedWin = true;
        }
    }

    public void CheckForPlayerStatus()
    {
        if(!firstTurn || GameManager.instance.IsAnAmbush)
        {
            battlePlayer.StatusEffectIndex = 0;

            battlePlayer.CheckForStatusEffectsBeforeTurn();
        }
        else
        {
            PlayerTurn();
        }
    }

    public void DealDamageToEachEnemy(float damage)
    {
        foreach(BattleEnemy enemy in enemies)
        {
            int percentage = Mathf.RoundToInt(enemy._EnemyStats.health * damage);

            enemy.TakeDamage(percentage, true, true, false);

            var damageParticle = Instantiate(battlePlayer.BasicAttackParticle, new Vector3(enemy.transform.position.x, enemy.transform.position.y + enemy._EnemyStats.hitAnimationOffsetY, 
                                                                                           enemy.transform.position.z), Quaternion.identity);

            damageParticle.gameObject.SetActive(true);
            damageParticle.gameObject.AddComponent(typeof(DestroyParticle));
            damageParticle.gameObject.GetComponent<DestroyParticle>().SetDestroyTime(2f);
        }
    }

    public void PlayerTurn()
    {
        if(turnWasSkipped || battlePlayer.ParalysisParticle.gameObject.activeSelf)
        {
            return;
        }

        turnCount++;

        usingCharity = false;
        hasDoubleCast = false;
        hasFreeCast = false;
        hasStoppedEnemies = false;
        battlePlayer.HasLuckyGuard = false;

        foreach(BattleEnemy e in enemies)
        {
            if(e._Animator.speed < 1)
            {
                e.SetAnimatorSpeed(1);
            }
        }

        CheckForHelpingHandSticker();

        int enemyDeathCount = 0;

        foreach (BattleEnemy e in enemies)
        {
            if (e.CurrentHealth <= 0)
            {
                enemyDeathCount++;
            }
        }

        if(enemyDeathCount >= enemies.Count)
        {
            return;
        }

        if (MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.MysticFavored))
        {
            stickerPowerHolder.CreateStickerMessage(StickerPower.MysticFavored);
            hasMysticFavoredPower = true;
        }

        battlePlayer.SetPerfectGuardToFalse();

        battleDeck.CardsToDrawForCharity = 0;

        enemyTarget = null;

        comboCards.ResetCards();

        if (cardTemplate != null)
        {
            cardTemplate = null;
        }

        EnableBattleUIInteracteability();

        if(gameObject != null)
        {
            if (charityButton.gameObject.activeSelf && !usedCharity)
                EnableCharityButton();
        }

        battlePlayer.EnableStatusEffectRaycast();

        if (!firstTurn || GameManager.instance.IsAnAmbush)
        {
            ReverseAllEnemyHealthBars();

            GameManager.instance.IsAnAmbush = false;
        }

        battlePlayer.DisableStunParticle();

        if(battleDeck.CurrentHandSize < battleDeck.MaxHandSize)
        {
            if(!battleDeck.DrawingCards)
            {
                battleDeck.Draw(0);
                battleDeck.DrawingCards = true;
            }
        }
        else
        {
            battleDeck.DrawingCards = false;

            if(firstTurn)
            {
                if (MenuController.instance._StickerPowerManager.CheckStartOfBattleStickerPower(StickerPower.Mulligan))
                {
                    OpenStickerMessagePanel("Do you want to mulligan?");

                    stickerPowerHolder.CreateStickerMessage(StickerPower.Mulligan);

                    DisableBattleUIInteracteability();

                    if (CheckControllerConnection())
                    {
                        _inputManager.FirstSelectedObject = mulliganMenuConfirm.gameObject;
                        _inputManager.SetSelectedObject(mulliganMenuConfirm);
                    }
                }
                else if(!MenuController.instance._StickerPowerManager.CheckStartOfBattleStickerPower(StickerPower.Mulligan))
                {
                    if (!isTutorial)
                    {
                        _inputManager.FirstSelectedObject = defaultAttackAnimator.gameObject;
                        _inputManager.SetSelectedObject(defaultAttackAnimator.gameObject);
                    }
                }

                eventSystem.sendNavigationEvents = true;

                navagationDisabled = false;

                if(GameManager.instance.IsAPreemptiveStrike)
                {
                    GameManager.instance.IsAPreemptiveStrike = false;
                }

                SetInitialBattleUINavigations();

                statusEffectManager.AdjustStatusEffectNavigationsForPlayer();
                statusEffectManager.AdjustStatusEffectNavigationsForEnemies();

                CheckCharityButtonNavigation();
            }
            else
            {
                eventSystem.sendNavigationEvents = true;

                navagationDisabled = false;

                SetInitialBattleUINavigations();

                _inputManager.SetSelectedObject(defaultAttackAnimator.gameObject);

                statusEffectManager.AdjustStatusEffectNavigationsForPlayer();
                statusEffectManager.AdjustStatusEffectNavigationsForEnemies();

                CheckCharityButtonNavigation();
            }
        }

        playersTurn = true;

        battlePlayer.ResetPlayerActions();

        if (battlePlayer.IsStunned)
        {
            battlePlayer.IsStunned = false;
        }

        targetIndex = 0;

        canOnlyHoverOverEnemies = false;
        isSelectingTarget = false;
        confirmedTarget = false;

        firstTurn = false;
    }

    private void CheckForHelpingHandSticker()
    {
        if(MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.HelpingHand))
        {
            int rand = Random.Range(0, 100);

            if(rand <= 20)
            {
                int positiveRand = Random.Range(0, 5);

                switch(positiveRand)
                {
                    case 0:
                        if(!CheckIfStatusEffectExists(StatusEffect.StrengthUp))
                        {
                            stickerPowerHolder.CreateStickerMessage(StickerPower.HelpingHand);
                            statusEffectManager.CreateStatusEffect(StatusEffect.StrengthUp, "Strength Up", 3, 30, false, statusEffectManager.StrengthUp, true);
                        } 
                        break;
                    case 1:
                        if(!CheckIfStatusEffectExists(StatusEffect.Thorns))
                        {
                            stickerPowerHolder.CreateStickerMessage(StickerPower.HelpingHand);
                            statusEffectManager.CreateStatusEffect(StatusEffect.Thorns, "Thorns", 5, 0, true, statusEffectManager.Thorns, true);
                        }   
                        break;
                    case 2:
                        if (!CheckIfStatusEffectExists(StatusEffect.DefenseUp))
                        {
                            stickerPowerHolder.CreateStickerMessage(StickerPower.HelpingHand);
                            statusEffectManager.CreateStatusEffect(StatusEffect.DefenseUp, "Defense Up", 3, 30, false, statusEffectManager.DefenseUp, true);
                        }
                        break;
                    case 3:
                        stickerPowerHolder.CreateStickerMessage(StickerPower.HelpingHand);
                        DealDamageToEachEnemy(0.10f);
                        break;
                    case 4:
                        if(battlePlayer.CurrentHealth > 0)
                        {
                            stickerPowerHolder.CreateStickerMessage(StickerPower.HelpingHand);

                            if(battlePlayer.IsUndead)
                            {
                                battlePlayer.TakeDamage(30, true, false);
                            }
                            else
                            {
                                battlePlayer.HealHealth(30, true);
                            }
                        }
                        break;
                    case 5:
                        stickerPowerHolder.CreateStickerMessage(StickerPower.HelpingHand);
                        battlePlayer.HealCardPoints(20, true);
                        break;
                }
            }
        }
    }

    public void AdjustNavigationsForPlayerAndEnemyStatusEffects()
    {
        statusEffectManager.AdjustStatusEffectNavigationsForEnemies();
        statusEffectManager.AdjustStatusEffectNavigationsForPlayer();
    }

    public void SetGraveyardButtonNavigations()
    {
        Graveyard grave = graveyardPosition.GetComponent<Graveyard>();

        Navigation exitNav = graveExitObject.GetComponent<Selectable>().navigation;

        if(grave.cards.Count > 0)
        {
            exitNav.selectOnDown = grave.cards[0].GetComponent<Selectable>();

            _inputManager.SetSelectedObject(grave.cards[0].gameObject);

            for(int i = 0; i < grave.cards.Count; i++)
            {
                Navigation graveNav = grave.cards[i].GetComponent<Selectable>().navigation;

                graveNav.selectOnRight = null;
                graveNav.selectOnLeft = null;

                if(i == 0)
                {
                    if(grave.cards.Count > 1)
                    {
                        graveNav.selectOnRight = grave.cards[i + 1].GetComponent<Selectable>();
                    }
                }
                else if(i >= grave.cards.Count - 1)
                {
                    graveNav.selectOnRight = grave.cards[0].GetComponent<Selectable>();
                    graveNav.selectOnLeft = grave.cards[i - 1].GetComponent<Selectable>();
                }
                else
                {
                    graveNav.selectOnRight = grave.cards[i + 1].GetComponent<Selectable>();
                    graveNav.selectOnLeft = grave.cards[i - 1].GetComponent<Selectable>();
                }

                int downWardCardIndex = i + 5;

                if (i <= 4)
                {
                    graveNav.selectOnUp = graveExitObject.GetComponent<Selectable>();
                }

                if(grave.cards.Count > 4)
                {
                    graveNav.selectOnDown = grave.cards[downWardCardIndex >= grave.cards.Count - 1 ? grave.cards.Count - 1 : downWardCardIndex].GetComponent<Selectable>();
                }

                if(i > 4)
                {
                    int upWardCardIndex = i - 5;
                    graveNav.selectOnUp = grave.cards[upWardCardIndex].GetComponent<Selectable>();
                }

                grave.cards[i].GetComponent<Selectable>().navigation = graveNav;
            }
        }
        else
        {
            exitNav.selectOnDown = null;

            _inputManager.SetSelectedObject(graveExitObject);
        }

        graveExitObject.GetComponent<Selectable>().navigation = exitNav;
    }

    public void SetDeckButtonNavigations()
    {
        Navigation exitNav = deckExitObject.GetComponent<Selectable>().navigation;

        if (battleDeck._Card.Count > 0)
        {
            exitNav.selectOnDown = battleDeck.DeckParent.GetChild(0).GetComponent<Selectable>();

            _inputManager.SetSelectedObject(battleDeck.DeckParent.GetChild(0).gameObject);

            for (int i = 0; i < battleDeck.DeckParent.childCount; i++)
            {
                Navigation deckNav = battleDeck.DeckParent.GetChild(i).GetComponent<Selectable>().navigation;

                deckNav.selectOnRight = null;
                deckNav.selectOnLeft = null;

                if (i == 0)
                {
                    if (battleDeck._Card.Count > 1)
                    {
                        deckNav.selectOnRight = battleDeck.DeckParent.GetChild(i + 1).GetComponent<Selectable>();
                        deckNav.selectOnLeft = battleDeck.DeckParent.GetChild(battleDeck.DeckParent.childCount - 1).GetComponent<Selectable>();
                    }
                }
                else if (i >= battleDeck.DeckParent.childCount - 1)
                {
                    deckNav.selectOnRight = battleDeck.DeckParent.GetChild(0).GetComponent<Selectable>();
                    deckNav.selectOnLeft = battleDeck.DeckParent.GetChild(i - 1).GetComponent<Selectable>();
                }
                else
                {
                    deckNav.selectOnRight = battleDeck.DeckParent.GetChild(i + 1).GetComponent<Selectable>();
                    deckNav.selectOnLeft = battleDeck.DeckParent.GetChild(i - 1).GetComponent<Selectable>();
                }

                int downWardCardIndex = i + 5;

                if (i <= 4)
                {
                    deckNav.selectOnUp = deckExitObject.GetComponent<Selectable>();
                }

                if (battleDeck.DeckParent.childCount > 4)
                {
                    deckNav.selectOnDown = battleDeck.DeckParent.GetChild(downWardCardIndex >= battleDeck._Card.Count - 1 ? battleDeck._Card.Count - 1 : downWardCardIndex).GetComponent<Selectable>();
                }

                if (i > 4)
                {
                    int upWardCardIndex = i - 5;
                    deckNav.selectOnUp = battleDeck.DeckParent.GetChild(upWardCardIndex).GetComponent<Selectable>();
                }

                battleDeck.DeckParent.GetChild(i).GetComponent<Selectable>().navigation = deckNav;
            }
        }
        else
        {
            exitNav.selectOnDown = null;

            _inputManager.SetSelectedObject(deckExitObject);
        }

        deckExitObject.GetComponent<Selectable>().navigation = exitNav;
    }

    public void SetInitialBattleUINavigations()
    {
        Graveyard grave = graveyardPosition.GetComponent<Graveyard>();

        Navigation defaultAttackNav = defaultAttackAnimator.GetComponent<Selectable>().navigation;
        Navigation fleeNavigation = fleeButton.GetComponent<Selectable>().navigation;
        Navigation graveNav = grave.GetComponent<Selectable>().navigation;

        if(battleDeck.CardsInHand.Count > 1)
        {
            graveNav.selectOnLeft = battleDeck.CardsInHand[battleDeck.CardsInHand.Count - 1].GetComponent<Selectable>();
        }
        else if(battleDeck.CardsInHand.Count == 1)
        {
            graveNav.selectOnLeft = battleDeck.CardsInHand[0].GetComponent<Selectable>();
        }
        else
        {
            graveNav.selectOnLeft = defaultAttackAnimator.GetComponent<Selectable>();
        }

        if(itemCards.Count > 0)
        {
            fleeNavigation.selectOnRight = itemCards[0].GetComponent<Selectable>();

            for (int i = 0; i < itemCards.Count; i++)
            {
                itemCardsNavigation = itemCards[i].GetComponent<Selectable>().navigation;

                if(i == 0)
                {
                    if(!GameManager.instance.IsBossFight && !MenuController.instance._StickerPowerManager.CheckStartOfBattleStickerPower(StickerPower.Enrage))
                       itemCardsNavigation.selectOnLeft = fleeButton.GetComponent<Selectable>();

                    if (itemCards.Count == 1)
                    {
                        itemCardsNavigation.selectOnRight = defaultAttackAnimator.GetComponent<Selectable>();

                        defaultAttackNav.selectOnLeft = itemCards[0].GetComponent<Selectable>();
                    }
                    else
                    {
                        itemCardsNavigation.selectOnRight = itemCards[1].GetComponent<Selectable>();
                    }
                }
                else if(i >= itemCards.Count - 1)
                {
                    if(itemCards.Count == 1)
                    {
                        itemCardsNavigation.selectOnRight = defaultAttackAnimator.GetComponent<Selectable>();
                        itemCardsNavigation.selectOnLeft = fleeButton.GetComponent<Selectable>();

                        defaultAttackNav.selectOnLeft = itemCards[0].GetComponent<Selectable>();
                    }
                    else
                    {
                        itemCardsNavigation.selectOnRight = defaultAttackAnimator.GetComponent<Selectable>();
                        itemCardsNavigation.selectOnLeft = itemCards[i - 1].GetComponent<Selectable>();

                        defaultAttackNav.selectOnLeft = itemCards[itemCards.Count - 1].GetComponent<Selectable>();
                    }
                }
                else
                {
                    itemCardsNavigation.selectOnRight = itemCards[i + 1].GetComponent<Selectable>();
                    itemCardsNavigation.selectOnLeft = itemCards[i - 1].GetComponent<Selectable>();
                }

                itemCards[i].GetComponent<Selectable>().navigation = itemCardsNavigation;
            }
        }
        else
        {
            fleeNavigation.selectOnRight = defaultAttackAnimator.GetComponent<Selectable>();
        }

        for(int i = 0; i < battleDeck.CardsInHand.Count; i++)
        {
            if(battleDeck.CardsInHand.Count > 0)
            {
                Navigation nav = battleDeck.CardsInHand[i].GetComponent<Selectable>().navigation;

                nav.selectOnUp = null;
                nav.selectOnDown = null;

                if (battleDeck.CardsInHand.Count == 1)
                {
                    nav.selectOnLeft = defaultAttackAnimator.GetComponent<Selectable>();
                    nav.selectOnRight = graveyardPosition.GetComponent<Selectable>();
                }
                else
                {
                    if (i == 0)
                    {
                        nav.selectOnLeft = defaultAttackAnimator.GetComponent<Selectable>();
                        nav.selectOnRight = battleDeck.CardsInHand[i + 1].GetComponent<Selectable>();
                    }
                    else if (i >= battleDeck.CardsInHand.Count - 1)
                    {
                        nav.selectOnLeft = battleDeck.CardsInHand[i - 1].GetComponent<Selectable>();
                        nav.selectOnRight = graveyardPosition.GetComponent<Selectable>();
                    }
                    else
                    {
                        nav.selectOnLeft = battleDeck.CardsInHand[i - 1].GetComponent<Selectable>();
                        nav.selectOnRight = battleDeck.CardsInHand[i + 1].GetComponent<Selectable>();
                    }
                }

                battleDeck.CardsInHand[i].GetComponent<Selectable>().navigation = nav;
            }
        }

        if(battleDeck.CardsInHand.Count > 0)
        {
            defaultAttackNav.selectOnRight = battleDeck.CardsInHand[0].GetComponent<Selectable>();
        }
        else
        {
            defaultAttackNav.selectOnRight = grave.GetComponent<Selectable>();
        }

        defaultAttackAnimator.GetComponent<Selectable>().navigation = defaultAttackNav;
        fleeButton.GetComponent<Selectable>().navigation = fleeNavigation;
        grave.GetComponent<Selectable>().navigation = graveNav;
    }

    private bool CheckControllerConnection()
    {
        return InputManager.instance.ControllerPluggedIn;
    }

    private void Grave(Card crd)
    {
        Graveyard grave = graveyardPosition.GetComponent<Graveyard>();

        grave.cards.Add(crd);

        grave.UpdateGraveyardCardCount();     

        crd.transform.SetParent(grave.GraveyardHolder, false);

        crd.CheckIfCardIsFoil();

        crd._PorpogateDrag.scrollView = GraveScroll;

        crd.CardButton.interactable = false;

        crd.gameObject.SetActive(true);

        crd._Animator.Play("Idle");

        crd._Animator.enabled = false;

        crd._Animator.runtimeAnimatorController = null;

        crd.RegenerateCard();

        if(usingCharity)
        {
            if(!battleDeck.DrawingCards)
            {
                battleDeck.DrawingCards = true;
                battleDeck.Draw(2);
            }  
        }

        if(cardToAddForMysticEffect != null)
        {
            if(cardTemplate.cardEffect == CardEffect.ReturnCard)
            {
                battleDeck.SetCardToTopOfTheDeck(cardToAddForMysticEffect);

                isGraveOpen = false;
            }
            if(cardTemplate.cardEffect == CardEffect.AddCard)
            {
                battleDeck.AddCardFromDeckToHand(cardToAddForMysticEffect);

                isDeckOpen = false;
            }

            cardToAddForMysticEffect = null;
        }
    }

    private IEnumerator WaitToRemoveCardFromHand(GameObject cardObj, Card crd)
    {
        yield return new WaitUntil(() => cardObj.GetComponent<Coffee.UIEffects.UIDissolve>().effectFactor >= 0.9f);

        crd.CardButton.interactable = false;

        battleDeck.RemoveCardFromHand(crd);
        lastSelectedObject = defaultSelectedObject;

        if(playersTurn)
        {
            SetInitialBattleUINavigations();
            _InputManager._EventSystem.sendNavigationEvents = true;
            navagationDisabled = false;
        }

        crd.gameObject.SetActive(false);

        Grave(crd);
    }

    private IEnumerator WaitToStartBattle()
    {
        yield return new WaitUntil(() => ScreenFade.instance._ScreenTransitionController.GetComponent<Coffee.UIEffects.UITransitionEffect>().effectFactor <= 0);

        if (MenuController.instance._StickerPowerManager.CheckStartOfBattleStickerPower(StickerPower.SecondChance))
        {
            stickerPowerHolder.CreateStickerMessage(StickerPower.SecondChance);

            statusEffectManager.CreateStatusEffect(StatusEffect.Revive, "Resurrect", -1, 0, false, statusEffectManager.ResurrectSprite, false);
        }
        if (MenuController.instance._StickerPowerManager.CheckStartOfBattleStickerPower(StickerPower.Impervious))
        {
            stickerPowerHolder.CreateStickerMessage(StickerPower.Impervious);

            statusEffectManager.CreateStatusEffect(StatusEffect.Impervious, "Impervious", -1, 0, false, statusEffectManager.ImperviousSprite, false);
        }
        if (MenuController.instance._StickerPowerManager.CheckStartOfBattleStickerPower(StickerPower.BurnsImmune))
        {
            stickerPowerHolder.CreateStickerMessage(StickerPower.BurnsImmune);

            statusEffectManager.CreateStatusEffect(StatusEffect.BurnsImmune, "Sunscreen", 3, 0, false, statusEffectManager.BurnsImmuneSprite, false);
        }
        if(MenuController.instance._StickerPowerManager.CheckStartOfBattleStickerPower(StickerPower.PoisionImmune))
        {
            stickerPowerHolder.CreateStickerMessage(StickerPower.PoisionImmune);

            statusEffectManager.CreateStatusEffect(StatusEffect.PoisonImmune, "Anti-Venom", 3, 0, false, statusEffectManager.PoisionImmuneSprite, false);
        }
        if (MenuController.instance._StickerPowerManager.CheckStartOfBattleStickerPower(StickerPower.ParalysisImmune))
        {
            stickerPowerHolder.CreateStickerMessage(StickerPower.ParalysisImmune);

            statusEffectManager.CreateStatusEffect(StatusEffect.ParalysisImmune, "Lightning Rod", 3, 0, false, statusEffectManager.ParalysisImmuneSprite, false);
        }
        if (MenuController.instance._StickerPowerManager.CheckStartOfBattleStickerPower(StickerPower.Purity))
        {
            stickerPowerHolder.CreateStickerMessage(StickerPower.Purity);

            statusEffectManager.CreateStatusEffect(StatusEffect.UndeadImmune, "Purity", 3, 0, false, statusEffectManager.UndeadImmuneSprite, false);
        }
        if (MenuController.instance._StickerPowerManager.CheckStartOfBattleStickerPower(StickerPower.HpHealing))
        {
            stickerPowerHolder.CreateStickerMessage(StickerPower.HpHealing);

            statusEffectManager.CreateStatusEffect(StatusEffect.HpRegen, "HP Regen", 3, 0, true, statusEffectManager.HpRegen, false);
        }
        if (MenuController.instance._StickerPowerManager.CheckStartOfBattleStickerPower(StickerPower.CpHealing))
        {
            stickerPowerHolder.CreateStickerMessage(StickerPower.CpHealing);

            statusEffectManager.CreateStatusEffect(StatusEffect.CpRegen, "CP Regen", 3, 0, true, statusEffectManager.CpRegen, false);
        }
        if (MenuController.instance._StickerPowerManager.CheckStartOfBattleStickerPower(StickerPower.Enrage))
        {
            stickerPowerHolder.CreateStickerMessage(StickerPower.Enrage);

            statusEffectManager.CreateStatusEffect(StatusEffect.Enrage, "Enrage", -1, 0, false, statusEffectManager.Enrage, false);

            battlePlayer.PlayerStrength = battlePlayer._MainCharacterStats.strength + battlePlayer.StrengthBoost;
        }
        if (MenuController.instance._StickerPowerManager.CheckStartOfBattleStickerPower(StickerPower.Thorns))
        {
            stickerPowerHolder.CreateStickerMessage(StickerPower.Thorns);

            statusEffectManager.CreateStatusEffect(StatusEffect.Thorns, "Thorns", 5, 0, true, statusEffectManager.Thorns, false);
        }
        if (MenuController.instance._StickerPowerManager.CheckStartOfBattleStickerPower(StickerPower.Charity))
        {
            stickerPowerHolder.CreateStickerMessage(StickerPower.Charity);

            charityButton.onClick.AddListener(ActivateCharityButton);
        }
        if (MenuController.instance._StickerPowerManager.CheckStartOfBattleStickerPower(StickerPower.Maximalist))
        {
            if(MenuController.instance.DeckListContent.childCount >= playerMenuInfo.currentDeckLimit)
            {
                stickerPowerHolder.CreateStickerMessage(StickerPower.Maximalist);

                statusEffectManager.CreateStatusEffect(StatusEffect.StrengthUp, "Strength Up", 3, 30, false, statusEffectManager.StrengthUp, false);
            }
        }
        if (MenuController.instance._StickerPowerManager.CheckStartOfBattleStickerPower(StickerPower.Minimalist))
        {
            if(MenuController.instance.DeckListContent.childCount <= playerMenuInfo.minimumDeckSize)
            {
                stickerPowerHolder.CreateStickerMessage(StickerPower.Minimalist);

                statusEffectManager.CreateStatusEffect(StatusEffect.DefenseUp, "Defense Up", 3, 30, false, statusEffectManager.DefenseUp, false);
            } 
        }
        if (MenuController.instance._StickerPowerManager.CheckStartOfBattleStickerPower(StickerPower.ShadowForm))
        {
            stickerPowerHolder.CreateStickerMessage(StickerPower.ShadowForm);

            statusEffectManager.CreateStatusEffect(StatusEffect.ShadowForm, "Shadow Form", -1, 0, false, statusEffectManager.ShadowFormSprite, false);
        }

        LookForEnemyStartingEffects();

        if (isTutorial)
        {
            DisableBattleUIInteracteability();
        }
        else
        {
            if(!ShouldEnemiesBeKilled())
            {
                if (GameManager.instance.IsAPreemptiveStrike)
                {
                    hittingAllEnemies = true;

                    battlePlayer._BattleActions = BattleActions.rangedAttack;

                    battlePlayer.RangedAttack();
                }
                else if (GameManager.instance.IsAnAmbush)
                {
                    EnemyTurn();
                    enemies[enemyIndex].ChooseAction();
                }
                else
                {
                    battleDeck.Draw(0);
                }
            }
        }

        battlePlayer.CheckCourageUnderFireSticker();
    }

    public void CreateRandomStatusEffect()
    {
        int Rand = Random.Range(0, 3);

        switch(Rand)
        {
            case 0:
                if(!CheckIfStatusEffectExists(StatusEffect.StrengthUp))
                    statusEffectManager.CreateStatusEffect(StatusEffect.StrengthUp, "Strength Up", 3, 30, false, statusEffectManager.StrengthUp, true);
                break;
            case 1:
                if(!CheckIfStatusEffectExists(StatusEffect.DefenseUp))
                    statusEffectManager.CreateStatusEffect(StatusEffect.DefenseUp, "Defense Up", 3, 30, false, statusEffectManager.DefenseUp, true);
                break;
            case 2:
                if(!CheckIfStatusEffectExists(StatusEffect.HpRegen))
                    statusEffectManager.CreateStatusEffect(StatusEffect.HpRegen, "HP Regen", 4, 0, true, statusEffectManager.HpRegen, true);
                break;
            case 3:
                if(!CheckIfStatusEffectExists(StatusEffect.CpRegen))
                    statusEffectManager.CreateStatusEffect(StatusEffect.CpRegen, "CP Regen", 4, 0, true, statusEffectManager.CpRegen, true);
                break;
        }
    }

    public bool CheckIfStatusEffectExists(StatusEffect effect)
    {
        bool hasSameStatus = false;

        if (battlePlayer.StatusEffectHolder.transform.childCount > 0)
        {
            for (int i = 0; i < battlePlayer.StatusEffectHolder.transform.childCount; i++)
            {
                StatusEffects statusEffects = battlePlayer.StatusEffectHolder.transform.GetChild(i).GetComponent<StatusEffects>();
                if (statusEffects._statusEffect == effect)
                {
                    hasSameStatus = true;
                    break;
                }
            }
        }

        return hasSameStatus;
    }

    private bool ShouldEnemiesBeKilled()
    {
        bool shouldkill = false;

        if(MenuController.instance._StickerPowerManager.CheckStartOfBattleStickerPower(StickerPower.CullTheWeak))
        {
            foreach (BattleEnemy enemy in enemies)
            {
                if (enemy._EnemyStats.level <= 10)
                {
                    enemy.Dead();

                    shouldkill = true;
                }
            }
        }

        return shouldkill;
    }

    private IEnumerator MovePlayerToVictoryPosition()
    {
        AudioManager.instance.PlayVictoryBGM();

        battlePlayer._Animator.Play("JumpVictory");

        yield return new WaitForSeconds(0.4f);

        battlePlayer.PlayJumpParticle();

        battleUIAnimator.Play("BattleUI");

        settingsButtonAnimator.Play("SettingsButton");

        battleResults.SetGainedExpAndMoney();

        float t = 0;

        while (t < 1f)
        {
            t += Time.deltaTime;

            Vector3 distance = victoryPosition.position - battlePlayer.transform.position;

            battlePlayer.transform.position += distance * 4.5f * Time.deltaTime;

            battlePlayer.transform.rotation = Quaternion.Slerp(battlePlayer.transform.rotation, victoryPosition.rotation, 6 * Time.deltaTime).normalized;

            yield return null;
        }
        victoryScreen.SetActive(true);

        battleResults.SetUpExperienceBar();
        battleResults.SetUpMoney();

        victoryScreenAnimator.Play("Backdrop");

        battleResultsPanelAnimator.Play("ResultsPanel");
        battleResults.MoneyPanelAnimator.Play("MoneyPanel");

        if(SpoilsManager.instance.BattleSpoils.Count > 0)
        {
            battleResults.ItemSpoilsPanelAnimator.Play("SpoilsPanel");

            SpoilsManager.instance.ReceiveItems();
        } 

        if(MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.ExtraExp))
        {
            stickerPowerHolder.CreateStickerMessage(StickerPower.ExtraExp);

            battleResults.ExtraExperienceBanner.gameObject.SetActive(true);
        }
        if (MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.ExtraGold))
        {
            stickerPowerHolder.CreateStickerMessage(StickerPower.ExtraGold);

            battleResults.ExtraGoldBanner.gameObject.SetActive(true);
        }

        battleResults.CanSkipResults = true;
        battleResults.CanSkipExperienceAndMoneyGain = true;

        ChangePlayerLayer();
    }

    private void LookForEnemyStartingEffects()
    {
        bool createdElectricStormSticker = false;
        bool createdSolarFlareSticker = false;
        bool createdPoisonGasSticker = false;
        bool createdCrystalWinterSticker = false;

        for(int i = 0; i < enemies.Count; i++)
        {
            BattleEnemy enemy = enemies[i];

            if(enemy._EnemyStats.statusEffectToStartWith.Length > 0)
            {
                if (MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.EvenGround))
                {
                    stickerPowerHolder.CreateStickerMessage(StickerPower.EvenGround);
                } 

                for(int j = 0; j < enemy._EnemyStats.statusEffectToStartWith.Length; j++)
                {
                    switch (enemy._EnemyStats.statusEffectToStartWith[j])
                    {
                        case (StatusEffect.StrengthUp):
                            statusEffectManager.CreateEnemyStatusEffect(enemy, StatusEffect.StrengthUp, "Strength Up", 3, 30, false, statusEffectManager.StrengthUp, false, false);
                            if (MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.EvenGround))
                            {
                                statusEffectManager.CreateStatusEffect(StatusEffect.StrengthUp, "Strength Up", 3, 30, false, statusEffectManager.StrengthUp, false);
                            }
                            break;
                        case (StatusEffect.DefenseUp):
                            statusEffectManager.CreateEnemyStatusEffect(enemy, StatusEffect.DefenseUp, "Defense Up", 3, 20, false, statusEffectManager.DefenseUp, false, false);
                            if (MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.EvenGround))
                            {
                                statusEffectManager.CreateStatusEffect(StatusEffect.DefenseUp, "Defense Up", 3, 20, false, statusEffectManager.DefenseUp, false);
                            }
                            break;
                        case (StatusEffect.HpRegen):
                            statusEffectManager.CreateEnemyStatusEffect(enemy, StatusEffect.HpRegen, "HP Regen", 3, 10, true, statusEffectManager.HpRegen, false, false);
                            if (MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.EvenGround))
                            {
                                statusEffectManager.CreateStatusEffect(StatusEffect.HpRegen, "HP Regen", 3, 10, true, statusEffectManager.HpRegen, false);
                            }
                            break;
                        case (StatusEffect.BurnsImmune):
                            statusEffectManager.CreateEnemyStatusEffect(enemy, StatusEffect.BurnsImmune, "Burn-Proof", -1, 0, false, statusEffectManager.BurnsImmuneSprite, false, false);
                            if (MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.EvenGround))
                            {
                                statusEffectManager.CreateStatusEffect(StatusEffect.BurnsImmune, "Burn-Proof", -1, 0, false, statusEffectManager.BurnsImmuneSprite, false);
                            }
                            break;
                        case (StatusEffect.PoisonImmune):
                            statusEffectManager.CreateEnemyStatusEffect(enemy, StatusEffect.PoisonImmune, "Poison-Proof", -1, 0, false, statusEffectManager.PoisionImmuneSprite, false, false);
                            if (MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.EvenGround))
                            {
                                statusEffectManager.CreateStatusEffect(StatusEffect.PoisonImmune, "Poison-Proof", -1, 30, false, statusEffectManager.BurnsImmuneSprite, false);
                            }
                            break;
                        case (StatusEffect.ParalysisImmune):
                            statusEffectManager.CreateEnemyStatusEffect(enemy, StatusEffect.ParalysisImmune, "Stun-Proof", -1, 0, false, statusEffectManager.ParalysisImmuneSprite, false, false);
                            if (MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.EvenGround))
                            {
                                statusEffectManager.CreateStatusEffect(StatusEffect.ParalysisImmune, "Stun-Proof", -1, 0, false, statusEffectManager.ParalysisImmuneSprite, false);
                            }
                            break;
                        case (StatusEffect.FreezeImmune):
                            statusEffectManager.CreateEnemyStatusEffect(enemy, StatusEffect.FreezeImmune, "Freeze-Proof", -1, 0, false, statusEffectManager.FreezeImmuneSprite, false, false);
                            break;
                    }
                }
            }

            if (MenuController.instance._StickerPowerManager.CheckStartOfBattleStickerPower(StickerPower.ElectricStorm))
            {
                if(!createdElectricStormSticker)
                {
                    stickerPowerHolder.CreateStickerMessage(StickerPower.ElectricStorm);

                    createdElectricStormSticker = true;
                }

                if(enemy.StatusEffectHolder.transform.childCount <= 0)
                {
                    statusEffectManager.CreateEnemyStatusEffect(enemy, StatusEffect.Paralysis, "Paralysis", 1, 0, true, statusEffectManager.ParalysisSprite, true, false);
                }
                else
                {
                    bool isImmune = false;

                    for (int j = 0; j < enemy.StatusEffectHolder.transform.childCount; j++)
                    {
                        StatusEffects effects = enemy.StatusEffectHolder.transform.GetChild(j).GetComponent<StatusEffects>();

                        if (effects._statusEffect == StatusEffect.ParalysisImmune)
                        {
                            enemy.StatusEffectText.transform.parent.GetComponent<Animator>().Play("StatusText", -1, 0);
                            enemy.StatusEffectText.text = "IMMUNE";

                            isImmune = true;

                            break;
                        }
                    }

                    if(!isImmune)
                    {
                        statusEffectManager.CreateEnemyStatusEffect(enemy, StatusEffect.Paralysis, "Paralysis", 1, 0, true, statusEffectManager.ParalysisSprite, true, false);
                    }
                }
            }

            if (MenuController.instance._StickerPowerManager.CheckStartOfBattleStickerPower(StickerPower.PoisonGas))
            {
                if(!createdPoisonGasSticker)
                {
                    stickerPowerHolder.CreateStickerMessage(StickerPower.PoisonGas);

                    createdPoisonGasSticker = true;
                }

                if (enemy.StatusEffectHolder.transform.childCount <= 0)
                {
                    statusEffectManager.CreateEnemyStatusEffect(enemy, StatusEffect.Poison, "Poison", 3, 0, true, statusEffectManager.PoisonSprite, true, false);
                }
                else
                {
                    bool isImmune = false;

                    for (int j = 0; j < enemy.StatusEffectHolder.transform.childCount; j++)
                    {
                        StatusEffects effects = enemy.StatusEffectHolder.transform.GetChild(j).GetComponent<StatusEffects>();

                        if (effects._statusEffect == StatusEffect.PoisonImmune)
                        {
                            enemy.StatusEffectText.transform.parent.GetComponent<Animator>().Play("StatusText", -1, 0);
                            enemy.StatusEffectText.text = "IMMUNE";

                            isImmune = true;

                            break;
                        }
                    }

                    if(!isImmune)
                    {
                        statusEffectManager.CreateEnemyStatusEffect(enemy, StatusEffect.Poison, "Poison", 3, 0, true, statusEffectManager.PoisonSprite, true, false);
                    }
                }
            }

            if (MenuController.instance._StickerPowerManager.CheckStartOfBattleStickerPower(StickerPower.SolarFlare))
            {
                if(!createdSolarFlareSticker)
                {
                    stickerPowerHolder.CreateStickerMessage(StickerPower.SolarFlare);

                    createdSolarFlareSticker = true;
                }

                if (enemy.StatusEffectHolder.transform.childCount <= 0)
                {
                    statusEffectManager.CreateEnemyStatusEffect(enemy, StatusEffect.Burns, "Burns", 3, 0, true, statusEffectManager.BurnsSprite, true, false);
                }
                else
                {
                    bool isImmune = false;

                    for (int j = 0; j < enemy.StatusEffectHolder.transform.childCount; j++)
                    {
                        StatusEffects effects = enemy.StatusEffectHolder.transform.GetChild(j).GetComponent<StatusEffects>();

                        if (effects._statusEffect == StatusEffect.BurnsImmune)
                        {
                            enemy.StatusEffectText.transform.parent.GetComponent<Animator>().Play("StatusText", -1, 0);
                            enemy.StatusEffectText.text = "IMMUNE";

                            isImmune = true;

                            break;
                        }
                    }

                    if(!isImmune)
                    {
                        statusEffectManager.CreateEnemyStatusEffect(enemy, StatusEffect.Burns, "Burns", 3, 0, true, statusEffectManager.BurnsSprite, true, false);
                    }
                }
            }

            if (MenuController.instance._StickerPowerManager.CheckStartOfBattleStickerPower(StickerPower.CrystalWinter))
            {
                if (!createdCrystalWinterSticker)
                {
                    stickerPowerHolder.CreateStickerMessage(StickerPower.CrystalWinter);

                    createdCrystalWinterSticker = true;
                }

                if (enemy.StatusEffectHolder.transform.childCount <= 0)
                {
                    statusEffectManager.CreateEnemyStatusEffect(enemy, StatusEffect.Freeze, "Freeze", 3, 0, true, statusEffectManager.FreezeSprite, true, false);
                }
                else
                {
                    bool isImmune = false;

                    for (int j = 0; j < enemy.StatusEffectHolder.transform.childCount; j++)
                    {
                        StatusEffects effects = enemy.StatusEffectHolder.transform.GetChild(j).GetComponent<StatusEffects>();

                        if (effects._statusEffect == StatusEffect.FreezeImmune)
                        {
                            enemy.StatusEffectText.transform.parent.GetComponent<Animator>().Play("StatusText", -1, 0);
                            enemy.StatusEffectText.text = "IMMUNE";

                            isImmune = true;

                            break;
                        }
                    }

                    if (!isImmune)
                    {
                        statusEffectManager.CreateEnemyStatusEffect(enemy, StatusEffect.Freeze, "Freeze", 3, 0, true, statusEffectManager.FreezeSprite, true, false);
                    }
                }
            }
        }

        statusEffectManager.AdjustStatusEffectNavigationsForPlayer();
        statusEffectManager.AdjustStatusEffectNavigationsForEnemies();
    }

    public void ShowBattleMessage(string message)
    {
        battleMessageAnimator.Play("Message", -1, 0);

        battleMessageText.text = message;

        AudioManager.instance.PlaySoundEffect(AudioManager.instance.ErrorMessageAudio);
    }

    private void ChangePlayerLayer()
    {
        foreach(Transform transform in battlePlayer.GetComponentsInChildren<Transform>(true))
        {
            transform.gameObject.layer = 6;
        }
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);

        gameOverScreenAnimator.Play("GameOverScreen");

        _inputManager.SetSelectedObject(gameOverRetryObject);

        _InputManager._EventSystem.sendNavigationEvents = true;

        navagationDisabled = false;
    }

    public void OpenGraveyard(Card card)
    {
        Graveyard grave = graveyardPosition.GetComponent<Graveyard>();

        if(grave.cards.Count > 0)
        {
            card.SelectedCardAnimator.Play("SelectedCard");

            card._Animator.Play("ReverseHover");

            DisableCharityButton();

            grave.AddListenerToGraveCards();

            usingMysticCard = true;

            grave.GetComponent<Button>().onClick.Invoke();
        }
        else
        {
            ShowBattleMessage("No cards in the grave.");

            usingMysticCard = false;
            
            DeselectTargets(true);

            usedCard = null;
        }
    }

    public void ResetCard()
    {
        UsedCard.SelectedCardAnimator.Play("Idle", -1, 0);
    }

    public void CloseGraveyard()
    {
        graveyardAnimator.Play("Reverse");

        DeselectTargets(true);

        usedCard = null;

        SendCardToGrave();

        graveyardPosition.GetComponent<Graveyard>().ResetCards();

        graveyardPosition.GetComponent<Graveyard>().CloseButton.onClick.RemoveAllListeners();
    }

    public void OpenHandPanel(Card card)
    {
        if(battleDeck.CardsInHand.Count <= 1)
        {
            ShowBattleMessage("There are no cards to copy.");

            return;
        }

        ResetHandObjectsGraveAndDeck();
        DisableBattleUIInteracteability();

        handCards.DestroyHandCards();
        handCards.OpenPanel();

        card.SelectedCardAnimator.Play("SelectedCard");
        card._Animator.Play("ReverseHover");

        DisableCharityButton();

        usingMysticCard = true;

        for(int i = 0; i < battleDeck.CardsInHand.Count; i++)
        {
            if(battleDeck.CardsInHand[i]._cardTemplate.cardEffect != CardEffect.CopyCard)
            {
                handCards.ShowHandCards(battleDeck.CardsInHand[i], battleDeck.CardsInHand[i]._cardTemplate);
            }
            else
            {
                handCards.SetCopyCard(battleDeck.CardsInHand[i]);
            }
        }

        handCards.AddListenerToCards();
        handCards.CardNavigationRoutine();
    }

    public void CloseHandPanel()
    {
        usingMysticCard = false;

        handCards.ClosePanel();
    }

    public void OpenDeck(Card card)
    {
        if (battleDeck._Card.Count > 0)
        {
            card.SelectedCardAnimator.Play("SelectedCard");
            card._Animator.Play("ReverseHover");

            DisableCharityButton();

            battleDeck.AddListenerToDeckCards();

            usingMysticCard = true;

            deckButton.onClick.Invoke();
        }
        else
        {
            ShowBattleMessage("No cards in the deck.");

            usingMysticCard = false;

            DeselectTargets(true);

            usedCard = null;
        }
    }

    public void CloseDeck()
    {
        deckAnimator.Play("Reverse");

        DeselectTargets(true);

        usedCard = null;

        battleDeck.ResetCards();

        SendCardToGrave();

        isDeckOpen = false;
    }

    public void FocusItem(RectTransform card)
    {
        if (_inputManager.ControllerPluggedIn)
        {
            if(card.parent == deckParent || card.parent == graveParent)
            {
                StartCoroutine(ScrollViewFocusFunctions.FocusOnItemCoroutine(card.GetComponent<Card>()._PropogateDrag.scrollView, card, 3f));
            }
        } 
    }

    public void AddEnemyToBattle(BattleEnemy enemy)
    {
        enemy.gameObject.SetActive(true);

        SetUpEnemies();
    }
}