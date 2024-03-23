using TMPro;
using UnityEngine;

public class GameHandler : MonoBehaviour 
{
    [SerializeField] private GameObject unitGameObject;
    private IUnit unit;

    private void Awake() 
    {
        unit = unitGameObject.GetComponent<IUnit>();
        SaveSystem.Init();
    }

    public void Save() 
    {
        int deckCount = unit.GetDeckCount();
        int itemCount = unit.GetItemCount();
        int[] stickerListId = unit.GetStickerListId();
        int[] deckCardId = unit.GetDeckCardId();
        int[] stashCardId = unit.GetStashCardId();
        int[] cardCollectionId = unit.GetCardCollection();
        int[] currentEquippedItems = unit.GetEquippedItemId();
        int[] equippedStickers = unit.GetEquippedStickers();
        int[] worldOneStageOneTreasureIndex = unit.GetWorldOneStageOneTreasures();
        int[] worldOneStageTwoTreasureIndex = unit.GetWorldOneStageTwoTreasures();
        int[] worldOneStageThreeTreasureIndex = unit.GetWorldOneStageThreeTreasures();
        int[] worldOneStageFourTreasureIndex = unit.GetWorldOneStageFourTreasures();
        int[] worldOneStageFiveTreasureIndex = unit.GetWorldOneStageFiveTreasures();
        int[] worldTwoStageOneTreasureIndex = unit.GetWorldTwoStageOneTreasures();
        int[] worldTwoStageTwoTreasureIndex = unit.GetWorldTwoStageTwoTreasures();
        int[] worldTwoStageThreeTreasureIndex = unit.GetWorldTwoStageThreeTreasures();
        int[] worldTwoStageFourTreasureIndex = unit.GetWorldTwoStageFourTreasures();
        int[] worldTwoStageFiveTreasureIndex = unit.GetWorldTwoStageFiveTreasures();
        int[] worldThreeStageOneTreasureIndex = unit.GetWorldThreeStageOneTreasures();
        int[] worldThreeStageTwoTreasureIndex = unit.GetWorldThreeStageTwoTreasures();
        int[] worldThreeStageThreeTreasureIndex = unit.GetWorldThreeStageThreeTreasures();
        int[] worldThreeStageFourTreasureIndex = unit.GetWorldThreeStageFourTreasures();
        int[] worldThreeStageFiveTreasureIndex = unit.GetWorldThreeStageFiveTreasures();
        int[] worldFourStageOneTreasureIndex = unit.GetWorldFourStageOneTreasures();
        int[] worldFourStageTwoTreasureIndex = unit.GetWorldFourStageTwoTreasures();
        int[] worldFourStageThreeTreasureIndex = unit.GetWorldFourStageThreeTreasures();
        int[] worldFourStageFourTreasureIndex = unit.GetWorldFourStageFourTreasures();
        int[] worldFourStageFiveTreasureIndex = unit.GetWorldFourStageFiveTreasures();
        int[] worldFiveStageOneTreasureIndex = unit.GetWorldFiveStageOneTreasures();
        int[] worldFiveStageTwoTreasureIndex = unit.GetWorldFiveStageTwoTreasures();
        int[] worldFiveStageThreeTreasureIndex = unit.GetWorldFiveStageThreeTreasures();
        int[] worldFiveStageFourTreasureIndex = unit.GetWorldFiveStageFourTreasures();
        int[] worldFiveStageFiveTreasureIndex = unit.GetWorldFiveStageFiveTreasures();
        int[] worldOneSecretStageOneTreasures = unit.GetWorldOneSecretStageOneTreasures();
        int[] worldOneSecretStageTwoTreasures = unit.GetWorldOneSecretStageTwoTreasures();
        int[] worldOneSecretStageThreeTreasures = unit.GetWorldOneSecretStageThreeTreasures();
        int[] worldTwoSecretStageOneTreasures = unit.GetWorldTwoSecretStageOneTreasures();
        int[] worldTwoSecretStageTwoTreasures = unit.GetWorldTwoSecretStageTwoTreasures();
        int[] worldTwoSecretStageThreeTreasures = unit.GetWorldTwoSecretStageThreeTreasures();
        int[] worldThreeSecretStageOneTreasures = unit.GetWorldThreeSecretStageOneTreasures();
        int[] worldThreeSecretStageTwoTreasures = unit.GetWorldThreeSecretStageTwoTreasures();
        int[] worldThreeSecretStageThreeTreasures = unit.GetWorldThreeSecretStageThreeTreasures();
        int[] worldFourSecretStageOneTreasures = unit.GetWorldFourSecretStageOneTreasures();
        int[] worldFourSecretStageTwoTreasures = unit.GetWorldFourSecretStageTwoTreasures();
        int[] worldFourSecretStageThreeTreasures = unit.GetWorldFourSecretStageThreeTreasures();
        int[] worldFiveSecretStageOneTreasures = unit.GetWorldFiveSecretStageOneTreasures();
        int[] worldFiveSecretStageTwoTreasures = unit.GetWorldFiveSecretStageTwoTreasures();
        int[] worldFiveSecretStageThreeTreasures = unit.GetWorldFiveSecretStageThreeTreasures();
        int[] worldOneStageTwoMoonstoneIndex = unit.GetWorldOneStageTwoMoonstones();
        int[] worldOneStageThreeMoonstoneIndex = unit.GetWorldOneStageThreeMoonstones();
        int[] worldOneStageFourMoonstoneIndex = unit.GetWorldOneStageFourMoonstones();
        int[] worldOneStageFiveMoonstoneIndex = unit.GetWorldOneStageFiveMoonstones();
        int[] worldTwoStageOneMoonstoneIndex = unit.GetWorldTwoStageOneMoonstones();
        int[] worldTwoStageTwoMoonstoneIndex = unit.GetWorldTwoStageTwoMoonstones();
        int[] worldTwoStageThreeMoonstoneIndex = unit.GetWorldTwoStageThreeMoonstones();
        int[] worldTwoStageFourMoonstoneIndex = unit.GetWorldTwoStageFourMoonstones();
        int[] worldTwoStageFiveMoonstoneIndex = unit.GetWorldTwoStageFiveMoonstones();
        int[] worldThreeStageOneMoonstoneIndex = unit.GetWorldThreeStageOneMoonstones();
        int[] worldThreeStageTwoMoonstoneIndex = unit.GetWorldThreeStageTwoMoonstones();
        int[] worldThreeStageThreeMoonstoneIndex = unit.GetWorldThreeStageThreeMoonstones();
        int[] worldThreeStageFourMoonstoneIndex = unit.GetWorldThreeStageFourMoonstones();
        int[] worldThreeStageFiveMoonstoneIndex = unit.GetWorldThreeStageFiveMoonstones();
        int[] worldFourStageOneMoonstoneIndex = unit.GetWorldFourStageOneMoonstones();
        int[] worldFourStageTwoMoonstoneIndex = unit.GetWorldFourStageTwoMoonstones();
        int[] worldFourStageThreeMoonstoneIndex = unit.GetWorldFourStageThreeMoonstones();
        int[] worldFourStageFourMoonstoneIndex = unit.GetWorldFourStageFourMoonstones();
        int[] worldFourStageFiveMoonstoneIndex = unit.GetWorldFourStageFiveMoonstones();
        int[] worldFiveStageOneMoonstoneIndex = unit.GetWorldFiveStageOneMoonstones();
        int[] worldFiveStageTwoMoonstoneIndex = unit.GetWorldFiveStageTwoMoonstones();
        int[] worldFiveStageThreeMoonstoneIndex = unit.GetWorldFiveStageThreeMoonstones();
        int[] worldFiveStageFourMoonstoneIndex = unit.GetWorldFiveStageFourMoonstones();
        int[] worldFiveStageFiveMoonstoneIndex = unit.GetWorldFiveStageFiveMoonstones();
        int[] worldOneSecretStageOneMoonstoneIndex = unit.GetWorldOneSecretStageOneMoonstones();
        int[] worldOneSecretStageTwoMoonstoneIndex = unit.GetWorldOneSecretStageTwoMoonstones();
        int[] worldTwoSecretStageOneMoonstoneIndex = unit.GetWorldTwoSecretStageOneMoonstones();
        int[] worldTwoSecretStageTwoMoonstoneIndex = unit.GetWorldTwoSecretStageTwoMoonstones();
        int[] worldTwoSecretStageThreeMoonstoneIndex = unit.GetWorldTwoSecretStageThreeMoonstones();
        int[] worldThreeSecretStageOneMoonstoneIndex = unit.GetWorldThreeSecretStageOneMoonstones();
        int[] worldThreeSecretStageTwoMoonstoneIndex = unit.GetWorldThreeSecretStageTwoMoonstones();
        int[] worldThreeSecretStageThreeMoonstoneIndex = unit.GetWorldThreeSecretStageThreeMoonstones();
        int[] worldFourSecretStageOneMoonstoneIndex = unit.GetWorldFourSecretStageOneMoonstones();
        int[] worldFourSecretStageTwoMoonstoneIndex = unit.GetWorldFourSecretStageTwoMoonstones();
        int[] worldFourSecretStageThreeMoonstoneIndex = unit.GetWorldFourSecretStageThreeMoonstones();
        int[] worldFiveSecretStageOneMoonstoneIndex = unit.GetWorldFiveSecretStageOneMoonstones();
        int[] worldFiveSecretStageTwoMoonstoneIndex = unit.GetWorldFiveSecretStageTwoMoonstones();
        int[] worldFiveSecretStageThreeMoonstoneIndex = unit.GetWorldFiveSecretStageThreeMoonstones();
        int[] worldOneStageOneFieldCards = unit.GetWorldOneStageOneFieldCards();
        int[] worldOneStageTwoFieldCards = unit.GetWorldOneStageTwoFieldCards();
        int[] worldOneStageThreeFieldCards = unit.GetWorldOneStageThreeFieldCards();
        int[] worldOneStageFourFieldCards = unit.GetWorldOneStageFourFieldCards();
        int[] worldOneStageFiveFieldCards = unit.GetWorldOneStageFiveFieldCards();
        int[] worldOneSecretStageOneFieldCards = unit.GetWorldOneSecretStageOneFieldCards();
        int[] worldOneSecretStageTwoFieldCards = unit.GetWorldOneSecretStageTwoFieldCards();
        int[] worldTwoStageOneFieldCards = unit.GetWorldTwoStageOneFieldCards();
        int[] worldTwoStageTwoFieldCards = unit.GetWorldTwoStageTwoFieldCards();
        int[] worldTwoStageThreeFieldCards = unit.GetWorldTwoStageThreeFieldCards();
        int[] worldTwoStageFourFieldCards = unit.GetWorldTwoStageFourFieldCards();
        int[] worldTwoStageFiveFieldCards = unit.GetWorldTwoStageFiveFieldCards();
        int[] worldTwoSecretStageOneFieldCards = unit.GetWorldTwoSecretStageOneFieldCards();
        int[] worldTwoSecretStageThreeFieldCards = unit.GetWorldTwoSecretStageThreeFieldCards();
        int[] worldThreeStageOneFieldCards = unit.GetWorldThreeStageOneFieldCards();
        int[] worldThreeStageTwoFieldCards = unit.GetWorldThreeStageTwoFieldCards();
        int[] worldThreeStageThreeFieldCards = unit.GetWorldThreeStageThreeFieldCards();
        int[] worldThreeStageFourFieldCards = unit.GetWorldThreeStageFourFieldCards();
        int[] worldThreeStageFiveFieldCards = unit.GetWorldThreeStageFiveFieldCards();
        int[] worldThreeSecretStageOneFieldCards = unit.GetWorldThreeSecretStageOneFieldCards();
        int[] worldThreeSecretStageTwoFieldCards = unit.GetWorldThreeSecretStageTwoFieldCards();
        int[] worldFourStageOneFieldCards = unit.GetWorldFourStageOneFieldCards();
        int[] worldFourStageTwoFieldCards = unit.GetWorldFourStageTwoFieldCards();
        int[] worldFourStageThreeFieldCards = unit.GetWorldFourStageThreeFieldCards();
        int[] worldFourStageFourFieldCards = unit.GetWorldFourStageFourFieldCards();
        int[] worldFourStageFiveFieldCards = unit.GetWorldFourStageFiveFieldCards();
        int[] worldFourSecretStageOneFieldCards = unit.GetWorldFourSecretStageOneFieldCards();
        int[] worldFourSecretStageTwoFieldCards = unit.GetWorldFourSecretStageTwoFieldCards();
        int[] worldFiveStageOneFieldCards = unit.GetWorldFiveStageOneFieldCards();
        int[] worldFiveStageTwoFieldCards = unit.GetWorldFiveStageTwoFieldCards();
        int[] worldFiveStageThreeFieldCards = unit.GetWorldFiveStageThreeFieldCards();
        int[] worldFiveStageFourFieldCards = unit.GetWorldFiveStageFourFieldCards();
        int[] worldFiveStageFiveFieldCards = unit.GetWorldFiveStageFiveFieldCards();
        int[] worldFiveSecretStageOneFieldCards = unit.GetWorldFiveSecretStageOneFieldCards();
        int[] worldFiveSecretStageTwoFieldCards = unit.GetWorldFiveSecretStageTwoFieldCards();
        int[] bestiaryId = unit.GetBestiary();
        int[] forestShopCardId = unit.GetForestShopCards();
        int[] forestShopStickerId = unit.GetForestShopStickers();
        int[] desertShopCardId = unit.GetDesertShopCards();
        int[] desertShopStickerId = unit.GetDesertShopStickers();
        int[] arcticShopCardId = unit.GetArcticShopCards();
        int[] arcticShopStickerId = unit.GetArcticShopStickers();
        int[] cemeteryShopCardId = unit.GetCemeteryShopCards();
        int[] cemeteryShopStickerId = unit.GetCemeteryShopStickers();
        int[] castleShopCardId = unit.GetCastleShopCards();
        int[] castleStickerId = unit.GetCastleShopStickers();

        string playerName = unit.GetPlayerName();
        int level = unit.GetLevel();
        int currentHealth = unit.GetCurrentHealth();
        int maxHealth = unit.GetMaxHealth();
        int currentCardPoints = unit.GetCurrentCardPoints();
        int maxCardPoints = unit.GetMaxCardPoints();
        int currentStickerPoints = unit.GetCurrentStickerPoints();
        int maxStickerPoints = unit.GetMaxStickerPoints();
        int strength = unit.GetStrength();
        int defense = unit.GetDefense();
        int moonStones = unit.GetMoonStones();
        int currentDeckLimit = unit.GetCurrentDeckLimit();
        int currentHandSize = unit.GetCurrentHandSize();
        int currentStickerSlotSize = unit.GetCurrentStickerSlotSize();
        int currentItemSlotSize = unit.GetCurrentItemSlotSize();
        int weaponIndex = unit.GetWeaponIndex();
        int shieldIndex = unit.GetShieldIndex();
        int armorIndex = unit.GetArmorIndex();
        int rankIndex = unit.GetRankIndex();
        int levelIndex = unit.GetLevelIndex();
        int fountainPowerIndex = unit.GetFountainPowerIndex();
        int worldIndex = unit.GetWorldIndex();
        int currentNodeIndex = unit.GetCurrentNodeIndex();
        int forestStagesUnlocked = unit.GetForestStagesUnlocked();
        int forestSecretStageOne = unit.GetForestSecretStageOne();
        int forestSecretStageTwo = unit.GetForestSecretStageTwo();
        int forestSecretStageThree = unit.GetForestSecretStageThree();
        int desertStagesUnlocked = unit.GetDesertStagesUnlocked();
        int desertSecretStageOne = unit.GetDesertSecretStageOne();
        int desertSecretStageTwo = unit.GetDesertSecretStageTwo();
        int desertSecretStageThree = unit.GetDesertSecretStageThree();
        int arcticStagesUnlocked = unit.GetArcticStagesUnlocked();
        int arcticSecretStageOne = unit.GetArcticSecretStageOne();
        int arcticSecretStageTwo = unit.GetArcticSecretStageTwo();
        int arcticSecretStageThree = unit.GetArcticSecretStageThree();
        int graveStagesUnlocked = unit.GetGraveStagesUnlocked();
        int graveSecretStageOne = unit.GetGraveSecretStageOne();
        int graveSecretStageTwo = unit.GetGraveSecretStageTwo();
        int graveSecretStageThree = unit.GetGraveSecretStageThree();
        int castleStagesUnlocked = unit.GetCastleStagesUnlocked();
        int castleSecretStageOne = unit.GetCastleSecretStageOne();
        int castleSecretStageTwo = unit.GetCastleSecretStageTwo();
        int castleSecretStageThree = unit.GetCastleSecretStageThree();
        int unlockedWorlds = unit.GetUnlockedWorlds();
        float currentExp = unit.GetCurrentExp();
        float nextToLevel = unit.GetNextToLevel();
        float money = unit.GetMoney();
        float guardGauge = unit.GetGuardGauge();
        bool changedDay = unit.GetChangedDay();
        bool changedWeather = unit.GetChangedWeather();
        bool forestBossDefeated = unit.GetForestBossDefeated();
        bool desertBossDefeated = unit.GetDesertBossDefeated();
        bool arcticBossDefeated = unit.GetArcticBossDefeated();
        bool graveBossDefeated = unit.GetGraveBossDefeated();
        bool forestSecretBossDefeated = unit.GetSecretBossForestDefeated();
        bool desertSecretBossDefeated = unit.GetSecretBossDesertDefeated();
        bool arcticSecretBossDefeated = unit.GetSecretBossArcticDefeated();
        bool graveSecretBossDefeated = unit.GetSecretBossGraveDefeated();
        bool castleSecretBossDefeated = unit.GetSecretBossCastleDefeated();
        bool battleOneTutorial = unit.GetBattleTutorialOne();
        bool battleTwoTutorial = unit.GetBattleTutorialTwo();
        bool battleThreeTutorial = unit.GetBattleTutorialThree();
        bool worldMapTutorial = unit.GetWorldMapTutorial();
        bool basicControlsTutorial = unit.GetBasicControlsTutorial();
        bool townTutorial = unit.GetTownTutorial();
        bool secretAreaTutorial = unit.GetSecretAreaTutorial();
        bool cardsTutorial = unit.GetCardsTutorial();
        bool stickersTutorial = unit.GetStickersTutorial();
        string worldSceneName = unit.GetWorldScene();

        SaveObjects saveObject = new SaveObjects
        {
            deckCount = deckCount,
            itemCount = itemCount,
            stickerListId = stickerListId,
            deckCardId = deckCardId,
            stashCardId = stashCardId,
            cardCollectionId = cardCollectionId,
            currentEquippedItems = currentEquippedItems,
            equippedStickerIndex = equippedStickers,
            playerName = playerName,
            level = level,
            currentHealth = currentHealth,
            maxHealth = maxHealth,
            currentCardPoints = currentCardPoints,
            maxCardPoints = maxCardPoints,
            currentStickerPoints = currentStickerPoints,
            maxStickerPoints = maxStickerPoints,
            strength = strength,
            defense = defense,
            moonStones = moonStones,
            currentExp = currentExp,
            nextToLevel = nextToLevel,
            money = money,
            guardGauge = guardGauge,
            currentDeckLimit = currentDeckLimit,
            currentHandSize = currentHandSize,
            currentStickerSlotSize = currentStickerSlotSize,
            currentItemSlotSize = currentItemSlotSize,
            weaponIndex = weaponIndex,
            shieldIndex = shieldIndex,
            armorIndex = armorIndex,
            rankIndex = rankIndex,
            levelIndex = levelIndex,
            worldOneStageOneTreasureIndex = worldOneStageOneTreasureIndex,
            worldOneStageTwoTreasureIndex = worldOneStageTwoTreasureIndex,
            worldOneStageThreeTreasureIndex = worldOneStageThreeTreasureIndex,
            worldOneStageFourTreasureIndex = worldOneStageFourTreasureIndex,
            worldOneStageFiveTreasureIndex = worldOneStageFiveTreasureIndex,
            worldTwoStageOneTreasureIndex = worldTwoStageOneTreasureIndex,
            worldTwoStageTwoTreasureIndex = worldTwoStageTwoTreasureIndex,
            worldTwoStageThreeTreasureIndex = worldTwoStageThreeTreasureIndex,
            worldTwoStageFourTreasureIndex = worldTwoStageFourTreasureIndex,
            worldTwoStageFiveTreasureIndex = worldTwoStageFiveTreasureIndex,
            worldThreeStageOneTreasureIndex = worldThreeStageOneTreasureIndex,
            worldThreeStageTwoTreasureIndex = worldThreeStageTwoTreasureIndex,
            worldThreeStageThreeTreasureIndex = worldThreeStageThreeTreasureIndex,
            worldThreeStageFourTreasureIndex = worldThreeStageFourTreasureIndex,
            worldThreeStageFiveTreasureIndex = worldThreeStageFiveTreasureIndex,
            worldFourStageOneTreasureIndex = worldFourStageOneTreasureIndex,
            worldFourStageTwoTreasureIndex = worldFourStageTwoTreasureIndex,
            worldFourStageThreeTreasureIndex = worldFourStageThreeTreasureIndex,
            worldFourStageFourTreasureIndex = worldFourStageFourTreasureIndex,
            worldFourStageFiveTreasureIndex = worldFourStageFiveTreasureIndex,
            worldFiveStageOneTreasureIndex = worldFiveStageOneTreasureIndex,
            worldFiveStageTwoTreasureIndex = worldFiveStageTwoTreasureIndex,
            worldFiveStageThreeTreasureIndex = worldFiveStageThreeTreasureIndex,
            worldFiveStageFourTreasureIndex = worldFiveStageFourTreasureIndex,
            worldFiveStageFiveTreasureIndex = worldFiveStageFiveTreasureIndex,
            worldOneSecretStageOneTreasureIndex = worldOneSecretStageOneTreasures,
            worldOneSecretStageTwoTreasureIndex = worldOneSecretStageTwoTreasures,
            worldOneSecretStageThreeTreasureIndex = worldOneSecretStageThreeTreasures,
            worldTwoSecretStageOneTreasureIndex = worldTwoSecretStageOneTreasures,
            worldTwoSecretStageTwoTreasureIndex = worldTwoSecretStageTwoTreasures,
            worldTwoSecretStageThreeTreasureIndex = worldTwoSecretStageThreeTreasures,
            worldThreeSecretStageOneTreasureIndex = worldThreeSecretStageOneTreasures,
            worldThreeSecretStageTwoTreasureIndex = worldThreeSecretStageTwoTreasures,
            worldThreeSecretStageThreeTreasureIndex = worldThreeSecretStageThreeTreasures,
            worldFourSecretStageOneTreasureIndex = worldFourSecretStageOneTreasures,
            worldFourSecretStageTwoTreasureIndex = worldFourSecretStageTwoTreasures,
            worldFourSecretStageThreeTreasureIndex = worldFourSecretStageThreeTreasures,
            worldFiveSecretStageOneTreasureIndex = worldFiveSecretStageOneTreasures,
            worldFiveSecretStageTwoTreasureIndex = worldFiveSecretStageTwoTreasures,
            worldFiveSecretStageThreeTreasureIndex = worldFiveSecretStageThreeTreasures,
            worldOneStageTwoMoonstoneIndex = worldOneStageTwoMoonstoneIndex,
            worldOneStageThreeMoonstoneIndex = worldOneStageThreeMoonstoneIndex,
            worldOneStageFourMoonstoneIndex = worldOneStageFourMoonstoneIndex,
            worldOneStageFiveMoonstoneIndex = worldOneStageFiveMoonstoneIndex,
            worldTwoStageOneMoonstoneIndex = worldTwoStageOneMoonstoneIndex,
            worldTwoStageTwoMoonstoneIndex = worldTwoStageTwoMoonstoneIndex,
            worldTwoStageThreeMoonstoneIndex = worldTwoStageThreeMoonstoneIndex,
            worldTwoStageFourMoonstoneIndex = worldTwoStageFourMoonstoneIndex,
            worldTwoStageFiveMoonstoneIndex = worldTwoStageFiveMoonstoneIndex,
            worldThreeStageOneMoonstoneIndex = worldThreeStageOneMoonstoneIndex,
            worldThreeStageTwoMoonstoneIndex = worldThreeStageTwoMoonstoneIndex,
            worldThreeStageThreeMoonstoneIndex = worldThreeStageThreeMoonstoneIndex,
            worldThreeStageFourMoonstoneIndex = worldThreeStageFourMoonstoneIndex,
            worldThreeStageFiveMoonstoneIndex = worldThreeStageFiveMoonstoneIndex,
            worldFourStageOneMoonstoneIndex = worldFourStageOneMoonstoneIndex,
            worldFourStageTwoMoonstoneIndex = worldFourStageTwoMoonstoneIndex,
            worldFourStageThreeMoonstoneIndex = worldFourStageThreeMoonstoneIndex,
            worldFourStageFourMoonstoneIndex = worldFourStageFourMoonstoneIndex,
            worldFourStageFiveMoonstoneIndex = worldFourStageFiveMoonstoneIndex,
            worldFiveStageOneMoonstoneIndex = worldFiveStageOneMoonstoneIndex,
            worldFiveStageTwoMoonstoneIndex = worldFiveStageTwoMoonstoneIndex,
            worldFiveStageThreeMoonstoneIndex = worldFiveStageThreeMoonstoneIndex,
            worldFiveStageFourMoonstoneIndex = worldFiveStageFourMoonstoneIndex,
            worldFiveStageFiveMoonstoneIndex = worldFiveStageFiveMoonstoneIndex,
            worldOneSecretStageOneMoonstoneIndex = worldOneSecretStageOneMoonstoneIndex,
            worldOneSecretStageTwoMoonstoneIndex = worldOneSecretStageTwoMoonstoneIndex,
            worldTwoSecretStageOneMoonstoneIndex = worldTwoSecretStageOneMoonstoneIndex,
            worldTwoSecretStageTwoMoonstoneIndex = worldTwoSecretStageTwoMoonstoneIndex,
            worldTwoSecretStageThreeMoonstoneIndex = worldTwoSecretStageThreeMoonstoneIndex,
            worldThreeSecretStageOneMoonstoneIndex = worldThreeSecretStageOneMoonstoneIndex,
            worldThreeSecretStageTwoMoonstoneIndex = worldThreeSecretStageTwoMoonstoneIndex,
            worldThreeSecretStageThreeMoonstoneIndex = worldThreeSecretStageThreeMoonstoneIndex,
            worldFourSecretStageOneMoonstoneIndex = worldFourSecretStageOneMoonstoneIndex,
            worldFourSecretStageTwoMoonstoneIndex = worldFourSecretStageTwoMoonstoneIndex,
            worldFourSecretStageThreeMoonstoneIndex = worldFourSecretStageThreeMoonstoneIndex,
            worldFiveSecretStageOneMoonstoneIndex = worldFiveSecretStageOneMoonstoneIndex,
            worldFiveSecretStageTwoMoonstoneIndex = worldFiveSecretStageTwoMoonstoneIndex,
            worldFiveSecretStageThreeMoonstoneIndex = worldFiveSecretStageThreeMoonstoneIndex,
            worldOneStageOneFieldCards = worldOneStageOneFieldCards,
            worldOneStageTwoFieldCards = worldOneStageTwoFieldCards,
            worldOneStageThreeFieldCards = worldOneStageThreeFieldCards,
            worldOneStageFourFieldCards = worldOneStageFourFieldCards,
            worldOneStageFiveFieldCards = worldOneStageFiveFieldCards,
            worldOneSecretStageOneFieldCards = worldOneSecretStageOneFieldCards,
            worldOneSecretStageTwoFieldCards = worldOneSecretStageTwoFieldCards,
            worldTwoStageOneFieldCards = worldTwoStageOneFieldCards,
            worldTwoStageTwoFieldCards = worldTwoStageTwoFieldCards,
            worldTwoStageThreeFieldCards = worldTwoStageThreeFieldCards,
            worldTwoStageFourFieldCards = worldTwoStageFourFieldCards,
            worldTwoStageFiveFieldCards = worldTwoStageFiveFieldCards,
            worldTwoSecretStageOneFieldCards = worldTwoSecretStageOneFieldCards,
            worldTwoSecretStageThreeFieldCards = worldTwoSecretStageThreeFieldCards,
            worldThreeStageOneFieldCards = worldThreeStageOneFieldCards,
            worldThreeStageTwoFieldCards = worldThreeStageTwoFieldCards,
            worldThreeStageThreeFieldCards = worldThreeStageThreeFieldCards,
            worldThreeStageFourFieldCards = worldThreeStageFourFieldCards,
            worldThreeStageFiveFieldCards = worldThreeStageFiveFieldCards,
            worldThreeSecretStageOneFieldCards = worldThreeSecretStageOneFieldCards,
            worldThreeSecretStageTwoFieldCards = worldThreeSecretStageTwoFieldCards,
            worldFourStageOneFieldCards = worldFourStageOneFieldCards,
            worldFourStageTwoFieldCards = worldFourStageTwoFieldCards,
            worldFourStageThreeFieldCards = worldFourStageThreeFieldCards,
            worldFourStageFourFieldCards = worldFourStageFourFieldCards,
            worldFourStageFiveFieldCards = worldFourStageFiveFieldCards,
            worldFourSecretStageOneFieldCards = worldFourSecretStageOneFieldCards,
            worldFourSecretStageTwoFieldCards = worldFourSecretStageTwoFieldCards,
            worldFiveStageOneFieldCards = worldFiveStageOneFieldCards,
            worldFiveStageTwoFieldCards = worldFiveStageTwoFieldCards,
            worldFiveStageThreeFieldCards = worldFiveStageThreeFieldCards,
            worldFiveStageFourFieldCards = worldFiveStageFourFieldCards,
            worldFiveStageFiveFieldCards = worldFiveStageFiveFieldCards,
            worldFiveSecretStageOneFieldCards = worldFiveSecretStageOneFieldCards,
            worldFiveSecretStageTwoFieldCards = worldFiveSecretStageTwoFieldCards,
            changedDay = changedDay,
            changedWeather = changedWeather,
            forestBossDefeated = forestBossDefeated,
            desertBossDefeated = desertBossDefeated,
            arcticBossDefeated = arcticBossDefeated,
            graveBossDefeated = graveBossDefeated,
            forestSecretBossDefeated = forestSecretBossDefeated,
            desertSecretBossDefeated = desertSecretBossDefeated,
            arcticSecretBossDefeated = arcticSecretBossDefeated,
            graveSecretBossDefeated = graveSecretBossDefeated,
            castleSecretBossDefeated = castleSecretBossDefeated,
            fountainPowerIndex = fountainPowerIndex,
            worldSceneName = worldSceneName,
            worldIndex = worldIndex,
            currentNodeIndex = currentNodeIndex,
            forestStagesUnlocked = forestStagesUnlocked,
            desertStagesUnlocked = desertStagesUnlocked,
            articStagesUnlocked = arcticStagesUnlocked,
            graveStagesUnlocked = graveStagesUnlocked,
            castleStagesUnlocked = castleStagesUnlocked,
            castleSecretOne = castleSecretStageOne,
            castleSecretTwo = castleSecretStageTwo,
            castleSecretThree = castleSecretStageThree,
            graveyardSecretOne = graveSecretStageOne,
            graveyardSecretTwo = graveSecretStageTwo,
            graveyardSecretThree = graveSecretStageThree,
            arcticSecretOne = arcticSecretStageOne,
            arcticSecretTwo = arcticSecretStageTwo,
            arcticSecretThree = arcticSecretStageThree,
            desertSecretOne = desertSecretStageOne,
            desertSecretTwo = desertSecretStageTwo,
            desertSecretThree = desertSecretStageThree,
            forestSecretOne = forestSecretStageOne,
            forestSecretTwo = forestSecretStageTwo,
            forestSecretThree = forestSecretStageThree,
            unlockedWorlds = unlockedWorlds,
            bestiaryId = bestiaryId,
            forestShopCardIndex = forestShopCardId,
            forestShopStickerIndex = forestShopStickerId,
            desertShopCardIndex = desertShopCardId,
            desertShopStickerIndex = desertShopStickerId,
            arcticShopCardIndex = arcticShopCardId,
            arcticShopStickerIndex = arcticShopStickerId,
            cemeteryShopCardIndex = cemeteryShopCardId,
            cemeteryShopStickerIndex = cemeteryShopStickerId,
            castleShopCardIndex = castleShopCardId,
            castleShopStickerIndex = castleStickerId,
            battleTutOne = battleOneTutorial,
            battleTutTwo = battleTwoTutorial,
            battleTutThree = battleThreeTutorial,
            worldMapTut = worldMapTutorial,
            secretAreaTut = secretAreaTutorial,
            townTut = townTutorial,
            cardsTut = cardsTutorial,
            stickersTut = stickersTutorial,
            basicControlsTut = basicControlsTutorial
        };
        string json = JsonUtility.ToJson(saveObject);
        SaveSystem.Save(json);
    }

