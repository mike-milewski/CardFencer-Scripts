using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Steamworks;

public class SalvageCardPanel : MonoBehaviour
{
    [SerializeField]
    MainCharacterStats mainCharacterStats;

    [SerializeField]
    private AnimatorEvents animatorEvents;

    [SerializeField]
    private Button confirmButton, declineButton;

    private MenuCard menuCrd = null;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private TextMeshProUGUI cardToSalvageText, goldToReceiveText;

    public Button ConfirmButton => confirmButton;

    public Button DeclineButton => declineButton;

    public void SetUpSalvagePanel(MenuCard menuCard)
    {
        menuCrd = menuCard;

        cardToSalvageText.text = "Do you want to salvage " + menuCard._cardTemplate.cardName + "?\n<size=18><color=red>Warning: This action cannot be undone.</color>";

        goldToReceiveText.text = menuCard._cardTemplate.sellValue.ToString();

        animator.Play("Open");
    }

    public void SetUpSalvageAllPanel(MenuCard menuCard, Transform stashCards)
    {
        menuCrd = menuCard;

        cardToSalvageText.text = "Do you want to salvage all cards in the stash?\n<size=18><color=red>Warning: This action cannot be undone.</color>";

        int totalMoney = 0;

        foreach(MenuCard card in stashCards.GetComponentsInChildren<MenuCard>())
        {
            totalMoney += card._cardTemplate.sellValue;

            goldToReceiveText.text = totalMoney.ToString();
        }

        animator.Play("Open");
    }

    public void Salvage()
    {
        mainCharacterStats.money += mainCharacterStats.money >= mainCharacterStats.maximumMoney ? 0 : menuCrd._cardTemplate.sellValue;

        if(mainCharacterStats.money >= mainCharacterStats.maximumMoney)
        {
            mainCharacterStats.money = mainCharacterStats.maximumMoney;
        }

        DestroyImmediate(menuCrd.gameObject);

        PlayerInformation playerInformation = FindObjectOfType<PlayerInformation>();

        playerInformation.UpdatePlayerInformation();

        animatorEvents.ObjectToSelectOnClose = MenuController.instance._DeckMenu.CurrentStashParent.childCount > 0 ? MenuController.instance._DeckMenu.CurrentStashParent.GetChild(0).gameObject :
                                               MenuController.instance._DeckMenu._CardCategories[0].gameObject;

        animator.Play("Reverse");

        MenuController.instance._DeckMenu.AdjustDeckCardsNavigations();
        MenuController.instance._DeckMenu.AdjustStashCardsNavigations();
        MenuController.instance._DeckMenu.AdjustCardCategoryButtonNavigations();
        MenuController.instance.UpdateStashCount();

        if(SteamManager.Initialized)
        {
            int money_deepPockets = 0;
            int money_deeperPockets = 0;
            int money_deepestPockets = 0;

            SteamUserStats.GetStat("ACH_1K_GOLD", out money_deepPockets);
            money_deepPockets += menuCrd._cardTemplate.sellValue;
            SteamUserStats.SetStat("ACH_1K_GOLD", money_deepPockets);
            SteamUserStats.StoreStats();

            SteamUserStats.GetStat("ACH_5K_GOLD", out money_deeperPockets);
            money_deeperPockets += menuCrd._cardTemplate.sellValue;
            SteamUserStats.SetStat("ACH_5K_GOLD", money_deeperPockets);
            SteamUserStats.StoreStats();

            SteamUserStats.GetStat("ACH_10K_GOLD", out money_deepestPockets);
            money_deepestPockets += menuCrd._cardTemplate.sellValue;
            SteamUserStats.SetStat("ACH_10K_GOLD", money_deepestPockets);
            SteamUserStats.StoreStats();

            SteamUserStats.GetAchievement("ACH_DEEP_POCKETS", out bool achievementCompleted);

            if (!achievementCompleted)
            {
                if (money_deepPockets >= 1000)
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
    }

    public void SalvageAll(Transform stashCards)
    {
        foreach (MenuCard card in stashCards.GetComponentsInChildren<MenuCard>())
        {
            mainCharacterStats.money += mainCharacterStats.money >= mainCharacterStats.maximumMoney ? 0 : card._cardTemplate.sellValue;

            if (mainCharacterStats.money >= mainCharacterStats.maximumMoney)
            {
                mainCharacterStats.money = mainCharacterStats.maximumMoney;
            }

            DestroyImmediate(card.gameObject);

            PlayerInformation playerInformation = FindObjectOfType<PlayerInformation>();

            playerInformation.UpdatePlayerInformation();
        }

        animatorEvents.ObjectToSelectOnClose = MenuController.instance._DeckMenu._CardCategories[0].gameObject;

        animator.Play("Reverse");

        MenuController.instance._DeckMenu.AdjustDeckCardsNavigations();
        MenuController.instance._DeckMenu.AdjustStashCardsNavigations();
        MenuController.instance._DeckMenu.AdjustCardCategoryButtonNavigations();
        MenuController.instance.UpdateStashCount();

        if (SteamManager.Initialized)
        {
            int money_deepPockets = 0;
            int money_deeperPockets = 0;
            int money_deepestPockets = 0;

            SteamUserStats.GetStat("ACH_1K_GOLD", out money_deepPockets);
            money_deepPockets += menuCrd._cardTemplate.sellValue;
            SteamUserStats.SetStat("ACH_1K_GOLD", money_deepPockets);
            SteamUserStats.StoreStats();

            SteamUserStats.GetStat("ACH_5K_GOLD", out money_deeperPockets);
            money_deeperPockets += menuCrd._cardTemplate.sellValue;
            SteamUserStats.SetStat("ACH_5K_GOLD", money_deeperPockets);
            SteamUserStats.StoreStats();

            SteamUserStats.GetStat("ACH_10K_GOLD", out money_deepestPockets);
            money_deepestPockets += menuCrd._cardTemplate.sellValue;
            SteamUserStats.SetStat("ACH_10K_GOLD", money_deepestPockets);
            SteamUserStats.StoreStats();

            SteamUserStats.GetAchievement("ACH_DEEP_POCKETS", out bool achievementCompleted);

            if (!achievementCompleted)
            {
                if (money_deepPockets >= 1000)
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
    }

    public void DeclineButtonListener(MenuCard card)
    {
        animatorEvents.ObjectToSelectOnClose = card.gameObject;
    }

    public void ConfirmButtonListener(GameObject selectedObj)
    {
        animatorEvents.ObjectToSelectOnClose = MenuController.instance._DeckMenu.CurrentStashParent.childCount > 0 ? MenuController.instance._DeckMenu.CurrentStashParent.GetChild(0).gameObject : selectedObj;
    }
}