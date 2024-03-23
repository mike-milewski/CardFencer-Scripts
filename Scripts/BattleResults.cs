using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Steamworks;

public class BattleResults : MonoBehaviour
{
    [SerializeField]
    private MainCharacterStats mainCharacterStats;

    [SerializeField]
    private PlayerMenuInfo playerMenuInfo;

    [SerializeField]
    private BattleSystem battleSystem;

    [SerializeField]
    private ResultsPanelAnimationEvent resultsPanelAnimationEvent;

    [SerializeField]
    private GameObject tookNoDamageBonusPanel, quickVictoryBonusPanel, nearDeathExperienceBonusPanel, armorRankUpParticle, noviceArmor, veteranArmor, eliteArmor, endCreditsObject;

    [SerializeField]
    private EndCredits endCredits;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private Animator experienceBarAnimator, levelUpAnimator, playerLevelTextAnimator, statUpgradeAnimator, statUpgradeTextAnimator, expAndMoneyPanelAnimator, rankAnimator, moneyPanelAnimator, itemSpoilsPanelAnimator,
                     demoCompletionAnimator, fadeScreenAnimator;

    [SerializeField]
    private ParticleSystem levelUpParticle;

    [SerializeField]
    private TextMeshProUGUI gainedExperienceText, gainedMoneyText, playerExpGainText, playerLevelText, moneyText, extraExperienceText, extraMoneyText, noDamageTakenBonusText, quickVictoryBonusText, nearDeathBonusText,
                            postGameTitleText, postGameMessageText;

    [SerializeField]
    private Text shinyExpBonusTextOne, shinyGoldTextOne, shinyExpBonusTextTwo, shinyGoldTextTwo, shinyExpBonusTextThree, shinyGoldTextThree;

    [SerializeField]
    private Image playerExperienceFillImage, rankImage, extraExperienceBanner, extraGoldBanner;

    [SerializeField]
    private StatUpgrade statToBeSelectedForController;

    [SerializeField]
    private RankBonusBattleInformation[] rankBonusInformation;

    private Coroutine expRoutine, moneyRoutine;

    [SerializeField]
    private string endOfDemoMessage, endOfGameMessage, endOfDemoTitle, endOfGameTitle;

    [SerializeField]
    private float gainedExperienceToReduce, gainedMoneyToReduce, experienceGainSpeed;

    [SerializeField]
    private float gainedExperience, gainedMoney, surplusExperience;

    [SerializeField]
    private int numberOfSkipsToEndResults;

    private int skipIndex;

    private float bonusExperienceToAdd, bonusMoneyToAdd;

    private bool canSkipResults, canSkipExperienceAndMoneyGain, leveledUp, endsDemo, gameIsBeaten;

    public GameObject TookNoDamageBonusPanel => tookNoDamageBonusPanel;

    public GameObject QuickVictoryBonusPanel => quickVictoryBonusPanel;

    public GameObject NearDeathEExperienceBonusPanel => nearDeathExperienceBonusPanel;

    public Image ExtraExperienceBanner => extraExperienceBanner;

    public Image ExtraGoldBanner => extraGoldBanner;

    public Coroutine ExpRoutine
    {
        get
        {
            return expRoutine;
        }
        set
        {
            expRoutine = value;
        }
    }

    public Coroutine MoneyRoutine
    {
        get
        {
            return moneyRoutine;
        }
        set
        {
            moneyRoutine = value;
        }
    }

    public Animator StatUpgradeTextAnimator
    {
        get
        {
            return statUpgradeTextAnimator;
        }
        set
        {
            statUpgradeTextAnimator = value;
        }
    }

    public Animator MoneyPanelAnimator => moneyPanelAnimator;

    public Animator ItemSpoilsPanelAnimator => itemSpoilsPanelAnimator;

    public float MoneyEarned
    {
        get
        {
            return gainedMoney;
        }
        set
        {
            gainedMoney = value;
        }
    }

    public float GainedExperienceToReduce
    {
        get
        {
            return gainedExperienceToReduce;
        }
        set
        {
            gainedExperienceToReduce = value;
        }
    }

