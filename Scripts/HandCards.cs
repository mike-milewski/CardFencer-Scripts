using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HandCards : MonoBehaviour
{
    [SerializeField]
    private BattleSystem battleSystem;

    [SerializeField]
    private Card cardToCreate;

    [SerializeField]
    private Transform cardParent;

    [SerializeField]
    private Animator handPanelAnimator;

    [SerializeField]
    private Selectable closeHandMenuSelectable;

    private Card copyCard;

    public void OpenPanel()
    {
        handPanelAnimator.Play("FadeIn", -1, 0);
    }

    public void ClosePanel()
    {
        closeHandMenuSelectable.GetComponent<Button>().onClick.Invoke();
    }

    public void ShowHandCards(Card crd, CardTemplate cardTemplate)
    {
        var card = Instantiate(cardToCreate, cardParent);

        card.gameObject.SetActive(true);
        card._cardTemplate = cardTemplate;
        card.CheckCardType();
        card.SetCardPoints(battleSystem._BattleDeck.PowerCardAmount, battleSystem._BattleDeck.MagicCardAmount, battleSystem._BattleDeck.SupportCardAmount);
        card.AddedSameNamedCardForTwoCardCombo = crd.AddedSameNamedCardForTwoCardCombo;
        card.AddedSameNamedCardForThreeCardCombo = crd.AddedSameNamedCardForThreeCardCombo;
        card.TwoComboTemplateName = crd.TwoComboTemplateName;
        card.ThreeComboTemplateName = crd.ThreeComboTemplateName;
    }

    public void SetCopyCard(Card crd)
    {
        copyCard = crd;
    }

    public void DestroyHandCards()
    {
        if (cardParent.childCount <= 0) return;

        foreach(Card c in cardParent.GetComponentsInChildren<Card>())
        {
            Destroy(c.gameObject);
        }
    }

    public void AddListenerToCards()
    {
        foreach (Card crd in cardParent.GetComponentsInChildren<Card>())
        {
            crd.CardButton.onClick.RemoveAllListeners();
            crd.CardButton.onClick.AddListener(() => crd.BecomeACopy(copyCard));

            crd.CardButton.interactable = true;
        }
    }

    public void CardNavigationRoutine()
    {
        StartCoroutine(UpdateCardNavigation());
    }

    private IEnumerator UpdateCardNavigation()
    {
        battleSystem._EventSystem.sendNavigationEvents = false;
        battleSystem.NavagationDisabled = true;

        yield return new WaitForSeconds(0.5f);

        Navigation closeButtonNav = closeHandMenuSelectable.navigation;

        for(int i = 0; i < cardParent.childCount; i++)
        {
            Navigation cardNav = cardParent.GetChild(i).GetComponent<Selectable>().navigation;

            switch(i)
            {
                case 0:
                    if (cardParent.childCount <= 1)
                    {
                        cardNav.selectOnRight = closeHandMenuSelectable;
                        closeButtonNav.selectOnLeft = cardParent.GetChild(i).GetComponent<Selectable>();
                    }
                    else
                    {
                        cardNav.selectOnRight = cardParent.GetChild(i + 1).GetComponent<Selectable>();
                    }
                    break;
                case 1:
                    if(i >= cardParent.childCount - 1)
                    {
                        cardNav.selectOnRight = closeHandMenuSelectable;
                        closeButtonNav.selectOnLeft = cardParent.GetChild(i).GetComponent<Selectable>();
                    }
                    else
                    {
                        cardNav.selectOnRight = cardParent.GetChild(i + 1).GetComponent<Selectable>();
                    }
                    cardNav.selectOnLeft = cardParent.GetChild(i - 1).GetComponent<Selectable>();
                    break;
                case 2:
                    if (i >= cardParent.childCount - 1)
                    {
                        cardNav.selectOnRight = closeHandMenuSelectable;
                        closeButtonNav.selectOnLeft = cardParent.GetChild(i).GetComponent<Selectable>();
                    }
                    else
                    {
                        cardNav.selectOnRight = cardParent.GetChild(i + 1).GetComponent<Selectable>();
                    }
                    cardNav.selectOnLeft = cardParent.GetChild(i - 1).GetComponent<Selectable>();
                    break;
                case 3:
                    cardNav.selectOnRight = closeHandMenuSelectable;
                    cardNav.selectOnLeft = cardParent.GetChild(i - 1).GetComponent<Selectable>();

                    closeButtonNav.selectOnLeft = cardParent.GetChild(i).GetComponent<Selectable>();
                    break;
            }

            cardParent.GetChild(i).GetComponent<Selectable>().navigation = cardNav;
        }

        battleSystem._InputManager.SetSelectedObject(cardParent.GetChild(0).gameObject);

        battleSystem._EventSystem.sendNavigationEvents = true;

        battleSystem.NavagationDisabled = false;

        closeHandMenuSelectable.navigation = closeButtonNav;
    }
}