using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Rank
{
    [SerializeField]
    private RankBonus[] rankBonus;

    [SerializeField]
    private Sprite itemSlotSprite, stickerSlotSprite, armorSprite, handSizeSprite, cardSprite;

    [SerializeField]
    private CardTemplate rankCard;

    [SerializeField]
    private StickerInformation rankSticker;

    private string bonusMessage;

    public Sprite SetBonus(int index)
    {
        Sprite sprite = null;

        switch (rankBonus[index])
        {
            case (RankBonus.ItemSlot):
                sprite = itemSlotSprite;
                bonusMessage = "Item Slot\nUnlocked!";
                MenuController.instance.UnlockItemSlot();
                break;
            case (RankBonus.StickerSlot):
                sprite = stickerSlotSprite;
                bonusMessage = "Sticker Slot\nUnlocked!";
                MenuController.instance.UnlockStickerSlot();
                break;
            case (RankBonus.Armor):
                sprite = armorSprite;
                bonusMessage = "Armor\nUnlocked!";
                MenuController.instance.UnlockArmor();
                break;
            case (RankBonus.HandSize):
                sprite = handSizeSprite;
                bonusMessage = "Hand Size\nUnlocked!";
                MenuController.instance.UnlockHandSize();
                break;
            case (RankBonus.Card):
                sprite = cardSprite;
                bonusMessage = "Card\nUnlocked!";
                MenuController.instance.UnlockCard(rankCard);
                break;
            case (RankBonus.Sticker):
                sprite = stickerSlotSprite;
                bonusMessage = "Sticker\nUnlocked!";
                MenuController.instance.UnlockSticker(rankSticker);
                break;
        }

        return sprite;
    }

    public string BonusMessage => bonusMessage;

    public RankBonus[] _rankBonus => rankBonus;
}

[CreateAssetMenu(fileName = "PlayerMenu", menuName = "ScriptableObjects/PlayerMenu", order = 1)]
public class PlayerMenuInfo : ScriptableObject
{
    public Rank[] _rank;

    [Header("Deck")]
    public int minimumDeckSize;
    public int currentDeckLimit;
    public int maximumDeckLimit;

    [Header("Hand Size")]
    public int currentHandSize;
    public int maximumHandSize;

    [Header("Sticker Slot")]
    public int equippedStickers;
    public int currentStickerSlotSize;
    public int maximumStickerSlotSize;
    public List<CardTemplate> currentEquippedItems = new List<CardTemplate>();

    [Header("Item Slot")]
    public int equippedItems;
    public int currentItemSlotSize;
    public int maximumItemSlotSize;

    [Header("Mystic Cards")]
    public int currentlyEquippedMysticCards;
    public int maximumMysticCards;

    [Header("Stash Limit")]
    public int stashLimit;

    [Header("Weapon")]
    public int weaponIndex;

    public string[] weaponName;

    [TextArea]
    public string[] weaponDescription;

    public Sprite[] weaponSprite;

    [Header("Shield")]
    public int shieldIndex;

    public string[] shieldName;

    [TextArea]
    public string[] shieldDescription;

    public Sprite[] shieldSprite;

    [Header("Armor")]
    public int armorIndex;

    public string[] armorName;

    [TextArea]
    public string[] armorDescription;

    public Sprite[] armorSprite;

    [Header("Rank")]
    public int rankIndex, levelIndex;

    public int[] levelRequirement;

    public string[] rankName;

    public Sprite[] rankSprites;
}