    public void Load() 
    {
        string saveString = SaveSystem.Load();
        if (saveString != null) 
        {
            SaveObjects saveObjects = JsonUtility.FromJson<SaveObjects>(saveString);

            unit.SetPlayerName(saveObjects.playerName);
            unit.SetLevel(saveObjects.level);
            unit.SetCurrentHealth(saveObjects.currentHealth);
            unit.SetMaxHealth(saveObjects.maxHealth);
            unit.SetCurrentCardPoints(saveObjects.currentCardPoints);
            unit.SetMaxCardPoints(saveObjects.maxCardPoints);
            unit.SetCurrentStickerPoints(saveObjects.currentStickerPoints);
            unit.SetMaxStickerPoints(saveObjects.maxStickerPoints);
            unit.SetStrength(saveObjects.strength);
            unit.SetDefense(saveObjects.defense);
            unit.SetMoonStones(saveObjects.moonStones);
            unit.SetCurrentExp(saveObjects.currentExp);
            unit.SetNextToLevel(saveObjects.nextToLevel);
            unit.SetMoney(saveObjects.money);
            unit.SetGuardGauge(saveObjects.guardGauge);
            unit.SetCurrentDeckLimit(saveObjects.currentDeckLimit);
            unit.SetCurrentHandSize(saveObjects.currentHandSize);
            unit.SetCurrentStickerSlotSize(saveObjects.currentStickerSlotSize);
            unit.SetCurrentItemSlotSize(saveObjects.currentItemSlotSize);
            unit.SetWeaponIndex(saveObjects.weaponIndex);
            unit.SetShieldIndex(saveObjects.shieldIndex);
            unit.SetArmorIndex(saveObjects.armorIndex);
            unit.SetRankIndex(saveObjects.rankIndex);
            unit.SetLevelIndex(saveObjects.levelIndex);
            unit.SetDeckCount(saveObjects.deckCount);
            unit.SetItemCount(saveObjects.itemCount);
            unit.SetDeckCardId(saveObjects.deckCardId);
            unit.SetStashCardId(saveObjects.stashCardId);
            unit.SetCardCollection(saveObjects.cardCollectionId);
            unit.SetEquippedItemId(saveObjects.currentEquippedItems);
            unit.SetEquippedStickers(saveObjects.equippedStickerIndex);
            unit.SetStickerListId(saveObjects.stickerListId);
            unit.SetWorldOneStageOneTreasures(saveObjects.worldOneStageOneTreasureIndex);
            unit.SetWorldOneStageTwoTreasures(saveObjects.worldOneStageTwoTreasureIndex);
            unit.SetWorldOneStageThreeTreasures(saveObjects.worldOneStageThreeTreasureIndex);
            unit.SetWorldOneStageFourTreasures(saveObjects.worldOneStageFourTreasureIndex);
            unit.SetWorldOneStageFiveTreasures(saveObjects.worldOneStageFiveTreasureIndex);
            unit.SetWorldTwoStageOneTreasures(saveObjects.worldTwoStageOneTreasureIndex);
            unit.SetWorldTwoStageTwoTreasures(saveObjects.worldTwoStageTwoTreasureIndex);
            unit.SetWorldTwoStageThreeTreasures(saveObjects.worldTwoStageThreeTreasureIndex);
            unit.SetWorldTwoStageFourTreasures(saveObjects.worldTwoStageFourTreasureIndex);
            unit.SetWorldTwoStageFiveTreasures(saveObjects.worldTwoStageFiveTreasureIndex);
            unit.SetWorldThreeStageOneTreasures(saveObjects.worldThreeStageOneTreasureIndex);
            unit.SetWorldThreeStageTwoTreasures(saveObjects.worldThreeStageTwoTreasureIndex);
            unit.SetWorldThreeStageThreeTreasures(saveObjects.worldThreeStageThreeTreasureIndex);
            unit.SetWorldThreeStageFourTreasures(saveObjects.worldThreeStageFourTreasureIndex);
            unit.SetWorldThreeStageFiveTreasures(saveObjects.worldThreeStageFiveTreasureIndex);
            unit.SetWorldFourStageOneTreasures(saveObjects.worldFourStageOneTreasureIndex);
            unit.SetWorldFourStageTwoTreasures(saveObjects.worldFourStageTwoTreasureIndex);
            unit.SetWorldFourStageThreeTreasures(saveObjects.worldFourStageThreeTreasureIndex);
            unit.SetWorldFourStageFourTreasures(saveObjects.worldFourStageFourTreasureIndex);
            unit.SetWorldFourStageFiveTreasures(saveObjects.worldFourStageFiveTreasureIndex);
            unit.SetWorldFiveStageOneTreasures(saveObjects.worldFiveStageOneTreasureIndex);
            unit.SetWorldFiveStageTwoTreasures(saveObjects.worldFiveStageTwoTreasureIndex);
            unit.SetWorldFiveStageThreeTreasures(saveObjects.worldFiveStageThreeTreasureIndex);
            unit.SetWorldFiveStageFourTreasures(saveObjects.worldFiveStageFourTreasureIndex);
            unit.SetWorldFiveStageFiveTreasures(saveObjects.worldFiveStageFiveTreasureIndex);
            unit.SetWorldOneStageTwoMoonstones(saveObjects.worldOneStageTwoMoonstoneIndex);
            unit.SetWorldOneStageThreeMoonstones(saveObjects.worldOneStageThreeMoonstoneIndex);
            unit.SetWorldOneStageFourMoonstones(saveObjects.worldOneStageFourMoonstoneIndex);
            unit.SetWorldOneStageFiveMoonstones(saveObjects.worldOneStageFiveMoonstoneIndex);
            unit.SetWorldTwoStageOneMoonstones(saveObjects.worldTwoStageOneMoonstoneIndex);
            unit.SetWorldTwoStageTwoMoonstones(saveObjects.worldTwoStageTwoMoonstoneIndex);
            unit.SetWorldTwoStageThreeMoonstones(saveObjects.worldTwoStageThreeMoonstoneIndex);
            unit.SetWorldTwoStageFourMoonstones(saveObjects.worldTwoStageFourMoonstoneIndex);
            unit.SetWorldTwoStageFiveMoonstones(saveObjects.worldTwoStageFiveMoonstoneIndex);
            unit.SetWorldThreeStageOneMoonstones(saveObjects.worldThreeStageOneMoonstoneIndex);
            unit.SetWorldThreeStageTwoMoonstones(saveObjects.worldThreeStageTwoMoonstoneIndex);
            unit.SetWorldThreeStageThreeMoonstones(saveObjects.worldThreeStageThreeMoonstoneIndex);
            unit.SetWorldThreeStageFourMoonstones(saveObjects.worldThreeStageFourMoonstoneIndex);
            unit.SetWorldThreeStageFiveMoonstones(saveObjects.worldThreeStageFiveMoonstoneIndex);
            unit.SetWorldFourStageOneMoonstones(saveObjects.worldFourStageOneMoonstoneIndex);
            unit.SetWorldFourStageTwoMoonstones(saveObjects.worldFourStageTwoMoonstoneIndex);
            unit.SetWorldFourStageThreeMoonstones(saveObjects.worldFourStageThreeMoonstoneIndex);
            unit.SetWorldFourStageFourMoonstones(saveObjects.worldFourStageFourMoonstoneIndex);
            unit.SetWorldFourStageFiveMoonstones(saveObjects.worldFourStageFiveMoonstoneIndex);
            unit.SetWorldFiveStageOneMoonstones(saveObjects.worldFiveStageOneMoonstoneIndex);
            unit.SetWorldFiveStageTwoMoonstones(saveObjects.worldFiveStageTwoMoonstoneIndex);
            unit.SetWorldFiveStageThreeMoonstones(saveObjects.worldFiveStageThreeMoonstoneIndex);
            unit.SetWorldFiveStageFourMoonstones(saveObjects.worldFiveStageFourMoonstoneIndex);
            unit.SetWorldFiveStageFiveMoonstones(saveObjects.worldFiveStageFiveMoonstoneIndex);
            unit.SetWorldOneSecretStageOneMoonstones(saveObjects.worldOneSecretStageOneMoonstoneIndex);
            unit.SetWorldOneSecretStageTwoMoonstones(saveObjects.worldOneSecretStageTwoMoonstoneIndex);
            unit.SetWorldTwoSecretStageOneMoonstones(saveObjects.worldTwoSecretStageOneMoonstoneIndex);
            unit.SetWorldTwoSecretStageTwoMoonstones(saveObjects.worldTwoSecretStageTwoMoonstoneIndex);
            unit.SetWorldTwoSecretStageThreeMoonstones(saveObjects.worldTwoSecretStageThreeMoonstoneIndex);
            unit.SetWorldThreeSecretStageOneMoonstones(saveObjects.worldThreeSecretStageOneMoonstoneIndex);
            unit.SetWorldThreeSecretStageTwoMoonstones(saveObjects.worldThreeSecretStageTwoMoonstoneIndex);
            unit.SetWorldThreeSecretStageThreeMoonstones(saveObjects.worldThreeSecretStageThreeMoonstoneIndex);
            unit.SetWorldFourSecretStageOneMoonstones(saveObjects.worldFourSecretStageOneMoonstoneIndex);
            unit.SetWorldFourSecretStageTwoMoonstones(saveObjects.worldFourSecretStageTwoMoonstoneIndex);
            unit.SetWorldFourSecretStageThreeMoonstones(saveObjects.worldFourSecretStageThreeMoonstoneIndex);
            unit.SetWorldFiveSecretStageOneMoonstones(saveObjects.worldFiveSecretStageOneMoonstoneIndex);
            unit.SetWorldFiveSecretStageTwoMoonstones(saveObjects.worldFiveSecretStageTwoMoonstoneIndex);
            unit.SetWorldFiveSecretStageThreeMoonstones(saveObjects.worldFiveSecretStageThreeMoonstoneIndex);
            unit.SetWorldOneSecretStageOneTreasures(saveObjects.worldOneSecretStageOneTreasureIndex);
            unit.SetWorldOneSecretStageTwoTreasures(saveObjects.worldOneSecretStageTwoTreasureIndex);
            unit.SetWorldOneSecretStageThreeTreasures(saveObjects.worldOneSecretStageThreeTreasureIndex);
            unit.SetWorldTwoSecretStageOneTreasures(saveObjects.worldTwoSecretStageOneTreasureIndex);
            unit.SetWorldTwoSecretStageTwoTreasures(saveObjects.worldTwoSecretStageTwoTreasureIndex);
            unit.SetWorldTwoSecretStageThreeTreasures(saveObjects.worldTwoSecretStageThreeTreasureIndex);
            unit.SetWorldThreeSecretStageOneTreasures(saveObjects.worldThreeSecretStageOneTreasureIndex);
            unit.SetWorldThreeSecretStageTwoTreasures(saveObjects.worldThreeSecretStageTwoTreasureIndex);
            unit.SetWorldThreeSecretStageThreeTreasures(saveObjects.worldThreeSecretStageThreeTreasureIndex);
            unit.SetWorldFourSecretStageOneTreasures(saveObjects.worldFourSecretStageOneTreasureIndex);
            unit.SetWorldFourSecretStageTwoTreasures(saveObjects.worldFourSecretStageTwoTreasureIndex);
            unit.SetWorldFourSecretStageThreeTreasures(saveObjects.worldFourSecretStageThreeTreasureIndex);
            unit.SetWorldFiveSecretStageOneTreasures(saveObjects.worldFiveSecretStageOneTreasureIndex);
            unit.SetWorldFiveSecretStageTwoTreasures(saveObjects.worldFiveSecretStageTwoTreasureIndex);
            unit.SetWorldFiveSecretStageThreeTreasures(saveObjects.worldFiveSecretStageThreeTreasureIndex);
            unit.SetWorldOneStageOneFieldCards(saveObjects.worldOneStageOneFieldCards);
            unit.SetWorldOneStageTwoFieldCards(saveObjects.worldOneStageTwoFieldCards);
            unit.SetWorldOneStageThreeFieldCards(saveObjects.worldOneStageThreeFieldCards);
            unit.SetWorldOneStageFourFieldCards(saveObjects.worldOneStageFourFieldCards);
            unit.SetWorldOneStageFiveFieldCards(saveObjects.worldOneStageFiveFieldCards);
            unit.SetWorldOneSecretStageOneFieldCards(saveObjects.worldOneSecretStageOneFieldCards);
            unit.SetWorldOneSecretStageTwoFieldCards(saveObjects.worldOneSecretStageTwoFieldCards);
            unit.SetWorldTwoStageOneFieldCards(saveObjects.worldTwoStageOneFieldCards);
            unit.SetWorldTwoStageTwoFieldCards(saveObjects.worldTwoStageTwoFieldCards);
            unit.SetWorldTwoStageThreeFieldCards(saveObjects.worldTwoStageThreeFieldCards);
            unit.SetWorldTwoStageFourFieldCards(saveObjects.worldTwoStageFourFieldCards);
            unit.SetWorldTwoStageFiveFieldCards(saveObjects.worldTwoStageFiveFieldCards);
            unit.SetWorldTwoSecretStageOneFieldCards(saveObjects.worldTwoSecretStageOneFieldCards);
            unit.SetWorldTwoSecretStageThreeFieldCards(saveObjects.worldTwoSecretStageThreeFieldCards);
            unit.SetWorldThreeStageOneFieldCards(saveObjects.worldThreeStageOneFieldCards);
            unit.SetWorldThreeStageTwoFieldCards(saveObjects.worldThreeStageTwoFieldCards);
            unit.SetWorldThreeStageThreeFieldCards(saveObjects.worldThreeStageThreeFieldCards);
            unit.SetWorldThreeStageFourFieldCards(saveObjects.worldThreeStageFourFieldCards);
            unit.SetWorldThreeStageFiveFieldCards(saveObjects.worldThreeStageFiveFieldCards);
            unit.SetWorldThreeSecretStageOneFieldCards(saveObjects.worldThreeSecretStageOneFieldCards);
            unit.SetWorldThreeSecretStageTwoFieldCards(saveObjects.worldThreeSecretStageTwoFieldCards);
            unit.SetWorldFourStageOneFieldCards(saveObjects.worldFourStageOneFieldCards);
            unit.SetWorldFourStageTwoFieldCards(saveObjects.worldFourStageTwoFieldCards);
            unit.SetWorldFourStageThreeFieldCards(saveObjects.worldFourStageThreeFieldCards);
            unit.SetWorldFourStageFourFieldCards(saveObjects.worldFourStageFourFieldCards);
            unit.SetWorldFourStageFiveFieldCards(saveObjects.worldFourStageFiveFieldCards);
            unit.SetWorldFourSecretStageOneFieldCards(saveObjects.worldFourSecretStageOneFieldCards);
            unit.SetWorldFourSecretStageTwoFieldCards(saveObjects.worldFourSecretStageTwoFieldCards);
            unit.SetWorldFiveStageOneFieldCards(saveObjects.worldFiveStageOneFieldCards);
            unit.SetWorldFiveStageTwoFieldCards(saveObjects.worldFiveStageTwoFieldCards);
            unit.SetWorldFiveStageThreeFieldCards(saveObjects.worldFiveStageThreeFieldCards);
            unit.SetWorldFiveStageFourFieldCards(saveObjects.worldFiveStageFourFieldCards);
            unit.SetWorldFiveStageFiveFieldCards(saveObjects.worldFiveStageFiveFieldCards);
            unit.SetWorldFiveSecretStageOneFieldCards(saveObjects.worldFiveSecretStageOneFieldCards);
            unit.SetWorldFiveSecretStageTwoFieldCards(saveObjects.worldFiveSecretStageTwoFieldCards);
            unit.SetUnlockedWorlds(saveObjects.unlockedWorlds);
            unit.SetWorldScene(saveObjects.worldSceneName);
            unit.SetWorldIndex(saveObjects.worldIndex);
            unit.SetChangedDay(saveObjects.changedDay);
            unit.SetChangedWeather(saveObjects.changedWeather);
            unit.SetForestBossDefeated(saveObjects.forestBossDefeated);
            unit.SetDesertBossDefeated(saveObjects.desertBossDefeated);
            unit.SetArcticBossDefeated(saveObjects.arcticBossDefeated);
            unit.SetGraveBossDefeated(saveObjects.graveBossDefeated);
            unit.SetSecretBossForestDefeated(saveObjects.forestSecretBossDefeated);
            unit.SetSecretBossDesertDefeated(saveObjects.desertSecretBossDefeated);
            unit.SetSecretBossArcticDefeated(saveObjects.arcticSecretBossDefeated);
            unit.SetSecretBossGraveDefeated(saveObjects.graveSecretBossDefeated);
            unit.SetSecretBossCastleDefeated(saveObjects.castleSecretBossDefeated);
            unit.SetFountainPowerIndex(saveObjects.fountainPowerIndex);
            unit.SetBestiary(saveObjects.bestiaryId);
            unit.SetForestShopCards(saveObjects.forestShopCardIndex);
            unit.SetForestShopStickers(saveObjects.forestShopStickerIndex);
            unit.SetDesertShopCards(saveObjects.desertShopCardIndex);
            unit.SetDesertShopStickers(saveObjects.desertShopStickerIndex);
            unit.SetArcticShopCards(saveObjects.arcticShopCardIndex);
            unit.SetArcticShopStickers(saveObjects.arcticShopStickerIndex);
            unit.SetCemeteryShopCards(saveObjects.cemeteryShopCardIndex);
            unit.SetCemeteryShopStickers(saveObjects.cemeteryShopStickerIndex);
            unit.SetCastleShopCards(saveObjects.castleShopCardIndex);
            unit.SetCastleShopStickers(saveObjects.castleShopStickerIndex);
            unit.SetCurrentNodeIndex(saveObjects.currentNodeIndex);
            unit.SetForestStagesUnlocked(saveObjects.forestStagesUnlocked);
            unit.SetDesertStagesUnlocked(saveObjects.desertStagesUnlocked);
            unit.SetArcticStagesUnlocked(saveObjects.articStagesUnlocked);
            unit.SetGraveStagesUnlocked(saveObjects.graveStagesUnlocked);
            unit.SetCastleStagesUnlocked(saveObjects.castleStagesUnlocked);
            unit.SetForestSecretStageOne(saveObjects.forestSecretOne);
            unit.SetForestSecretStageTwo(saveObjects.forestSecretTwo);
            unit.SetForestSecretStageThree(saveObjects.forestSecretThree);
            unit.SetDesertSecretStageOne(saveObjects.desertSecretOne);
            unit.SetDesertSecretStageTwo(saveObjects.desertSecretTwo);
            unit.SetDesertSecretStageThree(saveObjects.desertSecretThree);
            unit.SetArcticSecretStageOne(saveObjects.arcticSecretOne);
            unit.SetArcticSecretStageTwo(saveObjects.arcticSecretTwo);
            unit.SetArcticSecretStageThree(saveObjects.arcticSecretThree);
            unit.SetGraveSecretStageOne(saveObjects.graveyardSecretOne);
            unit.SetGraveSecretStageTwo(saveObjects.graveyardSecretTwo);
            unit.SetGraveSecretStageThree(saveObjects.graveyardSecretThree);
            unit.SetCastleSecretStageOne(saveObjects.castleSecretOne);
            unit.SetCastleSecretStageTwo(saveObjects.castleSecretTwo);
            unit.SetCastleSecretStageThree(saveObjects.castleSecretThree);
            unit.SetBattleTutorialOne(saveObjects.battleTutOne);
            unit.SetBattleTutorialTwo(saveObjects.battleTutTwo);
            unit.SetBattleTutorialThree(saveObjects.battleTutThree);
            unit.SetWorldMapTutorial(saveObjects.worldMapTut);
            unit.SetBasicControlsTutorial(saveObjects.basicControlsTut);
            unit.SetTownTutorial(saveObjects.townTut);
            unit.SetCardsTutorial(saveObjects.cardsTut);
            unit.SetStickersTutorial(saveObjects.stickersTut);
            unit.SetSecretAreaTutorial(saveObjects.secretAreaTut);
        }
    }

