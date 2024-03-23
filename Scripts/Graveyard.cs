using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Graveyard : MonoBehaviour
{
    [SerializeField]
    private List<Card> card = new List<Card>();

    [SerializeField]
    private BattleSystem battleSystem;

    [SerializeField]
    private Button closeButton;

    [SerializeField]
    private Transform graveyardHolder;

    [SerializeField]
    private TextMeshProUGUI graveyardCardAmountText;

    private int cardsInGraveyard;

    public List<Card> cards
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

    public Transform GraveyardHolder => graveyardHolder;

    public Button CloseButton => closeButton;

    public void UpdateGraveyardCardCount()
    {
        cardsInGraveyard = card.Count;

        graveyardCardAmountText.text = cardsInGraveyard.ToString();
    }

    public void AddListenerToGraveCards()
    {
        foreach (Card crd in cards)
        {
            crd.CardButton.onClick.RemoveAllListeners();
            crd.CardButton.onClick.AddListener(() => crd.AddCardToHand(true));

            crd.CardButton.interactable = true;
        }
        closeButton.onClick.AddListener(RemoveMysticCardListener);
    }

    private void RemoveMysticCardListener()
    {
        foreach (Card card in cards)
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
        battleSystem.UsedCard = null;

        closeButton.onClick.RemoveAllListeners();
    }

    public void ResetCards()
    {
        foreach(Card card in cards)
        {
            card.CardButton.onClick.RemoveAllListeners();
            card.CardButton.onClick.AddListener(card.SelectCard);
        }
    }
}