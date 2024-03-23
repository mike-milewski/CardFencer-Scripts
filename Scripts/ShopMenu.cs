using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.UI;
using Steamworks;

public class ShopMenu : MonoBehaviour
{
    public static ShopMenu instance = null;

    [SerializeField]
    private ShopInformation shopInformation;

    [SerializeField]
    private ShopKeeper shopKeeper;

    [SerializeField]
    private List<CardTemplate> cardTemplate = new List<CardTemplate>();

    [SerializeField]
    private List<StickerInformation> stickerInformation = new List<StickerInformation>();

    private MenuCard currentlyViewedCard;

    [SerializeField]
    private CameraFollow cameraFollow;

    private MenuCard menuCardPrefab, cardToBuyPrefab, upgradeCardPreview;

    private Sticker stickerPrefab, stickerPrefabToBuy;

    private CardTypeCategory cardTypeCategory;

    [SerializeField]
    private Selectable buyCardsSelectable, buyStickersSelectable, upgradeCardsSelectable, purchaseUpgradeSelectable, upgradeFromDeckSelectable, upgradeFromStashSelecable;

    [SerializeField]
    private MainCharacterStats mainCharacterStats;

    [SerializeField]
    private PlayerMovement playerMovement;

    [SerializeField]
    private PlayerMenuInfo playerMenuInfo;

    [SerializeField]
    private Button buyCardsButton;

    [SerializeField]
    private ScrollRectEx buyCardsScroll, upgradeCardsScrollDeck, upgradeCardsScrollStash, stickerListScroll;

    [SerializeField]
    private GameObject upgradePanel, buyCardsList, upgradeCardsListDeck, upgradeCardsListStash, deckListButton, stashListButton, upgradeCardButton, cardListTitle, newStatusEffectBanner, stickerStatusDurationPanel,
                       objectSelectForInputManager, buyTypePanel;

    [SerializeField]
    private RectTransform buyParticleRectTransform;

    [SerializeField]
    private Image statusEffectImage;

    [SerializeField]
    private ParticleSystem cardUpgradeParticle, buyParticle;

    [SerializeField]
    private Transform cardListParentBuy, cardListParentDeck, cardListParentStash, stickerList, stickerListParent;

    [SerializeField]
    private Animator animator, errorMessageAnimator, errorMessageAnimatorUpgrade, stickerInfoPanelAnimator, statusPanelAnimator, buyCardStatusPanelAnimator;

    [SerializeField]
    private TextMeshProUGUI cardListTitleFrameText, moneyText, errorMessageText, errorMessageTexttUpgrade, stickerPanelInfoText, stickerPointsText, stickerNameText, statusPanelNameText, statusPanelInfoText,
                            buyCardStatusPanelNameText, buyCardStatusPanelInfoText, buyCardStatusDurationText, upgradeCardStatusDurationText, stickerPanelStatusEffectText, stickerInfoStatusDescription,
                            stickerStatusDurationText, buyTypeText;

    private bool initialBuyCardsSetUp, initialBuyStickersSetUp, clearedCurrentCards, menuAnimationEvent, canSetToggle;

    public TextMeshProUGUI CardListTitleFrameText
    {
        get
        {
            return cardListTitleFrameText;
        }
        set
        {
            cardListTitleFrameText = value;
        }
    }

    public GameObject UpgradePanel => upgradePanel;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        Scene scene = SceneManager.GetActiveScene();

        cardTypeCategory = MenuController.instance._CardTypeCategory;

        menuCardPrefab = MenuController.instance._MenuCard;

        cardToBuyPrefab = MenuController.instance._MenuCard;

        stickerPrefab = MenuController.instance.StickerPrefab;

        stickerPrefabToBuy = MenuController.instance.StickerPrefabToBuy;

        switch(scene.name)
        {
            case ("ForestTown"):
                foreach(CardTemplate card in shopInformation.cardShopForest)
                {
                    cardTemplate.Add(card);

                    shopInformation.cardShopForest = shopInformation.cardShopForest.OrderBy(cardTemplate => cardTemplate.buyValue).ToList();
                }
                foreach(StickerInformation stickerInfo in shopInformation.stickerShopForest)
                {
                    stickerInformation.Add(stickerInfo);

                    shopInformation.stickerShopForest = shopInformation.stickerShopForest.OrderBy(stickerInformation => stickerInformation.buyValue).ToList();
                }
                break;
            case ("DesertTown"):
                foreach (CardTemplate card in shopInformation.cardShopDesert)
                {
                    cardTemplate.Add(card);

                    shopInformation.cardShopDesert = shopInformation.cardShopDesert.OrderBy(cardTemplate => cardTemplate.buyValue).ToList();
                }
                foreach (StickerInformation stickerInfo in shopInformation.stickerShopDesert)
                {
                    stickerInformation.Add(stickerInfo);

                    shopInformation.stickerShopDesert = shopInformation.stickerShopDesert.OrderBy(stickerInformation => stickerInformation.buyValue).ToList();
                }
                break;
            case ("ArcticTown"):
                foreach (CardTemplate card in shopInformation.cardShopArctic)
                {
                    cardTemplate.Add(card);

                    shopInformation.cardShopArctic = shopInformation.cardShopArctic.OrderBy(cardTemplate => cardTemplate.buyValue).ToList();
                }
                foreach (StickerInformation stickerInfo in shopInformation.stickerShopArctic)
                {
                    stickerInformation.Add(stickerInfo);

                    shopInformation.stickerShopArctic = shopInformation.stickerShopArctic.OrderBy(stickerInformation => stickerInformation.buyValue).ToList();
                }
                break;
            case ("GraveyardTown"):
                foreach (CardTemplate card in shopInformation.cardShopGraveyard)
                {
                    cardTemplate.Add(card);

                    shopInformation.cardShopGraveyard = shopInformation.cardShopGraveyard.OrderBy(cardTemplate => cardTemplate.buyValue).ToList();
                }
                foreach (StickerInformation stickerInfo in shopInformation.stickerShopGraveyard)
                {
                    stickerInformation.Add(stickerInfo);

                    shopInformation.stickerShopGraveyard = shopInformation.stickerShopGraveyard.OrderBy(stickerInformation => stickerInformation.buyValue).ToList();
                }
                break;
            case ("CastleTown"):
                foreach (CardTemplate card in shopInformation.cardShopCastle)
                {
                    cardTemplate.Add(card);

                    shopInformation.cardShopCastle = shopInformation.cardShopCastle.OrderBy(cardTemplate => cardTemplate.buyValue).ToList();
                }
                foreach (StickerInformation stickerInfo in shopInformation.stickerShopCastle)
                {
                    stickerInformation.Add(stickerInfo);

                    shopInformation.stickerShopCastle = shopInformation.stickerShopCastle.OrderBy(stickerInformation => stickerInformation.buyValue).ToList();
                }
                break;
        }