    private class SaveObjects 
    {
        public string playerName;

        public int[] deckCardId, stashCardId, currentEquippedItems, equippedStickerIndex, worldOneStageOneTreasureIndex, worldOneStageTwoTreasureIndex, worldOneStageThreeTreasureIndex, worldOneStageFourTreasureIndex,
                     worldOneStageFiveTreasureIndex, worldOneSecretStageOneTreasureIndex, worldOneSecretStageTwoTreasureIndex, worldOneSecretStageThreeTreasureIndex, worldOneStageTwoMoonstoneIndex, worldOneStageThreeMoonstoneIndex,
                     worldOneStageFourMoonstoneIndex, worldOneStageFiveMoonstoneIndex, worldOneSecretStageOneMoonstoneIndex, worldOneSecretStageTwoMoonstoneIndex, forestShopCardIndex, desertShopCardIndex, 
                     arcticShopCardIndex, cemeteryShopCardIndex, castleShopCardIndex, forestShopStickerIndex, desertShopStickerIndex, arcticShopStickerIndex, cemeteryShopStickerIndex, castleShopStickerIndex,
                     collectedCardIndex, stickerListId, bestiaryId;

        public int[] worldTwoStageOneTreasureIndex, worldTwoStageTwoTreasureIndex, worldTwoStageThreeTreasureIndex, worldTwoStageFourTreasureIndex, worldTwoStageFiveTreasureIndex;

