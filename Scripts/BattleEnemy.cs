using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using EpicToonFX;
using Steamworks;

public class BattleEnemy : EnemyStates
{
    [SerializeField]
    private EnemyStats enemyStats;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private BattleSystem battleSystem;

    [SerializeField]
    private BattlePlayer battlePlayer;

    [SerializeField]
    private BattleResults battleResults;

    [SerializeField]
    private StatusEffectChecker statusEffectChecker;

    [SerializeField]
    private ParticleSystem basicAttackParticle, burnsParticle, deathParticle, statusEffectRemoval, hpRegenParticle, paralysisParticle, poisonParticle, freezeParticle;

    [SerializeField]
    private BoxCollider hitBox;

    [SerializeField]
    private GameObject ProjectileParticle;

    [SerializeField]
    private Material alphaMaterial;

    [SerializeField]
    private SkinnedMeshRenderer skinnedMeshRenderer;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private StatusEffects statusEffectPrefab;

    [SerializeField]
    private EnemyItemDrop enemyItemDrop;

    [SerializeField]
    private bool usesFurtherProjectile;

    private CounterAttack counterAttack;

    private GameObject enemyAttackImage, enemyHealthBar, statusEffectHolder;

    private Transform defaultPosition;

    private TextMeshProUGUI battleNumber, statusEffectText, healthText;

    private Image healthFill;

    private Quaternion rotation;

    private int currentHealth, maxHealth, enemyStrength, enemyDefense, indexedEnemy, statusEffectIndex, totalAmountOfAttacks, startingPositionIndex;

    private float experiencePoints, money, distanceToPlayer;

    private bool moving, movingTowardsPlayerPhysical, movingTowardsPlayerRanged, endedAttack, attackInterrupted, damaged, isDead, usedAcornCharm, choseAction, hasTheStatusEffect, takingTurn, didIncrementIndex,
                 tookDamageFromTripWire, checkedSkippedTurn, isFrozen;

    private Coroutine repeatActionRoutine;

    public EnemyStats _EnemyStats => enemyStats;

    private Vector3 distance;

    public int TotalAmountOfAttacks => totalAmountOfAttacks;

    public bool DidIncrementIndex
    {
        get
        {
            return didIncrementIndex;
        }
        set
        {
            didIncrementIndex = value;
        }
    }

    public bool IsFrozen
    {
        get
        {
            return isFrozen;
        }
        set
        {
            isFrozen = value;
        }
    }

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

    public int MaxHealth
    {
        get
        {
            return maxHealth;
        }
        set
        {
            maxHealth = value;
        }
    }

    public int EnemyStrength
    {
        get
        {
            return enemyStrength;
        }
        set
        {
            enemyStrength = value;
        }
    }

    public int EnemyDefense
    {
        get
        {
            return enemyDefense;
        }
        set
        {
            enemyDefense = value;
        }
    }

