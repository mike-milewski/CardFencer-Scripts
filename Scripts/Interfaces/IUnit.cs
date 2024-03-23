
public interface IUnit 
{
    #region Deck
    int GetDeckCount();

    int SetDeckCount(int deckCount);

    int[] GetDeckCardId();

    int[] SetDeckCardId(int[] cardId);
    #endregion

    #region Items
    int GetItemCount();

    int SetItemCount(int items);

    int[] GetEquippedItemId();

    int[] SetEquippedItemId(int[] itemId);
    #endregion

    #region Stickers
    int[] GetStickerListId();

    int[] SetStickerListId(int[] stickerId);

    int[] GetEquippedStickers();

    int[] SetEquippedStickers(int[] stickerId);
    #endregion

    #region Stash
    int[] GetStashCardId();

    int[] SetStashCardId(int[] stashId);
    #endregion

    #region PlayerStats
    string GetPlayerName();

    string SetPlayerName(string playerName);

    int GetLevel();

    int SetLevel(int level);

    int GetCurrentHealth();

    int SetCurrentHealth(int currentHealth);

    int GetMaxHealth();

    int SetMaxHealth(int maxHealth);

    int GetCurrentCardPoints();

    int SetCurrentCardPoints(int currentCardPoints);

    int GetMaxCardPoints();

    int SetMaxCardPoints(int maxCardPoints);

    int GetCurrentStickerPoints();

    int SetCurrentStickerPoints(int currentStickerPoints);

    int GetMaxStickerPoints();

    int SetMaxStickerPoints(int maxStickerPoints);

    int GetStrength();

    int SetStrength(int strength);

    int GetDefense();

    int SetDefense(int defense);

    int GetMoonStones();

    int SetMoonStones(int moonStones);

    float GetMoney();

    float SetMoney(float money);

    float GetGuardGauge();

    float SetGuardGauge(float guardGauge);

    float GetCurrentExp();

    float SetCurrentExp(float exp);

    float GetNextToLevel();

    float SetNextToLevel(float nextToLevel);
    #endregion

    #region PlayerMenu
    int GetCurrentDeckLimit();

    int SetCurrentDeckLimit(int deckLimit);

    int GetCurrentHandSize();

    int SetCurrentHandSize(int handSize);

    int GetCurrentStickerSlotSize();

    int SetCurrentStickerSlotSize(int stickerSlotSize);

    int GetCurrentItemSlotSize();

    int SetCurrentItemSlotSize(int itemSlotSize);

    int GetWeaponIndex();

    int SetWeaponIndex(int weaponIndex);

    int GetShieldIndex();

    int SetShieldIndex(int shieldIndex);

    int GetArmorIndex();

    int SetArmorIndex(int armorIndex);

    int GetRankIndex();

    int SetRankIndex(int rankIndex);

    int GetLevelIndex();

    int SetLevelIndex(int levelIndex);

    int[] GetCardCollection();

    int[] SetCardCollection(int[] cardCollection);
    #endregion

    #region DayAndWeather
    bool GetChangedDay();

    bool SetChangedDay(bool changedDay);

    bool GetChangedWeather();

    bool SetChangedWeather(bool changedWeather);
    #endregion

    #region Fountain
    public int GetFountainPowerIndex();

    public int SetFountainPowerIndex(int powerIndex);
    #endregion

    #region WorldAndPlayerPosition
    public string GetWorldScene();

    public string SetWorldScene(string worldScene);

    public int GetWorldIndex();

    public int SetWorldIndex(int worldIndex);

    public int GetCurrentNodeIndex();

    public int SetCurrentNodeIndex(int index);

    public int GetForestStagesUnlocked();

    public int SetForestStagesUnlocked(int stages);

    public int GetForestSecretStageOne();

    public int SetForestSecretStageOne(int stages);

    public int GetForestSecretStageTwo();

    public int SetForestSecretStageTwo(int stages);

    public int GetForestSecretStageThree();

    public int SetForestSecretStageThree(int stages);

    public int GetDesertStagesUnlocked();

    public int SetDesertStagesUnlocked(int stages);

    public int GetDesertSecretStageOne();

