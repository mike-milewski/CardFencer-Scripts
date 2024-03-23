using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class SaveFile
{
    public static SaveFile instance = null;

    [SerializeField]
    private MainCharacterStats mainCharacterStats;

    [SerializeField]
    private ShopInformation shopInformation;

    [SerializeField]
    private PlayerMenuInfo playerMenuInfo;

    [SerializeField]
    private FieldCardData cardData;

    [SerializeField]
    private FountainData fountainData;

    [SerializeField]
    private MoonstoneData moonStoneData;

    [SerializeField]
    private WorldEnvironmentData worldEnvironmentData;

    [SerializeField]
    private TreasureData treasureData;

    public void SaveGame(bool showWarningMessage)
    {
        if(showWarningMessage)
        {
            if (!PlayerPrefs.HasKey("SaveFile"))
            {
                PlayerPrefs.SetInt("SaveFile", 0);

                SavePlayerStats();
                SavePlayerWorldAndPosition();
                SaveFountainData();
                SaveWorldEnvironmentData();

                PlayerPrefs.Save();
            }
            else
            {
                MenuController.instance.ShowSaveWarningPanel();
            }
        }
        else
        {
            SavePlayerStats();
            SavePlayerWorldAndPosition();
            SaveFountainData();
            SaveWorldEnvironmentData();

            PlayerPrefs.Save();
        }
    }

    public void LoadGame()
    {
        LoadPlayerStats();
        LoadPlayerWorldAndPosition();
        LoadFountainData();
        LoadWorldEnvironmentData();
    }

    private void SavePlayerStats()
    {
        PlayerPrefs.SetString("PlayerName", mainCharacterStats.playerName);
        PlayerPrefs.SetInt("PlayerLevel", mainCharacterStats.level);
        PlayerPrefs.SetInt("PlayerHealth", mainCharacterStats.maximumHealth);
        PlayerPrefs.SetInt("PlayerCardPoints", mainCharacterStats.maximumCardPoints);
        PlayerPrefs.SetInt("PlayerStrength", mainCharacterStats.strength);
        PlayerPrefs.SetInt("PlayerDefense", mainCharacterStats.defense);
        PlayerPrefs.SetInt("PlayerStickerPoints", mainCharacterStats.currentPlayerStickerPoints);
        PlayerPrefs.SetInt("PlayerMaxStickerPoints", mainCharacterStats.maximumStickerPoints);
        PlayerPrefs.SetFloat("PlayerMoney", mainCharacterStats.money);
        PlayerPrefs.SetFloat("CurrentExp", mainCharacterStats.currentExp);
        PlayerPrefs.SetFloat("NextToLevel", mainCharacterStats.nextExpToLevel);
        PlayerPrefs.SetInt("MoonStones", mainCharacterStats.moonStone);

        PlayerPrefs.SetInt("DeckLimit", playerMenuInfo.currentDeckLimit);
        PlayerPrefs.SetInt("HandSize", playerMenuInfo.currentHandSize);
        PlayerPrefs.SetInt("EquippedStickers", playerMenuInfo.equippedStickers);
        PlayerPrefs.SetInt("CurrentStickerSize", playerMenuInfo.currentStickerSlotSize);
        PlayerPrefs.SetInt("EquippedItems", playerMenuInfo.equippedItems);
        PlayerPrefs.SetInt("CurrentItemSize", playerMenuInfo.currentItemSlotSize);
        PlayerPrefs.SetInt("WeaponIndex", playerMenuInfo.weaponIndex);
        PlayerPrefs.SetInt("ShieldIndex", playerMenuInfo.shieldIndex);
        PlayerPrefs.SetInt("ArmorIndex", playerMenuInfo.armorIndex);
        PlayerPrefs.SetInt("RankIndex", playerMenuInfo.rankIndex);
        PlayerPrefs.SetInt("LevelIndex", playerMenuInfo.levelIndex);
    }

    private void SaveFountainData()
    {
        PlayerPrefs.SetInt("FountainPowerLevel", fountainData.powerIndex);
    }

    private void SaveWorldEnvironmentData()
    {
        PlayerPrefs.SetInt("ChangedWeather", worldEnvironmentData.changedWeather ? 1 : 0);
        PlayerPrefs.SetInt("ChangedDay", worldEnvironmentData.changedDay ? 1 : 0);
    }

    public void SavePlayerWorldAndPosition()
    {
        PlayerPrefs.SetInt("CurrentWorldIndex", NodeManager.instance.WorldIndex);
        PlayerPrefs.SetInt("WorldCurrentStageIndex", NodeManager.instance.CurrentNodeIndex);
        //PlayerPrefs.SetInt("StagesUnlocked", NodeManager.instance.StagesUnlocked);
        //PlayerPrefs.SetInt("SecretStagesUnlocked", NodeManager.instance.SecretStagesUnlocked);
        PlayerPrefs.SetInt("WorldsUnlocked", NodeManager.instance.UnlockedWorlds);
        PlayerPrefs.SetString("RecentSavedScene", NodeManager.instance.SceneName);
    }

    private void LoadPlayerStats()
    {
        mainCharacterStats.playerName = PlayerPrefs.GetString("PlayerName");
        mainCharacterStats.level = PlayerPrefs.GetInt("PlayerLevel");
        mainCharacterStats.currentPlayerHealth = PlayerPrefs.GetInt("PlayerHealth");
        mainCharacterStats.currentPlayerCardPoints = PlayerPrefs.GetInt("PlayerCardPoints");
        mainCharacterStats.strength = PlayerPrefs.GetInt("PlayerStrength");
        mainCharacterStats.defense = PlayerPrefs.GetInt("PlayerDefense");
        mainCharacterStats.currentPlayerStickerPoints = PlayerPrefs.GetInt("PlayerStickerPoints");
        mainCharacterStats.maximumStickerPoints = PlayerPrefs.GetInt("PlayerMaxStickerPoints");
        mainCharacterStats.money = PlayerPrefs.GetFloat("PlayerMoney");
        mainCharacterStats.currentExp = PlayerPrefs.GetFloat("CurrentExp");
        mainCharacterStats.nextExpToLevel = PlayerPrefs.GetFloat("NextToLevel");
        mainCharacterStats.moonStone = PlayerPrefs.GetInt("MoonStones");

        playerMenuInfo.currentDeckLimit = PlayerPrefs.GetInt("DeckLimit");
        playerMenuInfo.currentHandSize = PlayerPrefs.GetInt("HandSize");
        playerMenuInfo.equippedStickers = PlayerPrefs.GetInt("EquippedStickers");
        playerMenuInfo.currentStickerSlotSize = PlayerPrefs.GetInt("CurrentStickerSize");
        playerMenuInfo.equippedItems = PlayerPrefs.GetInt("EquippedItems");
        playerMenuInfo.currentItemSlotSize = PlayerPrefs.GetInt("CurrentItemSize");
        playerMenuInfo.weaponIndex = PlayerPrefs.GetInt("WeaponIndex");
        playerMenuInfo.shieldIndex = PlayerPrefs.GetInt("ShieldIndex");
        playerMenuInfo.armorIndex = PlayerPrefs.GetInt("ArmorIndex");
        playerMenuInfo.rankIndex = PlayerPrefs.GetInt("RankIndex");
        playerMenuInfo.levelIndex = PlayerPrefs.GetInt("LevelIndex");
    }

    private void LoadPlayerWorldAndPosition()
    {
        NodeManager.instance.WorldIndex = PlayerPrefs.GetInt("CurrentWorldIndex");
        NodeManager.instance.CurrentNodeIndex = PlayerPrefs.GetInt("WorldCurrentStageIndex");
        //NodeManager.instance.StagesUnlocked = PlayerPrefs.GetInt("StagesUnlocked");
        //NodeManager.instance.SecretStagesUnlocked = PlayerPrefs.GetInt("SecretStagesUnlocked");
        NodeManager.instance.UnlockedWorlds = PlayerPrefs.GetInt("WorldsUnlocked");
    }

    private void LoadFountainData()
    {
        fountainData.powerIndex = PlayerPrefs.GetInt("FountainPowerLevel");
    }

    private void LoadWorldEnvironmentData()
    {
        worldEnvironmentData.changedWeather = PlayerPrefs.GetInt("ChangedWeather") == 1 ? true : false;
        worldEnvironmentData.changedDay = PlayerPrefs.GetInt("ChangedDay") == 1 ? true : false;
    }
}