using System.Collections.Generic;
using UnityEngine;

public class StickerPowerManager : MonoBehaviour
{
    [SerializeField][Header("Field Stickers")]
    private List<StickerPower> fieldStickerPowers = new List<StickerPower>();

    [SerializeField][Header("Start Of Battle Stickers")]
    private List<StickerPower> startOfBattleStickerPowers = new List<StickerPower>();

    [SerializeField][Header("Middle Of Battle Stickers")]
    private List<StickerPower> midBattleStickerPowers = new List<StickerPower>();

    public List<StickerPower> FieldStickerPowers => fieldStickerPowers;

    public List<StickerPower> StartOfBattleStickerPowers => startOfBattleStickerPowers;

    public List<StickerPower> MidBattleStickerPowers => midBattleStickerPowers;

    public bool CheckFieldStickerPower(StickerPower stickerPower)
    {
        bool hasPower = false;

        for (int i = 0; i < fieldStickerPowers.Count; i++)
        {
            if (fieldStickerPowers[i] == stickerPower)
            {
                hasPower = true;
            }
        }

        return hasPower;
    }

    public bool CheckStartOfBattleStickerPower(StickerPower stickerPower)
    {
        bool hasPower = false;

        for (int i = 0; i < startOfBattleStickerPowers.Count; i++)
        {
            if (startOfBattleStickerPowers[i] == stickerPower)
            {
                hasPower = true;
            }
        }

        return hasPower;
    }

    public bool CheckMidBattleStickerPower(StickerPower stickerPower)
    {
        bool hasPower = false;

        for (int i = 0; i < midBattleStickerPowers.Count; i++)
        {
            if (midBattleStickerPowers[i] == stickerPower)
            {
                hasPower = true;
            }
        }

        return hasPower;
    }

    public void ClearStickerLists()
    {
        fieldStickerPowers.Clear();
        startOfBattleStickerPowers.Clear();
        midBattleStickerPowers.Clear();
    }
}