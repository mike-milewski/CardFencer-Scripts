using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboCards : MonoBehaviour
{
    [SerializeField]
    private List<Card> card = new List<Card>();

    [SerializeField]
    private List<CardTemplate> combinedCardTemplates = new List<CardTemplate>();

    [SerializeField]
    private BattleDeck battleDeck;

    [SerializeField]
    private BattleSystem battleSystem;

    [SerializeField]
    private PlayerMenuInfo playerMenuInfo;

    [SerializeField]
    private Card combinedCard, cardOne = null, cardTwo = null, cardThree = null, unusedCardOne = null, unusedCardTwo = null, unusedCardThree = null;

    [SerializeField]
    private Animator combinedCardAnimator;

    [SerializeField]
    private int maxSelectedCards;

    [SerializeField]
    private bool canSelectMoreCards;

    [SerializeField]
    private int currentSelectedCards;

    private bool combinedThreeCards;

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

    public Card CombinedCard => combinedCard;

    public Animator CombinedCardAnimator
    {
        get
        {
            return combinedCardAnimator;
        }
        set
        {
            combinedCardAnimator = value;
        }
    }

    public int MaxSelectedCards => maxSelectedCards;

    public int CurrentSelectedCards
    {
        get
        {
            return currentSelectedCards;
        }
        set
        {
            currentSelectedCards = value;
        }
    }

    public bool CombinedThreeCards
    {
        get
        {
            return combinedThreeCards;
        }
        set
        {
            combinedThreeCards = value;
        }
    }

    public bool CanSelectMoreCards => canSelectMoreCards;

    public void AddCardCombos()
    {
        for (int i = 0; i < combinedCardTemplates.Count; i++)
        {
            for(int j = 0; j < combinedCardTemplates[i].combination.Combinations.Length; j++)
            {
                foreach (Card c in battleDeck._Card)
                {
                    if (combinedCardTemplates[i].combination.Combinations.Length == 2)
                    {
                        if (c._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[j])
                        {
                            foreach(CardTemplateName ctn in combinedCardTemplates[i].combination.Combinations)
                            {
                                if(c._cardTemplate.cardTemplateName != ctn && !combinedCardTemplates[i].combination.UsesSameCard)
                                {
                                    c.TwoComboTemplateName.Add(ctn);
                                }
                                else if(c._cardTemplate.cardTemplateName == ctn && combinedCardTemplates[i].combination.UsesSameCard)
                                {
                                    if(!c.AddedSameNamedCardForTwoCardCombo)
                                    {
                                        c.TwoComboTemplateName.Add(ctn);

                                        c.AddedSameNamedCardForTwoCardCombo = true;
                                    }
                                }
                            }
                        }
                    }
                    else if(combinedCardTemplates[i].combination.Combinations.Length == 3)
                    {
                        if (c._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[j])
                        {
                            foreach (CardTemplateName ctn in combinedCardTemplates[i].combination.Combinations)
                            {
                                if (c._cardTemplate.cardTemplateName != ctn && !combinedCardTemplates[i].combination.UsesSameCard)
                                {
                                    c.ThreeComboTemplateName.Add(ctn);
                                }
                                else if (c._cardTemplate.cardTemplateName == ctn && combinedCardTemplates[i].combination.UsesSameCard)
                                {
                                    if(!c.AddedSameNamedCardForThreeCardCombo)
                                    {
                                        c.ThreeComboTemplateName.Add(ctn);

                                        c.AddedSameNamedCardForThreeCardCombo = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    public void CheckCombinations()
    {
        if(currentSelectedCards == 1)
        {
            for (int i = 0; i < battleDeck.CardsInHand.Count; i++)
            {
                for (int j = 0; j < battleDeck.CardsInHand[i].TwoComboTemplateName.Count; j++)
                {
                    if (cardOne._cardTemplate.cardTemplateName == battleDeck.CardsInHand[i].TwoComboTemplateName[j])
                    {
                        if (!battleDeck.CardsInHand[i].ChosenForCombining)
                        {
                            battleDeck.CardsInHand[i].AbleToCombineColorFrame();
                        }
                        battleDeck.CardsInHand[i].CanCombineWith = true;

                        break;
                    }
                    if (!battleDeck.CardsInHand[i].CanCombineWith || battleDeck.CardsInHand[i].CombinedCard.Count <= 0)
                    {
                        if (!battleDeck.CardsInHand[i].ChosenForCombining)
                        {
                            battleDeck.CardsInHand[i].UnableToCombineColorFrame();
                        }
                    }
                }

                if (battleDeck.CardsInHand[i]._cardTemplate.combination.CannotBeUsedToCombine)
                {
                    battleDeck.CardsInHand[i].UnableToCombineColorFrame();
                }
            }
        }

        if (currentSelectedCards == 2)
        {
            for (int i = 0; i < battleDeck.CardsInHand.Count; i++)
            {
                for (int j = 0; j < battleDeck.CardsInHand[i].ThreeComboTemplateName.Count; j++)
                {
                    if (cardOne._cardTemplate.cardTemplateName == battleDeck.CardsInHand[i].ThreeComboTemplateName[j] &&
                        cardTwo._cardTemplate.cardTemplateName == battleDeck.CardsInHand[i].ThreeComboTemplateName[j] ||
                        cardTwo._cardTemplate.cardTemplateName == battleDeck.CardsInHand[i].ThreeComboTemplateName[j] &&
                        cardOne._cardTemplate.cardTemplateName == battleDeck.CardsInHand[i].ThreeComboTemplateName[j])
                    {
                        if (!battleDeck.CardsInHand[i].ChosenForCombining)
                        {
                            battleDeck.CardsInHand[i].AbleToCombineColorFrame();
                        }
                        battleDeck.CardsInHand[i].CanCombineWith = true;

                        break;
                    }
                    if (!battleDeck.CardsInHand[i].CanCombineWith || battleDeck.CardsInHand[i].CombinedCard.Count <= 0)
                    {
                        if (!battleDeck.CardsInHand[i].ChosenForCombining)
                        {
                            battleDeck.CardsInHand[i].UnableToCombineColorFrame();
                        }
                    }
                }
            }

            CheckTwoCardCombos();

            if(playerMenuInfo.currentHandSize < 4)
            {
                foreach (Card c in battleDeck.CardsInHand)
                {
                    if (!c.ChosenForCombining)
                    {
                        unusedCardOne = c;
                    }
                }
            }
            else if(playerMenuInfo.currentHandSize == 4)
            {
                foreach (Card c in battleDeck.CardsInHand)
                {
                    if (!c.ChosenForCombining)
                    {
                        if(unusedCardOne == null)
                        {
                            unusedCardOne = c;
                        }
                        else
                        {
                            unusedCardTwo = c;
                        }
                    }
                }
            }
            else
            {
                foreach (Card c in battleDeck.CardsInHand)
                {
                    if (!c.ChosenForCombining)
                    {
                        if (unusedCardOne == null)
                        {
                            unusedCardOne = c;
                        }
                        else if(unusedCardTwo == null && unusedCardOne != null)
                        {
                            unusedCardTwo = c;
                        }
                        else if(unusedCardOne != null && unusedCardTwo != null)
                        {
                            unusedCardThree = c;
                        }
                    }
                }
            }


            if (playerMenuInfo.currentHandSize < 4)
            {
                if (unusedCardOne != null)
                    CheckThirdCombination();
            }
            else if (playerMenuInfo.currentHandSize == 4)
            {
                if (unusedCardOne != null && unusedCardTwo != null)
                    CheckThirdCombination();
            }
            else
            {
                if (unusedCardOne != null && unusedCardTwo != null && unusedCardThree != null)
                    CheckThirdCombination();
            }
        }
        else if(currentSelectedCards == 3)
        {
            CheckThreeCardCombos();

            if(playerMenuInfo.currentHandSize > 4)
            {
                for(int i = 0; i < battleDeck.CardsInHand.Count; i++)
                {
                    if(!battleDeck.CardsInHand[i].ChosenForCombining)
                    {
                        battleDeck.CardsInHand[i].UnableToCombineColorFrame();
                    }
                }
            }
        }
    }

    private void CheckThirdCombination()
    {
        int combos = 0;

        if (unusedCardOne != null)
        {
            unusedCardOne.IsCompatibleComboCard = false;
        }
        if (unusedCardTwo != null)
        {
            unusedCardTwo.IsCompatibleComboCard = false;
        }
        if (unusedCardThree != null)
        {
            unusedCardThree.IsCompatibleComboCard = false;
        }

        for (int i = 0; i < combinedCardTemplates.Count; i++)
        {
            if (combinedCardTemplates[i].combination.Combinations.Length == 3)
            {
                if(playerMenuInfo.currentHandSize < 4)
                {
                    if (cardOne._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[0] &&
                        cardTwo._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[1] &&
                        unusedCardOne._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[2] ||
                        cardOne._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[1] &&
                        cardTwo._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[0] &&
                        unusedCardOne._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[2] ||
                        cardOne._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[2] &&
                        cardTwo._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[1] &&
                        unusedCardOne._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[0] ||
                        cardOne._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[0] &&
                        cardTwo._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[2] &&
                        unusedCardOne._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[1] ||
                        cardOne._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[1] &&
                        cardTwo._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[2] &&
                        unusedCardOne._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[0] ||
                        cardOne._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[2] &&
                        cardTwo._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[0] &&
                        unusedCardOne._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[1])
                    {
                        combos++;
                    }
                }
                else if(playerMenuInfo.currentHandSize == 4)
                {
                    if(!unusedCardOne.IsCompatibleComboCard)
                    {
                        if (cardOne._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[0] &&
                            cardTwo._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[1] &&
                            unusedCardOne._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[2] ||
                            cardOne._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[1] &&
                            cardTwo._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[0] &&
                            unusedCardOne._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[2] ||
                            cardOne._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[2] &&
                            cardTwo._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[1] &&
                            unusedCardOne._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[0] ||
                            cardOne._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[0] &&
                            cardTwo._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[2] &&
                            unusedCardOne._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[1] ||
                            cardOne._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[1] &&
                            cardTwo._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[2] &&
                            unusedCardOne._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[0] ||
                            cardOne._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[2] &&
                            cardTwo._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[0] &&
                            unusedCardOne._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[1])
                        {
                            unusedCardOne.IsCompatibleComboCard = true;

                            combos++;
                        }
                    }
                    
                    if(!unusedCardTwo.IsCompatibleComboCard)
                    {
                        if (cardOne._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[0] &&
                            cardTwo._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[1] &&
                            unusedCardTwo._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[2] ||
                            cardOne._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[1] &&
                            cardTwo._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[0] &&
                            unusedCardTwo._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[2] ||
                            cardOne._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[2] &&
                            cardTwo._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[1] &&
                            unusedCardTwo._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[0] ||
                            cardOne._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[0] &&
                            cardTwo._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[2] &&
                            unusedCardTwo._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[1] ||
                            cardOne._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[1] &&
                            cardTwo._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[2] &&
                            unusedCardTwo._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[0] ||
                            cardOne._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[2] &&
                            cardTwo._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[0] &&
                            unusedCardTwo._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[1])
                        {
                            unusedCardTwo.IsCompatibleComboCard = true;

                            combos++;
                        }
                    }
                }
                else
                {
                    if(!unusedCardOne.IsCompatibleComboCard)
                    {
                        if (cardOne._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[0] &&
                            cardTwo._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[1] &&
                            unusedCardOne._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[2] ||
                            cardOne._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[1] &&
                            cardTwo._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[0] &&
                            unusedCardOne._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[2] ||
                            cardOne._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[2] &&
                            cardTwo._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[1] &&
                            unusedCardOne._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[0] ||
                            cardOne._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[0] &&
                            cardTwo._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[2] &&
                            unusedCardOne._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[1] ||
                            cardOne._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[1] &&
                            cardTwo._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[2] &&
                            unusedCardOne._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[0] ||
                            cardOne._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[2] &&
                            cardTwo._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[0] &&
                            unusedCardOne._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[1])
                        {
                            unusedCardOne.IsCompatibleComboCard = true;

                            combos++;
                        }
                    }
                    
                    if(!unusedCardTwo.IsCompatibleComboCard)
                    {
                        if (cardOne._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[0] &&
                            cardTwo._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[1] &&
                            unusedCardTwo._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[2] ||
                            cardOne._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[1] &&
                            cardTwo._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[0] &&
                            unusedCardTwo._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[2] ||
                            cardOne._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[2] &&
                            cardTwo._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[1] &&
                            unusedCardTwo._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[0] ||
                            cardOne._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[0] &&
                            cardTwo._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[2] &&
                            unusedCardTwo._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[1] ||
                            cardOne._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[1] &&
                            cardTwo._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[2] &&
                            unusedCardTwo._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[0] ||
                            cardOne._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[2] &&
                            cardTwo._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[0] &&
                            unusedCardTwo._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[1])
                        {
                            unusedCardTwo.IsCompatibleComboCard = true;

                            combos++;
                        }
                    }
                    
                    if(!unusedCardThree.IsCompatibleComboCard)
                    {
                        if (cardOne._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[0] &&
                            cardTwo._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[1] &&
                            unusedCardThree._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[2] ||
                            cardOne._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[1] &&
                            cardTwo._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[0] &&
                            unusedCardThree._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[2] ||
                            cardOne._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[2] &&
                            cardTwo._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[1] &&
                            unusedCardThree._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[0] ||
                            cardOne._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[0] &&
                            cardTwo._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[2] &&
                            unusedCardThree._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[1] ||
                            cardOne._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[1] &&
                            cardTwo._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[2] &&
                            unusedCardThree._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[0] ||
                            cardOne._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[2] &&
                            cardTwo._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[0] &&
                            unusedCardThree._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[1])
                        {
                            unusedCardThree.IsCompatibleComboCard = true;

                            combos++;
                        }
                    }
                }
            }
        }

        if (combos >= 1)
        {
            if(playerMenuInfo.currentHandSize < 4)
            {
                unusedCardOne.CanCombineWith = true;

                unusedCardOne.AbleToCombineColorFrame();
            }
            else if(playerMenuInfo.currentHandSize == 4)
            {
                if (unusedCardOne.IsCompatibleComboCard && unusedCardTwo.IsCompatibleComboCard)
                {
                    unusedCardOne.CanCombineWith = true;
                    unusedCardTwo.CanCombineWith = true;

                    unusedCardOne.AbleToCombineColorFrame();
                    unusedCardTwo.AbleToCombineColorFrame();
                }
                if (unusedCardOne.IsCompatibleComboCard && !unusedCardTwo.IsCompatibleComboCard)
                {
                    unusedCardOne.CanCombineWith = true;
                    unusedCardTwo.CanCombineWith = false;

                    unusedCardOne.AbleToCombineColorFrame();
                    unusedCardTwo.UnableToCombineColorFrame();
                }
                if (!unusedCardOne.IsCompatibleComboCard && unusedCardTwo.IsCompatibleComboCard)
                {
                    unusedCardOne.CanCombineWith = false;
                    unusedCardTwo.CanCombineWith = true;

                    unusedCardOne.UnableToCombineColorFrame();
                    unusedCardTwo.AbleToCombineColorFrame();
                }
            }
            else
            {
                if(unusedCardOne.IsCompatibleComboCard && !unusedCardTwo.IsCompatibleComboCard && !unusedCardThree.IsCompatibleComboCard)
                {
                    unusedCardOne.CanCombineWith = true;
                    unusedCardTwo.CanCombineWith = false;
                    unusedCardThree.CanCombineWith = false;

                    unusedCardOne.AbleToCombineColorFrame();
                    unusedCardTwo.UnableToCombineColorFrame();
                    unusedCardThree.UnableToCombineColorFrame();
                }
                if(unusedCardOne.IsCompatibleComboCard && unusedCardTwo.IsCompatibleComboCard && !unusedCardThree.IsCompatibleComboCard)
                {
                    unusedCardOne.CanCombineWith = true;
                    unusedCardTwo.CanCombineWith = true;
                    unusedCardThree.CanCombineWith = false;

                    unusedCardOne.AbleToCombineColorFrame();
                    unusedCardTwo.AbleToCombineColorFrame();
                    unusedCardThree.UnableToCombineColorFrame();
                }
                if(unusedCardOne.IsCompatibleComboCard && unusedCardTwo.IsCompatibleComboCard && unusedCardThree.IsCompatibleComboCard)
                {
                    unusedCardOne.CanCombineWith = true;
                    unusedCardTwo.CanCombineWith = true;
                    unusedCardThree.CanCombineWith = true;

                    unusedCardOne.AbleToCombineColorFrame();
                    unusedCardTwo.AbleToCombineColorFrame();
                    unusedCardThree.AbleToCombineColorFrame();
                }
                if (!unusedCardOne.IsCompatibleComboCard && unusedCardTwo.IsCompatibleComboCard && !unusedCardThree.IsCompatibleComboCard)
                {
                    unusedCardOne.CanCombineWith = false;
                    unusedCardTwo.CanCombineWith = true;
                    unusedCardThree.CanCombineWith = false;

                    unusedCardOne.UnableToCombineColorFrame();
                    unusedCardTwo.AbleToCombineColorFrame();
                    unusedCardThree.UnableToCombineColorFrame();
                }
                if (!unusedCardOne.IsCompatibleComboCard && unusedCardTwo.IsCompatibleComboCard && unusedCardThree.IsCompatibleComboCard)
                {
                    unusedCardOne.CanCombineWith = false;
                    unusedCardTwo.CanCombineWith = true;
                    unusedCardThree.CanCombineWith = true;

                    unusedCardOne.UnableToCombineColorFrame();
                    unusedCardTwo.AbleToCombineColorFrame();
                    unusedCardThree.AbleToCombineColorFrame();
                }
                if (!unusedCardOne.IsCompatibleComboCard && !unusedCardTwo.IsCompatibleComboCard && unusedCardThree.IsCompatibleComboCard)
                {
                    unusedCardOne.CanCombineWith = false;
                    unusedCardTwo.CanCombineWith = false;
                    unusedCardThree.CanCombineWith = true;

                    unusedCardOne.UnableToCombineColorFrame();
                    unusedCardTwo.UnableToCombineColorFrame();
                    unusedCardThree.AbleToCombineColorFrame();
                }
                if (unusedCardOne.IsCompatibleComboCard && !unusedCardTwo.IsCompatibleComboCard && unusedCardThree.IsCompatibleComboCard)
                {
                    unusedCardOne.CanCombineWith = true;
                    unusedCardTwo.CanCombineWith = false;
                    unusedCardThree.CanCombineWith = true;

                    unusedCardOne.AbleToCombineColorFrame();
                    unusedCardTwo.UnableToCombineColorFrame();
                    unusedCardThree.AbleToCombineColorFrame();
                }
            }
        }
        else
        {
            if(playerMenuInfo.currentHandSize < 4)
            {
                unusedCardOne.CanCombineWith = false;

                unusedCardOne.UnableToCombineColorFrame();
            }
            else if(playerMenuInfo.currentHandSize == 4)
            {
                unusedCardOne.CanCombineWith = false;
                unusedCardTwo.CanCombineWith = false;

                unusedCardOne.UnableToCombineColorFrame();
                unusedCardTwo.UnableToCombineColorFrame();
            }
            else
            {
                unusedCardOne.CanCombineWith = false;
                unusedCardTwo.CanCombineWith = false;
                unusedCardThree.CanCombineWith = false;

                unusedCardOne.UnableToCombineColorFrame();
                unusedCardTwo.UnableToCombineColorFrame();
                unusedCardThree.UnableToCombineColorFrame();
            }
        }
    }

    public void CheckIncompatibleCards()
    {
        if(cardOne != null && cardTwo != null)
        {
            int combos = 0;

            for (int i = 0; i < cardOne.TwoComboTemplateName.Count; i++)
            {
                for (int j = 0; j < cardTwo.TwoComboTemplateName.Count; j++)
                {
                    if (cardOne.TwoComboTemplateName[i] == cardTwo.TwoComboTemplateName[j] ||
                        cardTwo.TwoComboTemplateName[j] == cardOne.TwoComboTemplateName[i])
                    {
                        combos++;
                    }
                    else
                    {
                        combos = 0;
                        break;
                    }
                }
            }

            if (combos <= 0)
            {
                battleSystem.ResetHandObjects();

                if(unusedCardOne != null)
                {
                    unusedCardOne.IsCompatibleComboCard = false;
                }
                if(unusedCardTwo != null)
                {
                    unusedCardTwo.IsCompatibleComboCard = false;
                }
                if(unusedCardThree != null)
                {
                    unusedCardThree.IsCompatibleComboCard = false;
                }

                unusedCardOne = null;
                unusedCardTwo = null;
                unusedCardThree = null;
            }
        }
    }

    public void CheckTwoCardCombos()
    {
        for(int i = 0; i < combinedCardTemplates.Count; i++)
        {
            if(combinedCardTemplates[i].combination.Combinations.Length < 3)
            {
                if(cardOne._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[0] &&
                   cardTwo._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[1] ||
                   cardOne._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[1] &&
                   cardTwo._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[0])
                {
                    combinedCard._cardTemplate = combinedCardTemplates[i];

                    combinedCard.ParticleEffect = combinedCard.GetComponent<BattleCardParticleEffect>().SetUpParticle();

                    if (cardOne._cardTemplate.isAFoil && cardTwo._cardTemplate.isAFoil)
                    {
                        combinedCard._cardTemplate = combinedCard._cardTemplate.foilCard;
                    }

                    break;
                }
            }
        }

        if(!combinedThreeCards)
        {
            if (battleSystem.HasTheKnight)
            {
                combinedCard.CardStrength += 10;
                combinedCard.DamageValueText.text = combinedCard.CardStrength.ToString();
                combinedCard.DamageValueText.color = Color.green;
            }

            combinedCard.CheckCardType();
            combinedCard.CheckForEnrageStatusEffect();
            combinedCard.ChangeCardStrength(false, battleSystem._battlePlayer.StrengthPercentage);

            if(battleSystem._battlePlayer.HasCourageUnderFire)
            {
                combinedCard.DoubleCardStrength();
            }

            SetCombinedCardCost();
        }  

        ShowCombinedCard();
    }

    public void SetCombinedCardCost()
    {
        combinedCard.CardPoints = combinedCard._cardTemplate.cardPointCost;

        if(MenuController.instance._StickerPowerManager.CheckStartOfBattleStickerPower(StickerPower.CardMastery))
        {
            combinedCard.CardPoints -= 3;
        }

        if(MenuController.instance._StickerPowerManager.CheckStartOfBattleStickerPower(StickerPower.Alchemist))
        {
            combinedCard.CardPoints -= 2;
        }

        if(battleSystem.HasPowerPatron || battleSystem.HasMagicMonster || battleSystem.HasSupportStar)
        {
            combinedCard.CardPoints -= 1;
        }

        if(combinedCard._cardTemplate.isAFoil)
        {
            if (MenuController.instance._StickerPowerManager.CheckStartOfBattleStickerPower(StickerPower.FoilExpert))
            {
                int halfCost = Mathf.RoundToInt(combinedCard.CardPoints / 2);

                if (halfCost < 1)
                {
                    halfCost = 1;
                }

                combinedCard.CardPoints -= halfCost;
            }
        }

        if(battleSystem.HasTheMagician)
        {
            combinedCard.CardPoints -= 2;
        }

        if (combinedCard.CardPoints < combinedCard._cardTemplate.cardPointCost)
        {
            combinedCard.CardCostText.color = Color.green;
        }

        if (combinedCard.CardPoints <= 0)
        {
            combinedCard.CardPoints = 0;
        }
        
        combinedCard.CardCostText.text = combinedCard.CardPoints.ToString();
    }

    public void CheckThreeCardCombos()
    {
        int numberOfFoils = 0;

        if(cardOne._cardTemplate.isAFoil)
        {
            numberOfFoils++;
        }
        if(cardTwo._cardTemplate.isAFoil)
        {
            numberOfFoils++;
        }
        if(cardThree._cardTemplate.isAFoil)
        {
            numberOfFoils++;
        }

        for (int i = 0; i < combinedCardTemplates.Count; i++)
        {
            if (combinedCardTemplates[i].combination.Combinations.Length == 3)
            {
                if (cardOne._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[0] &&
                    cardTwo._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[1] &&
                    cardThree._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[2] ||
                    cardOne._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[1] &&
                    cardTwo._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[0] &&
                    cardThree._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[2] ||
                    cardOne._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[2] &&
                    cardTwo._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[1] &&
                    cardThree._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[0] ||
                    cardOne._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[1] &&
                    cardTwo._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[2] &&
                    cardThree._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[0] ||
                    cardOne._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[0] &&
                    cardTwo._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[2] &&
                    cardThree._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[1] ||
                    cardOne._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[2] &&
                    cardTwo._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[0] &&
                    cardThree._cardTemplate.cardTemplateName == combinedCardTemplates[i].combination.Combinations[1])
                {
                    combinedCard._cardTemplate = combinedCardTemplates[i];

                    combinedCard.ParticleEffect = combinedCard.GetComponent<BattleCardParticleEffect>().SetUpParticle();

                    if (numberOfFoils >= 3)
                    {
                        combinedCard._cardTemplate = combinedCard._cardTemplate.foilCard;
                    }

                    PlayCardRotateAnimation();

                    combinedCard.GetComponent<AudioSource>().clip = AudioManager.instance.CombinedCardAudio;
                    if (PlayerPrefs.HasKey("SoundEffects"))
                    {
                        combinedCard.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SoundEffects");
                        combinedCard.GetComponent<AudioSource>().Play();
                    }
                    else
                    {
                        combinedCard.GetComponent<AudioSource>().Play();
                    }

                    combinedThreeCards = true;
                    break;
                }
            }
        }
    }

    public void PlayCardRotateAnimation()
    {
        combinedCard.HideStatusEffectPanel();

        combinedCard.StopParticle();
        combinedCard._Animator.Play("CardRotate", -1, 0);
    }

    public void DisableCombineFrames()
    {
        for(int i = 0; i < battleDeck.CardsInHand.Count; i++)
        {
            battleDeck.CardsInHand[i].CombinationParticle.Stop();
            battleDeck.CardsInHand[i].CombinationParticle.gameObject.SetActive(false);
            battleDeck.CardsInHand[i].CombineFrame.gameObject.SetActive(false);

            if(battleDeck.CardsInHand[i]._cardTemplate.cardType != CardType.Mystic)
               battleDeck.CardsInHand[i].CanCombineWithOtherCards = true;
        }
    }

    public void ClearCardComboList()
    {
        foreach(Card crd in battleDeck.CardsInHand)
        {
            crd.CanCombineWith = false;
        }
        combinedThreeCards = false;

        combinedCard.TwoComboColor();
    }

    public void ShowCombinedCard()
    {
        if(combinedThreeCards)
        {
            PlayCardRotateAnimation();
        }
        else
        {
            combinedCardAnimator.Play("CombinedCardSlide");

            combinedCard.CardButton.interactable = true;

            combinedCard.CardEffects();

            combinedCard.SelectCombinedCard();
        }

        combinedCard.GetComponent<AudioSource>().clip = AudioManager.instance.CombinedCardAudio;
        if(PlayerPrefs.HasKey("SoundEffects"))
        {
            combinedCard.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SoundEffects");
            combinedCard.GetComponent<AudioSource>().Play();
        }
        else
        {
            combinedCard.GetComponent<AudioSource>().Play();
        }

        combinedCard.CardButton.interactable = true;

        SetCardNavigations();
    }

    private void SetCardNavigations()
    {
        for(int i = 0; i < battleDeck.CardsInHand.Count; i++)
        {
            Navigation nav = battleDeck.CardsInHand[i].GetComponent<Selectable>().navigation;

            nav.selectOnUp = combinedCard.GetComponent<Selectable>();

            battleDeck.CardsInHand[i].GetComponent<Selectable>().navigation = nav;
        }

        Navigation combinedCardNav = combinedCard.GetComponent<Selectable>().navigation;

        combinedCardNav.selectOnDown = battleDeck.CardsInHand[0].GetComponent<Selectable>();

        combinedCard.GetComponent<Selectable>().navigation = combinedCardNav;
    }

    public void ResetCardNavigations()
    {
        for (int i = 0; i < battleDeck.CardsInHand.Count; i++)
        {
            Navigation nav = battleDeck.CardsInHand[i].GetComponent<Selectable>().navigation;

            nav.selectOnUp = null;

            battleDeck.CardsInHand[i].GetComponent<Selectable>().navigation = nav;
        }
    }

    public void CheckSelectedCardNumbers()
    {
        for(int i = 0; i < card.Count; i++)
        {
            card[i].SelectedIndexText.text = (i + 1).ToString();
        }
    }

    public void ResetCards()
    {
        cardOne = null;
        cardTwo = null;
        cardThree = null;

        if(unusedCardOne != null)
        {
            unusedCardOne.IsCompatibleComboCard = false;
        }
        if(unusedCardTwo != null)
        {
            unusedCardTwo.IsCompatibleComboCard = false;
        }
        if(unusedCardThree != null)
        {
            unusedCardThree.IsCompatibleComboCard = false;
        }

        unusedCardOne = null;
        unusedCardTwo = null;
        unusedCardThree = null;

        currentSelectedCards = 0;
    }

    public void CheckCards(Card crd)
    {
        if(currentSelectedCards == 0)
        {
            cardOne = null;
            cardTwo = null;
            cardThree = null;

            unusedCardOne = null;
            unusedCardTwo = null;
            unusedCardThree = null;
        }
        else if(currentSelectedCards == 1)
        {
            cardOne = card[0];
            cardTwo = null;
            cardThree = null;

            unusedCardTwo = null;
            unusedCardThree = null;
        }
        else if(currentSelectedCards == 2)
        {
            cardOne = card[0];
            cardTwo = card[1];
            cardThree = null;

            unusedCardThree = null;
        }
        else if(currentSelectedCards == 3)
        {
            cardThree = card[2];
        }
    }
}