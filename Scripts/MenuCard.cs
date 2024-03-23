using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Coffee.UIEffects;
using Steamworks;
using Coffee.UIExtensions;

public class MenuCard : MonoBehaviour
{
    [SerializeField]
    private PlayerMenuInfo playerMenuInfo;

    [SerializeField]
    private MainCharacterStats mainCharacterStats;

    [SerializeField]
    private Deck deck;

    [SerializeField]
    private CardTemplate cardTemplate;

    [SerializeField]
    private UIShiny uiShiny;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private PropogateDrag propogateDrag;

    [SerializeField]
    private ParticleSystem cardSparkleParticle;

    [SerializeField]
    private Animator animator, errorMessageAnimator;

    private Animator spoilsInfoAnimator = null;

    [SerializeField]
    private Image cardTypeImage, cardImage;

    [SerializeField]
    private Sprite actionSprite, magicSprite, supportSprite, mysticSprite, itemSprite;

    [SerializeField]
    private GameObject cardCostObject, damageObject, healObject, maskImage, cardObject, cardPortrait, statusEffectPanelDecklist, statusEffectPanelStash, cardParticleObject, forbiddenIcon, foilBanner, cardPricePanel,
                       newCardText, statusEffectPanelItem, statusEffectPanelSettings;

    [SerializeField]
    private TextMeshProUGUI cardInformation, cardNameText, cardCostText, damageValueText, healValueText, errorMessageText, statusEffectNameDecklist, statusEffectDescriptionDecklist, statusEffectNameStash, statusEffectDescriptionStash,
                            statusEffectDurationDeckList, statusEffectDurationStash, cardPrice, statusEffectDurationItem, statusEffectDescriptionItem, statusEffectNameItem, statusEffectNameSettings,
                            statusEffectDescriptionSettings, statusEffectDurationSettings;

    [SerializeField]
    private RectTransform rectTransform;

    [SerializeField]
    private Transform deckListParent;

    [SerializeField]
    private Transform[] itemsTransform;

    [SerializeField]
    private Button cardButton;

    [SerializeField]
    private bool isInBattle, isInSettingsMenu;

    private bool hoveringOver, inDeckList, inItemSlot, hasStatusEffect, isInShopMenu;

    [SerializeField]
    private int cardIndex;

    [SerializeField]
    private int cardBuyValue, cardUpgradeValue;

    public TextMeshProUGUI CardPriceText => cardPrice;

    public Animator _Animator => animator;

    public Animator SpoilsInfoAnimator
    {
        get
        {
            return spoilsInfoAnimator;
        }
        set
        {
            spoilsInfoAnimator = value;
        }
    }

    public CardTemplate _cardTemplate
    {
        get
        {
            return cardTemplate;
        }
        set
        {
            cardTemplate = value;
        }
    }

    public PropogateDrag _propogateDrag
    {
        get
        {
            return propogateDrag;
        }
        set
        {
            propogateDrag = value;
        }
    }

    public Transform DeckListParent
    {
        get
        {
            return deckListParent;
        }
        set
        {
            deckListParent = value;
        }
    }

    public GameObject ForbiddenIcon => forbiddenIcon;

    public GameObject CardPricePanel => cardPricePanel;

    public GameObject NewCardText => newCardText;

    public Button CardButton => cardButton;

    public TextMeshProUGUI CardInformation => cardInformation;

    public TextMeshProUGUI CardCostText => cardCostText;

    public TextMeshProUGUI DamageValueText => damageValueText;

    public TextMeshProUGUI HealValueText => healValueText;

    public bool HoveringOver
    {
        get
        {
            return hoveringOver;
        }
        set
        {
            hoveringOver = value;
        }
    }

    public bool InDeckList
    {
        get
        {
            return inDeckList;
        }
        set
        {
            inDeckList = value;
        }
    }

    public bool IsInShopMenu
    {
        get
        {
            return isInShopMenu;
        }
        set
        {
            isInShopMenu = value;
        }
    }

    public bool InItemSlot => inItemSlot;

    public int CardIndex
    {
        get
        {
            return cardIndex;
        }
        set
        {
            cardIndex = value;
        }
    }

    public int CardBuyValue
    {
        get
        {
            return cardBuyValue;
        }
        set
        {
            cardBuyValue = value;
        }
    }

    public int CardUpgradeValue
    {
        get
        {
            return cardUpgradeValue;
        }
        set
        {
            cardUpgradeValue = value;
        }
    }

    private void OnEnable()
    {
        PlayAnimationOnSteamDeck();
    }

