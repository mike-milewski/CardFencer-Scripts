using UnityEngine;

public class ResetPlayerStats : MonoBehaviour
{
    [SerializeField]
    private MainCharacterStats mainCharacterStats;

    [SerializeField]
    private BossData bossData;

    [SerializeField]
    private TreasureData treasureData;

    [SerializeField]
    private FountainData fountainData;

    [SerializeField]
    private PlayerMenuInfo playerMenuInfo;

    [SerializeField]
    private FieldCardData fieldCardData;

    [SerializeField]
    private MoonstoneData moonStoneData;

    [SerializeField]
    private ShopInformation shopInformation;

    [SerializeField]
    private WorldEnvironmentData worldEnvironmentData;

    public void RestStats()
    {
        //PlayerPrefs.DeleteAll();

        fountainData.powerIndex = 0;

        playerMenuInfo.currentlyEquippedMysticCards = 0;

        mainCharacterStats.level = 1;
        mainCharacterStats.money = 0;
        mainCharacterStats.moonStone = 0;
        mainCharacterStats.currentExp = 0;
        mainCharacterStats.nextExpToLevel = 10;
        mainCharacterStats.currentPlayerHealth = 15;
        mainCharacterStats.maximumHealth = 15;
        mainCharacterStats.currentPlayerCardPoints = 10;
        mainCharacterStats.maximumCardPoints = 10;
        mainCharacterStats.currentPlayerStickerPoints = 2;
        mainCharacterStats.maximumStickerPoints = 2;
        mainCharacterStats.strength = 5;
        mainCharacterStats.defense = 2;
        mainCharacterStats.moveSpeed = 15;
        mainCharacterStats.increasedSprintSpeed = 3;
        mainCharacterStats.cardPointHealPercentage = 0.1f;

        ResetTreasureData();
        ResetFieldCardData();
        ResetMoonStoneData();

        MenuController.instance.gameObject.SetActive(true);
        MenuController.instance.SetDefaultDeck();
        MenuController.instance._StickerPage.ResetStickerPage();
        MenuController.instance._StickerPowerManager.ClearStickerLists();
        MenuController.instance._SettingsMenu.DisableTutorialButtons();
        MenuController.instance.MenuPlayerLights.SetActive(false);

        ResetCollectedCards();

        ResetShopCardsForest();
        ResetShopCardsDesert();
        ResetShopCardsArctic();
        ResetShopCardsGraveyard();
        ResetShopCardsCastle();

        ResetShopStickersForest();
        ResetShopStickersDesert();
        ResetShopStickersArctic();
        ResetShopStickersGraveyard();
        ResetShopStickersCastle();

        ResetBossStages();

        NodeManager.instance.ForestStagesUnlocked = 1;
        NodeManager.instance.DesertStagesUnlocked = 1;
        NodeManager.instance.ArcticStagesUnlocked = 1;
        NodeManager.instance.GraveStagesUnlocked = 1;
        NodeManager.instance.CastleStagesUnlocked = 1;
        NodeManager.instance.UnlockedWorlds = 1;
        NodeManager.instance.WorldIndex = 0;
        NodeManager.instance.ResetSecretStages();

        worldEnvironmentData.changedWeather = false;
        worldEnvironmentData.changedDay = false;
    }

    private void ResetBossStages()
    {
        bossData.forestBossDefeated = false;
        bossData.desertBossDefeated = false;
        bossData.arcticBossDefeated = false;
        bossData.graveBossDefeated = false;

        bossData.forestSecretBossDefeated = false;
        bossData.desertSecretBossDefeated = false;
        bossData.arcticSecretBossDefeated = false;
        bossData.graveSecretBossDefeated = false;
        bossData.castleSecretBossDefeated = false;
    }

    private void ResetShopCardsForest()
    {
        shopInformation.cardShopForest.Clear();
        
        for(int i = 0; i < shopInformation.startingCardShopForest.Count; i++)
        {
            CardTemplate shopCardTemplate = shopInformation.startingCardShopForest[i];

            shopInformation.cardShopForest.Add(shopCardTemplate);
        }
    }

    private void ResetShopCardsDesert()
    {
        shopInformation.cardShopDesert.Clear();

        for (int i = 0; i < shopInformation.startingCardShopDesert.Count; i++)
        {
            CardTemplate shopCardTemplate = shopInformation.startingCardShopDesert[i];

            shopInformation.cardShopDesert.Add(shopCardTemplate);
        }
    }

    private void ResetShopCardsArctic()
    {
        shopInformation.cardShopArctic.Clear();

        for (int i = 0; i < shopInformation.startingCardShopArctic.Count; i++)
        {
            CardTemplate shopCardTemplate = shopInformation.startingCardShopArctic[i];

            shopInformation.cardShopArctic.Add(shopCardTemplate);
        }
    }

    private void ResetShopCardsGraveyard()
    {
        shopInformation.cardShopGraveyard.Clear();

        for (int i = 0; i < shopInformation.startingCardShopGraveyard.Count; i++)
        {
            CardTemplate shopCardTemplate = shopInformation.startingCardShopGraveyard[i];

            shopInformation.cardShopGraveyard.Add(shopCardTemplate);
        }
    }

    private void ResetShopCardsCastle()
    {
        shopInformation.cardShopCastle.Clear();

        for (int i = 0; i < shopInformation.startingCardShopCastle.Count; i++)
        {
            CardTemplate shopCardTemplate = shopInformation.startingCardShopCastle[i];

            shopInformation.cardShopCastle.Add(shopCardTemplate);
        }
    }