    public float GainedMoneyToReduce
    {
        get
        {
            return gainedMoneyToReduce;
        }
        set
        {
            gainedMoneyToReduce = value;
        }
    }

    public bool CanSkipResults
    {
        get
        {
            return canSkipResults;
        }
        set
        {
            canSkipResults = value;
        }
    }

    public bool CanSkipExperienceAndMoneyGain
    {
        get
        {
            return canSkipExperienceAndMoneyGain;
        }
        set
        {
            canSkipExperienceAndMoneyGain = value;
        }
    }

    private void Awake()
    {
        if(GameManager.instance.EndsGameDemo)
        {
            endsDemo = true;
        }
        else if(GameManager.instance.IsFinalBossFight)
        {
            gameIsBeaten = true;
        }
    }

    public void SetUpExperienceBar()
    {
        experienceBarAnimator.Play("EXP");

        if (mainCharacterStats.level < mainCharacterStats.maximumLevel)
        {
            if(MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.ExtraExp))
            {
                if (MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.ThrillOfTheHunt))
                {
                    if(GameManager.instance.EnemiesInArea.Length <= 1)
                    {
                        battleSystem._StickerPowerHolder.CreateStickerMessage(StickerPower.ThrillOfTheHunt);

                        float extraExp = Mathf.RoundToInt(GainedExperienceToReduce * 0.50f);

                        if(GameManager.instance.IsFinalBossFight)
                        {
                            gainedExperienceToReduce = 0;
                            gainedExperience = 0;
                        }
                        else
                        {
                            if (extraExp < 1)
                            {
                                gainedExperienceToReduce += 1;
                                gainedExperience += 1;
                            }
                            else
                            {
                                gainedExperienceToReduce += extraExp;
                                gainedExperience += extraExp;
                            }
                        }
                    }
                }
                else
                {
                    float extraExp = Mathf.RoundToInt(GainedExperienceToReduce * 0.25f);

                    if (GameManager.instance.IsFinalBossFight)
                    {
                        gainedExperienceToReduce = 0;
                        gainedExperience = 0;
                    }
                    else
                    {
                        if (extraExp < 1)
                        {
                            gainedExperienceToReduce += 1;
                            gainedExperience += 1;
                        }
                        else
                        {
                            gainedExperienceToReduce += extraExp;
                            gainedExperience += extraExp;
                        }
                    }
                }
            }

