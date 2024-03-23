using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Security.Cryptography;
using Steamworks;

public enum BattleActions { NONE, closeUpAttack, rangedAttack, item, spell, support, helper };

public class BattlePlayer : MonoBehaviour
{
    [SerializeField]
    private BattleActions battleActions;

    [SerializeField]
    private MainCharacterStats mainCharacterStats;

    [SerializeField]
    private BattleSystem battleSystem;

    [SerializeField]
    private BattleDeck battleDeck;

    [SerializeField]
    private PerfectBlock perfectBlock;

    [SerializeField]
    private ParticleSystem basicAttackParticle, extraDamageParticle, guardDamageParticle, landParticle, jumpParticle, runParticleRight, runParticleLeft, stunParticle, burnsParticle, statusEffectRemoval, hpRegenParticle, cpRegenParticle, 
                           paralysisParticle, poisonParticle, enrageParticle, shieldBreakParticle, perfectGuardParticle;

    [SerializeField] [Header("Default Attack Particle")]
    private ParticleSystem attackParticleToUse;

    [SerializeField]
    private ParticleSystem criticalHitParticleIndicator;

    [SerializeField]
    private Animator animator, guardGaugeAnimator, lowGuardGaugeAnimator, healthBackgroundAnimator, healthAndCardPointsBarAnimator;

    [SerializeField]
    private Rigidbody rigidBody;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip jumpAudioClip, landAudioClip;

    [SerializeField]
    private TextMeshProUGUI playerHealthText, playerCardPointsText;

    [SerializeField]
    private Image healthFilledImage, cardPointsFilledImage, guardGaugeFilleImage, shieldImage;

    [SerializeField]
    private Sprite noviceShieldSprite, veteranShieldSprite, eliteShieldSprite;

    [SerializeField]
    private Transform defaultPosition, rangedAttackPosition, actionAreaAttackPosition;

    [SerializeField]
    private BoxCollider weaponHitBox, veteranSwordHitBox, eliteSwordHitBox;

    [SerializeField]
    private GameObject statusEffectHolder, criticalHitIcon;

    [SerializeField]
    private float moveSpeed, distanceBetweenSelfAndEnemy, distanceBackToDefaultPosition;

    [SerializeField]
    private int damageModifier;

    [SerializeField]
    private LayerMask layerMask;

    private CounterAttack counterAttack;

    private TextMeshProUGUI battleNumber, statusEffectText;

    private float currentGuardGauge, maxGuardGauge;

    [SerializeField]
    private int currentHealth, maxHealth, currentCardPoints, maxCardPoints, playerStrength, playerDefense, playerDefaultStrength, miracleGuardIndex, miracleCounterIndex,
                statusEffectIndex, strengthBoost, defenseBoost, cardPointsAttackHeal, randomHitAttackCount, strengthPercentage;

    private bool canGuard, isGuarding, canCounter, isStunned, isDamaged, canDealExtraDamage, activatedDefenseRegen, hasDefenseRegenStatus, hasCounterRegenStatus, hasCrackedShieldStatus, hasDullBladeStatus,
                 hasEmptyGuardGauge, isNearDeath, checkedPerfectBlock, isPerfectBlocking, isUndead, hasSecondChance, hasBlindRage, hasLuckyGuard, hasThorns, hasInvulnerable, isParalyzed, hasCourageUnderFire;

    private string animatorName;

    private Quaternion defaultRotation;

    private Coroutine defenseRegenRoutine;

    public MainCharacterStats _MainCharacterStats => mainCharacterStats;

    public bool HasCourageUnderFire => hasCourageUnderFire;

    public int PlayerDefense
    {
        get
        {
            return playerDefense;
        }
        set
        {
            playerDefense = value;
        }
    }

    public int PlayerStrength
    {
        get
        {
            return playerStrength;
        }
        set
        {
            playerStrength = value;
        }
    }

    public int StrengthBoost
    {
        get
        {
            return strengthBoost;
        }
        set
        {
            strengthBoost = value;
        }
    }

    public int StrengthPercentage
    {
        get
        {
            return strengthPercentage;
        }
        set
        {
            strengthPercentage = value;
        }
    }

    public int DefenseBoost
    {
        get
        {
            return defenseBoost;
        }
        set
        {
            defenseBoost = value;
        }
    }

    public int CardPointsAtttackHeal
    {
        get
        {
            return cardPointsAttackHeal;
        }
        set
        {
            cardPointsAttackHeal = value;
        }
    }

    public BattleSystem _BattleSystem => battleSystem;

    public ParticleSystem BurnsParticle => burnsParticle;

    public ParticleSystem HpRegenParticle => hpRegenParticle;

    public ParticleSystem CpRegenParticle => cpRegenParticle;

    public ParticleSystem ParalysisParticle => paralysisParticle;

    public ParticleSystem PoisonParticle => poisonParticle;

    public ParticleSystem EnrageParticle => enrageParticle;

    public ParticleSystem StunParticle
    {
        get
        {
            return stunParticle;
        }
        set
        {
            stunParticle = value;
        }
    }

    public ParticleSystem BasicAttackParticle
    {
        get
        {
            return basicAttackParticle;
        }
        set
        {
            basicAttackParticle = value;
        }
    }

    public MainCharacterStats _mainCharacterStats => mainCharacterStats;

    public Animator _Animator => animator;

    public Transform DefaultPosition => defaultPosition;

    public BoxCollider VeteranSwordHitBox => veteranSwordHitBox;

    public BoxCollider EliteSwordHitBox => eliteSwordHitBox;

    public Quaternion DefaultRotation => defaultRotation;

    public bool HasLuckyGuard
    {
        get
        {
            return hasLuckyGuard;
        }
        set
        {
            hasLuckyGuard = value;
        }
    }

    public bool HasInvulnerable
    {
        get
        {
            return hasInvulnerable;
        }
        set
        {
            hasInvulnerable = value;
        }
    }

    public bool HasThorns
    {
        get
        {
            return hasThorns;
        }
        set
        {
            hasThorns = value;
        }
    }

    public bool IsNearDeath
    {
        get
        {
            return isNearDeath;
        }
        set
        {
            isNearDeath = value;
        }
    }

    public LayerMask _layerMask
    {
        get
        {
            return layerMask;
        }
        set
        {
            layerMask = value;
        }
    }

    public BattleActions _BattleActions
    {
        get
        {
            return battleActions;
        }
        set
        {
            battleActions = value;
        }
    }

    public int MaxHealth => maxHealth;

    public int CurrentHealth
    {
        get
        {
            return currentHealth;
        }
        set
        {
            currentHealth = value;
        }
    }

    public int CurrentCardPoints
    {
        get
        {
            return currentCardPoints;
        }
        set
        {
            currentCardPoints = value;
        }
    }

    public int MiracleGuardIndex
    {
        get
        {
            return miracleGuardIndex;
        }
        set
        {
            miracleGuardIndex = value;
        }
    }

    public int MiracleCounterIndex
    {
        get
        {
            return miracleCounterIndex;
        }
        set
        {
            miracleCounterIndex = value;
        }
    }

    public int StatusEffectIndex
    {
        get
        {
            return statusEffectIndex;
        }
        set
        {
            statusEffectIndex = value;
        }
    }

    public CounterAttack _CounterAttack
    {
        get
        {
            return counterAttack;
        }
        set
        {
            counterAttack = value;
        }
    }

    public GameObject StatusEffectHolder
    {
        get
        {
            return statusEffectHolder;
        }
        set
        {
            statusEffectHolder = value;
        }
    }

    public Animator GuardGaugeAnimator
    {
        get
        {
            return guardGaugeAnimator;
        }
        set
        {
            guardGaugeAnimator = value;
        }
    }

    public BoxCollider WeaponHitBox
    {
        get
        {
            return weaponHitBox;
        }
        set
        {
            weaponHitBox = value;
        }
    }

    public TextMeshProUGUI BattleNumber
    {
        get
        {
            return battleNumber;
        }
        set
        {
            battleNumber = value;
        }
    }

    public TextMeshProUGUI StatusEffectText
    {
        get
        {
            return statusEffectText;
        }
        set
        {
            statusEffectText = value;
        }
    }

    public bool HasDefenseRegenStatus
    {
        get
        {
            return hasDefenseRegenStatus;
        }
        set
        {
            hasDefenseRegenStatus = value;
        }
    }

    public bool HasCrackedShieldStatus
    {
        get
        {
            return hasCrackedShieldStatus;
        }
        set
        {
            hasCrackedShieldStatus = value;
        }
    }

    public bool HasDullBladeStatus
    {
        get
        {
            return hasDullBladeStatus;
        }
        set
        {
            hasDullBladeStatus = value;
        }
    }

    public bool HasBlindRage
    {
        get
        {
            return hasBlindRage;
        }
        set
        {
            hasBlindRage = value;
        }
    }

    public bool HasSecondChance
    {
        get
        {
            return hasSecondChance;
        }
        set
        {
            hasSecondChance = value;
        }
    }

    public bool HasCounterRegenStatus
    {
        get
        {
            return hasCounterRegenStatus;
        }
        set
        {
            hasCounterRegenStatus = value;
        }
    }

    public bool IsParalyzed
    {
        get
        {
            return isParalyzed;
        }
        set
        {
            isParalyzed = value;
        }
    }

    public bool IsUndead
    {
        get
        {
            return isUndead;
        }
        set
        {
            isUndead = value;
        }
    }

    public bool IsGuarding
    {
        get
        {
            return isGuarding;
        }
        set
        {
            isGuarding = false;
        }
    }

    public bool IsStunned
    {
        get
        {
            return isStunned;
        }
        set
        {
            isStunned = value;
        }
    }

    public bool IsDamaged
    {
        get
        {
            return isDamaged;
        }
        set
        {
            isDamaged = value;
        }
    }

    public bool CanGuard
    {
        get
        {
            return canGuard;
        }
        set
        {
            canGuard = value;
        }
    }

    public bool CanCounter
    {
        get
        {
            return canCounter;
        }
        set
        {
            canCounter = value;
        }
    }

    public bool IsPerfectBlocking
    {
        get
        {
            return isPerfectBlocking;
        }
        set
        {
            isPerfectBlocking = value;
        }
    }

