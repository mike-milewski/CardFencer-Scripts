using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public enum StatusEffect { NONE, Burns, Paralysis, Poison, HpRegen, CpRegen, StrengthUp, DefenseUp, StrengthDown, DefenseDown, CrackedShield, DullBlade, Undead, BurnsImmune, PoisonImmune, Invulnerable, Enrage, Thorns, Revive, 
                           DefenseRegen, CounterRegen, ParalysisImmune, UndeadImmune, Impervious, ShadowForm, Freeze, FreezeImmune };

public class StatusEffects : MonoBehaviour
{
    private BattleSystem battleSystem;

    [SerializeField]
    private StatusEffect statusEffect;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private TextMeshProUGUI statusTimeText;

    private TextMeshProUGUI statusNameText;

    [SerializeField]
    private Image statusEffectImage;

    private GameObject target;

    private ParticleSystem particleToPlay, paralysisParticle, poisonParticle, enrageParticle;

    [SerializeField]
    private int statusTime;

    private int statusChangePercentage;

    private bool shouldCheckStatus, removedStatChangeStatus, shouldntCheckOnDestroy, isNegativeStatus, isQuitting;

    private Coroutine removeEffectCoroutine, playParticleRoutine;

    public StatusEffect _statusEffect
    {
        get
        {
            return statusEffect;
        }
        set
        {
            statusEffect = value;
        }
    }

    public GameObject Target
    {
        get
        {
            return target;
        }
        set
        {
            target = value;
        }
    }

    public ParticleSystem ParticleToPlay
    {
        get
        {
            return particleToPlay;
        }
        set
        {
            particleToPlay = value;
        }
    }

    public Image StatusEffectImage
    {
        get
        {
            return statusEffectImage;
        }
        set
        {
            statusEffectImage = value;
        }
    }

    public TextMeshProUGUI StatusTimeText
    {
        get
        {
            return statusTimeText;
        }
        set
        {
            statusTimeText = value;
        }
    }

    public TextMeshProUGUI StatusNameText => statusNameText;

    public bool IsNegativeStatus => isNegativeStatus;

    public int StatusTime
    {
        get
        {
            return statusTime;
        }
        set
        {
            statusTime = value;
        }
    }

    public int StatusChangePercentage
    {
        get
        {
            return statusChangePercentage;
        }
        set
        {
            statusChangePercentage = value;
        }
    }

    public bool ShouldCheckStatus
    {
        get
        {
            return shouldCheckStatus;
        }
        set
        {
            shouldCheckStatus = value;
        }
    }

    public bool ShouldntCheckOnDestroy
    {
        get
        {
            return shouldntCheckOnDestroy;
        }
        set
        {
            shouldntCheckOnDestroy = value;
        }
    }

    private void Awake()
    {
        if(statusTime <= -1)
        {
            statusTimeText.text = "";
        }

        battleSystem = FindObjectOfType<BattleSystem>();
    }

    private void OnApplicationQuit()
    {
        isQuitting = true;
    }

    private void OnDisable()
    {
        if (isQuitting || GameManager.instance.ScreenTransition.effectFactor > 0) return;

        if(target.GetComponent<BattleEnemy>())
        {
            if (target.GetComponent<BattleEnemy>().CurrentHealth <= 0) return;
        }

        if(target.GetComponent<BattlePlayer>())
        {
            if (target.GetComponent<BattlePlayer>().CurrentHealth <= 0) return;
        }

        if(!shouldntCheckOnDestroy)
        {
            if (target != null)
            {
                if (!removedStatChangeStatus)
                {
                    if (target.GetComponent<BattleEnemy>())
                    {
                        if (statusEffect != StatusEffect.Paralysis)
                        {
                            target.GetComponent<BattleEnemy>().CheckForStatusEffectsBeforeTurn();
                            target.GetComponent<BattleEnemy>().StatusEffectIndex--;
                        }
                    }
                    else
                    {
                        if (statusEffect != StatusEffect.Paralysis)
                        {
                            target.GetComponent<BattlePlayer>().CheckForStatusEffectsBeforeTurn();
                            target.GetComponent<BattlePlayer>().StatusEffectIndex--;
                        }
                    }
                }

                if (target.GetComponent<BattlePlayer>())
                {
                    if (target.GetComponent<BattlePlayer>()._BattleSystem != null)
                    {
                        target.GetComponent<BattlePlayer>()._BattleSystem.StartWaitToChaneCharityNavigations();
                    }
                }

                if (playParticleRoutine != null)
                {
                    StopCoroutine(playParticleRoutine);
                    playParticleRoutine = null;
                }
            }
        }
        else
        {
            target.GetComponent<BattlePlayer>()._BattleSystem.StartWaitToChaneCharityNavigations();
        }
    }

    public void SetStatusEffectNameText()
    {
        StatusEffectPanel statusEffectPanel = GetComponentInParent<StatusEffectPanel>();

        statusNameText = statusEffectPanel.StatusNameText;
    }

    public void ShowPanel()
    {
        StatusEffectPanel statusEffectPanel = GetComponentInParent<StatusEffectPanel>();

        statusEffectPanel.ShowStatusEffectPanel();

        StatusEffectText(statusEffectPanel.StatusText, statusEffectPanel.StatusNameText);
    }

    public void HidePanel()
    {
        StatusEffectPanel statusEffectPanel = GetComponentInParent<StatusEffectPanel>();

        statusEffectPanel.HideStatusEffectPanel();
    }

