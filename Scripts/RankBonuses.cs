using UnityEngine;

public enum RankBonus { ItemSlot, StickerSlot, Armor, HandSize, Card, Sticker };

[System.Serializable]
public class Ranks
{
    [SerializeField]
    private RankBonus rankBonus;

    public RankBonus _rankBonus
    {
        get
        {
            return rankBonus;
        }
        set
        {
            rankBonus = value;
        }
    }
}

public class RankBonuses : MonoBehaviour
{
    [SerializeField]
    private Ranks[] ranks;

    [SerializeField]
    private PlayerMenuInfo playerMenuInfo;

    [SerializeField]
    private MenuController menuController;

    [SerializeField]
    private Sprite itemSlotSprite, stickerSlotSprite, armorSprite, handSizeSprite, cardSprite;

    private string bonusMessage;

    public string BonusMessage
    {
        get
        {
            return bonusMessage;
        }
        set
        {
            bonusMessage = value;
        }
    }
}