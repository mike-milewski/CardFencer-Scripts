using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpoilsManager : MonoBehaviour
{
    public static SpoilsManager instance = null;

    [SerializeField]
    private List<GameObject> battleSpoils = new List<GameObject>();

    [SerializeField]
    private Transform[] itemParents;

    [SerializeField]
    private PlayerMenuInfo playerMenuInfo;

    [SerializeField]
    private Animator spoilsPanelAnimator;

    [SerializeField]
    private TextMeshProUGUI spoilsTitleText, spoilsInfoText, stickerStatusInfo, stickerStatusDescription, statusDurationText, stickerCostText;

    [SerializeField]
    private GameObject durationPanel, stickerCostFrame;

    [SerializeField]
    private Image stickerStatusImage;

    private MenuCard cardToEarn;

    private Sticker stickerToEarn;

    private int spoilsIndex;

    public List<GameObject> BattleSpoils => battleSpoils;

    public Image StickerStatusImage
    {
        get
        {
            return stickerStatusImage;
        }
        set
        {
            stickerStatusImage = value;
        }
    }

    public GameObject DurationPanel => durationPanel;

    public GameObject StickerCostFrame => stickerCostFrame;

    public TextMeshProUGUI StickerStatusInfo
    {
        get
        {
            return stickerStatusInfo;
        }
        set
        {
            stickerStatusInfo = value;
        }
    }

    public TextMeshProUGUI StickerStatusDescription
    {
        get
        {
            return stickerStatusDescription;
        }
        set
        {
            stickerStatusDescription = value;
        }
    }

    public TextMeshProUGUI SpoilsTitleText
    {
        get
        {
            return spoilsTitleText;
        }
        set
        {
            spoilsTitleText = value;
        }
    }

    public TextMeshProUGUI SpoilsInfoText
    {
        get
        {
            return spoilsInfoText;
        }
        set
        {
            spoilsInfoText = value;
        }
    }

    public TextMeshProUGUI StatusDurationText
    {
        get
        {
            return statusDurationText;
        }
        set
        {
            statusDurationText = value;
        }
    }
    
    public TextMeshProUGUI StickerCostText
    {
        get
        {
            return stickerCostText;
        }
        set
        {
            stickerCostText = value;
        }
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        cardToEarn = MenuController.instance.CardToWinInBattle;
        stickerToEarn = MenuController.instance.StickerToWinInBattle;
    }

    public void AddItem(bool isCard, CardTemplate cardTemplate, StickerInformation stickerInfo)
    {
        itemParents[spoilsIndex].gameObject.SetActive(true);

        if (isCard)
        {
            var card = Instantiate(cardToEarn, itemParents[spoilsIndex]);

            card.SpoilsInfoAnimator = spoilsPanelAnimator;

            card.gameObject.SetActive(true);

            card._cardTemplate = cardTemplate;

            card.UpdateCardInformation();

            battleSpoils.Add(card.gameObject);
        }
        else
        {
            var sticker = Instantiate(stickerToEarn, itemParents[spoilsIndex]);

            sticker.SpoilsInfoPanelAnimator = spoilsPanelAnimator;

            sticker.gameObject.SetActive(true);

            sticker.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);

            sticker._stickerInformation = stickerInfo;

            sticker.SetUpStickerInformation();

            battleSpoils.Add(sticker.gameObject);
        }

        spoilsIndex++;
    }

    public void ReceiveItems()
    {
        for(int i = 0; i < battleSpoils.Count; i++)
        {
            if(battleSpoils[i].GetComponent<MenuCard>())
            {
                if(MenuController.instance.StashCount < playerMenuInfo.stashLimit)
                {
                    MenuCard card = battleSpoils[i].GetComponent<MenuCard>();

                    var obtainedCard = Instantiate(MenuController.instance._MenuCard);

                    obtainedCard.gameObject.SetActive(true);

                    obtainedCard.NewCardText.SetActive(true);

                    obtainedCard._cardTemplate = card._cardTemplate;

                    obtainedCard.UpdateCardInformation();
                    obtainedCard.SetDefaultScrollView();
                    obtainedCard.CheckPenalty();

                    MenuController.instance._CardTypeCategory.SetCardParent(obtainedCard);
                    MenuController.instance.UpdateStashCount();
                    MenuController.instance._CardCollection.CheckCurrentList(obtainedCard._cardTemplate);
                }
            }
            else
            {
                Sticker sticker = battleSpoils[i].GetComponent<Sticker>();

                var obtainedSticker = Instantiate(MenuController.instance.StickerPrefab);

                obtainedSticker.gameObject.SetActive(true);

                obtainedSticker.NewStickerText.SetActive(true);

                obtainedSticker._stickerInformation = sticker._stickerInformation;

                obtainedSticker.SetUpStickerInformation();

                MenuController.instance._StickerPage.SetStickerCategory(obtainedSticker);
                MenuController.instance.CheckStickerAchievement();
            }
        }
    }
}