        public int[] worldTwoSecretStageOneTreasureIndex, worldTwoSecretStageTwoTreasureIndex, worldTwoSecretStageThreeTreasureIndex;

        public int[] worldThreeStageOneTreasureIndex, worldThreeStageTwoTreasureIndex, worldThreeStageThreeTreasureIndex, worldThreeStageFourTreasureIndex, worldThreeStageFiveTreasureIndex;

        public int[] worldThreeSecretStageOneTreasureIndex, worldThreeSecretStageTwoTreasureIndex, worldThreeSecretStageThreeTreasureIndex;

        public int[] worldFourStageOneTreasureIndex, worldFourStageTwoTreasureIndex, worldFourStageThreeTreasureIndex, worldFourStageFourTreasureIndex, worldFourStageFiveTreasureIndex;

        public int[] worldFourSecretStageOneTreasureIndex, worldFourSecretStageTwoTreasureIndex, worldFourSecretStageThreeTreasureIndex;

        public int[] worldFiveStageOneTreasureIndex, worldFiveStageTwoTreasureIndex, worldFiveStageThreeTreasureIndex, worldFiveStageFourTreasureIndex, worldFiveStageFiveTreasureIndex;

        public int[] worldFiveSecretStageOneTreasureIndex, worldFiveSecretStageTwoTreasureIndex, worldFiveSecretStageThreeTreasureIndex;