            if(!MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.ExtraExp) && MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.ThrillOfTheHunt))
            {
                if (GameManager.instance.EnemiesInArea.Length <= 1)
                {
                    battleSystem._StickerPowerHolder.CreateStickerMessage(StickerPower.ThrillOfTheHunt);

                    float extraExp = Mathf.RoundToInt(GainedExperienceToReduce * 0.50f);

                    if (GameManager.instance.IsFinalBossFight)
                    {
                        gainedExperienceToReduce = 0;
                        gainedExperience = 0;
                    }
                    else
                    {
                        if (extraExp < 1)
                        {
                            gainedExperienceToReduce += 1;
                            gainedExperience += 1;
                        }
                        else
                        {
                            gainedExperienceToReduce += extraExp;
                            gainedExperience += extraExp;
                        }
                    }
                }
            }

            gainedExperienceText.text = "EXP: " + gainedExperienceToReduce.ToString("F0");

            playerExpGainText.text = "EXP: " + mainCharacterStats.currentExp.ToString("F0") + "/" + mainCharacterStats.nextExpToLevel;

            playerExperienceFillImage.fillAmount = mainCharacterStats.currentExp / mainCharacterStats.nextExpToLevel;
        }
        else
        {
            gainedExperienceText.text = "EXP: " + gainedExperienceToReduce.ToString("F0");

            playerExpGainText.text = "EXP: ---/---";

            playerExperienceFillImage.fillAmount = 1;
        }

        playerLevelText.text = mainCharacterStats.level.ToString();
    }

    public void SetUpMoney()
    {
        if (MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.ExtraGold))
        {
            float extraGold = Mathf.RoundToInt(gainedMoneyToReduce * 0.25f);

            if(extraGold < 1)
            {
                gainedMoneyToReduce += 1;
                gainedMoney += 1;
            }
            else
            {
                gainedMoneyToReduce += extraGold;
                gainedMoney += extraGold;
            }
        }

        if(SteamManager.Initialized)
        {
            int money_deepPockets = 0;
            int money_deeperPockets = 0;
            int money_deepestPockets = 0;

            SteamUserStats.GetStat("ACH_1K_GOLD", out money_deepPockets);
            money_deepPockets += (int)gainedMoney;
            SteamUserStats.SetStat("ACH_1K_GOLD", money_deepPockets);
            SteamUserStats.StoreStats();

            SteamUserStats.GetStat("ACH_5K_GOLD", out money_deeperPockets);
            money_deeperPockets += (int)gainedMoney;
            SteamUserStats.SetStat("ACH_5K_GOLD", money_deeperPockets);
            SteamUserStats.StoreStats();

            SteamUserStats.GetStat("ACH_10K_GOLD", out money_deepestPockets);
            money_deepestPockets += (int)gainedMoney;
            SteamUserStats.SetStat("ACH_10K_GOLD", money_deepestPockets);
            SteamUserStats.StoreStats();

            SteamUserStats.GetAchievement("ACH_DEEP_POCKETS", out bool achievementCompleted);

            if(!achievementCompleted)
            {
                if(money_deepPockets >= 1000)
                {
                    SteamUserStats.SetAchievement("ACH_DEEP_POCKETS");
                    SteamUserStats.StoreStats();
                }
            }

            SteamUserStats.GetAchievement("ACH_DEEPER_POCKETS", out bool achievementCompleted2);

            if (!achievementCompleted2)
            {
                if (money_deeperPockets >= 5000)
                {
                    SteamUserStats.SetAchievement("ACH_DEEPER_POCKETS");
                    SteamUserStats.StoreStats();
                }
            }

            SteamUserStats.GetAchievement("ACH_DEEPEST_POCKETS", out bool achievementCompleted3);

            if (!achievementCompleted3)
            {
                if (money_deepestPockets >= 9999)
                {
                    SteamUserStats.SetAchievement("ACH_DEEPEST_POCKETS");
                    SteamUserStats.StoreStats();
                }
            }
        }

        gainedMoneyText.text = "Gold: " + gainedMoney;

        moneyText.text = mainCharacterStats.money.ToString();
    }

    private void Update()
    {
        if (SteamOverlayPause.instance.IsPaused) return;

        if(canSkipResults)
        {
            if(InputManager.instance.ControllerPluggedIn)
            {
                if(InputManager.instance.ControllerName == "xbox")
                {
                    if (Input.GetButtonDown("XboxAttack"))
                    {
                        skipIndex++;
                        if (skipIndex >= numberOfSkipsToEndResults)
                        {
                            ExitBattle();
                            canSkipResults = false;
                        }
                    }
                }
                else
                {
                    if (Input.GetButtonDown("Ps4Attack"))
                    {
                        skipIndex++;
                        if (skipIndex >= numberOfSkipsToEndResults)
                        {
                            ExitBattle();
                            canSkipResults = false;
                        }
                    }
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    skipIndex++;
                    if (skipIndex >= numberOfSkipsToEndResults)
                    {
                        ExitBattle();
                        canSkipResults = false;
                    }
                }
            }
        }

        if(canSkipExperienceAndMoneyGain)
        {
            if(InputManager.instance.ControllerPluggedIn)
            {
                if(InputManager.instance.ControllerName == "xbox")
                {
                    if (Input.GetButtonDown("XboxAttack"))
                    {
                        SkipMoneyGain();

                        if (mainCharacterStats.level < mainCharacterStats.maximumLevel)
                            SkipExperienceGain();
                    }
                }
                else
                {
                    if (Input.GetButtonDown("Ps4Attack"))
                    {
                        SkipMoneyGain();

                        if (mainCharacterStats.level < mainCharacterStats.maximumLevel)
                            SkipExperienceGain();
                    }
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    SkipMoneyGain();

                    if (mainCharacterStats.level < mainCharacterStats.maximumLevel)
                        SkipExperienceGain();
                }
            }
        }
    }

    public IEnumerator GainExperience()
    {
        int oldNumber = (int)gainedExperienceToReduce;

        while(gainedExperienceToReduce > 0 && mainCharacterStats.level < mainCharacterStats.maximumLevel)
        {
            gainedExperienceToReduce -= experienceGainSpeed * Time.deltaTime;
            if(gainedExperienceToReduce <= 0)
            {
                gainedExperienceToReduce = 0;
            }

            int newNumber = (int)gainedExperienceToReduce;

            if (oldNumber != newNumber)
            {
                AudioManager.instance.PlaySoundEffect(AudioManager.instance.ExpGainAudioClip);
                oldNumber = newNumber;
            }

            mainCharacterStats.currentExp += experienceGainSpeed * Time.deltaTime;

            gainedExperienceText.text = "EXP: " + gainedExperienceToReduce.ToString("F0");

            playerExpGainText.text = "EXP: " + mainCharacterStats.currentExp.ToString("F0") + "/" + mainCharacterStats.nextExpToLevel;

            playerExperienceFillImage.fillAmount = mainCharacterStats.currentExp / mainCharacterStats.nextExpToLevel;

            if (mainCharacterStats.currentExp >= mainCharacterStats.nextExpToLevel)
            {
                LevelUpPrecursor();
            }

            yield return null;
        }

        mainCharacterStats.currentExp = Mathf.RoundToInt(mainCharacterStats.currentExp);
        playerExperienceFillImage.fillAmount = mainCharacterStats.currentExp / mainCharacterStats.nextExpToLevel;

        if (leveledUp)
        {
            LevelUp();
        }

        skipIndex++;
    }

    public IEnumerator GainMoney()
    {
        int oldNumber = (int)gainedMoneyToReduce;

        while (gainedMoneyToReduce > 0)
        {
            gainedMoneyToReduce -= experienceGainSpeed * Time.deltaTime;
            if (gainedMoneyToReduce <= 0)
            {
                gainedMoneyToReduce = 0;
            }

            int newNumber = (int)gainedMoneyToReduce;

            if (oldNumber != newNumber)
            {
                AudioManager.instance.PlaySoundEffect(AudioManager.instance.ExpGainAudioClip);
                oldNumber = newNumber;
            }

            mainCharacterStats.money += mainCharacterStats.money >= mainCharacterStats.maximumMoney ? 0 : experienceGainSpeed * Time.deltaTime;

            moneyText.text = mainCharacterStats.money.ToString("F0");

            gainedMoneyText.text = "Gold: " + gainedMoneyToReduce.ToString("F0");

            yield return null;
        }
    }

    public IEnumerator WaitToShowStatsUpgrade()
    {
        yield return new WaitForSeconds(1.7f);
        expAndMoneyPanelAnimator.Play("ResultsAfterLevelUp");
        CheckBonusPanelFade();
        yield return new WaitForSeconds(0.5f);
        statUpgradeTextAnimator.Play("UpgradeText");
        statUpgradeAnimator.Play("Stats");

        if(!SteamOverlayPause.instance.IsPaused)
        {
            battleSystem._InputManager._EventSystem.sendNavigationEvents = true;
        }
        
        battleSystem._InputManager.SetSelectedObject(statToBeSelectedForController.gameObject);
    }

    private IEnumerator WaitToShowRankUpgrade()
    {
        yield return new WaitForSeconds(1.3f);
        rankAnimator.Play("RankUp");
    }

    private void CheckBonusPanelFade()
    {
        if(nearDeathExperienceBonusPanel.activeSelf || quickVictoryBonusPanel.activeSelf || tookNoDamageBonusPanel.activeSelf)
        {
            resultsPanelAnimationEvent.FadeOutBonusPanel();
        }
    }

    private void SkipExperienceGain()
    {
        mainCharacterStats.currentExp += gainedExperienceToReduce;

        mainCharacterStats.currentExp = Mathf.RoundToInt(mainCharacterStats.currentExp);

        surplusExperience = Mathf.Abs(mainCharacterStats.currentExp - mainCharacterStats.nextExpToLevel);

        gainedExperienceToReduce = 0;

        gainedExperienceText.text = "EXP: 0";

        playerExpGainText.text = "EXP: " + mainCharacterStats.currentExp.ToString("F0") + "/" + mainCharacterStats.nextExpToLevel;

        playerExperienceFillImage.fillAmount = mainCharacterStats.currentExp / mainCharacterStats.nextExpToLevel;

        if (mainCharacterStats.currentExp >= mainCharacterStats.nextExpToLevel)
        {
            LevelUpPrecursor();
            LevelUp();
        }
    }

    private void SkipMoneyGain()
    {
        mainCharacterStats.money += gainedMoneyToReduce;

        mainCharacterStats.money = mainCharacterStats.money >= mainCharacterStats.maximumMoney ? mainCharacterStats.maximumMoney : Mathf.RoundToInt(mainCharacterStats.money);

        gainedMoneyToReduce = 0;

        gainedMoneyText.text = "Gold: 0";

        moneyText.text = mainCharacterStats.money.ToString("F0");
    }

    private void LevelUpPrecursor()
    {
        leveledUp = true;

        canSkipResults = false;

        if(SteamManager.Initialized)
        {
            int level = mainCharacterStats.level;

            SteamUserStats.GetStat("ACH_PLAYER_LEVEL", out level);
            mainCharacterStats.level++;
            level++;
            SteamUserStats.SetStat("ACH_PLAYER_LEVEL", level);
            SteamUserStats.StoreStats();

            if(level >= mainCharacterStats.maximumLevel)
            {
                SteamUserStats.GetAchievement("ACH_MAX_POTENTIAL", out bool achievementCompleted);

                if(!achievementCompleted)
                {
                    SteamUserStats.SetAchievement("ACH_MAX_POTENTIAL");
                    SteamUserStats.StoreStats();
                }
            }
        }

        if (playerMenuInfo.currentDeckLimit < playerMenuInfo.maximumDeckLimit)
        {
            playerMenuInfo.currentDeckLimit++;
        }
        else
        {
            playerMenuInfo.currentDeckLimit = playerMenuInfo.maximumDeckLimit;
        }

        if (mainCharacterStats.level < mainCharacterStats.maximumLevel)
        {
            mainCharacterStats.currentExp = 0 + surplusExperience;

            playerLevelTextAnimator.Play("PlayerLevel", -1, 0);

            if(mainCharacterStats.level < 12)
            {
                mainCharacterStats.nextExpToLevel = Mathf.Round((mainCharacterStats.nextExpToLevel + mainCharacterStats.level) * 1.40f);
            }
            else
            {
                float newExp = Mathf.Round(mainCharacterStats.nextExpToLevel / 50);
                mainCharacterStats.nextExpToLevel += newExp + mainCharacterStats.level;
            }

            playerExpGainText.text = "EXP: " + mainCharacterStats.currentExp.ToString("F0") + "/" + (int)mainCharacterStats.nextExpToLevel;

            playerExperienceFillImage.fillAmount = mainCharacterStats.currentExp / mainCharacterStats.nextExpToLevel;
        }
        else
        {
            playerLevelTextAnimator.Play("PlayerLevel");

            playerExpGainText.text = "EXP: ---/---";

            gainedExperienceText.text = "EXP: 0";

            playerExperienceFillImage.fillAmount = 1;

            mainCharacterStats.currentExp = 0;
        }
    }

    private void LevelUp()
    {
        levelUpParticle.gameObject.SetActive(true);
        levelUpParticle.Play();

        levelUpAnimator.Play("LevelUp");

        mainCharacterStats.currentExp = Mathf.Round(mainCharacterStats.currentExp);

        if (PlayerPrefs.HasKey("SoundEffects"))
        {
            audioSource.volume = PlayerPrefs.GetFloat("SoundEffects");
            audioSource.Play();
        }
        else
        {
            audioSource.Play();
        }

        if(playerMenuInfo.rankIndex < playerMenuInfo.levelRequirement.Length - 1)
        {
            if (mainCharacterStats.level == playerMenuInfo.levelRequirement[playerMenuInfo.levelIndex])
            {
                RankBonus();
            }
            else
            {
                StartCoroutine(WaitToShowStatsUpgrade());
            }
        }
        else
        {
            StartCoroutine(WaitToShowStatsUpgrade());
        }

        canSkipResults = false;

        if(gainedMoneyToReduce <= 0 && gainedExperienceToReduce <= 0)
           canSkipExperienceAndMoneyGain = false;
    }

    private void RankBonus()
    {
        if(playerMenuInfo.rankIndex > -1)
        {
            rankImage.color = new Color(1, 1, 1, 1);

            rankImage.sprite = playerMenuInfo.rankSprites[playerMenuInfo.rankIndex];
        }
        else
        {
            rankImage.color = new Color(0, 0, 0, 1);

            rankImage.sprite = playerMenuInfo.rankSprites[0];
        }

        StartCoroutine("WaitToShowRankUpgrade");
    }

    public void SetRankBonusInfo()
    {
        for (int i = 0; i < playerMenuInfo._rank[playerMenuInfo.rankIndex]._rankBonus.Length; i++)
        {
            rankBonusInformation[i].SetBonusInfo(playerMenuInfo._rank[playerMenuInfo.rankIndex].SetBonus(i), playerMenuInfo._rank[playerMenuInfo.rankIndex].BonusMessage);

            if(playerMenuInfo._rank[playerMenuInfo.rankIndex]._rankBonus[i] == global::RankBonus.Armor)
            {
                armorRankUpParticle.SetActive(true);

                switch(playerMenuInfo.armorIndex)
                {
                    case 1:
                        noviceArmor.SetActive(false);
                        veteranArmor.SetActive(true);
                        mainCharacterStats.defense += 2;
                        break;
                    case 2:
                        veteranArmor.SetActive(false);
                        eliteArmor.SetActive(true);
                        mainCharacterStats.defense += 2;
                        break;
                }

                GameManager.instance.SetPlayerFieldEquipment();
            }
            else if(playerMenuInfo._rank[playerMenuInfo.rankIndex]._rankBonus[i] == global::RankBonus.Sticker)
            {
                rankBonusInformation[2].gameObject.SetActive(true);
            }
        }
        playerMenuInfo.levelIndex++;
    }

    public void SetBonusMoneyAndExperience()
    {
        if(tookNoDamageBonusPanel.activeSelf)
        {
            int amountOfEnemies = GameManager.instance.EnemiesToLoad.Count;

            float bonusExperience = Mathf.Round((battleSystem._battlePlayer._mainCharacterStats.level + amountOfEnemies) * 1.1f);

            gainedExperienceToReduce += bonusExperience;

            shinyExpBonusTextOne.text = "+" + bonusExperience;

            float bonusGold = Mathf.RoundToInt(bonusExperience * 1.1f);

            gainedMoneyToReduce += bonusGold;

            noDamageTakenBonusText.text = "No Damage Taken: " + "<color=#F8F3A6>+" + bonusExperience + " </color>EXP, " + "<color=#F8F3A6>+" + bonusGold + " </color>Gold";

            shinyGoldTextOne.text = "+" + bonusGold;
        }

        if(quickVictoryBonusPanel.activeSelf)
        {
            float enemyLevels = 0;

            foreach (EnemyStats enemyStats in GameManager.instance.EnemiesToLoad)
            {
                enemyLevels += enemyStats.level;
            }

            float bonusExperience = Mathf.RoundToInt((enemyLevels + battleSystem._battlePlayer._mainCharacterStats.level) / 1.7f);

            gainedExperienceToReduce += bonusExperience;

            shinyExpBonusTextTwo.text = "+" + bonusExperience;

            float bonusGold = Mathf.RoundToInt(bonusExperience * 1.4f);

            gainedMoneyToReduce += bonusGold;

            quickVictoryBonusText.text = "Quick Victory: " + "<color=#F8F3A6>+" + bonusExperience + " </color>EXP, " + "<color=#F8F3A6>+" + bonusGold + " </color>Gold";

            shinyGoldTextTwo.text = "+" + bonusGold;
        }

        if(nearDeathExperienceBonusPanel.activeSelf)
        {
            float bonusExperience = Mathf.RoundToInt(battleSystem._battlePlayer._mainCharacterStats.level * 1.1f);

            gainedExperienceToReduce += bonusExperience;

            shinyExpBonusTextThree.text = "+" + bonusExperience;

            float bonusGold = Mathf.RoundToInt(bonusExperience * 1.3f);

            gainedMoneyToReduce += bonusGold;

            nearDeathBonusText.text = "Near Death Experience: " + "<color=#F8F3A6>+" + bonusExperience + " </color>EXP, " + "<color=#F8F3A6>+" + bonusGold + " </color>Gold";

            shinyGoldTextThree.text = "+" + bonusGold;
        }

        gainedExperience = gainedExperienceToReduce;
        gainedMoney = gainedMoneyToReduce;
    }

    public void SetGainedExpAndMoney()
    {
        SetBonusMoneyAndExperience();
    }

    public void ExitBattle()
    {
        int currentExp = Mathf.RoundToInt(mainCharacterStats.currentExp);
        int currentMoney = Mathf.RoundToInt(mainCharacterStats.money);

        mainCharacterStats.currentExp = currentExp;
        mainCharacterStats.money = currentMoney;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        GameManager.instance.IsAPreemptiveStrike = false;
        GameManager.instance.IsAnAmbush = false;

        GameManager.instance._BestiaryMenu.ClearEnemyList();

        GameManager.instance.EnemiesToLoad.Clear();

        if(gameObject.activeSelf)
           StartCoroutine("FadeOutAudio");

        if(endsDemo)
        {
            postGameTitleText.text = endOfDemoTitle;
            postGameMessageText.text = endOfDemoMessage;

            canSkipResults = false;
            canSkipExperienceAndMoneyGain = false;

            fadeScreenAnimator.Play("Fading");

            StartCoroutine(ShowDemoCompletion());
        }
        else if(gameIsBeaten)
        {
            postGameTitleText.text = endOfGameTitle;
            postGameMessageText.text = endOfGameMessage;

            canSkipResults = false;
            canSkipExperienceAndMoneyGain = false;

            fadeScreenAnimator.Play("Fading");

            endCreditsObject.SetActive(true);

            endCredits.BeginCreditScrollCoroutine();

            if(SteamManager.Initialized)
            {
                SteamUserStats.GetAchievement("ACH_CASTLE_STOMPER", out bool achievementCompleted);

                if (!achievementCompleted)
                {
                    SteamUserStats.SetAchievement("ACH_CASTLE_STOMPER");
                    SteamUserStats.StoreStats();
                }
            }
        }
        else
        {
            GameManager.instance.ScreenTransition.Show();

            GameManager.instance.StartCoroutine(GameManager.instance.WaitToLoadFieldScene(false));
        }
    }

    private IEnumerator FadeOutAudio()
    {
        while(AudioManager.instance.BackgroundMusic.volume > 0)
        {
            AudioManager.instance.BackgroundMusic.volume -= 1.5f * Time.deltaTime;
            AudioManager.instance.SoundEffects.volume -= 1.3f * Time.deltaTime;

            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator ShowDemoCompletion()
    {
        yield return new WaitForSeconds(2f);
        AudioManager.instance.PlaySoundEffect(AudioManager.instance.OpenMenuAudio);
        demoCompletionAnimator.Play("Open");

        battleSystem._EventSystem.sendNavigationEvents = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}