using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Steamworks;

public class Card : MonoBehaviour
{
    [SerializeField]
    private Deck deck;

    [SerializeField]
    private BattleSystem battleSystem;

    [SerializeField]
    private ComboCards comboCards;

    [SerializeField]
    private CardTemplate cardTemplate;

    [SerializeField]
    private MysticCardAbilities mysticCardAbilities;

    [SerializeField]
    private List<CardTemplate> combinedCard = new List<CardTemplate>();

    [SerializeField]
    private List<CardTemplateName> twoComboTemplateName = new List<CardTemplateName>();

    [SerializeField]
    private List<CardTemplateName> threeComboTemplateName = new List<CardTemplateName>();

    [SerializeField]
    private StatusEffects statusEffectPrefab;

    [SerializeField]
    private Animator animator, statusEffectPanelAnimator, selectedCardAnimator, statusEffectPanelAnimatorHover;

    [SerializeField]
    private RuntimeAnimatorController handController;

    [SerializeField]
    private Transform deckParent, graveParent, handPanelParent;

    [SerializeField]
    private Button cardButton;

    [SerializeField]
    private Image cardTypeImage, cardImage, cardImageFoil, coolDownImage = null;

    [SerializeField]
    private RectTransform rectTransform;

    [SerializeField]
    private Sprite actionSprite, magicSprite, supportSprite, mysticSprite;

    [SerializeField]
    private GameObject combineFrame, selectionObject, cardCostObject, damageObject, healObject, maskImage, cardObject, cardPortrait, forbiddenIcon, foilBanner, charityCheckMark, hoverParticle;

    [SerializeField]
    private Color ableToCombineColor, unableToCombineColor, twoCardComboColor, threeCardComboColor;

    [SerializeField]
    private ParticleSystem combinationParticle, combinationChildParticle, particleEffect;

    [SerializeField]
    private TextMeshProUGUI cardInformation, selectedIndexText, cardNameText, cardCostText, damageValueText, healValueText, statusEffectNameText, statusEffectDescriptionText, statusEffectDurationText, 
                            statusEffectDurationTextHover, statusEffectNameTextHover, statusEffectDescriptionTextHover;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip cardSelectedAudioClip, hoveredOverAudioClip;

    [SerializeField]
    private PropogateDrag propogateDrag;

    [SerializeField]
    private int cardPositionIndex, cardPoints;

    [SerializeField]
    private bool isACombinedCard;

    [SerializeField]
    private bool canCombineWithOtherCards, chosenForCombining, isParticleAProjectile, canCombineWith, cannotBeUsed, chosenForCharity, addedSameNamedCardForTwoCardCombo,
                 addedSameNamedCardForThreeCardCombo, appliesStatusEffect, shouldCheckStatus, isCompatibleComboCard;

    private int selectedIndex, itemCoolDown, cardStrength, statusEffectChance, statusEffectTime;

    private string statusEffectName, cardInfo;

    private Sprite statusEffectSprite;

    private CardStatus cardStatus;

    private Scene scene;

    public GameObject CardObject => cardObject;

    public GameObject CombineFrame => combineFrame;

    public Animator _Animator => animator;

    public PropogateDrag _PropogateDrag => propogateDrag;

    public Animator SelectedCardAnimator
    {
        get
        {
            return selectedCardAnimator;
        }
        set
        {
            selectedCardAnimator = value;
        }
    }

    public List<CardTemplateName> TwoComboTemplateName
    {
        get
        {
            return twoComboTemplateName;
        }
        set
        {
            twoComboTemplateName = value;
        }
    }

    public List<CardTemplateName> ThreeComboTemplateName
    {
        get
        {
            return threeComboTemplateName;
        }
        set
        {
            threeComboTemplateName = value;
        }
    }

    public List<CardTemplate> CombinedCard
    {
        get
        {
            return combinedCard;
        }
        set
        {
            combinedCard = value;
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

    public ParticleSystem ParticleEffect
    {
        get
        {
            return particleEffect;
        }
        set
        {
            particleEffect = value;
        }
    }

    public ParticleSystem CombinationParticle
    {
        get
        {
            return combinationParticle;
        }
        set
        {
            combinationParticle = value;
        }
    }

    public GameObject CharityCheckMark
    {
        get
        {
            return charityCheckMark;
        }
        set
        {
            charityCheckMark = value;
        }
    }

    public GameObject SelectionObject
    {
        get
        {
            return selectionObject;
        }
        set
        {
            selectionObject = value;
        }
    }

    public TextMeshProUGUI SelectedIndexText
    {
        get
        {
            return selectedIndexText;
        }
        set
        {
            selectedIndexText = value;
        }
    }

    public TextMeshProUGUI CardCostText
    {
        get
        {
            return cardCostText;
        }
        set
        {
            cardCostText = value;
        }
    }

    public TextMeshProUGUI DamageValueText
    {
        get
        {
            return damageValueText;
        }
        set
        {
            damageValueText = value;
        }
    }

    public Button CardButton
    {
        get
        {
            return cardButton;
        }
        set
        {
            cardButton = value;
        }
    }

    public PropogateDrag _PorpogateDrag
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

    public Image CoolDownImage => coolDownImage;

    public CardStatus _CardStatus => cardStatus;

    public bool IsCompatibleComboCard
    {
        get
        {
            return isCompatibleComboCard;
        }
        set
        {
            isCompatibleComboCard = value;
        }
    }

    public bool AddedSameNamedCardForTwoCardCombo
    {
        get
        {
            return addedSameNamedCardForTwoCardCombo;
        }
        set
        {
            addedSameNamedCardForTwoCardCombo = value;
        }
    }

    public bool AddedSameNamedCardForThreeCardCombo
    {
        get
        {
            return addedSameNamedCardForThreeCardCombo;
        }
        set
        {
            addedSameNamedCardForThreeCardCombo = value;
        }
    }

    public bool ChosenForCharity
    {
        get
        {
            return chosenForCharity;
        }
        set
        {
            chosenForCharity = value;
        }
    }

    public bool CanCombineWithOtherCards
    {
        get
        {
            return canCombineWithOtherCards;
        }
        set
        {
            canCombineWithOtherCards = value;
        }
    }

    public bool CannotBeUsed
    {
        get
        {
            return cannotBeUsed;
        }
        set
        {
            cannotBeUsed = value;
        }
    }

    public bool IsAcombinedCard => isACombinedCard;

    public bool IsParticleAProjectile
    {
        get
        {
            return isParticleAProjectile;
        }
        set
        {
            isParticleAProjectile = value;
        }
    }

    public bool AppliesStatusEffect
    {
        get
        {
            return appliesStatusEffect;
        }
        set
        {
            appliesStatusEffect = value;
        }
    }

    public bool CanCombineWith
    {
        get
        {
            return canCombineWith;
        }
        set
        {
            canCombineWith = value;
        }
    }

    public int CardStrength
    {
        get
        {
            return cardStrength;
        }
        set
        {
            cardStrength = value;
        }
    }

    public int ItemCoolDown
    {
        get
        {
            return itemCoolDown;
        }
        set
        {
            itemCoolDown = value;
        }
    }

    public int CardPositionIndex
    {
        get
        {
            return cardPositionIndex;
        }
        set
        {
            cardPositionIndex = value;
        }
    }

    public int CardPoints
    {
        get
        {
            return cardPoints;
        }
        set
        {
            cardPoints = value;
        }
    }

    public int SelectedIndex
    {
        get
        {
            return selectedIndex;
        }
        set
        {
            selectedIndex = value;
        }
    }

    public bool ChosenForCombining
    {
        get
        {
            return chosenForCombining;
        }
        set
        {
            chosenForCombining = value;
        }
    }

    private void Awake()
    {
        CheckCurrentScene();

        cardButton.interactable = false;
    }

    public void CheckCardType()
    {
        cardStrength = cardTemplate.strength;

        if(cardTemplate.cardStatus !=  CardStatus.NONE)
        {
            cardStatus = cardTemplate.cardStatus;

            statusEffectName = cardTemplate.statusEffectName;
            statusEffectTime = cardTemplate.statusEffectTime;
            statusEffectChance = cardTemplate.statusEffectChance;
            statusEffectSprite = cardTemplate.statusEffectSprite;
            shouldCheckStatus = cardTemplate.shouldCheckStatus;
            cardInfo = cardTemplate.cardInformation;
        }
        else
        {
            cardStatus = CardStatus.NONE;
        }

        switch (cardTemplate.cardType)
        {
            case (CardType.Action):
                healObject.SetActive(false);
                damageObject.SetActive(true);
                cardCostObject.SetActive(true);
                cardImage.sprite = cardTemplate.cardSprite;
                cardNameText.text = cardTemplate.cardName;
                SetCardStrength();
                damageValueText.text = cardStrength.ToString();
                cardInformation.text = "Target: " + CardTarget();
                cardTypeImage.sprite = actionSprite;
                canCombineWithOtherCards = true;
                break;
            case (CardType.Magic):
                healObject.SetActive(false);
                damageObject.SetActive(true);
                cardCostObject.SetActive(true);
                cardImage.sprite = cardTemplate.cardSprite;
                cardNameText.text = cardTemplate.cardName;
                SetCardStrength();
                damageValueText.text = cardStrength.ToString();
                cardInformation.text = "Target: " + CardTarget();
                cardTypeImage.sprite = magicSprite;
                if (cardTemplate.canCombine)
                {
                    canCombineWithOtherCards = true;
                }
                else
                {
                    canCombineWithOtherCards = false;
                    canCombineWith = false;
                }
                break;
            case (CardType.Support):
                if (cardStrength > 0)
                {
                    healObject.SetActive(true);
                    damageObject.SetActive(false);
                }
                else
                {
                    healObject.SetActive(false);
                    damageObject.SetActive(false);
                }
                cardCostObject.SetActive(true);
                cardImage.sprite = cardTemplate.cardSprite;
                cardNameText.text = cardTemplate.cardName;
                SetCardStrength();
                cardInformation.text = cardTemplate.cardEffect == CardEffect.RemoveStatus ? cardTemplate.cardInformation : "Target: " + CardTarget();
                cardTypeImage.sprite = supportSprite;
                if(cardTemplate.canCombine)
                {
                    canCombineWithOtherCards = true;
                }
                else
                {
                    canCombineWithOtherCards = false;
                    canCombineWith = false;
                }
                break;
            case (CardType.Mystic):
                healObject.SetActive(false);
                damageObject.SetActive(false);
                cardCostObject.SetActive(true);
                cardImage.sprite = cardTemplate.cardSprite;
                cardNameText.text = cardTemplate.cardName;
                cardCostText.text = cardPoints.ToString();
                cardInformation.text = cardTemplate.cardInformation;
                cardTypeImage.sprite = mysticSprite;
                canCombineWithOtherCards = false;
                canCombineWith = false;
                break;
            case (CardType.Item):
                if (cardTemplate.target == Target.SingleEnemy || cardTemplate.target == Target.AllEnemies)
                {
                    healObject.SetActive(false);
                    if (cardStrength > 0)
                    {
                        damageObject.SetActive(true);
                    }
                    else
                    {
                        damageObject.SetActive(false);
                    }
                }
                else
                {
                    damageObject.SetActive(false);

                    if (cardStrength > 0)
                    {
                        healObject.SetActive(true);
                    }
                    else
                    {
                        healObject.SetActive(false);
                    }
                }
                cardCostObject.SetActive(false);
                cardImage.sprite = cardTemplate.cardSprite;
                cardNameText.text = cardTemplate.cardName;
                SetCardStrength();
                cardInformation.text = "Target: " + CardTarget();
                canCombineWithOtherCards = false;
                canCombineWith = false;
                break;
        }

        if(cardTemplate.isAFoil)
        {
            cardImageFoil.gameObject.SetActive(true);

            cardImageFoil.sprite = cardTemplate.cardSprite;

            foilBanner.SetActive(true);
        }
        else
        {
            cardImageFoil.gameObject.SetActive(false);

            foilBanner.SetActive(false);
        }

        if(isACombinedCard)
        {
            canCombineWith = false;
            canCombineWithOtherCards = false;
        }
    }

    public void AddStatusEffects()
    {
        if(MenuController.instance._StickerPowerManager.CheckStartOfBattleStickerPower(StickerPower.PlagueMasterMagic))
        {
            if (cardTemplate.cardType == CardType.Magic)
            {
                cardStatus = CardStatus.Poison;
                statusEffectSprite = battleSystem._StatusEffectManager.PoisonSprite;
                statusEffectName = "Poison";
                cardInfo = "Target suffers 10% damage at the start of their turn.";
                statusEffectTime = MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.Stretch) ? 2 : 1;
                statusEffectChance = 50;
                shouldCheckStatus = true;
            }
        }

        if (MenuController.instance._StickerPowerManager.CheckStartOfBattleStickerPower(StickerPower.PlaguueMasterPower))
        {
            if (cardTemplate.cardType == CardType.Action)
            {
                cardStatus = CardStatus.Poison;
                statusEffectSprite = battleSystem._StatusEffectManager.PoisonSprite;
                statusEffectName = "Poison";
                cardInfo = "Target suffers 10% damage at the start of their turn.";
                statusEffectTime = MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.Stretch) ? 2 : 1;
                statusEffectChance = 50;
                shouldCheckStatus = true;
            }
        }

        if (MenuController.instance._StickerPowerManager.CheckStartOfBattleStickerPower(StickerPower.ShockMasterMagic))
        {
            if(cardTemplate.cardType == CardType.Magic)
            {
                cardStatus = CardStatus.Paralysis;
                statusEffectSprite = battleSystem._StatusEffectManager.ParalysisSprite;
                statusEffectName = "Paralysis";
                cardInfo = "Target is unable to act, skipping their turn.";
                statusEffectTime = MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.Stretch) ? 2 : 1;
                statusEffectChance = 25;
                shouldCheckStatus = true;
            }
        }