        public int[] worldTwoStageOneMoonstoneIndex, worldTwoStageTwoMoonstoneIndex, worldTwoStageThreeMoonstoneIndex, worldTwoStageFourMoonstoneIndex, worldTwoStageFiveMoonstoneIndex;

        public int[] worldTwoSecretStageOneMoonstoneIndex, worldTwoSecretStageTwoMoonstoneIndex, worldTwoSecretStageThreeMoonstoneIndex;

        public int[] worldThreeStageOneMoonstoneIndex, worldThreeStageTwoMoonstoneIndex, worldThreeStageThreeMoonstoneIndex, worldThreeStageFourMoonstoneIndex, worldThreeStageFiveMoonstoneIndex;

        public int[] worldThreeSecretStageOneMoonstoneIndex, worldThreeSecretStageTwoMoonstoneIndex, worldThreeSecretStageThreeMoonstoneIndex;

        public int[] worldFourStageOneMoonstoneIndex, worldFourStageTwoMoonstoneIndex, worldFourStageThreeMoonstoneIndex, worldFourStageFourMoonstoneIndex, worldFourStageFiveMoonstoneIndex;

        public int[] worldFourSecretStageOneMoonstoneIndex, worldFourSecretStageTwoMoonstoneIndex, worldFourSecretStageThreeMoonstoneIndex;