    private void Awake()
    {
        UpdateCardInformation();
    }

    private void Update()
    {
        if(!hoveringOver && isInShopMenu && inDeckList && inItemSlot)
        {
            return;
        }
        else if(hoveringOver && !inDeckList && !inItemSlot && !isInShopMenu)
        {
            if(InputManager.instance.ControllerPluggedIn)
            {
                if(InputManager.instance.ControllerName == "xbox")
                {
                    if (Input.GetButtonDown("XboxOpenChest"))
                    {
                        SalvageCardPanel(this, false);
                    }
                    else if(Input.GetButtonDown("XboxSalvageAll"))
                    {
                        SalvageCardPanel(this, true);
                    }
                }
                else
                {
                    if (Input.GetButtonDown("Ps4OpenChest"))
                    {
                        SalvageCardPanel(this, false);
                    }
                    else if(Input.GetButtonDown("Ps4SalvageAll"))
                    {
                        SalvageCardPanel(this, true);
                    }
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(1))
                {
                    SalvageCardPanel(this, false);
                }
                else if(Input.GetMouseButtonDown(2))
                {
                    SalvageCardPanel(this, true);
                }
            }
        }
    }

    private void PlayAnimationOnSteamDeck()
    {
        if(SteamUtils.IsSteamRunningOnSteamDeck())
        {
            if (gameObject.activeSelf)
            {
                animator.enabled = true;

                cardParticleObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);

                if (gameObject.GetComponent<Canvas>())
                {
                    GetComponent<Canvas>().overrideSorting = true;
                    GetComponent<Canvas>().sortingOrder = 1;
                }

                if (!inItemSlot || isInShopMenu)
                {
                    animator.Play("Idle", -1, 0);
                }
            }
        }
    }

    public void UpdateCardInformation()
    {
        cardBuyValue = cardTemplate.buyValue;
        cardUpgradeValue = cardTemplate.upgradeValue;

        if (cardTemplate.isAFoil)
        {
            uiShiny.enabled = true;

            foilBanner.SetActive(true);
        }
        else
        {
            uiShiny.enabled = false;

            foilBanner.SetActive(false);
        }

        if (cardTemplate.cardStatus != CardStatus.NONE)
        {
            hasStatusEffect = true;
        }
        else
        {
            hasStatusEffect = false;
        }

        switch (cardTemplate.cardType)
        {
            case (CardType.Action):
                healObject.SetActive(false);
                damageObject.SetActive(true);
                cardCostObject.SetActive(true);
                cardImage.sprite = cardTemplate.cardSprite;
                cardNameText.text = cardTemplate.cardName;
                cardCostText.text = cardTemplate.cardPointCost.ToString();
                damageValueText.text = cardTemplate.strength.ToString();
                cardInformation.text = "Target: " + CardTarget() + TypeOfCard();
                cardTypeImage.sprite = actionSprite;
                break;
            case (CardType.Magic):
                healObject.SetActive(false);
                damageObject.SetActive(true);
                cardCostObject.SetActive(true);
                cardImage.sprite = cardTemplate.cardSprite;
                cardNameText.text = cardTemplate.cardName;
                cardCostText.text = cardTemplate.cardPointCost.ToString();
                damageValueText.text = cardTemplate.strength.ToString();
                cardInformation.text = "Target: " + CardTarget() + TypeOfCard();
                cardTypeImage.sprite = magicSprite;
                break;
            case (CardType.Support):
                if(cardTemplate.strength > 0)
                {
                    healObject.SetActive(true);
                }
                else
                {
                    healObject.SetActive(false);
                }
                damageObject.SetActive(false);
                cardCostObject.SetActive(true);
                cardImage.sprite = cardTemplate.cardSprite;
                cardNameText.text = cardTemplate.cardName;
                cardCostText.text = cardTemplate.cardPointCost.ToString();
                healValueText.text = cardTemplate.strength.ToString();
                cardInformation.text = cardTemplate.cardEffect == CardEffect.RemoveStatus ? cardTemplate.cardInformation + TypeOfCard() : "Target: " + CardTarget() + TypeOfCard();
                cardTypeImage.sprite = supportSprite;
                break;
            case (CardType.Mystic):
                healObject.SetActive(false);
                damageObject.SetActive(false);
                cardCostObject.SetActive(true);
                cardImage.sprite = cardTemplate.cardSprite;
                cardNameText.text = cardTemplate.cardName;
                cardCostText.text = cardTemplate.cardPointCost.ToString();
                cardInformation.text = cardTemplate.cardStatus == CardStatus.NONE ? cardTemplate.cardInformation + TypeOfCard() : "Target: " + CardTarget() + TypeOfCard();
                cardTypeImage.sprite = mysticSprite;
                break;
            case (CardType.Item):
                if(cardTemplate.target == Target.SingleEnemy || cardTemplate.target == Target.AllEnemies)
                {
                    healObject.SetActive(false);
                    if (cardTemplate.strength > 0)
                    {
                        damageObject.SetActive(true);
                        damageValueText.text = cardTemplate.strength.ToString();
                    }
                    else
                    {
                        damageObject.SetActive(false);
                    }
                }
                else
                {
                    damageObject.SetActive(false);
                    if (cardTemplate.strength > 0)
                    {
                        healObject.SetActive(true);
                        healValueText.text = cardTemplate.strength.ToString();
                    }
                    else
                    {
                        healObject.SetActive(false);
                    }
                }
                cardCostObject.SetActive(false);
                cardImage.sprite = cardTemplate.cardSprite;
                cardNameText.text = cardTemplate.cardName;
                cardInformation.text = cardTemplate.showInfo ? cardTemplate.cardInformation + TypeOfCard() : "Target: " + CardTarget() + TypeOfCard();
                cardTypeImage.sprite = itemSprite;
                break;
        }
    }

    public void SetCardPrice()
    {
        cardPrice.text = cardTemplate.buyValue.ToString();
    }

    public void SetUpgradePrice()
    {
        cardPrice.text = cardTemplate.upgradeValue.ToString();
    }

    public void CheckPenalty()
    {
        for (int i = 0; i < NodeManager.instance._StagePenalty.Count; i++)
        {
            switch (NodeManager.instance._StagePenalty[i])
            {
                case (StagePenalty.Power):
                    if (cardTemplate.cardType == CardType.Action)
                    {
                        forbiddenIcon.SetActive(true);
                    }
                    break;
                case (StagePenalty.Magic):
                    if (cardTemplate.cardType == CardType.Magic)
                    {
                        forbiddenIcon.SetActive(true);
                    }
                    break;
                case (StagePenalty.Support):
                    if (cardTemplate.cardType == CardType.Support)
                    {
                        forbiddenIcon.SetActive(true);
                    }
                    break;
                case (StagePenalty.Mystic):
                    if (cardTemplate.cardType == CardType.Mystic)
                    {
                        forbiddenIcon.SetActive(true);
                    }
                    break;
                case (StagePenalty.Item):
                    if (cardTemplate.cardType == CardType.Item)
                    {
                        forbiddenIcon.SetActive(true);
                    }
                    break;
            }
        }
    }

    private void SalvageCardPanel(MenuCard card, bool salvageAll)
    {
        if(MenuController.instance.DeckListContent.childCount < playerMenuInfo.minimumDeckSize)
        {
            MenuController.instance.ErrorMessage("Your deck must contain at least " + playerMenuInfo.minimumDeckSize + " cards before salvaging.");
        }
        else
        {
            AudioManager.instance.PlaySoundEffect(AudioManager.instance.OpenMenuAudio);

            if(!salvageAll)
            {
                MenuController.instance._SalvageCardPanel.SetUpSalvagePanel(card);

                MenuController.instance._SalvageCardPanel.ConfirmButton.onClick.RemoveAllListeners();

                MenuController.instance._SalvageCardPanel.DeclineButton.onClick.RemoveAllListeners();

                MenuController.instance._SalvageCardPanel.DeclineButton.onClick.AddListener(() => MenuController.instance._SalvageCardPanel.DeclineButtonListener(card));

                MenuController.instance._SalvageCardPanel.ConfirmButton.onClick.AddListener(() => MenuController.instance._SalvageCardPanel.Salvage());

                MenuController.instance.DeckUiBlocker.SetActive(true);
            }
            else
            {
                MenuController.instance._SalvageCardPanel.SetUpSalvageAllPanel(card, MenuController.instance.StashContent);

                MenuController.instance._SalvageCardPanel.ConfirmButton.onClick.RemoveAllListeners();

                MenuController.instance._SalvageCardPanel.DeclineButton.onClick.RemoveAllListeners();

                MenuController.instance._SalvageCardPanel.DeclineButton.onClick.AddListener(() => MenuController.instance._SalvageCardPanel.DeclineButtonListener(card));

                MenuController.instance._SalvageCardPanel.ConfirmButton.onClick.AddListener(() => MenuController.instance._SalvageCardPanel.SalvageAll(MenuController.instance.StashContent));

                MenuController.instance.DeckUiBlocker.SetActive(true);
            }
        }
    }

    private string CardTarget()
    {
        string target = "";

        switch (cardTemplate.target)
        {
            case (Target.Player):
                target = "Self";
                break;
            case (Target.SingleEnemy):
                target = "\nSingle Enemy";
                break;
            case (Target.AllEnemies):
                target = "\nAll Enemies";
                break;
        }

        return target;
    }

    private string TypeOfCard()
    {
        string type = "";

        switch (cardTemplate.cardType)
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

    public void ShowStatusPanel()
    {
        if(hasStatusEffect)
        {
            if (!isInBattle)
            {
                if (InDeckList)
                {
                    statusEffectPanelDecklist.SetActive(true);

                    statusEffectNameDecklist.text = cardTemplate.statusEffectName;
                    statusEffectDescriptionDecklist.text = cardTemplate.statusBoostPercentage > 0 ? cardTemplate.cardInformation + " " + cardTemplate.statusBoostPercentage + "%." : cardTemplate.cardInformation;
                    statusEffectDurationDeckList.text = cardTemplate.statusEffectTime <= 0 ? "Duration: Infinite" : "Duration: " + cardTemplate.statusEffectTime + " Turn(s)" + ", Success: " + cardTemplate.statusEffectChance + "%";
                }
                else if (!inDeckList && !inItemSlot)
                {
                    statusEffectPanelStash.SetActive(true);

                    statusEffectNameStash.text = cardTemplate.statusEffectName;
                    statusEffectDescriptionStash.text = cardTemplate.statusBoostPercentage > 0 ? cardTemplate.cardInformation + " " + cardTemplate.statusBoostPercentage + "%." : cardTemplate.cardInformation;
                    statusEffectDurationStash.text = cardTemplate.statusEffectTime <= 0 ? "Duration: Infinite" : "Duration: " + cardTemplate.statusEffectTime + " Turn(s)" + ", Success: " + cardTemplate.statusEffectChance + "%";
                }
                else if(inItemSlot)
                {
                    statusEffectPanelItem.SetActive(true);

                    statusEffectNameItem.text = cardTemplate.statusEffectName;
                    statusEffectDescriptionItem.text = cardTemplate.statusBoostPercentage > 0 ? cardTemplate.cardInformation + " " + cardTemplate.statusBoostPercentage + "%." : cardTemplate.cardInformation;
                    statusEffectDurationItem.text = cardTemplate.statusEffectTime <= 0 ? "Duration: Infinite" : "Duration: " + cardTemplate.statusEffectTime + " Turn(s)" + ", Success: " + cardTemplate.statusEffectChance + "%";
                }

                if(isInSettingsMenu)
                {
                    statusEffectPanelSettings.SetActive(true);

                    statusEffectNameSettings.text = cardTemplate.statusEffectName;
                    statusEffectDescriptionSettings.text = cardTemplate.statusBoostPercentage > 0 ? cardTemplate.cardInformation + " " + cardTemplate.statusBoostPercentage + "%." : cardTemplate.cardInformation;
                    statusEffectDurationSettings.text = cardTemplate.statusEffectTime <= 0 ? "Duration: Infinite" : "Duration: " + cardTemplate.statusEffectTime + " Turn(s)" + ", Success: " + cardTemplate.statusEffectChance + "%";
                }
            }
            else
            {
                spoilsInfoAnimator.Play("OnPointerStatusPanel", -1, 0);

                SpoilsManager.instance.StickerStatusInfo.gameObject.SetActive(false);
                SpoilsManager.instance.SpoilsInfoText.gameObject.SetActive(true);
                SpoilsManager.instance.StickerCostFrame.SetActive(false);
                SpoilsManager.instance.DurationPanel.SetActive(true);

                SpoilsManager.instance.StatusDurationText.text = cardTemplate.statusEffectTime <= 0 ? "Duration: Infinite" : "Duration: " + cardTemplate.statusEffectTime + " Turn(s)" + ", Success: " + cardTemplate.statusEffectChance + "%";

                SpoilsManager.instance.SpoilsTitleText.text = cardTemplate.statusEffectName;
                SpoilsManager.instance.SpoilsInfoText.text = cardTemplate.statusBoostPercentage > 0 ? cardTemplate.cardInformation + " " + cardTemplate.statusBoostPercentage + "%." : cardTemplate.cardInformation;
            }
        }
    }

    public void HideStatusPanel()
    {
        if (hasStatusEffect)
        {
            if (!isInBattle)
            {
                statusEffectPanelDecklist.SetActive(false);
                statusEffectPanelStash.SetActive(false);
                statusEffectPanelItem.SetActive(false);
                statusEffectPanelSettings.SetActive(false);
            }
            else
            {
                spoilsInfoAnimator.Play("Reverse", -1, 0);
            }
        }
    }

    public void HideStatusPanelController()
    {
        if(InputManager.instance.ControllerPluggedIn)
        {
            if (hasStatusEffect)
            {
                if (!isInBattle)
                {
                    statusEffectPanelDecklist.SetActive(false);
                    statusEffectPanelStash.SetActive(false);
                    statusEffectPanelItem.SetActive(false);
                    statusEffectPanelSettings.SetActive(false);
                }
                else
                {
                    spoilsInfoAnimator.Play("Reverse", -1, 0);
                }
            }
        }
    }

    public void MoveCard()
    {
        if(!isInShopMenu)
        {
            if (cardTemplate.cardType == CardType.Item)
            {
                if (!inItemSlot)
                {
                    if (playerMenuInfo.equippedItems < playerMenuInfo.currentItemSlotSize)
                    {
                        if (newCardText.activeSelf)
                        {
                            newCardText.SetActive(false);
                        }

                        EquipItemCard();

                        AudioManager.instance.PlaySoundEffect(AudioManager.instance.EquipAudio);

                        cardParticleObject.SetActive(false);

                        MenuController.instance._DeckMenu.AdjustDeckCardsNavigations();
                        MenuController.instance._DeckMenu.AdjustStashCardsNavigations();

                        if (MenuController.instance._DeckMenu.CurrentStashParent.childCount > 0)
                        {
                            InputManager.instance.SetSelectedObject(MenuController.instance._DeckMenu.CurrentStashParent.GetChild(0).gameObject);
                        }
                        else
                        {
                            InputManager.instance._EventSystem.SetSelectedGameObject(null);
                            InputManager.instance.SetSelectedObject(MenuController.instance._DeckMenu.ItemCards[0].gameObject);
                        }
                        MenuController.instance._DeckMenu.AdjustCardCategoryButtonNavigations();

                        cardSparkleParticle.gameObject.SetActive(true);
                        cardSparkleParticle.Play();
                    }
                    else
                    {
                        ErrorMessage("You can't add anymore items!");
                    }
                }
                else
                {
                    if(MenuController.instance.StashCount < playerMenuInfo.stashLimit)
                    {
                        UnEquipItemCard();

                        AudioManager.instance.PlaySoundEffect(AudioManager.instance.EquipAudio);

                        cardParticleObject.SetActive(false);

                        MenuController.instance._DeckMenu.AdjustDeckCardsNavigations();
                        MenuController.instance._DeckMenu.AdjustStashCardsNavigations();

                        if (MenuController.instance._DeckMenu.ItemCards.Count > 0)
                        {
                            InputManager.instance._EventSystem.SetSelectedGameObject(null);
                            InputManager.instance.SetSelectedObject(MenuController.instance._DeckMenu.ItemCards[0].gameObject);
                        }
                        else
                        {
                            InputManager.instance._EventSystem.SetSelectedGameObject(null);
                            if (MenuController.instance._DeckMenu.CurrentStashParent.childCount > 0)
                            {
                                InputManager.instance.SetSelectedObject(MenuController.instance._DeckMenu.CurrentStashParent.GetChild(0).gameObject);
                            }
                            else
                            {
                                InputManager.instance.SetSelectedObject(MenuController.instance._DeckMenu.ExitButton.gameObject);
                            }
                        }
                        MenuController.instance._DeckMenu.AdjustCardCategoryButtonNavigations();

                        cardSparkleParticle.gameObject.SetActive(true);
                        cardSparkleParticle.Play();
                    }
                    else
                    {
                        ErrorMessage("Stash is full!");
                    }
                }
            }
            else
            {
                if (inDeckList)
                {
                    if(MenuController.instance.StashCount < playerMenuInfo.stashLimit)
                    {
                        if (cardTemplate.cardType == CardType.Mystic)
                        {
                            playerMenuInfo.currentlyEquippedMysticCards--;

                            MenuController.instance.UpdateMysticCardCount();
                        }

                        transform.SetParent(MenuController.instance._CardTypeCategory.SetCardParent(this));
                        inDeckList = false;

                        HideStatusPanel();

                        propogateDrag.scrollView = MenuController.instance.CardListScroll;

                        mainCharacterStats.currentCards.RemoveAt(cardIndex);

                        MenuController.instance.UpdateDeckLimit();
                        MenuController.instance.UpdateStashCount();
                        MenuController.instance.ReOrganizeCardIndex();

                        AudioManager.instance.PlaySoundEffect(AudioManager.instance.EquipAudio);

                        cardParticleObject.SetActive(false);

                        MenuController.instance._DeckMenu.AdjustDeckCardsNavigations();
                        MenuController.instance._DeckMenu.AdjustStashCardsNavigations();

                        if (MenuController.instance._DeckMenu.DeckParent.childCount > 0)
                        {
                            InputManager.instance._EventSystem.SetSelectedGameObject(null);
                            InputManager.instance.SetSelectedObject(MenuController.instance._DeckMenu.DeckParent.GetChild(0).gameObject);
                        }
                        else
                        {
                            if (MenuController.instance._DeckMenu.CurrentStashParent.childCount > 0)
                            {
                                InputManager.instance.SetSelectedObject(MenuController.instance._DeckMenu.CurrentStashParent.GetChild(0).gameObject);
                            }
                            else
                            {
                                InputManager.instance.SetSelectedObject(MenuController.instance._DeckMenu.ExitButton.gameObject);
                            }
                        }
                        MenuController.instance._DeckMenu.AdjustCardCategoryButtonNavigations();

                        cardSparkleParticle.gameObject.SetActive(true);
                        cardSparkleParticle.Play();
                    }
                    else
                    {
                        ErrorMessage("Stash is full!");
                    }
                }
                else
                {
                    if (MenuController.instance.DeckListContent.childCount < playerMenuInfo.currentDeckLimit)
                    {
                        if(cardTemplate.cardType == CardType.Mystic)
                        {
                            if(playerMenuInfo.currentlyEquippedMysticCards < playerMenuInfo.maximumMysticCards)
                            {
                                playerMenuInfo.currentlyEquippedMysticCards++;

                                MenuController.instance.UpdateMysticCardCount();

                                if (newCardText.activeSelf)
                                {
                                    newCardText.SetActive(false);
                                }

                                transform.SetParent(deckListParent);
                                inDeckList = true;

                                propogateDrag.scrollView = MenuController.instance.DeckListScroll;

                                mainCharacterStats.currentCards.Add(_cardTemplate);

                                MenuController.instance.UpdateDeckLimit();
                                MenuController.instance.UpdateStashCount();
                                MenuController.instance.ReOrganizeCardIndex();

                                AudioManager.instance.PlaySoundEffect(AudioManager.instance.EquipAudio);

                                cardParticleObject.SetActive(false);

                                MenuController.instance._DeckMenu.AdjustDeckCardsNavigations();
                                MenuController.instance._DeckMenu.AdjustStashCardsNavigations();

                                if (MenuController.instance._DeckMenu.CurrentStashParent.childCount > 0)
                                {
                                    InputManager.instance.SetSelectedObject(MenuController.instance._DeckMenu.CurrentStashParent.GetChild(0).gameObject);
                                }
                                else
                                {
                                    InputManager.instance.SetSelectedObject(MenuController.instance._DeckMenu.DeckParent.GetChild(0).gameObject);
                                }
                                MenuController.instance._DeckMenu.AdjustCardCategoryButtonNavigations();

                                cardSparkleParticle.gameObject.SetActive(true);
                                cardSparkleParticle.Play();
                            }
                            else
                            {
                                ErrorMessage("You can't add anymore <color=#A433D2>Mystic</color> cards.");
                            }
                        }
                        else
                        {
                            if (newCardText.activeSelf)
                            {
                                newCardText.SetActive(false);
                            }

                            transform.SetParent(deckListParent);
                            inDeckList = true;

                            propogateDrag.scrollView = MenuController.instance.DeckListScroll;

                            mainCharacterStats.currentCards.Add(_cardTemplate);

                            MenuController.instance.UpdateDeckLimit();
                            MenuController.instance.UpdateStashCount();
                            MenuController.instance.ReOrganizeCardIndex();

                            AudioManager.instance.PlaySoundEffect(AudioManager.instance.EquipAudio);

                            cardParticleObject.SetActive(false);

                            MenuController.instance._DeckMenu.AdjustDeckCardsNavigations();
                            MenuController.instance._DeckMenu.AdjustStashCardsNavigations();

                            if (MenuController.instance._DeckMenu.CurrentStashParent.childCount > 0)
                            {
                                InputManager.instance.SetSelectedObject(MenuController.instance._DeckMenu.CurrentStashParent.GetChild(0).gameObject);
                            }
                            else
                            {
                                InputManager.instance.SetSelectedObject(MenuController.instance._DeckMenu.DeckParent.GetChild(0).gameObject);
                            }
                            MenuController.instance._DeckMenu.AdjustCardCategoryButtonNavigations();

                            cardSparkleParticle.gameObject.SetActive(true);
                            cardSparkleParticle.Play();

                            if (MenuController.instance.DeckListContent.childCount >= playerMenuInfo.currentDeckLimit)
                            {
                                if (SteamManager.Initialized)
                                {
                                    SteamUserStats.GetAchievement("ACH_FOILED_OUT", out bool achievementCompleted);

                                    if (!achievementCompleted)
                                    {
                                        int deckCount = 0;

                                        for(int i = 0; i < MenuController.instance.DeckListContent.childCount; i++)
                                        {
                                            MenuCard menuCrd = MenuController.instance.DeckListContent.GetChild(i).GetComponent<MenuCard>();

                                            if (!menuCrd.cardTemplate.isAFoil) break;

                                            deckCount++;
                                        }

                                        if(deckCount >= playerMenuInfo.currentDeckLimit)
                                        {
                                            SteamUserStats.SetAchievement("ACH_FOILED_OUT");
                                            SteamUserStats.StoreStats();
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        ErrorMessage("You can't add anymore cards.");
                    }
                }
            }
        }
    }

    public void EquipItemCard()
    {
        playerMenuInfo.equippedItems++;

        inItemSlot = true;

        transform.SetParent(MenuController.instance.ItemList, false);

        transform.SetAsFirstSibling();

        rectTransform.sizeDelta = new Vector2(90, 120);

        transform.localScale = new Vector3(0.4744057f, 0.4744057f, 0.4744057f);

        switch(playerMenuInfo.equippedItems)
        {
            case (1):
                itemsTransform[0].gameObject.SetActive(false);
                break;
            case (2):
                itemsTransform[1].gameObject.SetActive(false);
                break;
            case (3):
                itemsTransform[2].gameObject.SetActive(false);
                break;
        }

        playerMenuInfo.currentEquippedItems.Add(cardTemplate);

        cardParticleObject.SetActive(false);

        if(!SteamUtils.IsSteamRunningOnSteamDeck())
        {
            animator.enabled = true;
        }
        else
        {
            animator.Play("ReverseHoveredItemMenu", -1, 0);
        }

        MenuController.instance.UpdateStashCount();
    }

    private void UnEquipItemCard()
    {
        playerMenuInfo.equippedItems--;

        inItemSlot = false;

        MenuController.instance._CardTypeCategory.SetCardParent(this);

        HideStatusPanel();

        rectTransform.sizeDelta = new Vector2(100, 100);

        transform.localScale = new Vector3(0.8104491f, 0.8104491f, 0.8104491f);

        switch (playerMenuInfo.equippedItems)
        {
            case (0):
                itemsTransform[0].gameObject.SetActive(true);
                break;
            case (1):
                itemsTransform[1].gameObject.SetActive(true);
                break;
            case (2):
                itemsTransform[2].gameObject.SetActive(true);
                break;
        }

        playerMenuInfo.currentEquippedItems.Remove(cardTemplate);

        cardParticleObject.SetActive(false);

        if(!SteamUtils.IsSteamRunningOnSteamDeck())
        {
            animator.enabled = false;
        }

        MenuController.instance.UpdateStashCount();
    }

    private void ToggleImageMasks(bool toggle)
    {
        if(toggle)
        {
            cardNameText.maskable = false;
            cardObject.GetComponent<Image>().maskable = false;
            cardCostObject.GetComponent<Image>().maskable = false;
            cardCostText.maskable = false;
            newCardText.GetComponent<Text>().maskable = false;
        }
        else
        {
            cardNameText.maskable = true;
            cardObject.GetComponent<Image>().maskable = true;
            cardCostObject.GetComponent<Image>().maskable = false;
            cardCostText.maskable = true;
            newCardText.GetComponent<Text>().maskable = true;
        }
    }

    public void HoverItemCard()
    {
        if(SteamUtils.IsSteamRunningOnSteamDeck())
        {
            if (!isInShopMenu)
            {
                if (inItemSlot && !isInBattle)
                {
                    animator.Play("HoveredCardItemSteam", -1, 0);
                }
                else if (!inItemSlot && !isInBattle)
                {
                    ToggleImageMasks(true);
                    animator.Play("DeckHover", -1, 0);
                    GetComponent<Canvas>().sortingOrder = 5;
                }
            }
            else
            {
                if (!isInBattle)
                {
                    ToggleImageMasks(true);
                    animator.Play("HoveredShop", -1, 0);
                    GetComponent<Canvas>().sortingOrder = 5;
                }
            }
        }
        else
        {
            if (inItemSlot && !isInShopMenu && !isInBattle)
            {
                animator.Play("HoveredItemMenu", -1, 0);
            }
        }
    }

    public void UnHoverItemCard()
    {
        if(SteamUtils.IsSteamRunningOnSteamDeck())
        {
            if (inItemSlot && !isInShopMenu && !isInBattle)
            {
                animator.Play("ReversedHoveredCardItemSteam", -1, 0);
            }
            else if(!inItemSlot && !isInBattle || isInShopMenu)
            {
                ToggleImageMasks(false);
                if (isInShopMenu)
                {
                    animator.Play("ReverseShop", -1, 0);
                }
                else
                {
                    animator.Play("ReversedDeckHover", -1, 0);
                }
                GetComponent<Canvas>().sortingOrder = 1;
            }
        }
        else
        {
            if (inItemSlot && !isInShopMenu && !isInBattle)
            {
                animator.Play("ReverseHoveredItemMenu", -1, 0);
            }
        }
    }

    public void ShowCardParticle()
    {
        cardParticleObject.SetActive(true);
        cardParticleObject.GetComponent<UIParticle>().RefreshParticles();

        if(PlayerPrefs.HasKey("SoundEffects"))
        {
            audioSource.volume = PlayerPrefs.GetFloat("SoundEffects");
            audioSource.Play();
        }
        else
        {
            audioSource.Play();
        }

        if(isInShopMenu)
        {
            if(!ShopMenu.instance.UpgradePanel.activeSelf)
                ShopMenu.instance.ViewStatusEffectPanel(this);
        }
    }

    public void HideCardParticle()
    {
        cardParticleObject.SetActive(false);

        if(isInShopMenu)
        {
            if (!ShopMenu.instance.UpgradePanel.activeSelf)
                ShopMenu.instance.FadeOutStatusPanel(this);
        }
    }

    public void HideCardParticleController()
    {
        cardParticleObject.SetActive(false);

        if (isInShopMenu)
        {
            if(InputManager.instance.ControllerPluggedIn)
            {
                if(!ShopMenu.instance.UpgradePanel.activeSelf)
                    ShopMenu.instance.FadeOutStatusPanel(this);
            }
        }
    }

    public void ShowBestiaryInfo()
    {
        MenuController.instance._BestiaryMenu._MenuCard.gameObject.SetActive(true);

        MenuController.instance._BestiaryMenu._MenuCard.cardTemplate = cardTemplate;

        MenuController.instance._BestiaryMenu._MenuCard.UpdateCardInformation();

        if(hasStatusEffect)
        {
            MenuController.instance._BestiaryMenu.ItemInfoPanel.SetActive(true);
            MenuController.instance._BestiaryMenu.StatusDurationPanel.SetActive(true);
            MenuController.instance._BestiaryMenu.ItemInfoText.gameObject.SetActive(true);
            MenuController.instance._BestiaryMenu.ItemInfoTextStickerStatus.gameObject.SetActive(false);

            MenuController.instance._BestiaryMenu.StatusDurationText.text = cardTemplate.statusEffectTime == 0 ? "Duration: Infinite" : "Duration: " + cardTemplate.statusEffectTime + " Turn(s)";

            MenuController.instance._BestiaryMenu.ItemInfoNameText.text = cardTemplate.statusEffectName;

            MenuController.instance._BestiaryMenu.ItemInfoText.text = cardTemplate.statusBoostPercentage > 0 ? cardTemplate.cardInformation + " " + cardTemplate.statusBoostPercentage + "%." : cardTemplate.cardInformation;
        }
    }

    public void HideBestiaryInfo()
    {
        MenuController.instance._BestiaryMenu._MenuCard.gameObject.SetActive(false);
        MenuController.instance._BestiaryMenu.ItemInfoPanel.SetActive(false);
        MenuController.instance._BestiaryMenu.StatusDurationPanel.SetActive(false);
    }

    private void ErrorMessage(string message)
    {
        errorMessageText.text = message;

        AudioManager.instance.PlaySoundEffect(AudioManager.instance.ErrorMessageAudio);

        errorMessageAnimator.Play("Message", -1, 0);
    }

    public void ShowUnlockableCardStatusPanel(GameObject statusPanel)
    {
        if(hasStatusEffect)
        {
            statusPanel.SetActive(true);
        }
    }

    public void SetDefaultScrollView()
    {
        propogateDrag.scrollView = MenuController.instance.CardListScroll;
    }
}