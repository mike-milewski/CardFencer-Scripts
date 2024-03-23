using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Steamworks;

public class CardCollection : MonoBehaviour
{
    [SerializeField]
    private List<CardTemplate> cardTemplates = new List<CardTemplate>();

    [SerializeField]
    private List<MenuCard> cardParentCards = new List<MenuCard>();

    [SerializeField]
    private RectTransform[] cardCollectionParents;

    [SerializeField]
    private Selectable[] categorySelectables;

    [SerializeField]
    private CardTypeCategory cardTypeCategory;

    [SerializeField]
    private MenuCard menuCardPrefab;

    [SerializeField]
    private MainCharacterStats mainCharacterStats;

    public List<MenuCard> CardParentCards => cardParentCards;

    public List<CardTemplate> CardTemplates => cardTemplates;

    public RectTransform[] CardCollectionParents => cardCollectionParents;

    public void ClearList()
    {
        cardTemplates.Clear();
    }

    public void SetDefaultCardCollectionView()
    {
        foreach (RectTransform rect in cardCollectionParents)
        {
            if (rect.gameObject.activeSelf)
            {
                rect.gameObject.SetActive(false);
            }
        }

        cardCollectionParents[0].gameObject.SetActive(true);
    }

    public void AddStartingCardCollection()
    {
        SetDefaultCardCollectionView();

        for (int i = 0; i < mainCharacterStats.startingCards.Count; i++)
        {
            AddNewCard(mainCharacterStats.startingCards[i]);
        }

        if (mainCharacterStats.startingItem != null)
            MenuController.instance._CardCollection.AddNewCard(mainCharacterStats.startingItem);

        PopulateCardList();
    }

    public void AddNewCard(CardTemplate cardToCompare)
    {
        cardTemplates.Add(cardToCompare);

        CheckCardMasterAchievement();

        CheckDuplicates();
    }

    private void CheckDuplicates()
    {
        cardTemplates = cardTemplates.Distinct().ToList();
    }

    private void PopulateCardList()
    {
        foreach(CardTemplate cardTemp in cardTemplates)
        {
            var card = Instantiate(menuCardPrefab);

            card.gameObject.SetActive(true);

            cardTypeCategory.SetCardParent(card);

            card._cardTemplate = cardTemp;

            card.UpdateCardInformation();
        }
    }

    public void LoadCardCollection(CardTemplate cardTemplate)
    {
        cardTemplates.Add(cardTemplate);

        var card = Instantiate(menuCardPrefab);

        card.gameObject.SetActive(true);

        cardTypeCategory.SetCardParent(card);

        card._cardTemplate = cardTemplate;

        card.UpdateCardInformation();

        CheckCardMasterAchievement();
    }

    public void CheckCurrentList(CardTemplate template)
    {
        int sameIndex = 0;

        foreach(CardTemplate cardTemp in cardTemplates)
        {
            if(template.cardName == cardTemp.cardName)
            {
                sameIndex++;
            }
        }

        if(sameIndex <= 0)
        {
            cardTemplates.Add(template);

            var card = Instantiate(menuCardPrefab);

            card.gameObject.SetActive(true);

            card._cardTemplate = template;

            cardTypeCategory.SetCardParent(card);

            card.UpdateCardInformation();

            CheckCardMasterAchievement();
        }
    }

    public void CheckCardMasterAchievement()
    {
        if (SteamManager.Initialized)
        {
            int currentCards = cardTemplates.Count;

            SteamUserStats.GetStat("ACH_CARD_COUNT", out currentCards);
            currentCards = cardTemplates.Count;
            SteamUserStats.SetStat("ACH_CARD_COUNT", currentCards);
            SteamUserStats.StoreStats();

            SteamUserStats.GetAchievement("ACH_CARD_MASTER", out bool achievementCompleted);

            if (!achievementCompleted)
            {
                var powerCards = Resources.LoadAll("Cards/Power");
                var magicCards = Resources.LoadAll("Cards/Magic");
                var supportCards = Resources.LoadAll("Cards/Support");
                var itemCards = Resources.LoadAll("Cards/Item");
                var mysticCards = Resources.LoadAll("Cards/Mystic");

                int totalAmount = powerCards.Length + magicCards.Length + supportCards.Length + itemCards.Length + mysticCards.Length;

                if (currentCards >= totalAmount)
                {
                    SteamUserStats.SetAchievement("ACH_CARD_MASTER");
                    SteamUserStats.StoreStats();
                }
            }
        }
    }

    public void AdjustCardCollectionNavigations()
    {
        cardParentCards.Clear();

        int categoryIndex = 0;

        for(int i = 0; i < cardCollectionParents.Length; i++)
        {
            if(cardCollectionParents[i].gameObject.activeSelf)
            {
                if (cardCollectionParents[i].childCount > 0)
                {
                    categoryIndex = i;
                    foreach (MenuCard menuCard in cardCollectionParents[i].GetComponentsInChildren<MenuCard>())
                    {
                        if (menuCard.gameObject.activeSelf)
                        {
                            cardParentCards.Add(menuCard);
                        }
                    }
                }
            }
        }

        if (cardParentCards.Count <= 0) return;

        foreach (Selectable s in categorySelectables)
        {
            Navigation nav = s.navigation;

            nav.selectOnDown = cardParentCards[0].GetComponent<Selectable>();

            s.navigation = nav;
        }

        for (int i = 0; i < cardParentCards.Count; i++)
        {
            Navigation cardNav = cardParentCards[i].GetComponent<Selectable>().navigation;

            cardNav.selectOnLeft = null;
            cardNav.selectOnRight = null;
            cardNav.selectOnDown = null;
            cardNav.selectOnUp = null;

            if(i == 0)
            {
                if(cardParentCards.Count > 1)
                {
                    cardNav.selectOnLeft = cardParentCards[cardParentCards.Count - 1].GetComponent<Selectable>();
                    cardNav.selectOnRight = cardParentCards[i + 1].GetComponent<Selectable>();
                }
            }
            else if(i >= cardParentCards.Count - 1)
            {
                cardNav.selectOnRight = cardParentCards[0].GetComponent<Selectable>();
                cardNav.selectOnLeft = cardParentCards[i - 1].GetComponent<Selectable>();
            }
            else
            {
                cardNav.selectOnLeft = cardParentCards[i - 1].GetComponent<Selectable>();
                cardNav.selectOnRight = cardParentCards[i + 1].GetComponent<Selectable>();
            }



            if(i <= 4)
            {
                cardNav.selectOnUp = categorySelectables[categoryIndex];
            }

            if (cardParentCards.Count > 4)
            {
                int downWardCardIndex = i + 5;
                cardNav.selectOnDown = cardParentCards[downWardCardIndex >= cardParentCards.Count - 1 ? cardParentCards.Count - 1 : downWardCardIndex].GetComponent<Selectable>();
            }

            if(i > 4)
            {
                int upWardCardIndex = i - 5;
                cardNav.selectOnUp = cardParentCards[upWardCardIndex].GetComponent<Selectable>();
            }

            cardParentCards[i].GetComponent<Selectable>().navigation = cardNav;
        }
    }
}