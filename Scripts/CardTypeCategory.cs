using UnityEngine;
using TMPro;

public class CardTypeCategory : MonoBehaviour
{
    [SerializeField]
    private RectTransform allCardsCategory, powerCardsCategory, magicCardsCategory, supportCardsCategory, mysticCardsCategory, itemCardsCategory;

    [SerializeField]
    private ScrollRectEx scrollRect;

    [SerializeField]
    private Transform cardParent;

    [SerializeField]
    private TextMeshProUGUI currentCategoryText, cardCountText;

    public RectTransform AllCardsCategory => allCardsCategory;

    public void ViewAllCards()
    {
        allCardsCategory.gameObject.SetActive(true);
        powerCardsCategory.gameObject.SetActive(false);
        magicCardsCategory.gameObject.SetActive(false);
        supportCardsCategory.gameObject.SetActive(false);
        mysticCardsCategory.gameObject.SetActive(false);
        itemCardsCategory.gameObject.SetActive(false);

        scrollRect.content = allCardsCategory;

        foreach(MenuCard menuCard in cardParent.GetComponentsInChildren<MenuCard>(true))
        {
            menuCard.transform.SetParent(allCardsCategory.transform);
        }

        currentCategoryText.text = "All";

        var powerCards = Resources.LoadAll("Cards/Power");
        var magicCards = Resources.LoadAll("Cards/Magic");
        var supportCards = Resources.LoadAll("Cards/Support");
        var itemCards = Resources.LoadAll("Cards/Item");
        var mysticCards = Resources.LoadAll("Cards/Mystic");

        int totalAmount = powerCards.Length + magicCards.Length + supportCards.Length + itemCards.Length + mysticCards.Length;

        cardCountText.text = allCardsCategory.childCount + "/" + totalAmount;
    }

    public void ViewPowerCards()
    {
        powerCardsCategory.gameObject.SetActive(true);
        allCardsCategory.gameObject.SetActive(false);
        magicCardsCategory.gameObject.SetActive(false);
        supportCardsCategory.gameObject.SetActive(false);
        mysticCardsCategory.gameObject.SetActive(false);
        itemCardsCategory.gameObject.SetActive(false);

        scrollRect.content = powerCardsCategory;

        foreach (MenuCard menuCard in cardParent.GetComponentsInChildren<MenuCard>(true))
        {
            if(menuCard._cardTemplate.cardType == CardType.Action)
            {
                menuCard.transform.SetParent(powerCardsCategory.transform);
            }
        }

        currentCategoryText.text = "Power";

        var powerCards = Resources.LoadAll("Cards/Power");

        cardCountText.text = powerCardsCategory.childCount + "/" + powerCards.Length;
    }

    public void ViewMagicCards()
    {
        magicCardsCategory.gameObject.SetActive(true);
        allCardsCategory.gameObject.SetActive(false);
        powerCardsCategory.gameObject.SetActive(false);
        supportCardsCategory.gameObject.SetActive(false);
        mysticCardsCategory.gameObject.SetActive(false);
        itemCardsCategory.gameObject.SetActive(false);

        scrollRect.content = magicCardsCategory;

        foreach (MenuCard menuCard in cardParent.GetComponentsInChildren<MenuCard>(true))
        {
            if (menuCard._cardTemplate.cardType == CardType.Magic)
            {
                menuCard.transform.SetParent(magicCardsCategory.transform);
            }
        }

        currentCategoryText.text = "Magic";

        var magicCards = Resources.LoadAll("Cards/Magic");

        cardCountText.text = magicCardsCategory.childCount + "/" + magicCards.Length;
    }

    public void ViewSupportCards()
    {
        supportCardsCategory.gameObject.SetActive(true);
        allCardsCategory.gameObject.SetActive(false);
        powerCardsCategory.gameObject.SetActive(false);
        magicCardsCategory.gameObject.SetActive(false);
        mysticCardsCategory.gameObject.SetActive(false);
        itemCardsCategory.gameObject.SetActive(false);

        scrollRect.content = supportCardsCategory;

        foreach (MenuCard menuCard in cardParent.GetComponentsInChildren<MenuCard>(true))
        {
            if (menuCard._cardTemplate.cardType == CardType.Support)
            {
                menuCard.transform.SetParent(supportCardsCategory.transform);
            }
        }

        currentCategoryText.text = "Support";

        var supportCards = Resources.LoadAll("Cards/Support");

        cardCountText.text = supportCardsCategory.childCount + "/" + supportCards.Length;
    }

