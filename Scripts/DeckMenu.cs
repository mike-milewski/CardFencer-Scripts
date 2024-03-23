using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeckMenu : MonoBehaviour
{
    [SerializeField]
    private PlayerMenuInfo playerMenuInfo;

    [SerializeField]
    private List<MenuCard> itemCards;

    [SerializeField]
    private Selectable[] cardCategories;

    [SerializeField]
    private Transform[] stashParents;

    [SerializeField]
    private ScrollRectEx stashScrollRect, deckScrollRect;

    [SerializeField]
    private Selectable exitButton, fakeExitButton;

    [SerializeField]
    private GameObject statusEffectPanel;

    [SerializeField]
    private Transform deckParent, itemParent;

    private Transform currentStashParent;

    public ScrollRectEx StashScrollRect => stashScrollRect;

    public ScrollRectEx DeckScrollRect => deckScrollRect;

    public Transform[] StashParents => stashParents;

    public List<MenuCard> ItemCards => itemCards;

    public Selectable[] _CardCategories => cardCategories;

    public Selectable ExitButton => exitButton;

    public Selectable FakeExitButton => fakeExitButton;

    public Transform CurrentStashParent => currentStashParent;

    public Transform DeckParent => deckParent;

    private void Update()
    {
        if (!MenuController.instance.IsInDeckMenu) return;

        if (MenuController.instance.DeckUiBlocker.activeSelf) return;

        if (deckParent.childCount <= 1) return;

        if(InputManager.instance.ControllerPluggedIn)
        {
            if(InputManager.instance.ControllerName == "xbox")
            {
                if(Input.GetButtonDown("XboxOrganize"))
                {
                    SortDeckCards();
                }
            }
            else
            {
                if (Input.GetButtonDown("PlaystationOrganize"))
                {
                    SortDeckCards();
                }
            }
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.S))
            {
                SortDeckCards();
            }
        }
    }

    public void SetDefaultStashParent()
    {
        MenuController.instance._CardTypeCategory.ViewAllCards();
    }

    private void AdjustItemCardNavigations()
    {
        itemCards.Clear();

        Navigation exitNav = exitButton.navigation;
        Navigation fakeExitNav = fakeExitButton.navigation;

        foreach (MenuCard card in itemParent.GetComponentsInChildren<MenuCard>())
        {
            itemCards.Add(card);
        }

        if (itemCards.Count <= 0)
        {
            exitNav.selectOnRight = cardCategories[0].GetComponent<Selectable>();

            exitButton.navigation = exitNav;

            fakeExitNav.selectOnRight = cardCategories[0].GetComponent<Selectable>();

            fakeExitButton.navigation = fakeExitNav;

            return;
        }
        else
        {
            exitNav.selectOnRight = itemCards[0].GetComponent<Selectable>();

            exitButton.navigation = exitNav;

            fakeExitNav.selectOnRight = itemCards[0].GetComponent<Selectable>();

            fakeExitButton.navigation = fakeExitNav;
        }

        for(int i = 0; i < itemCards.Count; i++)
        {
            Navigation nav = itemCards[i].GetComponent<Selectable>().navigation;

            if (i == 0)
            {
                if(deckParent.childCount < playerMenuInfo.minimumDeckSize)
                {
                    nav.selectOnLeft = fakeExitButton;
                }
                else
                {
                    nav.selectOnLeft = exitButton;
                }

                if(itemCards.Count > 1)
                {
                    nav.selectOnRight = itemCards[i + 1].GetComponent<Selectable>();
                }
                else
                {
                    nav.selectOnRight = cardCategories[0];
                }
            }
            else if (i >= itemCards.Count - 1)
            {
                nav.selectOnRight = cardCategories[0];

                if(itemCards.Count > 1)
                   nav.selectOnLeft = itemCards[i - 1].GetComponent<Selectable>();
            }
            else
            {
                nav.selectOnRight = itemCards[i + 1].GetComponent<Selectable>();
                nav.selectOnLeft = itemCards[i - 1].GetComponent<Selectable>();
            }

            if (deckParent.childCount > 0)
            {
                nav.selectOnDown = deckParent.GetChild(0).GetComponent<Selectable>();
            }

            itemCards[i].GetComponent<Selectable>().navigation = nav;
        }
    }

    public void AdjustCardCategoryButtonNavigations()
    {
        foreach(Transform trans in stashParents)
        {
            if(trans.gameObject.activeSelf)
            {
                currentStashParent = trans;

                if (trans.childCount > 0)
                {
                    for (int i = 0; i < cardCategories.Length; i++)
                    {
                        Navigation nav = cardCategories[i].navigation;

                        nav.selectOnDown = trans.GetChild(0).GetComponent<Selectable>();

                        if(i == 0)
                        {
                            if (deckParent.childCount <= 0 && itemCards.Count > 0)
                            {
                                nav.selectOnLeft = itemCards[0].GetComponent<Selectable>();
                            }

                            if(deckParent.childCount <= 0 && itemCards.Count <= 0)
                            {
                                nav.selectOnLeft = fakeExitButton;
                            }
                        }

                        cardCategories[i].navigation = nav;
                    }
                }
                else
                {
                    for (int i = 0; i < cardCategories.Length; i++)
                    {
                        Navigation nav = cardCategories[i].navigation;

                        if (i == 0)
                        {
                            if (deckParent.childCount > 0)
                            {
                                nav.selectOnLeft = deckParent.GetChild(0).GetComponent<Selectable>();
                            }
                            if(itemCards.Count > 0)
                            {
                                nav.selectOnLeft = itemCards[0].GetComponent<Selectable>();
                            }
                            if(deckParent.childCount < playerMenuInfo.minimumDeckSize && itemCards.Count <= 0)
                            {
                                nav.selectOnLeft = fakeExitButton;
                            }
                            if(deckParent.childCount > playerMenuInfo.minimumDeckSize && itemCards.Count <= 0)
                            {
                                nav.selectOnLeft = exitButton;
                            }
                        }

                        cardCategories[i].navigation = nav;
                    }
                }
            }
        }
    }

    public void AdjustDeckCardsNavigations()
    {
        AdjustItemCardNavigations();

        Navigation exitNav = exitButton.navigation;
        Navigation fakeExitNav = fakeExitButton.navigation;

        if (deckParent.childCount > 0)
        {
            exitNav.selectOnDown = deckParent.GetChild(0).GetComponent<Selectable>();

            exitButton.navigation = exitNav;

            fakeExitNav.selectOnDown = deckParent.GetChild(0).GetComponent<Selectable>();

            fakeExitButton.navigation = fakeExitNav;

            for (int i = 0; i < deckParent.childCount; i++)
            {
                Navigation nav = deckParent.GetChild(i).GetComponent<Selectable>().navigation;

                nav.selectOnUp = null;
                nav.selectOnRight = null;
                nav.selectOnLeft = null;
                nav.selectOnDown = null;

                if(i == 0)
                {
                    if(deckParent.childCount < playerMenuInfo.minimumDeckSize)
                    {
                        nav.selectOnLeft = fakeExitButton;
                    }
                    else
                    {
                        nav.selectOnLeft = exitButton;
                    }

                    if(deckParent.childCount > 1)
                    {
                        nav.selectOnRight = deckParent.GetChild(i + 1).GetComponent<Selectable>();
                    }

                    if(deckParent.childCount == 1)
                    {
                        nav.selectOnRight = currentStashParent.GetChild(0).GetComponent<Selectable>();
                    }
                }
                else if(i >= deckParent.childCount - 1)
                {
                    if(currentStashParent.childCount <= 0)
                    {
                        nav.selectOnRight = deckParent.GetChild(0).GetComponent<Selectable>();
                    }
                    else
                    {
                        nav.selectOnRight = currentStashParent.GetChild(0).GetComponent<Selectable>();
                    }
                    nav.selectOnLeft = deckParent.GetChild(i - 1).GetComponent<Selectable>();
                }
                else
                {
                    nav.selectOnRight = deckParent.GetChild(i + 1).GetComponent<Selectable>();
                    nav.selectOnLeft = deckParent.GetChild(i - 1).GetComponent<Selectable>();
                }

                int farRightDeckEnd = i + 1;

                if(farRightDeckEnd % 5 == 0 && farRightDeckEnd <= deckParent.childCount && i != 0)
                {
                    if(currentStashParent.childCount > 0)
                    {
                        nav.selectOnRight = currentStashParent.GetChild(0).GetComponent<Selectable>();
                    }
                    else
                    {
                        nav.selectOnRight = cardCategories[0].GetComponent<Selectable>();
                    }
                }

                if(i <= 4)
                {
                    if(itemCards.Count > 0)
                    {
                        nav.selectOnUp = itemCards[0].GetComponent<Selectable>();
                    }
                    else
                    {
                        if(deckParent.childCount < playerMenuInfo.minimumDeckSize)
                        {
                            nav.selectOnUp = fakeExitButton;
                        }
                        else
                        {
                            nav.selectOnUp = exitButton;
                        }
                    }
                }

                if (deckParent.childCount > 4)
                {
                    int downWardCardIndex = i + 5;
                    nav.selectOnDown = deckParent.GetChild(downWardCardIndex >= deckParent.childCount - 1 ? deckParent.childCount - 1 : downWardCardIndex).GetComponent<Selectable>();
                }

                if (i > 4)
                {
                    int upWardCardIndex = i - 5;
                    nav.selectOnUp = deckParent.GetChild(upWardCardIndex).GetComponent<Selectable>();
                }

                deckParent.GetChild(i).GetComponent<Selectable>().navigation = nav;
            }
        }
    }

    public void AdjustStashCardsNavigations()
    {
        AdjustItemCardNavigations();

        if(currentStashParent.childCount > 0)
        {
            for (int i = 0; i < currentStashParent.childCount; i++)
            {
                Navigation nav = currentStashParent.GetChild(i).GetComponent<Selectable>().navigation;

                nav.selectOnUp = null;
                nav.selectOnRight = null;
                nav.selectOnLeft = null;
                nav.selectOnDown = null;

                if (i == 0)
                {
                    if (currentStashParent.childCount > 1)
                    {
                        nav.selectOnRight = currentStashParent.GetChild(i + 1).GetComponent<Selectable>();
                    }

                    if(deckParent.childCount > 0)
                    {
                        nav.selectOnLeft = deckParent.GetChild(deckParent.childCount - 1).GetComponent<Selectable>();
                    }
                }
                else if (i >= currentStashParent.childCount - 1)
                {
                    if (deckParent.childCount <= 0)
                    {
                        nav.selectOnRight = currentStashParent.GetChild(0).GetComponent<Selectable>();
                    }
                    nav.selectOnLeft = currentStashParent.GetChild(i - 1).GetComponent<Selectable>();
                }
                else
                {
                    nav.selectOnRight = currentStashParent.GetChild(i + 1).GetComponent<Selectable>();
                    nav.selectOnLeft = currentStashParent.GetChild(i - 1).GetComponent<Selectable>();
                }

                if (i % 5 == 0 && deckParent.childCount > 1)
                {
                    if (deckParent.childCount > 0)
                    {
                        nav.selectOnLeft = deckParent.GetChild(deckParent.childCount - 1).GetComponent<Selectable>();
                    }
                    else
                    {
                        if(itemCards.Count > 0)
                        {
                            nav.selectOnLeft = itemCards[0].GetComponent<Selectable>();
                        }
                        else
                        {
                            nav.selectOnLeft = exitButton;
                        }
                    }
                }

                if(i <= 4)
                {
                    nav.selectOnUp = cardCategories[0].GetComponent<Selectable>();
                }

                if (currentStashParent.childCount > 4)
                {
                    int downWardCardIndex = i + 5;
                    nav.selectOnDown = currentStashParent.GetChild(downWardCardIndex >= currentStashParent.childCount - 1 ? currentStashParent.childCount - 1 : downWardCardIndex).GetComponent<Selectable>();
                }

                if (i > 4)
                {
                    int upWardCardIndex = i - 5;
                    nav.selectOnUp = currentStashParent.GetChild(upWardCardIndex).GetComponent<Selectable>();
                }

                currentStashParent.GetChild(i).GetComponent<Selectable>().navigation = nav;
            }
        }
    }

    private void SortDeckCards()
    {
        AudioManager.instance.PlaySoundEffect(AudioManager.instance.CardAudio);

        MenuController.instance._MainCharacterStats.currentCards = MenuController.instance._MainCharacterStats.currentCards.OrderBy(CardTemplate => CardTemplate.cardType).ToList();

        foreach(MenuCard card in deckParent.GetComponentsInChildren<MenuCard>())
        {
            Destroy(card.gameObject);
        }

        for(int i = 0; i < MenuController.instance._MainCharacterStats.currentCards.Count; i++)
        {
            var card = Instantiate(MenuController.instance.MenuCardForDeckToCreate, deckParent);

            card.InDeckList = true;

            card.gameObject.SetActive(true);

            card._cardTemplate = MenuController.instance._MainCharacterStats.currentCards[i];

            MenuController.instance._MainCharacterStats.currentCards[i] = MenuController.instance._MainCharacterStats.currentCards[i];

            card.CardIndex = i;

            card.UpdateCardInformation();
        }

        Scene scene = SceneManager.GetActiveScene();

        if(scene.name.Contains("Secret"))
        {
            if (NodeManager.instance != null)
            {
                MenuController.instance.CheckStagePenalties();
            }
        }

        statusEffectPanel.SetActive(false);

        StartCoroutine(SetDeckCard());
    }

    private IEnumerator SetDeckCard()
    {
        yield return new WaitForSecondsRealtime(0.2f);

        InputManager.instance.SetSelectedObject(deckParent.GetChild(0).gameObject);

        MenuController.instance._DeckMenu.AdjustDeckCardsNavigations();
        MenuController.instance._DeckMenu.AdjustStashCardsNavigations();
    }

    public void FocusItem(RectTransform card)
    {
        if(InputManager.instance.ControllerPluggedIn)
        {
            if(card.GetComponent<MenuCard>()._propogateDrag.scrollView == null)
            {
                if(card.GetComponent<MenuCard>().InDeckList)
                {
                    card.GetComponent<MenuCard>()._propogateDrag.scrollView = MenuController.instance.DeckListScroll;
                }
                else
                {
                    card.GetComponent<MenuCard>()._propogateDrag.scrollView = MenuController.instance.CardListScroll;
                }

                StartCoroutine(ScrollViewFocusFunctions.FocusOnItemCoroutine(card.GetComponent<MenuCard>()._propogateDrag.scrollView, card, 3f));
            }
            else
            {
                StartCoroutine(ScrollViewFocusFunctions.FocusOnItemCoroutine(card.GetComponent<MenuCard>()._propogateDrag.scrollView, card, 3f));
            }
        }  
    }
}