        if(MenuController.instance._StickerPowerManager.CheckStartOfBattleStickerPower(StickerPower.ShockMasterPower))
        {
            if (cardTemplate.cardType == CardType.Action)
            {
                cardStatus = CardStatus.Paralysis;
                statusEffectSprite = battleSystem._StatusEffectManager.ParalysisSprite;
                statusEffectName = "Paralysis";
                cardInfo = "Target is unable to act, skipping their turn.";
                statusEffectTime = MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.Stretch) ? 2 : 1;
                statusEffectChance = 25;
                shouldCheckStatus = true;
            }
        }
    }

    public void SetCardStrength()
    {
        if(MenuController.instance._StickerPowerManager.CheckStartOfBattleStickerPower(StickerPower.ItemHater))
        {
            if(battleSystem._PlayerMenuInfo.currentEquippedItems.Count <= 0)
            {
                if (cardTemplate.cardType == CardType.Support)
                {
                    if (cardTemplate.cardEffect == CardEffect.HpHeal || cardTemplate.cardEffect == CardEffect.CpHeal)
                    {
                        cardStrength += 20;
                    }
                }
            }
        }

        if(MenuController.instance._StickerPowerManager.CheckStartOfBattleStickerPower(StickerPower.ItemLover))
        {
            if(battleSystem._PlayerMenuInfo.currentEquippedItems.Count >= battleSystem._PlayerMenuInfo.currentItemSlotSize)
            {
                if(cardTemplate.cardType == CardType.Item)
                {
                    cardStrength += 10;
                }
            }
        }

        if(MenuController.instance._StickerPowerManager.CheckStartOfBattleStickerPower(StickerPower.Apothecary))
        {
            if (cardTemplate.cardType == CardType.Item)
            {
                if (damageObject.activeSelf)
                {
                    cardStrength = cardTemplate.strength * 2;
                }
                else
                {
                    cardStrength = cardTemplate.strength * 2;
                }
            }
        }

        if(MenuController.instance._StickerPowerManager.CheckStartOfBattleStickerPower(StickerPower.Cleric))
        {
            if (cardTemplate.cardType == CardType.Support)
            {
                if (cardTemplate.cardEffect == CardEffect.HpHeal || cardTemplate.cardEffect == CardEffect.CpHeal)
                {
                    cardStrength += 5;
                }
            }
        }

        if(MenuController.instance._StickerPowerManager.CheckStartOfBattleStickerPower(StickerPower.Wizard))
        {
            if (cardTemplate.cardType == CardType.Magic)
            {
                cardStrength += 5;
            }
        }

        if(MenuController.instance._StickerPowerManager.CheckStartOfBattleStickerPower(StickerPower.Warrior))
        {
            if (cardTemplate.cardType == CardType.Action)
            {
                cardStrength += 5;
            }
        }

        if (damageObject.activeSelf)
        {
            if(cardStrength > cardTemplate.strength)
            {
                damageValueText.color = Color.green;
            }

            damageValueText.text = cardStrength.ToString();
        }
        else
        {
            if (cardStrength > cardTemplate.strength)
            {
                healValueText.color = Color.green;
            }

            healValueText.text = cardStrength.ToString();
        }
    }

    public void SetCardPoints(int pwrAmt, int magAmt, int supAmt)
    {
        cardPoints = cardTemplate.cardPointCost;

        if (MenuController.instance._StickerPowerManager.CheckStartOfBattleStickerPower(StickerPower.CardMastery))
        {
            cardPoints -= 3;
        }

        if (cardTemplate.cardType != CardType.Mystic || cardTemplate.cardType != CardType.Item)
        {
            if(MenuController.instance._StickerPowerManager.CheckStartOfBattleStickerPower(StickerPower.PowerPatron))
            {
                if (pwrAmt > magAmt && pwrAmt > supAmt)
                {
                    if(cardTemplate.cardType == CardType.Action)
                    {
                        cardPoints -= 1;
                        battleSystem.HasPowerPatron = true;
                    }
                }
            }
            if(MenuController.instance._StickerPowerManager.CheckStartOfBattleStickerPower(StickerPower.MagicMonster))
            {
                if (magAmt > pwrAmt && magAmt > supAmt)
                {
                    if(cardTemplate.cardType == CardType.Magic)
                    {
                        cardPoints -= 1;
                        battleSystem.HasMagicMonster = true;
                    }
                }
            }
            if(MenuController.instance._StickerPowerManager.CheckStartOfBattleStickerPower(StickerPower.SupportStar))
            {
                if (supAmt > pwrAmt && supAmt > magAmt)
                {
                    if(cardTemplate.cardType == CardType.Support)
                    {
                        cardPoints -= 1;
                        battleSystem.HasSupportStar = true;
                    }
                }
            }

            if(MenuController.instance._StickerPowerManager.CheckStartOfBattleStickerPower(StickerPower.TheMagician))
            {
                if(supAmt <= 0 && pwrAmt <= 0)
                {
                    if(cardTemplate.cardType == CardType.Magic)
                    {
                        cardPoints -= 2;
                        battleSystem.HasTheMagician = true;
                    }
                }
            }

            if(MenuController.instance._StickerPowerManager.CheckStartOfBattleStickerPower(StickerPower.TheKnight))
            {
                if(magAmt <= 0 && supAmt <= 0)
                {
                    if(cardTemplate.cardType == CardType.Action)
                    {
                        cardStrength += 10;
                        damageValueText.color = Color.green;
                        damageValueText.text = cardStrength.ToString();
                        battleSystem.HasTheKnight = true;
                    }
                }
            }

            if (MenuController.instance._StickerPowerManager.CheckStartOfBattleStickerPower(StickerPower.ThePriest))
            {
                if(pwrAmt <= 0 && magAmt <= 0)
                {
                    if (cardTemplate.cardType == CardType.Support)
                    {
                        if (cardTemplate.cardEffect == CardEffect.HpHeal || cardTemplate.cardEffect == CardEffect.CpHeal)
                        {
                            cardStrength *= 2;
                        }

                        battleSystem.HasThePriest = true;
                    }
                }
            }
        }

        if (cardTemplate.isAFoil)
        {
            if (MenuController.instance._StickerPowerManager.CheckStartOfBattleStickerPower(StickerPower.FoilExpert))
            {
                int halfCost = Mathf.RoundToInt(cardPoints / 2);

                if(halfCost < 1)
                {
                    halfCost = 1;
                }

                cardPoints -= halfCost;
            }
        }

        if(cardPoints < cardTemplate.cardPointCost)
        {
            cardCostText.color = Color.green;
        }

        if (cardPoints <= 0)
        {
            cardPoints = 0;
        }

        cardCostText.text = cardPoints.ToString();
    }

    public void CheckIfCardIsFoil()
    {
        if (cardTemplate.isAFoil)
        {
            cardImageFoil.gameObject.SetActive(true);
        }
    }

    public void DisableCardImage()
    {
        if (cardTemplate.isAFoil)
        {
            cardImageFoil.gameObject.SetActive(false);
            foilBanner.SetActive(false);
        }
    }

    public void EnableCardImage()
    {
        if (cardTemplate.isAFoil)
        {
            cardImageFoil.gameObject.SetActive(true);
            foilBanner.SetActive(true);
        }
    }

    public void CheckStagePenalties()
    {
        if (NodeManager.instance._StagePenalty.Count > 0)
        {
            for (int i = 0; i < NodeManager.instance._StagePenalty.Count; i++)
            {
                switch (NodeManager.instance._StagePenalty[i])
                {
                    case (StagePenalty.Power):
                        if (cardTemplate.cardType == CardType.Action)
                        {
                            forbiddenIcon.SetActive(true);
                            cannotBeUsed = true;
                        }
                        break;
                    case (StagePenalty.Magic):
                        if (cardTemplate.cardType == CardType.Magic)
                        {
                            forbiddenIcon.SetActive(true);
                            cannotBeUsed = true;
                        }
                        break;
                    case (StagePenalty.Support):
                        if (cardTemplate.cardType == CardType.Support)
                        {
                            forbiddenIcon.SetActive(true);
                            cannotBeUsed = true;
                        }
                        break;
                    case (StagePenalty.Mystic):
                        if (cardTemplate.cardType == CardType.Mystic)
                        {
                            forbiddenIcon.SetActive(true);
                            cannotBeUsed = true;
                        }
                        break;
                    case (StagePenalty.Item):
                        if (cardTemplate.cardType == CardType.Item)
                        {
                            forbiddenIcon.SetActive(true);
                            cannotBeUsed = true;
                        }
                        break;
                }
            }
        }
    }

    public void CombinedCardStats()
    {
        if (battleSystem.HasTheKnight)
        {
            cardStrength += 10;
            damageValueText.text = cardStrength.ToString();
            damageValueText.color = Color.green;
        }

        CheckCardType();
        CheckForEnrageStatusEffect();
        ChangeCardStrength(false, battleSystem._battlePlayer.StrengthPercentage);
        comboCards.SetCombinedCardCost();

        if(battleSystem._battlePlayer.HasCourageUnderFire)
        {
            DoubleCardStrength();
        }

        if(battleSystem.HasThePriest)
        {
            if (cardTemplate.cardEffect == CardEffect.HpHeal || cardTemplate.cardEffect == CardEffect.CpHeal)
            {
                cardStrength *= 2;
                healValueText.text = cardStrength.ToString();
                healValueText.color = Color.green;
            }
        }

        SelectCombinedCard();
    }

    public void CheckForEnrageStatusEffect()
    {
        if(MenuController.instance._StickerPowerManager.CheckStartOfBattleStickerPower(StickerPower.Enrage))
        {
            switch(cardTemplate.cardType)
            {
                case (CardType.Support):
                    forbiddenIcon.SetActive(true);
                    cannotBeUsed = true;
                    break;
                case (CardType.Mystic):
                    forbiddenIcon.SetActive(true);
                    cannotBeUsed = true;
                    break;
                case (CardType.Item):
                    forbiddenIcon.SetActive(true);
                    cannotBeUsed = true;
                    break;
                default:
                    cardStrength += 7;
                    damageValueText.text = cardStrength.ToString();
                    damageValueText.color = Color.green;
                    break;
            }
        }
    }

    public void ChangeCardStrength(bool includeSupportCards, float value)
    {
        float percent = value / 100;
        value = Mathf.RoundToInt(cardStrength * percent);

        if (!includeSupportCards)
        {
            if (damageObject.activeSelf)
            {
                cardStrength += (int)value;

                if (cardStrength > cardTemplate.strength)
                {
                    damageValueText.text = cardStrength.ToString();
                    damageValueText.color = Color.green;
                }
                else if (cardStrength < cardTemplate.strength)
                {
                    damageValueText.text = cardStrength.ToString();
                    damageValueText.color = new Color(1, 0.4591194f, 0.4591194f, 1);
                }
                else
                {
                    cardStrength = cardTemplate.strength;
                    damageValueText.text = cardStrength.ToString();
                    damageValueText.color = Color.white;
                }
            }
        }
        else
        {
            if (MenuController.instance._StickerPowerManager.CheckStartOfBattleStickerPower(StickerPower.Enrage))
            {
                value += 7;
            }

            cardStrength += (int)value;

            if (damageObject.activeSelf)
            {
                if (cardStrength > cardTemplate.strength)
                {
                    damageValueText.text = cardStrength.ToString();
                    damageValueText.color = Color.green;
                }
                else if (cardStrength < cardTemplate.strength)
                {
                    damageValueText.text = cardStrength.ToString();
                    damageValueText.color = new Color(1, 0.4591194f, 0.4591194f, 1);
                }
                else
                {
                    cardStrength = cardTemplate.strength;
                    damageValueText.text = cardStrength.ToString();
                    damageValueText.color = Color.white;
                }
            }
            else
            {
                if (cardStrength > cardTemplate.strength)
                {
                    healValueText.text = cardStrength.ToString();
                    healValueText.color = Color.green;
                }
                else if (cardStrength < cardTemplate.strength)
                {
                    healValueText.text = cardStrength.ToString();
                    healValueText.color = new Color(1, 0.4591194f, 0.4591194f, 1);
                }
                else
                {
                    cardStrength = cardTemplate.strength;
                    healValueText.text = cardStrength.ToString();
                    healValueText.color = Color.white;
                }
            }
        }
    }

    public void DoubleCardStrength()
    {
        cardStrength = cardTemplate.strength;

        float percent = (float)battleSystem._battlePlayer.StrengthPercentage / 100;

        int strength = MenuController.instance._StickerPowerManager.CheckStartOfBattleStickerPower(StickerPower.Enrage) ? (cardStrength + 7) * 2 : (cardStrength * 2);

        int strengthBoost = Mathf.RoundToInt(strength * percent);

        int totalStrength = strength + strengthBoost;

        if(damageObject.activeSelf)
        {
            if (strength > cardStrength)
            {
                cardStrength = totalStrength;
                damageValueText.text = totalStrength.ToString();
                damageValueText.color = Color.green;
            }
            else if (strength < cardStrength)
            {
                cardStrength = totalStrength;
                damageValueText.text = totalStrength.ToString();
                damageValueText.color = new Color(1, 0.4591194f, 0.4591194f, 1);
            }
        }
        else
        {
            if (strength > cardStrength)
            {
                cardStrength = totalStrength;
                healValueText.text = totalStrength.ToString();
                healValueText.color = Color.green;
            }
            else if (strength < cardStrength)
            {
                cardStrength = totalStrength;
                healValueText.text = totalStrength.ToString();
                healValueText.color = new Color(1, 0.4591194f, 0.4591194f, 1);
            }
        }
    }

    public void ResetCardStrength()
    {
        cardStrength = battleSystem._battlePlayer.HasCourageUnderFire ? cardTemplate.strength * 2 : cardTemplate.strength;

        float percent = (float)battleSystem._battlePlayer.StrengthPercentage / 100;

        int strength = Mathf.RoundToInt(cardStrength * percent);

        if (MenuController.instance._StickerPowerManager.CheckStartOfBattleStickerPower(StickerPower.Enrage))
        {
            strength += 7;
        }

        int totalStrength = cardStrength + strength;

        if (damageObject.activeSelf)
        {
            if (totalStrength > cardTemplate.strength)
            {
                cardStrength = totalStrength;
                damageValueText.text = totalStrength.ToString();
                damageValueText.color = Color.green;
            }
            else if (totalStrength < cardTemplate.strength)
            {
                cardStrength = totalStrength;
                damageValueText.text = totalStrength.ToString();
                damageValueText.color = new Color(1, 0.4591194f, 0.4591194f, 1);
            }
            else
            {
                cardStrength = totalStrength;
                damageValueText.text = totalStrength.ToString();
                damageValueText.color = Color.white;
            }
        }
        else
        {
            if (totalStrength > cardTemplate.strength)
            {
                cardStrength = totalStrength;
                healValueText.text = totalStrength.ToString();
                healValueText.color = Color.green;
            }
            else if (totalStrength < cardTemplate.strength)
            {
                cardStrength = totalStrength;
                healValueText.text = totalStrength.ToString();
                healValueText.color = new Color(1, 0.4591194f, 0.4591194f, 1);
            }
            else
            {
                cardStrength = totalStrength;
                healValueText.text = totalStrength.ToString();
                healValueText.color = Color.white;
            }
        }
    }

    public void SetHandCardController()
    {
        animator.runtimeAnimatorController = handController;
    }

    public void SetRectTransformAsLastSibling()
    {
        if(gameObject.transform.parent != deckParent && gameObject.transform.parent != graveParent && gameObject.transform.parent != handPanelParent)
        {
            rectTransform.SetAsLastSibling();
        }
    }

    public void CheckCardStatusEffect()
    {
        if (cardStatus != CardStatus.NONE)
        {
            appliesStatusEffect = true;
        }
        else
        {
            appliesStatusEffect = false;
        }

        statusEffectNameText.text = statusEffectName;
        statusEffectDescriptionText.text = cardInfo;
    }

    public void ApplyStatusEffect(bool targetsPlayer, BattleEnemy enemy = null)
    {
        if(targetsPlayer)
        {
            if (battleSystem._battlePlayer.CurrentHealth > 0)
            {
                if (battleSystem._battlePlayer.StatusEffectHolder.transform.childCount > 0)
                {
                    bool hasSameStatus = false;

                    for (int i = 0; i < battleSystem._battlePlayer.StatusEffectHolder.transform.childCount; i++)
                    {
                        StatusEffects statusEffects = battleSystem._battlePlayer.StatusEffectHolder.transform.GetChild(i).GetComponent<StatusEffects>();
                        if (statusEffects._statusEffect == (StatusEffect)cardStatus)
                        {
                            battleSystem._battlePlayer.StatusEffectText.transform.parent.GetComponent<Animator>().Play("StatusText", -1, 0);
                            battleSystem._battlePlayer.StatusEffectText.text = statusEffectName;

                            int effectTime = statusEffectTime;

                            if(MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.Stretch))
                            {
                                effectTime *= 2;
                            }
                            if(battleSystem.HasThePriest)
                            {
                                if(cardTemplate.cardType == CardType.Support)
                                   effectTime *= 2;
                            }

                            if(statusEffects.StatusTime <= effectTime)
                            {
                                statusEffects.StatusTime = effectTime;
                                statusEffects.StatusTimeText.text = effectTime.ToString();
                            }
                            
                            hasSameStatus = true;
                            break;
                        }
                        else
                        {
                            hasSameStatus = false;
                        }
                    }

                    if (!hasSameStatus)
                    {
                        var status = Instantiate(statusEffectPrefab, battleSystem._battlePlayer.StatusEffectHolder.transform);

                        battleSystem._battlePlayer.StatusEffectText.transform.parent.GetComponent<Animator>().Play("StatusText", -1, 0);
                        battleSystem._battlePlayer.StatusEffectText.text = statusEffectName;

                        status.Target = battleSystem._battlePlayer.gameObject;
                        status._statusEffect = (StatusEffect)cardStatus;
                        status.SetStatusParticle();

                        int effectTime = statusEffectTime;

                        if (MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.Stretch))
                        {
                            effectTime *= 2;
                        }
                        if (battleSystem.HasThePriest)
                        {
                            if (cardTemplate.cardType == CardType.Support)
                                effectTime *= 2;
                        }

                        status.StatusTime = effectTime;
                        status.StatusTimeText.text = effectTime.ToString();
                        status.StatusEffectImage.sprite = statusEffectSprite;
                        status.StatusChangePercentage = _cardTemplate.statusBoostPercentage;

                        status.CheckStatusChange();

                        if (shouldCheckStatus)
                        {
                            status.ShouldCheckStatus = true;
                        }
                    }
                }
                else
                {
                    var status = Instantiate(statusEffectPrefab, battleSystem._battlePlayer.StatusEffectHolder.transform);

                    battleSystem._battlePlayer.StatusEffectText.transform.parent.GetComponent<Animator>().Play("StatusText", -1, 0);
                    battleSystem._battlePlayer.StatusEffectText.text = statusEffectName;

                    status.Target = battleSystem._battlePlayer.gameObject;
                    status._statusEffect = (StatusEffect)cardStatus;
                    status.SetStatusParticle();

                    int effectTime = statusEffectTime;

                    if (MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.Stretch))
                    {
                        effectTime *= 2;
                    }
                    if (battleSystem.HasThePriest)
                    {
                        if (cardTemplate.cardType == CardType.Support)
                            effectTime *= 2;
                    }
                    status.StatusTime = effectTime;
                    status.StatusTimeText.text = effectTime.ToString();
                    status.StatusEffectImage.sprite = statusEffectSprite;
                    status.StatusChangePercentage = _cardTemplate.statusBoostPercentage;

                    status.CheckStatusChange();

                    if (shouldCheckStatus)
                    {
                        status.ShouldCheckStatus = true;
                    }
                }
            }
        }
        else
        {
            if (battleSystem.HittingAllEnemies)
            {
                for (int i = 0; i < battleSystem.Enemies.Count; i++)
                {
                    if (battleSystem.Enemies[i].CurrentHealth > 0)
                    {
                        if (battleSystem.Enemies[i].StatusEffectHolder.transform.childCount > 0)
                        {
                            bool hasSameStatus = false;

                            for (int j = 0; j < battleSystem.Enemies[i].StatusEffectHolder.transform.childCount; j++)
                            {
                                StatusEffects statusEffects = battleSystem.Enemies[i].StatusEffectHolder.transform.GetChild(j).GetComponent<StatusEffects>();
                                if (statusEffects._statusEffect == (StatusEffect)cardStatus)
                                {
                                    battleSystem.Enemies[i].StatusEffectText.transform.parent.GetComponent<Animator>().Play("StatusText", -1, 0);
                                    battleSystem.Enemies[i].StatusEffectText.text = statusEffectName;

                                    int effectTime = statusEffectTime;

                                    if(MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.Stretch))
                                    {
                                        effectTime *= 2;
                                    }
                                    if(battleSystem.HasThePriest)
                                    {
                                        effectTime *= 2;
                                    }

                                    if(statusEffects.StatusTime <= effectTime)
                                    {
                                        statusEffects.StatusTime = effectTime;
                                        statusEffects.StatusTimeText.text = effectTime.ToString();
                                    }
                                    
                                    hasSameStatus = true;
                                    break;
                                }
                                else
                                {
                                    hasSameStatus = false;
                                }
                            }

                            if (!hasSameStatus)
                            {
                                battleSystem.Enemies[i].CheckStatusEffectImmunities(cardStatus, statusEffectName, statusEffectChance, statusEffectTime, shouldCheckStatus, statusEffectSprite, _cardTemplate);
                            }
                        }
                        else
                        {
                            if (cardStatus == CardStatus.Paralysis)
                            {
                                int rand = Random.Range(0, 100);

                                if(rand <= statusEffectChance)
                                {
                                    var status = Instantiate(statusEffectPrefab, battleSystem.Enemies[i].StatusEffectHolder.transform);

                                    battleSystem.Enemies[i].StatusEffectText.transform.parent.GetComponent<Animator>().Play("StatusText", -1, 0);
                                    battleSystem.Enemies[i].StatusEffectText.text = statusEffectName;

                                    status.Target = battleSystem.Enemies[i].gameObject;
                                    status._statusEffect = (StatusEffect)cardStatus;
                                    status.SetStatusParticle();

                                    int effectTime = statusEffectTime;

                                    if (MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.Stretch))
                                    {
                                        effectTime *= 2;
                                    }
                                    if (battleSystem.HasThePriest)
                                    {
                                        effectTime *= 2;
                                    }

                                    status.StatusTime = effectTime;
                                    status.StatusTimeText.text = effectTime.ToString();
                                    status.StatusEffectImage.sprite = statusEffectSprite;
                                    status.StatusChangePercentage = _cardTemplate.statusBoostPercentage;

                                    status.CheckStatusChange();

                                    if (shouldCheckStatus)
                                    {
                                        status.ShouldCheckStatus = true;
                                    }
                                }
                            }
                            else
                            {
                                var status = Instantiate(statusEffectPrefab, battleSystem.Enemies[i].StatusEffectHolder.transform);

                                battleSystem.Enemies[i].StatusEffectText.transform.parent.GetComponent<Animator>().Play("StatusText", -1, 0);
                                battleSystem.Enemies[i].StatusEffectText.text = statusEffectName;

                                status.Target = battleSystem.Enemies[i].gameObject;
                                status._statusEffect = (StatusEffect)cardStatus;
                                status.SetStatusParticle();

                                int effectTime = statusEffectTime;

                                if (MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.Stretch))
                                {
                                    effectTime *= 2;
                                }
                                if (battleSystem.HasThePriest)
                                {
                                    effectTime *= 2;
                                }

                                status.StatusTime = effectTime;
                                status.StatusTimeText.text = effectTime.ToString();
                                status.StatusEffectImage.sprite = statusEffectSprite;
                                status.StatusChangePercentage = _cardTemplate.statusBoostPercentage;

                                status.CheckStatusChange();

                                if (shouldCheckStatus)
                                {
                                    status.ShouldCheckStatus = true;
                                }

                                if (status._statusEffect == StatusEffect.Burns || status._statusEffect == StatusEffect.Poison)
                                {
                                    status.transform.SetAsFirstSibling();
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if (enemy.CurrentHealth > 0)
                {
                    if (enemy.StatusEffectHolder.transform.childCount > 0)
                    {
                        bool hasSameStatus = false;

                        for (int i = 0; i < enemy.StatusEffectHolder.transform.childCount; i++)
                        {
                            StatusEffects statusEffects = enemy.StatusEffectHolder.transform.GetChild(i).GetComponent<StatusEffects>();
                            if (statusEffects._statusEffect == (StatusEffect)cardStatus)
                            {
                                enemy.StatusEffectText.transform.parent.GetComponent<Animator>().Play("StatusText", -1, 0);
                                enemy.StatusEffectText.text = statusEffectName;

                                int effectTime = statusEffectTime;

                                if (MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.Stretch))
                                {
                                    effectTime *= 2;
                                }
                                if (battleSystem.HasThePriest)
                                {
                                    effectTime *= 2;
                                }

                                if(statusEffects.StatusTime <= effectTime)
                                {
                                    statusEffects.StatusTime = effectTime;
                                    statusEffects.StatusTimeText.text = effectTime.ToString();
                                }
                                
                                hasSameStatus = true;
                                break;
                            }
                            else
                            {
                                hasSameStatus = false;
                            }
                        }

                        if (!hasSameStatus)
                        {
                            for (int k = 0; k < enemy.StatusEffectHolder.transform.childCount; k++)
                            {
                                StatusEffects statusEffects = enemy.StatusEffectHolder.transform.GetChild(k).GetComponent<StatusEffects>();

                                if (cardStatus == CardStatus.Burns && statusEffects._statusEffect == StatusEffect.BurnsImmune)
                                {
                                    enemy.StatusEffectText.transform.parent.GetComponent<Animator>().Play("StatusText", -1, 0);
                                    enemy.StatusEffectText.text = "IMMUNE";

                                    return;
                                }
                                if (cardStatus == CardStatus.Poison && statusEffects._statusEffect == StatusEffect.PoisonImmune)
                                {
                                    enemy.StatusEffectText.transform.parent.GetComponent<Animator>().Play("StatusText", -1, 0);
                                    enemy.StatusEffectText.text = "IMMUNE";

                                    return;
                                }
                                if (cardStatus == CardStatus.Paralysis && statusEffects._statusEffect == StatusEffect.ParalysisImmune)
                                {
                                    enemy.StatusEffectText.transform.parent.GetComponent<Animator>().Play("StatusText", -1, 0);
                                    enemy.StatusEffectText.text = "IMMUNE";

                                    return;
                                }
                                if (cardStatus == CardStatus.Freeze && statusEffects._statusEffect == StatusEffect.FreezeImmune)
                                {
                                    enemy.StatusEffectText.transform.parent.GetComponent<Animator>().Play("StatusText", -1, 0);
                                    enemy.StatusEffectText.text = "IMMUNE";

                                    return;
                                }
                            }

                            if(cardStatus == CardStatus.Paralysis)
                            {
                                int rand = Random.Range(0, 100);

                                if(rand <= statusEffectChance)
                                {
                                    var status = Instantiate(statusEffectPrefab, enemy.StatusEffectHolder.transform);

                                    enemy.StatusEffectText.transform.parent.GetComponent<Animator>().Play("StatusText", -1, 0);
                                    enemy.StatusEffectText.text = statusEffectName;

                                    status.Target = enemy.gameObject;
                                    status._statusEffect = (StatusEffect)cardStatus;
                                    status.SetStatusParticle();

                                    int effectTime = statusEffectTime;

                                    if (MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.Stretch))
                                    {
                                        effectTime *= 2;
                                    }
                                    if (battleSystem.HasThePriest)
                                    {
                                        effectTime *= 2;
                                    }

                                    status.StatusTime = effectTime;
                                    status.StatusTimeText.text = effectTime.ToString();
                                    status.StatusEffectImage.sprite = statusEffectSprite;
                                    status.StatusChangePercentage = _cardTemplate.statusBoostPercentage;

                                    status.CheckStatusChange();

                                    if (shouldCheckStatus)
                                    {
                                        status.ShouldCheckStatus = true;
                                    }
                                }
                            }
                            else
                            {
                                var status = Instantiate(statusEffectPrefab, enemy.StatusEffectHolder.transform);

                                enemy.StatusEffectText.transform.parent.GetComponent<Animator>().Play("StatusText", -1, 0);
                                enemy.StatusEffectText.text = statusEffectName;

                                status.Target = enemy.gameObject;
                                status._statusEffect = (StatusEffect)cardStatus;
                                status.SetStatusParticle();

                                int effectTime = statusEffectTime;

                                if (MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.Stretch))
                                {
                                    effectTime *= 2;
                                }
                                if (battleSystem.HasThePriest)
                                {
                                    effectTime *= 2;
                                }

                                status.StatusTime = effectTime;
                                status.StatusTimeText.text = effectTime.ToString();
                                status.StatusEffectImage.sprite = statusEffectSprite;
                                status.StatusChangePercentage = _cardTemplate.statusBoostPercentage;

                                status.CheckStatusChange();

                                if (shouldCheckStatus)
                                {
                                    status.ShouldCheckStatus = true;
                                }

                                if (status._statusEffect == StatusEffect.Burns || status._statusEffect == StatusEffect.Poison)
                                {
                                    status.transform.SetAsFirstSibling();
                                }
                            }
                        }
                    }
                    else
                    {
                        if(cardStatus == CardStatus.Paralysis)
                        {
                            int rand = Random.Range(0, 100);

                            if (rand <= statusEffectChance)
                            {
                                var status = Instantiate(statusEffectPrefab, enemy.StatusEffectHolder.transform);

                                enemy.StatusEffectText.transform.parent.GetComponent<Animator>().Play("StatusText", -1, 0);
                                enemy.StatusEffectText.text = statusEffectName;

                                status.Target = enemy.gameObject;
                                status._statusEffect = (StatusEffect)cardStatus;
                                status.SetStatusParticle();

                                int effectTime = statusEffectTime;

                                if (MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.Stretch))
                                {
                                    effectTime *= 2;
                                }
                                if (battleSystem.HasThePriest)
                                {
                                    effectTime *= 2;
                                }

                                status.StatusTime = effectTime;
                                status.StatusTimeText.text = effectTime.ToString();
                                status.StatusEffectImage.sprite = statusEffectSprite;
                                status.StatusChangePercentage = _cardTemplate.statusBoostPercentage;

                                status.CheckStatusChange();

                                if (shouldCheckStatus)
                                {
                                    status.ShouldCheckStatus = true;
                                }
                            }
                        }
                        else
                        {
                            var status = Instantiate(statusEffectPrefab, enemy.StatusEffectHolder.transform);

                            enemy.StatusEffectText.transform.parent.GetComponent<Animator>().Play("StatusText", -1, 0);
                            enemy.StatusEffectText.text = statusEffectName;

                            status.Target = enemy.gameObject;
                            status._statusEffect = (StatusEffect)cardStatus;
                            status.SetStatusParticle();

                            int effectTime = statusEffectTime;

                            if (MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.Stretch))
                            {
                                effectTime *= 2;
                            }
                            if (battleSystem.HasThePriest)
                            {
                                effectTime *= 2;
                            }

                            status.StatusTime = effectTime;
                            status.StatusTimeText.text = effectTime.ToString();
                            status.StatusEffectImage.sprite = statusEffectSprite;
                            status.StatusChangePercentage = _cardTemplate.statusBoostPercentage;

                            status.CheckStatusChange();

                            if (shouldCheckStatus)
                            {
                                status.ShouldCheckStatus = true;
                            }

                            if (status._statusEffect == StatusEffect.Burns || status._statusEffect == StatusEffect.Poison)
                            {
                                status.transform.SetAsFirstSibling();
                            }
                        }
                    }
                }
            }
        }
    }

    public void CardEffects()
    {
        if(cardStatus == CardStatus.NONE)
        {
            appliesStatusEffect = false;

            if (statusEffectPanelAnimator.GetCurrentAnimatorClipInfo(0).Length > 0)
                statusEffectPanelAnimator.Play("Idle");
        }
        else
        {
            appliesStatusEffect = true;

            statusEffectPanelAnimator.Play("StatusEffectPanel", -1, 0);
        }

        statusEffectNameText.text = statusEffectName;
        statusEffectDescriptionText.text = cardTemplate.statusBoostPercentage > 0 ? cardInfo + " " + cardTemplate.statusBoostPercentage + "%" : cardInfo;

        int effectTime = statusEffectTime;

        bool hasExtendedDuration = false;

        if(MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.Stretch))
        {
            effectTime *= 2;

            hasExtendedDuration = true;
        }
        if(battleSystem.HasThePriest)
        {
            if(cardTemplate.cardType == CardType.Support)
            {
                effectTime *= 2;

                hasExtendedDuration = true;
            } 
        }
        statusEffectDurationText.text = hasExtendedDuration ? "Duration: " + "<color=#08FF00>" + effectTime + "</color>" + " turn(s)" : "Duration: " + "<color=#FFFFFF>" + effectTime + "</color>" + " turn(s)";

        if (statusEffectTime > 0)
        {
            statusEffectDurationText.text += ", Success: " + statusEffectChance + "%";
        }
    }

    public void HideStatusEffectPanel()
    {
        if(statusEffectPanelAnimator.GetCurrentAnimatorClipInfo(0).Length > 0)
            statusEffectPanelAnimator.Play("Idle");
    }

    private string CardTarget()
    {
        string target = "";

        switch(cardTemplate.target)
        {
            case (Target.Player):
                target = "\nSelf";
                break;
            case (Target.SingleEnemy):
                target = "Single Enemy";
                break;
            case (Target.AllEnemies):
                target = "\nAll Enemies";
                break;
            case (Target.RandomEnemy):
                target = "\nRandom Enemy";
                break;
        }

        return target;
    }

    private void CheckCardCombos()
    {
        Card card = comboCards.CombinedCard;
        Card hoveredOverCard = battleSystem.ObjectHoveredOver.GetComponent<Card>();

        if(!battleSystem._InputManager.ControllerPluggedIn)
        {
            battleSystem._cardTemplate = this.cardTemplate;
            battleSystem.UsedCard = this;
        }
        else
        {
            if (battleSystem.ObjectHoveredOver != null)
            {
                if (hoveredOverCard.isACombinedCard)
                {
                    if (battleSystem._battlePlayer.CurrentCardPoints >= card.cardTemplate.cardPointCost)
                    {
                        battleSystem._cardTemplate = card.cardTemplate;
                        battleSystem.UsedCard = card;
                    }
                }
                else
                {
                    if (battleSystem._battlePlayer.CurrentCardPoints >= hoveredOverCard.cardTemplate.cardPointCost)
                    {
                        battleSystem._cardTemplate = cardTemplate;
                        battleSystem.UsedCard = hoveredOverCard;
                    }
                }
            }
        }
    }

    public void AddCardToHand(bool grave)
    {
        battleSystem.CardToAddForMysticEffect = this;

        cardButton.onClick.RemoveAllListeners();

        if (grave)
        {
            battleSystem.CloseGraveyard();
        }
        else
        {
            battleSystem.CloseDeck();
        }

        cardButton.GetComponent<ButtonListeners>().AttachListener();

        battleSystem.UsingMysticCard = false;

        hoverParticle.SetActive(false);
    }

    public void BecomeACopy(Card crd)
    {
        crd.cardTemplate = cardTemplate;

        crd.CheckCardType();
        crd.SetCardPoints(battleSystem._BattleDeck.PowerCardAmount, battleSystem._BattleDeck.MagicCardAmount, battleSystem._BattleDeck.SupportCardAmount);
        crd.SetCardStrength();
        crd.ChangeCardStrength(false, battleSystem._battlePlayer.StrengthBoost);
        crd.GetComponent<BattleCardParticleEffect>().SetUpParticle();
        crd.AddStatusEffects();
        crd.CheckCardStatusEffect();
        crd.CheckForEnrageStatusEffect();
        crd.CheckStagePenalties();

        crd.twoComboTemplateName = this.twoComboTemplateName;
        crd.threeComboTemplateName = this.threeComboTemplateName;
        crd.addedSameNamedCardForTwoCardCombo = this.addedSameNamedCardForTwoCardCombo;
        crd.addedSameNamedCardForThreeCardCombo = this.addedSameNamedCardForThreeCardCombo;

        battleSystem.CloseHandPanel();
    }

    public void SelectCard()
    {
        CheckCardCombos();

        if(!cannotBeUsed && !battleSystem.UsingCharity)
        {
            if(battleSystem._cardTemplate != null)
            {
                if(battleSystem._cardTemplate == cardTemplate)
                {
                    if (battleSystem._battlePlayer.CurrentCardPoints >= battleSystem._cardTemplate.cardPointCost)
                    {
                        if (cardTemplate.cardType == CardType.Mystic && cardTemplate.cardStatus == CardStatus.NONE && _cardTemplate.cardEffect != CardEffect.RetainCard)
                        {
                            if(battleSystem._cardTemplate.target == Target.AllEnemies)
                            {
                                CheckLastSelectedGameObject();
                                battleSystem.SetLastSelectedObject(this.gameObject);
                                battleSystem._EventSystem.SetSelectedGameObject(null);
                                battleSystem.CanOnlyHoverOverEnemies = false;
                                battleSystem.HoverOverAllEnemies = true;
                                battleSystem.HittingAllEnemies = true;
                                battleSystem.IsSelectingTarget = true;
                                battleSystem.CanHoverOverTargets = true;
                                if (InputManager.instance.ControllerPluggedIn)
                                {
                                    battleSystem._InputManager._EventSystem.SetSelectedGameObject(null);
                                    battleSystem._InputManager.SetSelectedObject(battleSystem.Enemies[0].gameObject);
                                    battleSystem.ChooseAllEnemyTargets();
                                }
                            }
                            else if(battleSystem._cardTemplate.target == Target.SingleEnemy)
                            {
                                CheckLastSelectedGameObject();
                                battleSystem.SetLastSelectedObject(this.gameObject);
                                battleSystem.CanOnlyHoverOverEnemies = true;
                                battleSystem.HoverOverAllEnemies = false;
                                battleSystem.HittingAllEnemies = false;
                                battleSystem.IsSelectingTarget = true;
                                battleSystem.CanHoverOverTargets = true;
                                if (InputManager.instance.ControllerPluggedIn)
                                {
                                    battleSystem._InputManager._EventSystem.SetSelectedGameObject(null);
                                    battleSystem._InputManager.SetSelectedObject(battleSystem.Enemies[0].gameObject);
                                }
                            }
                            else if(battleSystem._cardTemplate.target == Target.Player)
                            {
                                CheckLastSelectedGameObject();
                                battleSystem.SetLastSelectedObject(this.gameObject);
                                battleSystem.CanOnlyHoverOverEnemies = false;
                                battleSystem.HoverOverAllEnemies = false;
                                battleSystem.HittingAllEnemies = false;
                                battleSystem.IsSelectingTarget = true;
                                battleSystem.CanHoverOverTargets = true;
                                if (InputManager.instance.ControllerPluggedIn)
                                {
                                    battleSystem._InputManager._EventSystem.SetSelectedGameObject(null);
                                    battleSystem._InputManager.SetSelectedObject(battleSystem._battlePlayer.gameObject);
                                }
                            }
                            else
                            {
                                CheckLastSelectedGameObject();
                                battleSystem.SetLastSelectedObject(this.gameObject);
                                battleSystem.CanOnlyHoverOverEnemies = false;
                                battleSystem.HoverOverAllEnemies = false;
                                battleSystem.HittingAllEnemies = false;
                                battleSystem.IsSelectingTarget = false;
                                battleSystem.CanHoverOverTargets = false;

                                battleSystem.ResetHandObjects();

                                mysticCardAbilities.CheckMysticCardEffect();
                            }
                        }
                        else
                        {
                            battleSystem.DisableCharityButton();

                            selectedCardAnimator.Play("SelectedCard");

                            switch (battleSystem._cardTemplate.target)
                            {
                                case (Target.Player):
                                    CheckLastSelectedGameObject();
                                    battleSystem.SetLastSelectedObject(this.gameObject);
                                    battleSystem.CanOnlyHoverOverEnemies = false;
                                    battleSystem.HoverOverAllEnemies = false;
                                    battleSystem.HittingAllEnemies = false;
                                    battleSystem.IsSelectingTarget = true;
                                    battleSystem.CanHoverOverTargets = true;
                                    if(InputManager.instance.ControllerPluggedIn)
                                    {
                                        battleSystem._InputManager._EventSystem.SetSelectedGameObject(null);
                                        battleSystem._InputManager.SetSelectedObject(battleSystem._battlePlayer.gameObject);
                                    }
                                    break;
                                case (Target.SingleEnemy):
                                    CheckLastSelectedGameObject();
                                    battleSystem.SetLastSelectedObject(this.gameObject);
                                    battleSystem.CanOnlyHoverOverEnemies = true;
                                    battleSystem.HoverOverAllEnemies = false;
                                    battleSystem.HittingAllEnemies = false;
                                    battleSystem.IsSelectingTarget = true;
                                    battleSystem.CanHoverOverTargets = true;
                                    if (InputManager.instance.ControllerPluggedIn)
                                    {
                                        battleSystem._InputManager._EventSystem.SetSelectedGameObject(null);
                                        battleSystem._InputManager.SetSelectedObject(battleSystem.Enemies[0].gameObject);
                                    }
                                    break;
                                case (Target.AllEnemies):
                                    CheckLastSelectedGameObject();
                                    battleSystem.SetLastSelectedObject(this.gameObject);
                                    battleSystem._EventSystem.SetSelectedGameObject(null);
                                    battleSystem.CanOnlyHoverOverEnemies = false;
                                    battleSystem.HoverOverAllEnemies = true;
                                    battleSystem.HittingAllEnemies = true;
                                    battleSystem.IsSelectingTarget = true;
                                    battleSystem.CanHoverOverTargets = true;
                                    if(InputManager.instance.ControllerPluggedIn)
                                    {
                                        battleSystem._InputManager._EventSystem.SetSelectedGameObject(null);
                                        battleSystem._InputManager.SetSelectedObject(battleSystem.Enemies[0].gameObject);
                                        battleSystem.ChooseAllEnemyTargets();
                                    }
                                    break;
                                case (Target.RandomEnemy):
                                    CheckLastSelectedGameObject();
                                    battleSystem.SetLastSelectedObject(this.gameObject);
                                    battleSystem._EventSystem.SetSelectedGameObject(null);
                                    battleSystem.CanOnlyHoverOverEnemies = false;
                                    battleSystem.HoverOverAllEnemies = true;
                                    battleSystem.HittingAllEnemies = false;
                                    battleSystem.IsSelectingTarget = true;
                                    battleSystem.CanHoverOverTargets = true;
                                    if (InputManager.instance.ControllerPluggedIn)
                                    {
                                        battleSystem._InputManager._EventSystem.SetSelectedGameObject(null);
                                        battleSystem._InputManager.SetSelectedObject(battleSystem.Enemies[0].gameObject);
                                        battleSystem.ChooseAllEnemyTargets();
                                    }
                                    break;
                            }
                        }
                    }
                    else
                    {
                        battleSystem.ShowBattleMessage("Not enough Card Points!");
                    }
                }
                else
                {
                    battleSystem.ShowBattleMessage("Not enough Card Points!");
                }
            }
            else
            {
                battleSystem.ShowBattleMessage("Not enough Card Points!");
            }
        }
        else if(battleSystem.UsingCharity)
        {
            if(!chosenForCharity)
            {
                battleSystem.CharityCards.Add(this);

                charityCheckMark.SetActive(true);

                chosenForCharity = true;

                battleSystem.CheckCardsChosenForCharity();
            }
            else
            {
                battleSystem.CharityCards.Remove(this);

                charityCheckMark.SetActive(false);

                chosenForCharity = false;

                battleSystem.CheckCardsChosenForCharity();
            }
        }
        else
        {
            battleSystem.ShowBattleMessage("This card cannot be used!");
        }
    }

    private void CheckLastSelectedGameObject()
    {
        if(battleSystem.LastSelectedObject != this.gameObject)
        {
            battleSystem.DeselectDefaultAttack();

            if(battleSystem.LastSelectedObject.GetComponent<Card>())
               battleSystem.LastSelectedObject.GetComponent<Card>().selectedCardAnimator.Play("Idle");
        }
    }

    public void PlayHoverAnimation()
    {
        if(!battleSystem.UsingMysticCard)
        {
            if (this.gameObject.transform.parent != deckParent && this.gameObject.transform.parent != graveParent && this.gameObject.transform.parent != handPanelParent)
            {
                if (cardButton.interactable)
                {
                    audioSource.clip = hoveredOverAudioClip;
                    if (PlayerPrefs.HasKey("SoundEffects"))
                    {
                        audioSource.volume = PlayerPrefs.GetFloat("SoundEffects");
                        audioSource.Play();
                    }
                    else
                    {
                        audioSource.Play();
                    }
                }

                if (cardTemplate.cardType == CardType.Item)
                {
                    if (battleSystem.ObjectHoveredOver == this.gameObject)
                        animator.Play("HoveredCardItem");
                }
                else
                {
                    if(InputManager.instance.ControllerPluggedIn)
                    {
                        if (battleSystem.ObjectHoveredOver == this.gameObject)
                            animator.Play("HoveredCard");
                    }
                    else
                    {
                        if (battleSystem.ObjectHoveredOver == this.gameObject && !this.chosenForCombining)
                            animator.Play("HoveredCard");
                    }
                }

                if (cardStatus != CardStatus.NONE)
                {
                    statusEffectPanelAnimatorHover.Play("StatusEffectPanel", -1, 0);

                    int effectTime = statusEffectTime;

                    bool hasExtendedDuration = false;

                    if (MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.Stretch))
                    {
                        effectTime *= 2;

                        hasExtendedDuration = true;
                    }
                    if (battleSystem.HasThePriest)
                    {
                        if (cardTemplate.cardType == CardType.Support)
                        {
                            effectTime *= 2;

                            hasExtendedDuration = true;
                        }
                    }

                    statusEffectNameTextHover.text = statusEffectName;
                    statusEffectDescriptionTextHover.text = cardTemplate.statusBoostPercentage > 0 ? cardInfo + " " + cardTemplate.statusBoostPercentage + "%" : cardInfo;
                    statusEffectDurationTextHover.text = hasExtendedDuration ? "Duration: " + "<color=#08FF00>" + effectTime + "</color>" + " turn(s)" : "Duration: " + "<color=#FFFFFF>" + 
                                                         effectTime + "</color>" + " turn(s)";

                    if (statusEffectChance > 0)
                    {
                        statusEffectDurationTextHover.text += ", Success: " + statusEffectChance + "%";
                    }
                }
            }
        }
        

        if(this.gameObject.transform.parent == deckParent || this.gameObject.transform.parent == graveParent || this.gameObject.transform.parent == handPanelParent)
        {
            if(battleSystem.UsingMysticCard)
            {
                hoverParticle.SetActive(true);
            }
        }
    }

    public void ControllerSelectCard()
    {
        if(InputManager.instance.ControllerPluggedIn)
        {
            battleSystem.SetObjectHoveredOver(this);

            PlayHoverAnimation();
        }
    }

    public void ControllerDeselectCard()
    {
        if(InputManager.instance.ControllerPluggedIn)
        {
            battleSystem.SetSelectedObjectToNull();

            if (!battleSystem.UsingMysticCard)
            {
                if (cardTemplate.cardType == CardType.Item)
                {
                    animator.Play("ReverseHoveredItem");
                }
                else
                {
                    animator.Play("ReverseHover");
                }

                if (cardStatus != CardStatus.NONE)
                {
                    if (statusEffectPanelAnimatorHover.GetCurrentAnimatorStateInfo(0).IsName("StatusEffectPanel"))
                        statusEffectPanelAnimatorHover.Play("Reverse", -1, 0);
                }
            }

            rectTransform.SetAsLastSibling();
        }
    }

    public void ControllerSelectCardDeckItem()
    {
        if(InputManager.instance.ControllerPluggedIn)
        {
            if(transform.parent != deckParent && transform.parent != graveParent && transform.parent != handPanelParent)
            {
                battleSystem.SetObjectHoveredOver(this);
                battleSystem.SetUsedCard(this);

                PlayHoverAnimation();

                rectTransform.SetAsLastSibling();
            }
        }
    }

    public void ControllerCombinedCardSelect()
    {
        if (InputManager.instance.ControllerPluggedIn)
        {
            battleSystem.SetObjectHoveredOver(this);
            battleSystem.SetUsedCard(this);
            battleSystem._EventSystem.firstSelectedGameObject = gameObject;
        }
    }

    public void ControllerCombinedCardDeselect()
    {
        if(InputManager.instance.ControllerPluggedIn)
        {
            battleSystem.SetObjectHoveredOverToNull();
        }
    }

    public void ControllerDeselectCardDeckItem(bool playStatusPanelAnimation)
    {
        if(InputManager.instance.ControllerPluggedIn)
        {
            if (transform.parent != deckParent && transform.parent != graveParent && transform.parent != handPanelParent)
                battleSystem.SetSelectedObjectToNull();

            if(!battleSystem.UsingMysticCard)
            {
                if (cardTemplate.cardType == CardType.Item)
                {
                    animator.Play("ReverseHoveredItem");
                }
                else
                {
                    if(transform.parent != deckParent && transform.parent != graveParent && transform.parent != handPanelParent)
                       animator.Play("ReverseHover");
                }

                if (cardStatus != CardStatus.NONE)
                {
                    if(transform.parent != deckParent && transform.parent != graveParent && transform.parent != handPanelParent)
                    {
                        if(playStatusPanelAnimation)
                        {
                            if(statusEffectPanelAnimatorHover.GetCurrentAnimatorClipInfo(0).Length > 0)
                               statusEffectPanelAnimatorHover.Play("Reverse", -1, 0);
                        }
                    }
                }
            }
        }
    }

    public void PlayReverseHoverAnimation()
    {
        if(!battleSystem.UsingMysticCard)
        {
            if (this.gameObject.transform.parent != deckParent && this.gameObject.transform.parent != graveParent && this.gameObject.transform.parent != handPanelParent)
            {
                if (cardTemplate.cardType == CardType.Item)
                {
                    if (battleSystem.ObjectHoveredOver != this.gameObject)
                        animator.Play("ReverseHoveredItem");

                    if (cardTemplate.cardStatus != CardStatus.NONE)
                    {
                        if (statusEffectPanelAnimatorHover.GetCurrentAnimatorClipInfo(0).Length > 0)
                            statusEffectPanelAnimatorHover.Play("Reverse", -1, 0);
                    }
                }
                else
                {
                    if (battleSystem.ObjectHoveredOver != this.gameObject && !this.chosenForCombining)
                        animator.Play("ReverseHover");

                    if (cardStatus != CardStatus.NONE)
                    {
                        if (statusEffectPanelAnimatorHover.GetCurrentAnimatorClipInfo(0).Length > 0)
                            statusEffectPanelAnimatorHover.Play("Reverse", -1, 0);
                    }
                }
            }
        }
        

        if(this.gameObject.transform.parent == deckParent || this.gameObject.transform.parent == graveParent || this.gameObject.transform.parent == handPanelParent)
        {
            if(battleSystem.UsingMysticCard)
            {
                hoverParticle.SetActive(false);
            }
        }
    }

    public void DefaultColorFrame()
    {
        combinationParticle.Stop();
        combinationParticle.gameObject.SetActive(false);
        combineFrame.gameObject.SetActive(false);

        if(cardTemplate.cardType != CardType.Mystic)
           canCombineWithOtherCards = true;
    }

    public void AbleToCombineColorFrame()
    {
        combineFrame.gameObject.SetActive(true);

        combinationParticle.Stop();
        combinationChildParticle.Stop();

        var startClr = combinationParticle.main;
        startClr.startColor = ableToCombineColor;

        var startClrChild = combinationChildParticle.main;
        startClrChild.startColor = ableToCombineColor;

        combinationParticle.gameObject.SetActive(true);

        if (cardTemplate.cardType != CardType.Mystic)
            canCombineWithOtherCards = true;

        combinationParticle.Play();
        combinationChildParticle.Play();
    }

    public void UnableToCombineColorFrame()
    {
        combineFrame.gameObject.SetActive(true);

        combinationParticle.Stop();
        combinationChildParticle.Stop();

        var startClr = combinationParticle.main;
        startClr.startColor = unableToCombineColor;

        var startClrChild = combinationChildParticle.main;
        startClrChild.startColor = unableToCombineColor;

        combinationParticle.gameObject.SetActive(true);

        canCombineWithOtherCards = false;

        combinationParticle.Play();
        combinationChildParticle.Play();
    }

    public void PlayCardParticleController()
    {
        if(InputManager.instance.ControllerPluggedIn)
        {
            if (transform.parent == deckParent || transform.parent == graveParent || transform.parent == handPanelParent)
            {
                combineFrame.gameObject.SetActive(true);

                combinationParticle.Stop();
                combinationChildParticle.Stop();

                var startClr = combinationParticle.main;
                startClr.startColor = ableToCombineColor;

                var startClrChild = combinationChildParticle.main;
                startClrChild.startColor = ableToCombineColor;

                combinationParticle.gameObject.SetActive(true);

                if (cardTemplate.cardType != CardType.Mystic)
                    canCombineWithOtherCards = true;

                combinationParticle.Play();
                combinationChildParticle.Play();

                AudioManager.instance.PlaySoundEffect(AudioManager.instance.CursorAudio);
            }
        }
    }

    public void StopParticleController()
    {
        if(InputManager.instance.ControllerPluggedIn)
        {
            if (transform.parent == deckParent || transform.parent == graveParent || transform.parent == handPanelParent)
            {
                combinationParticle.gameObject.SetActive(false);
                combinationChildParticle.gameObject.SetActive(false);
            }
        }
    }

    public void PlayCombinedCardParticleController(ParticleSystem particle)
    {
        if (InputManager.instance.ControllerPluggedIn)
        {
            particle.gameObject.SetActive(true);
            particle.Play();

            AudioManager.instance.PlaySoundEffect(AudioManager.instance.CursorAudio);
        }
    }

    public void StopCombinedCardParticleController(ParticleSystem particle)
    {
        if (InputManager.instance.ControllerPluggedIn)
        {
            particle.Stop();
            particle.gameObject.SetActive(false);
        }
    }

    public void StopParticle()
    {
        combinationParticle.gameObject.SetActive(false);
        combinationChildParticle.gameObject.SetActive(false);
    }

    public void ChangeCard()
    {
        cardButton.interactable = true;

        battleSystem.ResetSelectedCardAnimation();

        if(battleSystem._InputManager.ControllerPluggedIn)
        {
            battleSystem.DeselectTargets(true);

            selectedCardAnimator.Play("Idle");
        }

        switch(comboCards.CurrentSelectedCards)
        {
            case (2):
                TwoComboColor();
                break;
            case (3):
                ThreeCardComboColor();
                break;
        }
    }

    public void SelectCombinedCard()
    {
        if (!battleSystem._InputManager.ControllerPluggedIn)
        {
            if(battleSystem._battlePlayer.CurrentCardPoints >= comboCards.CombinedCard.cardPoints)
            {
                comboCards.CombinedCard.CardButton.onClick.Invoke();

                battleSystem.SetUsedCard(comboCards.CombinedCard);
                battleSystem._EventSystem.firstSelectedGameObject = gameObject;
            }
            else
            {
                battleSystem.DeselectTargets(true);

                comboCards.CombinedCard.SelectedCardAnimator.Play("Idle");
            }
        }
    }

    public void TwoComboColor()
    {
        combinationParticle.gameObject.SetActive(true);
        combinationChildParticle.gameObject.SetActive(true);

        combinationParticle.Stop();
        combinationChildParticle.Stop();

        ParticleSystem.MainModule pMain = combinationChildParticle.main;

        pMain.startLifetime = 0.5f;

        var startClr = combinationParticle.main;
        startClr.startColor = twoCardComboColor;

        var startClrChild = combinationChildParticle.main;
        startClrChild.startColor = twoCardComboColor;

        combinationParticle.Play();
        combinationChildParticle.Play();

        comboCards.CombinedThreeCards = false;

        if(comboCards.CurrentSelectedCards > 1)
           CardEffects();
    }

    public void ThreeCardComboColor()
    {
        combinationParticle.gameObject.SetActive(true);
        combinationChildParticle.gameObject.SetActive(true);

        combinationParticle.Stop();
        combinationChildParticle.Stop();

        ParticleSystem.MainModule pMain = combinationChildParticle.main;

        pMain.startLifetime = new ParticleSystem.MinMaxCurve(2f, 2.5f);

        var startClr = combinationParticle.main;
        startClr.startColor = threeCardComboColor;

        var startClrChild = combinationChildParticle.main;
        startClrChild.startColor = threeCardComboColor;

        combinationParticle.Play();
        combinationChildParticle.Play();

        CardEffects();
    }

    public void ResetChildLifeTime()
    {
        TwoComboColor();
    }

    private void CheckCurrentScene()
    {
        cardButton.onClick.RemoveAllListeners();

        cardButton.onClick.AddListener(SelectCard);
    }

    private void FadeOutText()
    {
        cardNameText.GetComponent<Animator>().Play("FadeOut");
        cardCostText.GetComponent<Animator>().Play("FadeOut");
        cardInformation.GetComponent<Animator>().Play("FadeOut");

        if(damageValueText.gameObject.activeInHierarchy)
        {
            damageValueText.GetComponent<Animator>().Play("FadeOut");
        }
        if(healValueText.gameObject.activeInHierarchy)
        {
            healValueText.GetComponent<Animator>().Play("FadeOut");
        }
    }

    private void FadeInText()
    {
        cardNameText.GetComponent<Animator>().Play("FadeIn");
        cardCostText.GetComponent<Animator>().Play("FadeIn");
        cardInformation.GetComponent<Animator>().Play("FadeIn");

        if (damageValueText.gameObject.activeInHierarchy)
        {
            damageValueText.GetComponent<Animator>().Play("FadeIn");
        }
        if (healValueText.gameObject.activeInHierarchy)
        {
            healValueText.GetComponent<Animator>().Play("FadeIn");
        }
    }

    public void RegenerateCard()
    {
        FadeInText();

        audioSource.clip = cardSelectedAudioClip;

        cardCostObject.GetComponent<Coffee.UIEffects.UIDissolve>().effectFactor = 0;
        damageObject.GetComponent<Coffee.UIEffects.UIDissolve>().effectFactor = 0;
        healObject.GetComponent<Coffee.UIEffects.UIDissolve>().effectFactor = 0;
        cardTypeImage.GetComponent<Coffee.UIEffects.UIDissolve>().effectFactor = 0;
        maskImage.GetComponent<Coffee.UIEffects.UIDissolve>().effectFactor = 0;
        cardObject.GetComponent<Coffee.UIEffects.UIDissolve>().effectFactor = 0;

        cardCostObject.GetComponent<Coffee.UIEffects.UIDissolve>().enabled = false;
        damageObject.GetComponent<Coffee.UIEffects.UIDissolve>().enabled = false;
        healObject.GetComponent<Coffee.UIEffects.UIDissolve>().enabled = false;
        cardTypeImage.GetComponent<Coffee.UIEffects.UIDissolve>().enabled = false;
        maskImage.GetComponent<Coffee.UIEffects.UIDissolve>().enabled = false;
        cardPortrait.GetComponent<Coffee.UIEffects.UIDissolve>().enabled = false;
        cardObject.GetComponent<Coffee.UIEffects.UIDissolve>().enabled = false;

        EnableCardImage();
    }

    public void DissolveCard()
    {
        FadeOutText();

        if (PlayerPrefs.HasKey("SoundEffects"))
        {
            GetComponent<AudioSource>().clip = AudioManager.instance.CardDissolveAudio;
            GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SoundEffects");
            GetComponent<AudioSource>().Play();
        }
        else
        {
            GetComponent<AudioSource>().clip = AudioManager.instance.CardDissolveAudio;
            GetComponent<AudioSource>().Play();
        }

        cardCostObject.GetComponent<Coffee.UIEffects.UIDissolve>().enabled = true;
        damageObject.GetComponent<Coffee.UIEffects.UIDissolve>().enabled = true;
        healObject.GetComponent<Coffee.UIEffects.UIDissolve>().enabled = true;
        cardTypeImage.GetComponent<Coffee.UIEffects.UIDissolve>().enabled = true;
        maskImage.GetComponent<Coffee.UIEffects.UIDissolve>().enabled = true;
        cardPortrait.GetComponent<Coffee.UIEffects.UIDissolve>().enabled = true;
        cardObject.GetComponent<Coffee.UIEffects.UIDissolve>().enabled = true;

        cardCostObject.GetComponent<Coffee.UIEffects.UIDissolve>().Play();
        damageObject.GetComponent<Coffee.UIEffects.UIDissolve>().Play();
        healObject.GetComponent<Coffee.UIEffects.UIDissolve>().Play();
        cardTypeImage.GetComponent<Coffee.UIEffects.UIDissolve>().Play();
        maskImage.GetComponent<Coffee.UIEffects.UIDissolve>().Play();
        cardPortrait.GetComponent<Coffee.UIEffects.UIDissolve>().Play();
        cardObject.GetComponent<Coffee.UIEffects.UIDissolve>().Play();
    }
}