    private void ResetShopStickersForest()
    {
        shopInformation.stickerShopForest.Clear();

        for (int i = 0; i < shopInformation.startingStickerShopForest.Count; i++)
        {
            StickerInformation shopStickerInfo = shopInformation.startingStickerShopForest[i];

            shopInformation.stickerShopForest.Add(shopStickerInfo);
        }
    }

    private void ResetShopStickersDesert()
    {
        shopInformation.stickerShopDesert.Clear();

        for (int i = 0; i < shopInformation.startingStickerShopDesert.Count; i++)
        {
            StickerInformation shopStickerInfo = shopInformation.startingStickerShopDesert[i];

            shopInformation.stickerShopDesert.Add(shopStickerInfo);
        }
    }

    private void ResetShopStickersArctic()
    {
        shopInformation.stickerShopArctic.Clear();

        for (int i = 0; i < shopInformation.startingStickerShopArctic.Count; i++)
        {
            StickerInformation shopStickerInfo = shopInformation.startingStickerShopArctic[i];

            shopInformation.stickerShopArctic.Add(shopStickerInfo);
        }
    }

    private void ResetShopStickersGraveyard()
    {
        shopInformation.stickerShopGraveyard.Clear();

        for (int i = 0; i < shopInformation.startingStickerShopGraveyard.Count; i++)
        {
            StickerInformation shopStickerInfo = shopInformation.startingStickerShopGraveyard[i];

            shopInformation.stickerShopGraveyard.Add(shopStickerInfo);
        }
    }

    private void ResetShopStickersCastle()
    {
        shopInformation.stickerShopCastle.Clear();

        for (int i = 0; i < shopInformation.startingStickerShopCastle.Count; i++)
        {
            StickerInformation shopStickerInfo = shopInformation.startingStickerShopCastle[i];

            shopInformation.stickerShopCastle.Add(shopStickerInfo);
        }
    }

    private void ResetCollectedCards()
    {
        for (int i = 0; i < MenuController.instance._CardCollection.CardCollectionParents.Length; i++)
        {
            for (int j = 0; j < MenuController.instance._CardCollection.CardCollectionParents[i].childCount; j++)
            {
                Destroy(MenuController.instance._CardCollection.CardCollectionParents[i].GetChild(j).gameObject);
            }
        }

        MenuController.instance._CardCollection.ClearList();
        MenuController.instance._CardCollection.AddStartingCardCollection();
    }

    private void ResetTreasureData()
    {
        for(int i = 0; i < treasureData.worldTreasures.Length; i++)
        {
            for(int j = 0; j < treasureData.worldTreasures[i].stageTreasures.Length; j++)
            {
                for(int k = 0; k < treasureData.worldTreasures[i].stageTreasures[j].treasures.Length; k++)
                {
                    treasureData.worldTreasures[i].stageTreasures[j].treasures[k] = 0;
                }
            }
        }

        for (int i = 0; i < treasureData.worldTreasures.Length; i++)
        {
            for (int j = 0; j < treasureData.worldTreasures[i].secretStageTreasures.Length; j++)
            {
                for (int k = 0; k < treasureData.worldTreasures[i].secretStageTreasures[j].treasures.Length; k++)
                {
                    treasureData.worldTreasures[i].secretStageTreasures[j].treasures[k] = 0;
                }
            }
        }
    }

    private void ResetFieldCardData()
    {
        for (int i = 0; i < fieldCardData.worldCards.Length; i++)
        {
            for (int j = 0; j < fieldCardData.worldCards[i].stageCards.Length; j++)
            {
                for(int k = 0; k < fieldCardData.worldCards[i].stageCards[j].fieldCards.Length; k++)
                {
                    fieldCardData.worldCards[i].stageCards[j].fieldCards[k] = 0;
                }
            }
        }

        for (int i = 0; i < fieldCardData.worldCards.Length; i++)
        {
            for (int j = 0; j < fieldCardData.worldCards[i].secretStageCards.Length; j++)
            {
                for (int k = 0; k < fieldCardData.worldCards[i].secretStageCards[j].fieldCards.Length; k++)
                {
                    fieldCardData.worldCards[i].secretStageCards[j].fieldCards[k] = 0;
                }
            }
        }
    }

    private void ResetMoonStoneData()
    {
        for (int i = 0; i < moonStoneData.worldMoonStones.Length; i++)
        {
            for (int j = 0; j < moonStoneData.worldMoonStones[i].stageMoonstones.Length; j++)
            {
                for(int k = 0; k < moonStoneData.worldMoonStones[i].stageMoonstones[j].moonStones.Length; k++)
                {
                    moonStoneData.worldMoonStones[i].stageMoonstones[j].moonStones[k] = 0;
                }
            }
        }

        for (int i = 0; i < moonStoneData.worldMoonStones.Length; i++)
        {
            for (int j = 0; j < moonStoneData.worldMoonStones[i].secretStageMoonstones.Length; j++)
            {
                for (int k = 0; k < moonStoneData.worldMoonStones[i].secretStageMoonstones[j].moonStones.Length; k++)
                {
                    moonStoneData.worldMoonStones[i].secretStageMoonstones[j].moonStones[k] = 0;
                }
            }
        }
    }
}