using UnityEngine;
using TMPro;

public class StagePenaltyPanel : MonoBehaviour
{
    [SerializeField]
    private Animator penaltyPanel;

    [SerializeField]
    private TextMeshProUGUI penaltyText;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private bool hasPower, hasMagic, hasSupport, hasItem, hasMystic;

    public void ShowPenaltyPanel()
    {
        if(HasPenaltyCardsInDeck())
        {
            string penalty = "";

            penaltyText.text = "";

            penaltyPanel.Play("Show", -1, 0);

            if (PlayerPrefs.HasKey("SoundEffects"))
            {
                audioSource.volume = PlayerPrefs.GetFloat("SoundEffects");
                audioSource.Play();
            }
            else
            {
                audioSource.Play();
            }

            if(hasPower)
            {
                penalty += "<color=#FF3737>Power</color> ";
            }
            if(hasMagic)
            {
                penalty += "<color=#37FFFD>Magic</color> ";
            }
            if(hasSupport)
            {
                penalty += "<color=#37FF59>Support</color> ";
            }
            if(hasItem)
            {
                penalty += "<color=#D1D1D1>Item</color> ";
            }
            if(hasMystic)
            {
                penalty += "<color=#A333D1>Mystic</color> ";
            }

            penaltyText.text = penalty + "Card(s) are still in your deck.";
        }

        SetMenuPenaltyPanel();
    }

    private void SetMenuPenaltyPanel()
    {
        MenuController.instance.StagePenatlyPanel.SetActive(true);
        MenuController.instance.StagePenaltyText.text = "";

        string penalty = "";

        for (int i = 0; i < NodeManager.instance._StagePenalty.Count; i++)
        {
            switch (NodeManager.instance._StagePenalty[i])
            {
                case (StagePenalty.Power):
                    penalty += "<color=#FF3737>Power</color> ";
                    break;
                case (StagePenalty.Magic):
                    penalty += "<color=#37FFFD>Magic</color> ";
                    break;
                case (StagePenalty.Support):
                    penalty += "<color=#37FF59>Support</color> ";
                    break;
                case (StagePenalty.Mystic):
                    penalty += "<color=#A333D1>Mystic</color> ";
                    break;
                case (StagePenalty.Item):
                    penalty += "<color=#D1D1D1>Item</color> ";
                    break;
            }
        }

        MenuController.instance.StagePenaltyText.text = penalty + "Cards";
    }

    private bool HasPenaltyCardsInDeck()
    {
        bool hasCards = false;

        hasPower = false;
        hasMagic = false;
        hasSupport = false;
        hasMystic = false;
        hasItem = false;

        foreach(MenuCard card in MenuController.instance.DeckListContent.GetComponentsInChildren<MenuCard>())
        {
            if(card.ForbiddenIcon.activeSelf)
            {
                switch(card._cardTemplate.cardType)
                {
                    case CardType.Action:
                        hasPower = true;
                        break;
                    case CardType.Magic:
                        hasMagic = true;
                        break;
                    case CardType.Support:
                        hasSupport = true;
                        break;
                    case CardType.Mystic:
                        hasMystic = true;
                        break;
                }

                hasCards = true;
            }
        }

        foreach (MenuCard card in MenuController.instance.ItemList.GetComponentsInChildren<MenuCard>())
        {
            if (card.ForbiddenIcon.activeSelf)
            {
                hasItem = true;

                hasCards = true;
                break;
            }
        }

        return hasCards;
    }

    public void ResetPenaltyAnimator()
    {
        penaltyPanel.Play("Idle", -1, 0);

        GetComponent<CanvasGroup>().alpha = 0;
    }
}