        cardTemplate = cardTemplate.OrderBy(cardTemplate => cardTemplate.buyValue).ToList();
        stickerInformation = stickerInformation.OrderBy(stickerInformation => stickerInformation.buyValue).ToList();
    }

    private void Start()
    {
        MenuCard preview = Instantiate(MenuController.instance.ShopPreviewCard, upgradePanel.transform);

        preview._propogateDrag.enabled = false;

        preview.gameObject.SetActive(true);

        RectTransform rect = preview.GetComponent<RectTransform>();

        rect.anchorMin = new Vector2(0.5f, 0.5f);
        rect.anchorMax = new Vector2(0.5f, 0.5f);

        rect.anchoredPosition = new Vector2(0, 0);

        upgradeCardPreview = preview;
    }

    private void Update()
    {
        if (SteamOverlayPause.instance.IsPaused) return;

        if(InputManager.instance.ControllerPluggedIn)
        {
            if (shopKeeper.OpenedShopMenu)
            {
                if(InputManager.instance.ControllerName == "xbox")
                {
                    if (Input.GetButtonDown("XboxCancel"))
                    {
                        CloseShop();

                        AudioManager.instance.PlaySoundEffect(AudioManager.instance.CloseMenuAudio);
                    }
                }
                else
                {
                    if (Input.GetButtonDown("Ps4Cancel"))
                    {
                        CloseShop();

                        AudioManager.instance.PlaySoundEffect(AudioManager.instance.CloseMenuAudio);
                    }
                }
            }
        }
        else
        {
            if(shopKeeper.OpenedShopMenu)
            {
                if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Tab))
                {
                    CloseShop();

                    AudioManager.instance.PlaySoundEffect(AudioManager.instance.CloseMenuAudio);
                }
            }
        }
    }

    public void OpenShop()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        AudioManager.instance.PlaySoundEffect(AudioManager.instance.OpenMenuAudio);

        InputManager.instance.ForceCursorOn = true;

        playerMovement.PlayerAnimator.Play("IdlePose");

        playerMovement.enabled = false;

        DisableUpgradeButton();

        MenuController.instance.CanToggleMenu = false;

        cameraFollow.enabled = false;

        animator.Play("FadeIn");

        SetShopDiscounts();
        SetUpShop();

        InputManager.instance.SetSelectedObject(null);

        buyTypePanel.SetActive(true);
        buyTypeText.text = "cards";
    }

    public void CloseShop()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        playerMovement.enabled = true;

        shopKeeper.OpenedShopMenu = false;

        ResetPlayerCardsList();

        ReturnCardsBackToMenu();

        cameraFollow.enabled = true;

        DisableUpgradeButton();

        upgradePanel.SetActive(false);

        upgradeCardPreview.gameObject.SetActive(false);

        cardListTitle.SetActive(false);

        clearedCurrentCards = false;

        statusPanelAnimator.Play("Idle");

        animator.Play("FadeOut");

        SetInitialButtonNavigations();

        InputManager.instance.SetSelectedObject(null);
    }

    public void SetClosedMenu()
    {
        menuAnimationEvent = true;

        if(!canSetToggle)
        {
            InputManager.instance.SetSelectedObject(buyCardsButton.gameObject);

            canSetToggle = true;
        }
    }

    public void SetCanTogglePlayerMenu()
    {
        if(menuAnimationEvent)
        {
            MenuController.instance.CanToggleMenu = true;
            menuAnimationEvent = false;

            if(canSetToggle)
            {
                InputManager.instance.SetSelectedObject(null);

                canSetToggle = false;
            }
        }
    }

    public void ResetSelectedObject()
    {
        InputManager.instance.SetSelectedObject(null);
    }

    public void DisablePreviewCard()
    {
        statusPanelAnimator.Play("Idle");

        upgradeCardPreview.gameObject.SetActive(false);
    }

    public void PlayStickerPanel(bool show, Sticker sticker)
    {
        if(show)
        {
            stickerInfoPanelAnimator.Play("Show", -1, 0);

            stickerNameText.text = sticker._stickerInformation.stickerName;

            stickerPointsText.text = sticker._stickerInformation.stickerCost + " SP";

            if (sticker._stickerInformation.appliesStatusEffect)
            {
                stickerStatusDurationPanel.SetActive(true);

                stickerStatusDurationText.text = sticker._stickerInformation.statusDuration == 0 ? "Duration: Infinite" : "Duration: " + sticker._stickerInformation.statusDuration + " Turn(s)";

                stickerPanelInfoText.gameObject.SetActive(false);

                stickerPanelStatusEffectText.gameObject.SetActive(true);

                stickerPanelStatusEffectText.text = sticker._stickerInformation.stickerDescription;

                stickerInfoStatusDescription.text = sticker._stickerInformation.statusEffectDescription;

                statusEffectImage.sprite = sticker._stickerInformation.statusEffectSprite;
            }
            else
            {
                stickerStatusDurationPanel.SetActive(false);

                stickerPanelInfoText.gameObject.SetActive(true);

                stickerPanelStatusEffectText.gameObject.SetActive(false);

                stickerPanelInfoText.text = sticker._stickerInformation.stickerDescription;
            }
        }
        else
        {
            stickerInfoPanelAnimator.Play("Hide", -1, 0);
        }
    }

    public void SetUpShop()
    {
        BuyCardsMenu();
        BuyStickersMenu();

        moneyText.text = mainCharacterStats.money.ToString();
    }

    private void BuyCard(MenuCard card)
    {
        if(mainCharacterStats.money >= card.CardBuyValue)
        {
            if(MenuController.instance.StashCount < playerMenuInfo.stashLimit)
            {
                AudioManager.instance.PlaySoundEffect(AudioManager.instance.BuyItemAudio);

                mainCharacterStats.money -= card.CardBuyValue;

                moneyText.text = mainCharacterStats.money.ToString();

                buyParticleRectTransform.transform.position = card.transform.position;

                buyParticle.gameObject.SetActive(true);

                buyParticle.Play();

                var boughtCard = Instantiate(menuCardPrefab);

                boughtCard.gameObject.SetActive(true);

                boughtCard.NewCardText.SetActive(true);

                boughtCard._cardTemplate = card._cardTemplate;

                boughtCard.UpdateCardInformation();

                boughtCard.SetDefaultScrollView();

                boughtCard.IsInShopMenu = false;

                boughtCard.CardButton.onClick.RemoveAllListeners();

                cardTypeCategory.SetCardParent(boughtCard);

                MenuController.instance.UpdateStashCount();

                boughtCard.HideStatusPanel();

                RemoveCardFromShopList(card);

                FadeOutStatusPanel(card);

                card.transform.SetParent(shopKeeper.transform);

                Destroy(card.gameObject);

                MenuController.instance._CardCollection.CheckCurrentList(boughtCard._cardTemplate);

                AdjustBuyCardsNavigations();
                SetInitialButtonNavigations();

                InputManager.instance._EventSystem.SetSelectedGameObject(null);
                InputManager.instance.SetSelectedObject(cardListParentBuy.childCount > 0 ? cardListParentBuy.GetChild(0).gameObject : buyCardsSelectable.gameObject);
            }
            else
            {
                ErrorMessage("Stash is Full!", false);
            }
        }
        else
        {
            ErrorMessage("Not Enough Gold!", false);
        }
    }

    private void BuySticker(Sticker sticker)
    {
        if (mainCharacterStats.money >= sticker.StickerBuyValue)
        {
            AudioManager.instance.PlaySoundEffect(AudioManager.instance.BuyItemAudio);

            mainCharacterStats.money -= sticker.StickerBuyValue;

            moneyText.text = mainCharacterStats.money.ToString();

            buyParticleRectTransform.position = sticker.transform.position;

            buyParticle.gameObject.SetActive(true);

            buyParticle.Play();

            var boughtSticker = Instantiate(stickerPrefab);

            boughtSticker.gameObject.SetActive(true);

            boughtSticker._StickerPage.SetStickerCategory(boughtSticker);

            boughtSticker.NewStickerText.SetActive(true);

            boughtSticker.IsInShopMenu = false;

            boughtSticker._PropogateDrag.scrollView = MenuController.instance.StickerListScroll;

            boughtSticker._stickerInformation = sticker._stickerInformation;

            boughtSticker.SetUpStickerInformation();

            boughtSticker.StickerButton.onClick.RemoveAllListeners();

            CheckStickerList();

            RemoveStickerFromShopList(sticker._stickerInformation);

            sticker.transform.SetParent(shopKeeper.transform);

            Destroy(sticker.gameObject);

            AdjustBuyStickersNavigations();
            SetBuyStickersButtonNavigations();

            MenuController.instance.CheckStickerAchievement();

            InputManager.instance._EventSystem.SetSelectedGameObject(null);
            InputManager.instance.SetSelectedObject(stickerListParent.childCount > 0 ? stickerListParent.GetChild(0).gameObject : buyStickersSelectable.gameObject);
        }
        else
        {
            ErrorMessage("Not Enough Gold!", true);
        }
    }

    public void UpgradeCard()
    {
        if (mainCharacterStats.money >= currentlyViewedCard._cardTemplate.upgradeValue)
        {
            AudioManager.instance.PlaySoundEffect(AudioManager.instance.BuyItemAudio);

            cardUpgradeParticle.gameObject.SetActive(true);

            cardUpgradeParticle.Play();

            mainCharacterStats.money -= currentlyViewedCard._cardTemplate.upgradeValue;

            moneyText.text = mainCharacterStats.money.ToString();

            currentlyViewedCard._cardTemplate = currentlyViewedCard._cardTemplate.foilCard;

            currentlyViewedCard.UpdateCardInformation();

            DisableUpgradeButton();

            DisablePreviewCard();

            if(SteamManager.Initialized)
            {
                SteamUserStats.GetAchievement("ACH_MY_FIRST_FOIL", out bool achievementCompleted);

                if (!achievementCompleted)
                {
                    SteamUserStats.SetAchievement("ACH_MY_FIRST_FOIL");
                    SteamUserStats.StoreStats();
                }
            }

            if(currentlyViewedCard.InDeckList)
            {
                currentlyViewedCard.transform.SetParent(MenuController.instance.DeckListContent, false);

                currentlyViewedCard.CardPricePanel.SetActive(false);

                currentlyViewedCard.CardButton.onClick.RemoveAllListeners();

                currentlyViewedCard.IsInShopMenu = false;

                currentlyViewedCard._propogateDrag.scrollView = MenuController.instance._DeckMenu.DeckScrollRect;

                mainCharacterStats.currentCards.Add(currentlyViewedCard._cardTemplate);

                AdjustUpgradeDeckCardsNavigations();
                SetUpgradeCardsButtonNavigation(true);

                InputManager.instance.SetSelectedObject(cardListParentDeck.childCount > 0 ? cardListParentDeck.GetChild(0).gameObject : upgradeCardsSelectable.gameObject);
            }
            else if(currentlyViewedCard.InItemSlot)
            {
                currentlyViewedCard.CardPricePanel.SetActive(false);

                currentlyViewedCard.CardButton.onClick.RemoveAllListeners();

                currentlyViewedCard.IsInShopMenu = false;

                currentlyViewedCard._Animator.enabled = true;

                currentlyViewedCard.EquipItemCard();

                AdjustUpgradeCardsStashNavigations();
                SetUpgradeCardsButtonNavigation(false);

                InputManager.instance.SetSelectedObject(cardListParentStash.childCount > 0 ? cardListParentStash.GetChild(0).gameObject : upgradeCardsSelectable.gameObject);
            }
            else
            {
                cardTypeCategory.SetCardParent(currentlyViewedCard);

                currentlyViewedCard.CardPricePanel.SetActive(false);

                currentlyViewedCard.CardButton.onClick.RemoveAllListeners();

                currentlyViewedCard.IsInShopMenu = false;

                currentlyViewedCard._propogateDrag.scrollView = MenuController.instance._DeckMenu.StashScrollRect;

                AdjustUpgradeCardsStashNavigations();
                SetUpgradeCardsButtonNavigation(false);

                InputManager.instance.SetSelectedObject(cardListParentStash.childCount > 0 ? cardListParentStash.GetChild(0).gameObject : upgradeCardsSelectable.gameObject);
            }
        }
        else
        {
            ErrorMessage("Not Enough Gold!", true);
        }
    }

    public void SetInitialButtonNavigations()
    {
        Navigation buyCardsNav = buyCardsSelectable.navigation;
        Navigation upgradeCardsNav = upgradeCardsSelectable.navigation;
        Navigation buyStickersNav = buyStickersSelectable.navigation;

        buyCardsNav.selectOnRight = cardListParentBuy.childCount > 0 ? cardListParentBuy.GetChild(0).GetComponent<Selectable>() : buyStickersSelectable;
        upgradeCardsNav.selectOnRight = cardListParentBuy.childCount > 0 ? cardListParentBuy.GetChild(0).GetComponent<Selectable>() : buyStickersSelectable;
        buyStickersNav.selectOnLeft = cardListParentBuy.childCount > 0 ? cardListParentBuy.GetChild(cardListParentBuy.childCount - 1).GetComponent<Selectable>() : buyCardsSelectable;

        upgradeCardsNav.selectOnDown = null;

        buyCardsSelectable.navigation = buyCardsNav;
        upgradeCardsSelectable.navigation = upgradeCardsNav;
        buyStickersSelectable.navigation = buyStickersNav;

        AdjustBuyCardsNavigations();
    }

    public void SetBuyStickersButtonNavigations()
    {
        Navigation buyCardsNav = buyCardsSelectable.navigation;
        Navigation upgradeCardsNav = upgradeCardsSelectable.navigation;
        Navigation buyStickersNav = buyStickersSelectable.navigation;

        buyCardsNav.selectOnRight = stickerListParent.childCount > 0 ? stickerListParent.GetChild(0).GetComponent<Selectable>() : buyStickersSelectable;
        upgradeCardsNav.selectOnRight = stickerListParent.childCount > 0 ? stickerListParent.GetChild(0).GetComponent<Selectable>() : buyStickersSelectable;
        buyStickersNav.selectOnLeft = stickerListParent.childCount > 0 ? stickerListParent.GetChild(stickerListParent.childCount - 1).GetComponent<Selectable>() : buyCardsSelectable;

        upgradeCardsNav.selectOnDown = null;

        buyCardsSelectable.navigation = buyCardsNav;
        upgradeCardsSelectable.navigation = upgradeCardsNav;
        buyStickersSelectable.navigation = buyStickersNav;

        AdjustBuyStickersNavigations();
    }

    public void SetUpgradeCardsButtonNavigation(bool deckList)
    {
        Navigation buyCardsNav = buyCardsSelectable.navigation;
        Navigation upgradeCardsNav = upgradeCardsSelectable.navigation;
        Navigation buyStickersNav = buyStickersSelectable.navigation;
        Navigation upgradeFromDeckNav = upgradeFromDeckSelectable.navigation;
        Navigation upgradeFromStashNav = upgradeFromStashSelecable.navigation;

        if(deckList)
        {
            buyCardsNav.selectOnRight = cardListParentDeck.childCount > 0 ? cardListParentDeck.GetChild(0).GetComponent<Selectable>() : buyStickersSelectable;
            upgradeCardsNav.selectOnRight = cardListParentDeck.childCount > 0 ? cardListParentDeck.GetChild(0).GetComponent<Selectable>() : buyStickersSelectable;
            buyStickersNav.selectOnLeft = cardListParentDeck.childCount > 0 ? cardListParentDeck.GetChild(cardListParentDeck.childCount - 1).GetComponent<Selectable>() : buyCardsSelectable;
            upgradeFromDeckNav.selectOnRight = cardListParentDeck.childCount > 0 ? cardListParentDeck.GetChild(0).GetComponent<Selectable>() : buyCardsSelectable;
            upgradeFromStashNav.selectOnRight = cardListParentDeck.childCount > 0 ? cardListParentDeck.GetChild(0).GetComponent<Selectable>() : buyCardsSelectable;

            AdjustUpgradeDeckCardsNavigations();
        }
        else
        {
            buyCardsNav.selectOnRight = cardListParentStash.childCount > 0 ? cardListParentStash.GetChild(0).GetComponent<Selectable>() : buyStickersSelectable;
            upgradeCardsNav.selectOnRight = cardListParentStash.childCount > 0 ? cardListParentStash.GetChild(0).GetComponent<Selectable>() : buyStickersSelectable;
            buyStickersNav.selectOnLeft = cardListParentStash.childCount > 0 ? cardListParentStash.GetChild(cardListParentStash.childCount - 1).GetComponent<Selectable>() : buyCardsSelectable;
            upgradeFromDeckNav.selectOnRight = cardListParentStash.childCount > 0 ? cardListParentStash.GetChild(0).GetComponent<Selectable>() : buyCardsSelectable;
            upgradeFromStashNav.selectOnRight = cardListParentStash.childCount > 0 ? cardListParentStash.GetChild(0).GetComponent<Selectable>() : buyCardsSelectable;

            AdjustUpgradeCardsStashNavigations();
        }

        upgradeCardsNav.selectOnDown = upgradeFromDeckSelectable;

        buyCardsSelectable.navigation = buyCardsNav;
        upgradeCardsSelectable.navigation = upgradeCardsNav;
        buyStickersSelectable.navigation = buyStickersNav;
        upgradeFromDeckSelectable.navigation = upgradeFromDeckNav;
        upgradeFromStashSelecable.navigation = upgradeFromStashNav;
    }

    private void AdjustBuyCardsNavigations()
    {
        if(cardListParentBuy.childCount > 0)
        {
            for(int i = 0; i < cardListParentBuy.childCount; i++)
            {
                Navigation nav = cardListParentBuy.GetChild(i).GetComponent<Selectable>().navigation;

                if (i >= cardListParentBuy.childCount - 1)
                {
                    nav.selectOnRight = buyStickersSelectable;

                    if (cardListParentBuy.childCount == 1)
                    {
                        nav.selectOnLeft = buyCardsSelectable;
                    }
                    else
                    {
                        nav.selectOnLeft = cardListParentBuy.GetChild(i - 1).GetComponent<Selectable>();
                    }
                }
                else
                {
                    nav.selectOnRight = cardListParentBuy.GetChild(i + 1).GetComponent<Selectable>();

                    int farRightSide = i + 1;

                    if (i % 5 == 0)
                    {
                        nav.selectOnLeft = buyCardsSelectable;
                    }
                    else if (farRightSide % 5 == 0)
                    {
                        nav.selectOnRight = buyStickersSelectable;
                        nav.selectOnLeft = cardListParentBuy.GetChild(i - 1).GetComponent<Selectable>();
                    }
                    else
                    {
                        nav.selectOnLeft = cardListParentBuy.GetChild(i - 1).GetComponent<Selectable>();
                    }
                }

                if (cardListParentBuy.childCount > 4)
                {
                    int downWardCardIndex = i + 5;
                    nav.selectOnDown = cardListParentBuy.GetChild(downWardCardIndex >= cardListParentBuy.childCount - 1 ? cardListParentBuy.childCount - 1 : downWardCardIndex).GetComponent<Selectable>();
                }

                if (i > 4)
                {
                    int upWardCardIndex = i - 5;
                    nav.selectOnUp = cardListParentBuy.GetChild(upWardCardIndex).GetComponent<Selectable>();
                }

                cardListParentBuy.GetChild(i).GetComponent<Selectable>().navigation = nav;
            }
        }
    }

    private void AdjustBuyStickersNavigations()
    {
        if (stickerListParent.childCount > 0)
        {
            for (int i = 0; i < stickerListParent.childCount; i++)
            {
                Navigation nav = stickerListParent.GetChild(i).GetComponent<Selectable>().navigation;

                if (i >= stickerListParent.childCount - 1)
                {
                    nav.selectOnRight = buyStickersSelectable;

                    if(stickerListParent.childCount == 1)
                    {
                        nav.selectOnLeft = buyCardsSelectable;
                    }
                    else
                    {
                        nav.selectOnLeft = stickerListParent.GetChild(i - 1).GetComponent<Selectable>();
                    }
                }
                else
                {
                    nav.selectOnRight = stickerListParent.GetChild(i + 1).GetComponent<Selectable>();

                    int farRightSide = i + 1;

                    if (i % 5 == 0)
                    {
                        nav.selectOnLeft = buyCardsSelectable;
                    }
                    else if (farRightSide % 5 == 0)
                    {
                        nav.selectOnRight = buyStickersSelectable;
                        nav.selectOnLeft = stickerListParent.GetChild(i - 1).GetComponent<Selectable>();
                    }
                    else
                    {
                        nav.selectOnLeft = stickerListParent.GetChild(i - 1).GetComponent<Selectable>();
                    }
                }

                if (stickerListParent.childCount > 4)
                {
                    int downWardCardIndex = i + 5;
                    nav.selectOnDown = stickerListParent.GetChild(downWardCardIndex >= stickerListParent.childCount - 1 ? stickerListParent.childCount - 1 : downWardCardIndex).GetComponent<Selectable>();
                }

                if (i > 4)
                {
                    int upWardCardIndex = i - 5;
                    nav.selectOnUp = stickerListParent.GetChild(upWardCardIndex).GetComponent<Selectable>();
                }

                stickerListParent.GetChild(i).GetComponent<Selectable>().navigation = nav;
            }
        }
    }

    private void AdjustUpgradeDeckCardsNavigations()
    {
        if (cardListParentDeck.childCount > 0)
        {
            for (int i = 0; i < cardListParentDeck.childCount; i++)
            {
                Navigation nav = cardListParentDeck.GetChild(i).GetComponent<Selectable>().navigation;

                if (i >= cardListParentDeck.childCount - 1)
                {
                    nav.selectOnRight = buyStickersSelectable;

                    if(cardListParentDeck.childCount == 1)
                    {
                        nav.selectOnLeft = buyCardsSelectable;
                    }
                    else
                    {
                        nav.selectOnLeft = cardListParentDeck.GetChild(i - 1).GetComponent<Selectable>();
                    }
                }
                else
                {
                    nav.selectOnRight = cardListParentDeck.GetChild(i + 1).GetComponent<Selectable>();

                    int farRightSide = i + 1;

                    if (i % 5 == 0)
                    {
                        nav.selectOnLeft = buyCardsSelectable;
                    }
                    else if (farRightSide % 5 == 0)
                    {
                        nav.selectOnRight = buyStickersSelectable;
                        nav.selectOnLeft = cardListParentDeck.GetChild(i - 1).GetComponent<Selectable>();
                    }
                    else
                    {
                        nav.selectOnLeft = cardListParentDeck.GetChild(i - 1).GetComponent<Selectable>();
                    }
                }

                if (cardListParentDeck.childCount > 4)
                {
                    int downWardCardIndex = i + 5;
                    nav.selectOnDown = cardListParentDeck.GetChild(downWardCardIndex >= cardListParentDeck.childCount - 1 ? cardListParentDeck.childCount - 1 : downWardCardIndex).GetComponent<Selectable>();
                }

                if(i <= 4)
                {
                    nav.selectOnUp = null;
                }

                if (i > 4)
                {
                    int upWardCardIndex = i - 5;
                    nav.selectOnUp = cardListParentDeck.GetChild(upWardCardIndex).GetComponent<Selectable>();
                }

                cardListParentDeck.GetChild(i).GetComponent<Selectable>().navigation = nav;
            }
        }
    }

    private void AdjustUpgradeCardsStashNavigations()
    {
        if (cardListParentStash.childCount > 0)
        {
            for (int i = 0; i < cardListParentStash.childCount; i++)
            {
                Navigation nav = cardListParentStash.GetChild(i).GetComponent<Selectable>().navigation;

                if (i >= cardListParentStash.childCount - 1)
                {
                    nav.selectOnRight = buyStickersSelectable;

                    if(cardListParentStash.childCount == 1)
                    {
                        nav.selectOnLeft = buyCardsSelectable;
                    }
                    else
                    {
                        nav.selectOnLeft = cardListParentStash.GetChild(i - 1).GetComponent<Selectable>();
                    }
                }
                else
                {
                    nav.selectOnRight = cardListParentStash.GetChild(i + 1).GetComponent<Selectable>();

                    int farRightSide = i + 1;

                    if (i % 5 == 0)
                    {
                        nav.selectOnLeft = buyCardsSelectable;
                    }
                    else if (farRightSide % 5 == 0)
                    {
                        nav.selectOnRight = buyStickersSelectable;
                        nav.selectOnLeft = cardListParentStash.GetChild(i - 1).GetComponent<Selectable>();
                    }
                    else
                    {
                        nav.selectOnLeft = cardListParentStash.GetChild(i - 1).GetComponent<Selectable>();
                    }
                }

                if (cardListParentStash.childCount > 4)
                {
                    int downWardCardIndex = i + 5;
                    nav.selectOnDown = cardListParentStash.GetChild(downWardCardIndex >= cardListParentStash.childCount - 1 ? cardListParentStash.childCount - 1 : downWardCardIndex).GetComponent<Selectable>();
                }

                if (i <= 4)
                {
                    nav.selectOnUp = null;
                }

                if (i > 4)
                {
                    int upWardCardIndex = i - 5;
                    nav.selectOnUp = cardListParentStash.GetChild(upWardCardIndex).GetComponent<Selectable>();
                }

                cardListParentStash.GetChild(i).GetComponent<Selectable>().navigation = nav;
            }
        }
    }

    private void SetUpUpgradeNavigations()
    {
        Navigation upgradeCardButtonNav = upgradeCardButton.GetComponent<Selectable>().navigation;

        if(upgradeCardsListDeck.activeSelf)
        {
            upgradeCardButtonNav.selectOnDown = cardListParentDeck.GetChild(0).GetComponent<Selectable>();

            if(cardListParentDeck.childCount > 0)
            {
                for(int i = 0; i < cardListParentDeck.childCount; i++)
                {
                    Navigation deckListNav = cardListParentDeck.GetChild(i).GetComponent<Selectable>().navigation;

                    if(i <= 4)
                    {
                        deckListNav.selectOnUp = upgradeCardButton.GetComponent<Selectable>();
                    }

                    cardListParentDeck.GetChild(i).GetComponent<Selectable>().navigation = deckListNav;
                }
            }
        }
        else if(upgradeCardsListStash.activeSelf)
        {
            upgradeCardButtonNav.selectOnDown = cardListParentStash.GetChild(0).GetComponent<Selectable>();

            if (cardListParentStash.childCount > 0)
            {
                for (int i = 0; i < cardListParentStash.childCount; i++)
                {
                    Navigation deckListNav = cardListParentStash.GetChild(i).GetComponent<Selectable>().navigation;

                    if (i <= 4)
                    {
                        deckListNav.selectOnUp = upgradeCardButton.GetComponent<Selectable>();
                    }

                    cardListParentStash.GetChild(i).GetComponent<Selectable>().navigation = deckListNav;
                }
            }
        }

        upgradeCardButton.GetComponent<Selectable>().navigation = upgradeCardButtonNav;
    }

    public void BuyCardsMenu()
    {
        buyCardsList.SetActive(true);

        upgradeCardsListDeck.SetActive(false);
        upgradeCardsListStash.SetActive(false);
        stickerList.gameObject.SetActive(false);

        DisableUpgradeButton();

        DisablePreviewCard();

        upgradePanel.SetActive(false);

        cardListTitleFrameText.text = "Buy";

        deckListButton.SetActive(false);
        stashListButton.SetActive(false);

        if (!initialBuyCardsSetUp)
        {
            for (int i = 0; i < cardTemplate.Count; i++)
            {
                var card = Instantiate(cardToBuyPrefab, cardListParentBuy);

                card.gameObject.SetActive(true);

                card.IsInShopMenu = true;

                card._cardTemplate = cardTemplate[i];

                card.UpdateCardInformation();

                card.CardPricePanel.SetActive(true);

                card.SetCardPrice();

                card._propogateDrag.scrollView = buyCardsScroll;

                card.CardButton.onClick.AddListener(() => BuyCard(card));

                CheckShopDiscounts(null, card);
            }
            initialBuyCardsSetUp = true;

            SetInitialButtonNavigations();
        }
    }

    public void UpgradeCardsMenu()
    {
        upgradeCardsListDeck.SetActive(true);
        upgradeCardsListStash.SetActive(false);
        stickerList.gameObject.SetActive(false);

        buyCardsList.SetActive(false);

        upgradePanel.SetActive(true);

        cardListTitleFrameText.text = "Upgrade";

        deckListButton.SetActive(true);
        stashListButton.SetActive(true);

        foreach(MenuCard card in MenuController.instance.DeckListContent.GetComponentsInChildren<MenuCard>(false))
        {
            if (!card._cardTemplate.isAFoil && card._cardTemplate.cardType != CardType.Mystic)
            {
                card.transform.SetParent(cardListParentDeck, false);

                card.IsInShopMenu = true;

                card._propogateDrag.scrollView = upgradeCardsScrollDeck;

                card.CardPricePanel.SetActive(true);

                card.CardPriceText.text = card._cardTemplate.upgradeValue.ToString();

                card.CardButton.onClick.AddListener(() => ViewCard(card._cardTemplate.foilCard, card));

                SetUpgradeCardDiscount(card);
            }
        }

        foreach (MenuCard card in MenuController.instance.StashContent.GetComponentsInChildren<MenuCard>(true))
        {
            if (!card._cardTemplate.isAFoil && card._cardTemplate.cardType != CardType.Mystic)
            {
                card.transform.SetParent(cardListParentStash, false);

                card.IsInShopMenu = true;

                card._propogateDrag.scrollView = upgradeCardsScrollStash;

                card.CardPricePanel.SetActive(true);

                card.CardPriceText.text = card._cardTemplate.upgradeValue.ToString();

                card.CardButton.onClick.AddListener(() => ViewCard(card._cardTemplate.foilCard, card));

                SetUpgradeCardDiscount(card);
            }
        }

        foreach (MenuCard card in MenuController.instance.ItemList.GetComponentsInChildren<MenuCard>(true))
        {
            if (!card._cardTemplate.isAFoil)
            {
                card.transform.SetParent(cardListParentStash, false);

                card._Animator.enabled = false;

                card.IsInShopMenu = true;

                card.transform.localScale = new Vector3(0.8104491f, 0.8104491f, 1);

                card._propogateDrag.scrollView = upgradeCardsScrollStash;

                card.CardPricePanel.SetActive(true);

                card.CardPriceText.text = card._cardTemplate.upgradeValue.ToString();

                card.CardButton.onClick.AddListener(() => ViewCard(card._cardTemplate.foilCard, card));

                SetUpgradeCardDiscount(card);
            }
        }
    }

    private void BuyStickersMenu()
    {
        if (!initialBuyStickersSetUp)
        {
            for (int i = 0; i < stickerInformation.Count; i++)
            {
                var sticker = Instantiate(stickerPrefabToBuy, stickerListParent);

                sticker.gameObject.SetActive(true);

                sticker.IsInShopMenu = true;

                sticker._stickerInformation = stickerInformation[i];

                sticker.StickerPricePanel.SetActive(true);

                sticker._PropogateDrag.scrollView = stickerListScroll;

                sticker.SetUpStickerInformation();

                sticker.SetStickerBuyValue();

                sticker.StickerPriceText.text = sticker._stickerInformation.buyValue.ToString();

                sticker.StickerButton.onClick.AddListener(() => BuySticker(sticker));

                CheckShopDiscounts(sticker, null);
            }
            initialBuyStickersSetUp = true;
        }
    }

    private void SetShopDiscounts()
    {
        if (initialBuyCardsSetUp)
        {
            if (cardListParentBuy.childCount <= 0) return;

            foreach (MenuCard menuCrd in cardListParentBuy.GetComponentsInChildren<MenuCard>(true))
            {
                if(MenuController.instance._StickerPowerManager.CheckFieldStickerPower(StickerPower.Bargainer))
                {
                    if (menuCrd.CardBuyValue >= menuCrd._cardTemplate.buyValue)
                        CheckShopDiscounts(null, menuCrd);
                }
                else if(!MenuController.instance._StickerPowerManager.CheckFieldStickerPower(StickerPower.Bargainer))
                {
                    if (menuCrd.CardBuyValue < menuCrd._cardTemplate.buyValue)
                        ResetShopPrices(null, menuCrd);
                }
            }
        }

        if (initialBuyStickersSetUp)
        {
            if (stickerListParent.childCount <= 0) return;

            foreach (Sticker s in stickerListParent.GetComponentsInChildren<Sticker>(true))
            {
                if(MenuController.instance._StickerPowerManager.CheckFieldStickerPower(StickerPower.Bargainer))
                {
                    if (s.StickerBuyValue >= s._stickerInformation.buyValue)
                        CheckShopDiscounts(s, null);
                }
                else if(!MenuController.instance._StickerPowerManager.CheckFieldStickerPower(StickerPower.Bargainer))
                {
                    if (s.StickerBuyValue < s._stickerInformation.buyValue)
                        ResetShopPrices(s, null);
                }
            }
        }
    }

    private void SetUpgradeCardDiscount(MenuCard upgradeCard)
    {
        if(MenuController.instance._StickerPowerManager.CheckFieldStickerPower(StickerPower.Bargainer))
        {
            if(upgradeCard.CardUpgradeValue >= upgradeCard._cardTemplate.upgradeValue)
            {
                int discountCost = Mathf.RoundToInt(upgradeCard.CardUpgradeValue * 0.25f);

                upgradeCard.CardUpgradeValue -= discountCost;

                if(upgradeCard.CardUpgradeValue <= 0)
                {
                    upgradeCard.CardUpgradeValue = 0;
                }

                upgradeCard.CardPriceText.text = upgradeCard.CardUpgradeValue.ToString();

                upgradeCard.CardPriceText.color = Color.green;
            }
        }
        else if(!MenuController.instance._StickerPowerManager.CheckFieldStickerPower(StickerPower.Bargainer))
        {
            if (upgradeCard.CardUpgradeValue < upgradeCard._cardTemplate.upgradeValue)
            {
                upgradeCard.CardUpgradeValue = upgradeCard._cardTemplate.upgradeValue;

                upgradeCard.CardPriceText.text = upgradeCard.CardUpgradeValue.ToString();

                upgradeCard.CardPriceText.color = Color.white;
            }
        }
    }

    private void CheckShopDiscounts(Sticker shopSticker, MenuCard shopCard)
    {
        if (MenuController.instance._StickerPowerManager.CheckFieldStickerPower(StickerPower.Bargainer))
        {
            if (shopSticker != null)
            {
                int discountAmount = 0;

                discountAmount = Mathf.RoundToInt(shopSticker._stickerInformation.buyValue * 0.25f);

                shopSticker.StickerBuyValue -= discountAmount;

                if (shopSticker.StickerBuyValue <= 0)
                {
                    shopSticker.StickerBuyValue = 0;
                }

                shopSticker.StickerPriceText.text = shopSticker.StickerBuyValue.ToString();

                shopSticker.StickerPriceText.color = Color.green;
            }

            if (shopCard != null)
            {
                int discountAmount = 0;

                discountAmount = Mathf.RoundToInt(shopCard._cardTemplate.buyValue * 0.25f);

                shopCard.CardBuyValue -= discountAmount;

                if (shopCard.CardBuyValue <= 0)
                {
                    shopCard.CardBuyValue = 0;
                }

                shopCard.CardPriceText.text = shopCard.CardBuyValue.ToString();

                shopCard.CardPriceText.color = Color.green;
            }
        }
    }

    private void ResetShopPrices(Sticker shopSticker, MenuCard shopCard)
    {
        if(shopSticker != null)
        {
            shopSticker.StickerBuyValue = shopSticker._stickerInformation.buyValue;

            shopSticker.StickerPriceText.text = shopSticker.StickerBuyValue.ToString();

            shopSticker.StickerPriceText.color = Color.white;
        }

        if(shopCard != null)
        {
            shopCard.CardBuyValue = shopCard._cardTemplate.buyValue;

            shopCard.CardPriceText.text = shopCard.CardBuyValue.ToString();

            shopCard.CardPriceText.color = Color.white;
        }
    }

    public void ReturnCardsBackToMenu()
    {
        foreach(MenuCard menuCard in cardListParentDeck.GetComponentsInChildren<MenuCard>())
        {
            menuCard.CardButton.onClick.RemoveAllListeners();

            menuCard.transform.SetParent(MenuController.instance.DeckListContent, false);

            menuCard._propogateDrag.scrollView = MenuController.instance.DeckListScroll;

            menuCard.CardPricePanel.SetActive(false);

            menuCard.IsInShopMenu = false;
        }

        foreach (MenuCard menuCard in cardListParentStash.GetComponentsInChildren<MenuCard>())
        {
            menuCard.CardButton.onClick.RemoveAllListeners();

            if (menuCard._cardTemplate.cardType == CardType.Item && menuCard.InItemSlot)
            {
                menuCard._Animator.enabled = true;

                menuCard.EquipItemCard();
            }
            else
            {
                cardTypeCategory.SetCardParent(menuCard);
            }

            menuCard.SetDefaultScrollView();

            menuCard.CardPricePanel.SetActive(false);

            menuCard.IsInShopMenu = false;
        }

        MenuController.instance.ReOrganizeCardIndex();
    }

    private void RemoveCardFromShopList(MenuCard card)
    {
        Scene scene = SceneManager.GetActiveScene();

        switch(scene.name)
        {
            case ("ForestTown"):
                shopInformation.cardShopForest.Remove(card._cardTemplate);
                break;
            case ("DesertTown"):
                shopInformation.cardShopDesert.Remove(card._cardTemplate);
                break;
            case ("ArcticTown"):
                shopInformation.cardShopArctic.Remove(card._cardTemplate);
                break;
            case ("GraveyardTown"):
                shopInformation.cardShopGraveyard.Remove(card._cardTemplate);
                break;
            case ("CastleTown"):
                shopInformation.cardShopCastle.Remove(card._cardTemplate);
                break;
        }
    }

    private void RemoveStickerFromShopList(StickerInformation stickerInformation)
    {
        Scene scene = SceneManager.GetActiveScene();

        switch (scene.name)
        {
            case ("ForestTown"):
                shopInformation.stickerShopForest.Remove(stickerInformation);
                break;
            case ("DesertTown"):
                shopInformation.stickerShopDesert.Remove(stickerInformation);
                break;
            case ("ArcticTown"):
                shopInformation.stickerShopArctic.Remove(stickerInformation);
                break;
            case ("GraveyardTown"):
                shopInformation.stickerShopGraveyard.Remove(stickerInformation);
                break;
            case ("CastleTown"):
                shopInformation.stickerShopCastle.Remove(stickerInformation);
                break;
        }
    }

    public void ClearPlayerCardsList()
    {
        if(!clearedCurrentCards)
        {
            for(int i = 0; i < mainCharacterStats.currentCards.Count; i++)
            {
                if(mainCharacterStats.currentCards[i].cardType != CardType.Mystic && !mainCharacterStats.currentCards[i].isAFoil)
                {
                    mainCharacterStats.currentCards.RemoveAt(i);

                    i--;
                }
            }

            if(playerMenuInfo.currentEquippedItems.Count > 0)
            {
                for (int i = 0; i < playerMenuInfo.currentEquippedItems.Count; i++)
                {
                    if (!playerMenuInfo.currentEquippedItems[i].isAFoil)
                    {
                        playerMenuInfo.currentEquippedItems.RemoveAt(i);

                        playerMenuInfo.equippedItems--;

                        i--;
                    }
                }
            }

            clearedCurrentCards = true;
        }
    }

    private void ResetPlayerCardsList()
    {
        if(clearedCurrentCards)
        {
            foreach (MenuCard menuCard in cardListParentDeck.GetComponentsInChildren<MenuCard>())
            {
                mainCharacterStats.currentCards.Add(menuCard._cardTemplate);
            }
        }
    }

    public void ViewStatusEffectPanel(MenuCard menuCard)
    {
        if(menuCard._cardTemplate.cardStatus != CardStatus.NONE)
        {
            buyCardStatusPanelAnimator.Play("FadeIn", -1, 0);

            buyCardStatusPanelNameText.text = menuCard._cardTemplate.statusEffectName;

            buyCardStatusPanelInfoText.text = menuCard._cardTemplate.statusBoostPercentage > 0 ? menuCard._cardTemplate.cardInformation + " " + menuCard._cardTemplate.statusBoostPercentage + "%." : 
                                              menuCard._cardTemplate.cardInformation;

            buyCardStatusDurationText.text = "Duration: " + menuCard._cardTemplate.statusEffectTime.ToString() + " Turn(s)" + ", Success: " + menuCard._cardTemplate.statusEffectChance + "%";
        }
        else
        {
            if(buyCardStatusPanelAnimator.GetCurrentAnimatorClipInfo(0).Length < 0)
                buyCardStatusPanelAnimator.Play("FadeOut", -1, 0);
        }
    }

    public void FadeOutStatusPanel(MenuCard menuCard)
    {
        if (menuCard._cardTemplate.cardStatus != CardStatus.NONE)
        {
            buyCardStatusPanelAnimator.Play("FadeOut", -1, 0);
        }
    }

    private void ViewCard(CardTemplate cardTemplate, MenuCard currentCard)
    {
        currentlyViewedCard = currentCard;

        upgradeCardPreview.gameObject.SetActive(true);

        upgradeCardPreview.NewCardText.SetActive(false);

        AudioManager.instance.PlaySoundEffect(AudioManager.instance.SelectedAudio);

        upgradeCardButton.GetComponent<CanvasGroup>().interactable = true;
        upgradeCardButton.GetComponent<CanvasGroup>().alpha = 1;
        upgradeCardButton.GetComponent<CanvasGroup>().blocksRaycasts = true;

        upgradeCardPreview._cardTemplate = cardTemplate;

        CheckNewStatUpgrades();

        if (upgradeCardPreview._cardTemplate.cardStatus != CardStatus.NONE)
        {
            statusPanelAnimator.Play("SlideOut");

            statusPanelNameText.text = upgradeCardPreview._cardTemplate.statusEffectName;

            statusPanelInfoText.text = upgradeCardPreview._cardTemplate.statusBoostPercentage > 0 ? upgradeCardPreview._cardTemplate.cardInformation + " " + 
                                       upgradeCardPreview._cardTemplate.statusBoostPercentage + "%." : upgradeCardPreview._cardTemplate.cardInformation;

            upgradeCardStatusDurationText.text = "Duration: " + upgradeCardPreview._cardTemplate.statusEffectTime.ToString() + " Turn(s)" + ", Success: " + upgradeCardPreview._cardTemplate.statusEffectChance + "%";

            if(upgradeCardPreview._cardTemplate.cardStatus != currentCard._cardTemplate.cardStatus)
            {
                newStatusEffectBanner.SetActive(true);
            }
            else
            {
                if (upgradeCardPreview._cardTemplate.statusBoostPercentage != currentCard._cardTemplate.statusBoostPercentage)
                {
                    statusPanelInfoText.text = upgradeCardPreview._cardTemplate.statusBoostPercentage > 0 ? upgradeCardPreview._cardTemplate.cardInformation + " " +
                                               "<color=green>" + upgradeCardPreview._cardTemplate.statusBoostPercentage + "</color>%." : upgradeCardPreview._cardTemplate.cardInformation;
                }
                else
                {
                    statusPanelInfoText.text = upgradeCardPreview._cardTemplate.statusBoostPercentage > 0 ? upgradeCardPreview._cardTemplate.cardInformation + " " +
                                               upgradeCardPreview._cardTemplate.statusBoostPercentage + "%." : upgradeCardPreview._cardTemplate.cardInformation;
                }
                newStatusEffectBanner.SetActive(false);
            }
        }
        else
        {
            statusPanelAnimator.Play("Idle");
        }

        SetUpUpgradeNavigations();

        upgradeCardPreview.UpdateCardInformation();

        if (currentlyViewedCard._cardTemplate.target != upgradeCardPreview._cardTemplate.target)
        {
            upgradeCardPreview.CardInformation.text = "Target: <color=green>" + upgradeCardPreview._cardTemplate.target + "</color>" + TypeOfCard(upgradeCardPreview._cardTemplate);
        }
    }

    private string TypeOfCard(CardTemplate template)
    {
        string type = "";

        switch (template.cardType)
        {
            case (CardType.Action):
                type = "\nType: <color=red>Power</color>";
                break;
            case (CardType.Magic):
                type = "\nType: <color=blue>Magic</color>";
                break;
            case (CardType.Support):
                type = "\nType: <color=green>Support</color>";
                break;
            case (CardType.Item):
                type = "\nType: <color=#D1D1D1>Item</color>";
                break;
            case (CardType.Mystic):
                type = "\nType: <color=#A433D2>Mystic</color>";
                break;
        }

        return type;
    }

    private void CheckNewStatUpgrades()
    {
        if(currentlyViewedCard._cardTemplate.cardPointCost != upgradeCardPreview._cardTemplate.cardPointCost)
        {
            upgradeCardPreview.CardCostText.color = Color.green;
        }
        else
        {
            upgradeCardPreview.CardCostText.color = Color.white;
        }

        if(currentlyViewedCard._cardTemplate.strength != upgradeCardPreview._cardTemplate.strength)
        {
            if (upgradeCardPreview.DamageValueText.gameObject.activeSelf)
            {
                upgradeCardPreview.DamageValueText.color = Color.green;
            }
            if (upgradeCardPreview.HealValueText.gameObject.activeSelf)
            {
                upgradeCardPreview.HealValueText.color = Color.green;
            }
        }
        else
        {
            if (upgradeCardPreview.DamageValueText.gameObject.activeSelf)
            {
                upgradeCardPreview.DamageValueText.color = Color.white;
            }
            else if (upgradeCardPreview.HealValueText.gameObject.activeSelf)
            {
                upgradeCardPreview.HealValueText.color = Color.white;
            }
        }
    }

    private void CheckStickerList()
    {
        if(stickerListParent.childCount <= 1)
        {
            stickerInfoPanelAnimator.Play("Hide", -1, 0);
        }
    }

    private void ErrorMessage(string message, bool buyCardsMenu)
    {
        if(!buyCardsMenu)
        {
            errorMessageText.text = message;

            errorMessageAnimator.Play("Message", -1, 0);
        }
        else
        {
            errorMessageTexttUpgrade.text = message;

            errorMessageAnimatorUpgrade.Play("Message", -1, 0);
        }
        
        AudioManager.instance.PlaySoundEffect(AudioManager.instance.ErrorMessageAudio);
    }

    public void DisableUpgradeButton()
    {
        CanvasGroup canvasGroup = upgradeCardButton.GetComponent<CanvasGroup>();

        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}