    public void ViewMysticCards()
    {
        mysticCardsCategory.gameObject.SetActive(true);
        allCardsCategory.gameObject.SetActive(false);
        powerCardsCategory.gameObject.SetActive(false);
        magicCardsCategory.gameObject.SetActive(false);
        supportCardsCategory.gameObject.SetActive(false);
        itemCardsCategory.gameObject.SetActive(false);

        scrollRect.content = mysticCardsCategory;

        foreach (MenuCard menuCard in cardParent.GetComponentsInChildren<MenuCard>(true))
        {
            if (menuCard._cardTemplate.cardType == CardType.Mystic)
            {
                menuCard.transform.SetParent(mysticCardsCategory.transform);
            }
        }

        currentCategoryText.text = "Mystic";

        var mysticCards = Resources.LoadAll("Cards/Mystic");

        cardCountText.text = mysticCardsCategory.childCount + "/" + mysticCards.Length;
    }

    public void ViewItemCards()
    {
        itemCardsCategory.gameObject.SetActive(true);
        allCardsCategory.gameObject.SetActive(false);
        powerCardsCategory.gameObject.SetActive(false);
        magicCardsCategory.gameObject.SetActive(false);
        supportCardsCategory.gameObject.SetActive(false);
        mysticCardsCategory.gameObject.SetActive(false);

        scrollRect.content = itemCardsCategory;

        foreach (MenuCard menuCard in cardParent.GetComponentsInChildren<MenuCard>(true))
        {
            if (menuCard._cardTemplate.cardType == CardType.Item)
            {
                menuCard.transform.SetParent(itemCardsCategory.transform);
            }
        }

        currentCategoryText.text = "Item";

        var itemCards = Resources.LoadAll("Cards/Item");

        cardCountText.text = itemCardsCategory.childCount + "/" + itemCards.Length;
    }

    public void UpdateCardCount()
    {
        if(allCardsCategory.gameObject.activeSelf)
        {
            var powerCards = Resources.LoadAll("Cards/Power");
            var magicCards = Resources.LoadAll("Cards/Magic");
            var supportCards = Resources.LoadAll("Cards/Support");
            var itemCards = Resources.LoadAll("Cards/Item");
            var mysticCards = Resources.LoadAll("Cards/Mystic");

            int totalAmount = powerCards.Length + magicCards.Length + supportCards.Length + itemCards.Length + mysticCards.Length;

            cardCountText.text = allCardsCategory.childCount + "/" + totalAmount;
        }
        else if(powerCardsCategory.gameObject.activeSelf)
        {
            var powerCards = Resources.LoadAll("Cards/Power");

            cardCountText.text = powerCardsCategory.childCount + "/" + powerCards.Length;
        }
        else if (magicCardsCategory.gameObject.activeSelf)
        {
            var magicCards = Resources.LoadAll("Cards/Magic");

            cardCountText.text = magicCardsCategory.childCount + "/" + magicCards.Length;
        }
        else if(supportCardsCategory.gameObject.activeSelf)
        {
            var supportCards = Resources.LoadAll("Cards/Support");

            cardCountText.text = supportCardsCategory.childCount + "/" + supportCards.Length;
        }
        else if(itemCardsCategory.gameObject.activeSelf)
        {
            var itemCards = Resources.LoadAll("Cards/Item");

            cardCountText.text = itemCardsCategory.childCount + "/" + itemCards.Length;
        }
        else if(mysticCardsCategory.gameObject.activeSelf)
        {
            var mysticCards = Resources.LoadAll("Cards/Mystic");

            cardCountText.text = mysticCardsCategory.childCount + "/" + mysticCards.Length;
        }
    }

    public Transform SetCardParent(MenuCard menuCard)
    {
        Transform trans = cardParent;

        if(allCardsCategory.gameObject.activeSelf)
        {
            trans = allCardsCategory.transform;
        }
        else
        {
            switch(menuCard._cardTemplate.cardType)
            {
                case (CardType.Action):
                    trans = powerCardsCategory.transform;
                    break;
                case (CardType.Magic):
                    trans = magicCardsCategory.transform;
                    break;
                case (CardType.Support):
                    trans = supportCardsCategory.transform;
                    break;
                case (CardType.Item):
                    trans = itemCardsCategory.transform;
                    break;
                case (CardType.Mystic):
                    trans = mysticCardsCategory.transform;
                    break;
            }
        }

        menuCard.transform.SetParent(trans, false);

        return trans;
    }
}