    private TextMeshProUGUI StatusEffectText(TextMeshProUGUI statusText, TextMeshProUGUI statusNameText)
    {
        switch (statusEffect)
        {
            case (StatusEffect.Burns):
                statusNameText.text = "Burns";
                statusText.text = "Target suffers 15% damage at the start of their turn.";
                break;
            case (StatusEffect.Paralysis):
                statusNameText.text = "Paralysis";
                statusText.text = "Target is unable to act, skipping their turn.</size>";
                break;
            case (StatusEffect.Poison):
                statusNameText.text = "Poison";
                statusText.text = "Target suffers 10% damage at the start of their turn.</size>";
                break;
            case (StatusEffect.HpRegen):
                statusNameText.text = "HP Regen";
                statusText.text = "Target restores 10% HP at the start of their turn.</size>";
                break;
            case (StatusEffect.CpRegen):
                statusNameText.text = "CP Regen";
                statusText.text = "Target restores 10% CP at the start of their turn.</size>";
                break;
            case (StatusEffect.StrengthUp):
                statusNameText.text = "Strength Up";
                statusText.text = target.GetComponent<BattleEnemy>() ? "Increases strength by " + statusChangePercentage + "%." : "Increases strength by " + statusChangePercentage + "%.";
                break;
            case (StatusEffect.DefenseUp):
                statusNameText.text = "Defense Up";
                statusText.text = target.GetComponent<BattleEnemy>() ? "Increases defense by " + statusChangePercentage + "%." : "Increases defense by " + statusChangePercentage + "%.";
                break;
            case (StatusEffect.StrengthDown):
                statusNameText.text = "Strength Down";
                statusText.text = target.GetComponent<BattleEnemy>() ? "Decreases strength by " + statusChangePercentage + "%." : "Decreases strength by " + statusChangePercentage + "%.";
                break;
            case (StatusEffect.DefenseDown):
                statusNameText.text = "Defense Down";
                statusText.text = target.GetComponent<BattleEnemy>() ? "Decreases defense by " + statusChangePercentage + "%." : "Decreases defense by " + statusChangePercentage + "%.";
                break;
            case (StatusEffect.BurnsImmune):
                statusNameText.text = "Burn-Proof";
                statusText.text = "Prevents the Burns status.</size>";
                break;
            case (StatusEffect.ParalysisImmune):
                statusNameText.text = "Paralysis-Proof";
                statusText.text = "Prevents the Paralysis status.</size>";
                break;
            case (StatusEffect.UndeadImmune):
                statusNameText.text = "Undead-Proof";
                statusText.text = "Prevents the Undead status.</size>";
                break;
            case (StatusEffect.PoisonImmune):
                statusNameText.text = "Anti-Venom";
                statusText.text = "Prevents the Poison status.</size>";
                break;
            case (StatusEffect.Enrage):
                statusNameText.text = "Enrage";
                statusText.text = "Greatly increases strength. Cannot block or escape.</size>";
                break;
            case (StatusEffect.Thorns):
                statusNameText.text = "Thorns";
                statusText.text = "Enemies receive 50% of the damage they deal to you during a counter action.</size>";
                break;
            case (StatusEffect.DefenseRegen):
                statusNameText.text = "Guard Regen";
                statusText.text = "Recover 1 HP for each second you have your shield up.";
                break;
            case (StatusEffect.CrackedShield):
                statusNameText.text = "Cracked Shield";
                statusText.text = "The Guard Gauge becomes halved during guard actions.";
                break;
            case (StatusEffect.DullBlade):
                statusNameText.text = "Fragile Blade";
                statusText.text = "Reduces the success rate of the Counter Gauge.";
                break;
            case (StatusEffect.CounterRegen):
                statusNameText.text = "Counter Regen";
                statusText.text = "Recover 1 HP for each time you successfully counter.";
                break;
            case (StatusEffect.Impervious):
                statusNameText.text = "Impervious";
                statusText.text = "Prevents all negative status effects.";
                break;
            case (StatusEffect.Undead):
                statusNameText.text = "Undead";
                statusText.text = "HP healing effects deal that much damage to you instead.";
                break;
            case (StatusEffect.Revive):
                statusNameText.text = "Resurrect";
                statusText.text = "Fully restores HP if it would fall to zero once.";
                break;
            case (StatusEffect.ShadowForm):
                statusNameText.text = "Shadow Form";
                statusText.text = "Immune to counter actions, but you can no longer block.";
                break;
            case (StatusEffect.Freeze):
                statusNameText.text = "Freeze";
                statusText.text = "Reduces the number of attacks performed by 2. Cannot be reduced to less than 1.";
                break;
            case (StatusEffect.FreezeImmune):
                statusNameText.text = "Freeze-Proof";
                statusText.text = "Prevents the Freeze status.</size>";
                break;
            case (StatusEffect.Invulnerable):
                statusNameText.text = "Invulnerable";
                statusText.text = "Prevents damage and negative status effects.</size>";
                break;
        }

        return statusText;
    }

    public void UpdateStatusTime()
    {
        if(statusTime > -1)
        {
            statusTime--;

            statusTimeText.text = statusTime.ToString();

            if(statusTime <= 0)
            {
                statusTimeText.text = "0";

                removedStatChangeStatus = false;

                if(statusEffect != StatusEffect.Paralysis)
                {
                    removeEffectCoroutine = null;

                    removeEffectCoroutine = StartCoroutine("RemoveEffectCoroutine");
                }
            }
        }
    }

    public void BeginRemoveEffectRoutine()
    {
        RemoveEffect(true, true);
        Destroy(this.gameObject);
    }

    public void PlayAnimator()
    {
        if(gameObject != null)
        {
            if (gameObject.activeSelf && statusTime > 0)
                animator.Play("ScaleUp", -1, 0);
        }
    }

    public void ShowPanelController(GameObject selectObj)
    {
        if(InputManager.instance.ControllerPluggedIn)
        {
            ShowPanel();

            selectObj.gameObject.SetActive(true);
        }
    }

    public void HidePanelController(GameObject selectObj)
    {
        HidePanel();

        selectObj.gameObject.SetActive(false);
    }

    public void ApplyEffect()
    {
        if (isQuitting || GameManager.instance.ScreenTransition.effectFactor > 0) return;

        if(gameObject != null)
        {
            PlayAnimator();

            switch (statusEffect)
            {
                case (StatusEffect.Burns):
                    SetBurnsParticleToPlay();
                    playParticleRoutine = null;
                    if (this != null) playParticleRoutine = StartCoroutine(PlayParticle());
                    break;
                case (StatusEffect.Paralysis):
                    SetParalysisParticleToPlay();
                    playParticleRoutine = null;
                    if (this != null) playParticleRoutine = StartCoroutine(PlayParticle());
                    break;
                case (StatusEffect.Poison):
                    SetPoisonParticleToPlay();
                    playParticleRoutine = null;
                    if (this != null) playParticleRoutine = StartCoroutine(PlayParticle());
                    break;
                case (StatusEffect.HpRegen):
                    playParticleRoutine = null;
                    if (this != null) playParticleRoutine = StartCoroutine(PlayParticle());
                    break;
                case (StatusEffect.CpRegen):
                    playParticleRoutine = null;
                    if (this != null) playParticleRoutine = StartCoroutine(PlayParticle());
                    break;
                case (StatusEffect.StrengthUp):
                    playParticleRoutine = null;
                    if(this != null) playParticleRoutine = StartCoroutine(PlayParticle());
                    break;
                case (StatusEffect.DefenseUp):
                    playParticleRoutine = null;
                    if (this != null) playParticleRoutine = StartCoroutine(PlayParticle());
                    break;
                case (StatusEffect.StrengthDown):
                    playParticleRoutine = null;
                    if (this != null) playParticleRoutine = StartCoroutine(PlayParticle());
                    break;
                case (StatusEffect.DefenseDown):
                    playParticleRoutine = null;
                    if (this != null) playParticleRoutine = StartCoroutine(PlayParticle());
                    break;
                case (StatusEffect.BurnsImmune):
                    playParticleRoutine = null;
                    if (this != null) playParticleRoutine = StartCoroutine(PlayParticle());
                    break;
                case (StatusEffect.ParalysisImmune):
                    playParticleRoutine = null;
                    if (this != null) playParticleRoutine = StartCoroutine(PlayParticle());
                    break;
                case (StatusEffect.UndeadImmune):
                    playParticleRoutine = null;
                    if (this != null) playParticleRoutine = StartCoroutine(PlayParticle());
                    break;
                case (StatusEffect.PoisonImmune):
                    playParticleRoutine = null;
                    if (this != null) playParticleRoutine = StartCoroutine(PlayParticle());
                    break;
                case (StatusEffect.FreezeImmune):
                    playParticleRoutine = null;
                    if (this != null) playParticleRoutine = StartCoroutine(PlayParticle());
                    break;
                case (StatusEffect.Enrage):
                    SetEnrageParticleToPlay();
                    if (this != null) playParticleRoutine = StartCoroutine(PlayParticle());
                    break;
                case (StatusEffect.Thorns):
                    playParticleRoutine = null;
                    if (this != null) playParticleRoutine = StartCoroutine(PlayParticle());
                    break;
                case (StatusEffect.DefenseRegen):
                    target.GetComponent<BattlePlayer>().HasDefenseRegenStatus = true;
                    playParticleRoutine = null;
                    if (this != null) playParticleRoutine = StartCoroutine(PlayParticle());
                    break;
                case (StatusEffect.CounterRegen):
                    target.GetComponent<BattlePlayer>().HasCounterRegenStatus = true;
                    playParticleRoutine = null;
                    if (this != null) playParticleRoutine = StartCoroutine(PlayParticle());
                    break;
                case (StatusEffect.CrackedShield):
                    target.GetComponent<BattlePlayer>().HasCrackedShieldStatus = true;
                    playParticleRoutine = null;
                    if (this != null) playParticleRoutine = StartCoroutine(PlayParticle());
                    break;
                case (StatusEffect.DullBlade):
                    target.GetComponent<BattlePlayer>().HasDullBladeStatus = true;
                    playParticleRoutine = null;
                    if (this != null) playParticleRoutine = StartCoroutine(PlayParticle());
                    break;
                case (StatusEffect.Undead):
                    target.GetComponent<BattlePlayer>().IsUndead = true;
                    playParticleRoutine = null;
                    if (this != null) playParticleRoutine = StartCoroutine(PlayParticle());
                    break;
                case (StatusEffect.Freeze):
                    playParticleRoutine = null;
                    if (this != null) playParticleRoutine = StartCoroutine(PlayParticle());
                    break;
            }
        }
    }

