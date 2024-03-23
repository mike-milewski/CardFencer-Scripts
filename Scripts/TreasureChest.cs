using UnityEngine;
using TMPro;
using System.Collections;

public class TreasureChest : MonoBehaviour
{
    [SerializeField]
    private MainCharacterStats mainCharacterStats;

    [SerializeField]
    private TreasureChestManager treasureChestManager;

    [SerializeField]
    private Animator animator, gotItemAnimator;

    [SerializeField]
    private BoxCollider boxCollider;

    [SerializeField]
    private Transform treasureChestLid;

    [SerializeField]
    private EnableTreasureChest enableTreasureChest;

    [SerializeField]
    private MoonstoneManager moonStoneManager = null;

    [SerializeField]
    private GameObject itemPrefab;

    [SerializeField]
    private CardTemplate cardTemplate;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private StickerInformation stickerInformation;

    [SerializeField]
    private TextMeshProUGUI gotItemDescription, itemTypeText;

    [SerializeField]
    private ParticleSystem itemGotParticle, openChestParticle;

    [SerializeField]
    private int treasureIndex, moonStoneIndex;

    [SerializeField]
    private bool containsMoonStone;

    public EnableTreasureChest _EnableTreasureChest => enableTreasureChest;

    public void OpenChest()
    {
        enableTreasureChest.gameObject.SetActive(false);

        enableTreasureChest.MessagePromptAnimator.Play("FadeOut");

        animator.Play("Open");

        GetItem();

        if(NodeManager.instance.CurrentNodeIndex > -1)
        {
            treasureChestManager._TreasureData.worldTreasures[NodeManager.instance.WorldIndex].stageTreasures[treasureChestManager.StageIndex].treasures[treasureIndex] = 1;
        }
        else
        {
            switch(NodeManager.instance.CurrentNodeIndex)
            {
                case (-1):
                    treasureChestManager._TreasureData.worldTreasures[NodeManager.instance.WorldIndex].secretStageTreasures[0].treasures[treasureIndex] = 1;
                    break;
                case (-2):
                    treasureChestManager._TreasureData.worldTreasures[NodeManager.instance.WorldIndex].secretStageTreasures[1].treasures[treasureIndex] = 1;
                    break;
                case (-3):
                    treasureChestManager._TreasureData.worldTreasures[NodeManager.instance.WorldIndex].secretStageTreasures[2].treasures[treasureIndex] = 1;
                    break;
            }
        }

        MenuController.instance._FieldTreasurePanel.DecrementFieldTreasuresText();

        if (PlayerPrefs.HasKey("SoundEffects"))
        {
            audioSource.volume = PlayerPrefs.GetFloat("SoundEffects");
            audioSource.Play();
        }
        else
        {
            audioSource.Play();
        }
    }

    private void CreateItem()
    {
        var item = Instantiate(itemPrefab);

        item.transform.SetParent(transform, true);

        item.gameObject.SetActive(true);

        item.GetComponent<BoxCollider>().enabled = false;

        item.GetComponent<Animator>().Play("GotItem", -1, 0);

        if(GameManager.instance._WorldEnvironmentData.changedDay)
        {
            item.GetComponent<TreasureItemLight>().EnableLightObject();
        }

        gotItemAnimator.Play("GotItem", -1, 0);

        if(containsMoonStone)
        {
            string gotItem = string.Format("\"{0}\"", "Moonstone");
            gotItemDescription.text = "Obtained\n" + gotItem;
            itemTypeText.text = "Moonstone";
        }
        else
        {
            if (itemPrefab.GetComponent<CardPickUp>())
            {
                string gotSticker = string.Format("\"{0}\"", cardTemplate.cardName);
                gotItemDescription.text = "Obtained\n" + gotSticker;
                itemTypeText.text = "Card";
            }
            else if (itemPrefab.GetComponent<ItemPickUp>())
            {
                string gotItem = string.Format("\"{0}\"", stickerInformation.stickerName);
                gotItemDescription.text = "Obtained\n" + gotItem;
                itemTypeText.text = "Sticker";
            }
        }

        AudioManager.instance.PlaySoundEffect(AudioManager.instance.GotItemAudio);

        StartCoroutine(WaitToPlayParticle(item));

        openChestParticle.gameObject.SetActive(true);
        openChestParticle.Play();
    }

