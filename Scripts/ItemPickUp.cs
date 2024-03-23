using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemPickUp : MonoBehaviour
{
    [SerializeField]
    private StickerInformation stickerInformation;

    [SerializeField]
    private Animator gotItemAnimator;

    [SerializeField]
    private Transform stickerListParent;

    [SerializeField]
    private TextMeshProUGUI gotItemDescription, itemTypeText;

    [SerializeField]
    private Sticker stickerPrefab;

    [SerializeField]
    private Image frontStickerImage, backStickerImage;

    [SerializeField]
    private bool destroySticker;

    public bool DestroySticker
    {
        get
        {
            return destroySticker;
        }
        set
        {
            destroySticker = value;
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

    public Transform StickerListParent
    {
        get
        {
            return stickerListParent;
        }
        set
        {
            stickerListParent = value;
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

    public StickerInformation _StickerInformation
    {
        get
        {
            return stickerInformation;
        }
        set
        {
            stickerInformation = value;
        }
    }

    public Sticker StickerPrefab
    {
        get
        {
            return stickerPrefab;
        }
        set
        {
            stickerPrefab = value;
        }
    }

    private void Awake()
    {
        frontStickerImage.sprite = stickerInformation.stickerSprite;
        backStickerImage.sprite = stickerInformation.stickerSprite;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerCollision>())
        {
            GetSticker();
            ShowItemPanel();
        }
    }

    public void GetSticker()
    {
        var item = Instantiate(stickerPrefab);

        item.gameObject.SetActive(true);

        item.NewStickerText.SetActive(true);

        item._stickerInformation = stickerInformation;

        item.SetUpStickerInformation();

        item._StickerPage.SetStickerCategory(item);

        MenuController.instance.CheckStickerAchievement();

        if(destroySticker)
        {
            GameManager.instance.CreateItemPickUpParticle(transform.position);

            Destroy(gameObject);
        }
    }

    private void ShowItemPanel()
    {
        gotItemAnimator.Play("GotItem", -1, 0);

        AudioManager.instance.PlaySoundEffect(AudioManager.instance.GotItemAudio);

        gotItemDescription.text = "Obtained\n" + string.Format("\"{0}\"", stickerInformation.stickerName);

        itemTypeText.text = "Sticker";
    }
}