    private void Awake()
    {
        maxHealth = mainCharacterStats.maximumHealth;
        maxCardPoints = mainCharacterStats.maximumCardPoints;
        maxGuardGauge = mainCharacterStats.guardGauge;
        playerStrength = mainCharacterStats.strength;
        playerDefense = mainCharacterStats.defense;

        cardPointsAttackHeal = CardPointHealValue();

        mainCharacterStats.currentPlayerHealth = GameManager.instance.CurrentPlayerHealth;
        mainCharacterStats.currentPlayerCardPoints = GameManager.instance.CurrentPlayerCardPoints;

        currentHealth = GameManager.instance.CurrentPlayerHealth;
        currentCardPoints = GameManager.instance.CurrentPlayerCardPoints;

        currentGuardGauge = maxGuardGauge;

        healthFilledImage.fillAmount = (float)currentHealth / maxHealth;
        cardPointsFilledImage.fillAmount = (float)currentCardPoints / maxCardPoints;
        guardGaugeFilleImage.fillAmount = currentGuardGauge / maxGuardGauge;

        playerHealthText.text = currentHealth + "/" + maxHealth;
        playerCardPointsText.text = currentCardPoints + "/" + maxCardPoints;

        defaultPosition.position = transform.position;
        defaultRotation = transform.rotation;

        SetUpShieldSprite();

        if (currentHealth <= maxHealth / 4)
        {
            isNearDeath = true;

            healthBackgroundAnimator.Play("LowHealth", -1, 0);
        }
    }

    private void Update()
    {
        if (SteamOverlayPause.instance.IsPaused) return;

        if(canGuard)
        {
            if(!isParalyzed)
                GuardAction();
        }
        else if (canCounter)
        {
            if(!isParalyzed)
                CounterAction();
        }

        CheckExtraDamage();
    }

    private void SetUpShieldSprite()
    {
        switch(GameManager.instance._PlayerMenuInfo.shieldIndex)
        {
            case 0:
                shieldImage.sprite = noviceShieldSprite;
                break;
            case 1:
                shieldImage.sprite = veteranShieldSprite;
                break;
            case 2:
                shieldImage.sprite = eliteShieldSprite;
                break;
        }
    }