    private void GetItem()
    {
        if(containsMoonStone)
        {
            mainCharacterStats.moonStone++;

            SetMoonStoneData();

            MenuController.instance._FieldTreasurePanel.DecrementFieldMoonstoneText();
        }
        else
        {
            if (itemPrefab.GetComponent<CardPickUp>())
            {
                CardPickUp cardPickUp = itemPrefab.GetComponent<CardPickUp>();

                cardPickUp._CardTemplate = cardTemplate;
                cardPickUp.MenuCardPrefab = GameManager.instance._MenuCard;
                MenuController.instance._CardTypeCategory = GameManager.instance._CardTypeCategory;
                cardPickUp.GotItemDescription = GameManager.instance.GotItemInfoText;
                cardPickUp.ItemTypeText = GameManager.instance.ItemTypeText;

                cardPickUp.DestroyCard = false;

                cardPickUp.GetCard();
            }
            else if (itemPrefab.GetComponent<ItemPickUp>())
            {
                ItemPickUp itemPickUp = itemPrefab.GetComponent<ItemPickUp>();

                itemPickUp._StickerInformation = stickerInformation;
                itemPickUp.StickerPrefab = GameManager.instance._Sticker;
                itemPickUp.StickerListParent = GameManager.instance.StickerListParent;
                itemPickUp.GotItemDescription = GameManager.instance.GotItemInfoText;
                itemPickUp.ItemTypeText = GameManager.instance.ItemTypeText;

                itemPickUp.DestroySticker = false;

                itemPickUp.GetSticker();
            }
        }
    }

    private void SetMoonStoneData()
    {
        if (moonStoneManager != null)
        {
            if (NodeManager.instance.CurrentNodeIndex > -1)
            {
                moonStoneManager._MoonStoneData.worldMoonStones[NodeManager.instance.WorldIndex].stageMoonstones[moonStoneManager.StageIndex].moonStones[moonStoneIndex] = 1;
            }
            else
            {
                switch (NodeManager.instance.CurrentNodeIndex)
                {
                    case (-1):
                        moonStoneManager._MoonStoneData.worldMoonStones[NodeManager.instance.WorldIndex].secretStageMoonstones[0].moonStones[moonStoneIndex] = 1;
                        break;
                    case (-2):
                        moonStoneManager._MoonStoneData.worldMoonStones[NodeManager.instance.WorldIndex].secretStageMoonstones[1].moonStones[moonStoneIndex] = 1;
                        break;
                    case (-3):
                        moonStoneManager._MoonStoneData.worldMoonStones[NodeManager.instance.WorldIndex].secretStageMoonstones[2].moonStones[moonStoneIndex] = 1;
                        break;
                }
            }
        }
    }

    private IEnumerator WaitToPlayParticle(GameObject item)
    {
        yield return new WaitForSecondsRealtime(2.5f);
        itemGotParticle.gameObject.SetActive(true);

        itemGotParticle.Play();

        if(Time.timeScale > 0)
        {
            if (PlayerPrefs.HasKey("SoundEffects"))
            {
                itemGotParticle.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SoundEffects");
                itemGotParticle.GetComponent<AudioSource>().Play();
            }
            else
            {
                itemGotParticle.GetComponent<AudioSource>().Play();
            }
        }

        Destroy(item);

        yield return new WaitForSecondsRealtime(1.2f);

        itemGotParticle.gameObject.SetActive(false);
        openChestParticle.gameObject.SetActive(false);
    }

    public void SetOpenChest()
    {
        animator.enabled = false;

        treasureChestLid.transform.Rotate(new Vector3(90, 0, 0), Space.Self);

        enableTreasureChest.gameObject.SetActive(false);
    }
}