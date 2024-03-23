using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardPickUp : MonoBehaviour
{
    [SerializeField]
    private CardTemplate cardTemplate;

    [SerializeField]
    private FieldCardManager fieldCardManager;

    [SerializeField]
    private Animator gotItemAnimator;

    [SerializeField]
    private Image crystalImage;

    [SerializeField]
    private TextMeshProUGUI gotItemDescription, itemTypeText;

    private MenuCard menuCardPrefab;

    [SerializeField]
    private MeshRenderer meshRenderer;

    [SerializeField]
    private Sprite actionSprite, magicSprite, supportSprite, mysticSprite, itemSprite;

    [SerializeField]
    private int cardIndex;

    [SerializeField]
    private bool destroyCard;

    public bool DestroyCard
    {
        get
        {
            return destroyCard;
        }
        set
        {
            destroyCard = value;
        }
    }

    public TextMeshProUGUI GotItemDescription
    {
        get
        {
            return gotItemDescription;
        }
        set
        {
            gotItemDescription = value;
        }
    }

    public TextMeshProUGUI ItemTypeText
    {
        get
        {
            return itemTypeText;
        }
        set
        {
            itemTypeText = value;
        }
    }

    public Animator GotItemAnimator
    {
        get
        {
            return gotItemAnimator;
        }
        set
        {
            gotItemAnimator = value;
        }
    }

    public CardTemplate _CardTemplate
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

    public MenuCard MenuCardPrefab
    {
        get
        {
            return menuCardPrefab;
        }
        set
        {
            menuCardPrefab = value;
        }
    }

    private void Awake()
    {
        meshRenderer.material = cardTemplate.material;

        CheckCardType();

        menuCardPrefab = MenuController.instance._MenuCard;
    }

    private void CheckCardType()
    {
        switch(cardTemplate.cardType)
        {
            case (CardType.Action):
                crystalImage.sprite = actionSprite;
                break;
            case (CardType.Magic):
                crystalImage.sprite = magicSprite;
                break;
            case (CardType.Support):
                crystalImage.sprite = supportSprite;
                break;
            case (CardType.Mystic):
                crystalImage.sprite = mysticSprite;
                break;
            case (CardType.Item):
                crystalImage.sprite = itemSprite;
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerCollision>())
        {
            if(MenuController.instance.StashCount < GameManager.instance._PlayerMenuInfo.stashLimit)
            {
                GetCard();
                ShowGotItemPanel(false);
            }
            else
            {
                ShowGotItemPanel(true);
            }
        }
    }

    public void GetCard()
    {
        var item = Instantiate(menuCardPrefab);

        item.gameObject.SetActive(true);

        item.NewCardText.SetActive(true);

        item._cardTemplate = cardTemplate;

        item.UpdateCardInformation();

        item.SetDefaultScrollView();

        item.CheckPenalty();

        MenuController.instance._CardTypeCategory.SetCardParent(item);
        MenuController.instance.UpdateStashCount();
        MenuController.instance._CardCollection.CheckCurrentList(item._cardTemplate);

        if(destroyCard)
        {
            GameManager.instance.CreateItemPickUpParticle(transform.position);

            if(NodeManager.instance.CurrentNodeIndex > -1)
            {
                fieldCardManager._FieldCardData.worldCards[NodeManager.instance.WorldIndex].stageCards[fieldCardManager.StageIndex].fieldCards[cardIndex] = 1;
            }
            else
            {
                switch(NodeManager.instance.CurrentNodeIndex)
                {
                    case (-1):
                        fieldCardManager._FieldCardData.worldCards[NodeManager.instance.WorldIndex].secretStageCards[0].fieldCards[cardIndex] = 1;
                        break;
                    case (-2):
                        fieldCardManager._FieldCardData.worldCards[NodeManager.instance.WorldIndex].secretStageCards[1].fieldCards[cardIndex] = 1;
                        break;
                    case (-3):
                        fieldCardManager._FieldCardData.worldCards[NodeManager.instance.WorldIndex].secretStageCards[2].fieldCards[cardIndex] = 1;
                        break;
                }
            }

            MenuController.instance._FieldTreasurePanel.DecrementFieldCardText();

            Destroy(gameObject);
        }
    }

    private void ShowGotItemPanel(bool stashFull)
    {
        gotItemAnimator.Play("GotItem", -1, 0);

        if(!stashFull)
        {
            AudioManager.instance.PlaySoundEffect(AudioManager.instance.GotItemAudio);

            gotItemDescription.color = Color.white;

            gotItemDescription.text = "Obtained\n" + string.Format("\"{0}\"", cardTemplate.cardName);

            itemTypeText.text = "Card";
        }
        else
        {
            AudioManager.instance.PlaySoundEffect(AudioManager.instance.ErrorMessageAudio);

            gotItemDescription.color = Color.red;

            gotItemDescription.text = "Stash is Full!";

            itemTypeText.text = "Message";
        }
    }
}