    private void SetBurnsParticleToPlay()
    {
        particleToPlay = target.GetComponent<BattleEnemy>() ? target.GetComponent<BattleEnemy>().BurnsParticle : target.GetComponent<BattlePlayer>().BurnsParticle;
    }

    private void SetPoisonParticleToPlay()
    {
        poisonParticle = target.GetComponent<BattleEnemy>() ? target.GetComponent<BattleEnemy>().PoisonParticle : target.GetComponent<BattlePlayer>().PoisonParticle;
    }

    private void SetParalysisParticleToPlay()
    {
        paralysisParticle = target.GetComponent<BattleEnemy>() ? target.GetComponent<BattleEnemy>().ParalysisParticle : target.GetComponent<BattlePlayer>().ParalysisParticle;
    }

    private void SetEnrageParticleToPlay()
    {
        enrageParticle = target.GetComponent<BattlePlayer>().EnrageParticle;
    }

    private IEnumerator RemoveEffectCoroutine()
    {
        yield return new WaitForSeconds(1f);
        RemoveEffect(true, false);
        Destroy(this.gameObject);
    }

    public void RemoveEffect(bool skipTurn, bool shouldIgnore)
    {
        if(shouldCheckStatus)
        {
            switch (statusEffect)
            {
                case (StatusEffect.Burns):
                    if (target.GetComponent<BattleEnemy>())
                    {
                        target.GetComponent<BattleEnemy>().BurnsParticle.gameObject.SetActive(false);
                    }
                    else
                    {
                        target.GetComponent<BattlePlayer>().BurnsParticle.gameObject.SetActive(false);

                        if(target.GetComponent<BattlePlayer>().CurrentHealth > 0)
                        {
                            if (MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.NegativeBenefits))
                            {
                                battleSystem._StickerPowerHolder.CreateStickerMessage(StickerPower.NegativeBenefits);
                                AudioManager.instance.PlaySoundEffect(AudioManager.instance.HpHealAudio);
                                var hpParticle = target.GetComponent<BattlePlayer>().HpRegenParticle;
                                hpParticle.gameObject.SetActive(true);
                                hpParticle.transform.position = new Vector3(target.transform.position.x, target.transform.position.y + 0.3f, target.transform.position.z);
                                hpParticle.Play();
                                if (target.GetComponent<BattlePlayer>().IsUndead)
                                {
                                    CheckTarget(true);
                                }
                                else
                                {
                                    HpRegen();
                                }
                            }
                        }
                    }
                    break;
                case (StatusEffect.Poison):
                    if (target.GetComponent<BattleEnemy>())
                    {
                        target.GetComponent<BattleEnemy>().PoisonParticle.gameObject.SetActive(false);
                    }
                    else
                    {
                        target.GetComponent<BattlePlayer>().PoisonParticle.gameObject.SetActive(false);

                        if (target.GetComponent<BattlePlayer>().CurrentHealth > 0)
                        {
                            if (MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.NegativeBenefits))
                            {
                                battleSystem._StickerPowerHolder.CreateStickerMessage(StickerPower.NegativeBenefits);
                                AudioManager.instance.PlaySoundEffect(AudioManager.instance.HpHealAudio);
                                var hpParticle = target.GetComponent<BattlePlayer>().HpRegenParticle;
                                hpParticle.gameObject.SetActive(true);
                                hpParticle.transform.position = new Vector3(target.transform.position.x, target.transform.position.y + 0.3f, target.transform.position.z);
                                hpParticle.Play();
                                if (target.GetComponent<BattlePlayer>().IsUndead)
                                {
                                    CheckTarget(true);
                                }
                                else
                                {
                                    HpRegen();
                                }
                            }
                        }
                    }
                    break;
                case (StatusEffect.Undead):
                    if (target.GetComponent<BattlePlayer>().CurrentHealth > 0)
                    {
                        if (MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.NegativeBenefits))
                        {
                            battleSystem._StickerPowerHolder.CreateStickerMessage(StickerPower.NegativeBenefits);
                            AudioManager.instance.PlaySoundEffect(AudioManager.instance.HpHealAudio);
                            var hpParticle = target.GetComponent<BattlePlayer>().HpRegenParticle;
                            hpParticle.gameObject.SetActive(true);
                            hpParticle.transform.position = new Vector3(target.transform.position.x, target.transform.position.y + 0.3f, target.transform.position.z);
                            hpParticle.Play();
                            if (target.GetComponent<BattlePlayer>().IsUndead)
                            {
                                CheckTarget(true);
                            }
                            else
                            {
                                HpRegen();
                            }
                        }
                    }
                    break;
                case (StatusEffect.Paralysis):
                    if (target.GetComponent<BattleEnemy>())
                    {
                        target.GetComponent<BattleEnemy>().ParalysisParticle.gameObject.SetActive(false);
                    }
                    else
                    {
                        target.GetComponent<BattlePlayer>().ParalysisParticle.gameObject.SetActive(false);

                        battleSystem.TurnWasSkipped = true;

                        if (MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.NegativeBenefits))
                        {
                            battleSystem._StickerPowerHolder.CreateStickerMessage(StickerPower.NegativeBenefits);
                            AudioManager.instance.PlaySoundEffect(AudioManager.instance.HpHealAudio);
                            var hpParticle = target.GetComponent<BattlePlayer>().HpRegenParticle;
                            hpParticle.gameObject.SetActive(true);
                            hpParticle.transform.position = new Vector3(target.transform.position.x, target.transform.position.y + 0.3f, target.transform.position.z);
                            hpParticle.Play();
                            if (target.GetComponent<BattlePlayer>().IsUndead)
                            {
                                CheckTarget(true);
                            }
                            else
                            {
                                HpRegen();
                            }
                        }
                    }

                    if (target.GetComponent<BattlePlayer>())
                    {
                        BattleSystem battleSystem = FindObjectOfType<BattleSystem>();

                        target.GetComponent<BattlePlayer>().StatusEffectIndex = 0;

                        battleSystem.EnemyTurn();
                        battleSystem.ResetEnemyStatusIndex();
                        battleSystem.CheckEnemyStatus();
                    }
                    else if (target.GetComponent<BattleEnemy>())
                    {
                        target.GetComponent<BattleEnemy>().StatusEffectIndex = 0;

                        target.GetComponent<BattleEnemy>().EndedTurn(false, true);
                    }
                    break;
                case (StatusEffect.Thorns):
                    target.GetComponent<BattlePlayer>().HasThorns = false;
                    break;
                case (StatusEffect.Freeze):
                    target.GetComponent<BattleEnemy>().FreezeParticle.gameObject.SetActive(false);
                    target.GetComponent<BattleEnemy>().IsFrozen = false;
                    break;
            }
            PlayStatusRecoveryParticle(statusEffect == StatusEffect.Paralysis ? false : true, shouldIgnore);
            if (playParticleRoutine != null)
            {
                StopCoroutine(playParticleRoutine);
                playParticleRoutine = null;
            }
        }
        else
        {
            if(target.GetComponent<BattleEnemy>() && !removedStatChangeStatus)
            {
                PlayStatusRecoveryParticle(true, shouldIgnore);
            }
            else
            {
                PlayStatusRecoveryParticle(false, shouldIgnore);
            }
            
            if(target.GetComponent<BattlePlayer>())
            {
                if(!skipTurn)
                {
                    PlayStatusRecoveryParticle(false, shouldIgnore);
                }
                else
                {
                    PlayStatusRecoveryParticle(true, shouldIgnore);
                }
            }

            switch(statusEffect)
            {
                case (StatusEffect.StrengthUp):
                    RevertStrengthChanges(false);
                    break;
                case (StatusEffect.StrengthDown):
                    RevertStrengthChanges(true);
                    if(target.GetComponent<BattlePlayer>())
                    {
                        if (MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.NegativeBenefits))
                        {
                            battleSystem._StickerPowerHolder.CreateStickerMessage(StickerPower.NegativeBenefits);
                            AudioManager.instance.PlaySoundEffect(AudioManager.instance.HpHealAudio);
                            var hpParticle = target.GetComponent<BattlePlayer>().HpRegenParticle;
                            hpParticle.gameObject.SetActive(true);
                            hpParticle.transform.position = new Vector3(target.transform.position.x, target.transform.position.y + 0.3f, target.transform.position.z);
                            hpParticle.Play();
                            if (target.GetComponent<BattlePlayer>().IsUndead)
                            {
                                CheckTarget(true);
                            }
                            else
                            {
                                HpRegen();
                            }
                        }
                    }
                    break;
                case (StatusEffect.DefenseUp):
                    RevertDefenseChanges();
                    break;
                case (StatusEffect.DefenseDown):
                    RevertDefenseChanges();
                    if (target.GetComponent<BattlePlayer>())
                    {
                        if (MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.NegativeBenefits))
                        {
                            battleSystem._StickerPowerHolder.CreateStickerMessage(StickerPower.NegativeBenefits);
                            AudioManager.instance.PlaySoundEffect(AudioManager.instance.HpHealAudio);
                            var hpParticle = target.GetComponent<BattlePlayer>().HpRegenParticle;
                            hpParticle.gameObject.SetActive(true);
                            hpParticle.transform.position = new Vector3(target.transform.position.x, target.transform.position.y + 0.3f, target.transform.position.z);
                            hpParticle.Play();
                            if (target.GetComponent<BattlePlayer>().IsUndead)
                            {
                                CheckTarget(true);
                            }
                            else
                            {
                                HpRegen();
                            }
                        }
                    }
                    break;
                case (StatusEffect.DefenseRegen):
                    target.GetComponent<BattlePlayer>().HasDefenseRegenStatus = false;
                    break;
                case (StatusEffect.CounterRegen):
                    target.GetComponent<BattlePlayer>().HasCounterRegenStatus = false;
                    break;
                case (StatusEffect.CrackedShield):
                    target.GetComponent<BattlePlayer>().HasCrackedShieldStatus = false;
                    target.GetComponent<BattlePlayer>().ResetGuardGauge();
                    if (MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.NegativeBenefits))
                    {
                        battleSystem._StickerPowerHolder.CreateStickerMessage(StickerPower.NegativeBenefits);
                        AudioManager.instance.PlaySoundEffect(AudioManager.instance.HpHealAudio);
                        var hpParticle = target.GetComponent<BattlePlayer>().HpRegenParticle;
                        hpParticle.gameObject.SetActive(true);
                        hpParticle.transform.position = new Vector3(target.transform.position.x, target.transform.position.y + 0.3f, target.transform.position.z);
                        hpParticle.Play();
                        if (target.GetComponent<BattlePlayer>().IsUndead)
                        {
                            CheckTarget(true);
                        }
                        else
                        {
                            HpRegen();
                        }
                    }
                    break;
                case (StatusEffect.DullBlade):
                    target.GetComponent<BattlePlayer>().HasDullBladeStatus = false;
                    target.GetComponent<BattlePlayer>().ResetCounterAttack();
                    if (MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.NegativeBenefits))
                    {
                        battleSystem._StickerPowerHolder.CreateStickerMessage(StickerPower.NegativeBenefits);
                        AudioManager.instance.PlaySoundEffect(AudioManager.instance.HpHealAudio);
                        var hpParticle = target.GetComponent<BattlePlayer>().HpRegenParticle;
                        hpParticle.gameObject.SetActive(true);
                        hpParticle.transform.position = new Vector3(target.transform.position.x, target.transform.position.y + 0.3f, target.transform.position.z);
                        hpParticle.Play();
                        if(target.GetComponent<BattlePlayer>().IsUndead)
                        {
                            CheckTarget(true);
                        }
                        else
                        {
                            HpRegen();
                        }
                    }
                    break;
                case (StatusEffect.Undead):
                    target.GetComponent<BattlePlayer>().IsUndead = false;
                    if (target.GetComponent<BattlePlayer>().CurrentHealth > 0)
                    {
                        if (MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.NegativeBenefits))
                        {
                            battleSystem._StickerPowerHolder.CreateStickerMessage(StickerPower.NegativeBenefits);
                            AudioManager.instance.PlaySoundEffect(AudioManager.instance.HpHealAudio);
                            var hpParticle = target.GetComponent<BattlePlayer>().HpRegenParticle;
                            hpParticle.gameObject.SetActive(true);
                            hpParticle.transform.position = new Vector3(target.transform.position.x, target.transform.position.y + 0.3f, target.transform.position.z);
                            hpParticle.Play();
                            HpRegen();
                        }
                    }
                    break;
                case (StatusEffect.Revive):
                    target.GetComponent<BattlePlayer>().HasSecondChance = false;
                    break;
                case (StatusEffect.Invulnerable):
                    target.GetComponent<BattlePlayer>().HasInvulnerable = false;
                    break;
            }

            if (playParticleRoutine != null)
            {
                StopCoroutine(playParticleRoutine);
                playParticleRoutine = null;
            }
        }
    }

    public void RemoveAllEffectsPostBattle()
    {
        switch (statusEffect)
        {
            case (StatusEffect.Burns):
                if(target.GetComponent<BattlePlayer>())
                {
                    target.GetComponent<BattlePlayer>().BurnsParticle.gameObject.SetActive(false);
                }
                else
                {
                    target.GetComponent<BattleEnemy>().BurnsParticle.gameObject.SetActive(false);
                }
                break;
            case (StatusEffect.Poison):
                if (target.GetComponent<BattlePlayer>())
                {
                    target.GetComponent<BattlePlayer>().PoisonParticle.gameObject.SetActive(false);
                }
                else
                {
                    target.GetComponent<BattleEnemy>().PoisonParticle.gameObject.SetActive(false);
                }
                break;
            case (StatusEffect.Paralysis):
                if (target.GetComponent<BattlePlayer>())
                {
                    target.GetComponent<BattlePlayer>().ParalysisParticle.gameObject.SetActive(false);
                }
                else
                {
                    target.GetComponent<BattleEnemy>().ParalysisParticle.gameObject.SetActive(false);
                }
                break;
            case (StatusEffect.Enrage):
                target.GetComponent<BattlePlayer>().EnrageParticle.gameObject.SetActive(false);
                break;
            case (StatusEffect.HpRegen):
                break;
            case (StatusEffect.CpRegen):
                break;
            case (StatusEffect.Thorns):
                break;
        }
    }

    public void RemoveSpecificEffect(StatusEffect effect)
    {
        if(statusEffect == effect)
        {
            RemoveEffect(false, false);
            if (target.GetComponent<BattlePlayer>()._BattleSystem != null)
            {
                target.GetComponent<BattlePlayer>()._BattleSystem.StartWaitToChaneCharityNavigations();
            }
            Destroy(this.gameObject);
        }
    }

    private void PlayStatusRecoveryParticle(bool changeTurn, bool shouldIgnore)
    {
        if (shouldIgnore) return;

        if (target.GetComponent<BattleEnemy>())
        {
            target.GetComponent<BattleEnemy>().PlayStatusRemovalParticle(changeTurn);
        }
        else
        {
            target.GetComponent<BattlePlayer>().PlayStatusRemovalParticle(changeTurn);
        }
    }

    public IEnumerator WaitForDamageAnimation()
    {
        yield return new WaitForSeconds(1f);

        if(gameObject.activeSelf)
        {
            StartCoroutine("ReapplyCheck");
        }
        else
        {
            StopCoroutine(ReapplyCheck());
        }
    }

    public void CheckStatusCoroutine()
    {
        if(gameObject.activeSelf)
        {
            StartCoroutine("ReapplyCheck");
        }
        else
        {
            StopCoroutine(ReapplyCheck());
        }
    }

    public IEnumerator ReapplyCheck()
    {
        if(statusTime == 0)
        {
            yield return new WaitForSeconds(1f);
            RemoveEffect(true, false);
            Destroy(this.gameObject);
        }
        else
        {
            if (target.GetComponent<BattleEnemy>())
            {
                target.GetComponent<BattleEnemy>().CheckForStatusEffectsBeforeTurn();
            }
            else
            {
                target.GetComponent<BattlePlayer>().CheckForStatusEffectsBeforeTurn();
            }
        }
    }

    private void CheckTarget(bool hasUndead)
    {
        int damageValue = 0;

        if(hasUndead)
        {
            damageValue = 10;
        }
        else
        {
            damageValue = statusEffect == StatusEffect.Poison ? 10 : 15;
        }

        float percentage = target.GetComponent<BattleEnemy>() ? Mathf.Round(((float)target.GetComponent<BattleEnemy>()._EnemyStats.health / 100) * damageValue) : Mathf.Round(((float)target.GetComponent<BattlePlayer>()._mainCharacterStats.maximumHealth / 100) * damageValue);

        if (target.GetComponent<BattleEnemy>())
        {
            target.GetComponent<BattleEnemy>().TakeDamage((int)percentage, true, true, false);

            if(target.GetComponent<BattleEnemy>().CurrentHealth > 0)
            {
                StartCoroutine("WaitForDamageAnimation");
            }
            else
            {
                BattleSystem battleSystem = FindObjectOfType<BattleSystem>();

                if(battleSystem.Enemies.Count > 1)
                {
                    target.GetComponent<BattleEnemy>().EndedTurn(true, false);
                } 
            }
        }
        else
        {
            target.GetComponent<BattlePlayer>().SetBattleNumberPosition();

            target.GetComponent<BattlePlayer>().TakeDamage((int)percentage, true, true);

            target.GetComponent<BattlePlayer>().IsDamaged = true;

            if(target.GetComponent<BattlePlayer>().CurrentHealth > 0)
               StartCoroutine("ReapplyCheck");
        }

        if(statusEffect == StatusEffect.Burns)
           AudioManager.instance.PlaySoundEffect(AudioManager.instance.FireAudio);
    }

    public void SetStatusParticle()
    {
        switch(statusEffect)
        {
            case (StatusEffect.Burns):
                isNegativeStatus = true;
                if (target.GetComponent<BattleEnemy>())
                {
                    target.GetComponent<BattleEnemy>().BurnsParticle.gameObject.SetActive(true);
                }
                else
                {
                    target.GetComponent<BattlePlayer>().BurnsParticle.gameObject.SetActive(true);
                }
                break;
            case (StatusEffect.Poison):
                isNegativeStatus = true;
                if (target.GetComponent<BattleEnemy>())
                {
                    target.GetComponent<BattleEnemy>().PoisonParticle.gameObject.SetActive(true);
                }
                else
                {
                    target.GetComponent<BattlePlayer>().PoisonParticle.gameObject.SetActive(true);
                }
                break;
            case (StatusEffect.Paralysis):
                isNegativeStatus = true;
                if (target.GetComponent<BattleEnemy>())
                {
                    target.GetComponent<BattleEnemy>().ParalysisParticle.gameObject.SetActive(true);
                }
                else
                {
                    target.GetComponent<BattlePlayer>().ParalysisParticle.gameObject.SetActive(true);
                }
                break;
            case (StatusEffect.Enrage):
                target.GetComponent<BattlePlayer>().EnrageParticle.gameObject.SetActive(true);
                if(PlayerPrefs.HasKey("SoundEffects"))
                {
                    target.GetComponent<BattlePlayer>().EnrageParticle.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SoundEffects");
                    target.GetComponent<BattlePlayer>().EnrageParticle.GetComponent<AudioSource>().Play();
                }
                else
                {
                    target.GetComponent<BattlePlayer>().EnrageParticle.GetComponent<AudioSource>().Play();
                }
                break;
            case (StatusEffect.Freeze):
                isNegativeStatus = true;
                if (target.GetComponent<BattleEnemy>())
                {
                    target.GetComponent<BattleEnemy>().FreezeParticle.gameObject.SetActive(true);
                }
                break;
            case (StatusEffect.Undead):
                isNegativeStatus = true;
                break;
        }
    }

    private IEnumerator PlayParticle()
    {
        if(gameObject != null)
        {
            switch (statusEffect)
            {
                case (StatusEffect.Burns):
                    AudioManager.instance.PlaySoundEffect(AudioManager.instance.FireAudio);

                    ParticleSystem.MainModule pMain = particleToPlay.main;

                    pMain.startSize = new ParticleSystem.MinMaxCurve(2f, 2.5f);

                    yield return new WaitForSeconds(1f);

                    pMain.startSize = new ParticleSystem.MinMaxCurve(0.7f, 1f);
                    CheckTarget(false);
                    break;
                case (StatusEffect.Poison):
                    if (PlayerPrefs.HasKey("SoundEffects"))
                    {
                        poisonParticle.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SoundEffects");
                        poisonParticle.GetComponent<AudioSource>().Play();
                    }
                    else
                    {
                        poisonParticle.GetComponent<AudioSource>().Play();
                    }

                    ParticleSystem.MainModule pMain3 = poisonParticle.main;

                    pMain3.startSize = 0.8f;

                    yield return new WaitForSeconds(1f);

                    pMain3.startSize = 0.5f;
                    CheckTarget(false);
                    break;
                case (StatusEffect.Paralysis):
                    AudioManager.instance.PlaySoundEffect(AudioManager.instance.ParalysisAudio);

                    ParticleSystem.MainModule pMain2 = paralysisParticle.main;

                    pMain2.startSize = new ParticleSystem.MinMaxCurve(0.85f, 0.95f);

                    yield return new WaitForSeconds(1f);

                    pMain2.startSize = new ParticleSystem.MinMaxCurve(0.65f, 0.75f);
                    RemoveEffect(true, false);
                    Destroy(this.gameObject);
                    break;
                case (StatusEffect.HpRegen):
                    AudioManager.instance.PlaySoundEffect(AudioManager.instance.HpHealAudio);
                    var hpParticle = target.GetComponent<BattleEnemy>() ? target.GetComponent<BattleEnemy>().HpRegenParticle : target.GetComponent<BattlePlayer>().HpRegenParticle;
                    hpParticle.gameObject.SetActive(true);
                    hpParticle.transform.position = new Vector3(target.transform.position.x, target.transform.position.y + 0.3f, target.transform.position.z);
                    hpParticle.Play();
                    yield return new WaitForSeconds(1f);
                    if(target.GetComponent<BattlePlayer>())
                    {
                        if(target.GetComponent<BattlePlayer>().IsUndead)
                        {
                            CheckTarget(true);
                        }
                        else
                        {
                            HpRegen();
                        }
                    }
                    else
                    {
                        HpRegen();
                    }
                    break;
                case (StatusEffect.CpRegen):
                    AudioManager.instance.PlaySoundEffect(AudioManager.instance.CpHealAudio);
                    var cpParticle = target.GetComponent<BattlePlayer>().CpRegenParticle;
                    cpParticle.gameObject.SetActive(true);
                    cpParticle.Play();
                    yield return new WaitForSeconds(1f);
                    CpRegen();
                    break;
                case (StatusEffect.Thorns):
                    yield return new WaitForSeconds(1f);
                    StartCoroutine("ReapplyCheck");
                    break;
                case (StatusEffect.StrengthUp):
                    yield return new WaitForSeconds(1f);
                    StartCoroutine("ReapplyCheck");
                    break;
                case (StatusEffect.StrengthDown):
                    yield return new WaitForSeconds(1f);
                    StartCoroutine("ReapplyCheck");
                    break;
                case (StatusEffect.DefenseUp):
                    yield return new WaitForSeconds(1f);
                    StartCoroutine("ReapplyCheck");
                    break;
                case (StatusEffect.DefenseDown):
                    yield return new WaitForSeconds(1f);
                    StartCoroutine("ReapplyCheck");
                    break;
                case (StatusEffect.DefenseRegen):
                    yield return new WaitForSeconds(1f);
                    StartCoroutine("ReapplyCheck");
                    break;
                case (StatusEffect.CounterRegen):
                    yield return new WaitForSeconds(1f);
                    StartCoroutine("ReapplyCheck");
                    break;
                case (StatusEffect.BurnsImmune):
                    if (statusTime < 0)
                    {
                        yield return new WaitForSeconds(1f);
                        StartCoroutine("ReapplyCheck");
                    }
                    else
                    {
                        yield return new WaitForSeconds(1f);
                        StartCoroutine("ReapplyCheck");
                    }
                    break;
                case (StatusEffect.ParalysisImmune):
                    if (statusTime < 0)
                    {
                        yield return new WaitForSeconds(1f);
                        StartCoroutine("ReapplyCheck");
                    }
                    else
                    {
                        yield return new WaitForSeconds(1f);
                        StartCoroutine("ReapplyCheck");
                    }
                    break;
                case (StatusEffect.PoisonImmune):
                    if(statusTime < 0)
                    {
                        yield return new WaitForSeconds(1f);
                        StartCoroutine("ReapplyCheck");
                    }
                    else
                    {
                        yield return new WaitForSeconds(1f);
                        StartCoroutine("ReapplyCheck");
                    }
                    break;
                case (StatusEffect.FreezeImmune):
                    if (statusTime < 0)
                    {
                        yield return new WaitForSeconds(1f);
                        StartCoroutine("ReapplyCheck");
                    }
                    else
                    {
                        yield return new WaitForSeconds(1f);
                        StartCoroutine("ReapplyCheck");
                    }
                    break;
                case (StatusEffect.UndeadImmune):
                    yield return new WaitForSeconds(1f);
                    StartCoroutine("ReapplyCheck");
                    break;
                case (StatusEffect.Undead):
                    yield return new WaitForSeconds(1f);
                    StartCoroutine("ReapplyCheck");
                    break;
                case (StatusEffect.CrackedShield):
                    yield return new WaitForSeconds(1f);
                    StartCoroutine("ReapplyCheck");
                    break;
                case (StatusEffect.DullBlade):
                    yield return new WaitForSeconds(1f);
                    StartCoroutine("ReapplyCheck");
                    break;
                case (StatusEffect.Freeze):
                    yield return new WaitForSeconds(1f);
                    StartCoroutine("ReapplyCheck");
                    break;
                case (StatusEffect.Invulnerable):
                    yield return new WaitForSeconds(1f);
                    StartCoroutine("ReapplyCheck");
                    break;
                case (StatusEffect.Enrage):
                    yield return new WaitForSeconds(1f);
                    StartCoroutine("ReapplyCheck");
                    break;
            }
        }
    }

    private void StrengthUp()
    {
        if (target.GetComponent<BattlePlayer>())
        {
            BattlePlayer battlePlayer = target.GetComponent<BattlePlayer>();
            for (int i = 0; i < battlePlayer.StatusEffectHolder.transform.childCount; i++)
            {
                StatusEffects status = battlePlayer.StatusEffectHolder.transform.GetChild(i).GetComponent<StatusEffects>();
                if (status.statusEffect == StatusEffect.StrengthDown)
                {
                    status.RemoveEffect(false, false);
                    status.removedStatChangeStatus = true;
                    Destroy(status.gameObject);
                }
            }
        }
        else if (target.GetComponent<BattleEnemy>())
        {
            BattleEnemy battleEnemy = target.GetComponent<BattleEnemy>();
            for (int i = 0; i < battleEnemy.StatusEffectHolder.transform.childCount; i++)
            {
                StatusEffects status = battleEnemy.StatusEffectHolder.transform.GetChild(i).GetComponent<StatusEffects>();
                if (status.statusEffect == StatusEffect.StrengthDown)
                {
                    status.removedStatChangeStatus = true;
                    status.RemoveEffect(false, false);
                    Destroy(status.gameObject);
                }
            }
        }

        float percentage = target.GetComponent<BattleEnemy>() ? (target.GetComponent<BattleEnemy>().EnemyStrength / (float)100) * statusChangePercentage :
                           (target.GetComponent<BattlePlayer>()._mainCharacterStats.strength / (float)100) * statusChangePercentage;

        if(percentage < 1)
        {
            percentage = 1;
        }

        percentage = Mathf.Round(percentage);

        if (target.GetComponent<BattleEnemy>())
        {
            target.GetComponent<BattleEnemy>().EnemyStrength += (int)percentage;
        }
        else
        {
            target.GetComponent<BattlePlayer>().StrengthBoost += (int)percentage;
            target.GetComponent<BattlePlayer>().PlayerStrength += (int)percentage;
            target.GetComponent<BattlePlayer>().StrengthPercentage = statusChangePercentage;

            target.GetComponent<BattlePlayer>()._BattleSystem.SetCardStrength(statusChangePercentage, true);
            target.GetComponent<BattlePlayer>()._BattleSystem.ChangeBasicAttackDamageValue();
        }
    }

    private void StrengthDown()
    {
        isNegativeStatus = true;

        if (target.GetComponent<BattlePlayer>())
        {
            BattlePlayer battlePlayer = target.GetComponent<BattlePlayer>();
            for(int i = 0; i < battlePlayer.StatusEffectHolder.transform.childCount; i++)
            {
                StatusEffects status = battlePlayer.StatusEffectHolder.transform.GetChild(i).GetComponent<StatusEffects>();
                if(status.statusEffect == StatusEffect.StrengthUp)
                {
                    status.RemoveEffect(false, false);
                    status.removedStatChangeStatus = true;
                    Destroy(status.gameObject);
                }
            }
        }
        else if(target.GetComponent<BattleEnemy>())
        {
            BattleEnemy battleEnemy = target.GetComponent<BattleEnemy>();
            for (int i = 0; i < battleEnemy.StatusEffectHolder.transform.childCount; i++)
            {
                StatusEffects status = battleEnemy.StatusEffectHolder.transform.GetChild(i).GetComponent<StatusEffects>();
                if (status.statusEffect == StatusEffect.StrengthUp)
                {
                    status.removedStatChangeStatus = true;
                    status.RemoveEffect(false, false);
                    Destroy(status.gameObject);
                }
            }
        }

        float percentage = target.GetComponent<BattleEnemy>() ? (target.GetComponent<BattleEnemy>().EnemyStrength / (float)100) * statusChangePercentage : 
                           (target.GetComponent<BattlePlayer>()._mainCharacterStats.strength / (float)100) * statusChangePercentage;

        if (percentage < 1)
        {
            percentage = 1;
        }

        percentage = Mathf.Round(percentage);

        if (target.GetComponent<BattleEnemy>())
        {
            if(target.GetComponent<BattleEnemy>().EnemyStrength > 0)
               target.GetComponent<BattleEnemy>().EnemyStrength -= (int)percentage;
        }
        else
        {
            target.GetComponent<BattlePlayer>().StrengthBoost -= (int)percentage;
            target.GetComponent<BattlePlayer>().StrengthPercentage = -statusChangePercentage;

            if (target.GetComponent<BattlePlayer>().PlayerStrength > 0)
               target.GetComponent<BattlePlayer>().PlayerStrength -= (int)percentage;

            target.GetComponent<BattlePlayer>()._BattleSystem.SetCardStrength(statusChangePercentage, false);
            target.GetComponent<BattlePlayer>()._BattleSystem.ChangeBasicAttackDamageValue();
        }
    }

    private void DefenseUp()
    {
        if (target.GetComponent<BattlePlayer>())
        {
            BattlePlayer battlePlayer = target.GetComponent<BattlePlayer>();
            for (int i = 0; i < battlePlayer.StatusEffectHolder.transform.childCount; i++)
            {
                StatusEffects status = battlePlayer.StatusEffectHolder.transform.GetChild(i).GetComponent<StatusEffects>();
                if (status.statusEffect == StatusEffect.DefenseDown)
                {
                    status.RemoveEffect(false, false);
                    status.removedStatChangeStatus = true;
                    Destroy(status.gameObject);
                }
            }
        }
        else if (target.GetComponent<BattleEnemy>())
        {
            BattleEnemy battleEnemy = target.GetComponent<BattleEnemy>();
            for (int i = 0; i < battleEnemy.StatusEffectHolder.transform.childCount; i++)
            {
                StatusEffects status = battleEnemy.StatusEffectHolder.transform.GetChild(i).GetComponent<StatusEffects>();
                if (status.statusEffect == StatusEffect.DefenseDown)
                {
                    status.removedStatChangeStatus = true;
                    status.RemoveEffect(false, false);
                    Destroy(status.gameObject);
                }
            }
        }

        float percentage = target.GetComponent<BattleEnemy>() ? (target.GetComponent<BattleEnemy>().EnemyDefense / 100) * statusChangePercentage :
                           (target.GetComponent<BattlePlayer>()._mainCharacterStats.defense / (float)100) * statusChangePercentage;

        if (percentage < 1)
        {
            percentage = 1;
        }

        percentage = Mathf.Round(percentage);

        if (target.GetComponent<BattleEnemy>())
        {
            target.GetComponent<BattleEnemy>().EnemyDefense += (int)percentage;
        }
        else
        {
            target.GetComponent<BattlePlayer>().DefenseBoost += (int)percentage;
            target.GetComponent<BattlePlayer>().PlayerDefense += (int)percentage;
        }
    }

    private void DefenseDown()
    {
        isNegativeStatus = true;

        if (target.GetComponent<BattlePlayer>())
        {
            BattlePlayer battlePlayer = target.GetComponent<BattlePlayer>();
            for (int i = 0; i < battlePlayer.StatusEffectHolder.transform.childCount; i++)
            {
                StatusEffects status = battlePlayer.StatusEffectHolder.transform.GetChild(i).GetComponent<StatusEffects>();
                if (status.statusEffect == StatusEffect.DefenseUp)
                {
                    status.RemoveEffect(false, false);
                    status.removedStatChangeStatus = true;
                    Destroy(status.gameObject);
                }
            }
        }
        else if (target.GetComponent<BattleEnemy>())
        {
            BattleEnemy battleEnemy = target.GetComponent<BattleEnemy>();
            for (int i = 0; i < battleEnemy.StatusEffectHolder.transform.childCount; i++)
            {
                StatusEffects status = battleEnemy.StatusEffectHolder.transform.GetChild(i).GetComponent<StatusEffects>();
                if (status.statusEffect == StatusEffect.DefenseUp)
                {
                    status.removedStatChangeStatus = true;
                    status.RemoveEffect(false, false);
                    Destroy(status.gameObject);
                }
            }
        }

        float percentage = target.GetComponent<BattleEnemy>() ? (target.GetComponent<BattleEnemy>().EnemyDefense / (float)100) * statusChangePercentage : 
                           (target.GetComponent<BattlePlayer>()._mainCharacterStats.defense / (float)100) * statusChangePercentage;

        if (percentage < 1)
        {
            percentage = 1;
        }

        percentage = Mathf.Round(percentage);

        if (target.GetComponent<BattleEnemy>())
        {
            if(target.GetComponent<BattleEnemy>().EnemyDefense > 0)
               target.GetComponent<BattleEnemy>().EnemyDefense -= (int)percentage;
        }
        else
        {
            target.GetComponent<BattlePlayer>().DefenseBoost -= (int)percentage;

            if(target.GetComponent<BattlePlayer>().PlayerDefense > 0)
               target.GetComponent<BattlePlayer>().PlayerDefense -= (int)percentage;
        }
    }

    private void HpRegen()
    {
        int healValue = 10;

        float percentage = target.GetComponent<BattleEnemy>() ? Mathf.Round(((float)target.GetComponent<BattleEnemy>()._EnemyStats.health / 100) * healValue) : Mathf.Round(((float)target.GetComponent<BattlePlayer>()._mainCharacterStats.maximumHealth / 100) * healValue);

        if (target.GetComponent<BattleEnemy>())
        {
            target.GetComponent<BattleEnemy>().HealHealth((int)percentage, true);

            StartCoroutine("ReapplyCheck");
        }
        else
        {
            target.GetComponent<BattlePlayer>().HealHealth((int)percentage, true);

            StartCoroutine("ReapplyCheck");
        }
    }

    private void CpRegen()
    {
        int healValue = 10;

        float percentage = Mathf.Round(((float)target.GetComponent<BattlePlayer>()._mainCharacterStats.maximumHealth / 100) * healValue);

        target.GetComponent<BattlePlayer>().HealCardPoints((int)percentage, true);

        StartCoroutine("ReapplyCheck");
    }

    public void CheckStatusChange()
    {
        switch(statusEffect)
        {
            case (StatusEffect.StrengthUp):
                StrengthUp();
                break;
            case (StatusEffect.StrengthDown):
                StrengthDown();
                break;
            case (StatusEffect.DefenseUp):
                DefenseUp();
                break;
            case (StatusEffect.DefenseDown):
                DefenseDown();
                break;
            case (StatusEffect.Paralysis):
                if(target.GetComponent<BattlePlayer>())
                {
                    target.GetComponent<BattlePlayer>().IsParalyzed = true;
                }
                break;
            case (StatusEffect.DefenseRegen):
                target.GetComponent<BattlePlayer>().HasDefenseRegenStatus = true;
                break;
            case (StatusEffect.CounterRegen):
                target.GetComponent<BattlePlayer>().HasCounterRegenStatus = true;
                break;
            case (StatusEffect.CrackedShield):
                target.GetComponent<BattlePlayer>().HasCrackedShieldStatus = true;
                break;
            case (StatusEffect.DullBlade):
                target.GetComponent<BattlePlayer>().HasDullBladeStatus = true;
                break;
            case (StatusEffect.Undead):
                target.GetComponent<BattlePlayer>().IsUndead = true;
                break;
            case (StatusEffect.Revive):
                target.GetComponent<BattlePlayer>().HasSecondChance = true;
                shouldntCheckOnDestroy = true;
                break;
            case (StatusEffect.Thorns):
                target.GetComponent<BattlePlayer>().HasThorns = true;
                break;
            case (StatusEffect.Freeze):
                target.GetComponent<BattleEnemy>().IsFrozen = true;
                break;
            case (StatusEffect.Invulnerable):
                target.GetComponent<BattlePlayer>().HasInvulnerable = true;
                break;
        }
    }

    private void RevertStrengthChanges(bool increase)
    {
        if (target.GetComponent<BattleEnemy>())
        {
            target.GetComponent<BattleEnemy>().EnemyStrength = target.GetComponent<BattleEnemy>()._EnemyStats.strength;
        }
        else
        {
            if(MenuController.instance._StickerPowerManager.CheckStartOfBattleStickerPower(StickerPower.StrengthUp))
            {
                target.GetComponent<BattlePlayer>().StrengthBoost = 2;
                target.GetComponent<BattlePlayer>().PlayerStrength = target.GetComponent<BattlePlayer>()._mainCharacterStats.strength;
            }
            else
            {
                target.GetComponent<BattlePlayer>().StrengthBoost = 0;
                target.GetComponent<BattlePlayer>().PlayerStrength = target.GetComponent<BattlePlayer>()._mainCharacterStats.strength;
            }

            float percentage = target.GetComponent<BattleEnemy>() ? (target.GetComponent<BattleEnemy>().EnemyStrength / (float)100) * statusChangePercentage :
                               (target.GetComponent<BattlePlayer>()._mainCharacterStats.strength / (float)100) * statusChangePercentage;

            if (percentage < 1)
            {
                percentage = 1;
            }

            percentage = Mathf.Round(percentage);

            target.GetComponent<BattlePlayer>().StrengthPercentage = 0;

            target.GetComponent<BattlePlayer>()._BattleSystem.ResetCardStrength();
            target.GetComponent<BattlePlayer>()._BattleSystem.ChangeBasicAttackDamageValue();
        }
    }

    private void RevertDefenseChanges()
    {
        if (target.GetComponent<BattleEnemy>())
        {
            target.GetComponent<BattleEnemy>().EnemyDefense = target.GetComponent<BattleEnemy>()._EnemyStats.defense;
        }
        else
        {
            target.GetComponent<BattlePlayer>().DefenseBoost = 0;
            target.GetComponent<BattlePlayer>().PlayerDefense = target.GetComponent<BattlePlayer>()._mainCharacterStats.defense;
        }
    }

    public void EnableParticleEffect()
    {
        particleToPlay.gameObject.SetActive(true);
    }

    public void DisableParticleEffect()
    {
        particleToPlay.gameObject.SetActive(false);
    }
}