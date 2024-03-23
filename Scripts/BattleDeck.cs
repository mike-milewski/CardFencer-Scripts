using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BattleDeck : MonoBehaviour
{
    [SerializeField]
    private PlayerMenuInfo playerMenuInfo;

    [SerializeField]
    private Deck deck;

    [SerializeField]
    private Card battleCard;

    [SerializeField]
    private MainCharacterStats mainCharacterStats;

    [SerializeField]
    private ComboCards comboCards;

    [SerializeField]
    private List<Card> card = new List<Card>();

    [SerializeField]
    private List<Card> cardsInHand = new List<Card>();

    [SerializeField]
    private RectTransform[] cardPositions;

    [SerializeField]
    private BattleSystem battleSystem;

    [SerializeField]
    private Graveyard graveyard;

    [SerializeField]
    private Button deckCloseButton;

    [SerializeField]
    private Transform deckParent, drawPositionParent;

    [SerializeField]
    private TextMeshProUGUI deckAmountText;

    [SerializeField]
    private bool drawingCards;

    [SerializeField]
    [Header("Deck Info")]
    private int currentCardsInDeck;

    [SerializeField]
    [Header("Hand Info")]
    private int currentHandSize;
    [SerializeField]
    private int maxHandSize;
    [SerializeField]
    private int handSizeLimit;

    [SerializeField]
    private int cardsToDrawForCharity;

    private int powerCardAmount, magicCardAmount, supportCardAmount;

    private bool usedDeckedOutPower;

    public List<Card> _Card
    {
        get
        {
            return card;
        }
        set
        {
            card = value;
        }
    }

    public List<Card> CardsInHand
    {
        get
        {
            return cardsInHand;
        }
        set
        {
            cardsInHand = value;
        }
    }

    public Transform DeckParent => deckParent;

    public int MaxHandSize => maxHandSize;

    public int PowerCardAmount => powerCardAmount;

    public int MagicCardAmount => magicCardAmount;

    public int SupportCardAmount => supportCardAmount;

    public int CardsToDrawForCharity
    {
        get
        {
            return cardsToDrawForCharity;
        }
        set
        {
            cardsToDrawForCharity = value;
        }
    }

    public int CurrentHandSize
    {
        get
        {
            return currentHandSize;
        }
        set
        {
            currentHandSize = value;
        }
    }

    public bool DrawingCards
    {
        get
        {
            return drawingCards;
        }
        set
        {
            drawingCards = value;
        }
    }

    private void Start()
    {
        CreateBattleCards();

        maxHandSize = playerMenuInfo.currentHandSize;
    }

    public void Draw(int cardsToDraw)
    {
        if(cardsToDraw > 0)
        {
            if (cardsToDrawForCharity < cardsToDraw && battleSystem.Enemies.Count > 0)
            {
                cardsToDrawForCharity++;

                if (card.Count <= 0)
                {
                    if (MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.DeckedOut))
                    {
                        if (!usedDeckedOutPower)
                        {
                            usedDeckedOutPower = true;

                            battleSystem._StickerPowerHolder.CreateStickerMessage(StickerPower.DeckedOut);

                            battleSystem.DealDamageToEachEnemy(0.10f);
                        }
                    }

                    ReshuffleGraveyardIntoDeck();
                }
                else
                {
                    WaitToDraw(true);
                }
            }
        }
        else
        {
            if (currentHandSize < maxHandSize && battleSystem.Enemies.Count > 0)
            {
                if (card.Count <= 0)
                {
                    if (MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.DeckedOut))
                    {
                        if (!usedDeckedOutPower)
                        {
                            usedDeckedOutPower = true;

                            battleSystem._StickerPowerHolder.CreateStickerMessage(StickerPower.DeckedOut);

                            battleSystem.DealDamageToEachEnemy(0.10f);
                        }
                    }

                    ReshuffleGraveyardIntoDeck();
                }
                else
                {
                    WaitToDraw(true);
                }
            }
        }
    }

    private void CreateBattleCards()
    {
        foreach (CardTemplate template in mainCharacterStats.currentCards)
        {
            var battleC = Instantiate(battleCard, deckParent);

            battleC.gameObject.SetActive(true);

            battleC._cardTemplate = template;

            battleC.CheckCardType();
            battleC.CheckForEnrageStatusEffect();
            battleC.CheckStagePenalties();
            battleC.AddStatusEffects();
            battleC.CheckCardStatusEffect();

            battleC.name = template.cardName;

            card.Add(battleC);
        }
        comboCards.AddCardCombos();

        RandomizeCards();
        UpdateDeckAmount();
        CheckCardTypesInList();
    }

    private void CheckCardTypesInList()
    {
        powerCardAmount = 0;
        magicCardAmount = 0;
        supportCardAmount = 0;

        for (int i = 0; i < card.Count; i++)
        {
            var battleC = card[i].GetComponent<Card>();

            switch(battleC._cardTemplate.cardType)
            {
                case CardType.Action:
                    powerCardAmount++;
                    break;
                case CardType.Magic:
                    magicCardAmount++;
                    break;
                case CardType.Support:
                    supportCardAmount++;
                    break;
            }
        }

        for (int i = 0; i < card.Count; i++)
        {
            var battleC = card[i].GetComponent<Card>();

            battleC.SetCardPoints(powerCardAmount, magicCardAmount, supportCardAmount);
        }
    }

    public void AddListenerToDeckCards()
    {
        foreach (Card crd in card)
        {
            crd.CardButton.onClick.RemoveAllListeners();
            crd.CardButton.onClick.AddListener(() => crd.AddCardToHand(false));

            crd.CardButton.interactable = true;
        }
        deckCloseButton.onClick.AddListener(RemoveMysticCardListener);
    }

    private void RemoveMysticCardListener()
    {
        foreach (Card card in card)
        {
            card.CardButton.interactable = false;

            card.CardButton.onClick.RemoveAllListeners();
            card.CardButton.onClick.AddListener(card.SelectCard);
        }
        battleSystem.UsedCard.SelectedCardAnimator.Play("Idle", -1, 0);
        battleSystem.DeselectTargets(true);
        battleSystem.UsingMysticCard = false;

        battleSystem.EnableCharityButton();

        battleSystem.CardToAddForMysticEffect = null;

        deckCloseButton.onClick.RemoveAllListeners();
    }

    public void RemoveCardFromHand(Card handCard)
    {
        cardsInHand.Remove(handCard);
        AdjustCardPositionsInHand();
    }

    public void AdjustCardPositionsInHand()
    {
        if(gameObject != null)
        {
            if (cardsInHand.Count > 0)
            {
                for (int i = 0; i < cardsInHand.Count; i++)
                {
                    cardsInHand[i].GetComponent<CardMovement>().TargetPosition = cardPositions[i];

                    cardsInHand[i].CardPositionIndex = i;

                    cardsInHand[i].GetComponent<CardMovement>().StartCoroutine(cardsInHand[i].GetComponent<CardMovement>().MoveToPosition(false));
                }
            }
        }
    }

    private void RandomizeCards()
    {
        for (int i = 0; i < card.Count; i++)
        {
            Card temp = card[i];
            int randomIndex = Random.Range(i, card.Count);
            card[i] = card[randomIndex];
            card[randomIndex] = temp;
        }
    }

    public void SetCardToTopOfTheDeck(Card crd)
    {
        graveyard.cards.Remove(crd);
        graveyard.UpdateGraveyardCardCount();

        card.Insert(0, crd);

        battleSystem._InputManager.SetSelectedObject(null);

        SetCardToHand(false);

        if (graveyard.cards.Count > 0)
        {
            foreach (Card card in graveyard.cards)
            {
                card.CardButton.interactable = false;
            }
        }
    }

    public void AddCardFromDeckToHand(Card crd)
    {
        card.Remove(crd);
        card.Insert(0, crd);

        SetCardToHand(true);

        RandomizeCards();

        if (graveyard.cards.Count > 0)
        {
            foreach (Card cards in graveyard.cards)
            {
                cards.CardButton.interactable = false;
            }
        }
    }

    private void ReshuffleGraveyardIntoDeck()
    {
        foreach (Card cards in graveyard.cards)
        {
            card.Add(cards);

            ReparentCardHolder();
        }
        RandomizeCards();

        graveyard.cards.Clear();
        graveyard.UpdateGraveyardCardCount();

        UpdateDeckAmount();
        WaitToDraw(true);

        usedDeckedOutPower = false;
    }

    private void ReparentCardHolder()
    {
        foreach (Card c in graveyard.GraveyardHolder.GetComponentsInChildren<Card>())
        {
            c.transform.SetParent(deckParent.transform);
            c.CardButton.interactable = false;
            c._Animator.enabled = false;
            c._PorpogateDrag.scrollView = battleSystem.DeckScroll;
        }
    }

    private void ReparentCardHolderDeck()
    {
        foreach (Card c in battleSystem.CardHolder.GetComponentsInChildren<Card>())
        {
            c.transform.SetParent(deckParent.transform);
            c.CardButton.interactable = false;
            c._Animator.enabled = false;
            c._PorpogateDrag.scrollView = battleSystem.DeckScroll;
        }
    }

    public void ResetCards()
    {
        foreach (Card c in card)
        {
            c.CardButton.interactable = false;
            c._Animator.enabled = false;

            c.CardButton.onClick.RemoveAllListeners();
            c.CardButton.onClick.AddListener(c.SelectCard);
        }
        deckCloseButton.onClick.RemoveAllListeners();
    }

    private void WaitToDraw(bool decrementDeck)
    {
        SetCardToDrawPosition();

        if (decrementDeck)
            currentCardsInDeck--;

        AudioManager.instance.PlaySoundEffect(AudioManager.instance.CardAudio);

        cardsInHand.Add(card[0]);

        AdjustCardPositionsInHand();

        card[0].SetHandCardController();

        card[0].GetComponent<CardMovement>().StartCoroutine(card[0].GetComponent<CardMovement>().MoveToPosition(true));
        card.RemoveAt(0);

        UpdateDeckAmount();

        if(MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.Happiness))
        {
            AudioManager.instance.PlaySoundEffect(AudioManager.instance.HpHealAudio);

            var hpParticle = battleSystem._battlePlayer.HpRegenParticle;

            hpParticle.gameObject.SetActive(true);
            hpParticle.transform.position = new Vector3(battleSystem._battlePlayer.transform.position.x, battleSystem._battlePlayer.transform.position.y + 0.3f, battleSystem._battlePlayer.transform.position.z);
            hpParticle.Play();

            if(battleSystem._battlePlayer.IsUndead)
            {
                battleSystem._battlePlayer.TakeDamage(1, true, false);
            }
            else
            {
                battleSystem._battlePlayer.HealHealth(1, true);
            }
        }
    }

    private void SetCardToHand(bool decrementDeck)
    {
        SetCardToDrawPosition();

        if (decrementDeck)
            currentCardsInDeck--;

        AudioManager.instance.PlaySoundEffect(AudioManager.instance.CardAudio);

        cardsInHand.Add(card[0]);

        AdjustCardPositionsInHand();

        card[0].SetHandCardController();

        card[0].GetComponent<CardMovement>().StartCoroutine(card[0].GetComponent<CardMovement>().SetToPosition());
        card.RemoveAt(0);

        UpdateDeckAmount();
    }

    private void SetCardToDrawPosition()
    {
        if(card[0] != null)
        {
            card[0].transform.SetParent(drawPositionParent);

            RectTransform rectTrans = card[0].GetComponent<RectTransform>();

            rectTrans.anchorMax = new Vector2(0.5f, 0.5f);
            rectTrans.anchorMin = new Vector2(0.5f, 0.5f);

            rectTrans.anchoredPosition = new Vector2(-10, 0);
        }
    }

    private void UpdateDeckAmount()
    {
        currentCardsInDeck = card.Count;

        deckAmountText.text = currentCardsInDeck.ToString();
    }

    public void CheckCardsInHand()
    {
        if(battleSystem.UsingCharity)
        {
            if(cardsToDrawForCharity >= 2)
            {
                drawingCards = false;
                battleSystem.EnableBattleUIInteracteability();
                battleSystem._EventSystem.sendNavigationEvents = true;
                battleSystem.NavagationDisabled = false;
                battleSystem.SetInitialBattleUINavigations();
                battleSystem._InputManager.SetSelectedObject(battleSystem.DefaultAttackAnimator.gameObject);
                battleSystem.CheckCharityButtonNavigation();
                battleSystem.AdjustNavigationsForPlayerAndEnemyStatusEffects();

                battleSystem.UsingCharity = false;
                cardsToDrawForCharity = 0;
            }
            else
            {
                Draw(2);
            }
        }
        else
        {
            if (currentHandSize >= maxHandSize)
            {
                cardsToDrawForCharity = 0;
                drawingCards = false;
                battleSystem.PlayerTurn();
            }
            else
            {
                Draw(0);
            }
        }
    }

    public void ContinuePlayersTurn()
    {
        battleSystem.ContinuePlayerTurn();
    }

    public void ClearCards()
    {
        currentHandSize = 0;

        foreach (Card cards in cardsInHand)
        {
            card.Add(cards);

            cards.DisableCardImage();
            cards.DissolveCard();
        }

        StartCoroutine(WaitToRedrawCards());
    }

    private IEnumerator WaitToRedrawCards()
    {
        yield return new WaitForSeconds(1f);

        ReparentCardHolderDeck();

        foreach (Card cards in cardsInHand)
        {
            cards.RegenerateCard();
        }

        cardsInHand.Clear();

        RandomizeCards();

        UpdateDeckAmount();
        WaitToDraw(true);
    }
}