    public int SetDesertSecretStageOne(int stages);

    public int GetDesertSecretStageTwo();

    public int SetDesertSecretStageTwo(int stages);

    public int GetDesertSecretStageThree();

    public int SetDesertSecretStageThree(int stages);

    public int GetArcticStagesUnlocked();

    public int SetArcticStagesUnlocked(int stages);

    public int GetArcticSecretStageOne();

    public int SetArcticSecretStageOne(int stages);

    public int GetArcticSecretStageTwo();

    public int SetArcticSecretStageTwo(int stages);

    public int GetArcticSecretStageThree();

    public int SetArcticSecretStageThree(int stages);

    public int GetGraveStagesUnlocked();

    public int SetGraveStagesUnlocked(int stages);

    public int GetGraveSecretStageOne();

    public int SetGraveSecretStageOne(int stages);

    public int GetGraveSecretStageTwo();

    public int SetGraveSecretStageTwo(int stages);

    public int GetGraveSecretStageThree();

    public int SetGraveSecretStageThree(int stages);

    public int GetCastleStagesUnlocked();

    public int SetCastleStagesUnlocked(int stages);

    public int GetCastleSecretStageOne();

    public int SetCastleSecretStageOne(int stages);

    public int GetCastleSecretStageTwo();

    public int SetCastleSecretStageTwo(int stages);

    public int GetCastleSecretStageThree();

    public int SetCastleSecretStageThree(int stages);

    public int GetUnlockedWorlds();

    public int SetUnlockedWorlds(int worlds);
    #endregion

    #region TreasureChests
    int[] GetWorldOneStageOneTreasures();

    int[] SetWorldOneStageOneTreasures(int[] treasures);

    int[] GetWorldOneStageTwoTreasures();

    int[] SetWorldOneStageTwoTreasures(int[] treasures);

    int[] GetWorldOneStageThreeTreasures();

    int[] SetWorldOneStageThreeTreasures(int[] treasures);

    int[] GetWorldOneStageFourTreasures();

    int[] SetWorldOneStageFourTreasures(int[] treasures);

    int[] GetWorldOneStageFiveTreasures();

    int[] SetWorldOneStageFiveTreasures(int[] treasures);

    int[] GetWorldTwoStageOneTreasures();

    int[] SetWorldTwoStageOneTreasures(int[] treasures);

    int[] GetWorldTwoStageTwoTreasures();

    int[] SetWorldTwoStageTwoTreasures(int[] treasures);

    int[] GetWorldTwoStageThreeTreasures();

    int[] SetWorldTwoStageThreeTreasures(int[] treasures);

    int[] GetWorldTwoStageFourTreasures();

    int[] SetWorldTwoStageFourTreasures(int[] treasures);

    int[] GetWorldTwoStageFiveTreasures();

    int[] SetWorldTwoStageFiveTreasures(int[] treasures);

    int[] GetWorldThreeStageOneTreasures();

    int[] SetWorldThreeStageOneTreasures(int[] treasures);

    int[] GetWorldThreeStageTwoTreasures();

    int[] SetWorldThreeStageTwoTreasures(int[] treasures);

    int[] GetWorldThreeStageThreeTreasures();

    int[] SetWorldThreeStageThreeTreasures(int[] treasures);

    int[] GetWorldThreeStageFourTreasures();

    int[] SetWorldThreeStageFourTreasures(int[] treasures);

    int[] GetWorldThreeStageFiveTreasures();

    int[] SetWorldThreeStageFiveTreasures(int[] treasures);

    int[] GetWorldFourStageOneTreasures();

    int[] SetWorldFourStageOneTreasures(int[] treasures);

    int[] GetWorldFourStageTwoTreasures();

    int[] SetWorldFourStageTwoTreasures(int[] treasures);

    int[] GetWorldFourStageThreeTreasures();

    int[] SetWorldFourStageThreeTreasures(int[] treasures);

    int[] GetWorldFourStageFourTreasures();

    int[] SetWorldFourStageFourTreasures(int[] treasures);

    int[] GetWorldFourStageFiveTreasures();

    int[] SetWorldFourStageFiveTreasures(int[] treasures);

    int[] GetWorldFiveStageOneTreasures();