    public int IndexedEnemy
    {
        get
        {
            return indexedEnemy;
        }
        set
        {
            indexedEnemy = value;
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

    public int StartingPositionIndex
    {
        get
        {
            return startingPositionIndex;
        }
        set
        {
            startingPositionIndex = value;
        }
    }

    public bool CheckedSkippedTurn
    {
        get
        {
            return checkedSkippedTurn;
        }
        set
        {
            checkedSkippedTurn = value;
        }
    }

    public bool MovingTowardsPlayerPhysical
    {
        get
        {
            return movingTowardsPlayerPhysical;
        }
        set
        {
            movingTowardsPlayerPhysical = value;
        }
    }

    public bool HasTheStatusEffect
    {
        get
        {
            return hasTheStatusEffect;
        }
        set
        {
            hasTheStatusEffect = value;
        }
    }

    public bool TakingTurn
    {
        get
        {
            return takingTurn;
        }
        set
        {
            takingTurn = value;
        }
    }

    public bool AttackInterrupted
    {
        get
        {
            return attackInterrupted;
        }
        set
        {
            attackInterrupted = value;
        }
    }

    public bool Damaged
    {
        get
        {
            return damaged;
        }
        set
        {
            damaged = value;
        }
    }

    public BattleSystem _BattleSystem => battleSystem;

    public ParticleSystem BurnsParticle => burnsParticle;

    public ParticleSystem HpRegenParticle => hpRegenParticle;

    public ParticleSystem ParalysisParticle => paralysisParticle;

    public ParticleSystem PoisonParticle => poisonParticle;

    public ParticleSystem FreezeParticle => freezeParticle;

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

    public GameObject EnemyHealthBar
    {
        get
        {
            return enemyHealthBar;
        }
        set
        {
            enemyHealthBar = value;
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

    public GameObject EnemyAttackImage
    {
        get
        {
            return enemyAttackImage;
        }
        set
        {
            enemyAttackImage = value;
        }
    }

    public Transform DefaultPosition
    {
        get
        {
            return defaultPosition;
        }
        set
        {
            defaultPosition = value;
        }
    }

    public Image HealthFill
    {
        get
        {
            return healthFill;
        }
        set
        {
            healthFill = value;
        }
    }

    public Animator _Animator
    {
        get
        {
            return animator;
        }
        set
        {
            animator = value;
        }
    }

    public BoxCollider HitBox
    {
        get
        {
            return hitBox;
        }
        set
        {
            hitBox = value;
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

    public TextMeshProUGUI HealthText
    {
        get
        {
            return healthText;
        }
        set
        {
            healthText = value;
        }
    }

    private void Awake()
    {
        maxHealth = enemyStats.health;
        enemyStrength = enemyStats.strength;
        enemyDefense = enemyStats.defense;
        experiencePoints = enemyStats.exp;
        money = enemyStats.money;

        currentHealth = maxHealth;

        battleSystem.Enemies.Add(this);
    }

    private void Update()
    {
        if(!isDead)
        {
            if (moving)
            {
                Moving();
            }
            else if (endedAttack && !damaged) 
            {
                ReturnBackToPosition();
            }
        }
    }

    public void PlayHitAudio()
    {
        if(PlayerPrefs.HasKey("SoundEffects"))
        {
            audioSource.volume = PlayerPrefs.GetFloat("SoundEffects");
            audioSource.Play();
        }
        else
        {
            audioSource.Play();
        }
    }

    public void SetHealthBarTransform()
    {
        RectTransform rectTransform = enemyHealthBar.GetComponent<RectTransform>();

        if (battleSystem.Enemies.Count == 1)
        {
            if(GameManager.instance.IsBossFight)
            {
                rectTransform.anchoredPosition3D = new Vector3(-0.06300241f, 20, -11.715f);
            }
            else
            {
                rectTransform.anchoredPosition3D = new Vector3(-0.06300241f, 20.292f, -11.715f);
            }
        }
        else
        {
            switch (indexedEnemy)
            {
                case (0):
                    rectTransform.anchoredPosition3D = new Vector3(-1.414f, 20.579f, -12.722f);
                    break;
                case (1):
                    rectTransform.anchoredPosition3D = new Vector3(-0.06300241f, 20.292f, -11.715f);
                    break;
                case (2):
                    rectTransform.anchoredPosition3D = new Vector3(1.39f, 19.511f, -11.082f);
                    break;
            }
        }
    }

    public void FadeHealth()
    {
        Animator animator = enemyHealthBar.GetComponent<Animator>();

        if(animator.GetCurrentAnimatorClipInfo(0).Length > 0)
           animator.Play("EnemyHealthFade");
    }

    public void ReverseHealthFade()
    {
        if(gameObject != null)
        {
            if(currentHealth > 0)
            {
                if (animator.GetCurrentAnimatorClipInfo(0).Length > 0)
                    enemyHealthBar.GetComponent<Animator>().Play("Reverse");
            }
        }
    }

    private void Moving()
    {
        battleSystem.FadeAllEnemyHealthBars();

        hitBox.enabled = false;

        if (movingTowardsPlayerPhysical)
        {
            SetAttackImageTransform();
            SetCounterAttackTransform();

            distance = new Vector3(battleSystem.EnemyAttackPositionPhysical.position.x - transform.position.x, 0,
                                   battleSystem.EnemyAttackPositionPhysical.position.z - transform.position.z).normalized;

            if (distance != Vector3.zero)
                rotation = Quaternion.LookRotation(distance);

            distanceToPlayer = Vector3.Distance(transform.position, battleSystem.EnemyAttackPositionPhysical.position);

            if(MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.Tripwire))
            {
                if (!tookDamageFromTripWire)
                {
                    if (distanceToPlayer > 3.5f)
                    {
                        transform.position += distance * enemyStats.moveSpeed * Time.deltaTime;

                        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5 * Time.deltaTime);
                    }
                    else
                    {
                        battleSystem._StickerPowerHolder.CreateStickerMessage(StickerPower.Tripwire);

                        totalAmountOfAttacks = 0;

                        statusEffectChecker._EnemyActions[ActionIndex].AttackIndex = statusEffectChecker._EnemyActions[ActionIndex].AdditionalAttacks;

                        TextMeshProUGUI attackText = enemyAttackImage.GetComponentInChildren<TextMeshProUGUI>();

                        attackText.text = "<size=0.3>x </size>" + totalAmountOfAttacks;

                        int percentage = Mathf.RoundToInt(maxHealth * 0.2f);

                        TakeDamage(percentage, true, true, true);

                        SetBattleNumberPosition(new Vector3(transform.position.x + 1.5f, transform.position.y, transform.position.z));
                        SetBattleNumberRotation(Quaternion.Euler(15.042f, -51.49f, -7f));

                        basicAttackParticle.gameObject.SetActive(true);
                        basicAttackParticle.Play();
                        basicAttackParticle.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + enemyStats.hitAnimationOffsetY, transform.position.z);

                        AudioManager.instance.PlaySoundEffect(AudioManager.instance.BasicSwordAudio);

                        attackInterrupted = true;

                        moving = false;

                        tookDamageFromTripWire = true;
                    }
                }
                else
                {
                    if (distanceToPlayer > enemyStats.distanceBetweenPlayerAndSelf)
                    {
                        transform.position += distance * enemyStats.moveSpeed * Time.deltaTime;

                        if (distanceToPlayer > 2.5f)
                        {
                            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5 * Time.deltaTime);
                        }
                        else
                        {
                            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 180, 0), 5 * Time.deltaTime);
                        }
                    }
                    else
                    {
                        transform.position = new Vector3(battleSystem.EnemyAttackPositionPhysical.position.x, transform.position.y, battleSystem.EnemyAttackPositionPhysical.position.z);

                        animator.Play(statusEffectChecker._EnemyActions[ActionIndex].AnimatorName);

                        SetBattleNumberPosition(new Vector3(transform.position.x + 1.5f, transform.position.y, transform.position.z));
                        SetBattleNumberRotation(Quaternion.Euler(15.042f, -51.49f, -7f));

                        damaged = false;

                        hitBox.enabled = true;

                        moving = false;
                    }
                }
            }
            else
            {
                if (distanceToPlayer > enemyStats.distanceBetweenPlayerAndSelf)
                {
                    transform.position += distance * enemyStats.moveSpeed * Time.deltaTime;

                    if (distanceToPlayer > 2.5f)
                    {
                        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5 * Time.deltaTime);
                    }
                    else
                    {
                        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 180, 0), 5 * Time.deltaTime);
                    }
                }
                else
                {
                    transform.position = new Vector3(battleSystem.EnemyAttackPositionPhysical.position.x, transform.position.y, battleSystem.EnemyAttackPositionPhysical.position.z);

                    animator.Play(statusEffectChecker._EnemyActions[ActionIndex].AnimatorName);

                    SetBattleNumberPosition(new Vector3(transform.position.x + 1.5f, transform.position.y, transform.position.z));
                    SetBattleNumberRotation(Quaternion.Euler(15.042f, -51.49f, -7f));

                    damaged = false;

                    hitBox.enabled = true;

                    moving = false;
                }
            }
        }
        else if(movingTowardsPlayerRanged)
        {
            SetAttackImageTransform();

            distance = new Vector3(battleSystem.EnemyAttackPositionRanged.position.x - transform.position.x, 0,
                                   battleSystem.EnemyAttackPositionRanged.position.z - transform.position.z).normalized;

            if (distance != Vector3.zero)
                rotation = Quaternion.LookRotation(distance);

            distanceToPlayer = Vector3.Distance(transform.position, battleSystem.EnemyAttackPositionRanged.position);

            if (MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.Tripwire))
            {
                if(!tookDamageFromTripWire)
                {
                    if (distanceToPlayer > 3.5f)
                    {
                        transform.position += distance * enemyStats.moveSpeed * Time.deltaTime;

                        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5 * Time.deltaTime);
                    }
                    else
                    {
                        battleSystem._StickerPowerHolder.CreateStickerMessage(StickerPower.Tripwire);

                        totalAmountOfAttacks = 0;

                        statusEffectChecker._EnemyActions[ActionIndex].AttackIndex = statusEffectChecker._EnemyActions[ActionIndex].AdditionalAttacks;

                        TextMeshProUGUI attackText = enemyAttackImage.GetComponentInChildren<TextMeshProUGUI>();

                        attackText.text = "<size=0.3>x </size>" + totalAmountOfAttacks;

                        int percentage = Mathf.RoundToInt(maxHealth * 0.2f);

                        TakeDamage(percentage, true, true, true);

                        SetBattleNumberPosition(new Vector3(transform.position.x + 1.5f, transform.position.y, transform.position.z));
                        SetBattleNumberRotation(Quaternion.Euler(15.042f, -51.49f, -7f));

                        basicAttackParticle.gameObject.SetActive(true);
                        basicAttackParticle.Play();
                        basicAttackParticle.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + enemyStats.hitAnimationOffsetY, transform.position.z);

                        AudioManager.instance.PlaySoundEffect(AudioManager.instance.BasicSwordAudio);

                        attackInterrupted = true;

                        moving = false;

                        tookDamageFromTripWire = true;
                    }
                }
                else
                {
                    if (distanceToPlayer > enemyStats.distanceToPlayerRanged)
                    {
                        transform.position += distance * enemyStats.moveSpeed * Time.deltaTime;

                        if (distanceToPlayer > 2.5f)
                        {
                            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5 * Time.deltaTime);
                        }
                        else
                        {
                            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 180, 0), 5 * Time.deltaTime);
                        }
                    }
                    else
                    {
                        if (!usesFurtherProjectile)
                        {
                            transform.position = new Vector3(battleSystem.EnemyAttackPositionRanged.position.x, transform.position.y, battleSystem.EnemyAttackPositionRanged.position.z);
                        }
                        else
                        {
                            transform.position = new Vector3(battleSystem.EnemyAttackPositionRanged.position.x, transform.position.y,
                                                         battleSystem.EnemyAttackPositionRanged.position.z + enemyStats.distanceToPlayerRanged);
                        }

                        animator.Play(statusEffectChecker._EnemyActions[ActionIndex].AnimatorName);

                        SetBattleNumberPosition(new Vector3(transform.position.x + 1.5f, transform.position.y, transform.position.z));
                        SetBattleNumberRotation(Quaternion.Euler(15.042f, -51.49f, -7f));

                        damaged = false;

                        hitBox.enabled = true;

                        moving = false;
                    }
                }
            }
            else
            {
                if (distanceToPlayer > enemyStats.distanceToPlayerRanged)
                {
                    transform.position += distance * enemyStats.moveSpeed * Time.deltaTime;

                    if (distanceToPlayer > 2.5f)
                    {
                        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5 * Time.deltaTime);
                    }
                    else
                    {
                        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 180, 0), 5 * Time.deltaTime);
                    }
                }
                else
                {
                    if (!usesFurtherProjectile)
                    {
                        transform.position = new Vector3(battleSystem.EnemyAttackPositionRanged.position.x, transform.position.y, battleSystem.EnemyAttackPositionRanged.position.z);
                    }
                    else
                    {
                        transform.position = new Vector3(battleSystem.EnemyAttackPositionRanged.position.x, transform.position.y,
                                                     battleSystem.EnemyAttackPositionRanged.position.z + enemyStats.distanceToPlayerRanged);
                    }

                    animator.Play(statusEffectChecker._EnemyActions[ActionIndex].AnimatorName);

                    SetBattleNumberPosition(new Vector3(transform.position.x + 1.5f, transform.position.y, transform.position.z));
                    SetBattleNumberRotation(Quaternion.Euler(15.042f, -51.49f, -7f));

                    damaged = false;

                    hitBox.enabled = true;

                    moving = false;
                }
            }
        }
        else
        {
            distance = new Vector3(battleSystem.EnemyAttackPositionPhysical.position.x - transform.position.x, 0,
                                   battleSystem.EnemyAttackPositionPhysical.position.z - transform.position.z).normalized;

            distanceToPlayer = Vector3.Distance(transform.position, battleSystem.EnemyAttackPositionPhysical.position);

            if (distanceToPlayer > enemyStats.distanceBetweenPlayerAndSelf * 20)
            {
                transform.position += distance * enemyStats.moveSpeed * Time.deltaTime;
            }
            else
            {
                animator.Play(statusEffectChecker._EnemyActions[ActionIndex].AnimatorName);

                damaged = false;

                moving = false;
            }
        }
    }

    private void ReturnBackToPosition()
    {
        battlePlayer.HasBlindRage = false;

        Vector3 distance = new Vector3(transform.position.x - defaultPosition.position.x, 0,
                                       transform.position.z - defaultPosition.position.z).normalized;

        float distanceToDefaultPosition = Vector3.Distance(transform.position, defaultPosition.position);

        if (statusEffectChecker._EnemyActions[ActionIndex]._EnemyActions == EnemyActions.RangedAction)
        {
            if (distanceToDefaultPosition < 4.55f)
            {
                enemyAttackImage.SetActive(false);

                battleSystem.BattleInformationAnimator.Play("Reverse");

                if (battlePlayer.CanGuard)
                {
                    if (battlePlayer.GuardGaugeAnimator.GetCurrentAnimatorClipInfo(0).Length > 0)
                        battlePlayer.GuardGaugeAnimator.Play("Reverse");
                }
                if (battlePlayer.CanCounter)
                {
                    counterAttack.StopNotch();
                }

                if (battleSystem.CounterMeasurePromptAnimator.GetCurrentAnimatorClipInfo(0).Length > 0)
                    battleSystem.CounterMeasurePromptAnimator.Play("Reverse");

                battlePlayer.CanGuard = false;
                battlePlayer.CanCounter = false;
            }
        }
        else
        {
            enemyAttackImage.SetActive(false);

            battleSystem.BattleInformationAnimator.Play("Reverse");

            if (statusEffectChecker._EnemyActions[ActionIndex]._EnemyActions != EnemyActions.SupportAction && statusEffectChecker._EnemyActions[ActionIndex]._EnemyActions != EnemyActions.HealAction)
            {
                if (battlePlayer.CanGuard)
                {
                    if (battlePlayer.GuardGaugeAnimator.GetCurrentAnimatorClipInfo(0).Length > 0)
                        battlePlayer.GuardGaugeAnimator.Play("Reverse");
                }
                if (battlePlayer.CanCounter)
                {
                    counterAttack.StopNotch();
                }

                if (battleSystem.CounterMeasurePromptAnimator.GetCurrentAnimatorClipInfo(0).Length > 0)
                    battleSystem.CounterMeasurePromptAnimator.Play("Reverse");

                battlePlayer.CanGuard = false;
                battlePlayer.CanCounter = false;
            }
        }

        if (distanceToDefaultPosition <= enemyStats.distanceBackToDefaultPosition)
        {
            transform.position = new Vector3(defaultPosition.position.x, enemyStats.defaultBattlePositionY, defaultPosition.position.z);

            transform.rotation = defaultPosition.rotation;

            animator.Play("Idle");

            SetBattleNumberPosition(new Vector3(transform.position.x - 1, transform.position.y, transform.position.z));
            SetBattleNumberRotation(Quaternion.Euler(0, -30, 0));

            SetCounterAttackTransform();

            hitBox.enabled = true;

            EndedTurn(true, true);
        }
        else
        {
            transform.position -= distance * enemyStats.moveSpeed * Time.deltaTime;

            if (repeatActionRoutine != null)
            {
                StopCoroutine(repeatActionRoutine);
                repeatActionRoutine = null;
            }  

            MoveAnimation();
        }
    }

    public void MoveAnimation()
    {
        animator.Play("Move");
    }

    public void HitPlayer()
    {
        int damageValue = enemyStrength + statusEffectChecker._EnemyActions[ActionIndex].ActionStrength;

        if(battlePlayer.CurrentHealth > 0)
        {
            if(MenuController.instance._StickerPowerManager.CheckStartOfBattleStickerPower(StickerPower.ShadowForm))
            {
                if (_enemyAction[ActionIndex]._EnemyActions == EnemyActions.PhysicalAction)
                {
                    DamagePlayer(0);
                }
                else
                {
                    DamagePlayer(damageValue);
                }
            }
            else
            {
                DamagePlayer(damageValue);
            }
        }
    }

    public int GetActionStrength()
    {
        return statusEffectChecker._EnemyActions[ActionIndex].ActionStrength;
    }

    public void TakeDamage(int value, bool ignoreDefense, bool playDamageAnimation, bool decrementAttacks)
    {
        if(battleSystem._cardTemplate != null && !MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.SoulDrain))
        {
            if (MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.GraveTender))
            {
                Graveyard grave = battleSystem.GraveyardPosition.GetComponent<Graveyard>();

                float percentage = grave.cards.Count / 100;

                int damageBoost = Mathf.RoundToInt(percentage * value);

                if (grave.cards.Count > 0 && damageBoost < 1)
                {
                    damageBoost = 1;
                }

                value += damageBoost;
            }
        }

        if (!ignoreDefense)
        {
            value = value - enemyDefense;
        }

        if (decrementAttacks && value > 0)
        {
            DecrementTotalAmountOfAttacks();
            IncrementAttackIndex();
        }

        if(value > 0)
        {
            if(playDamageAnimation)
               animator.Play("TakeDamage", -1, 0);

            if(decrementAttacks || _enemyAction[ActionIndex]._EnemyActions == EnemyActions.RangedAction)
            {
                damaged = true;
            } 
        }
        else
        {
            value = 1;

            DecrementTotalAmountOfAttacks();
            IncrementAttackIndex();

            if (playDamageAnimation)
                animator.Play("TakeDamage", -1, 0);

            if (decrementAttacks || _enemyAction[ActionIndex]._EnemyActions == EnemyActions.RangedAction)
            {
                damaged = true;
            }
        }

        if(battleSystem.HasDoubleDamage)
        {
            value *= 2;
        }

        BattleNumber _battleNumber = battleNumber.GetComponent<BattleNumber>();

        _battleNumber.BattleNumberText.text = value.ToString();
        _battleNumber.ApplyDamageColor();

        currentHealth -= Mathf.Abs(value);

        if (currentHealth < 0)
            currentHealth = 0;

        CalculateHealth();

        if(currentHealth <= MaxHealth / 4)
        {
            enemyHealthBar.GetComponent<HealthAnimation>()._animator.Play("LowHealth");
        }
        else
        {
            enemyHealthBar.GetComponent<HealthAnimation>()._animator.Play("Idle");
        }

        if (currentHealth <= 0)
        {
            Dead();
        }
    }

    public void SetAnimatorSpeed(float value)
    {
        animator.speed = value;
    }

    public void CalculateHealth()
    {
        healthText.text = currentHealth + "/" + maxHealth;

        healthFill.fillAmount = currentHealth / (float)maxHealth;
    }

    public void HealHealth(int value, bool showNumber)
    {
        currentHealth += value;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        CalculateHealth();

        if (currentHealth <= MaxHealth / 4)
        {
            enemyHealthBar.GetComponent<HealthAnimation>()._animator.Play("LowHealth");
        }
        else
        {
            enemyHealthBar.GetComponent<HealthAnimation>()._animator.Play("Idle");
        }

        if (showNumber)
        {
            BattleNumber _battleNumber = battleNumber.GetComponent<BattleNumber>();

            _battleNumber.BattleNumberText.text = value.ToString();
            _battleNumber.ApplyHpHealColor();
        }
    }

    private void RestoreHealAcion(int value, bool multiTarget)
    {
        if(multiTarget)
        {
            battleSystem._ParticleEffectManager.SpawnParticle(statusEffectChecker._EnemyActions[ActionIndex].ActionName, new Vector3(transform.position.x, transform.position.y + 0.7f, transform.position.z), true);

            for (int i = 0; i < battleSystem.Enemies.Count; i++)
            {
                battleSystem.Enemies[i].currentHealth += value;

                if (battleSystem.Enemies[i].currentHealth > battleSystem.Enemies[i].maxHealth)
                {
                    battleSystem.Enemies[i].currentHealth = battleSystem.Enemies[i].maxHealth;
                }

                battleSystem.Enemies[i].healthText.text = battleSystem.Enemies[i].currentHealth + "/" + battleSystem.Enemies[i].maxHealth;

                battleSystem.Enemies[i].healthFill.fillAmount = battleSystem.Enemies[i].currentHealth / (float)battleSystem.Enemies[i].maxHealth;

                if (battleSystem.Enemies[i].currentHealth <= battleSystem.Enemies[i].MaxHealth / 4)
                {
                    battleSystem.Enemies[i].enemyHealthBar.GetComponent<HealthAnimation>()._animator.Play("LowHealth");
                }
                else
                {
                    battleSystem.Enemies[i].enemyHealthBar.GetComponent<HealthAnimation>()._animator.Play("Idle");
                }

                BattleNumber _battleNumber = battleSystem.Enemies[i].battleNumber.GetComponent<BattleNumber>();

                _battleNumber.BattleNumberText.text = value.ToString();
                _battleNumber.ApplyHpHealColor();
            }
        }
        else
        {
            int tempIndex = statusEffectChecker.EnemyIndex;

            if(tempIndex > battleSystem.Enemies.Count)
            {
                tempIndex = 0;

                statusEffectChecker.EnemyIndex = tempIndex;
            }

            battleSystem._ParticleEffectManager.SpawnParticle(statusEffectChecker._EnemyActions[ActionIndex].ActionName, 
                                                              new Vector3(battleSystem.Enemies[statusEffectChecker.EnemyIndex].transform.position.x, 
                                                              battleSystem.Enemies[statusEffectChecker.EnemyIndex].transform.position.y + 0.7f,
                                                              battleSystem.Enemies[statusEffectChecker.EnemyIndex].transform.position.z), false);

            battleSystem.Enemies[statusEffectChecker.EnemyIndex].currentHealth += value;

            if (battleSystem.Enemies[statusEffectChecker.EnemyIndex].currentHealth > battleSystem.Enemies[statusEffectChecker.EnemyIndex].maxHealth)
            {
                battleSystem.Enemies[statusEffectChecker.EnemyIndex].currentHealth = battleSystem.Enemies[statusEffectChecker.EnemyIndex].maxHealth;
            }

            battleSystem.Enemies[statusEffectChecker.EnemyIndex].healthText.text = battleSystem.Enemies[statusEffectChecker.EnemyIndex].currentHealth + "/" + battleSystem.Enemies[statusEffectChecker.EnemyIndex].maxHealth;

            battleSystem.Enemies[statusEffectChecker.EnemyIndex].healthFill.fillAmount = battleSystem.Enemies[statusEffectChecker.EnemyIndex].currentHealth / (float)battleSystem.Enemies[statusEffectChecker.EnemyIndex].maxHealth;

            if (battleSystem.Enemies[statusEffectChecker.EnemyIndex].currentHealth <= battleSystem.Enemies[statusEffectChecker.EnemyIndex].MaxHealth / 4)
            {
                battleSystem.Enemies[statusEffectChecker.EnemyIndex].enemyHealthBar.GetComponent<HealthAnimation>()._animator.Play("LowHealth");
            }
            else
            {
                battleSystem.Enemies[statusEffectChecker.EnemyIndex].enemyHealthBar.GetComponent<HealthAnimation>()._animator.Play("Idle");
            }

            BattleNumber _battleNumber = battleSystem.Enemies[statusEffectChecker.EnemyIndex].battleNumber.GetComponent<BattleNumber>();

            _battleNumber.BattleNumberText.text = value.ToString();
            _battleNumber.ApplyHpHealColor();
        }
    }

    private void SetBattleNumberPosition(Vector3 position)
    {
        battleNumber.transform.position = position;
    }

    private void SetBattleNumberRotation(Quaternion rotation)
    {
        battleNumber.transform.rotation = rotation;
    }

    public void Dead()
    {
        SetAnimatorSpeed(1);

        isDead = true;

        hitBox.gameObject.SetActive(false);

        enemyAttackImage.SetActive(false);

        counterAttack.gameObject.SetActive(false);

        animator.SetTrigger("Dead");

        if (battleSystem.EnemyTarget != null)
            battleSystem.EnemyTarget = null;

        if(repeatActionRoutine != null)
        {
            StopCoroutine(repeatActionRoutine);
            repeatActionRoutine = null;
        }

        FadeHealth();

        if(battleSystem.BattleInformationAnimator.GetCurrentAnimatorClipInfo(0).Length > 0)
        {
            battleSystem.BattleInformationAnimator.Play("Reverse");
        }
        if(battlePlayer.GuardGaugeAnimator.GetCurrentAnimatorClipInfo(0).Length > 0)
        {
            battlePlayer.GuardGaugeAnimator.Play("Reverse");
        }
        if(battleSystem.CounterMeasurePromptAnimator.GetCurrentAnimatorClipInfo(0).Length > 0)
        {
            battleSystem.CounterMeasurePromptAnimator.Play("Reverse");
        }

        if(statusEffectHolder.transform.childCount > 0)
        {
            foreach (StatusEffects status in statusEffectHolder.GetComponentsInChildren<StatusEffects>())
            {
                status.RemoveAllEffectsPostBattle();
            }
        }

        battlePlayer.CanGuard = false;
        battlePlayer.CanCounter = false;

        if(!GameManager.instance.IsFinalBossFight)
        {
            if (battlePlayer._mainCharacterStats.level < battlePlayer._mainCharacterStats.maximumLevel)
            {
                if (battlePlayer._mainCharacterStats.level > enemyStats.level)
                {
                    if (enemyStats.exp > 0)
                        ReduceExperience();
                }
                else if (battlePlayer._mainCharacterStats.level < enemyStats.level)
                {
                    if (enemyStats.exp > 0)
                        IncreaseExperience();
                }
                else
                {
                    battleResults.GainedExperienceToReduce += enemyStats.exp;
                }
            }

            IncreaseMoney();

            enemyItemDrop.DropItem();

            GameManager.instance._BestiaryMenu.Enemies.Add(enemyStats);
            GameManager.instance._BestiaryMenu.CompareEnemies(enemyStats);
        }

        if(MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.HpSteal))
        {
            battleSystem._StickerPowerHolder.CreateStickerMessage(StickerPower.HpSteal);

            battleSystem._ParticleEffectManager.PlayParticle(battleSystem._ParticleEffectManager.HpParticle, new Vector3(battlePlayer.transform.position.x, battlePlayer.transform.position.y, battlePlayer.transform.position.z));

            float percentage = Mathf.Round(battlePlayer.MaxHealth * 0.10f);

            battlePlayer.HealHealth((int)percentage, false);
        }
    }

    private void IncreaseExperience()
    {
        int levelDifference = enemyStats.level - battlePlayer._mainCharacterStats.level;

        float levelModfication = levelDifference * 0.10f;

        if (levelModfication > 0.8f)
        {
            levelModfication = 0.8f;
        }

        float expPoints = enemyStats.exp;

        float incrementExperience = Mathf.Round(expPoints * levelModfication);

        if (incrementExperience < 1)
        {
            incrementExperience = 1;
        }

        float earnedEXP = expPoints + incrementExperience;

        battleResults.GainedExperienceToReduce += earnedEXP;
    }

    private void ReduceExperience()
    {
        int levelDifference = battlePlayer._mainCharacterStats.level - enemyStats.level;

        float levelModfication = levelDifference * 0.10f;

        if(levelModfication > 0.8f)
        {
            levelModfication = 0.8f;
        }

        float expPoints = enemyStats.exp;

        float diminishingReturns = Mathf.Round(expPoints * levelModfication);

        if(diminishingReturns < 1)
        {
            diminishingReturns = 1;
        }

        float earnedEXP = expPoints - diminishingReturns;

        battleResults.GainedExperienceToReduce += earnedEXP;
    }

    private void IncreaseMoney()
    {
        battleResults.GainedMoneyToReduce += money;
    }

    public void RemoveEnemyFromList()
    {
        battleSystem.Enemies.Remove(this);

        battleSystem.SetEnemyButtonNavigations();
    }

    public void ControllerSelectTarget()
    {
        if(InputManager.instance.ControllerPluggedIn)
        {
            battleSystem.TargetIndex = indexedEnemy;

            if(battleSystem.UsedCard != null)
            {
                if (!battleSystem.HittingAllEnemies && battleSystem.UsedCard._cardTemplate.target != Target.RandomEnemy)
                    battleSystem.ChooseTarget(gameObject);
            }
            else
            {
                if (!battleSystem.HittingAllEnemies)
                    battleSystem.ChooseTarget(gameObject);
            }

            battleSystem.ConfirmedTarget = true;

            AudioManager.instance.PlaySoundEffect(AudioManager.instance.CursorAudio);
        }
    }

    public void ControllerDeSelectTarget()
    {
        if (InputManager.instance.ControllerPluggedIn)
        {
            battleSystem.ConfirmedTarget = false;
        }
    }

    public void FadeOutMaterial()
    {
        battleSystem.SetEnemyIndex();

        if (this.movingTowardsPlayerPhysical || this.movingTowardsPlayerRanged)
        {
            battleSystem.EnemyTurn();
            battleSystem.EnemyCheckStatus();

            battlePlayer.ResetGuard();
        }

        var deathPar = Instantiate(deathParticle);

        deathPar.gameObject.SetActive(true);

        deathPar.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + enemyStats.deathParticleYOffSet, transform.position.z);

        deathPar.Play();

        if(PlayerPrefs.HasKey("SoundEffects"))
        {
            deathPar.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SoundEffects");
            deathPar.GetComponent<AudioSource>().Play();
        }
        else
        {
            deathPar.GetComponent<AudioSource>().Play();
        }

        skinnedMeshRenderer.material = alphaMaterial;
        skinnedMeshRenderer.receiveShadows = false;
        skinnedMeshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        StartCoroutine("BeginFadeOut");
    }

    public void FadeOutFinalBoss()
    {
        var deathPar = Instantiate(deathParticle);

        deathPar.gameObject.SetActive(true);

        deathPar.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + enemyStats.deathParticleYOffSet, transform.position.z);

        deathPar.Play();

        if (PlayerPrefs.HasKey("SoundEffects"))
        {
            deathPar.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SoundEffects");
            deathPar.GetComponent<AudioSource>().Play();
        }
        else
        {
            deathPar.GetComponent<AudioSource>().Play();
        }

        skinnedMeshRenderer.material = alphaMaterial;
        skinnedMeshRenderer.receiveShadows = false;
        skinnedMeshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        StartCoroutine("BeginFadeFinalBoss");
    }

    private IEnumerator BeginFadeFinalBoss()
    {
        float alpha = skinnedMeshRenderer.material.color.a;

        while (alpha > 0)
        {
            alpha -= Time.deltaTime;

            skinnedMeshRenderer.material.color = new Color(skinnedMeshRenderer.material.color.a, skinnedMeshRenderer.material.color.g, skinnedMeshRenderer.material.color.g, alpha);

            yield return new WaitForFixedUpdate();
        }
        alpha = 0;

        skinnedMeshRenderer.enabled = false;

        GetComponent<SpawnNewEnemy>().SpawnEnemy();
    }

    public void StartEnemyTurn()
    {
        battleSystem.SetEnemyIndex();

        battleSystem.EnemyTurn();
        battleSystem.EnemyCheckStatus();

        if(!battlePlayer.StunParticle.isPlaying)
            battlePlayer.ResetGuard();
    }

    private IEnumerator BeginFadeOut()
    {
        float alpha = skinnedMeshRenderer.material.color.a;

        while(alpha > 0)
        {
            alpha -= Time.deltaTime;

            skinnedMeshRenderer.material.color = new Color(skinnedMeshRenderer.material.color.a, skinnedMeshRenderer.material.color.g, skinnedMeshRenderer.material.color.g, alpha);

            yield return new WaitForFixedUpdate();
        }
        alpha = 0;

        skinnedMeshRenderer.enabled = false;

        CheckIfPlayerWon();

        gameObject.SetActive(false);
    }

    public void CheckIfPlayerWon()
    {
        battleSystem.CheckIfBattleIsWon();
    }

    public void DamagePlayer(float value)
    {
        if (battlePlayer.CurrentHealth <= 0) return;

        if(battlePlayer.HasInvulnerable)
        {
            float dmg = 0;

            ApplyStatusEffect();

            basicAttackParticle.gameObject.SetActive(true);

            basicAttackParticle.Play();

            basicAttackParticle.gameObject.transform.position = new Vector3(battlePlayer.transform.position.x, battlePlayer.transform.position.y + 0.5f, battlePlayer.transform.position.z);

            BattleNumber _battleNumber = battlePlayer.BattleNumber.GetComponent<BattleNumber>();

            _battleNumber.transform.position = new Vector3(battlePlayer.transform.position.x - 0.6f, battlePlayer.transform.position.y + 2, battlePlayer.transform.position.z - 0.6f);

            _battleNumber.BattleNumberText.text = dmg.ToString();
            _battleNumber.ApplyDamageColor();

            if (MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.AcornCharm))
            {
                if (battlePlayer.CurrentHealth <= battlePlayer._mainCharacterStats.maximumHealth / 2 && statusEffectChecker._EnemyActions[ActionIndex].AdditionalAttacks > 0)
                {
                    if (!usedAcornCharm)
                    {
                        if (totalAmountOfAttacks > 0)
                        {
                            battleSystem._StickerPowerHolder.CreateStickerMessage(StickerPower.AcornCharm);

                            TextMeshProUGUI attackText = enemyAttackImage.GetComponentInChildren<TextMeshProUGUI>();

                            ParticleSystem attackReductionParticle = enemyAttackImage.GetComponentInChildren<ParticleSystem>();

                            attackReductionParticle.Play();

                            if (totalAmountOfAttacks <= 0)
                            {
                                attackText.text = "<size=0.3>x </size>0";
                            }
                            else
                            {
                                totalAmountOfAttacks--;

                                attackText.text = "<size=0.3>x </size>" + totalAmountOfAttacks;
                            }

                            usedAcornCharm = true;
                        }
                    }
                }
            }
        }
        else
        {
            if (MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.DeathsDefense))
            {
                Graveyard grave = battleSystem.GraveyardPosition.GetComponent<Graveyard>();

                float deathsDefense = grave.cards.Count / 100;

                int damageReduction = Mathf.RoundToInt(value * deathsDefense);

                if (grave.cards.Count > 0 && damageReduction < 1)
                {
                    damageReduction = 1;
                }

                value -= Mathf.Abs(damageReduction);
            }

            float dmg = value;

            if (battlePlayer.IsGuarding)
            {
                if (battlePlayer.IsPerfectBlocking)
                {
                    battlePlayer.PlayPerfectGuardParticle();

                    dmg = 0;
                }
                else
                {
                    dmg = Mathf.RoundToInt((value - battlePlayer.PlayerDefense) / 2);

                    if (dmg <= 0)
                    {
                        dmg = 0;
                    }
                    else
                    {
                        if (MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.CardGuard))
                        {
                            float percentage = Mathf.RoundToInt(battlePlayer.MaxHealth * 0.40f);

                            if (battlePlayer.CurrentHealth <= percentage)
                            {
                                if (battlePlayer.CurrentCardPoints > 0)
                                {
                                    battlePlayer.CalculateCardPoints(-(int)dmg);
                                }
                                else
                                {
                                    battlePlayer.CurrentHealth -= (int)dmg;
                                    battlePlayer._mainCharacterStats.currentPlayerHealth = battlePlayer.CurrentHealth;
                                    battlePlayer.CalculateHealth();
                                }
                            }
                            else
                            {
                                battlePlayer.CurrentHealth -= (int)dmg;
                                battlePlayer._mainCharacterStats.currentPlayerHealth = battlePlayer.CurrentHealth;
                                battlePlayer.CalculateHealth();
                            }
                        }
                        else
                        {
                            battlePlayer.CurrentHealth -= (int)dmg;
                            battlePlayer._mainCharacterStats.currentPlayerHealth = battlePlayer.CurrentHealth;
                            battlePlayer.CalculateHealth();
                        }

                        battlePlayer._Animator.SetBool("GuardHit", true);

                        battleSystem.DidPlayerTakeDamage = true;
                    }

                    battlePlayer.DamageGuardParticle();
                }
            }
            else
            {
                dmg -= battlePlayer.PlayerDefense;

                if (dmg <= 0)
                {
                    dmg = 0;
                }
                else
                {
                    battlePlayer.IsStunned = true;
                    battlePlayer._Animator.Play("GetHit", -1, 0);

                    if (MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.CardGuard))
                    {
                        float percentage = Mathf.RoundToInt(battlePlayer.MaxHealth * 0.40f);

                        if (battlePlayer.CurrentHealth <= percentage)
                        {
                            if (battlePlayer.CurrentCardPoints > 0)
                            {
                                battlePlayer.CalculateCardPoints(-(int)dmg);
                                battlePlayer._mainCharacterStats.currentPlayerCardPoints = battlePlayer.CurrentCardPoints;
                            }
                            else
                            {
                                battlePlayer.CurrentHealth -= (int)dmg;
                                battlePlayer._mainCharacterStats.currentPlayerHealth = battlePlayer.CurrentHealth;
                                battlePlayer.CalculateHealth();
                            }
                        }
                        else
                        {
                            battlePlayer.CurrentHealth -= (int)dmg;
                            battlePlayer._mainCharacterStats.currentPlayerHealth = battlePlayer.CurrentHealth;
                            battlePlayer.CalculateHealth();
                        }
                    }
                    else
                    {
                        battlePlayer.CurrentHealth -= (int)dmg;
                        battlePlayer._mainCharacterStats.currentPlayerHealth = battlePlayer.CurrentHealth;
                        battlePlayer.CalculateHealth();
                    }

                    battleSystem.DidPlayerTakeDamage = true;
                }

                ApplyStatusEffect();

                basicAttackParticle.gameObject.SetActive(true);

                basicAttackParticle.Play();

                basicAttackParticle.gameObject.transform.position = new Vector3(battlePlayer.transform.position.x, battlePlayer.transform.position.y + 0.5f, battlePlayer.transform.position.z);

                if (battlePlayer.HasThorns)
                {
                    if (dmg > 0)
                    {
                        if (battlePlayer.CurrentHealth > 0)
                        {
                            if (_enemyAction[ActionIndex]._EnemyActions == EnemyActions.PhysicalAction)
                            {
                                float thorns = Mathf.RoundToInt(dmg / 2);

                                dmg = (int)thorns;

                                if (dmg < 1)
                                {
                                    dmg = 1;
                                }

                                TakeDamage((int)dmg, true, false, false);

                                battleSystem._StickerPowerHolder.CreateStickerMessage(StickerPower.Thorns);
                            }
                        }
                    }
                }
            }

            BattleNumber _battleNumber = battlePlayer.BattleNumber.GetComponent<BattleNumber>();

            _battleNumber.transform.position = new Vector3(battlePlayer.transform.position.x - 0.6f, battlePlayer.transform.position.y + 2, battlePlayer.transform.position.z - 0.6f);

            _battleNumber.BattleNumberText.text = dmg.ToString();
            _battleNumber.ApplyDamageColor();

            battlePlayer.WeaponHitBox.enabled = false;

            if (MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.AcornCharm))
            {
                if (battlePlayer.CurrentHealth <= battlePlayer._mainCharacterStats.maximumHealth / 2 && statusEffectChecker._EnemyActions[ActionIndex].AdditionalAttacks > 0)
                {
                    if (!usedAcornCharm)
                    {
                        if (totalAmountOfAttacks > 0)
                        {
                            battleSystem._StickerPowerHolder.CreateStickerMessage(StickerPower.AcornCharm);

                            TextMeshProUGUI attackText = enemyAttackImage.GetComponentInChildren<TextMeshProUGUI>();

                            ParticleSystem attackReductionParticle = enemyAttackImage.GetComponentInChildren<ParticleSystem>();

                            attackReductionParticle.Play();

                            if (totalAmountOfAttacks <= 0)
                            {
                                attackText.text = "<size=0.3>x </size>0";
                            }
                            else
                            {
                                totalAmountOfAttacks--;

                                attackText.text = "<size=0.3>x </size>" + totalAmountOfAttacks;
                            }

                            usedAcornCharm = true;
                        }
                    }
                }
            }

            battlePlayer.CheckCourageUnderFireSticker();
        }
    }

    private void ApplyStatusEffect()
    {
        if(statusEffectChecker._EnemyActions[ActionIndex]._actionStatus != ActionStatus.NONE)
        {
            if(battlePlayer.CurrentHealth > 0)
            {
                bool hasSameStatus = false;

                battleSystem._ParticleEffectManager.SpawnParticle(statusEffectChecker._EnemyActions[ActionIndex]._actionStatus.ToString(), 
                                                                  new Vector3(battlePlayer.transform.position.x, battlePlayer.transform.position.y, battlePlayer.transform.position.z), false);

                if (battlePlayer.StatusEffectHolder.transform.childCount > 0)
                {
                    for (int i = 0; i < battlePlayer.StatusEffectHolder.transform.childCount; i++)
                    {
                        StatusEffects statusEffects = battlePlayer.StatusEffectHolder.transform.GetChild(i).GetComponent<StatusEffects>();

                        if (statusEffectChecker._EnemyActions[ActionIndex]._actionStatus == (ActionStatus)statusEffects._statusEffect)
                        {
                            battlePlayer.StatusEffectText.transform.parent.GetComponent<Animator>().Play("StatusText", -1, 0);
                            battlePlayer.StatusEffectText.text = statusEffectChecker._EnemyActions[ActionIndex].StatusEffectName;

                            statusEffects.StatusTime = statusEffectChecker._EnemyActions[ActionIndex].StatusEffectDuration;
                            statusEffects.StatusTimeText.text = statusEffectChecker._EnemyActions[ActionIndex].StatusEffectDuration.ToString();

                            hasSameStatus = true;
                            break;
                        }
                        else
                        {
                            hasSameStatus = false;
                        }
                    }

                    if(!hasSameStatus)
                    {
                        if(battlePlayer.HasInvulnerable)
                        {
                            battlePlayer.StatusEffectText.transform.parent.GetComponent<Animator>().Play("StatusText", -1, 0);
                            battlePlayer.StatusEffectText.text = "IMMUNE";

                            return;
                        }
                        else
                        {
                            for (int i = 0; i < battlePlayer.StatusEffectHolder.transform.childCount; i++)
                            {
                                StatusEffects statusEffects = battlePlayer.StatusEffectHolder.transform.GetChild(i).GetComponent<StatusEffects>();

                                if (statusEffectChecker._EnemyActions[ActionIndex]._actionStatus != ActionStatus.NONE && statusEffects._statusEffect == StatusEffect.Impervious)
                                {
                                    battlePlayer.StatusEffectText.transform.parent.GetComponent<Animator>().Play("StatusText", -1, 0);
                                    battlePlayer.StatusEffectText.text = "IMMUNE";

                                    return;
                                }
                                if (statusEffectChecker._EnemyActions[ActionIndex]._actionStatus == ActionStatus.Burns && statusEffects._statusEffect == StatusEffect.BurnsImmune)
                                {
                                    battlePlayer.StatusEffectText.transform.parent.GetComponent<Animator>().Play("StatusText", -1, 0);
                                    battlePlayer.StatusEffectText.text = "IMMUNE";

                                    return;
                                }
                                if (statusEffectChecker._EnemyActions[ActionIndex]._actionStatus == ActionStatus.Poison && statusEffects._statusEffect == StatusEffect.PoisonImmune)
                                {
                                    battlePlayer.StatusEffectText.transform.parent.GetComponent<Animator>().Play("StatusText", -1, 0);
                                    battlePlayer.StatusEffectText.text = "IMMUNE";

                                    return;
                                }
                                if (statusEffectChecker._EnemyActions[ActionIndex]._actionStatus == ActionStatus.Paralysis && statusEffects._statusEffect == StatusEffect.ParalysisImmune)
                                {
                                    battlePlayer.StatusEffectText.transform.parent.GetComponent<Animator>().Play("StatusText", -1, 0);
                                    battlePlayer.StatusEffectText.text = "IMMUNE";

                                    return;
                                }
                                if (statusEffectChecker._EnemyActions[ActionIndex]._actionStatus == ActionStatus.Undead && statusEffects._statusEffect == StatusEffect.UndeadImmune)
                                {
                                    battlePlayer.StatusEffectText.transform.parent.GetComponent<Animator>().Play("StatusText", -1, 0);
                                    battlePlayer.StatusEffectText.text = "IMMUNE";

                                    return;
                                }
                            }

                            var status = Instantiate(statusEffectPrefab, battlePlayer.StatusEffectHolder.transform);

                            battlePlayer.StatusEffectText.transform.parent.GetComponent<Animator>().Play("StatusText", -1, 0);
                            battlePlayer.StatusEffectText.text = statusEffectChecker._EnemyActions[ActionIndex].StatusEffectName;

                            status.Target = battlePlayer.gameObject;
                            status._statusEffect = (StatusEffect)statusEffectChecker._EnemyActions[ActionIndex]._actionStatus;
                            status.SetStatusParticle();
                            status.StatusTime = statusEffectChecker._EnemyActions[ActionIndex].StatusEffectDuration;
                            status.StatusTimeText.text = status.StatusTime.ToString();
                            status.StatusEffectImage.sprite = statusEffectChecker._EnemyActions[ActionIndex].StatusEffectSprite;
                            status.StatusChangePercentage = statusEffectChecker._EnemyActions[ActionIndex].StatusBoostPercentage;

                            status.CheckStatusChange();

                            if (statusEffectChecker._EnemyActions[ActionIndex].ShouldCheckStatus)
                            {
                                ; status.ShouldCheckStatus = true;
                            }

                            if (status._statusEffect != StatusEffect.Paralysis)
                            {
                                status.transform.SetAsFirstSibling();
                            }
                        }
                    }
                }
                else
                {
                    var status = Instantiate(statusEffectPrefab, battlePlayer.StatusEffectHolder.transform);

                    battlePlayer.StatusEffectText.transform.parent.GetComponent<Animator>().Play("StatusText", -1, 0);
                    battlePlayer.StatusEffectText.text = statusEffectChecker._EnemyActions[ActionIndex].StatusEffectName;

                    status.Target = battlePlayer.gameObject;
                    status._statusEffect = (StatusEffect)statusEffectChecker._EnemyActions[ActionIndex]._actionStatus;
                    status.SetStatusParticle();
                    status.StatusTime = statusEffectChecker._EnemyActions[ActionIndex].StatusEffectDuration;
                    status.StatusTimeText.text = status.StatusTime.ToString();
                    status.StatusEffectImage.sprite = statusEffectChecker._EnemyActions[ActionIndex].StatusEffectSprite;
                    status.StatusChangePercentage = statusEffectChecker._EnemyActions[ActionIndex].StatusBoostPercentage;

                    status.CheckStatusChange();

                    if (statusEffectChecker._EnemyActions[ActionIndex].ShouldCheckStatus)
                    {
                        status.ShouldCheckStatus = true;
                    }
                }
                battlePlayer.DisableStatusEffectRaycast();
            }
        }
    }

    public void CheckStatusEffectImmunities(CardStatus cardStatus, string statusEffectName, float statusEffectChance, int statusEffectTime, bool shouldCheckStatus, Sprite statusEffectSprite, CardTemplate _cardTemplate)
    {
        bool isImmune = false;

        for (int i = 0; i < statusEffectHolder.transform.childCount; i++)
        {
            StatusEffects statusEffects = statusEffectHolder.transform.GetChild(i).GetComponent<StatusEffects>();

            if (cardStatus == CardStatus.Burns && statusEffects._statusEffect == StatusEffect.BurnsImmune)
            {
                statusEffectText.transform.parent.GetComponent<Animator>().Play("StatusText", -1, 0);
                statusEffectText.text = "IMMUNE";

                isImmune = true;
            }
            if (cardStatus == CardStatus.Poison && statusEffects._statusEffect == StatusEffect.PoisonImmune)
            {
                statusEffectText.transform.parent.GetComponent<Animator>().Play("StatusText", -1, 0);
                statusEffectText.text = "IMMUNE";

                isImmune = true;
            }
            if (cardStatus == CardStatus.Paralysis && statusEffects._statusEffect == StatusEffect.ParalysisImmune)
            {
                statusEffectText.transform.parent.GetComponent<Animator>().Play("StatusText", -1, 0);
                statusEffectText.text = "IMMUNE";

                isImmune = true;
            }
            if (cardStatus == CardStatus.Freeze && statusEffects._statusEffect == StatusEffect.FreezeImmune)
            {
                statusEffectText.transform.parent.GetComponent<Animator>().Play("StatusText", -1, 0);
                statusEffectText.text = "IMMUNE";

                isImmune = true;
            }
        }

        if(!isImmune)
        {
            if (cardStatus == CardStatus.Paralysis)
            {
                int rand = Random.Range(0, 100);

                if (rand <= statusEffectChance)
                {
                    var status = Instantiate(statusEffectPrefab, statusEffectHolder.transform);

                    statusEffectText.transform.parent.GetComponent<Animator>().Play("StatusText", -1, 0);
                    statusEffectText.text = statusEffectName;

                    status.Target = gameObject;
                    status._statusEffect = (StatusEffect)cardStatus;
                    status.SetStatusParticle();

                    int effectTime = statusEffectTime;

                    if (MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.Stretch))
                    {
                        effectTime *= 2;
                    }
                    if (battleSystem.HasThePriest)
                    {
                        effectTime *= 2;
                    }

                    status.StatusTime = effectTime;
                    status.StatusTimeText.text = effectTime.ToString();
                    status.StatusEffectImage.sprite = statusEffectSprite;
                    status.StatusChangePercentage = _cardTemplate.statusBoostPercentage;

                    status.CheckStatusChange();

                    if (shouldCheckStatus)
                    {
                        status.ShouldCheckStatus = true;
                    }
                }
            }
            else
            {
                var status = Instantiate(statusEffectPrefab, statusEffectHolder.transform);

                statusEffectText.transform.parent.GetComponent<Animator>().Play("StatusText", -1, 0);
                statusEffectText.text = statusEffectName;

                status.Target = gameObject;
                status._statusEffect = (StatusEffect)cardStatus;
                status.SetStatusParticle();

                int effectTime = statusEffectTime;

                if (MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.Stretch))
                {
                    effectTime *= 2;
                }
                if (battleSystem.HasThePriest)
                {
                    effectTime *= 2;
                }

                status.StatusTime = effectTime;
                status.StatusTimeText.text = effectTime.ToString();
                status.StatusEffectImage.sprite = statusEffectSprite;
                status.StatusChangePercentage = _cardTemplate.statusBoostPercentage;

                status.CheckStatusChange();

                if (shouldCheckStatus)
                {
                    status.ShouldCheckStatus = true;
                }

                if (status._statusEffect == StatusEffect.Burns || status._statusEffect == StatusEffect.Poison)
                {
                    status.transform.SetAsFirstSibling();
                }
            }
        }
    }

    public void EndedAttack()
    {
        if(battlePlayer.CurrentHealth > 0)
        {
            if (battlePlayer.CurrentHealth <= battlePlayer._mainCharacterStats.maximumHealth / 2)
            {
                if (MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.AcornCharm))
                {
                    int additionalAttacks = statusEffectChecker._EnemyActions[ActionIndex].AdditionalAttacks > 0 ? statusEffectChecker._EnemyActions[ActionIndex].AdditionalAttacks - 1 : 
                                            statusEffectChecker._EnemyActions[ActionIndex].AdditionalAttacks;

                    if(isFrozen)
                    {
                        additionalAttacks -= 2;

                        if(additionalAttacks <= 0)
                        {
                            additionalAttacks = 0;
                        }
                    }

                    if (statusEffectChecker._EnemyActions[ActionIndex].AttackIndex <= additionalAttacks && currentHealth > 0)
                    {
                        if(!usedAcornCharm)
                        {
                            battleSystem._StickerPowerHolder.CreateStickerMessage(StickerPower.AcornCharm);

                            TextMeshProUGUI attackText = enemyAttackImage.GetComponentInChildren<TextMeshProUGUI>();

                            ParticleSystem attackReductionParticle = enemyAttackImage.GetComponentInChildren<ParticleSystem>();

                            attackReductionParticle.Play();

                            attackText.text = "<size=0.3>x </size>" + (totalAmountOfAttacks - 1);

                            usedAcornCharm = true;
                        }

                        repeatActionRoutine = StartCoroutine(RepeatAction());
                    }
                    else
                    {
                        ResetAttack();

                        usedAcornCharm = false;
                    }
                }
                else
                {
                    int additionalAttacks = statusEffectChecker._EnemyActions[ActionIndex].AdditionalAttacks;

                    if (isFrozen)
                    {
                        additionalAttacks -= 2;

                        if (additionalAttacks <= 0)
                        {
                            additionalAttacks = 0;
                        }
                    }

                    if (statusEffectChecker._EnemyActions[ActionIndex].AttackIndex <= additionalAttacks && currentHealth > 0)
                    {
                        repeatActionRoutine = StartCoroutine(RepeatAction());
                    }
                    else
                    {
                        ResetAttack();

                        usedAcornCharm = false;
                    }
                }
            }
            else
            {
                int additionalAttacks = statusEffectChecker._EnemyActions[ActionIndex].AdditionalAttacks;

                if (isFrozen)
                {
                    additionalAttacks -= 2;

                    if (additionalAttacks <= 0)
                    {
                        additionalAttacks = 0;
                    }
                }

                if (statusEffectChecker._EnemyActions[ActionIndex].AttackIndex <= additionalAttacks && currentHealth > 0)
                {
                    repeatActionRoutine = StartCoroutine(RepeatAction());
                }
                else
                {
                    ResetAttack();
                }
            }
        }
        else
        {
            animator.Play("Idle");
        }
    }

    private void ResetAttack()
    {
        if (statusEffectChecker._EnemyActions[ActionIndex]._EnemyActions != EnemyActions.SupportAction && statusEffectChecker._EnemyActions[ActionIndex]._EnemyActions != EnemyActions.HealAction)
        {
            if(!battlePlayer.StunParticle.isPlaying)
                StartCoroutine(battlePlayer.WaitToResetGuard());
        }

        statusEffectChecker._EnemyActions[ActionIndex].AttackIndex = 0;

        hitBox.enabled = false;

        takingTurn = false;

        attackInterrupted = false;

        endedAttack = true;

        statusEffectIndex = 0;
    }

    public void EndedTurn(bool checkStatus, bool incrementEnemyIndex)
    {
        endedAttack = false;
        movingTowardsPlayerPhysical = false;
        movingTowardsPlayerRanged = false;
        attackInterrupted = false;
        damaged = false;
        choseAction = false;

        hitBox.enabled = true;

        statusEffectChecker._EnemyActions[ActionIndex].AttackIndex = 0;

        if(incrementEnemyIndex)
        {
            battleSystem.EnemyIndex++;

            battleSystem.EnemyTurn();
            battleSystem.ResetEnemyStatusIndex();

            if (checkStatus)
            {
                battleSystem.CheckEnemyStatus();
            }

            takingTurn = false;
        }
        else
        {
            StartCoroutine(WaitToBeginTurn(checkStatus));
        }
    }

    private IEnumerator WaitToBeginTurn(bool checkStatus)
    {
        yield return new WaitForSeconds(0.5f);

        battleSystem.EnemyTurn();
        battleSystem.ResetEnemyStatusIndex();

        if (checkStatus)
        {
            battleSystem.CheckEnemyStatus();
        }

        takingTurn = false;
    }

    private void SetAttackImageTransform()
    {
        enemyAttackImage.SetActive(true);

        enemyAttackImage.transform.position = new Vector3(transform.position.x - 0.15f, transform.position.y + enemyStats.attackImagePositionY, transform.position.z);

        if(statusEffectChecker._EnemyActions[ActionIndex]._actionStatus != ActionStatus.NONE)
        {
            enemyAttackImage.transform.GetChild(2).GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            enemyAttackImage.transform.GetChild(2).GetChild(0).gameObject.SetActive(false);
        }

        if(statusEffectChecker._EnemyActions[ActionIndex].IsAProjectile)
        {
            enemyAttackImage.transform.GetChild(2).GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            enemyAttackImage.transform.GetChild(2).GetChild(1).gameObject.SetActive(false);
        }

        SetAttackImageText();
    }

    private void SetCounterAttackTransform()
    {
        counterAttack.transform.position = new Vector3(transform.position.x - 0.15f, transform.position.y + enemyStats.attackImagePositionY + 0.7f, transform.position.z);
    }

    private void SetAttackImageText()
    {
        TextMeshProUGUI attackText = enemyAttackImage.GetComponentInChildren<TextMeshProUGUI>();

        if(MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.AcornCharm))
        {
            if (battlePlayer.CurrentHealth <= battlePlayer._mainCharacterStats.maximumHealth / 2 && statusEffectChecker._EnemyActions[ActionIndex].AdditionalAttacks > 0)
            {
                if(!usedAcornCharm)
                {
                    battleSystem._StickerPowerHolder.CreateStickerMessage(StickerPower.AcornCharm);

                    ParticleSystem attackReductionParticle = enemyAttackImage.GetComponentInChildren<ParticleSystem>();

                    attackReductionParticle.Play();

                    usedAcornCharm = true;

                    if (statusEffectChecker._EnemyActions[ActionIndex].AdditionalAttacks > 0)
                    {
                        totalAmountOfAttacks = (statusEffectChecker._EnemyActions[ActionIndex].AdditionalAttacks) - statusEffectChecker._EnemyActions[ActionIndex].AttackIndex;
                    }

                    if (IsFrozen)
                    {
                        totalAmountOfAttacks = ((statusEffectChecker._EnemyActions[ActionIndex].AdditionalAttacks) - statusEffectChecker._EnemyActions[ActionIndex].AttackIndex) - 2;

                        if (totalAmountOfAttacks <= 0)
                        {
                            totalAmountOfAttacks = 1;
                        }
                    }
                }
            }
            else
            {
                if (IsFrozen)
                {
                    totalAmountOfAttacks = ((statusEffectChecker._EnemyActions[ActionIndex].AdditionalAttacks + 1) - statusEffectChecker._EnemyActions[ActionIndex].AttackIndex) - 2;

                    if (totalAmountOfAttacks <= 0)
                    {
                        totalAmountOfAttacks = 1;
                    }
                }
                else
                {
                    totalAmountOfAttacks = (statusEffectChecker._EnemyActions[ActionIndex].AdditionalAttacks + 1) - statusEffectChecker._EnemyActions[ActionIndex].AttackIndex;
                }
            }
        }
        else
        {
            if (IsFrozen)
            {
                totalAmountOfAttacks = ((statusEffectChecker._EnemyActions[ActionIndex].AdditionalAttacks + 1) - statusEffectChecker._EnemyActions[ActionIndex].AttackIndex) - 2;

                if (totalAmountOfAttacks <= 0)
                {
                    totalAmountOfAttacks = 1;
                }
            }
            else
            {
                totalAmountOfAttacks = (statusEffectChecker._EnemyActions[ActionIndex].AdditionalAttacks + 1) - statusEffectChecker._EnemyActions[ActionIndex].AttackIndex;
            }
        }

        attackText.text = "<size=0.3>x </size>" + totalAmountOfAttacks;
    }

    public void DecrementTotalAmountOfAttacks()
    {
        if(enemyAttackImage.activeInHierarchy || statusEffectChecker._EnemyActions[ActionIndex]._EnemyActions == EnemyActions.SupportAction)
        {
            if (totalAmountOfAttacks > 0)
                totalAmountOfAttacks--;

            TextMeshProUGUI attackText = enemyAttackImage.GetComponentInChildren<TextMeshProUGUI>();

            attackText.text = "<size=0.3>x </size>" + totalAmountOfAttacks;
        }
    }

    public void CheckForStatusEffectsBeforeTurn()
    {
        if(battleSystem.Enemies.Count > 0)
        {
            if (statusEffectHolder.transform.childCount <= 0)
            {
                ChooseAction();
            }
            else
            {
                StatusEffects statusEffect = null;

                if (statusEffectIndex < statusEffectHolder.transform.childCount)
                {
                    statusEffect = statusEffectHolder.transform.GetChild(statusEffectIndex).GetComponent<StatusEffects>();

                    if (statusEffect.ShouldCheckStatus)
                    {
                        ReverseHealthFade();

                        statusEffect.ApplyEffect();

                        statusEffectIndex++;
                    }
                    else
                    {
                        statusEffectIndex++;

                        ReverseHealthFade();

                        statusEffect.PlayAnimator();

                        if (statusEffect != null && statusEffect.gameObject.activeSelf)
                            statusEffect.ApplyEffect();
                    }
                }
                else
                {
                    statusEffect = statusEffectHolder.transform.GetChild(statusEffectIndex - 1).GetComponent<StatusEffects>();

                    if (statusEffect != null)
                    {
                        if (statusEffect.StatusTime > 0)
                        {
                            ChooseAction();
                        }
                        else if (statusEffect.StatusTime <= 0)
                        {
                            ChooseAction();
                        }
                    }
                }
            }
        }
    }

    public void ChooseAction()
    {
        if(!choseAction)
        {
            battlePlayer.SetPerfectGuardToFalse();

            if (!battlePlayer.ParalysisParticle.gameObject.activeSelf)
                battlePlayer.IsParalyzed = false;

            if (CheckCrocodileTearsSticker()) return;

            hasTheStatusEffect = false;

            didIncrementIndex = false;

            statusEffectChecker.ResetActions();

            int action = statusEffectChecker.ActionIndex;

            ActionIndex = action;

            moving = true;

            ActionChoices();

            if (statusEffectChecker._EnemyActions[ActionIndex].CanBeBlocked && !MenuController.instance._StickerPowerManager.CheckStartOfBattleStickerPower(StickerPower.Enrage) &&
                !MenuController.instance._StickerPowerManager.CheckStartOfBattleStickerPower(StickerPower.ShadowForm))
            {
                if(!battlePlayer.IsParalyzed && !battlePlayer.StunParticle.isPlaying)
                {
                    if (MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.MiracleGuard))
                    {
                        battlePlayer.MiracleGuardIndex++;

                        if (battlePlayer.MiracleGuardIndex <= 2)
                        {
                            battleSystem._StickerPowerHolder.CreateStickerMessage(StickerPower.MiracleGuard);
                        }
                    }

                    AudioManager.instance.PlaySoundEffect(AudioManager.instance.GuardAudio);

                    battleSystem.CounterMeasurePromptText.text = "Hold!";

                    if (InputManager.instance.ControllerPluggedIn)
                    {
                        if (InputManager.instance.ControllerName == "xbox")
                        {
                            if(SteamUtils.IsSteamRunningOnSteamDeck())
                            {
                                battleSystem.CounterMeasureButtonText.text = "L1";
                            }
                            else
                            {
                                battleSystem.CounterMeasureButtonText.text = "LB";
                            }
                        }
                        else
                        {
                            battleSystem.CounterMeasureButtonText.text = "R2";
                        }
                    }
                    else
                    {
                        battleSystem.CounterMeasureButtonText.text = "Z";
                    }

                    battlePlayer.GuardGaugeAnimator.Play("Guard");

                    battlePlayer.CanGuard = true;
                    battlePlayer.CanCounter = false;
                }
            }
            else if (statusEffectChecker._EnemyActions[ActionIndex].CanBeCountered)
            {
                if(!battlePlayer.IsParalyzed && !battlePlayer.StunParticle.isPlaying)
                {
                    if (MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.MiracleCounter))
                    {
                        battlePlayer.MiracleCounterIndex++;

                        if (battlePlayer.MiracleCounterIndex <= 2)
                        {
                            battleSystem._StickerPowerHolder.CreateStickerMessage(StickerPower.MiracleCounter);
                        }

                        if (MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.BlindRage))
                        {
                            if(battlePlayer.MiracleCounterIndex > 2)
                            {
                                battleSystem._StickerPowerHolder.CreateStickerMessage(StickerPower.BlindRage);

                                battlePlayer.HasBlindRage = true;

                                battlePlayer.ResetCounterAttack();
                            }
                        }
                    }
                    else if(!MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.MiracleCounter) &&
                             MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.BlindRage))
                    {
                        battleSystem._StickerPowerHolder.CreateStickerMessage(StickerPower.BlindRage);

                        battlePlayer.HasBlindRage = true;

                        battlePlayer.ResetCounterAttack();
                    }

                    battlePlayer._CounterAttack = counterAttack;

                    counterAttack.StartNotch();

                    AudioManager.instance.PlaySoundEffect(AudioManager.instance.CounterAudio);

                    battleSystem.CounterMeasurePromptText.text = "Press!";

                    if (InputManager.instance.ControllerPluggedIn)
                    {
                        if (InputManager.instance.ControllerName == "xbox")
                        {
                            battleSystem.CounterMeasureButtonText.text = "A";
                        }
                        else
                        {
                            battleSystem.CounterMeasureButtonText.text = "X";
                        }
                    }
                    else
                    {
                        battleSystem.CounterMeasureButtonText.text = "X";
                    }

                    battlePlayer.CanCounter = true;
                    battlePlayer.CanGuard = false;
                }
            }
            else
            {
                battlePlayer.CanGuard = false;
                battlePlayer.CanCounter = false;
            }

            if (battleSystem.BattleInformationAnimator != null)
            {
                battleSystem.BattleInformationAnimator.Play("BattleInformation", -1, 0);
                battleSystem.BattleInformationText.text = statusEffectChecker._EnemyActions[ActionIndex].ActionName;
            }

            MoveAnimation();

            choseAction = true;
        }
    }

    private bool CheckCrocodileTearsSticker()
    {
        bool skipTurn = false;

        if(MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.CrocodileTears))
        {
            int rand = Random.Range(0, 100);

            if (rand <= 15)
            {
                if(takingTurn)
                {
                    if(!checkedSkippedTurn)
                    {
                        battleSystem._StickerPowerHolder.CreateStickerMessage(StickerPower.CrocodileTears);

                        battleSystem.EnemyIndex++;

                        battleSystem.EnemyTurn();
                        battleSystem.ResetEnemyStatusIndex();

                        checkedSkippedTurn = true;

                        takingTurn = false;

                        skipTurn = true;
                    }
                }
            }
        }

        return skipTurn;
    }

    private void ActionChoices()
    {
        if(repeatActionRoutine != null)
        {
            StopCoroutine(repeatActionRoutine);
            repeatActionRoutine = null;
        }

        switch (statusEffectChecker._EnemyActions[ActionIndex]._EnemyActions)
        {
            case (EnemyActions.PhysicalAction):
                movingTowardsPlayerPhysical = true;
                battlePlayer.CheckIfPlayerIsStunned();
                if(!battlePlayer.IsParalyzed && !battlePlayer.StunParticle.isPlaying)
                {
                    battleSystem.CounterMeasurePromptAnimator.Play("ActionPrompt");
                    statusEffectChecker._EnemyActions[ActionIndex].AttackIndex = 0;
                }
                hitBox.enabled = true;
                break;
            case (EnemyActions.RangedAction):
                if(!battlePlayer.IsParalyzed && !battlePlayer.StunParticle.isPlaying)
                {
                    if(!MenuController.instance._StickerPowerManager.CheckStartOfBattleStickerPower(StickerPower.Enrage) && 
                       !MenuController.instance._StickerPowerManager.CheckStartOfBattleStickerPower(StickerPower.ShadowForm))
                    {
                        battleSystem.CounterMeasurePromptAnimator.Play("ActionPrompt");
                    }
                }
                statusEffectChecker._EnemyActions[ActionIndex].AttackIndex = 0;
                movingTowardsPlayerRanged = true;
                break;
            case (EnemyActions.SupportAction):
                statusEffectChecker._EnemyActions[ActionIndex].AttackIndex = 0;
                break;
            case (EnemyActions.HealAction):
                statusEffectChecker._EnemyActions[ActionIndex].AttackIndex = 0;
                break;
        }
    }

    public void ChooseSupportTarget()
    {
        if(statusEffectChecker._EnemyActions[ActionIndex]._EnemyActions == EnemyActions.SupportAction)
        {
            switch (statusEffectChecker._EnemyActions[ActionIndex]._ActionTarget)
            {
                case (ActionTarget.SingleEnemy):
                    ApplyEnemyStatus(false);
                    break;
                case (ActionTarget.MultiEnemy):
                    ApplyEnemyStatus(battleSystem.Enemies.Count > 1 ? true : false);
                    break;
                case (ActionTarget.Player):
                    ApplyStatusEffect();
                    break;
            }
        }
        else
        {
            switch (statusEffectChecker._EnemyActions[ActionIndex]._ActionTarget)
            {
                case (ActionTarget.SingleEnemy):
                    RestoreHealAcion(statusEffectChecker._EnemyActions[ActionIndex].StatusBoostPercentage, false);
                    break;
                case (ActionTarget.MultiEnemy):
                    RestoreHealAcion(statusEffectChecker._EnemyActions[ActionIndex].StatusBoostPercentage, battleSystem.Enemies.Count > 1 ? true : false);
                    break;
            }
        }
    }

    private void ApplyEnemyStatus(bool multipleEnemies)
    {
        if (statusEffectChecker._EnemyActions[ActionIndex]._actionStatus != ActionStatus.NONE)
        {
            if(multipleEnemies)
            {
                battleSystem._ParticleEffectManager.SpawnParticle(statusEffectChecker._EnemyActions[ActionIndex]._actionStatus.ToString(), 
                                                                  new Vector3(transform.position.x, transform.position.y + 1.2f, transform.position.z), true);

                for (int i = 0; i < battleSystem.Enemies.Count; i++)
                {
                    if (battleSystem.Enemies[i].statusEffectHolder.transform.childCount > 0)
                    {
                        bool hasSameStatus = false;

                        for (int j = 0; j < battleSystem.Enemies[i].StatusEffectHolder.transform.childCount; j++)
                        {
                            StatusEffects statusEffects = battleSystem.Enemies[i].StatusEffectHolder.transform.GetChild(j).GetComponent<StatusEffects>();
                            if (statusEffects._statusEffect == (StatusEffect)statusEffectChecker._EnemyActions[ActionIndex]._actionStatus)
                            {
                                battleSystem.Enemies[i].StatusEffectText.transform.parent.GetComponent<Animator>().Play("StatusText", -1, 0);
                                battleSystem.Enemies[i].StatusEffectText.text = statusEffectChecker._EnemyActions[ActionIndex].StatusEffectName;

                                statusEffects.StatusTime = statusEffectChecker._EnemyActions[ActionIndex].StatusEffectDuration;
                                statusEffects.StatusTimeText.text = statusEffectChecker._EnemyActions[ActionIndex].StatusEffectDuration.ToString();
                                hasSameStatus = true;
                                break;
                            }
                            else
                            {
                                hasSameStatus = false;
                            }
                        }

                        if(!hasSameStatus)
                        {
                            var status = Instantiate(statusEffectPrefab, battleSystem.Enemies[i].StatusEffectHolder.transform);

                            battleSystem.Enemies[i].StatusEffectText.transform.parent.GetComponent<Animator>().Play("StatusText", -1, 0);
                            battleSystem.Enemies[i].StatusEffectText.text = statusEffectChecker._EnemyActions[ActionIndex].StatusEffectName;

                            status.Target = battleSystem.Enemies[i].gameObject;
                            status._statusEffect = (StatusEffect)statusEffectChecker._EnemyActions[ActionIndex]._actionStatus;
                            status.SetStatusParticle();
                            status.StatusTime = statusEffectChecker._EnemyActions[ActionIndex].StatusEffectDuration;
                            status.StatusTimeText.text = status.StatusTime.ToString();
                            status.StatusEffectImage.sprite = statusEffectChecker._EnemyActions[ActionIndex].StatusEffectSprite;
                            status.StatusChangePercentage = statusEffectChecker._EnemyActions[ActionIndex].StatusBoostPercentage;

                            status.CheckStatusChange();
                        }
                    }
                    else
                    {
                        var status = Instantiate(statusEffectPrefab, battleSystem.Enemies[i].StatusEffectHolder.transform);

                        battleSystem.Enemies[i].StatusEffectText.transform.parent.GetComponent<Animator>().Play("StatusText", -1, 0);
                        battleSystem.Enemies[i].StatusEffectText.text = statusEffectChecker._EnemyActions[ActionIndex].StatusEffectName;

                        status.Target = battleSystem.Enemies[i].gameObject;
                        status._statusEffect = (StatusEffect)statusEffectChecker._EnemyActions[ActionIndex]._actionStatus;
                        status.SetStatusParticle();
                        status.StatusTime = statusEffectChecker._EnemyActions[ActionIndex].StatusEffectDuration;
                        status.StatusTimeText.text = status.StatusTime.ToString();
                        status.StatusEffectImage.sprite = statusEffectChecker._EnemyActions[ActionIndex].StatusEffectSprite;
                        status.StatusChangePercentage = statusEffectChecker._EnemyActions[ActionIndex].StatusBoostPercentage;

                        status.CheckStatusChange();
                    }
                }
            }
            else
            {
                if(battleSystem.Enemies.Count > 1)
                {
                    battleSystem._ParticleEffectManager.SpawnParticle(statusEffectChecker._EnemyActions[ActionIndex]._actionStatus.ToString(), 
                                                                      new Vector3(statusEffectChecker._BattleEnemy.transform.position.x,
                                                                                  statusEffectChecker._BattleEnemy.transform.position.y +
                                                                                  statusEffectChecker._BattleEnemy.enemyStats.buffOffSetY,
                                                                                  statusEffectChecker._BattleEnemy.transform.position.z), false);
                }
                else
                {
                    battleSystem._ParticleEffectManager.SpawnParticle(statusEffectChecker._EnemyActions[ActionIndex]._actionStatus.ToString(), 
                                                                      new Vector3(transform.position.x, transform.position.y + battleSystem.Enemies[statusEffectChecker.EnemyIndex].enemyStats.buffOffSetY, 
                                                                      transform.position.z), false);
                }

                if(statusEffectChecker._BattleEnemy.statusEffectHolder.transform.childCount > 0)
                {
                    bool hasSameStatus = false;

                    for (int i = 0; i < statusEffectChecker._BattleEnemy.statusEffectHolder.transform.childCount; i++)
                    {
                        StatusEffects statusEffects = statusEffectChecker._BattleEnemy.statusEffectHolder.transform.GetChild(i).GetComponent<StatusEffects>();
                        if (statusEffects._statusEffect == (StatusEffect)statusEffectChecker._EnemyActions[ActionIndex]._actionStatus)
                        {
                            statusEffectChecker._BattleEnemy.statusEffectText.transform.parent.GetComponent<Animator>().Play("StatusText", -1, 0);
                            statusEffectChecker._BattleEnemy.statusEffectText.text = statusEffectChecker._EnemyActions[ActionIndex].StatusEffectName;

                            statusEffects.StatusTime = statusEffectChecker._EnemyActions[ActionIndex].StatusEffectDuration;
                            statusEffects.StatusTimeText.text = statusEffects.StatusTime.ToString();
                            hasSameStatus = true;
                            break;
                        }
                        else
                        {
                            hasSameStatus = false;
                        }
                    }

                    if (!hasSameStatus)
                    {
                        var status = Instantiate(statusEffectPrefab, statusEffectChecker._BattleEnemy.statusEffectHolder.transform);

                        statusEffectChecker._BattleEnemy.statusEffectText.transform.parent.GetComponent<Animator>().Play("StatusText", -1, 0);
                        statusEffectChecker._BattleEnemy.statusEffectText.text = statusEffectChecker._EnemyActions[ActionIndex].StatusEffectName;

                        status.Target = statusEffectChecker._BattleEnemy.gameObject;
                        status._statusEffect = (StatusEffect)statusEffectChecker._EnemyActions[ActionIndex]._actionStatus;
                        status.SetStatusParticle();
                        status.StatusTime = statusEffectChecker._EnemyActions[ActionIndex].StatusEffectDuration;
                        status.StatusTimeText.text = status.StatusTime.ToString();
                        status.StatusEffectImage.sprite = statusEffectChecker._EnemyActions[ActionIndex].StatusEffectSprite;
                        status.StatusChangePercentage = statusEffectChecker._EnemyActions[ActionIndex].StatusBoostPercentage;

                        status.CheckStatusChange();
                    }
                }
                else
                {
                    var status = Instantiate(statusEffectPrefab, statusEffectChecker._BattleEnemy.statusEffectHolder.transform);

                    statusEffectChecker._BattleEnemy.statusEffectText.transform.parent.GetComponent<Animator>().Play("StatusText", -1, 0);
                    statusEffectChecker._BattleEnemy.statusEffectText.text = statusEffectChecker._EnemyActions[ActionIndex].StatusEffectName;

                    status.Target = statusEffectChecker._BattleEnemy.gameObject;
                    status._statusEffect = (StatusEffect)statusEffectChecker._EnemyActions[ActionIndex]._actionStatus;
                    status.SetStatusParticle();
                    status.StatusTime = statusEffectChecker._EnemyActions[ActionIndex].StatusEffectDuration;
                    status.StatusTimeText.text = status.StatusTime.ToString();
                    status.StatusEffectImage.sprite = statusEffectChecker._EnemyActions[ActionIndex].StatusEffectSprite;
                    status.StatusChangePercentage = statusEffectChecker._EnemyActions[ActionIndex].StatusBoostPercentage;

                    status.CheckStatusChange();
                }
            }
        }
    }

    public void PlayStatusRemovalParticle(bool changeTurn)
    {
        statusEffectRemoval.gameObject.SetActive(true);
        statusEffectRemoval.Play();

        if(PlayerPrefs.HasKey("SoundEffects"))
        {
            statusEffectRemoval.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SoundEffects");
            statusEffectRemoval.GetComponent<AudioSource>().Play();
        }
        else
        {
            statusEffectRemoval.GetComponent<AudioSource>().Play();
        }

        if(!GameManager.instance.IsAPreemptiveStrike)
        {
            if (changeTurn)
            {
                battleSystem.EnemyTurn();
                battleSystem.BeginEnemyActionCoroutine();
            }
            else
            {
                if (battleSystem.Enemies.Count > 1 && battleSystem.EnemyIndex > 0)
                {
                    battleSystem.ResetEnemyStatusIndex();
                    battleSystem.CheckEnemyStatus();
                }
            }
        }
    }

    public void EnableProjectileAttack()
    {
        ProjectileParticle.gameObject.SetActive(true);

        if(ProjectileParticle.GetComponent<ParticleSystem>())
            ProjectileParticle.GetComponent<ParticleSystem>().Play();

        if(!ProjectileParticle.GetComponent<ETFXProjectileScript>())
        {
            ProjectileParticle.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + statusEffectChecker._EnemyActions[ActionIndex].ProjectileOffsetY, transform.position.z);
        }
        else
        {
            ProjectileParticle.GetComponent<ETFXProjectileScript>().Enemy = this;

            ProjectileParticle.GetComponent<ETFXProjectileScript>().PlayedAudio = false;

            ProjectileParticle.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + statusEffectChecker._EnemyActions[ActionIndex].ProjectileOffsetY, transform.position.z - 1f);
        }

        AudioSource audioSource = ProjectileParticle.GetComponent<AudioSource>();

        if (PlayerPrefs.HasKey("SoundEffects"))
        {
            audioSource.volume = PlayerPrefs.GetFloat("SoundEffects");
            audioSource.Play();
        }
        else
        {
            audioSource.Play();
        }
    }

    public void CheckIfAttackWasInterrupted()
    {
        if(attackInterrupted)
        {
            if(repeatActionRoutine != null)
            {
                StopCoroutine(repeatActionRoutine);
                repeatActionRoutine = null;
            }

            EndedAttack();

            attackInterrupted = false;
        }
    }

    public void ResetDamaged()
    {
        damaged = false;

        if (statusEffectHolder.transform.childCount <= 0 && takingTurn)
        {
            if(!choseAction)
            {
                ChooseAction();
            }
        }
    }

    public void IncrementAttackIndex()
    {
        statusEffectChecker._EnemyActions[ActionIndex].AttackIndex++;

        didIncrementIndex = true;
    }

    public void DisableHitBox()
    {
        hitBox.enabled = false;
    }

    public void EnableHitBox()
    {
        hitBox.enabled = true;
    }

    public void PlayWalkAudio(AudioClip audioClip)
    {
        if (PlayerPrefs.HasKey("SoundEffects"))
        {
            audioSource.volume = PlayerPrefs.GetFloat("SoundEffects");
        }
        else
        {
            audioSource.volume = 1.0f;
        }

        audioSource.clip = audioClip;
        audioSource.Play();
    }

    public void ChangeAudioPitch(float pitch)
    {
        audioSource.pitch = pitch;
    }

    private IEnumerator RepeatAction()
    {
        if (battlePlayer.CurrentHealth <= 0) yield return null;

        if(!isDead)
        {
            EnableHitBox();

            battlePlayer.SetPerfectGuardToFalse();

            if(!damaged)
                animator.Play("Idle");

            didIncrementIndex = false;

            float t = 0;

            while (t < 0.85f)
            {
                if (attackInterrupted)
                {
                    t = 0;

                    if(repeatActionRoutine != null)
                    {
                        StopCoroutine(repeatActionRoutine);
                        repeatActionRoutine = null;
                    }
                    
                    yield return null;
                }
                else
                {
                    t += Time.deltaTime;
                }
                yield return new WaitForFixedUpdate();
            }

            if (statusEffectChecker._EnemyActions[ActionIndex].AttackIndex <= statusEffectChecker._EnemyActions[ActionIndex].AdditionalAttacks)
            {
                statusEffectChecker._EnemyActions[ActionIndex]._EnemyActions = statusEffectChecker._EnemyActions[ActionIndex]._EnemyActions;

                animator.Play(statusEffectChecker._EnemyActions[ActionIndex].AnimatorName);
            }
            else
            {
                ResetAttack();
            }
        }
    }
}