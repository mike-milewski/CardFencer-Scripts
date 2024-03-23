using System.Collections;
using UnityEngine;

public class CardMovement : MonoBehaviour
{
    [SerializeField]
    private Card card;

    [SerializeField]
    private BattleDeck battleDeck;

    private RectTransform targetPosition;

    [SerializeField]
    private float moveSpeed;
    
    public RectTransform TargetPosition
    {
        get
        {
            return targetPosition;
        }
        set
        {
            targetPosition = value;
        }
    }

    public IEnumerator MoveToPosition(bool shouldCheckHand)
    {
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();

        float distance = Vector2.Distance(rectTransform.anchoredPosition, targetPosition.anchoredPosition);

        while (distance > 0)
        {
            rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, targetPosition.anchoredPosition, moveSpeed);

            distance = Vector2.Distance(rectTransform.anchoredPosition, targetPosition.anchoredPosition);

            yield return new WaitForEndOfFrame();
        }

        rectTransform.anchoredPosition = targetPosition.anchoredPosition;

        card._Animator.enabled = true;

        card.CardButton.GetComponent<ButtonListeners>().AttachListener();

        card.CardButton.interactable = true;

        if (shouldCheckHand)
        {
            battleDeck.CurrentHandSize++;
            battleDeck.CheckCardsInHand();
        }
    }

    public IEnumerator SetToPosition()
    {
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();

        float distance = Vector2.Distance(rectTransform.anchoredPosition, targetPosition.anchoredPosition);

        while (distance > 0)
        {
            rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, targetPosition.anchoredPosition, moveSpeed);

            distance = Vector2.Distance(rectTransform.anchoredPosition, targetPosition.anchoredPosition);

            yield return new WaitForEndOfFrame();
        }

        rectTransform.anchoredPosition = targetPosition.anchoredPosition;

        card._Animator.enabled = true;

        card.CardButton.interactable = true;

        battleDeck.CurrentHandSize++;

        battleDeck.ContinuePlayersTurn();
    }
}