    int[] SetWorldFiveStageOneTreasures(int[] treasures);

    int[] GetWorldFiveStageTwoTreasures();

    int[] SetWorldFiveStageTwoTreasures(int[] treasures);

    int[] GetWorldFiveStageThreeTreasures();

    int[] SetWorldFiveStageThreeTreasures(int[] treasures);

    int[] GetWorldFiveStageFourTreasures();

    int[] SetWorldFiveStageFourTreasures(int[] treasures);

    int[] GetWorldFiveStageFiveTreasures();

    int[] SetWorldFiveStageFiveTreasures(int[] treasures);

    int[] GetWorldOneSecretStageOneTreasures();

    int[] SetWorldOneSecretStageOneTreasures(int[] secretTreasures);

    int[] GetWorldOneSecretStageTwoTreasures();

    int[] SetWorldOneSecretStageTwoTreasures(int[] secretTreasures);

    int[] GetWorldOneSecretStageThreeTreasures();

    int[] SetWorldOneSecretStageThreeTreasures(int[] secretTreasures);

    int[] GetWorldTwoSecretStageOneTreasures();

    int[] SetWorldTwoSecretStageOneTreasures(int[] secretTreasures);

    int[] GetWorldTwoSecretStageTwoTreasures();

    int[] SetWorldTwoSecretStageTwoTreasures(int[] secretTreasures);

    int[] GetWorldTwoSecretStageThreeTreasures();

    int[] SetWorldTwoSecretStageThreeTreasures(int[] secretTreasures);

    int[] GetWorldThreeSecretStageOneTreasures();

    int[] SetWorldThreeSecretStageOneTreasures(int[] secretTreasures);

    int[] GetWorldThreeSecretStageTwoTreasures();

    int[] SetWorldThreeSecretStageTwoTreasures(int[] secretTreasures);

    int[] GetWorldThreeSecretStageThreeTreasures();

    int[] SetWorldThreeSecretStageThreeTreasures(int[] secretTreasures);

    int[] GetWorldFourSecretStageOneTreasures();

    int[] SetWorldFourSecretStageOneTreasures(int[] secretTreasures);

    int[] GetWorldFourSecretStageTwoTreasures();

    int[] SetWorldFourSecretStageTwoTreasures(int[] secretTreasures);

    int[] GetWorldFourSecretStageThreeTreasures();

    int[] SetWorldFourSecretStageThreeTreasures(int[] secretTreasures);

    int[] GetWorldFiveSecretStageOneTreasures();

    int[] SetWorldFiveSecretStageOneTreasures(int[] secretTreasures);

    int[] GetWorldFiveSecretStageTwoTreasures();

    int[] SetWorldFiveSecretStageTwoTreasures(int[] secretTreasures);

    int[] GetWorldFiveSecretStageThreeTreasures();

    int[] SetWorldFiveSecretStageThreeTreasures(int[] secretTreasures);
    #endregion

    #region FieldCards
    int[] GetWorldOneStageOneFieldCards();

    int[] SetWorldOneStageOneFieldCards(int[] fieldCards);

    int[] GetWorldOneStageTwoFieldCards();

    int[] SetWorldOneStageTwoFieldCards(int[] fieldCards);

    int[] GetWorldOneStageThreeFieldCards();

    int[] SetWorldOneStageThreeFieldCards(int[] fieldCards);

    int[] GetWorldOneStageFourFieldCards();

    int[] SetWorldOneStageFourFieldCards(int[] fieldCards);

    int[] GetWorldOneStageFiveFieldCards();

    int[] SetWorldOneStageFiveFieldCards(int[] fieldCards);

    int[] GetWorldOneSecretStageOneFieldCards();

    int[] SetWorldOneSecretStageOneFieldCards(int[] fieldCards);

    int[] GetWorldOneSecretStageTwoFieldCards();

    int[] SetWorldOneSecretStageTwoFieldCards(int[] fieldCards);

    int[] GetWorldTwoStageOneFieldCards();

    int[] SetWorldTwoStageOneFieldCards(int[] fieldCards);

    int[] GetWorldTwoStageTwoFieldCards();