        public int[] worldFiveStageOneMoonstoneIndex, worldFiveStageTwoMoonstoneIndex, worldFiveStageThreeMoonstoneIndex, worldFiveStageFourMoonstoneIndex, worldFiveStageFiveMoonstoneIndex;

        public int[] worldOneStageOneFieldCards, worldOneStageTwoFieldCards, worldOneStageThreeFieldCards, worldOneStageFourFieldCards, worldOneStageFiveFieldCards;

        public int[] worldOneSecretStageOneFieldCards, worldOneSecretStageTwoFieldCards;

        public int[] worldTwoStageOneFieldCards, worldTwoStageTwoFieldCards, worldTwoStageThreeFieldCards, worldTwoStageFourFieldCards, worldTwoStageFiveFieldCards;

        public int[] worldTwoSecretStageOneFieldCards, worldTwoSecretStageThreeFieldCards;

        public int[] worldThreeStageOneFieldCards, worldThreeStageTwoFieldCards, worldThreeStageThreeFieldCards, worldThreeStageFourFieldCards, worldThreeStageFiveFieldCards;

        public int[] worldThreeSecretStageOneFieldCards, worldThreeSecretStageTwoFieldCards;

        public int[] worldFourStageOneFieldCards, worldFourStageTwoFieldCards, worldFourStageThreeFieldCards, worldFourStageFourFieldCards, worldFourStageFiveFieldCards;