    private void GuardAction()
    {
        if(currentGuardGauge <= maxGuardGauge / 4)
        {
            lowGuardGaugeAnimator.Play("LowGauge");
        }
        else
        {
            lowGuardGaugeAnimator.Play("Idle");
        }

        if(currentGuardGauge > 0)
        {
            if(InputManager.instance.ControllerPluggedIn)
            {
                if(InputManager.instance.ControllerName == "xbox")
                {
                    if (Input.GetButton("XboxSalvageAll"))
                    {
                        if (!animator.GetBool("GuardHit"))
                            animator.Play("Guard");

                        isGuarding = true;

                        if (hasDefenseRegenStatus)
                        {
                            if (!activatedDefenseRegen)
                            {
                                defenseRegenRoutine = null;

                                defenseRegenRoutine = StartCoroutine("BeginDefenseRegen");

                                activatedDefenseRegen = true;
                            }
                        }

                        if (MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.MiracleGuard))
                        {
                            if (miracleGuardIndex > 2)
                            {
                                currentGuardGauge -= Time.deltaTime;

                                guardGaugeFilleImage.fillAmount = currentGuardGauge / maxGuardGauge;
                            }
                        }
                        else
                        {
                            currentGuardGauge -= Time.deltaTime;

                            guardGaugeFilleImage.fillAmount = currentGuardGauge / maxGuardGauge;
                        }
                    }

                    if (Input.GetButtonUp("XboxSalvageAll"))
                    {
                        isGuarding = false;

                        checkedPerfectBlock = false;

                        isPerfectBlocking = false;

                        activatedDefenseRegen = false;

                        if (defenseRegenRoutine != null)
                            StopCoroutine(defenseRegenRoutine);

                        defenseRegenRoutine = null;

                        animator.SetBool("GuardHit", false);

                        animator.Play("Idle_Battle");
                    }
                }
                else
                {
                    if (Input.GetButton("Ps4OpenChest"))
                    {
                        if (!animator.GetBool("GuardHit"))
                            animator.Play("Guard");

                        isGuarding = true;

                        if (hasDefenseRegenStatus)
                        {
                            if (!activatedDefenseRegen)
                            {
                                defenseRegenRoutine = null;

                                defenseRegenRoutine = StartCoroutine("BeginDefenseRegen");

                                activatedDefenseRegen = true;
                            }
                        }

                        if (MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.MiracleGuard))
                        {
                            if (miracleGuardIndex > 2)
                            {
                                currentGuardGauge -= Time.deltaTime;

                                guardGaugeFilleImage.fillAmount = currentGuardGauge / maxGuardGauge;
                            }
                        }
                        else
                        {
                            currentGuardGauge -= Time.deltaTime;

                            guardGaugeFilleImage.fillAmount = currentGuardGauge / maxGuardGauge;
                        }
                    }

                    if (Input.GetButtonUp("Ps4OpenChest"))
                    {
                        isGuarding = false;

                        checkedPerfectBlock = false;

                        isPerfectBlocking = false;

                        activatedDefenseRegen = false;

                        if (defenseRegenRoutine != null)
                            StopCoroutine(defenseRegenRoutine);

                        defenseRegenRoutine = null;

                        animator.SetBool("GuardHit", false);

                        animator.Play("Idle_Battle");
                    }
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.Z))
                {
                    if (!animator.GetBool("GuardHit"))
                        animator.Play("Guard");

                    isGuarding = true;

                    if (hasDefenseRegenStatus)
                    {
                        if (!activatedDefenseRegen)
                        {
                            defenseRegenRoutine = null;

                            defenseRegenRoutine = StartCoroutine("BeginDefenseRegen");

                            activatedDefenseRegen = true;
                        }
                    }

                    if (MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.MiracleGuard))
                    {
                        if (miracleGuardIndex > 2)
                        {
                            currentGuardGauge -= Time.deltaTime;

                            guardGaugeFilleImage.fillAmount = currentGuardGauge / maxGuardGauge;
                        }
                    }
                    else
                    {
                        currentGuardGauge -= Time.deltaTime;

                        guardGaugeFilleImage.fillAmount = currentGuardGauge / maxGuardGauge;
                    }
                }
                if (Input.GetKeyUp(KeyCode.Z))
                {
                    isGuarding = false;

                    checkedPerfectBlock = false;

                    isPerfectBlocking = false;

                    activatedDefenseRegen = false;

                    if (defenseRegenRoutine != null)
                        StopCoroutine(defenseRegenRoutine);

                    defenseRegenRoutine = null;

                    animator.SetBool("GuardHit", false);

                    animator.Play("Idle_Battle");
                }
            }
        }
        else
        {
            if(MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.LuckyGuard))
            {
                if(!hasLuckyGuard)
                {
                    battleSystem._StickerPowerHolder.CreateStickerMessage(StickerPower.LuckyGuard);

                    currentGuardGauge = maxGuardGauge / 2;

                    guardGaugeFilleImage.fillAmount = 0.5f;

                    hasLuckyGuard = true;
                }
                else
                {
                    if (!hasEmptyGuardGauge)
                    {
                        isGuarding = false;

                        checkedPerfectBlock = false;

                        isPerfectBlocking = false;

                        stunParticle.gameObject.SetActive(true);
                        stunParticle.Play();

                        animator.SetBool("IsStunned", true);

                        shieldBreakParticle.gameObject.SetActive(true);
                        shieldBreakParticle.Play();

                        if (PlayerPrefs.HasKey("SoundEffects"))
                        {
                            shieldBreakParticle.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SoundEffects");
                            shieldBreakParticle.GetComponent<AudioSource>().Play();
                        }
                        else
                        {
                            shieldBreakParticle.GetComponent<AudioSource>().Play();
                        }

                        animator.Play("Stunned");

                        hasEmptyGuardGauge = true;
                    }
                }
            }
            else
            {
                if (!hasEmptyGuardGauge)
                {
                    isGuarding = false;

                    checkedPerfectBlock = false;

                    isPerfectBlocking = false;

                    stunParticle.gameObject.SetActive(true);
                    animator.SetBool("IsStunned", true);
                    stunParticle.Play();

                    shieldBreakParticle.gameObject.SetActive(true);
                    shieldBreakParticle.Play();

                    if (PlayerPrefs.HasKey("SoundEffects"))
                    {
                        shieldBreakParticle.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SoundEffects");
                        shieldBreakParticle.GetComponent<AudioSource>().Play();
                    }
                    else
                    {
                        shieldBreakParticle.GetComponent<AudioSource>().Play();
                    }

                    animator.Play("Stunned");

                    hasEmptyGuardGauge = true;
                }
            }
        }
    }

    public void SetPerfectGuardToTrue()
    {
        if(!checkedPerfectBlock)
        {
            isPerfectBlocking = true;

            checkedPerfectBlock = true;

            perfectBlock._BoxCollider.enabled = true;
        }
    }

    public void SetPerfectGuardToFalse()
    {
        isPerfectBlocking = false;

        perfectBlock._BoxCollider.enabled = false;
    }

    private IEnumerator BeginDefenseRegen()
    {
        float regenTime = 0;

        while(regenTime < 1)
        {
            regenTime += Time.deltaTime;

            yield return null;
        }

        if(isUndead)
        {
            TakeDamage(1, true, false);
        }
        else
        {
            HealHealth(1, true);
        }

        hpRegenParticle.gameObject.SetActive(true);
        hpRegenParticle.Play();
        hpRegenParticle.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        hpRegenParticle.GetComponent<CardParticleAudio>().CheckAudio();

        activatedDefenseRegen = false;
    }

    private void CounterAction()
    {
        if(InputManager.instance.ControllerPluggedIn)
        {
            if(InputManager.instance.ControllerName == "xbox")
            {
                if (Input.GetButtonDown("XboxAttack"))
                {
                    if (battleSystem.Enemies[EnemyTurnIndex()].TotalAmountOfAttacks > 0)
                    {
                        if (counterAttack.CanCounter() && !isStunned && !stunParticle.isPlaying)
                        {
                            animator.Play("AttackCounter");
                        }
                        else if(!counterAttack.CanCounter())
                        {
                            counterAttack.StartMissedAttackRoutine();
                        }
                        else if (isStunned || !stunParticle.isPlaying)
                        {
                            battleSystem.ShowBattleMessage("Unable to counter at this time!");
                        }
                    }
                }
            }
            else
            {
                if (Input.GetButtonDown("Ps4Attack"))
                {
                    if (battleSystem.Enemies[EnemyTurnIndex()].TotalAmountOfAttacks > 0)
                    {
                        if (counterAttack.CanCounter() && !isStunned && !stunParticle.isPlaying)
                        {
                            animator.Play("AttackCounter");
                        }
                        else if (!counterAttack.CanCounter())
                        {
                            counterAttack.StartMissedAttackRoutine();
                        }
                        else if (isStunned || !stunParticle.isPlaying)
                        {
                            battleSystem.ShowBattleMessage("Unable to counter at this time!");
                        }
                    }
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                if (battleSystem.Enemies[EnemyTurnIndex()].TotalAmountOfAttacks > 0)
                {
                    if (counterAttack.CanCounter() && !isStunned)
                    {
                        animator.Play("AttackCounter");
                    }
                    else if (!counterAttack.CanCounter())
                    {
                        counterAttack.StartMissedAttackRoutine();
                    }
                    else if (isStunned)
                    {
                        battleSystem.ShowBattleMessage("Unable to counter at this time!");
                    }
                }
            }
        }
    }

    private int EnemyTurnIndex()
    {
        int enemyIndex = 0;

        for(int i = 0; i < battleSystem.Enemies.Count; i++)
        {
            if(battleSystem.Enemies[i].TakingTurn)
            {
                enemyIndex = i;
            }
        }

        return enemyIndex;
    }

    public void ResetGuard()
    {
        if (battleSystem.Enemies.Count <= 0) return;

        isGuarding = false;

        isPerfectBlocking = false;

        checkedPerfectBlock = false;

        animator.SetBool("GuardHit", false);

        animator.Play("Idle_Battle");
    }

    public void DisableStunParticle()
    {
        if(gameObject != null)
        {
            if(stunParticle != null)
            {
                if (stunParticle.gameObject.activeSelf)
                {
                    stunParticle.Stop();
                    stunParticle.gameObject.SetActive(false);
                    animator.SetBool("IsStunned", false);
                }
            }
        }
    }

    public void ResetGuardGauge()
    {
        if(hasCrackedShieldStatus)
        {
            currentGuardGauge = maxGuardGauge;

            float tempGauge = currentGuardGauge / 2;
            currentGuardGauge = tempGauge;

            guardGaugeFilleImage.fillAmount = 0.5f;
        }
        else
        {
            currentGuardGauge = maxGuardGauge;

            guardGaugeFilleImage.fillAmount = 1;
        }
    }

    public void ResetCounterAttack()
    {
        if(hasDullBladeStatus || hasBlindRage)
        {
            counterAttack.SetSuccessNotch();
        }
        else
        {
            counterAttack.SetSuccessNotch();
        }
    }

    private void CheckExtraDamage()
    {
        if(InputManager.instance.ControllerPluggedIn)
        {
            if(InputManager.instance.ControllerName == "xbox")
            {
                if (Input.GetButtonDown("XboxAttack"))
                {
                    if (canDealExtraDamage)
                    {
                        damageModifier = 2;

                        attackParticleToUse = extraDamageParticle;
                    }
                }
            }
            else
            {
                if (Input.GetButtonDown("Ps4Attack"))
                {
                    if (canDealExtraDamage)
                    {
                        damageModifier = 2;

                        attackParticleToUse = extraDamageParticle;
                    }
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (canDealExtraDamage)
                {
                    damageModifier = 2;

                    attackParticleToUse = extraDamageParticle;
                }
            }
        }
    }

    private int CardPointHealValue()
    {
        float percentage = ((float)maxCardPoints / 100) * mainCharacterStats.cardPointHealPercentage;

        float value = percentage * 100;

        value = Mathf.RoundToInt(value);

        return (int)value;
    }

    private IEnumerator MoveTowardsEnemy()
    {
        float distanceToEnemy = Vector3.Distance(transform.position, battleSystem.Enemies[battleSystem.TargetIndex].transform.position);

        battleSystem.FadeAllEnemyHealthBars();

        if(!battleSystem.HittingAllEnemies)
        {
            while (distanceToEnemy > distanceBetweenSelfAndEnemy)
            {
                Vector3 distance = new Vector3(battleSystem.Enemies[battleSystem.TargetIndex].transform.position.x - transform.position.x, 0,
                                               battleSystem.Enemies[battleSystem.TargetIndex].transform.position.z - transform.position.z).normalized;

                distanceToEnemy = Vector3.Distance(transform.position, battleSystem.Enemies[battleSystem.TargetIndex].transform.position);

                transform.position += distance * moveSpeed * Time.deltaTime;

                if (!battleSystem.HoverOverAllEnemies)
                {
                    Quaternion rotation = Quaternion.LookRotation(distance);

                    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5 * Time.deltaTime);
                }

                yield return null;
            }
        }
        else
        {
            float distanceForAttack = Vector3.Distance(transform.position, actionAreaAttackPosition.position);

            while (distanceForAttack > 2)
            {
                Vector3 distance = new Vector3(actionAreaAttackPosition.position.x - transform.position.x, 0,
                                               actionAreaAttackPosition.position.z - transform.position.z).normalized;

                distanceForAttack = Vector3.Distance(transform.position, actionAreaAttackPosition.position);

                transform.position += distance * moveSpeed * Time.deltaTime;

                yield return null;
            }
        }

        if(battleSystem._cardTemplate != null)
        {
            if (battleSystem._cardTemplate.cardName.Contains("Dual"))
            {
                animator.Play("FirstAttack");
            }
            else if(battleSystem._cardTemplate.cardName.Contains("Tri"))
            {
                animator.Play("FirstAttackTri");
            }
            else if(battleSystem._cardTemplate.cardName.Contains("Thrust") || battleSystem._cardTemplate.cardName.Contains("Point") || battleSystem._cardTemplate.cardName.Contains("Brave"))
            {
                animator.Play("ThrustAttack");
            }
            else if(battleSystem._cardTemplate.cardName.Contains("Rend") || battleSystem._cardTemplate.cardName.Contains("Claw") || battleSystem._cardTemplate.cardName.Contains("Shred") ||
                    battleSystem._cardTemplate.cardName.Contains("Pulverize") || battleSystem._cardTemplate.cardName.Contains("Edge") || battleSystem._cardTemplate.cardName.Contains("Earth Rend") ||
                    battleSystem._cardTemplate.cardName.Contains("Thunder Rend") || battleSystem._cardTemplate.cardName.Contains("Frigid Rend") || battleSystem._cardTemplate.cardName.Contains("Subdue"))
            {
                animator.Play("SlashAttack");
            }
            else if(battleSystem._cardTemplate.cardName.Contains("Sky") || battleSystem._cardTemplate.cardName.Contains("Boom"))
            {
                animator.Play("JumpSlash");
            }
            else if(battleSystem._cardTemplate.cardName.Contains("Upper") || battleSystem._cardTemplate.cardName.Contains("Power") || battleSystem._cardTemplate.cardName.Contains("Impact") || 
                    battleSystem._cardTemplate.cardName.Contains("Marine") || battleSystem._cardTemplate.cardName.Contains("Puncture") || battleSystem._cardTemplate.cardName.Contains("Fury"))
            {
                animator.Play("Uppercut");
            }
            else if (battleSystem._cardTemplate.cardName.Contains("Somersault") || battleSystem._cardTemplate.cardName.Contains("Assault") || battleSystem._cardTemplate.cardName.Contains("Curvet") ||
                     battleSystem._cardTemplate.cardName.Contains("Blossom"))
            {
                animator.Play("Roll");
            }
            else if(battleSystem._cardTemplate.cardName.Contains("Devastation"))
            {
                battleSystem.UsedCard.ParticleEffect.transform.GetChild(0).gameObject.SetActive(true);
                battleSystem.UsedCard.ParticleEffect.transform.GetChild(1).gameObject.SetActive(false);
                battleSystem.UsedCard.ParticleEffect.transform.GetChild(2).gameObject.SetActive(false);

                animator.Play("DevastationCombo_1");
            }
            else if (battleSystem._cardTemplate.cardName.Contains("Piercing Strike"))
            {
                battleSystem.UsedCard.ParticleEffect.transform.GetChild(0).gameObject.SetActive(true);
                battleSystem.UsedCard.ParticleEffect.transform.GetChild(1).gameObject.SetActive(false);

                animator.Play("RendingThrustCombo_1");
            }
            else
            {
                animator.Play("Attack01");
            }
        }
        else
        {
            animator.Play("Attack01");
        }
    }

    private IEnumerator MoveForRangedAttack()
    {
        float distanceToEnemy = Vector3.Distance(transform.position, rangedAttackPosition.position);

        battleSystem.FadeAllEnemyHealthBars();

        while (distanceToEnemy > distanceBetweenSelfAndEnemy)
        {
            Vector3 distance = new Vector3(rangedAttackPosition.position.x - transform.position.x, 0,
                                           rangedAttackPosition.position.z - transform.position.z).normalized;

            distanceToEnemy = Vector3.Distance(transform.position, rangedAttackPosition.position);

            transform.position += distance * moveSpeed * Time.deltaTime;

            if(!battleSystem.HoverOverAllEnemies)
            {
                if(!GameManager.instance.IsAPreemptiveStrike)
                {
                    Vector3 lookVector = new Vector3(battleSystem.Enemies[battleSystem.TargetIndex].transform.position.x - transform.position.x, 0,
                                             battleSystem.Enemies[battleSystem.TargetIndex].transform.position.z - transform.position.z).normalized;

                    Quaternion rot = Quaternion.LookRotation(lookVector);
                    transform.rotation = Quaternion.Slerp(transform.rotation, rot, 10 * Time.deltaTime);
                }
            }

            yield return null;
        }

        if (battleSystem._cardTemplate != null)
        {
            if (battleSystem._cardTemplate.cardName.Contains("Brave"))
            {
                animator.Play("ThrustAttackCast");
            }
            else if(battleSystem._cardTemplate.cardName.Contains("Dropkick") || battleSystem._cardTemplate.cardName.Contains("Assault"))
            {
                animator.Play("RollCast");
            }
            else if (battleSystem._cardTemplate.cardName.Contains("Void"))
            {
                animator.Play("UppercutCast");
            }
            else
            {
                CastSpellAnimation();
            }
        }
        else
        {
            CastSpellAnimation();
        }
    }

    private IEnumerator ReturnBackToDefaultPosition()
    {
        animator.Play("JumpToPosition");

        yield return new WaitForSeconds(0.3f);

        rigidBody.useGravity = true;
        rigidBody.isKinematic = false;

        rigidBody.AddForce(Vector3.up * 3.5f, ForceMode.Impulse);

        PlayJumpParticle();

        float distanceToDefaultPosition = Vector3.Distance(transform.position, defaultPosition.position);

        while(distanceToDefaultPosition > distanceBackToDefaultPosition)
        {
            Vector3 distance = new Vector3(transform.position.x - defaultPosition.position.x, 0,
                                           transform.position.z - defaultPosition.position.z).normalized;

            distanceToDefaultPosition = Vector3.Distance(transform.position, defaultPosition.position);

            transform.position -= distance * 10 * Time.deltaTime;

            transform.rotation = Quaternion.Slerp(transform.rotation, defaultRotation, 3 * Time.deltaTime);

            yield return null;
        }

        transform.position = defaultPosition.position;

        transform.rotation = defaultPosition.rotation;

        landParticle.gameObject.SetActive(true);
        landParticle.Play();

        TurnOffPlayerGravity();

        healthAndCardPointsBarAnimator.Play("PlayerStats");

        if (!canGuard && !canCounter)
            SetAnimationBackToIdle();

        if (battleSystem.PlayersTurn)
        {
            if(!battleSystem.HasMysticFavoredPower)
            {
                battleSystem.EnemyTurn();
                if (battleSystem.Enemies.Count > 0)
                {
                    battleSystem.ResetEnemyStatusIndex();
                    battleSystem.CheckEnemyStatus();
                }
            }
            else
            {
                battleSystem.ContinuePlayerTurn();

                battleSystem.HasMysticFavoredPower = false;
            }
        }  
    }

    public IEnumerator WalkBackToDefaultPosition()
    {
        animator.Play("RunBackwardBattle");

        float distanceToDefaultPosition = Vector3.Distance(transform.position, defaultPosition.position);

        while (distanceToDefaultPosition > distanceBackToDefaultPosition)
        {
            Vector3 distance = new Vector3(transform.position.x - defaultPosition.position.x, 0,
                                           transform.position.z - defaultPosition.position.z).normalized;

            distanceToDefaultPosition = Vector3.Distance(transform.position, defaultPosition.position);

            transform.position -= distance * 5 * Time.deltaTime;

            transform.rotation = Quaternion.Slerp(transform.rotation, defaultRotation, 3 * Time.deltaTime);

            yield return null;
        }
        transform.position = defaultPosition.position;
        transform.rotation = defaultPosition.rotation;

        healthAndCardPointsBarAnimator.Play("PlayerStats");

        if (!canGuard && !canCounter)
            SetAnimationBackToIdle();

        if(GameManager.instance.IsAPreemptiveStrike)
        {
            for(int i = 0; i < battleSystem.Enemies.Count; i++)
            {
                if(battleSystem.Enemies[i].CurrentHealth > 0)
                {
                    battleSystem.ReverseAllEnemyHealthBars();
                }
            }
            battleDeck.Draw(0);
            GameManager.instance.IsAPreemptiveStrike = false;
        }
        else
        {
            if (battleSystem.PlayersTurn)
            {
                if(!battleSystem.HasMysticFavoredPower)
                {
                    if (battleSystem.Enemies.Count > 0)
                    {
                        battleSystem.EnemyTurn();
                        battleSystem.ResetEnemyStatusIndex();

                        if(battleSystem.Enemies[battleSystem.EnemyIndex].StatusEffectHolder.transform.childCount > 0)
                           battleSystem.CheckEnemyStatus();
                    }
                }
                else
                {
                    battleSystem.ContinuePlayerTurn();

                    battleSystem.HasMysticFavoredPower = false;
                }
            }  
        }

        TurnOffPlayerGravity();
    }

    public void TurnOffPlayerGravity()
    {
        rigidBody.useGravity = false;
        rigidBody.isKinematic = true;
    }

    public void CheckForStatusEffectsBeforeTurn()
    {
        if(statusEffectHolder.transform.childCount <= 0)
        {
            if (battleDeck.CurrentHandSize < battleDeck.MaxHandSize)
            {
                if(!battleDeck.DrawingCards)
                {
                    battleDeck.DrawingCards = true;
                    battleDeck.Draw(0);
                }
            }
            else
            {
                if(!isParalyzed)
                    battleSystem.PlayerTurn();
            }
        }
        else
        {
            if(currentHealth > 0)
            {
                StatusEffects statusEffect = null;

                if (statusEffectIndex < statusEffectHolder.transform.childCount)
                {
                    statusEffect = statusEffectHolder.transform.GetChild(statusEffectIndex).GetComponent<StatusEffects>();

                    if (statusEffect.ShouldCheckStatus)
                    {
                        statusEffect.ApplyEffect();

                        statusEffectIndex++;
                    }
                    else
                    {
                        statusEffect.ApplyEffect();

                        statusEffectIndex++;
                    }
                }
                else
                {
                    statusEffect = statusEffectHolder.transform.GetChild(statusEffectIndex - 1).GetComponent<StatusEffects>();

                    if (statusEffect != null)
                    {
                        if (statusEffect.StatusTime > 0)
                        {
                            if(battleDeck.CurrentHandSize < battleDeck.MaxHandSize)
                            {
                                if(!isParalyzed)
                                {
                                    if (!battleDeck.DrawingCards)
                                    {
                                        battleDeck.DrawingCards = true;
                                        battleDeck.Draw(0);
                                    }
                                }
                            }
                            else
                            {
                                if(battleSystem.BattleUICanvasgroup != null)
                                {
                                    if(!isParalyzed)
                                    {
                                        if (!battleSystem.PlayersTurn)
                                            battleSystem.PlayerTurn();
                                    }
                                }
                            }
                        }
                        else if (statusEffect.StatusTime <= 0)
                        {
                            if(!isParalyzed)
                            {
                                if (!battleSystem.PlayersTurn)
                                    battleSystem.PlayerTurn();
                            }
                        }
                    }
                }
            }
        }
    }

    public bool IsCheckingStatusEffects()
    {
        return statusEffectIndex >= statusEffectHolder.transform.childCount;
    }

    public void EnableStatusEffectRaycast()
    {
        if (statusEffectHolder.transform.childCount > 0)
        {
            for (int i = 0; i < statusEffectHolder.transform.childCount; i++)
            {
                StatusEffects statusEffects = statusEffectHolder.transform.GetChild(i).GetComponent<StatusEffects>();

                statusEffects.GetComponent<Image>().raycastTarget = true;
            }
        }
    }

    public void DisableStatusEffectRaycast()
    {
        if(statusEffectHolder.transform.childCount > 0)
        {
            for(int i = 0; i < statusEffectHolder.transform.childCount; i++)
            {
                StatusEffects statusEffects = statusEffectHolder.transform.GetChild(i).GetComponent<StatusEffects>();

                statusEffects.GetComponent<Image>().raycastTarget = false;
            }
        }
    }

    public void Action()
    {
        switch (battleActions)
        {
            case (BattleActions.closeUpAttack):
                DefaultAttack();
                break;
            case (BattleActions.support):
                NonAttackAnimation();
                break;
            case (BattleActions.helper):
                NonAttackAnimation();
                break;
            case (BattleActions.spell):
                RangedAttack();
                break;
            case (BattleActions.item):
                if (battleSystem.UsedCard._cardTemplate.cardEffect == CardEffect.Damage)
                {
                    RangedAttack();
                }
                else
                {
                    NonAttackAnimation();
                }
                break;
            default:
                battleActions = BattleActions.closeUpAttack;
                DefaultAttack();
                break;
        }
    }

    public void DisableBattleInformation()
    {
        if(battleSystem.BattleInformationAnimator.GetCurrentAnimatorClipInfo(0).Length > 0)
           battleSystem.BattleInformationAnimator.Play("Reverse");
    }

    public void CalculateHealth()
    {
        healthFilledImage.fillAmount = (float)currentHealth / maxHealth;

        if(currentHealth <= 0)
        {
            if(hasSecondChance)
            {
                HealHealth(mainCharacterStats.maximumHealth, true);

                RemoveSpecificStatusEffect(StatusEffect.Revive);
            }
            else
            {
                currentHealth = 0;

                animator.Play("Dead");

                stunParticle.Stop();

                if (guardGaugeAnimator.GetCurrentAnimatorClipInfo(0).Length > 0)
                {
                    guardGaugeAnimator.Play("Reverse");
                }

                if (battleSystem.CounterMeasurePromptAnimator.GetCurrentAnimatorClipInfo(0).Length > 0)
                {
                    battleSystem.CounterMeasurePromptAnimator.Play("Reverse");
                }

                canGuard = false;
                canCounter = false;

                RemoveAllStatusEffects(false);
            }
        }

        playerHealthText.text = currentHealth + "/" + maxHealth;

        if(currentHealth <= maxHealth / 4)
        {
            isNearDeath = true;

            healthBackgroundAnimator.Play("LowHealth", -1, 0);
        }
        else
        {
            isNearDeath = false;

            healthBackgroundAnimator.Play("Idle");
        }
    }

    public void CheckCourageUnderFireSticker()
    {
        if(MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.CourageUnderFire))
        {
            int percentage = Mathf.RoundToInt(MaxHealth / 5);

            if (currentHealth <= percentage)
            {
                if (!hasCourageUnderFire)
                {
                    battleSystem._StickerPowerHolder.CreateStickerMessage(StickerPower.CourageUnderFire);
                    battleSystem.DoubleCardStrength();

                    hasCourageUnderFire = true;
                }
            }
            else
            {
                if(hasCourageUnderFire)
                {
                    hasCourageUnderFire = false;

                    battleSystem.HalveCardStrength();
                }
            }
        }
    }

    public void TakeDamage(int value, bool ignoreDefense, bool playHitAnimation)
    {
        if (isGuarding)
        {
            if(!ignoreDefense)
            {
                value = Mathf.Abs(value - playerDefense) / 2;

                Mathf.Round(value);

                if (value <= 0)
                {
                    value = 0;
                }
                else
                {
                    battleSystem.DidPlayerTakeDamage = true;

                    currentHealth -= Mathf.Abs(value);

                    animator.SetBool("GuardHit", true);
                }
            }
            else
            {
                currentHealth -= Mathf.Abs(value);
            }

            DamageGuardParticle();
        }
        else
        {
            if(!ignoreDefense)
            {
                value = value - playerDefense;
            }

            if (value <= 0)
            {
                value = 0;
            }
            else
            {
                if(playHitAnimation)
                {
                    battleSystem.DidPlayerTakeDamage = true;
                    isStunned = true;
                    animator.Play("GetHit");
                }

                currentHealth -= Mathf.Abs(value);
            }
        }

        mainCharacterStats.currentPlayerHealth = currentHealth;

        BattleNumber _battleNumber = battleNumber.GetComponent<BattleNumber>();

        _battleNumber.BattleNumberText.text = value.ToString();
        _battleNumber.ApplyDamageColor();

        weaponHitBox.enabled = false;
        CalculateHealth();
        CheckCourageUnderFireSticker();
    }

    public void SetBattleNumberPosition()
    {
        BattleNumber _battleNumber = battleNumber.GetComponent<BattleNumber>();

        _battleNumber.transform.position = new Vector3(transform.position.x - 0.6f, transform.position.y + 2, transform.position.z - 0.6f);
    }

    public void CalculateCardPoints(int value)
    {
        currentCardPoints += value;

        if(currentCardPoints > maxCardPoints)
        {
            currentCardPoints = maxCardPoints;
        }
        else if(currentCardPoints <= 0)
        {
            currentCardPoints = 0;
        }

        cardPointsFilledImage.fillAmount = (float)currentCardPoints / maxCardPoints;

        playerCardPointsText.text = currentCardPoints + "/" + maxCardPoints;
    }

    public void HealHealth(int value, bool showNumber)
    {
        currentHealth += value;

        mainCharacterStats.currentPlayerHealth = currentHealth;

        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
            mainCharacterStats.currentPlayerHealth = mainCharacterStats.maximumHealth;
        }

        CalculateHealth();

        if(showNumber)
        {
            BattleNumber _battleNumber = battleNumber.GetComponent<BattleNumber>();

            _battleNumber.transform.position = new Vector3(transform.position.x - 0.6f, transform.position.y + 2, transform.position.z - 0.6f);

            _battleNumber.BattleNumberText.text = value.ToString();
            _battleNumber.ApplyHpHealColor();
        }

        CheckCourageUnderFireSticker();
    }

    public void HealCardPoints(int value, bool showNumber)
    {
        CalculateCardPoints(value);

        mainCharacterStats.currentPlayerCardPoints = currentCardPoints;

        if(currentCardPoints > maxCardPoints)
        {
            currentCardPoints = maxCardPoints;
            mainCharacterStats.currentPlayerCardPoints = mainCharacterStats.maximumCardPoints;
        }

        if(showNumber)
        {
            BattleNumber _battleNumber = battleNumber.GetComponent<BattleNumber>();

            if(battleSystem.UsedCard != null)
            {
                _battleNumber.transform.position = new Vector3(transform.position.x - 0.6f, transform.position.y + 2, transform.position.z - 0.6f);
            }
            else
            {
                _battleNumber.transform.position = new Vector3(transform.position.x - 1f, transform.position.y + 2, transform.position.z - 0.6f);
            }

            _battleNumber.BattleNumberText.text = value.ToString() + " <size=0.35>CP";
            _battleNumber.ApplyCpHealColor();
        }
    }

    public void ShowGameOverScreen()
    {
        battleSystem.GameOver();
    }

    public void ControllerSelectTarget()
    {
        if (InputManager.instance.ControllerPluggedIn)
        {
            battleSystem.ConfirmedTarget = true;

            battleSystem.ChooseTarget(gameObject);
        }
    }

    public void ControllerDeSelectTarget()
    {
        if (InputManager.instance.ControllerPluggedIn)
        {
            battleSystem.ConfirmedTarget = false;
        }
    }

    private void DefaultAttack()
    {
        if(MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.IncreaedBasicAttack))
        {
            if(battleSystem.UsedCard == null)
            {
                battleSystem._StickerPowerHolder.CreateStickerMessage(StickerPower.IncreaedBasicAttack);

                playerStrength += 2;
            }
        }

        healthAndCardPointsBarAnimator.Play("Reverse");

        animator.Play("RunForwardBattle");

        StartCoroutine("MoveTowardsEnemy");
    }

    public void RangedAttack()
    {
        healthAndCardPointsBarAnimator.Play("Reverse");

        animator.Play("RunForwardBattle");

        StartCoroutine("MoveForRangedAttack");
    }

    private void NonAttackAnimation()
    {
        animator.Play("NonAttackAnimation");
    }

    private void CastSpellAnimation()
    {
        if(GameManager.instance.IsAPreemptiveStrike)
        {
            animator.Play("PreemptiveStrike");

            animatorName = "PreemptiveStrike";
        }
        else
        {
            animator.Play("CastSpell");

            animatorName = "CastSpell";
        }
    }

    public void HideStatsBar()
    {
        healthAndCardPointsBarAnimator.Play("Reverse");
    }

    public void SetAnimationBackToIdle()
    {
        if(gameObject.activeSelf)
           animator.Play("Idle_Battle");
    }

    public void SetGuardHitToFalse()
    {
        animator.SetBool("GuardHit", false);
    }

    public void PlayCriticalHitIndicator()
    {
        if(battleSystem._cardTemplate == null)
           criticalHitParticleIndicator.Play();
    }

    public void ToggleCanDealExtraDamage()
    {
        if(battleSystem._cardTemplate == null)
           canDealExtraDamage = true;
    }

    public void PlayJumpParticle()
    {
        jumpParticle.gameObject.SetActive(true);

        jumpParticle.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        jumpParticle.Play();
    }

    public void PlayRunRightParticle()
    {
        runParticleRight.Play();
    }

    public void PlayRunLeftParticle()
    {
        runParticleLeft.Play();
    }

    public void HitEnemy()
    {
        if(battleSystem.HittingAllEnemies)
        {
            HitAllEnemies();
        }
        else
        {
            if(MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.Pierce))
            {
                if (MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.SoulDrain) && battleSystem._cardTemplate == null)
                {
                    battleSystem.Enemies[battleSystem.TargetIndex].TakeDamage(1, true, true, true);
                }
                else
                {
                    battleSystem.Enemies[battleSystem.TargetIndex].TakeDamage(DamageValue(), true, true, true);
                }

                if (battleSystem._cardTemplate == null)
                {
                    if (MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.DispellingSword))
                    {
                        if (battleSystem.Enemies[battleSystem.TargetIndex].StatusEffectHolder.transform.childCount > 0)
                        {
                            battleSystem._StickerPowerHolder.CreateStickerMessage(StickerPower.DispellingSword);

                            foreach (StatusEffects effects in battleSystem.Enemies[battleSystem.TargetIndex].StatusEffectHolder.transform.GetComponentsInChildren<StatusEffects>())
                            {
                                switch (effects._statusEffect)
                                {
                                    case StatusEffect.StrengthUp:
                                        effects.BeginRemoveEffectRoutine();
                                        break;
                                    case StatusEffect.DefenseUp:
                                        effects.BeginRemoveEffectRoutine();
                                        break;
                                    case StatusEffect.HpRegen:
                                        effects.BeginRemoveEffectRoutine();
                                        break;
                                    case StatusEffect.ParalysisImmune:
                                        effects.BeginRemoveEffectRoutine();
                                        break;
                                    case StatusEffect.PoisonImmune:
                                        effects.BeginRemoveEffectRoutine();
                                        break;
                                    case StatusEffect.BurnsImmune:
                                        effects.BeginRemoveEffectRoutine();
                                        break;
                                    case StatusEffect.Thorns:
                                        effects.BeginRemoveEffectRoutine();
                                        break;
                                }
                            }
                        }
                    }

                    if (MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.Paralyze))
                    {
                        int percentChance = Random.Range(0, 100);

                        if (percentChance <= 25)
                        {
                            battleSystem._StickerPowerHolder.CreateStickerMessage(StickerPower.Paralyze);

                            battleSystem._StatusEffectManager.CreateEnemyStatusEffect(battleSystem.Enemies[battleSystem.TargetIndex], StatusEffect.Paralysis, "Paralysis", 1, 0, true,
                                                                                      battleSystem._StatusEffectManager.ParalysisSprite, true, true);
                        }
                    }

                    if (MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.Envenom))
                    {
                        int percentChance = Random.Range(0, 100);

                        if (percentChance <= 50)
                        {
                            battleSystem._StickerPowerHolder.CreateStickerMessage(StickerPower.Envenom);

                            battleSystem._StatusEffectManager.CreateEnemyStatusEffect(battleSystem.Enemies[battleSystem.TargetIndex], StatusEffect.Poison, "Burns", 3, 0, true,
                                                                                      battleSystem._StatusEffectManager.PoisonSprite, true, true);
                        }
                    }

                    if (MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.Sunburn))
                    {
                        int percentChance = Random.Range(0, 100);

                        if (percentChance <= 50)
                        {
                            battleSystem._StickerPowerHolder.CreateStickerMessage(StickerPower.Sunburn);

                            battleSystem._StatusEffectManager.CreateEnemyStatusEffect(battleSystem.Enemies[battleSystem.TargetIndex], StatusEffect.Burns, "Burns", 3, 0, true,
                                                                                      battleSystem._StatusEffectManager.BurnsSprite, true, true);
                        }
                    }
                }
            }
            else
            {
                if(MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.SoulDrain) && battleSystem._cardTemplate == null)
                {
                    battleSystem.Enemies[battleSystem.TargetIndex].TakeDamage(1, false, true, true);
                }
                else
                {
                    battleSystem.Enemies[battleSystem.TargetIndex].TakeDamage(DamageValue(), false, true, true);
                }

                if(battleSystem._cardTemplate == null)
                {
                    if (MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.DispellingSword))
                    {
                        if (battleSystem.Enemies[battleSystem.TargetIndex].StatusEffectHolder.transform.childCount > 0)
                        {
                            battleSystem._StickerPowerHolder.CreateStickerMessage(StickerPower.DispellingSword);

                            foreach (StatusEffects effects in battleSystem.Enemies[battleSystem.TargetIndex].StatusEffectHolder.transform.GetComponentsInChildren<StatusEffects>())
                            {
                                switch (effects._statusEffect)
                                {
                                    case StatusEffect.StrengthUp:
                                        effects.BeginRemoveEffectRoutine();
                                        break;
                                    case StatusEffect.DefenseUp:
                                        effects.BeginRemoveEffectRoutine();
                                        break;
                                    case StatusEffect.HpRegen:
                                        effects.BeginRemoveEffectRoutine();
                                        break;
                                    case StatusEffect.ParalysisImmune:
                                        effects.BeginRemoveEffectRoutine();
                                        break;
                                    case StatusEffect.PoisonImmune:
                                        effects.BeginRemoveEffectRoutine();
                                        break;
                                    case StatusEffect.BurnsImmune:
                                        effects.BeginRemoveEffectRoutine();
                                        break;
                                    case StatusEffect.Thorns:
                                        effects.BeginRemoveEffectRoutine();
                                        break;
                                }
                            }
                        }
                    }

                    if (MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.Paralyze))
                    {
                        int percentChance = Random.Range(0, 100);

                        if (percentChance <= 25)
                        {
                            battleSystem._StickerPowerHolder.CreateStickerMessage(StickerPower.Paralyze);

                            battleSystem._StatusEffectManager.CreateEnemyStatusEffect(battleSystem.Enemies[battleSystem.TargetIndex], StatusEffect.Paralysis, "Paralysis", 1, 0, true,
                                                                                      battleSystem._StatusEffectManager.ParalysisSprite, true, true);
                        }
                    }

                    if (MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.Envenom))
                    {
                        int percentChance = Random.Range(0, 100);

                        if (percentChance <= 50)
                        {
                            battleSystem._StickerPowerHolder.CreateStickerMessage(StickerPower.Envenom);

                            battleSystem._StatusEffectManager.CreateEnemyStatusEffect(battleSystem.Enemies[battleSystem.TargetIndex], StatusEffect.Poison, "Poison", 3, 0, true,
                                                                                      battleSystem._StatusEffectManager.PoisonSprite, true, true);
                        }
                    }

                    if (MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.Sunburn))
                    {
                        int percentChance = Random.Range(0, 100);

                        if (percentChance <= 50)
                        {
                            battleSystem._StickerPowerHolder.CreateStickerMessage(StickerPower.Sunburn);

                            battleSystem._StatusEffectManager.CreateEnemyStatusEffect(battleSystem.Enemies[battleSystem.TargetIndex], StatusEffect.Burns, "Burns", 3, 0, true,
                                                                                      battleSystem._StatusEffectManager.BurnsSprite, true, true);
                        }
                    }
                }
            }
        }
    }

    public void HitAllEnemies()
    {
        bool createdStickerMessage = false;
        bool createdDispellingSwordMessage = false;

        foreach(BattleEnemy enemies in battleSystem.Enemies)
        {
            if(MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.DispellingSword))
            {
                if(battleSystem._cardTemplate == null)
                {
                    if (enemies.StatusEffectHolder.transform.childCount > 0)
                    {
                        if (!createdDispellingSwordMessage)
                        {
                            battleSystem._StickerPowerHolder.CreateStickerMessage(StickerPower.DispellingSword);

                            createdDispellingSwordMessage = true;
                        }

                        foreach (StatusEffects effects in enemies.StatusEffectHolder.transform.GetComponentsInChildren<StatusEffects>())
                        {
                            switch (effects._statusEffect)
                            {
                                case StatusEffect.StrengthUp:
                                    effects.BeginRemoveEffectRoutine();
                                    break;
                                case StatusEffect.DefenseUp:
                                    effects.BeginRemoveEffectRoutine();
                                    break;
                                case StatusEffect.HpRegen:
                                    effects.BeginRemoveEffectRoutine();
                                    break;
                                case StatusEffect.ParalysisImmune:
                                    effects.BeginRemoveEffectRoutine();
                                    break;
                                case StatusEffect.PoisonImmune:
                                    effects.BeginRemoveEffectRoutine();
                                    break;
                                case StatusEffect.BurnsImmune:
                                    effects.BeginRemoveEffectRoutine();
                                    break;
                                case StatusEffect.FreezeImmune:
                                    effects.BeginRemoveEffectRoutine();
                                    break;
                                case StatusEffect.Thorns:
                                    effects.BeginRemoveEffectRoutine();
                                    break;
                            }
                        }
                    }
                }
            }

            if (MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.Pierce))
            {
                if (GameManager.instance.IsAPreemptiveStrike)
                {
                    if (MenuController.instance._StickerPowerManager.CheckStartOfBattleStickerPower(StickerPower.ForcefulStrike))
                    {
                        if (!createdStickerMessage)
                        {
                            battleSystem._StickerPowerHolder.CreateStickerMessage(StickerPower.ForcefulStrike);

                            createdStickerMessage = true;
                        }

                        if(MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.SoulDrain))
                        {
                            enemies.TakeDamage(1, true, true, true);
                        }
                        else
                        {
                            enemies.TakeDamage(DamageValue() * 2, true, true, true);
                        }
                    }
                    else
                    {
                        if(MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.SoulDrain))
                        {
                            enemies.TakeDamage(1, true, true, true);
                        }
                        else
                        {
                            enemies.TakeDamage(DamageValue(), true, true, true);
                        }
                    }
                }
                else
                {
                    if(MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.SoulDrain))
                    {
                        enemies.TakeDamage(1, true, true, true);
                    }
                    else
                    {
                        enemies.TakeDamage(DamageValue(), true, true, true);
                    }
                }
            }
            else
            {
                if(GameManager.instance.IsAPreemptiveStrike)
                {
                    if (MenuController.instance._StickerPowerManager.CheckStartOfBattleStickerPower(StickerPower.ForcefulStrike))
                    {
                        if (!createdStickerMessage)
                        {
                            battleSystem._StickerPowerHolder.CreateStickerMessage(StickerPower.ForcefulStrike);

                            createdStickerMessage = true;
                        }

                        if (MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.SoulDrain))
                        {
                            enemies.TakeDamage(1, false, true, true);
                        }
                        else
                        {
                            enemies.TakeDamage(DamageValue() * 2, false, true, true);
                        }
                    }
                    else
                    {
                        if (MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.SoulDrain))
                        {
                            enemies.TakeDamage(1, false, true, true);
                        }
                        else
                        {
                            enemies.TakeDamage(DamageValue(), false, true, true);
                        }
                    }
                }
                else
                {
                    if(battleSystem._cardTemplate == null)
                    {
                        if (MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.SoulDrain))
                        {
                            enemies.TakeDamage(1, false, true, true);
                        }
                        else
                        {
                            enemies.TakeDamage(DamageValue(), false, true, true);
                        }
                    }
                    else
                    {
                        enemies.TakeDamage(DamageValue(), false, true, true);
                    }
                }
            }
        }
    }

    public int DamageValue()
    {
        int damage = playerStrength * damageModifier;

        return damage;
    }

    public void PlayActionAnimation()
    {
        if (battleSystem._cardTemplate != null)
        {
            ParticleSystem ps = battleSystem.UsedCard.ParticleEffect;

            ps.gameObject.SetActive(true);

            ps.Play();

            if(ps.GetComponent<AudioSource>())
            {
                if(ps.GetComponent<CardParticleAudio>())
                {
                    if (!ps.GetComponent<AudioSource>().isPlaying)
                         ps.GetComponent<CardParticleAudio>().CheckAudio();

                    if(ps.GetComponent<ChildParticleAudio>())
                    {
                        ps.GetComponent<ChildParticleAudio>().CheckChildAudio();
                    }
                }
            }

            switch(battleSystem.UsedCard._cardTemplate.cardType)
            {
                case (CardType.Action):
                    if (battleSystem.UsedCard.IsParticleAProjectile)
                    {
                        if(battleSystem._cardTemplate.isGrounded)
                        {
                            ps.gameObject.transform.position = new Vector3(transform.position.x, ps.gameObject.transform.position.y, transform.position.z + 1);
                        }
                        else
                        {
                            ps.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z + 1);
                        }

                        if(battleSystem.UsedCard._cardTemplate.target == Target.RandomEnemy)
                        {
                            int rand = Random.Range(0, battleSystem.Enemies.Count);

                            ps.GetComponent<EpicToonFX.ETFXProjectileScript>().EnemyTarget = battleSystem.Enemies[rand].transform;

                            battleSystem.TargetIndex = rand;
                        }
                        else
                        {
                            if(ps.GetComponent<EpicToonFX.ETFXProjectileScript>())
                            {
                                ps.GetComponent<EpicToonFX.ETFXProjectileScript>().EnemyTarget = battleSystem.Enemies[battleSystem.TargetIndex].transform;
                            }
                            else
                            {
                                StartCoroutine(WaitUntilParticleFinishes(ps));
                            }
                        }

                        if(ps.GetComponent<EpicToonFX.ETFXProjectileScript>())
                           ps.GetComponent<EpicToonFX.ETFXProjectileScript>().SetDistance();
                    }
                    else
                    {
                        if (!battleSystem.HittingAllEnemies)
                        {
                            if(battleSystem.UsedCard._cardTemplate.isActionAOE)
                            {
                                if(battleSystem.UsedCard._cardTemplate.hasOffset)
                                {
                                    ps.gameObject.transform.position = new Vector3(battleSystem.Enemies[battleSystem.TargetIndex].transform.position.x, ps.gameObject.transform.position.y,
                                                                                   battleSystem.Enemies[battleSystem.TargetIndex].transform.position.z - 5);
                                }
                                else
                                {
                                    ps.gameObject.transform.position = new Vector3(battleSystem.Enemies[battleSystem.TargetIndex].transform.position.x, ps.gameObject.transform.position.y,
                                                                                   battleSystem.Enemies[battleSystem.TargetIndex].transform.position.z);
                                }

                                StartCoroutine(WaitUntilParticleFinishes(ps));
                            }
                            else
                            {
                                if(battleSystem.UsedCard._cardTemplate.isGrounded)
                                {
                                    ps.gameObject.transform.position = new Vector3(battleSystem.Enemies[battleSystem.TargetIndex].transform.position.x, ps.transform.position.y,
                                                                                   battleSystem.Enemies[battleSystem.TargetIndex].transform.position.z - 0.5f);
                                }
                                else
                                {
                                    ps.gameObject.transform.position = new Vector3(battleSystem.Enemies[battleSystem.TargetIndex].transform.position.x, battleSystem.Enemies[battleSystem.TargetIndex].transform.position.y +
                                                                                   battleSystem.Enemies[battleSystem.TargetIndex]._EnemyStats.hitAnimationOffsetY, battleSystem.Enemies[battleSystem.TargetIndex].transform.position.z - 0.5f);
                                }

                                ActionEffect();
                            }
                        }
                        else
                        {
                            StartCoroutine(WaitUntilParticleFinishes(ps));
                        }
                    }
                    break;
                case (CardType.Magic):
                    if(battleSystem.UsedCard.IsParticleAProjectile)
                    {
                        ps.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z + 1);

                        ps.GetComponent<EpicToonFX.ETFXProjectileScript>().EnemyTarget = battleSystem.Enemies[battleSystem.TargetIndex].transform;

                        ps.GetComponent<EpicToonFX.ETFXProjectileScript>().SetDistance();
                    }
                    else
                    {
                        if(!battleSystem.HittingAllEnemies)
                        {
                            if(battleSystem._cardTemplate.isGrounded)
                            {
                                ps.gameObject.transform.position = new Vector3(battleSystem.Enemies[battleSystem.TargetIndex].transform.position.x, ps.gameObject.transform.position.y, 
                                                                               battleSystem.Enemies[battleSystem.TargetIndex].transform.position.z);
                            }
                            else
                            {
                                ps.gameObject.transform.position = new Vector3(battleSystem.Enemies[battleSystem.TargetIndex].transform.position.x, battleSystem.Enemies[battleSystem.TargetIndex].transform.position.y +
                                                                       battleSystem.Enemies[battleSystem.TargetIndex]._EnemyStats.hitAnimationOffsetY, battleSystem.Enemies[battleSystem.TargetIndex].transform.position.z);
                            }
                        }

                        StartCoroutine(WaitUntilParticleFinishes(ps));
                    }
                    break;
                case (CardType.Support):
                    if(battleSystem.UsedCard._cardTemplate.target == Target.AllEnemies)
                    {
                        StartCoroutine(WaitUntilParticleFinishes(ps));
                    }
                    else
                    {
                        if(battleSystem.UsedCard._cardTemplate.isGrounded)
                        {
                            ps.gameObject.transform.position = new Vector3(transform.position.x, ps.transform.position.y, transform.position.z);
                        }
                        else
                        {
                            ps.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
                        }

                        StartCoroutine(WaitUntilParticleFinishes(ps));
                    }
                    break;
                case (CardType.Item):
                    if(PlayerPrefs.HasKey("SoundEffects"))
                    {
                        ps.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SoundEffects");
                        ps.GetComponent<AudioSource>().Play();
                    }
                    else
                    {
                        ps.GetComponent<AudioSource>().Play();
                    }
                    if (battleSystem.UsedCard._cardTemplate.target == Target.AllEnemies)
                    {
                        if (battleSystem.Enemies.Count > 1)
                        {
                            bool startedCoroutine = false;

                            ps.gameObject.SetActive(false);

                            for (int i = 0; i < battleSystem.Enemies.Count; i++)
                            {
                                Transform trans = battleSystem.Enemies[i].transform;

                                ParticleSystem newPS = Instantiate(ps);

                                newPS.transform.position = new Vector3(trans.position.x, battleSystem.UsedCard._cardTemplate.isGrounded ? newPS.transform.position.y : 
                                                                       trans.position.y + battleSystem.Enemies[i]._EnemyStats.hitAnimationOffsetY, trans.position.z);

                                newPS.gameObject.SetActive(true);

                                newPS.gameObject.AddComponent<DestroyParticle>();
                                newPS.GetComponent<DestroyParticle>().SetDestroyTime(3f);

                                if (!startedCoroutine)
                                {
                                    StartCoroutine(WaitUntilParticleFinishes(newPS));

                                    startedCoroutine = true;
                                }
                            }
                        }
                        else
                        {
                            ps.gameObject.transform.position = new Vector3(battleSystem.Enemies[battleSystem.TargetIndex].transform.position.x, battleSystem.Enemies[battleSystem.TargetIndex].transform.position.y +
                                                                       battleSystem.Enemies[battleSystem.TargetIndex]._EnemyStats.hitAnimationOffsetY, battleSystem.Enemies[battleSystem.TargetIndex].transform.position.z);

                            StartCoroutine(WaitUntilParticleFinishes(ps));
                        }
                    }
                    else if(battleSystem.UsedCard._cardTemplate.target == Target.SingleEnemy)
                    {
                        ps.gameObject.transform.position = new Vector3(battleSystem.Enemies[battleSystem.TargetIndex].transform.position.x, battleSystem.Enemies[battleSystem.TargetIndex].transform.position.y +
                                                                       battleSystem.Enemies[battleSystem.TargetIndex]._EnemyStats.hitAnimationOffsetY, battleSystem.Enemies[battleSystem.TargetIndex].transform.position.z);

                        StartCoroutine(WaitUntilParticleFinishes(ps));
                    }
                    else
                    {
                        ps.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);

                        StartCoroutine(WaitUntilParticleFinishes(ps));
                    }
                    break;
                case (CardType.Mystic):
                    switch(battleSystem._cardTemplate.cardEffect)
                    {
                        case CardEffect.RetainCard:
                            battleSystem.RetainCards = true;
                            break;
                        case CardEffect.CutHealth:
                            foreach (BattleEnemy e in battleSystem.Enemies)
                            {
                                int healthCut = Mathf.RoundToInt(e.MaxHealth / 4);

                                if (healthCut < 1)
                                {
                                    healthCut = 1;
                                }

                                e.CurrentHealth -= healthCut;

                                e.CalculateHealth();

                                if (e.CurrentHealth <= 0)
                                {
                                    e.Dead();
                                }
                            }
                            break;
                        case CardEffect.InstantKO:
                            if(!battleSystem.Enemies[battleSystem.TargetIndex]._EnemyStats.isABoss)
                            {
                                ps.gameObject.transform.position = new Vector3(battleSystem.Enemies[battleSystem.TargetIndex].transform.position.x, ps.gameObject.transform.position.y, 
                                                                               battleSystem.Enemies[battleSystem.TargetIndex].transform.position.z);

                                battleSystem.Enemies[battleSystem.TargetIndex].Dead();
                            }
                            break;
                        case CardEffect.SkipEnemyTurn:
                            foreach (BattleEnemy e in battleSystem.Enemies)
                            {
                                e.SetAnimatorSpeed(0);
                            }
                            battleSystem.HasStoppedEnemies = true;
                            break;
                        case CardEffect.DoubleCast:
                            battleSystem.HasDoubleCast = true;
                            break;
                        case CardEffect.ZeroCardCost:
                            battleSystem.HasFreeCast = true;
                            break;
                        case CardEffect.DoubleDamage:
                            battleSystem.HasDoubleDamage = true;
                            break;
                    }
                    break;
            }
        }
        else
        {
            int cardPointHeal = 0;

            if(battleSystem.HittingAllEnemies)
            {
                for(int i = 0; i < battleSystem.Enemies.Count; i++)
                {
                    var attackParticle = Instantiate(attackParticleToUse, battleSystem.ParticlesPosition);

                    attackParticle.gameObject.SetActive(true);

                    attackParticle.Play();

                    attackParticle.transform.position = new Vector3(battleSystem.Enemies[i].transform.position.x, battleSystem.Enemies[i].transform.position.y + 
                                                                    battleSystem.Enemies[i]._EnemyStats.hitAnimationOffsetY, battleSystem.Enemies[i].transform.position.z);

                    if(MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.SoulDrain))
                    {
                        cardPointHeal = mainCharacterStats.maximumCardPoints;
                    }
                    else
                    {
                        cardPointHeal += cardPointsAttackHeal;
                    }
                }

                if(MenuController.instance._StickerPowerManager.CheckFieldStickerPower(StickerPower.BloodSucker))
                {
                    HealHealth(cardPointHeal, true);
                }
                else
                {
                    HealCardPoints(cardPointHeal, true);
                }
            }
            else
            {
                attackParticleToUse.gameObject.SetActive(true);

                attackParticleToUse.Play();

                if(!attackParticleToUse.GetComponent<AudioSource>().isPlaying)
                    attackParticleToUse.GetComponent<CardParticleAudio>().CheckAudio();

                attackParticleToUse.transform.position = new Vector3(battleSystem.Enemies[battleSystem.TargetIndex].transform.position.x, battleSystem.Enemies[battleSystem.TargetIndex].transform.position.y +
                                                                     battleSystem.Enemies[battleSystem.TargetIndex]._EnemyStats.hitAnimationOffsetY, battleSystem.Enemies[battleSystem.TargetIndex].transform.position.z);

                if(MenuController.instance._StickerPowerManager.CheckFieldStickerPower(StickerPower.BloodSucker))
                {
                    if(MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.SoulDrain))
                    {
                        HealHealth(mainCharacterStats.maximumHealth, true);
                    }
                    else
                    {
                        HealHealth(damageModifier > 1 ? cardPointsAttackHeal * 2 : cardPointsAttackHeal, true);
                    }
                }
                else
                {
                    if(MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.SoulDrain))
                    {
                        HealCardPoints(mainCharacterStats.maximumCardPoints, true);
                    }
                    else
                    {
                        HealCardPoints(damageModifier > 1 ? cardPointsAttackHeal * 2 : cardPointsAttackHeal, true);
                    }
                }
            }

            ActionEffect();
        }
    }

    public void ActionEffect()
    {
        if(battleSystem._cardTemplate != null)
        {
            switch (battleSystem._cardTemplate.cardEffect)
            {
                case (CardEffect.Damage):
                    HitEnemy();
                    break;
                case (CardEffect.HpHeal):
                    if(isUndead)
                    {
                        TakeDamage(playerStrength, true, true);
                    }
                    else
                    {
                        HealHealth(playerStrength, true);
                    }
                    break;
                case (CardEffect.CpHeal):
                    HealCardPoints(playerStrength, true);
                    break;
                case (CardEffect.RemoveStatus):
                    RemoveAllStatusEffects(true);
                    break;
                case (CardEffect.DrainHP):
                    HitEnemy();
                    HealHealth(DamageValue(), true);
                    break;
                case (CardEffect.Status):
                    break;
                default:
                    HitEnemy();
                    break;
            }

            if(battleSystem.UsedCard != null)
            {
                if(battleSystem.UsedCard.AppliesStatusEffect)
                {
                    if(battleSystem.UsedCard._cardTemplate.target == Target.Player)
                    {
                        battleSystem.UsedCard.ApplyStatusEffect(true);
                    }
                    else
                    {
                        if (battleSystem.HittingAllEnemies)
                        {
                            battleSystem.UsedCard.ApplyStatusEffect(false);
                        }
                        else
                        {
                            battleSystem.UsedCard.ApplyStatusEffect(false, battleSystem.Enemies[battleSystem.TargetIndex]);
                        }
                    }
                }
            }
        }
        else
        {
            HitEnemy();
        }
    }

    private IEnumerator WaitUntilParticleFinishes(ParticleSystem particle)
    {
        yield return new WaitForSeconds(battleSystem.UsedCard._cardTemplate.particleTime);
        ActionEffect();
        EndAction();
    }

    public void DamageGuardParticle()
    {
        guardDamageParticle.gameObject.SetActive(true);

        guardDamageParticle.Play();

        guardDamageParticle.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z + 0.4f);
    }

    private IEnumerator WaitToRecastAnimation()
    {
        yield return new WaitForSeconds(0.5f);

        if(battleSystem.Enemies.Count <= 0)
        {
            randomHitAttackCount = battleSystem.UsedCard._cardTemplate.numberOfHits;

            EndAction();
        }
        else
        {
            CastSpellAnimation();
        }
    }

    private IEnumerator WaitToCastAnimation()
    {
        yield return new WaitForSeconds(0.5f);

        if (battleSystem.Enemies.Count <= 0)
        {
            EndAction();
        }
        else
        {
            animator.Play(animatorName, -1, 0);
        }
    }

    public void CheckAction()
    {
        if(battleSystem.UsedCard._cardTemplate.cardType == CardType.Mystic)
        {
            EndAction();
        }
    }

    public void EndAction()
    {
        if(battleSystem.UsedCard != null)
        {
            if(battleSystem.UsedCard._cardTemplate.target == Target.RandomEnemy)
            {
                randomHitAttackCount++;

                if (randomHitAttackCount < battleSystem.UsedCard._cardTemplate.numberOfHits)
                {
                    StartCoroutine(WaitToRecastAnimation());

                    return;
                }
                else
                {
                    if(battleSystem.HasDoubleCast)
                    {
                        randomHitAttackCount = 0;

                        StartCoroutine(WaitToRecastAnimation());

                        battleSystem.HasDoubleCast = false;

                        return;
                    }
                }
            }
        }

        if(battleSystem.HasDoubleCast)
        {
            if(battleSystem.UsedCard != null)
            {
                if(battleSystem.UsedCard._cardTemplate.cardType != CardType.Mystic)
                {
                    if (battleSystem.HittingAllEnemies)
                    {
                        if (battleSystem.Enemies.Count > 0)
                        {
                            StartCoroutine(WaitToCastAnimation());

                            battleSystem.HasDoubleCast = false;

                            return;
                        }
                    }
                    else
                    {
                        if (battleSystem.EnemyTarget != null)
                        {
                            if (battleSystem._cardTemplate.cardName.Contains("Tri"))
                            {
                                animator.Play("FirstAttackTri", -1, 0);
                            }
                            else if (battleSystem._cardTemplate.cardName.Contains("Dual"))
                            {
                                animator.Play("FirstAttack", -1, 0);
                            }
                            else
                            {
                                AnimatorClipInfo[] animatorClipInfo = animator.GetCurrentAnimatorClipInfo(0);
                                animator.Play(animatorClipInfo[0].clip.name, -1, 0);
                            }

                            battleSystem.HasDoubleCast = false;

                            return;
                        }
                    }
                }
            }
        }

        randomHitAttackCount = 0;

        canDealExtraDamage = false;

        damageModifier = 1;

        playerStrength = MenuController.instance._StickerPowerManager.CheckStartOfBattleStickerPower(StickerPower.StrengthUp) ? (mainCharacterStats.strength - 2) + strengthBoost :
                         mainCharacterStats.strength + strengthBoost;

        playerDefense = mainCharacterStats.defense + defenseBoost;

        DisableBattleInformation();

        attackParticleToUse = basicAttackParticle;

        if (battleSystem.UsedCard != null)
        {
            battleSystem.UsedCard.SelectedCardAnimator.Play("Idle");
        }

        battleSystem.SendCardToGrave();

        if(battleSystem.UsedCard != null)
        {
            if(battleSystem.UsedCard._cardTemplate.cardType == CardType.Item)
            {
                battleSystem.UsedCard.ControllerDeselectCard();
            }
        }

        battleSystem._InputManager.SetSelectedObject(null);

        if(currentHealth > 0)
        {
            switch (battleActions)
            {
                case (BattleActions.closeUpAttack):
                    StartCoroutine("ReturnBackToDefaultPosition");
                    battleSystem._InputManager.SetSelectedObject(null);
                    break;
                case (BattleActions.rangedAttack):
                    StartCoroutine("WalkBackToDefaultPosition");
                    break;
                case (BattleActions.spell):
                    StartCoroutine("WalkBackToDefaultPosition");
                    break;
                case (BattleActions.helper):
                    SetAnimationBackToIdle();
                    if(battleSystem.Enemies.Count > 0)
                    {
                        battleSystem.EnableBattleUIInteracteability();
                        battleSystem._InputManager.SetSelectedObject(battleSystem.DefaultAttackAnimator.gameObject);
                        battleSystem.EnableCharityButton();
                    }
                    break;
                default:
                    if(!battleSystem.HasMysticFavoredPower)
                    {
                        if(battleSystem.UsedCard._cardTemplate.cardEffect == CardEffect.Damage)
                        {
                            StartCoroutine("WalkBackToDefaultPosition");
                        }
                        else
                        {
                            SetAnimationBackToIdle();

                            if (battleSystem.Enemies.Count > 0)
                            {
                                battleSystem.EnemyTurn();
                                battleSystem.ResetEnemyStatusIndex();
                                battleSystem.CheckEnemyStatus();
                            }
                        }
                    }
                    else
                    {
                        if (battleSystem.UsedCard._cardTemplate.cardEffect == CardEffect.Damage)
                        {
                            StartCoroutine("WalkBackToDefaultPosition");
                        }
                        else
                        {
                            SetAnimationBackToIdle();

                            if (battleSystem.Enemies.Count > 0)
                            {
                                battleSystem.EnableBattleUIInteracteability();
                                battleSystem._InputManager.SetSelectedObject(battleSystem.DefaultAttackAnimator.gameObject);
                                battleSystem.EnableCharityButton();
                                battleSystem._EventSystem.sendNavigationEvents = true;
                                battleSystem.NavagationDisabled = false;
                            }
                        }

                        battleSystem.HasMysticFavoredPower = false;
                    }
                    break;
            }
        }

        if(battleSystem._InputManager.ControllerPluggedIn)
        {
            battleSystem.DeselectDefaultAttackController();
        }
        else
        {
            battleSystem.DeselectDefaultAttack();
        }
        battleSystem.ResetHandObjects();

        battleSystem.HoverOverAllEnemies = false;
    }

    public void CheckDualStrikeHit()
    {
        if(battleSystem.EnemyTarget != null)
        {
            animator.Play("SecondAttack");
        }
        else
        {
            EndAction();
        }
    }

    public void CheckSecondTriAttackHit()
    {
        if (battleSystem.EnemyTarget != null)
        {
            animator.Play("SecondAttackTri");
        }
        else
        {
            EndAction();
        }
    }

    public void CheckThirdTriAttackHit()
    {
        if (battleSystem.EnemyTarget != null)
        {
            animator.Play("ThirdAttackTri");
        }
        else
        {
            EndAction();
        }
    }

    public void CheckSecondDevastationHit()
    {
        if (battleSystem.EnemyTarget != null)
        {
            if(battleSystem._cardTemplate != null)
            {
                battleSystem.UsedCard.ParticleEffect.transform.GetChild(0).gameObject.SetActive(false);
                battleSystem.UsedCard.ParticleEffect.transform.GetChild(1).gameObject.SetActive(true);
                battleSystem.UsedCard.ParticleEffect.transform.GetChild(2).gameObject.SetActive(false);
            }

            animator.Play("DevastationCombo_2");
        }
        else
        {
            EndAction();
        }
    }

    public void CheckThirdDevastationHit()
    {
        if (battleSystem.EnemyTarget != null)
        {
            if (battleSystem._cardTemplate != null)
            {
                battleSystem.UsedCard.ParticleEffect.transform.GetChild(0).gameObject.SetActive(false);
                battleSystem.UsedCard.ParticleEffect.transform.GetChild(1).gameObject.SetActive(false);
                battleSystem.UsedCard.ParticleEffect.transform.GetChild(2).gameObject.SetActive(true);
            }

            animator.Play("DevastationCombo_3");
        }
        else
        {
            EndAction();
        }
    }

    public void CheckRendingThrustHit()
    {
        if (battleSystem.EnemyTarget != null)
        {
            if (battleSystem._cardTemplate != null)
            {
                battleSystem.UsedCard.ParticleEffect.transform.GetChild(0).gameObject.SetActive(false);
                battleSystem.UsedCard.ParticleEffect.transform.GetChild(1).gameObject.SetActive(true);
            }

            animator.Play("RendingThrustCombo_2");
        }
        else
        {
            EndAction();
        }
    }

    public void ResetPlayerActions()
    {
        ResetGuardGauge();
        ResetCounterAttack();

        activatedDefenseRegen = false;

        if(defenseRegenRoutine != null)
        {
            StopCoroutine(defenseRegenRoutine);

            defenseRegenRoutine = null;
        }

        canCounter = false;
        canGuard = false;

        hasEmptyGuardGauge = false;

        isGuarding = false;

        attackParticleToUse = basicAttackParticle;

        if(!isDamaged)
           SetAnimationBackToIdle();

        SetGuardHitToFalse();
    }

    public void PlayStatusRemovalParticle(bool changeTurn)
    {
        statusEffectRemoval.gameObject.SetActive(true);
        statusEffectRemoval.Play();

        if (PlayerPrefs.HasKey("SoundEffects"))
        {
            statusEffectRemoval.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SoundEffects");
            statusEffectRemoval.GetComponent<AudioSource>().Play();
        }
        else
        {
            statusEffectRemoval.GetComponent<AudioSource>().Play();
        }

        if (changeTurn && !battleSystem.PlayersTurn && !battleSystem._battlePlayer.IsCheckingStatusEffects())
        {
            if (!battleSystem.PlayersTurn)
                battleSystem.PlayerTurn();
        }
    }

    private void RemoveAllStatusEffects(bool removeNegativeEffects)
    {
        if (statusEffectHolder.transform.childCount > 0)
        {
            if(removeNegativeEffects)
            {
                foreach (StatusEffects status in statusEffectHolder.GetComponentsInChildren<StatusEffects>())
                {
                    if(status.IsNegativeStatus)
                    {
                        status.ShouldntCheckOnDestroy = true;
                        status.RemoveEffect(true, false);
                        Destroy(status.gameObject); 
                    }
                }
            }
            else
            {
                foreach (StatusEffects status in statusEffectHolder.GetComponentsInChildren<StatusEffects>())
                {
                    status.RemoveAllEffectsPostBattle();
                }
            }
        }
    }

    private void RemoveSpecificStatusEffect(StatusEffect effect)
    {
        if (statusEffectHolder.transform.childCount > 0)
        {
            foreach (StatusEffects status in statusEffectHolder.GetComponentsInChildren<StatusEffects>())
            {
                status.RemoveSpecificEffect(effect);
            }
        }
    }

    public void PlayPerfectGuardParticle()
    {
        perfectGuardParticle.gameObject.SetActive(true);

        perfectGuardParticle.Play();

        perfectGuardParticle.GetComponent<CardParticleAudio>().CheckAudio();
    }

    public void EnableWeaponCollider()
    {
        weaponHitBox.enabled = true;
    }

    public void DisableWeaponCollider()
    {
        weaponHitBox.enabled = false;
    }

    public void SetIsStunned()
    {
        IsStunned = false;
        weaponHitBox.enabled = false;
    }

    public void CheckIfPlayerIsStunned()
    {
        if(!stunParticle.isPlaying || !stunParticle.gameObject.activeSelf)
        {
            IsStunned = false;
            animator.SetBool("IsStunned", false);
        }
    }

    public void ResetDamaged()
    {
        isDamaged = false;
    }

    public IEnumerator WaitToResetGuard()
    {
        float t = 0;

        while(t < 0.8f)
        {
            t += Time.deltaTime;

            yield return new WaitForFixedUpdate();
        }
        ResetGuard();
    }

    public void PlayJumpAudio()
    {
        if(PlayerPrefs.HasKey("SoundEffects"))
        {
            audioSource.clip = jumpAudioClip;
            audioSource.volume = PlayerPrefs.GetFloat("SoundEffects");
            audioSource.Play();
        }
        else
        {
            audioSource.clip = jumpAudioClip;
            audioSource.Play();
        }
        
    }

    public void PlayLandAudio()
    {
        if (PlayerPrefs.HasKey("SoundEffects"))
        {
            audioSource.clip = landAudioClip;
            audioSource.volume = PlayerPrefs.GetFloat("SoundEffects");
            audioSource.Play();
        }
        else
        {
            audioSource.clip = landAudioClip;
            audioSource.Play();
        }
    }

    public void FallAudio(AudioClip audioClip)
    {
        if (PlayerPrefs.HasKey("SoundEffects"))
        {
            audioSource.clip = audioClip;
            audioSource.volume = PlayerPrefs.GetFloat("SoundEffects");
            audioSource.Play();
        }
        else
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }

        AudioManager.instance.PlayBGM(AudioManager.instance.GameOverAudio);

        AudioManager.instance.BackgroundMusic.loop = false;
    }
}