    int[] SetWorldTwoStageTwoFieldCards(int[] fieldCards);

    int[] GetWorldTwoStageThreeFieldCards();

    int[] SetWorldTwoStageThreeFieldCards(int[] fieldCards);

    int[] GetWorldTwoStageFourFieldCards();

    int[] SetWorldTwoStageFourFieldCards(int[] fieldCards);

    int[] GetWorldTwoStageFiveFieldCards();

    int[] SetWorldTwoStageFiveFieldCards(int[] fieldCards);

    int[] GetWorldTwoSecretStageOneFieldCards();

    int[] SetWorldTwoSecretStageOneFieldCards(int[] fieldCards);

    int[] GetWorldTwoSecretStageThreeFieldCards();

    int[] SetWorldTwoSecretStageThreeFieldCards(int[] fieldCards);

    int[] GetWorldThreeStageOneFieldCards();

    int[] SetWorldThreeStageOneFieldCards(int[] fieldCards);

    int[] GetWorldThreeStageTwoFieldCards();

    int[] SetWorldThreeStageTwoFieldCards(int[] fieldCards);

    int[] GetWorldThreeStageThreeFieldCards();

    int[] SetWorldThreeStageThreeFieldCards(int[] fieldCards);

    int[] GetWorldThreeStageFourFieldCards();

    int[] SetWorldThreeStageFourFieldCards(int[] fieldCards);

    int[] GetWorldThreeStageFiveFieldCards();

    int[] SetWorldThreeStageFiveFieldCards(int[] fieldCards);

    int[] GetWorldThreeSecretStageOneFieldCards();

    int[] SetWorldThreeSecretStageOneFieldCards(int[] fieldCards);

    int[] GetWorldThreeSecretStageTwoFieldCards();

    int[] SetWorldThreeSecretStageTwoFieldCards(int[] fieldCards);

    int[] GetWorldFourStageOneFieldCards();

    int[] SetWorldFourStageOneFieldCards(int[] fieldCards);

    int[] GetWorldFourStageTwoFieldCards();

    int[] SetWorldFourStageTwoFieldCards(int[] fieldCards);

    int[] GetWorldFourStageThreeFieldCards();

    int[] SetWorldFourStageThreeFieldCards(int[] fieldCards);

    int[] GetWorldFourStageFourFieldCards();

    int[] SetWorldFourStageFourFieldCards(int[] fieldCards);

    int[] GetWorldFourStageFiveFieldCards();

    int[] SetWorldFourStageFiveFieldCards(int[] fieldCards);

    int[] GetWorldFourSecretStageOneFieldCards();

    int[] SetWorldFourSecretStageOneFieldCards(int[] fieldCards);

    int[] GetWorldFourSecretStageTwoFieldCards();

    int[] SetWorldFourSecretStageTwoFieldCards(int[] fieldCards);

    int[] GetWorldFiveStageOneFieldCards();

    int[] SetWorldFiveStageOneFieldCards(int[] fieldCards);

    int[] GetWorldFiveStageTwoFieldCards();

    int[] SetWorldFiveStageTwoFieldCards(int[] fieldCards);

    int[] GetWorldFiveStageThreeFieldCards();

    int[] SetWorldFiveStageThreeFieldCards(int[] fieldCards);

    int[] GetWorldFiveStageFourFieldCards();

    int[] SetWorldFiveStageFourFieldCards(int[] fieldCards);

    int[] GetWorldFiveStageFiveFieldCards();

    int[] SetWorldFiveStageFiveFieldCards(int[] fieldCards);

    int[] GetWorldFiveSecretStageOneFieldCards();

    int[] SetWorldFiveSecretStageOneFieldCards(int[] fieldCards);

    int[] GetWorldFiveSecretStageTwoFieldCards();

    int[] SetWorldFiveSecretStageTwoFieldCards(int[] fieldCards);
    #endregion

    #region MoonStones
    int[] GetWorldOneStageTwoMoonstones();

    int[] SetWorldOneStageTwoMoonstones(int[] moonStones);

    int[] GetWorldOneStageThreeMoonstones();

    int[] SetWorldOneStageThreeMoonstones(int[] moonStones);