        public int[] worldFourSecretStageOneFieldCards, worldFourSecretStageTwoFieldCards;

        public int[] worldFiveStageOneFieldCards, worldFiveStageTwoFieldCards, worldFiveStageThreeFieldCards, worldFiveStageFourFieldCards, worldFiveStageFiveFieldCards;

        public int[] worldFiveSecretStageOneFieldCards, worldFiveSecretStageTwoFieldCards;

        public int[] worldFiveSecretStageOneMoonstoneIndex, worldFiveSecretStageTwoMoonstoneIndex, worldFiveSecretStageThreeMoonstoneIndex;

        public int[] cardCollectionId;

        public int deckCount, itemCount, level, currentHealth, maxHealth, currentCardPoints, maxCardPoints, currentStickerPoints, maxStickerPoints, strength, defense, moonStones, counterChances, currentDeckLimit,
                   currentHandSize, currentStickerSlotSize, currentItemSlotSize, weaponIndex, shieldIndex, armorIndex, rankIndex, levelIndex, fountainPowerIndex, worldIndex, currentNodeIndex, forestStagesUnlocked,
                   desertStagesUnlocked, articStagesUnlocked, graveStagesUnlocked, castleStagesUnlocked, unlockedWorlds, forestSecretOne, forestSecretTwo, forestSecretThree, desertSecretOne, desertSecretTwo,
                   desertSecretThree, arcticSecretOne, arcticSecretTwo, arcticSecretThree, graveyardSecretOne, graveyardSecretTwo, graveyardSecretThree, castleSecretOne, castleSecretTwo, castleSecretThree;

        public float money, guardGauge, currentExp, nextToLevel;

        public bool changedDay, changedWeather, forestBossDefeated, desertBossDefeated, arcticBossDefeated, graveBossDefeated, castleBossDefeated, forestSecretBossDefeated, desertSecretBossDefeated,
                    arcticSecretBossDefeated, graveSecretBossDefeated, castleSecretBossDefeated;

        public bool battleTutOne, battleTutTwo, battleTutThree, worldMapTut, basicControlsTut, secretAreaTut, cardsTut, stickersTut, townTut;

        public string worldSceneName;
    }
}