    int[] GetWorldOneStageFourMoonstones();

    int[] SetWorldOneStageFourMoonstones(int[] moonStones);

    int[] GetWorldOneStageFiveMoonstones();

    int[] SetWorldOneStageFiveMoonstones(int[] moonStones);

    int[] GetWorldTwoStageOneMoonstones();

    int[] SetWorldTwoStageOneMoonstones(int[] moonStones);

    int[] GetWorldTwoStageTwoMoonstones();

    int[] SetWorldTwoStageTwoMoonstones(int[] moonStones);

    int[] GetWorldTwoStageThreeMoonstones();

    int[] SetWorldTwoStageThreeMoonstones(int[] moonStones);

    int[] GetWorldTwoStageFourMoonstones();

    int[] SetWorldTwoStageFourMoonstones(int[] moonStones);

    int[] GetWorldTwoStageFiveMoonstones();

    int[] SetWorldTwoStageFiveMoonstones(int[] moonStones);

    int[] GetWorldThreeStageOneMoonstones();

    int[] SetWorldThreeStageOneMoonstones(int[] moonStones);

    int[] GetWorldThreeStageTwoMoonstones();

    int[] SetWorldThreeStageTwoMoonstones(int[] moonStones);

    int[] GetWorldThreeStageThreeMoonstones();

    int[] SetWorldThreeStageThreeMoonstones(int[] moonStones);

    int[] GetWorldThreeStageFourMoonstones();

    int[] SetWorldThreeStageFourMoonstones(int[] moonStones);

    int[] GetWorldThreeStageFiveMoonstones();

    int[] SetWorldThreeStageFiveMoonstones(int[] moonStones);

    int[] GetWorldFourStageOneMoonstones();

    int[] SetWorldFourStageOneMoonstones(int[] moonStones);

    int[] GetWorldFourStageTwoMoonstones();

    int[] SetWorldFourStageTwoMoonstones(int[] moonStones);

    int[] GetWorldFourStageThreeMoonstones();

    int[] SetWorldFourStageThreeMoonstones(int[] moonStones);

    int[] GetWorldFourStageFourMoonstones();

    int[] SetWorldFourStageFourMoonstones(int[] moonStones);

    int[] GetWorldFourStageFiveMoonstones();

    int[] SetWorldFourStageFiveMoonstones(int[] moonStones);

    int[] GetWorldFiveStageOneMoonstones();

    int[] SetWorldFiveStageOneMoonstones(int[] moonStones);

    int[] GetWorldFiveStageTwoMoonstones();

    int[] SetWorldFiveStageTwoMoonstones(int[] moonStones);

    int[] GetWorldFiveStageThreeMoonstones();

    int[] SetWorldFiveStageThreeMoonstones(int[] moonStones);

    int[] GetWorldFiveStageFourMoonstones();

    int[] SetWorldFiveStageFourMoonstones(int[] moonStones);

    int[] GetWorldFiveStageFiveMoonstones();

    int[] SetWorldFiveStageFiveMoonstones(int[] moonStones);

    int[] GetWorldOneSecretStageOneMoonstones();

    int[] SetWorldOneSecretStageOneMoonstones(int[] secretMoonStones);

    int[] GetWorldOneSecretStageTwoMoonstones();

    int[] SetWorldOneSecretStageTwoMoonstones(int[] secretMoonStones);

    int[] GetWorldTwoSecretStageOneMoonstones();

    int[] SetWorldTwoSecretStageOneMoonstones(int[] secretMoonStones);

    int[] GetWorldTwoSecretStageTwoMoonstones();

    int[] SetWorldTwoSecretStageTwoMoonstones(int[] secretMoonStones);

    int[] GetWorldTwoSecretStageThreeMoonstones();

    int[] SetWorldTwoSecretStageThreeMoonstones(int[] secretMoonStones);

    int[] GetWorldThreeSecretStageOneMoonstones();

    int[] SetWorldThreeSecretStageOneMoonstones(int[] secretMoonStones);

    int[] GetWorldThreeSecretStageTwoMoonstones();

    int[] SetWorldThreeSecretStageTwoMoonstones(int[] secretMoonStones);

    int[] GetWorldThreeSecretStageThreeMoonstones();

    int[] SetWorldThreeSecretStageThreeMoonstones(int[] secretMoonStones);

    int[] GetWorldFourSecretStageOneMoonstones();

    int[] SetWorldFourSecretStageOneMoonstones(int[] secretMoonStones);

    int[] GetWorldFourSecretStageTwoMoonstones();

    int[] SetWorldFourSecretStageTwoMoonstones(int[] secretMoonStones);

    int[] GetWorldFourSecretStageThreeMoonstones();

    int[] SetWorldFourSecretStageThreeMoonstones(int[] secretMoonStones);

    int[] GetWorldFiveSecretStageOneMoonstones();

    int[] SetWorldFiveSecretStageOneMoonstones(int[] secretMoonStones);

    int[] GetWorldFiveSecretStageTwoMoonstones();

    int[] SetWorldFiveSecretStageTwoMoonstones(int[] secretMoonStones);

    int[] GetWorldFiveSecretStageThreeMoonstones();

    int[] SetWorldFiveSecretStageThreeMoonstones(int[] secretMoonStones);
    #endregion

    #region Bestiary
    int[] GetBestiary();

    int[] SetBestiary(int[] bestiaryId);
    #endregion

    #region Shop
    int[] GetForestShopCards();

    int[] SetForestShopCards(int[] cardId);

    int[] GetDesertShopCards();

    int[] SetDesertShopCards(int[] cardId);

    int[] GetArcticShopCards();

    int[] SetArcticShopCards(int[] cardId);

    int[] GetCemeteryShopCards();

    int[] SetCemeteryShopCards(int[] cardId);

    int[] GetCastleShopCards();

    int[] SetCastleShopCards(int[] cardId);

    int[] GetForestShopStickers();

    int[] SetForestShopStickers(int[] stickerId);

    int[] GetDesertShopStickers();

    int[] SetDesertShopStickers(int[] stickerId);

    int[] GetArcticShopStickers();

    int[] SetArcticShopStickers(int[] stickerId);

    int[] GetCemeteryShopStickers();

    int[] SetCemeteryShopStickers(int[] stickerId);

    int[] GetCastleShopStickers();

    int[] SetCastleShopStickers(int[] stickerId);
    #endregion

    #region Bosses
    bool GetForestBossDefeated();

    bool SetForestBossDefeated(bool isBossDead);

    bool GetDesertBossDefeated();

    bool SetDesertBossDefeated(bool isBossDead);

    bool GetArcticBossDefeated();

    bool SetArcticBossDefeated(bool isBossDead);

    bool GetGraveBossDefeated();

    bool SetGraveBossDefeated(bool isBossDead);

    bool GetSecretBossForestDefeated();

    bool SetSecretBossForestDefeated(bool isDead);

    bool GetSecretBossDesertDefeated();

    bool SetSecretBossDesertDefeated(bool isDead);

    bool GetSecretBossArcticDefeated();

    bool SetSecretBossArcticDefeated(bool isDead);

    bool GetSecretBossGraveDefeated();

    bool SetSecretBossGraveDefeated(bool isDead);

    bool GetSecretBossCastleDefeated();

    bool SetSecretBossCastleDefeated(bool isDead);
    #endregion

    #region Tutorials
    bool GetBattleTutorialOne();

    bool SetBattleTutorialOne(bool hasTut);

    bool GetBattleTutorialTwo();

    bool SetBattleTutorialTwo(bool hasTut);

    bool GetBattleTutorialThree();

    bool SetBattleTutorialThree(bool hasTut);

    bool GetWorldMapTutorial();

    bool SetWorldMapTutorial(bool hasTut);

    bool GetTownTutorial();

    bool SetTownTutorial(bool hasTut);

    bool GetBasicControlsTutorial();

    bool SetBasicControlsTutorial(bool hasTut);

    bool GetCardsTutorial();

    bool SetCardsTutorial(bool hasTut);

    bool GetStickersTutorial();

    bool SetStickersTutorial(bool hasTut);

    bool GetSecretAreaTutorial();

    bool SetSecretAreaTutorial(bool hasTut);
    #endregion
}